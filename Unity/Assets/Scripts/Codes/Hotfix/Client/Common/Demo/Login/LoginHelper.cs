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
                EventSystem.Instance.Publish(clientScene, new ClientEventType.NoticeUIShowCommonLoading(){bForce = true});

                // 创建一个ETModel层的Session
                clientScene.RemoveComponent<RouterAddressComponent>();
                clientScene.RemoveComponent<NetClientComponent>();
                // 获取路由跟realmDispatcher地址
                RouterAddressComponent routerAddressComponent = clientScene.GetComponent<RouterAddressComponent>();
                if (routerAddressComponent == null)
                {
                    string RouterHttpHost = ConstValue.RouterHttpHost;
                    int RouterHttpPort = ConstValue.RouterHttpPort;

                    (RouterHttpHost, RouterHttpPort) =
                        EventSystem.Instance.Invoke<ConfigComponent.GetRouterHttpHostAndPort, (string, int)>(
                            new ConfigComponent.GetRouterHttpHostAndPort());

                    Log.Debug($"===ET.Client.LoginHelper.Login RouterHttpHost[{RouterHttpHost}] RouterHttpPort[{RouterHttpPort}]");
                    routerAddressComponent = clientScene.AddComponent<RouterAddressComponent, string, int>(RouterHttpHost, RouterHttpPort);
                    await routerAddressComponent.Init();

                    if (clientScene.GetComponent<NetClientComponent>() != null)
                    {
                        return (false, "");
                    }
                    clientScene.AddComponent<NetClientComponent, AddressFamily>(routerAddressComponent.RouterManagerIPAddress.AddressFamily);
                }

                IPEndPoint realmAddress = routerAddressComponent.GetRealmAddress(account);

                Log.Debug(realmAddress.Address + "+++++++++++++++++");

                R2C_Login r2CLogin;
                using (Session session = await RouterHelper.CreateRouterSession(clientScene, realmAddress))
                {
                    r2CLogin = (R2C_Login)await session.Call(new C2R_Login() { Account = account, Password = password, LoginType = (int)loginType });
                }

                if (r2CLogin.Error == ET.ErrorCode.ERR_LogicError)
                {
                    string msg = r2CLogin.Message;
                    Log.Error(msg);

                    return (false, msg);
                }

                bool IsFirstLogin = r2CLogin.IsFirstLogin == 1? true : false;

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
                    new C2G_LoginGate()
                    {
                        Key = r2CLogin.Key,
                        GateId = r2CLogin.GateId,
                        IsFirstLogin = IsFirstLogin?1:0,
                    });
                if (g2CLoginGate.Error == ET.ErrorCode.ERR_LogicError)
                {
                    string msg = g2CLoginGate.Message;
                    Log.Error(msg);

                    return (false, msg);
                }

                byte[] byts = g2CLoginGate.PlayerComponentBytes;
                Entity playerEntity = MongoHelper.Deserialize<Entity>(byts);
                ET.Client.PlayerStatusHelper.RefreshMyPlayer(clientScene, playerEntity);
                byts = g2CLoginGate.PlayerStatusComponentBytes;
                Entity playerStatusEntity = MongoHelper.Deserialize<Entity>(byts);
                ET.Client.PlayerStatusHelper.RefreshMyPlayerStatus(clientScene, playerStatusEntity);

                Log.Debug("登陆gate成功!");

                long myPlayerId = ET.Client.PlayerStatusHelper.GetMyPlayerId(clientScene);
                EventSystem.Instance.Publish(clientScene, new ClientEventType.NoticeEventLoggingLoginIn() { playerId = myPlayerId });

                PlayerBaseInfoComponent playerBaseInfoComponent = await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(clientScene);
                string accountType = "guess";
                if (playerBaseInfoComponent.BindLoginType == LoginType.Editor)
                {
                }
                else if (playerBaseInfoComponent.BindLoginType == LoginType.GoogleSDK)
                {
                    accountType = "google";
                }
                else if (playerBaseInfoComponent.BindLoginType == LoginType.AppleSDK)
                {
                    accountType = "apple";
                }

                EventSystem.Instance.Publish(clientScene,
                    new ClientEventType.NoticeEventLogging()
                    {
                        eventName = "RoleLoggedIn", properties = new() { { "first_login", IsFirstLogin }, { "account_type", accountType }, }
                    });

                EventSystem.Instance.Publish(clientScene,
                    new ClientEventType.NoticeEventLoggingSetCommonProperties() { properties = new() { { "account_type", loginType.ToString() }, } });

                Log.Debug("PlayerCacheHelper.GetMyPlayerModelAll before");
                await ET.Client.PlayerCacheHelper.GetMyPlayerModelAll(clientScene);
                Log.Debug("PlayerCacheHelper.GetMyPlayerModelAll after");


                if (IsFirstLogin)
                {
                    EventSystem.Instance.Publish(clientScene, new ClientEventType.NoticeEventLoggingSetUserProperties()
                    {
                        properties = new()
                        {
                            //{"account_id", account},
                            //{"role_id", playerBaseInfoComponent.GetPlayerId()},
                            //{"role_name", playerBaseInfoComponent.GetPlayerName()},
                            { "artd_first_login_dt", TimeHelper.ClientNow() },
                            { "artd_infinity_max_num", playerBaseInfoComponent.GetEndlessChallengeScore() },
                            { "artd_account_icon_id_code", playerBaseInfoComponent.GetIconIndex() },
                            //{"account_type", loginType.ToString()},
                        }
                    });
                }
                else
                {
                    EventSystem.Instance.Publish(clientScene, new ClientEventType.NoticeEventLoggingSetUserProperties()
                    {
                        properties = new()
                        {
                            //{"account_id", account},
                            //{"role_id", playerBaseInfoComponent.GetPlayerId()},
                            //{"role_name", playerBaseInfoComponent.GetPlayerName()},
                            { "artd_infinity_max_num", playerBaseInfoComponent.GetEndlessChallengeScore() },
                            { "artd_account_icon_id_code", playerBaseInfoComponent.GetIconIndex() },
                            //{"account_type", loginType.ToString()},
                        }
                    });
                }

                EventSystem.Instance.Publish(clientScene, new ClientEventType.NoticeUIHideCommonLoading() { bForce = true });

                await EventSystem.Instance.PublishAsync(clientScene, new ClientEventType.LoginFinish());
                return (true, "");
            }
            catch (Exception e)
            {
                Log.Error(e);
                return (false, e.Message);
            }
        }

        public static async ETTask<(bool, string)> LoginWithAuth(Scene clientScene, string account, string password, LoginType loginType, string name,
        string token, string email)
        {
            try
            {
                EventSystem.Instance.Publish(clientScene, new ClientEventType.NoticeUIShowCommonLoading() { bForce = true });

                // 创建一个ETModel层的Session
                clientScene.RemoveComponent<RouterAddressComponent>();
                clientScene.RemoveComponent<NetClientComponent>();
                // 获取路由跟realmDispatcher地址
                RouterAddressComponent routerAddressComponent = clientScene.GetComponent<RouterAddressComponent>();
                if (routerAddressComponent == null)
                {
                    string RouterHttpHost = ConstValue.RouterHttpHost;
                    int RouterHttpPort = ConstValue.RouterHttpPort;

                    (RouterHttpHost, RouterHttpPort) =
                        EventSystem.Instance.Invoke<ConfigComponent.GetRouterHttpHostAndPort, (string, int)>(
                            new ConfigComponent.GetRouterHttpHostAndPort());

                    Log.Debug($"===ET.Client.LoginHelper.Login RouterHttpHost[{RouterHttpHost}] RouterHttpPort[{RouterHttpPort}]");
                    routerAddressComponent = clientScene.AddComponent<RouterAddressComponent, string, int>(RouterHttpHost, RouterHttpPort);
                    await routerAddressComponent.Init();

                    clientScene.AddComponent<NetClientComponent, AddressFamily>(routerAddressComponent.RouterManagerIPAddress.AddressFamily);
                }

                IPEndPoint realmAddress = routerAddressComponent.GetRealmAddress(account);

                Log.Debug(realmAddress.Address + "+++++++++++++++++");

                R2C_LoginWithAuth r2CLogin;
                using (Session session = await RouterHelper.CreateRouterSession(clientScene, realmAddress))
                {
                    r2CLogin = (R2C_LoginWithAuth)await session.Call(new C2R_LoginWithAuth()
                    {
                        Account = account,
                        Password = password,
                        LoginType = (int)loginType,
                        Name = name,
                        Token = token
                    });
                }

                if (r2CLogin.Error == ET.ErrorCode.ERR_LogicError)
                {
                    string msg = r2CLogin.Message;
                    Log.Error(msg);

                    return (false, msg);
                }

                bool IsFirstLogin = r2CLogin.IsFirstLogin == 1? true : false;

                // 创建一个gate Session,并且保存到SessionComponent中
                Log.Debug(r2CLogin.Address + "+++++++++++++++++");
                Session gateSession = await RouterHelper.CreateRouterSession(clientScene, NetworkHelper.ToIPEndPoint(r2CLogin.Address));
                SessionComponent sessionComponent = ET.Client.SessionHelper.GetSessionCompent(clientScene);
                if (sessionComponent == null)
                {
                    sessionComponent = clientScene.AddComponent<SessionComponent>();
                }

                if (sessionComponent.Session != null)
                {
                    sessionComponent.Session.Dispose();
                }

                sessionComponent.Session = gateSession;

                G2C_LoginGate g2CLoginGate = (G2C_LoginGate)await gateSession.Call(
                    new C2G_LoginGate()
                    {
                        Key = r2CLogin.Key,
                        GateId = r2CLogin.GateId,
                        IsFirstLogin = IsFirstLogin?1:0,
                    });

                if (g2CLoginGate.Error == ET.ErrorCode.ERR_LogicError)
                {
                    string msg = g2CLoginGate.Message;
                    Log.Error(msg);

                    return (false, msg);
                }

                byte[] byts = g2CLoginGate.PlayerComponentBytes;
                Entity playerEntity = MongoHelper.Deserialize<Entity>(byts);
                ET.Client.PlayerStatusHelper.RefreshMyPlayer(clientScene, playerEntity);
                byts = g2CLoginGate.PlayerStatusComponentBytes;
                Entity playerStatusEntity = MongoHelper.Deserialize<Entity>(byts);
                ET.Client.PlayerStatusHelper.RefreshMyPlayerStatus(clientScene, playerStatusEntity);

                Log.Debug("登陆gate成功!");

                long myPlayerId = ET.Client.PlayerStatusHelper.GetMyPlayerId(clientScene);
                EventSystem.Instance.Publish(clientScene, new ClientEventType.NoticeEventLoggingLoginIn() { playerId = myPlayerId });

                EventSystem.Instance.Publish(clientScene,
                    new ClientEventType.NoticeEventLoggingSetCommonProperties() { properties = new() { { "account_type", loginType.ToString() }, } });

                await ET.Client.PlayerCacheHelper.GetMyPlayerModelAll(clientScene);

                PlayerBaseInfoComponent playerBaseInfoComponent = await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(clientScene);

                if (IsFirstLogin)
                {
                    if (playerBaseInfoComponent.PlayerName != name)
                    {
                        playerBaseInfoComponent.PlayerName = name;
                        await ET.Client.PlayerCacheHelper.SaveMyPlayerModel(clientScene, PlayerModelType.BaseInfo, new() { "PlayerName" });
                    }

                    EventSystem.Instance.Publish(clientScene, new ClientEventType.NoticeEventLoggingSetUserProperties()
                    {
                        properties = new()
                        {
                            //{"account_id", account},
                            //{"role_id", playerBaseInfoComponent.GetPlayerId()},
                            //{"role_name", playerBaseInfoComponent.GetPlayerName()},
                            { "artd_first_login_dt", TimeHelper.ClientNow() },
                            { "artd_infinity_max_num", playerBaseInfoComponent.GetEndlessChallengeScore() },
                            { "artd_account_icon_id_code", playerBaseInfoComponent.GetIconIndex() },
                            //{"account_type", loginType.ToString()},
                        }
                    });
                }
                else
                {
                    EventSystem.Instance.Publish(clientScene, new ClientEventType.NoticeEventLoggingSetUserProperties()
                    {
                        properties = new()
                        {
                            //{"account_id", account},
                            //{"role_id", playerBaseInfoComponent.GetPlayerId()},
                            //{"role_name", playerBaseInfoComponent.GetPlayerName()},
                            { "artd_infinity_max_num", playerBaseInfoComponent.GetEndlessChallengeScore() },
                            { "artd_account_icon_id_code", playerBaseInfoComponent.GetIconIndex() },
                            //{"account_type", loginType.ToString()},
                        }
                    });
                }

                if (playerBaseInfoComponent.BindLoginType != loginType)
                {
                    playerBaseInfoComponent.BindLoginType = loginType;
                    playerBaseInfoComponent.BindEmail = email;
                    await ET.Client.PlayerCacheHelper.SaveMyPlayerModel(clientScene, PlayerModelType.BaseInfo,
                        new() { "BindLoginType", "BindEmail" });
                }

                string accountType = "guess";
                if (playerBaseInfoComponent.BindLoginType == LoginType.Editor)
                {
                }
                else if (playerBaseInfoComponent.BindLoginType == LoginType.GoogleSDK)
                {
                    accountType = "google";
                }
                else if (playerBaseInfoComponent.BindLoginType == LoginType.AppleSDK)
                {
                    accountType = "apple";
                }

                EventSystem.Instance.Publish(clientScene,
                    new ClientEventType.NoticeEventLogging()
                    {
                        eventName = "RoleLoggedIn", properties = new() { { "first_login", IsFirstLogin }, { "account_type", accountType }, }
                    });

                EventSystem.Instance.Publish(clientScene, new ClientEventType.NoticeUIHideCommonLoading() { bForce = true });

                await EventSystem.Instance.PublishAsync(clientScene, new ClientEventType.LoginFinish());
                return (true, "");
            }
            catch (Exception e)
            {
                Log.Error(e);
                return (false, e.Message);
            }
        }

        public static async ETTask LoginOut(Scene clientScene, bool isNeedLoginOutAccount = false)
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

                await EventSystem.Instance.PublishAsync(clientScene,
                    new ClientEventType.LoginOutFinish() { isNeedLoginOutAccount = isNeedLoginOutAccount, });

                await SceneHelper.EnterLogin(clientScene, false);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public static async ETTask<(bool, string)> ReLogin(Scene clientScene)
        {
            try
            {
                SessionComponent sessionComponent = ET.Client.SessionHelper.GetSessionCompent(clientScene);
                if (sessionComponent == null)
                {
                    return (false, "sessionComponent == null");
                }

                if (sessionComponent.Session == null)
                {
                    return (false, "sessionComponent.Session == null");
                }

                Session sessionOld = sessionComponent.Session;
                IPEndPoint gateAddress = sessionComponent.Session.RemoteAddress;

                clientScene.RemoveComponent<NetClientComponent>();
                // 获取路由跟realmDispatcher地址
                RouterAddressComponent routerAddressComponent = clientScene.GetComponent<RouterAddressComponent>();
                clientScene.AddComponent<NetClientComponent, AddressFamily>(routerAddressComponent.RouterManagerIPAddress.AddressFamily);

                // 创建一个gate Session,并且保存到SessionComponent中
                Session gateSession = await RouterHelper.CreateRouterSession(clientScene, gateAddress);
                sessionComponent.Session = gateSession;

                long myPlayerId = ET.Client.PlayerStatusHelper.GetMyPlayerId(clientScene);
                G2C_ReLoginGate _G2C_ReLoginGate = (G2C_ReLoginGate)await gateSession.Call(new C2G_ReLoginGate() { PlayerId = myPlayerId });
                if (_G2C_ReLoginGate.Error == ET.ErrorCode.ERR_LogicError)
                {
                    string msg = _G2C_ReLoginGate.Message;
                    Log.Error(msg);

                    return (false, $"ReLoginGate Fail: {msg}");
                }

                if (sessionOld != null)
                {
                    sessionOld.Dispose();
                }

                await EventSystem.Instance.PublishAsync(clientScene, new ClientEventType.ReLoginFinish());

                Log.Debug("重现登陆gate成功!");

                return (true, "");
            }
            catch (Exception e)
            {
                Log.Error(e);
                return (false, e.Message);
            }
        }

        public static async ETTask<(bool, string)> BindAccountWithAuth(Scene clientScene, string account, string bindAccount, LoginType loginType,
        string name, string token, string email)
        {
            try
            {
                G2C_BindAccountWithAuth _G2C_BindAccountWithAuth = await ET.Client.SessionHelper.GetSession(clientScene).Call(
                    new C2G_BindAccountWithAuth()
                    {
                        Account = account,
                        BindAccount = bindAccount,
                        LoginType = (int)loginType,
                        Name = name,
                        Token = token,
                    }) as G2C_BindAccountWithAuth;

                if (_G2C_BindAccountWithAuth.Error != ET.ErrorCode.ERR_Success)
                {
                    Log.Error($"ET.Client.LoginHelper.BindAccountWithAuth Error==1 msg={_G2C_BindAccountWithAuth.Message}");
                    return (false, _G2C_BindAccountWithAuth.Message);
                }

                if (_G2C_BindAccountWithAuth.IsBindSuccess == 1)
                {
                    PlayerBaseInfoComponent playerBaseInfoComponent = await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(clientScene);

                    playerBaseInfoComponent.PlayerName = name;
                    playerBaseInfoComponent.BindLoginType = loginType;
                    playerBaseInfoComponent.BindEmail = email;
                    await ET.Client.PlayerCacheHelper.SaveMyPlayerModel(clientScene, PlayerModelType.BaseInfo,
                        new() { "PlayerName", "BindLoginType", "BindEmail" });

                    return (true, "");
                }
                else
                {
                    return (false, LocalizeComponent.Instance.GetTextValue("TextCode_Key_BindAccount_BindFail"));
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
                return (false, e.Message);
            }
        }
    }
}