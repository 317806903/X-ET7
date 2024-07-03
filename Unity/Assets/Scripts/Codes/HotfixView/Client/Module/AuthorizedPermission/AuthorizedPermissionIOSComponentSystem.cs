using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using ET.Client;
using Unity.Mathematics;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

namespace ET.Client
{
    [FriendOf(typeof (AuthorizedPermissionIOSComponent))]
    public static class AuthorizedPermissionIOSComponentSystem
    {
        [ObjectSystem]
        public class AuthorizedPermissionIOSComponentAwakeSystem: AwakeSystem<AuthorizedPermissionIOSComponent>
        {
            protected override void Awake(AuthorizedPermissionIOSComponent self)
            {
                self.Init();
            }
        }

        [ObjectSystem]
        public class AuthorizedPermissionIOSComponentDestroySystem: DestroySystem<AuthorizedPermissionIOSComponent>
        {
            protected override void Destroy(AuthorizedPermissionIOSComponent self)
            {
                if (self.root != null)
                {
                    GameObject.Destroy(self.root);
                    self.root = null;
                }
            }
        }

        [ObjectSystem]
        public class AuthorizedPermissionIOSComponentFixedUpdateSystem: FixedUpdateSystem<AuthorizedPermissionIOSComponent>
        {
            protected override void FixedUpdate(AuthorizedPermissionIOSComponent self)
            {
                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static void Init(this AuthorizedPermissionIOSComponent self)
        {
            GameObject go = new GameObject("AuthorizedPermissionIOS");
            self.root = go;
            go.transform.SetParent(GlobalComponent.Instance.ClientManagerRoot);
            go.transform.localPosition = UnityEngine.Vector3.zero;
            go.transform.localScale = UnityEngine.Vector3.one;
        }

        public static void FixedUpdate(this AuthorizedPermissionIOSComponent self, float fixedDeltaTime)
        {
        }

        public static async ETTask<bool> ChkCameraAuthorization(this AuthorizedPermissionIOSComponent self)
        {
            if (Application.HasUserAuthorization(UserAuthorization.WebCam) == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static async ETTask ChkCameraAuthorizationAndRequest(this AuthorizedPermissionIOSComponent self, Action<bool> callBack)
        {
            if (Application.HasUserAuthorization(UserAuthorization.WebCam) == false)
            {
                AsyncOperation asyncOperation = Application.RequestUserAuthorization(UserAuthorization.WebCam);
                while (asyncOperation.isDone == false)
                {
                    await TimerComponent.Instance.WaitFrameAsync();
                    if (self.IsDisposed)
                    {
                        Log.Error($"ChkCameraAuthorization_IOS self.IsDisposed");
                        callBack(false);
                        return;
                    }
                }
                bool bCameraAuthorization = Application.HasUserAuthorization(UserAuthorization.WebCam);
                callBack(bCameraAuthorization);
            }
            else
            {
                callBack(true);
            }
        }
    }
}