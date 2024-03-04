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

            bool bChg = ET.Client.DebugConnectHelper.RetSetResConfig();
            if (bChg)
            {
                await MonoResComponent.Instance.ReLoadWhenDebugConnect();
            }

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
            bool bRet = await ChkHotUpdateAsync(clientScene);
            if (bRet == false)
            {
            }
        }

        public static async ETTask<bool> ChkHotUpdateAsync(Scene clientScene, bool onlyChk = false)
        {
            if (onlyChk == false)
            {
                UIComponent uiComponent = UIManagerHelper.GetUIComponent(clientScene);
                // 打开热更界面
                await uiComponent.ShowWindowAsync<DlgUpdate>();
            }

            int retryTimes = 10;
            while (IsNetworkReachability() == false)
            {
                await TimerComponent.Instance.WaitAsync(100);
                retryTimes--;
                if (retryTimes == 0)
                {
                    string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Net_ChkConnect");
                    string title = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Net_ChkDisconnect");
                    UIManagerHelper.ShowOnlyConfirm(clientScene, msg, () =>
                    {
                        ChkHotUpdateAsync(clientScene).Coroutine();
                    },null,null,title);
                    return false;
                }
            }

            // 热更流程
            bool bRet = await HotUpdateAsync(clientScene);
            if (bRet)
            {
                if (onlyChk == false)
                {
                    await DoAfterChkHotUpdate(clientScene);
                }
                return true;
            }
            return false;
        }

        public static async ETTask DoAfterChkHotUpdate(Scene clientScene)
        {
            ConfigComponent configComponent = Game.GetExistSingleton<ConfigComponent>();
            if (configComponent.ChkFinishLoad() == false)
            {
                UIComponent uiComponent = UIManagerHelper.GetUIComponent(clientScene);
                // 打开热更界面
                await uiComponent.ShowWindowAsync<DlgLoading>();
                DlgLoading _DlgLoading = uiComponent.GetDlgLogic<DlgLoading>(true);

                await Game.GetExistSingleton<ConfigComponent>().LoadAsync(_DlgLoading.UpdateProcess);
            }

            LocalizeComponent.Instance.ResetLanguage();

            if (clientScene.GetComponent<LoginSDKManagerComponent>() == null)
            {
                clientScene.AddComponent<LoginSDKManagerComponent>();
            }
            if (clientScene.GetComponent<EventLoggingSDKComponent>() == null)
            {
                clientScene.AddComponent<EventLoggingSDKComponent>();
            }
            if (clientScene.GetComponent<AdmobSDKComponent>() == null)
            {
                clientScene.AddComponent<AdmobSDKComponent>();
            }

            await EnterLogin(clientScene);
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

        private static async ETTask ShowHotUpdateInfo(Scene clientScene)
        {
            if (Application.isMobilePlatform)
            {
                YooAsset.EPlayMode resLoadMode = ResConfig.Instance.ResLoadMode;
                if (resLoadMode == YooAsset.EPlayMode.EditorSimulateMode)
                {
                    Log.Debug($"--ShowHotUpdateInfo resLoadMode == YooAsset.EPlayMode.EditorSimulateMode");
                }
                else if (resLoadMode == YooAsset.EPlayMode.OfflinePlayMode)
                {
                    Log.Debug($"--ShowHotUpdateInfo resLoadMode == YooAsset.EPlayMode.OfflinePlayMode");
                }
                else if (resLoadMode == YooAsset.EPlayMode.HostPlayMode)
                {
                    Log.Debug($"--ShowHotUpdateInfo resLoadMode == YooAsset.EPlayMode.HostPlayMode");
                }
            }
            await ETTask.CompletedTask;
        }

        private static async ETTask<bool> HotUpdateAsync(Scene clientScene)
        {
            await ShowHotUpdateInfo(clientScene);

            // UIComponent uiComponent = UIManagerHelper.GetUIComponent(clientScene);
            // // 打开热更界面
            // await uiComponent.ShowWindowAsync<DlgUpdate>();
            UIManagerHelper.PreLoadConfirm(clientScene);

            Log.Debug($"NetWork.GetIP[{NetWork.GetIP()}]");
            // 更新版本号
            (bool bNeedUpdate, int errorCode) = await ResComponent.Instance.UpdateVersionAsync();
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
            if (bNeedUpdate)
            {
                Log.Debug("bNeedUpdate == true");

                string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_NewGameVersion");
                UIManagerHelper.ShowOnlyConfirm(clientScene, msg, () =>
                {
                    if (ResConfig.Instance.Channel == "10001")
                    {
                        Application.OpenURL("https://play.google.com/store/apps/details?id=com.dm.realityguard&hl=en-US&gl=US");
                    }
                    else if (ResConfig.Instance.Channel == "10002")
                    {
                        Application.OpenURL("https://testflight.apple.com/join/WUOiuC2s");
                    }
                    else
                    {
                        Application.OpenURL("http://artd.corp.deepmirror.com/");
                    }

                    ChkHotUpdateAsync(clientScene).Coroutine();
                });
                return false;
            }

            // 更新资源版本号
            errorCode = await ResComponent.Instance.UpdateMainifestVersionAsync();
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

            YooAsset.UpdatePackageManifestOperation updatePackageManifestOperation = null;
            // 更新Manifest
            (errorCode, updatePackageManifestOperation) = await ResComponent.Instance.UpdateManifestAsync();
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
                if (ResComponent.Instance.Downloader.TotalDownloadCount == 0)
                {
                    return true;
                }

                long totalDownloadMB = ResComponent.Instance.Downloader.TotalDownloadBytes / (1024 * 1024);
                if (totalDownloadMB == 0)
                {
                    totalDownloadMB = 1;
                }
                string msgTxt =
                        LocalizeComponent.Instance.GetTextValue("TextCode_Key_Res_UpdateTip", totalDownloadMB);
                string titleTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Res_UpdateTitle");
                string sureTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Res_Download");
                UIManagerHelper.ShowOnlyConfirm(clientScene, msgTxt, () =>
                {
                    DownloadPatch(clientScene, updatePackageManifestOperation).Coroutine();
                },sureTxt,null,titleTxt);
                return false;
            }
            else
            {
                updatePackageManifestOperation.SavePackageVersion();
                return true;
            }
        }

        private static async ETTask<bool> DownloadPatch(Scene clientScene, YooAsset.UpdatePackageManifestOperation updatePackageManifestOperation)
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
            else
            {
                updatePackageManifestOperation.SavePackageVersion();
            }

            int modelVersion = GlobalConfig.Instance.ModelVersion;
            int hotFixVersion = GlobalConfig.Instance.HotFixVersion;
            await MonoResComponent.Instance.RestartAsync();
            bool modelChanged = modelVersion != GlobalConfig.Instance.ModelVersion;
            bool hotfixChanged = hotFixVersion != GlobalConfig.Instance.HotFixVersion;
            Log.Debug($" OldModelVersion[{modelVersion}] NewModelVersion[{GlobalConfig.Instance.ModelVersion}]");
            Log.Debug($" OldHotFixVersion[{hotFixVersion}] NewHotFixVersion[{GlobalConfig.Instance.HotFixVersion}]");

            string msgTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Res_UpdateSuccessDes");
            string sureTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Res_Restart");
            string titleTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Res_DownLoadSuccess");
            UIManagerHelper.ShowOnlyConfirm(clientScene, msgTxt, () =>
            {
                if (modelChanged || hotfixChanged)
                {
                    ReloadAll(clientScene).Coroutine();
                }
                else
                {
                    // 只是资源更新就直接进入游戏。
                    DoAfterChkHotUpdate(clientScene).Coroutine();
                }
            },sureTxt,null,titleTxt);
            return false;
        }

        private static async ETTask ReloadAll(Scene scene)
        {
            await TimerComponent.Instance.WaitFrameAsync();
            GameObject.Find("/Init").GetComponent<Init>().Restart().Coroutine();
        }

        private static async ETTask EnterLogin(Scene scene)
        {
            await SceneHelper.EnterLogin(scene, true);
        }
    }
}