using System;
using System.Net;
using System.Net.Sockets;

namespace ET.Client
{
    public static class LoginHelper
    {
        public static async ETTask Login(Scene clientScene, string account, string password)
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

                R2C_Login r2CLogin;
                using (Session session = await RouterHelper.CreateRouterSession(clientScene, realmAddress))
                {
                    r2CLogin = (R2C_Login) await session.Call(new C2R_Login() { Account = account, Password = password });
                }

                // 创建一个gate Session,并且保存到SessionComponent中
                Session gateSession = await RouterHelper.CreateRouterSession(clientScene, NetworkHelper.ToIPEndPoint(r2CLogin.Address));
                SessionComponent sessionComponent = ET.Client.SessionHelper.GetSessionCompent(clientScene);
                if (sessionComponent == null)
                {
                    sessionComponent = clientScene.AddComponent<SessionComponent>();
                }
                sessionComponent.Session = gateSession;

                G2C_LoginGate g2CLoginGate = (G2C_LoginGate)await gateSession.Call(
                    new C2G_LoginGate() { Key = r2CLogin.Key, GateId = r2CLogin.GateId});

                PlayerComponent playerComponent = ET.Client.PlayerHelper.GetMyPlayerComponent(clientScene);
                playerComponent.MyId = g2CLoginGate.PlayerId;

                playerComponent.PlayerGameMode = EnumHelper.FromString<PlayerGameMode>(g2CLoginGate.PlayerGameMode);
                playerComponent.PlayerStatus = EnumHelper.FromString<PlayerStatus>(g2CLoginGate.PlayerStatus);
                playerComponent.RoomId = g2CLoginGate.RoomId;

                Log.Debug("登陆gate成功!");

                await EventSystem.Instance.PublishAsync(clientScene, new EventType.LoginFinish());
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public static async ETTask LoginOut(Scene clientScene)
        {
            try
            {
                try
                {
                    Session gateSession = ET.Client.SessionHelper.GetSession(clientScene);
                    G2C_LoginOut _G2C_LoginOut = (G2C_LoginOut)await gateSession.Call(
                        new C2G_LoginOut());
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }

                clientScene.RemoveComponent<RouterAddressComponent>();
                clientScene.RemoveComponent<NetClientComponent>();
                clientScene.RemoveComponent<SessionComponent>();

                await SceneHelper.EnterLogin(clientScene, false);
                await EventSystem.Instance.PublishAsync(clientScene, new EventType.LoginOutFinish());
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

                PlayerComponent playerComponent = ET.Client.PlayerHelper.GetMyPlayerComponent(clientScene);
                playerComponent.MyId = g2CLoginGate.PlayerId;
                Log.Debug("登陆gate成功!");

                await EventSystem.Instance.PublishAsync(clientScene, new EventType.LoginFinish());
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
}