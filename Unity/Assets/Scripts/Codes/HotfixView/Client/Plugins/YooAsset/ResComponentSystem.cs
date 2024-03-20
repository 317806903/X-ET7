using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using YooAsset;

namespace ET.Client
{
    public class ResComponentAwakeSystem: AwakeSystem<ResComponent>
    {
        protected override void Awake(ResComponent self)
        {
            self.Awake();
        }
    }

    public class ResComponentDestroySystem: DestroySystem<ResComponent>
    {
        protected override void Destroy(ResComponent self)
        {
            self.Destroy();
        }
    }

    public class ResComponentUpdateSystem: UpdateSystem<ResComponent>
    {
        protected override void Update(ResComponent self)
        {
            self.Update();
        }
    }

    [FriendOf(typeof (ResComponent))]
    public static class ResComponentSystem
    {
        #region 生命周期

        public static void Awake(this ResComponent self)
        {
            ResComponent.Instance = self;
        }

        public static void Destroy(this ResComponent self)
        {
            self.ForceUnloadAllAssets();

            ResComponent.Instance = null;
            self.PackageVersion = string.Empty;
            self.Downloader = null;

            self.AssetsOperationHandles.Clear();
            self.SubAssetsOperationHandles.Clear();
            self.SceneOperationHandles.Clear();
            self.RawFileOperationHandles.Clear();
            self.HandleProgresses.Clear();
            self.DoneHandleQueue.Clear();

            YooAssets.Destroy();
        }

        public static void Update(this ResComponent self)
        {
            foreach (var kv in self.HandleProgresses)
            {
                OperationHandleBase handle = kv.Key;
                Action<float> progress = kv.Value;

                if (!handle.IsValid)
                {
                    continue;
                }

                if (handle.IsDone)
                {
                    self.DoneHandleQueue.Enqueue(handle);
                    progress?.Invoke(1);
                    continue;
                }

                progress?.Invoke(handle.Progress);
            }

            while (self.DoneHandleQueue.Count > 0)
            {
                var handle = self.DoneHandleQueue.Dequeue();
                self.HandleProgresses.Remove(handle);
            }
        }

        #endregion

        #region 热更相关


