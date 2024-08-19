using System.Net;
using ET.AbilityConfig;

namespace ET.Server
{
    [Event(SceneType.Process)]
    public class EntryEvent2_InitServer: AEvent<Scene, ET.EventType.EntryEvent2>
    {
        protected override async ETTask Run(Scene scene, ET.EventType.EntryEvent2 args)
        {
            string codeMode = EventSystem.Instance.Invoke<ConfigComponent.GetCodeMode, string>(new ConfigComponent.GetCodeMode());
            if (codeMode == "Client")
            {
                return;
            }

            if (Game.ChkIsExistSingleton<ConfigComponent>() == false)
            {
                await Game.AddSingleton<ConfigComponent>().LoadAsync(null);
            }

            if (codeMode == "Server")
            {
                LanguageType languageType;
                if (Options.Instance.LanguageType == "CN")
                {
                    languageType = LanguageType.CN;
                }
                else if (Options.Instance.LanguageType == "EN")
                {
                    languageType = LanguageType.EN;
                }
                else if (Options.Instance.LanguageType == "TW")
                {
                    languageType = LanguageType.TW;
                }
                else
                {
                    languageType = LanguageType.EN;
                }
                LocalizeComponent.Instance.SwitchLanguage(languageType, true);
#if UNITY_EDITOR
                LocalizeComponent.Instance.IsShowLanguagePre = ResConfig.Instance.IsShowLanguagePre;
#endif
            }

            // 发送普通actor消息
            Root.Instance.Scene.AddComponent<ActorMessageSenderComponent>();
            // 发送location actor消息
            Root.Instance.Scene.AddComponent<ActorLocationSenderComponent>();
            // 访问location server的组件
            Root.Instance.Scene.AddComponent<LocationProxyComponent>();
            Root.Instance.Scene.AddComponent<ActorMessageDispatcherComponent>();
            Root.Instance.Scene.AddComponent<ServerSceneManagerComponent>();
            Root.Instance.Scene.AddComponent<RobotCaseComponent>();

            //Root.Instance.Scene.AddComponent<NavmeshManagerComponent>();

            DBManagerComponent dbManagerComponent = Root.Instance.Scene.AddComponent<DBManagerComponent>();
            dbManagerComponent.NeedDB = Options.Instance.NeedDB == 1;
            Log.Error($"--zpb-- Options.Instance.NeedDB {Options.Instance.NeedDB}");

            ServerSceneManagerComponent.Instance.IsGameModeArcade = Options.Instance.IsGameModeArcade == 1;
            Log.Error($"--zpb-- Options.Instance.IsGameModeArcade {Options.Instance.IsGameModeArcade}");

            while (true)
            {
                ConfigComponent configComponent = Game.GetExistSingleton<ConfigComponent>();
                if (configComponent.ChkFinishLoad() == false)
                {
                    await TimerComponent.Instance.WaitFrameAsync();
                }
                else
                {
                    break;
                }
            }

            if (ET.SceneHelper.ChkIsGameModeArcade())
            {
                ET.ConstValue.SessionTimeoutTime = GlobalSettingCfgCategory.Instance.GameModeArcadeSessionTimeOut * 1000;
            }

            //Options.Instance.Process = 2;
            StartProcessConfig processConfig = StartProcessConfigCategory.Instance.Get(Options.Instance.Process);
            switch (Options.Instance.AppType)
            {
                case AppType.Server:
                {
                    Root.Instance.Scene.AddComponent<NetInnerComponent, IPEndPoint>(processConfig.InnerIPPort);

                    var processScenes = StartSceneConfigCategory.Instance.GetByProcess(Options.Instance.Process);
                    foreach (StartSceneConfig startConfig in processScenes.Values)
                    {
                        await SceneHelper.CreateServerScene(ServerSceneManagerComponent.Instance, startConfig.Id, startConfig.InstanceId, startConfig.Zone, startConfig.Name,
                            startConfig.Type, startConfig);
                    }

                    break;
                }
                case AppType.Watcher:
                {
                    StartMachineConfig startMachineConfig = WatcherHelper.GetThisMachineConfig();
                    WatcherComponent watcherComponent = Root.Instance.Scene.AddComponent<WatcherComponent>();
                    watcherComponent.Start(Options.Instance.CreateScenes);
                    Root.Instance.Scene.AddComponent<NetInnerComponent, IPEndPoint>(NetworkHelper.ToIPEndPoint($"{startMachineConfig.InnerIP}:{startMachineConfig.WatcherPort}"));
                    break;
                }
                case AppType.GameTool:
                    break;
            }

            if (Options.Instance.Console == 1)
            {
                Root.Instance.Scene.AddComponent<ConsoleComponent>();
            }
        }
    }
}