using System;
using UnityEngine;
using MirrorVerse;
using MirrorVerse.UI.MirrorSceneDefaultUI;
using UnityEngine.Rendering.Universal;

namespace ET.Client
{
	[FriendOf(typeof(ARSessionComponent))]
	public static class ARSessionComponentSystem
	{
		[ObjectSystem]
		public class ARSessionComponentAwakeSystem : AwakeSystem<ARSessionComponent>
		{
			protected override void Awake(ARSessionComponent self)
			{
				self.Awake();
			}
		}

		[ObjectSystem]
		public class ARSessionComponentDestroySystem : DestroySystem<ARSessionComponent>
		{
			protected override void Destroy(ARSessionComponent self)
			{
				self.ResetMainCamera(false);
				if (self.ARSessoinGo != null)
				{
					GameObject.Destroy(self.ARSessoinGo);
					self.ARSessoinGo = null;
				}
			}
		}

		private static void Awake(this ARSessionComponent self)
		{
			self.LoadARSession().Coroutine();
		}

		public static void Init(this ARSessionComponent self, Action OnMenuCancelCallBack, Action OnMenuFinishedCallBack)
		{
			self.OnMenuCancelCallBack = OnMenuCancelCallBack;
			self.OnMenuFinishedCallBack = OnMenuFinishedCallBack;
		}

		public static async ETTask LoadARSession(this ARSessionComponent self)
		{
			GameObject ARSessionPrefab = await ResComponent.Instance.LoadAssetAsync<GameObject>("ARSession");
			ARSessionPrefab = GameObject.Instantiate(ARSessionPrefab);
			ARSessionPrefab.name = ARSessionPrefab.name.Replace("(Clone)", "");
			self.ARSessoinGo = ARSessionPrefab;
			
			UnityEngine.Object.DontDestroyOnLoad(self.ARSessoinGo);
			Transform ARCameraTrans = self.ARSessoinGo.transform.Find("MirrorSceneAll/ArFoundationAdapter/ArFoundationCamera");
			self.ARCamera = ARCameraTrans.gameObject.GetComponent<Camera>();

			if (MirrorScene.IsAvailable())
			{
				self.ResetMainCamera(true);

				DefaultUI.Instance.onMenuFinish += ()=>
				{
					self.OnMenuFinished();
				};
				DefaultUI.Instance.onMenuCancel += ()=>
				{
					self.OnMenuCancel();
				};
				DefaultUI.Instance.Restart();
			}
			else
			{
				self.OnMenuCancel();
			}
		}
        
		public static void ResetMainCamera(this ARSessionComponent self, bool isARCamera)
		{
			if (isARCamera)
			{
				GlobalComponent.Instance.MainCamera.enabled = false;
				if (self.ARCamera != null)
				{
					self.ARCamera.enabled = true;
					UniversalAdditionalCameraData universalAdditionalCameraDataMain = GlobalComponent.Instance.MainCamera.gameObject.GetComponent<UniversalAdditionalCameraData>();
					UniversalAdditionalCameraData universalAdditionalCameraDataAR = self.ARCamera.gameObject.GetComponent<UniversalAdditionalCameraData>();
					universalAdditionalCameraDataAR.cameraStack.Clear();
					universalAdditionalCameraDataAR.cameraStack.AddRange(universalAdditionalCameraDataMain.cameraStack);
				}
			}
			else
			{
				GlobalComponent.Instance.MainCamera.enabled = true;
				if (self.ARCamera != null)
				{
					self.ARCamera.enabled = false;
				}
			}

		}

		public static void OnMenuFinished(this ARSessionComponent self)
		{
			Log.Debug($"ARSessionComponent OnMenuFinished");
			self.OnMenuFinishedCallBack();
		}

		public static void OnMenuCancel(this ARSessionComponent self)
		{
			Log.Debug($"ARSessionComponent OnMenuCancel");
			self.OnMenuCancelCallBack();
		}

	}
}