        public static async ETTask<(int, bool, bool, string, int)> UpdateVersionAsync(this ResComponent self)
        {
            int errorCode = ErrorCode.ERR_Success;
            bool bNeedUpdate = false;
            bool bIsAuditing = false;
            string auditingRouterHttpHost = "";
            int auditingRouterHttpPort = 0;

            YooAsset.EPlayMode resLoadMode = ResConfig.Instance.ResLoadMode;
            if (resLoadMode == YooAsset.EPlayMode.EditorSimulateMode)
            {
            }
            else if (resLoadMode == YooAsset.EPlayMode.OfflinePlayMode)
            {
            }
            else if (resLoadMode == YooAsset.EPlayMode.HostPlayMode)
            {
                string url = $"{MonoResComponent.GetHostServerVersionURL()}?v={RandomGenerator.RandUInt32()}";
                Log.Debug($"UpdateVersionAsync url: {url}");
                try
                {
                    string versionText = await HttpClientHelper.Get(url);
                    versionText = versionText.Trim();
                    Log.Debug($"UpdateVersionAsync versionText: {versionText}");
                    // HttpGetRouterResponse httpGetRouterResponse = JsonHelper.FromJson<HttpGetRouterResponse>(routerInfo);

                    string [] versionInfoList = versionText.Split("|");
                    string newVersion = versionInfoList[0];
                    newVersion = newVersion.Trim();
                    {
                        string [] curVersionTmp = ResConfig.Instance.Version.Split(".");
                        string [] newVersionTmp = newVersion.Split(".");
                        Log.Debug($"UpdateVersionAsync curVersion[{ResConfig.Instance.Version}], newVersion[{newVersion}]");

                        if (curVersionTmp[0].CompareTo(newVersionTmp[0]) < 0)
                        {
                            bNeedUpdate = true;
                        }
                        else if (curVersionTmp[0].CompareTo(newVersionTmp[0]) == 0)
                        {
                            if (int.Parse(curVersionTmp[1]) < int.Parse(newVersionTmp[1]))
                            {
                                bNeedUpdate = true;
                            }
                        }
                    }
                    if (versionInfoList.Length >= 2)
                    {
                        string auditingVersions = versionInfoList[1];
                        auditingVersions = auditingVersions.Trim();
                        string [] versionInfoList2 = auditingVersions.Split(";");
                        foreach (string auditingVersion in versionInfoList2)
                        {
                            if (string.IsNullOrEmpty(auditingVersion))
                            {
                                continue;
                            }
                            if (newVersion != auditingVersion && ResConfig.Instance.Version == auditingVersion)
                            {
                                bIsAuditing = true;
                                break;
                            }
                        }
                    }

                    if (bIsAuditing)
                    {
                        if (versionInfoList.Length >= 3)
                        {
                            auditingRouterHttpHost = versionInfoList[2];
                            auditingRouterHttpHost = auditingRouterHttpHost.Trim();
                        }
                        if (versionInfoList.Length >= 4)
                        {
                            string auditingRouterHttpPortTmp = versionInfoList[3];
                            auditingRouterHttpPortTmp = auditingRouterHttpPortTmp.Trim();
                            auditingRouterHttpPort = int.Parse(auditingRouterHttpPortTmp);
                        }
                    }
                }
                catch (Exception e)
                {
                    Log.Error($"UpdateVersionAsync Exception[{e.Message}]");
                    errorCode = ErrorCode.ERR_ResourceInitError;
                    bNeedUpdate = false;
                }
            }

            return (errorCode, bNeedUpdate, bIsAuditing, auditingRouterHttpHost, auditingRouterHttpPort);
        }

        public static async ETTask<int> UpdateMainifestVersionAsync(this ResComponent self, int timeout = 30)
        {
            var package = YooAssets.GetPackage("DefaultPackage");
            var operation = package.UpdatePackageVersionAsync();

            await operation.GetAwaiter();

            if (operation.Status != EOperationStatus.Succeed)
            {
                return ErrorCode.ERR_ResourceUpdateVersionError;
            }

            self.PackageVersion = operation.PackageVersion;
            return ErrorCode.ERR_Success;
        }

        public static async ETTask<(int, UpdatePackageManifestOperation)> UpdateManifestAsync(this ResComponent self)
        {
            var package = YooAssets.GetPackage("DefaultPackage");
            var operation = package.UpdatePackageManifestAsync(self.PackageVersion, false);

            await operation.GetAwaiter();

            if (operation.Status != EOperationStatus.Succeed)
            {
                return (ErrorCode.ERR_ResourceUpdateManifestError, null);
            }

            return (ErrorCode.ERR_Success, operation);
        }

        public static int CreateDownloader(this ResComponent self)
        {
            int downloadingMaxNum = 10;
            int failedTryAgain = 3;
            ResourceDownloaderOperation downloader = YooAssets.CreateResourceDownloader(downloadingMaxNum, failedTryAgain);
            self.Downloader = downloader;
            if (downloader.TotalDownloadCount == 0)
            {
                Log.Debug("---CreateDownloader 没有发现需要下载的资源");
            }
            else
            {
                Log.Debug("一共发现了{0}个资源需要更新下载。".Fmt(downloader.TotalDownloadCount));
            }

            return ErrorCode.ERR_Success;
        }

