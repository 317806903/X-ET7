using System.Collections.Generic;
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

            ChannelSettingComponent channelSettingComponent = Root.Instance.Scene.AddComponent<ChannelSettingComponent>();
            channelSettingComponent.Init(ResConfig.Instance.Channel);
            bool isExist = channelSettingComponent.ChkChannelCfg();
            if (Application.isMobilePlatform && isExist)
            {
                ResConfig.Instance.IsGameModeArcade = channelSettingComponent.GetDeviceGameMode() == DeviceGameMode.Arcade;
                ResConfig.Instance.IsDemoShow = channelSettingComponent.GetDeviceGameMode() == DeviceGameMode.DemoShow;

                ResConfig.Instance.ResHostServerIP = channelSettingComponent.GetResHostServerIP();
                ResConfig.Instance.ResGameVersion = channelSettingComponent.GetResGameVersion();
                ResConfig.Instance.RouterHttpHost = channelSettingComponent.GetRouterHttpHost();
                ResConfig.Instance.RouterHttpPort = channelSettingComponent.GetRouterHttpPort();


                string areaTypeStr = channelSettingComponent.GetAreaType();
                AreaType areaType = AreaType.CN;
                if (System.Enum.TryParse(areaTypeStr, out areaType))
                {
                }
                ResConfig.Instance.areaType = areaType;

                ResConfig.Instance.languageType = channelSettingComponent.GetLanguageType();

                ResConfig.Instance.IsNeedSendEventLog = channelSettingComponent.ChkIsNeedSendEventLog();

                await MonoResComponent.Instance.ReLoadWhenDebugConnect();
            }

            GlobalComponent globalComponent = Root.Instance.Scene.AddComponent<GlobalComponent>();
            await globalComponent.Init();

            Root.Instance.Scene.AddComponent<FsmDispatcherComponent>();

            Scene clientScene = await SceneFactory.CreateClientScene(1, "Game");

            bool bChg = ET.Client.DebugConnectHelper.RetSetResConfig();
            if (bChg)
            {
                await MonoResComponent.Instance.ReLoadWhenDebugConnect();
            }

            ClientSceneManagerComponent.Instance.IsGameModeArcade = ResConfig.Instance.IsGameModeArcade;
            ClientSceneManagerComponent.Instance.IsDemoShow = ResConfig.Instance.IsDemoShow;

            LanguageType languageType = LanguageType.EN;
            if (ResConfig.Instance.languageType == LanguageType.Auto.ToString())
            {
                switch(Application.systemLanguage)
                {
                    case SystemLanguage.Chinese://中文
                    case SystemLanguage.ChineseSimplified://中文简体
                        //这两个要一起判断，有的机型返回Chinese,有的返回ChineseSimplified
                        languageType = LanguageType.CN;
                        break;
                    case SystemLanguage.ChineseTraditional://中文繁体
                        languageType = LanguageType.TW;
                        break;
                    case SystemLanguage.English://
                        languageType = LanguageType.EN;
                        break;
                    case SystemLanguage.Unknown://
                        break;
                    default:
                        languageType = LanguageType.EN;
                        break;
                    //...
                }
            }
            else
            {
                if (System.Enum.TryParse(ResConfig.Instance.languageType, out languageType) == false)
                {
                    languageType = LanguageType.EN;
                }
            }
            LocalizeComponent.Instance.SwitchLanguage(languageType, true);
#if UNITY_EDITOR
            LocalizeComponent.Instance.IsShowLanguagePre = ResConfig.Instance.IsShowLanguagePre;
