using System;
using UnityEngine;

namespace ET.Client
{
    [FriendOf(typeof (AuthorizedPermissionManagerComponent))]
    public static class AuthorizedPermissionManagerComponentSystem
    {
        [ObjectSystem]
        public class AuthorizedPermissionManagerComponentAwakeSystem: AwakeSystem<AuthorizedPermissionManagerComponent>
        {
            protected override void Awake(AuthorizedPermissionManagerComponent self)
            {
                AuthorizedPermissionManagerComponent.Instance = self;
                self.Init();
            }
        }

        [ObjectSystem]
        public class AuthorizedPermissionManagerComponentDestroySystem: DestroySystem<AuthorizedPermissionManagerComponent>
        {
            protected override void Destroy(AuthorizedPermissionManagerComponent self)
            {
                AuthorizedPermissionManagerComponent.Instance = null;
            }
        }

        public static void Init(this AuthorizedPermissionManagerComponent self)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                self.AddComponent<AuthorizedPermissionAndroidComponent>();
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                self.AddComponent<AuthorizedPermissionIOSComponent>();
            }
        }

        public static async ETTask ChkCameraAuthorization(this AuthorizedPermissionManagerComponent self, Action<bool> callBack)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                await self.GetComponent<AuthorizedPermissionAndroidComponent>().ChkCameraAuthorization(callBack);
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                await self.GetComponent<AuthorizedPermissionIOSComponent>().ChkCameraAuthorization(callBack);
            }
        }

    }
}