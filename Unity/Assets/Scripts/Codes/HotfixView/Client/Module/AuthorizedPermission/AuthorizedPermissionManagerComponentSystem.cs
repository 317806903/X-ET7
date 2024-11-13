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

        public static async ETTask<bool> ChkCameraAuthorization(this AuthorizedPermissionManagerComponent self)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                return await self.GetComponent<AuthorizedPermissionAndroidComponent>().ChkCameraAuthorization();
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                return await self.GetComponent<AuthorizedPermissionIOSComponent>().ChkCameraAuthorization();
            }
            return false;
        }

        public static async ETTask ChkCameraAuthorizationAndRequest(this AuthorizedPermissionManagerComponent self, Action<bool, bool> callBack)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                await self.GetComponent<AuthorizedPermissionAndroidComponent>().ChkCameraAuthorizationAndRequest(callBack);
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                await self.GetComponent<AuthorizedPermissionIOSComponent>().ChkCameraAuthorizationAndRequest(callBack);
            }
        }

        public static async ETTask JumpToSettings(this AuthorizedPermissionManagerComponent self)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                await self.GetComponent<AuthorizedPermissionAndroidComponent>().JumpToSettings();
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                await self.GetComponent<AuthorizedPermissionIOSComponent>().JumpToSettings();
            }
        }

    }
}