        public static async ETTask<int> DonwloadWebFilesAsync(this ResComponent self,
        DownloaderOperation.OnStartDownloadFile onStartDownloadFileCallback = null,
        DownloaderOperation.OnDownloadProgress onDownloadProgress = null,
        DownloaderOperation.OnDownloadError onDownloadError = null,
        DownloaderOperation.OnDownloadOver onDownloadOver = null)
        {
            if (self.Downloader == null)
            {
                return ErrorCode.ERR_Success;
            }

            // 注册下载回调
            self.Downloader.OnStartDownloadFileCallback = onStartDownloadFileCallback;
            self.Downloader.OnDownloadProgressCallback = onDownloadProgress;
            self.Downloader.OnDownloadErrorCallback = onDownloadError;
            self.Downloader.OnDownloadOverCallback = onDownloadOver;
            self.Downloader.BeginDownload();
            await self.Downloader.GetAwaiter();

            // 检测下载结果
            if (self.Downloader.Status != EOperationStatus.Succeed)
            {
                return ErrorCode.ERR_ResourceUpdateDownloadError;
            }

            return ErrorCode.ERR_Success;
        }

        #endregion

        #region 卸载

        public static void UnloadUnusedAssets(this ResComponent self)
        {
            ResourcePackage package = YooAssets.GetPackage("DefaultPackage");
            package.UnloadUnusedAssets();
        }

        public static void ForceUnloadAllAssets(this ResComponent self)
        {
            ResourcePackage package = YooAssets.GetPackage("DefaultPackage");
            package.ForceUnloadAllAssets();
        }

        public static void UnloadAsset(this ResComponent self, string location)
        {
            if (self.AssetsOperationHandles.TryGetValue(location, out AssetOperationHandle assetOperationHandle))
            {
                assetOperationHandle.Release();
                self.AssetsOperationHandles.Remove(location);
            }
            else if (self.RawFileOperationHandles.TryGetValue(location, out RawFileOperationHandle rawFileOperationHandle))
            {
                rawFileOperationHandle.Release();
                self.RawFileOperationHandles.Remove(location);
            }
            else if (self.SubAssetsOperationHandles.TryGetValue(location, out SubAssetsOperationHandle subAssetsOperationHandle))
            {
                subAssetsOperationHandle.Release();
                self.SubAssetsOperationHandles.Remove(location);
            }
            else
            {
                Log.Error($"资源{location}不存在");
            }
        }

        #endregion

        #region 异步加载

        public static async ETTask<T> LoadAssetAsync<T>(this ResComponent self, string location) where T : UnityEngine.Object
        {
            self.AssetsOperationHandles.TryGetValue(location, out AssetOperationHandle handle);

            if (handle == null || handle.IsValid == false)
            {
                handle = YooAssets.LoadAssetAsync<T>(location);
                self.AssetsOperationHandles[location] = handle;
            }

            await handle;

            if (typeof(T) == typeof (UnityEngine.GameObject))
            {
                self.ResetShaderWhenEditor((UnityEngine.GameObject)handle.AssetObject);
            }

            return handle.GetAssetObject<T>();
        }

        public static async ETTask<UnityEngine.Object> LoadAssetAsync(this ResComponent self, string location, Type type)
        {
            self.AssetsOperationHandles.TryGetValue(location, out AssetOperationHandle handle);

            if (handle == null || handle.IsValid == false)
            {
                handle = YooAssets.LoadAssetAsync(location, type);
                self.AssetsOperationHandles[location] = handle;
            }

            await handle;

            if (type == typeof (UnityEngine.GameObject))
            {
                self.ResetShaderWhenEditor((UnityEngine.GameObject)handle.AssetObject);
            }

            return handle.AssetObject;
        }

