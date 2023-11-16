using System.Threading;
using System.Threading.Tasks;
using ET.AbilityConfig;
using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.Process)]
    public class EntryEvent3_InitClient: AEvent<Scene, ET.EventType.EntryEvent3>
    {
        protected override async ETTask Run(Scene scene, ET.EventType.EntryEvent3 args)
        {
            if (GlobalConfig.Instance.CodeMode == CodeMode.Server)
            {
                return;
            }

            if (Game.ChkIsExistSingleton<ConfigComponent>() == false)
            {
                Game.AddSingleton<ConfigComponent>();
            }

            Root.Instance.Scene.AddComponent<ResComponent>();

            Root.Instance.Scene.AddComponent<GlobalComponent>();

            Root.Instance.Scene.AddComponent<FsmDispatcherComponent>();

            Scene clientScene = await SceneFactory.CreateClientScene(1, "Game");

            LanguageType languageType;
            switch (ResConfig.Instance.areaType)
            {
                case AreaType.CN:
                    languageType = LanguageType.EN;
                    break;
                case AreaType.EN:
                    languageType = LanguageType.EN;
                    break;
                case AreaType.TW:
                    languageType = LanguageType.TW;
                    break;
                default:
                    languageType = LanguageType.EN;
                    break;
            }
            LocalizeComponent.Instance.SwitchLanguage(languageType, true);

            // 热更流程
            await ChkHotUpdateAsync(clientScene);
        }

        public static async ETTask ChkHotUpdateAsync(Scene clientScene)
        {
            int retryTimes = 10;
            while (IsNetworkReachability() == false)
            {
                await TimerComponent.Instance.WaitAsync(100);
                retryTimes--;
                if (retryTimes == 0)
                {
                    string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Net_ChkConnect");
                    UIManagerHelper.ShowOnlyConfirm(clientScene, msg, () =>
                    {
                        ChkHotUpdateAsync(clientScene).Coroutine();
                    });
                    return;
                }
            }


            ET.Client.DebugConnectHelper.RetSetResConfig();

            // 热更流程
            bool bRet = await HotUpdateAsync(clientScene);
            if (bRet)
            {
                ConfigComponent configComponent = Game.GetExistSingleton<ConfigComponent>();
                if (configComponent.ChkFinishLoad() == false)
                {
                    await Game.GetExistSingleton<ConfigComponent>().LoadAsync();
                }

                LocalizeComponent.Instance.ResetLanguage();

                await EnterLogin(clientScene);
            }
        }

        public static bool IsNetworkReachability()
        {
            switch (Application.internetReachability)
            {
                case NetworkReachability.ReachableViaLocalAreaNetwork:
                    //Logger.Log("当前使用的是：WiFi，请放心更新！");
                    return true;
                case NetworkReachability.ReachableViaCarrierDataNetwork:
                    //Logger.Log("当前使用的是移动网络，是否继续更新？");
                    return true;
                default:
                    //Logger.Log("当前没有联网，请您先联网后再进行操作！");
                    return false;
            }
        }

        private static async ETTask<bool> HotUpdateAsync(Scene clientScene)
        {
            UIComponent uiComponent = UIManagerHelper.GetUIComponent(clientScene);

            // 打开热更界面
            await uiComponent.ShowWindowAsync<DlgUpdate>();

            // 更新版本号
            int errorCode = await ResComponent.Instance.UpdateVersionAsync();
            if (errorCode != ErrorCode.ERR_Success)
            {
                Log.Error("FsmUpdateStaticVersion 出错！{0}".Fmt(errorCode));
                string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Res_UpdateErr", errorCode);
                //UIManagerHelper.ShowConfirmNoClose(clientScene, msg);
                UIManagerHelper.ShowOnlyConfirm(clientScene, msg, () =>
                {
                    ChkHotUpdateAsync(clientScene).Coroutine();
                });
                return false;
            }

            // 更新Manifest
            errorCode = await ResComponent.Instance.UpdateManifestAsync();
            if (errorCode != ErrorCode.ERR_Success)
            {
                Log.Error("ResourceComponent.UpdateManifest 出错！{0}".Fmt(errorCode));
                string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Res_UpdateErr", errorCode);
                //UIManagerHelper.ShowConfirmNoClose(clientScene, msg);
                UIManagerHelper.ShowOnlyConfirm(clientScene, msg, () =>
                {
                    ChkHotUpdateAsync(clientScene).Coroutine();
                });
                return false;
            }

            // 创建下载器
            errorCode = ResComponent.Instance.CreateDownloader();
            if (errorCode != ErrorCode.ERR_Success)
            {
                Log.Error("ResourceComponent.FsmCreateDownloader 出错！{0}".Fmt(errorCode));
                string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Res_UpdateErr", errorCode);
                //UIManagerHelper.ShowConfirmNoClose(clientScene, msg);
                UIManagerHelper.ShowOnlyConfirm(clientScene, msg, () =>
                {
                    ChkHotUpdateAsync(clientScene).Coroutine();
                });
                return false;
            }

            // Downloader不为空，说明有需要下载的资源
            if (ResComponent.Instance.Downloader != null)
            {
                return await DownloadPatch(clientScene);
            }
            else
            {
                return true;
            }
        }

        private static async ETTask<bool> DownloadPatch(Scene clientScene)
        {
            // 下载资源
            Log.Info("Count: {0}, Bytes: {1}".Fmt(ResComponent.Instance.Downloader.TotalDownloadCount, ResComponent.Instance.Downloader.TotalDownloadBytes));
            int errorCode = await ResComponent.Instance.DonwloadWebFilesAsync(
                // 开始下载回调
                null,

                // 下载进度回调
                (totalDownloadCount, currentDownloadCount, totalDownloadBytes, currentDownloadBytes) =>
                {
                    // 更新进度条
                    EventSystem.Instance.Publish(clientScene, new EventType.OnPatchDownloadProgress() { TotalDownloadCount = totalDownloadCount, CurrentDownloadCount = currentDownloadCount, TotalDownloadSizeBytes = totalDownloadBytes, CurrentDownloadSizeBytes = currentDownloadBytes });
                },

                // 下载失败回调
                (fileName, error) =>
                {
                    // 下载失败
                    EventSystem.Instance.Publish(clientScene, new EventType.OnPatchDownlodFailed() { FileName = fileName, Error = error });
                },

                // 下载完成回调
                null);

            if (errorCode != ErrorCode.ERR_Success)
            {
                // todo: 弹出错误提示，确定后重试。
                Log.Error("ResourceComponent.FsmDonwloadWebFiles 出错！{0}".Fmt(errorCode));
                return false;
            }

            int modelVersion = GlobalConfig.Instance.ModelVersion;
            int hotFixVersion = GlobalConfig.Instance.HotFixVersion;
            await MonoResComponent.Instance.RestartAsync();
            bool modelChanged = modelVersion != GlobalConfig.Instance.ModelVersion;
            bool hotfixChanged = hotFixVersion != GlobalConfig.Instance.HotFixVersion;

            if (modelChanged || hotfixChanged)
            {
                string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Res_DownLoadSuccess");
                UIManagerHelper.ShowOnlyConfirm(clientScene, msg, () =>
                {
                    ReloadAll(clientScene).Coroutine();
                });
                return false;
            }
            else
            {
                // 只是资源更新就直接进入游戏。
                return true;
            }
        }

        private static async ETTask ReloadAll(Scene scene)
        {
            await GameObject.Find("/Init").GetComponent<Init>().Restart();
        }

        private static async ETTask EnterLogin(Scene scene)
        {
            await SceneHelper.EnterLogin(scene, true);
        }
    }
}