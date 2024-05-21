using System;
using System.Collections;
using System.IO;
using UnityEngine;
using YooAsset;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ET
{
    public class MonoResComponent
    {
        public static MonoResComponent Instance { get; private set; } = new MonoResComponent();

        private ResourcePackage defaultPackage;

        public async ETTask InitAsync()
        {
            Log.Debug($"ET.MonoResComponent.InitAsync 11");
            // 初始化资源系统
            YooAssets.Initialize();
            Log.Debug($"ET.MonoResComponent.InitAsync 22");
            YooAssets.SetOperationSystemMaxTimeSlice(30);
            Log.Debug($"ET.MonoResComponent.InitAsync 33");

            this.LoadResConfig();
            Log.Debug($"ET.MonoResComponent.InitAsync 44");
            await InitPackage();
            Log.Debug($"ET.MonoResComponent.InitAsync 55");

            await this.LoadGlobalConfig();
            Log.Debug($"ET.MonoResComponent.InitAsync 66");
        }

        public async ETTask RestartAsync()
        {
            await this.LoadGlobalConfigAsync();
        }

        private async ETTask InitPackage()
        {
            EPlayMode resLoadMode = ResConfig.Instance.ResLoadMode;
#if UNITY_EDITOR
#else
            if (resLoadMode == EPlayMode.EditorSimulateMode)
            {
                //resLoadMode = EPlayMode.HostPlayMode;
                resLoadMode = EPlayMode.OfflinePlayMode;
            }
#endif
            // 创建默认的资源包
            string packageName = "DefaultPackage";
            defaultPackage = YooAssets.TryGetPackage(packageName);
            if (defaultPackage == null)
            {
                defaultPackage = YooAssets.CreatePackage(packageName);
                YooAssets.SetDefaultPackage(defaultPackage);
            }

            Log.Debug($"---- ResUpdate resLoadMode[{resLoadMode.ToString()}]");
            // 编辑器下的模拟模式
            InitializationOperation initializationOperation = null;
            if (resLoadMode == EPlayMode.EditorSimulateMode)
            {
                var createParameters = new EditorSimulateModeParameters();
                createParameters.SimulateManifestFilePath = EditorSimulateModeHelper.SimulateBuild(packageName);
                initializationOperation = defaultPackage.InitializeAsync(createParameters);
            }
            else if (resLoadMode == EPlayMode.OfflinePlayMode){
                var createParameters = new OfflinePlayModeParameters();
                initializationOperation = defaultPackage.InitializeAsync(createParameters);
            }
            else if (resLoadMode == EPlayMode.HostPlayMode)
            {
                var createParameters = new HostPlayModeParameters();
                createParameters.DecryptionServices = new GameDecryptionServices();
                createParameters.QueryServices = new GameQueryServices();
                createParameters.DefaultHostServer = GetHostServerURL();
                createParameters.FallbackHostServer = GetHostServerURL();
                initializationOperation = defaultPackage.InitializeAsync(createParameters);

                Log.Debug($"---- ResUpdateUrl GetHostServerURL[{createParameters.DefaultHostServer}]");
            }
            while (initializationOperation.IsDone == false)
            {
                await TimerComponent.Instance.WaitFrameAsync();
            }

            if (defaultPackage.InitializeStatus != EOperationStatus.Succeed)
            {
                Debug.LogError($"{initializationOperation.Error}");
            }
        }

        private async ETTask LoadGlobalConfig()
        {
            Log.Debug($"ET.MonoResComponent.LoadGlobalConfig 11");
            AssetOperationHandle handler = YooAssets.LoadAssetAsync<GlobalConfig>("GlobalConfig");

            Log.Debug($"ET.MonoResComponent.LoadGlobalConfig 22");
            while (handler.IsDone == false)
            {
                await TimerComponent.Instance.WaitFrameAsync();
            }
            Log.Debug($"ET.MonoResComponent.LoadGlobalConfig 33");
            GlobalConfig.Instance = handler.AssetObject as GlobalConfig;
            Log.Debug($"ET.MonoResComponent.LoadGlobalConfig 44");
            handler.Release();
            Log.Debug($"ET.MonoResComponent.LoadGlobalConfig 55");
            defaultPackage.UnloadUnusedAssets();
            Log.Debug($"ET.MonoResComponent.LoadGlobalConfig 66");
        }

        private void LoadResConfig()
        {
            ResConfig.Instance = Resources.Load<ResConfig>("ResConfig");
        }

        private async ETTask LoadGlobalConfigAsync()
        {
            AssetOperationHandle handler = YooAssets.LoadAssetAsync<GlobalConfig>("GlobalConfig");
            await handler;
            GlobalConfig.Instance = handler.AssetObject as GlobalConfig;
            handler.Release();
            defaultPackage.UnloadUnusedAssets();
        }

        public async ETTask Destroy()
        {
            YooAssets.Destroy();
        }

        public async ETTask ReLoadMap(bool releaseAB = false)
        {
            if (releaseAB)
            {
                ResourcePackage package = YooAssets.GetPackage("DefaultPackage");
                package.ForceUnloadAllAssets();
            }
            defaultPackage.ReLoadMap();
            await InitPackage();
            await this.LoadGlobalConfigAsync();
        }

        public async ETTask ReLoadWhenDebugConnect()
        {
            EPlayMode resLoadMode = ResConfig.Instance.ResLoadMode;
            if (resLoadMode == EPlayMode.HostPlayMode)
            {
                string packageName = "DefaultPackage";
                defaultPackage = YooAssets.TryGetPackage(packageName);
                defaultPackage.ResetHostServer(GetHostServerURL());

                YooAssets.Clear();
            }

            await ETTask.CompletedTask;
        }

        public byte[] LoadRawFile(string location)
        {
            RawFileOperationHandle handle = YooAssets.LoadRawFileSync(location);
            return handle.GetRawFileData();
        }

        public async ETTask<byte[]> LoadRawFileAsync(string location)
        {
            RawFileOperationHandle handle = YooAssets.LoadRawFileAsync(location);
            await handle;
            return handle.GetRawFileData();
        }

        public AssetOperationHandle LoadAssetAsync<T>(string location)where T: UnityEngine.Object
        {
            return YooAssets.LoadAssetAsync<T>(location);
        }

        public string[] GetAddressesByTag(string tag)
        {
            AssetInfo[] assetInfos = YooAssets.GetAssetInfos(tag);
            string[] addresses = new string[assetInfos.Length];
            for(int i = 0; i < assetInfos.Length; i++)
            {
                addresses[i] = assetInfos[i].Address;
            }

            return addresses;
        }

        /// <summary>
        /// 获取资源服务器版本地址
        /// </summary>
        public static string GetHostServerVersionURL()
        {
            //string hostServerIP = "http://10.0.2.2"; //安卓模拟器地址
            string hostServerIP = ResConfig.Instance.ResHostServerIP;
            string version = "Version.txt";

#if UNITY_EDITOR
            if (UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.Android)
                return $"{hostServerIP}/CDN/Android/{version}";
            else if (UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.iOS)
                return $"{hostServerIP}/CDN/IOS/{version}";
            else if (UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.WebGL)
                return $"{hostServerIP}/CDN/WebGL/{version}";
            else
                return $"{hostServerIP}/CDN/PC/{version}";
#else
		    if (Application.platform == RuntimePlatform.Android)
			    return $"{hostServerIP}/CDN/Android/{version}";
		    else if (Application.platform == RuntimePlatform.IPhonePlayer)
			    return $"{hostServerIP}/CDN/IOS/{version}";
		    else if (Application.platform == RuntimePlatform.WebGLPlayer)
			    return $"{hostServerIP}/CDN/WebGL/{version}";
		    else
			    return $"{hostServerIP}/CDN/PC/{version}";
#endif
        }

        /// <summary>
        /// 获取资源服务器地址
        /// </summary>
        private string GetHostServerURL()
        {
            //string hostServerIP = "http://10.0.2.2"; //安卓模拟器地址
            string hostServerIP = ResConfig.Instance.ResHostServerIP;
            string gameVersion = ResConfig.Instance.ResGameVersion;

#if UNITY_EDITOR
            if (UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.Android)
                return $"{hostServerIP}/CDN/Android/{gameVersion}";
            else if (UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.iOS)
                return $"{hostServerIP}/CDN/IOS/{gameVersion}";
            else if (UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.WebGL)
                return $"{hostServerIP}/CDN/WebGL/{gameVersion}";
            else
                return $"{hostServerIP}/CDN/PC/{gameVersion}";
#else
		    if (Application.platform == RuntimePlatform.Android)
			    return $"{hostServerIP}/CDN/Android/{gameVersion}";
		    else if (Application.platform == RuntimePlatform.IPhonePlayer)
			    return $"{hostServerIP}/CDN/IOS/{gameVersion}";
		    else if (Application.platform == RuntimePlatform.WebGLPlayer)
			    return $"{hostServerIP}/CDN/WebGL/{gameVersion}";
		    else
			    return $"{hostServerIP}/CDN/PC/{gameVersion}";
#endif
        }

        /// <summary>
        /// 内置文件查询服务类
        /// </summary>
        private class GameQueryServices : IQueryServices
        {
            public bool QueryStreamingAssets(string fileName)
            {
                // 注意：使用了BetterStreamingAssets插件，使用前需要初始化该插件！
                string buildinFolderName = YooAssets.GetStreamingAssetBuildinFolderName();
                return StreamingAssetsHelper.FileExists($"{buildinFolderName}/{fileName}");
            }
        }

        /// <summary>
        /// 资源文件解密服务类
        /// </summary>
        private class GameDecryptionServices : IDecryptionServices
        {
            public ulong LoadFromFileOffset(DecryptFileInfo fileInfo)
            {
                return 32;
            }

            public byte[] LoadFromMemory(DecryptFileInfo fileInfo)
            {
                throw new NotImplementedException();
            }

            public Stream LoadFromStream(DecryptFileInfo fileInfo)
            {
                BundleStream bundleStream = new BundleStream(fileInfo.FilePath, FileMode.Open);
                return bundleStream;
            }

            public uint GetManagedReadBufferSize()
            {
                return 1024;
            }
        }
    }
}
