using System.Net;
using System.Net.Sockets;

namespace ET.Server
{
    public static class SceneHelper
    {
        public static async ETTask<Scene> CreateServerScene(Entity parent, long id, long instanceId, int zone, string name, SceneType sceneType, StartSceneConfig startSceneConfig = null)
        {
            await ETTask.CompletedTask;
            Scene scene = EntitySceneFactory.CreateScene(id, instanceId, zone, sceneType, name, parent);

            scene.AddComponent<MailBoxComponent, MailboxType>(MailboxType.UnOrderMessageDispatcher);
            //scene.AddComponent<NavmeshManagerComponent>();

            Log.Debug($"CreateServerScene [{scene.SceneType.ToString()}] [{scene.Id}] [{scene.InstanceId}]");
            switch (scene.SceneType)
            {
                case SceneType.Router:
                    // 云服务器中，一般来说router要单独部署，不过大家经常放在一起，那么下面要修改
                    // startSceneConfig.OuterIPPort 改成 startSceneConfig.InnerIPOutPort
                    // 然后云服务器防火墙把端口映射过来
                    scene.AddComponent<RouterComponent, IPEndPoint, string>(startSceneConfig.InnerIPOutPort,
                        startSceneConfig.StartProcessConfig.InnerIP
                    );
					//scene.AddComponent<RouterComponent, IPEndPoint, string>(startSceneConfig.OuterIPPort,
                    //    startSceneConfig.StartProcessConfig.InnerIP
                    //);
                    break;
                case SceneType.RouterManager: // 正式发布请用CDN代替RouterManager
                    // 云服务器在防火墙那里做端口映射
                    scene.AddComponent<HttpComponent, string>($"http://*:{startSceneConfig.OuterPort}/");
                    break;
                case SceneType.Realm:
                    scene.AddComponent<NetServerComponent, IPEndPoint>(startSceneConfig.InnerIPOutPort);
                    scene.AddComponent<RealmGetGatePlayerCountComponent>();
                    break;
                case SceneType.Gate:
                    scene.AddComponent<NetServerComponent, IPEndPoint>(startSceneConfig.InnerIPOutPort);
                    scene.AddComponent<PlayerComponent>();
                    scene.AddComponent<GateSessionKeyComponent>();
                    break;
                case SceneType.Map:
                    ET.SceneHelper.InitWhenServer(scene);
                    scene.AddComponent<AOIManagerComponent>();
                    break;
                case SceneType.Location:
                    scene.AddComponent<LocationManagerComoponent>();
                    break;
                case SceneType.Robot:
                    scene.AddComponent<RobotManagerComponent>();
                    break;
                case SceneType.BenchmarkServer:
                    scene.AddComponent<BenchmarkServerComponent>();
                    scene.AddComponent<NetServerComponent, IPEndPoint>(startSceneConfig.OuterIPPort);
                    break;
                case SceneType.BenchmarkClient:
                    scene.AddComponent<BenchmarkClientComponent>();
                    break;
                case SceneType.Room:
                    RoomManagerComponent roomManagerComponent = scene.AddComponent<RoomManagerComponent>();
                    roomManagerComponent.AddComponent<PlayerLocationChkComponent>();
                    scene.AddComponent<RoomGetDynamicMapCountComponent>();
                    break;
                case SceneType.Match:
                    break;
                case SceneType.Account:
                    scene.AddComponent<AccountManagerComponent>();
                    break;
                case SceneType.Rank:
                    scene.AddComponent<RankManagerComponent>();
                    scene.AddComponent<RankShowManagerComponent>();
                    break;
                case SceneType.PlayerCache:
                    scene.AddComponent<PlayerCacheManagerComponent>();
                    break;
                case SceneType.Mail:
                    scene.AddComponent<MailManagerComponent>();
                    scene.AddComponent<MailHistoryManagerComponent>();
                    break;
                case SceneType.Season:
                    scene.AddComponent<SeasonManagerComponent>().Init();
                    break;
                case SceneType.Pay:
                    scene.AddComponent<HttpComponent, string>($"http://*:{startSceneConfig.OuterPort}/");
                    scene.AddComponent<PayManagerComponent>();
                    break;
                case SceneType.ActionFromHttp:
                    scene.AddComponent<HttpComponent, string>($"http://*:{startSceneConfig.OuterPort}/");
                    scene.AddComponent<ActionFromHttpManagerComponent>();
                    break;
            }

            return scene;
        }

    }
}