        public static async ETTask<UnityEngine.SceneManagement.Scene> LoadSceneAsync(this ResComponent self, string location,
        Action<float> progressCallback = null, LoadSceneMode loadSceneMode = LoadSceneMode.Single, bool activateOnLoad = true, int priority = 100)
        {
            bool bRet = self.SceneOperationHandles.TryGetValue(location, out SceneOperationHandle handle);
            if (bRet == false || handle.IsValid == false)
            {
                handle = YooAssets.LoadSceneAsync(location, loadSceneMode, activateOnLoad, priority);
                self.SceneOperationHandles[location] = handle;
            }

            if (progressCallback != null)
            {
                if (self.HandleProgresses.TryGetValue(handle, out var callbacks))
                {
                    callbacks += progressCallback;
                }
                else
                {
                    self.HandleProgresses.Add(handle, progressCallback);
                }
            }

            if (handle.IsValid)
            {
                await handle;
            }

            foreach (GameObject rootGameObject in handle.SceneObject.GetRootGameObjects())
            {
                self.ResetShaderWhenEditor(rootGameObject);
            }

            return handle.SceneObject;
        }

        public static async ETTask<byte[]> LoadRawFileDataAsync(this ResComponent self, string location)
        {
            self.RawFileOperationHandles.TryGetValue(location, out RawFileOperationHandle handle);

            if (handle == null || handle.IsValid == false)
            {
                handle = YooAssets.LoadRawFileAsync(location);
                self.RawFileOperationHandles[location] = handle;
            }

            await handle;

            return handle.GetRawFileData();
        }

        public static async ETTask<string> LoadRawFileTextAsync(this ResComponent self, string location)
        {
            self.RawFileOperationHandles.TryGetValue(location, out RawFileOperationHandle handle);

            if (handle == null || handle.IsValid == false)
            {
                handle = YooAssets.LoadRawFileAsync(location);
                self.RawFileOperationHandles[location] = handle;
            }

            await handle;

            return handle.GetRawFileText();
        }

        #endregion

        #region 同步加载

        public static T LoadAsset<T>(this ResComponent self, string location) where T : UnityEngine.Object
        {
            self.AssetsOperationHandles.TryGetValue(location, out AssetOperationHandle handle);

            if (handle == null || handle.IsValid == false)
            {
                handle = YooAssets.LoadAssetSync<T>(location);
                self.AssetsOperationHandles[location] = handle;
            }

            if (typeof (T) == typeof (UnityEngine.GameObject))
            {
                self.ResetShaderWhenEditor((UnityEngine.GameObject)handle.AssetObject);
            }

            return handle.AssetObject as T;
        }

        public static UnityEngine.Object LoadAsset(this ResComponent self, string location, Type type)
        {
            self.AssetsOperationHandles.TryGetValue(location, out AssetOperationHandle handle);

            if (handle == null || handle.IsValid == false)
            {
                handle = YooAssets.LoadAssetSync(location, type);
                self.AssetsOperationHandles[location] = handle;
            }

            if (type == typeof (UnityEngine.GameObject))
            {
                self.ResetShaderWhenEditor((UnityEngine.GameObject)handle.AssetObject);
            }

            return handle.AssetObject;
        }

        public static void ResetShaderWhenEditor(this ResComponent self, UnityEngine.GameObject gameObject)
        {
#if UNITY_ANDROID || UNITY_IOS
            if (UnityEngine.Application.isMobilePlatform == false)
            {
                try
                {
                    var renderers = gameObject.GetComponentsInChildren<UnityEngine.Renderer>();

                    foreach (var renderer in renderers)
                    {
                        if (renderer.sharedMaterials != null)
                        {
                            foreach (UnityEngine.Material material in renderer.sharedMaterials)
                            {
                                if (material != null)
                                {
                                    string shaderName = material.shader.name;
                                    material.shader = Shader.Find(shaderName);
                                }
                            }
                        }
                    }

                    var textMeshProUGUIs = gameObject.GetComponentsInChildren<TMPro.TextMeshProUGUI>();

                    foreach (var textMeshProUGUI in textMeshProUGUIs)
                    {
                        if (textMeshProUGUI.fontSharedMaterial != null)
                        {
                            UnityEngine.Material material = textMeshProUGUI.fontSharedMaterial;
                            if (material != null)
                            {
                                string shaderName = material.shader.name;
                                material.shader = Shader.Find(shaderName);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }
#endif
        }

        #endregion
    }
}