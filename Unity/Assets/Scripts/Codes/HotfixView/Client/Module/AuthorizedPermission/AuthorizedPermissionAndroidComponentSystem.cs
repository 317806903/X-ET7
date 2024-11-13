using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using ET.Client;
using Unity.Mathematics;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

namespace ET.Client
{
    [FriendOf(typeof (AuthorizedPermissionAndroidComponent))]
    public static class AuthorizedPermissionAndroidComponentSystem
    {
        [ObjectSystem]
        public class AuthorizedPermissionAndroidComponentAwakeSystem: AwakeSystem<AuthorizedPermissionAndroidComponent>
        {
            protected override void Awake(AuthorizedPermissionAndroidComponent self)
            {
                self.Init();
            }
        }

        [ObjectSystem]
        public class AuthorizedPermissionAndroidComponentDestroySystem: DestroySystem<AuthorizedPermissionAndroidComponent>
        {
            protected override void Destroy(AuthorizedPermissionAndroidComponent self)
            {
                if (self.root != null)
                {
                    GameObject.Destroy(self.root);
                    self.root = null;
                }
            }
        }

        [ObjectSystem]
        public class AuthorizedPermissionAndroidComponentFixedUpdateSystem: FixedUpdateSystem<AuthorizedPermissionAndroidComponent>
        {
            protected override void FixedUpdate(AuthorizedPermissionAndroidComponent self)
            {
                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static void Init(this AuthorizedPermissionAndroidComponent self)
        {
            GameObject go = new GameObject("AuthorizedPermissionAndroid");
            self.root = go;
            go.transform.SetParent(GlobalComponent.Instance.ClientManagerRoot);
            go.transform.localPosition = UnityEngine.Vector3.zero;
            go.transform.localScale = UnityEngine.Vector3.one;
        }

        public static void FixedUpdate(this AuthorizedPermissionAndroidComponent self, float fixedDeltaTime)
        {
        }

		public static async ETTask<bool> ChkCameraAuthorization(this AuthorizedPermissionAndroidComponent self)
		{
			Log.Debug($"000 00 {UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.Camera)}");

			if (UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.Camera) == false)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public static async ETTask ChkCameraAuthorizationAndRequest(this AuthorizedPermissionAndroidComponent self, Action<bool, bool> callBack)
		{
			Log.Debug($"000 11 {UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.Camera)}");

			if (UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.Camera) == false)
			{
				self.callBack = callBack;

				if (self.permissionCallbacks == null)
				{
					self.permissionCallbacks = new UnityEngine.Android.PermissionCallbacks();
					self.permissionCallbacks.PermissionDenied -= self.OnPermissionDenied;
					self.permissionCallbacks.PermissionDenied += self.OnPermissionDenied;
					self.permissionCallbacks.PermissionGranted -= self.OnPermissionGranted;
					self.permissionCallbacks.PermissionGranted += self.OnPermissionGranted;
					self.permissionCallbacks.PermissionDeniedAndDontAskAgain -= self.OnPermissionDeniedAndDontAskAgain;
					self.permissionCallbacks.PermissionDeniedAndDontAskAgain += self.OnPermissionDeniedAndDontAskAgain;
				}

				UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.Camera, self.permissionCallbacks);
			}
			else
			{
				callBack(true, false);
			}
		}

		/// <summary>
		/// 申请权限被拒绝
		/// </summary>
		/// <param name="permission"></param>
		private static void OnPermissionDenied(this AuthorizedPermissionAndroidComponent self, string permission)
		{
			Log.Error($"== OnPermissionDenied {permission}");
			self.callBack(false, true);
		}

		/// <summary>
		/// 申请权限成功
		/// </summary>
		/// <param name="permission"></param>
		private static void OnPermissionGranted(this AuthorizedPermissionAndroidComponent self, string permission)
		{
			Log.Error($"== OnPermissionGranted {permission}");
			self.RemoveDontAskAgainPermission(permission);
			self.callBack(true, false);
		}

		/// <summary>
		/// 申请权限被拒绝,且不再询问
		/// </summary>
		/// <param name="permission"></param>
		private static void OnPermissionDeniedAndDontAskAgain(this AuthorizedPermissionAndroidComponent self, string permission)
		{
			Log.Error($"== OnPermissionDeniedAndDontAskAgain {permission}");
			self.SetDontAskAgainPermission(permission);
			self.callBack(false, true);
		}

		public static void SetDontAskAgainPermission(this AuthorizedPermissionAndroidComponent self, string permission)
		{
			string key = $"AuthorizedPermissionAndroid_{permission}";
			PlayerPrefs.SetInt(key, 1);
		}

		public static void RemoveDontAskAgainPermission(this AuthorizedPermissionAndroidComponent self, string permission)
		{
			string key = $"AuthorizedPermissionAndroid_{permission}";
			PlayerPrefs.DeleteKey(key);
		}

		public static bool ChkDontAskAgainPermission(this AuthorizedPermissionAndroidComponent self, string permission)
		{
			string key = $"AuthorizedPermissionAndroid_{permission}";
			if (PlayerPrefs.HasKey(key))
			{
				return false;
			}
			int value = PlayerPrefs.GetInt(key);
			return value == 1;
		}

		public static async ETTask JumpToSettings(this AuthorizedPermissionAndroidComponent self)
		{
			using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			{
				AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

				// 尝试使用权限设置 Intent
				using (AndroidJavaObject intentPermissionSettings = new AndroidJavaObject("android.content.Intent", "android.settings.PERMISSION_SETTINGS"))
				{
					try
					{
						activity.Call("startActivity", intentPermissionSettings);
					}
					catch (AndroidJavaException e)
					{
						// 创建 Intent 跳转到权限设置界面
						using (AndroidJavaObject intentSettings = new AndroidJavaObject("android.content.Intent", "android.settings.APPLICATION_DETAILS_SETTINGS"))
						{
							string packageName = activity.Call<string>("getPackageName");
							// 使用 Uri.parse 创建 URI
							using (AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri"))
							{
								AndroidJavaObject uri = uriClass.CallStatic<AndroidJavaObject>("parse", "package:" + packageName);
								intentSettings.Call<AndroidJavaObject>("setData", uri);
							}

							// 启动 Activity
							activity.Call("startActivity", intentSettings);
						}
					}
				}
			}
		}
    }
}