#endif

            // 热更流程
            bool bRet = await ChkHotUpdateAsync(clientScene);
            if (bRet == false)
            {
            }
        }

        public static async ETTask<bool> ChkHotUpdateAsync(Scene clientScene, bool isAutoInToDlgUpdate = true, bool isShowTipWhenNeedUpdate = true)
        {
            if (isAutoInToDlgUpdate)
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
                    UIManagerHelper.ShowOnlyConfirmHighest(clientScene, msg, () =>
                    {
                        ChkHotUpdateAsync(clientScene).Coroutine();
                    }, null, title);
                    return false;
                }
            }

            // 热更流程
            bool bRet = await HotUpdateAsync(clientScene, isShowTipWhenNeedUpdate);
            if (bRet)
            {
                await ET.Client.GlobalComponent.Instance.SetUpdateFinished();
                if (isAutoInToDlgUpdate)
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
            if (clientScene.GetComponent<AppsflyerSDKComponent>() == null)
            {
                clientScene.AddComponent<AppsflyerSDKComponent>();
            }
            if (clientScene.GetComponent<EventLoggingSDKComponent>() == null)
            {
                clientScene.AddComponent<EventLoggingSDKComponent>();
            }
            if (clientScene.GetComponent<AdmobSDKComponent>() == null)
            {
                clientScene.AddComponent<AdmobSDKComponent>();
            }

            if (ET.SceneHelper.ChkIsGameModeArcade())
            {
                ET.ConstValue.SessionTimeoutTime = GlobalSettingCfgCategory.Instance.GameModeArcadeSessionTimeOut * 1000;
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

        private static async ETTask<bool> HotUpdateAsync(Scene clientScene, bool isShowTipWhenNeedUpdate = true)
        {
            await ShowHotUpdateInfo(clientScene);

            // UIComponent uiComponent = UIManagerHelper.GetUIComponent(clientScene);
            // // 打开热更界面
            // await uiComponent.ShowWindowAsync<DlgUpdate>();
            UIManagerHelper.PreLoadConfirm(clientScene);

            Log.Debug($"NetWork.GetIP[{NetWork.GetIP()}]");
            int errorCode = 0;
            bool isActivity = ChannelSettingComponent.Instance.ChkIsActivity();
            if (isActivity)
            {
                // 更新版本号
                (int errorCodeTmp, Dictionary<string, object> versionActivity) = await ResComponent.Instance.UpdateVersionWhenActivityAsync(ChannelSettingComponent.Instance.channelId);
                if (errorCodeTmp != ErrorCode.ERR_Success)
                {
                    Log.Error("FsmUpdateStaticVersion 出错！{0}".Fmt(errorCodeTmp));
                    if (isShowTipWhenNeedUpdate)
                    {
                        string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Res_UpdateErr", errorCodeTmp);
                        //UIManagerHelper.ShowConfirmNoClose(clientScene, msg);
                        UIManagerHelper.ShowOnlyConfirmHighest(clientScene, msg, () =>
                        {
                            ChkHotUpdateAsync(clientScene).Coroutine();
                        });
                    }
                    return false;
                }
                else if ((bool)versionActivity["bNeedUpdate"])
                {
                    Log.Debug("bNeedUpdate == true");
                    if (isShowTipWhenNeedUpdate)
                    {
                        string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_NewGameVersion");
                        UIManagerHelper.ShowOnlyConfirmHighest(clientScene, msg, () =>
                        {
                            string url = ChannelSettingComponent.Instance.GetGameDownLoadURL();
                            bool isWebView = ChannelSettingComponent.Instance.ChkIsGameDownLoadWebView();

                            ET.Client.UIManagerHelper.ShowUrl(clientScene, url, isWebView);

                            ChkHotUpdateAsync(clientScene).Coroutine();
                        });
                    }
                    return false;
                }
                else if (string.IsNullOrEmpty((string)versionActivity["serverBusyMsg"]) == false)
                {
                    Log.Error($"serverBusyMsg {(string)versionActivity["serverBusyMsg"]}");
                    if (isShowTipWhenNeedUpdate)
                    {
                        string msg = (string)versionActivity["serverBusyMsg"];
                        UIManagerHelper.ShowOnlyConfirmHighest(clientScene, msg, () =>
                        {
                            ChkHotUpdateAsync(clientScene).Coroutine();
                        });
                    }
                    return false;
                }
                else if ((bool)versionActivity["bInActivityTime"] == false)
                {
                    Log.Debug("bInActivityTime == false");

                    if (isShowTipWhenNeedUpdate)
                    {
                        string msg = (string)versionActivity["activityNotInTimeMsg"];
                        UIManagerHelper.ShowOnlyConfirmHighest(clientScene, msg, () =>
                        {
                            ChkHotUpdateAsync(clientScene).Coroutine();
                        });
                    }
                    return false;
                }
                else
                {
                    string ResHostServerIP = (string)versionActivity["ResHostServerIP"];
                    if (string.IsNullOrEmpty(ResHostServerIP) == false)
                    {
                        ResConfig.Instance.ResHostServerIP = ResHostServerIP;
                        await MonoResComponent.Instance.ReLoadWhenDebugConnect();
                    }
                    string RouterHttpHost = (string)versionActivity["RouterHttpHost"];
                    if (string.IsNullOrEmpty(RouterHttpHost) == false)
                    {
                        ResConfig.Instance.RouterHttpHost = RouterHttpHost;

                        int RouterHttpPort = (int)versionActivity["RouterHttpPort"];
                        ResConfig.Instance.RouterHttpPort = RouterHttpPort;
                    }
                }
            }
            else
            {
                // 更新版本号
                (int errorCodeTmp, Dictionary<string, object> versionResult) = await ResComponent.Instance.UpdateVersionAsync();
                if (errorCodeTmp != ErrorCode.ERR_Success)
                {
                    Log.Error("FsmUpdateStaticVersion 出错！{0}".Fmt(errorCodeTmp));
                    if (isShowTipWhenNeedUpdate)
                    {
                        string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Res_UpdateErr", errorCodeTmp);
                        //UIManagerHelper.ShowConfirmNoClose(clientScene, msg);
                        UIManagerHelper.ShowOnlyConfirmHighest(clientScene, msg, () =>
                        {
                            ChkHotUpdateAsync(clientScene).Coroutine();
                        });
                    }
                    return false;
                }
                else if ((bool)versionResult["bNeedUpdate"])
                {
                    Log.Debug("bNeedUpdate == true");

                    if (isShowTipWhenNeedUpdate)
                    {
                        string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_NewGameVersion");
                        UIManagerHelper.ShowOnlyConfirmHighest(clientScene, msg, () =>
                        {
                            string url = ChannelSettingComponent.Instance.GetGameDownLoadURL();
                            bool isWebView = ChannelSettingComponent.Instance.ChkIsGameDownLoadWebView();

                            ET.Client.UIManagerHelper.ShowUrl(clientScene, url, isWebView);

                            ChkHotUpdateAsync(clientScene).Coroutine();
                        });
                    }
                    return false;
                }
                else if ((bool)versionResult["bIsAuditing"])
                {
                    Log.Debug("bIsAuditing == true");

                    string RouterHttpHost = (string)versionResult["auditingRouterHttpHost"];
                    if (string.IsNullOrEmpty(RouterHttpHost) == false)
                    {
                        ResConfig.Instance.RouterHttpHost = RouterHttpHost;

                        int RouterHttpPort = (int)versionResult["auditingRouterHttpPort"];
                        ResConfig.Instance.RouterHttpPort = RouterHttpPort;
                    }

                    ResConfig.Instance.ResGameVersion = "vAuditing";
                    await MonoResComponent.Instance.ReLoadWhenDebugConnect();
                }
                else if (string.IsNullOrEmpty((string)versionResult["serverBusyMsg"]) == false)
                {
                    Log.Error($"serverBusyMsg {(string)versionResult["serverBusyMsg"]}");

                    if (isShowTipWhenNeedUpdate)
                    {
                        string msg = (string)versionResult["serverBusyMsg"];
                        UIManagerHelper.ShowOnlyConfirmHighest(clientScene, msg, () =>
                        {
                            ChkHotUpdateAsync(clientScene).Coroutine();
                        });
                    }
                    return false;
                }
            }

            // 更新资源版本号
            errorCode = await ResComponent.Instance.UpdateMainifestVersionAsync();
            if (errorCode != ErrorCode.ERR_Success)
            {
                Log.Error("FsmUpdateStaticVersion 出错！{0}".Fmt(errorCode));
                if (isShowTipWhenNeedUpdate)
                {
                    string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Res_UpdateErr", errorCode);
                    //UIManagerHelper.ShowConfirmNoClose(clientScene, msg);
                    UIManagerHelper.ShowOnlyConfirmHighest(clientScene, msg, () =>
                    {
                        ChkHotUpdateAsync(clientScene).Coroutine();
                    });
                }
                return false;
            }

            YooAsset.UpdatePackageManifestOperation updatePackageManifestOperation = null;
            // 更新Manifest
            (errorCode, updatePackageManifestOperation) = await ResComponent.Instance.UpdateManifestAsync();
            if (errorCode != ErrorCode.ERR_Success)
            {
                Log.Error("ResourceComponent.UpdateManifest 出错！{0}".Fmt(errorCode));
                if (isShowTipWhenNeedUpdate)
                {
                    string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Res_UpdateErr", errorCode);
                    //UIManagerHelper.ShowConfirmNoClose(clientScene, msg);
                    UIManagerHelper.ShowOnlyConfirmHighest(clientScene, msg, () =>
                    {
                        ChkHotUpdateAsync(clientScene).Coroutine();
                    });
                }
                return false;
            }

            // 创建下载器
            errorCode = ResComponent.Instance.CreateDownloader();
            if (errorCode != ErrorCode.ERR_Success)
            {
                Log.Error("ResourceComponent.FsmCreateDownloader 出错！{0}".Fmt(errorCode));
                if (isShowTipWhenNeedUpdate)
                {
                    string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Res_UpdateErr", errorCode);
                    //UIManagerHelper.ShowConfirmNoClose(clientScene, msg);
                    UIManagerHelper.ShowOnlyConfirmHighest(clientScene, msg, () =>
                    {
                        ChkHotUpdateAsync(clientScene).Coroutine();
                    });
                }
                return false;
            }

            // Downloader不为空，说明有需要下载的资源
            if (ResComponent.Instance.Downloader != null)
            {
                if (ResComponent.Instance.Downloader.TotalDownloadCount == 0)
                {
                    return true;
                }

                if (isShowTipWhenNeedUpdate)
                {
                    long totalDownloadMB = ResComponent.Instance.Downloader.TotalDownloadBytes / (1024 * 1024);
                    if (totalDownloadMB == 0)
                    {
                        totalDownloadMB = 1;
                    }
                    string msgTxt =
                        LocalizeComponent.Instance.GetTextValue("TextCode_Key_Res_UpdateTip", totalDownloadMB);
                    string titleTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Res_UpdateTitle");
                    string sureTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Res_Download");
                    UIManagerHelper.ShowOnlyConfirmHighest(clientScene, msgTxt, () =>
                    {
                        DownloadPatch(clientScene, updatePackageManifestOperation).Coroutine();
                    }, sureTxt ,titleTxt);

                }
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
            UIManagerHelper.ShowOnlyConfirmHighest(clientScene, msgTxt, () =>
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
            }, sureTxt ,titleTxt);
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