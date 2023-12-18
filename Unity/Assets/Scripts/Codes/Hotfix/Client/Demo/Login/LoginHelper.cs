using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace ET.Client
{
    public static class LoginHelper
    {
        public static async ETTask<(bool, string)> Login(Scene clientScene, string account, string password, LoginType loginType)
        {
            try
            {
                // 创建一个ETModel层的Session
                clientScene.RemoveComponent<RouterAddressComponent>();
                clientScene.RemoveComponent<NetClientComponent>();
                // 获取路由跟realmDispatcher地址
                RouterAddressComponent routerAddressComponent = clientScene.GetComponent<RouterAddressComponent>();
                if (routerAddressComponent == null)
                {
                    string RouterHttpHost = ConstValue.RouterHttpHost;
                    int RouterHttpPort = ConstValue.RouterHttpPort;

                    (RouterHttpHost, RouterHttpPort) = EventSystem.Instance.Invoke<ConfigComponent.GetRouterHttpHostAndPort, (string, int)>(new ConfigComponent.GetRouterHttpHostAndPort());

                    Log.Debug($"===ET.Client.LoginHelper.Login RouterHttpHost[{RouterHttpHost}] RouterHttpPort[{RouterHttpPort}]");
                    routerAddressComponent = clientScene.AddComponent<RouterAddressComponent, string, int>(RouterHttpHost, RouterHttpPort);
                    await routerAddressComponent.Init();

                    clientScene.AddComponent<NetClientComponent, AddressFamily>(routerAddressComponent.RouterManagerIPAddress.AddressFamily);
                }
                IPEndPoint realmAddress = routerAddressComponent.GetRealmAddress(account);

                Log.Debug(realmAddress.Address + "+++++++++++++++++");

                R2C_Login r2CLogin;
                using (Session session = await RouterHelper.CreateRouterSession(clientScene, realmAddress))
                {
                    r2CLogin = (R2C_Login) await session.Call(new C2R_Login() { Account = account, Password = password, LoginType = (int)loginType });
                }

                if (r2CLogin.Error == ET.ErrorCode.ERR_LogicError)
                {
                    string msg = r2CLogin.Message;
                    Log.Error(msg);

                    return (false, msg);
                }

                bool IsFirstLogin = r2CLogin.IsFirstLogin == 1?true:false;

                // 创建一个gate Session,并且保存到SessionComponent中
                Log.Debug(r2CLogin.Address + "+++++++++++++++++");
                Session gateSession = await RouterHelper.CreateRouterSession(clientScene, NetworkHelper.ToIPEndPoint(r2CLogin.Address));
                SessionComponent sessionComponent = ET.Client.SessionHelper.GetSessionCompent(clientScene);
                if (sessionComponent == null)
                {
                    sessionComponent = clientScene.AddComponent<SessionComponent>();
                }
                sessionComponent.Session = gateSession;

                G2C_LoginGate g2CLoginGate = (G2C_LoginGate)await gateSession.Call(
                    new C2G_LoginGate() { Key = r2CLogin.Key, GateId = r2CLogin.GateId});

                byte[] byts = g2CLoginGate.PlayerComponentBytes;
                Entity playerEntity = MongoHelper.Deserialize<Entity>(byts);
                ET.Client.PlayerHelper.RefreshMyPlayer(clientScene, playerEntity);
                byts = g2CLoginGate.PlayerStatusComponentBytes;
                Entity playerStatusEntity = MongoHelper.Deserialize<Entity>(byts);
                ET.Client.PlayerHelper.RefreshMyPlayerStatus(clientScene, playerStatusEntity);

                Log.Debug("登陆gate成功!");

                long myPlayerId = ET.Client.PlayerHelper.GetMyPlayerId(clientScene);
                EventSystem.Instance.Publish(clientScene, new EventType.NoticeEventLoggingLoginIn() { playerId = myPlayerId});

                EventSystem.Instance.Publish(clientScene, new EventType.NoticeEventLogging()
                {
                    eventName = "RoleLoggedIn",
                    properties = new()
                    {
                        {"first_login", IsFirstLogin},
                    }
                });

                EventSystem.Instance.Publish(clientScene, new EventType.NoticeEventLoggingSetCommonProperties()
                {
                    properties = new()
                    {
                        {"account_type", loginType.ToString()},
                    }
                });

                await ET.Client.PlayerCacheHelper.GetMyPlayerModelAll(clientScene);

                PlayerBaseInfoComponent playerBaseInfoComponent = await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(clientScene);
                if (IsFirstLogin)
                {
                    EventSystem.Instance.Publish(clientScene, new EventType.NoticeEventLoggingSetUserProperties()
                    {
                        properties = new()
                        {
                            //{"account_id", account},
                            //{"role_id", playerBaseInfoComponent.GetPlayerId()},
                            //{"role_name", playerBaseInfoComponent.GetPlayerName()},
                            {"artd_first_login_dt", TimeHelper.ClientNow()},
                            {"artd_infinity_max_num", playerBaseInfoComponent.GetEndlessChallengeScore()},
                            {"artd_account_icon_id_code", playerBaseInfoComponent.GetIconIndex()},
                            //{"account_type", loginType.ToString()},
                        }
                    });
                }
                else
                {
                    EventSystem.Instance.Publish(clientScene, new EventType.NoticeEventLoggingSetUserProperties()
                    {
                        properties = new()
                        {
                            //{"account_id", account},
                            //{"role_id", playerBaseInfoComponent.GetPlayerId()},
                            //{"role_name", playerBaseInfoComponent.GetPlayerName()},
                            {"artd_infinity_max_num", playerBaseInfoComponent.GetEndlessChallengeScore()},
                            {"artd_account_icon_id_code", playerBaseInfoComponent.GetIconIndex()},
                            //{"account_type", loginType.ToString()},
                        }
                    });
                }

                await EventSystem.Instance.PublishAsync(clientScene, new EventType.LoginFinish()
                {
                });
                return (true, "");
            }
            catch (Exception e)
            {
                Log.Error(e);
                return (false, e.Message);
            }
        }

        public static async ETTask LoginOut(Scene clientScene)
        {
            try
            {
                try
                {
                    Session gateSession = ET.Client.SessionHelper.GetSession(clientScene);
                    gateSession.Send(new C2G_LoginOut());
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }

                clientScene.RemoveComponent<RouterAddressComponent>();
                clientScene.RemoveComponent<NetClientComponent>();
                clientScene.RemoveComponent<SessionComponent>();

                await EventSystem.Instance.PublishAsync(clientScene, new EventType.LoginOutFinish());

                await SceneHelper.EnterLogin(clientScene, false);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public static async ETTask ReLogin(Scene clientScene, string account, string password)
        {
            try
            {
                // 创建一个ETModel层的Session
                //clientScene.RemoveComponent<RouterAddressComponent>();
                // 获取路由跟realmDispatcher地址
                RouterAddressComponent routerAddressComponent = clientScene.GetComponent<RouterAddressComponent>();
                if (routerAddressComponent == null)
                {
                    string RouterHttpHost = ConstValue.RouterHttpHost;
                    int RouterHttpPort = ConstValue.RouterHttpPort;

                    (RouterHttpHost, RouterHttpPort) = EventSystem.Instance.Invoke<ConfigComponent.GetRouterHttpHostAndPort, (string, int)>(new ConfigComponent.GetRouterHttpHostAndPort());

                    routerAddressComponent = clientScene.AddComponent<RouterAddressComponent, string, int>(RouterHttpHost, RouterHttpPort);
                    await routerAddressComponent.Init();

                    clientScene.AddComponent<NetClientComponent, AddressFamily>(routerAddressComponent.RouterManagerIPAddress.AddressFamily);
                }
                IPEndPoint realmAddress = routerAddressComponent.GetRealmAddress(account);

                R2C_Login r2CLogin;
                using (Session session = await RouterHelper.CreateRouterSession(clientScene, realmAddress))
                {
                    r2CLogin = (R2C_Login) await session.Call(new C2R_Login() { Account = account, Password = password });
                }

                // 创建一个gate Session,并且保存到SessionComponent中
                Session gateSession = await RouterHelper.CreateRouterSession(clientScene, NetworkHelper.ToIPEndPoint(r2CLogin.Address));
                clientScene.AddComponent<SessionComponent>().Session = gateSession;

                G2C_LoginGate g2CLoginGate = (G2C_LoginGate)await gateSession.Call(
                    new C2G_LoginGate() { Key = r2CLogin.Key, GateId = r2CLogin.GateId});

                byte[] byts = g2CLoginGate.PlayerComponentBytes;
                Entity playerEntity = MongoHelper.Deserialize<Entity>(byts);
                ET.Client.PlayerHelper.RefreshMyPlayer(clientScene, playerEntity);
                byts = g2CLoginGate.PlayerStatusComponentBytes;
                Entity playerStatusEntity = MongoHelper.Deserialize<Entity>(byts);
                ET.Client.PlayerHelper.RefreshMyPlayerStatus(clientScene, playerStatusEntity);

                Log.Debug("登陆gate成功!");

                await ET.Client.PlayerCacheHelper.GetMyPlayerModelAll(clientScene);

                await EventSystem.Instance.PublishAsync(clientScene, new EventType.LoginFinish());
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
}