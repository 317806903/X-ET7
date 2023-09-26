using System;
using System.Reflection;
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
		}

		public static async ETTask Init(this ARSessionComponent self,
		Action OnMenuCancelCallBack,
		Action OnMenuFinishedCallBack,
		Action OnMenuCreateSceneCallBack,
		Action OnMenuQuitSceneCallBack,
		Action OnMenuJoinByExistSceneCallBack,
		Action<string> OnMenuJoinByQRCodeCallBack,
		Func<(bool, string)> OnGetQRCodeInfo,
		string arSceneId)
		{
			self.OnMenuCancelCallBack = OnMenuCancelCallBack;
			self.OnMenuFinishedCallBack = OnMenuFinishedCallBack;
			self.OnMenuCreateSceneCallBack = OnMenuCreateSceneCallBack;
			self.OnMenuQuitSceneCallBack = OnMenuQuitSceneCallBack;
			self.OnMenuJoinByExistSceneCallBack = OnMenuJoinByExistSceneCallBack;
			self.OnMenuJoinByQRCodeCallBack = OnMenuJoinByQRCodeCallBack;
			self.OnGetQRCodeInfo = OnGetQRCodeInfo;

			await self.InitCallBack(arSceneId);
		}

		public static async ETTask LoadARSession(this ARSessionComponent self)
		{
			if (self.ARSessoinGo != null)
			{
				return;
			}
			GameObject ARSessionPrefab = await ResComponent.Instance.LoadAssetAsync<GameObject>("ARSession");
			ARSessionPrefab = GameObject.Instantiate(ARSessionPrefab);
			ARSessionPrefab.name = ARSessionPrefab.name.Replace("(Clone)", "");
			self.ARSessoinGo = ARSessionPrefab;

			UnityEngine.Object.DontDestroyOnLoad(self.ARSessoinGo);
			self.StaticMeshTran = self.ARSessoinGo.transform.Find("MirrorSceneAll/MirrorSceneRenderer/StaticMesh");
			Transform ARCameraTrans = self.ARSessoinGo.transform.Find("MirrorSceneAll/ArFoundationAdapter/ArFoundationCamera");
			self.ARCamera = ARCameraTrans.gameObject.GetComponent<Camera>();

			self.ScaleARCameraGo = self.ARSessoinGo.transform.Find("MirrorSceneAll/ArScaleCamera").gameObject;
			self.ScaleARCamera = self.ScaleARCameraGo.transform.GetComponent<Camera>();
		}

		public static async ETTask InitCallBack(this ARSessionComponent self, string arSceneId)
		{
			await self.LoadARSession();

			if (MirrorScene.IsAvailable())
			{
				self.ResetMainCamera(true);

				DefaultUI.Instance.onMenuFinish += ()=>
				{
					GameObject go = null;
					StatusOr<GameObject> sceneMesh = MirrorScene.Get().GetSceneMeshWrapperObject();
					if (sceneMesh.HasValue && sceneMesh.Value != null)
					{
						go = sceneMesh.Value;
					}

					self.OnMenuFinished(go);
				};
				DefaultUI.Instance.onMenuCancel += ()=>
				{
					self.OnMenuCancel();
				};
				DefaultUI.Instance.onMenuCreateScene += ()=>
				{
					self.OnMenuCreateSceneCallBack();
				};
				DefaultUI.Instance.onMenuQuitScene += ()=>
				{
					self.OnMenuQuitSceneCallBack();
				};
				DefaultUI.Instance.onMenuJoinByExistScene += ()=>
				{
					self.OnMenuJoinByExistSceneCallBack();
				};
				DefaultUI.Instance.onMenuJoinByQRCode += (sQRCodeInfo)=>
				{
					self.OnMenuJoinByQRCodeCallBack(sQRCodeInfo);
				};
				DefaultUI.Instance.onGetQRCodeInfo += ()=>
				{
					return self.OnGetQRCodeInfo();
				};

				//self.SetMeshShow();

				DefaultUI.Instance.Restart();

				Log.Debug($"arSceneId [{arSceneId}]");
				if (string.IsNullOrEmpty(arSceneId) == false)
				{
					Log.Debug($"InitCallBack 00");
					ProcessingMenu.Instance.UpdateProcessingText(ProcessingState.Downloading);
					Log.Debug($"InitCallBack 11");
					DefaultUI.Instance.SwitchMenu(SystemMenuType.ProcessingMenu);
					Log.Debug($"InitCallBack 22");
					await TimerComponent.Instance.WaitFrameAsync();
					Log.Debug($"InitCallBack 33");
					DefaultUI.Instance.TriggerJoinScene(arSceneId);
					Log.Debug($"InitCallBack 44");
				}
			}
			else
			{
				self.OnMenuCancel();
			}
		}

		public static async ETTask ReStart(this ARSessionComponent self)
		{
			if (MirrorScene.IsAvailable())
			{
				self.ResetMainCamera(true);
				DefaultUI.Instance.Restart();
			}
			else
			{
				self.OnMenuCancel();
			}
		}

		public static async ETTask HideMenu(this ARSessionComponent self)
		{
			if (MirrorScene.IsAvailable())
			{
				DefaultUI.Instance.HideMenu();
			}
			else
			{
			}
		}

		public static string GetARSceneId(this ARSessionComponent self)
		{
			StatusOr<SceneInfo> sceneInfo = MirrorScene.Get().GetSceneInfo();
			if (sceneInfo.HasValue)
			{
				return sceneInfo.Value.sceneId;
			}

			return "";
		}

		public static string GetARMeshDownLoadUrl(this ARSessionComponent self)
		{
			Log.Debug($"====zpb xxxxxxxxxxxxxx 0==========");
			IMirrorScene iMirrorScene = MirrorScene.Get();
			const BindingFlags InstanceBindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
			Log.Debug($"==== 000 = iMirrorScene={iMirrorScene}");
			var coreField = iMirrorScene.GetType().GetField("_core", InstanceBindFlags);
			Log.Debug($"==== 111 = coreField={coreField}");
			var coreValue = coreField.GetValue(iMirrorScene);
			Log.Debug($"coreValue {coreValue}");

			var arSessionField = coreValue.GetType().GetField("_arSession", InstanceBindFlags);
			Log.Debug($"arSessionField {arSessionField}");
			var arSessionValue = arSessionField.GetValue(coreValue);
			Log.Debug($"arSessionValue {arSessionValue}");

			var meshUrlProperTy = arSessionValue.GetType().GetProperty("MeshUrl", InstanceBindFlags);
			Log.Debug($"meshUrlProperTy {meshUrlProperTy}");
			var meshUrlValue = meshUrlProperTy.GetValue(arSessionValue);

			Log.Debug($"meshUrlValue {meshUrlValue}");
			return meshUrlValue.ToString();
		}

		public static void TriggerShowQrCode(this ARSessionComponent self)
		{
			DefaultUI.Instance.TriggerShowQrCode();
		}

		public static bool ChkARSceneStatusCompleted(this ARSessionComponent self)
		{
			if (DefaultUI.Instance == null)
			{
				return false;
			}
			return DefaultUI.Instance.ChkSceneStatusCompleted();
		}

		public static void ResetMainCamera(this ARSessionComponent self, bool isARCamera)
		{
			self.SetScaleARCamera(1);
			if (isARCamera)
			{
				if (self.ARSessoinGo != null)
				{
					self.ARSessoinGo.SetActive(true);
				}

				AudioListener audioListenerMain = GlobalComponent.Instance.MainCamera.gameObject.GetComponent<AudioListener>();
				audioListenerMain.enabled = false;
				GlobalComponent.Instance.MainCamera.enabled = false;
				if (self.ARCamera != null)
				{
					AudioListener audioListenerAR = self.ARCamera.gameObject.GetComponent<AudioListener>();
					audioListenerAR.enabled = true;
					self.ARCamera.enabled = true;
					UniversalAdditionalCameraData universalAdditionalCameraDataMain = GlobalComponent.Instance.MainCamera.gameObject.GetComponent<UniversalAdditionalCameraData>();
					UniversalAdditionalCameraData universalAdditionalCameraDataAR = self.ARCamera.gameObject.GetComponent<UniversalAdditionalCameraData>();
					universalAdditionalCameraDataAR.cameraStack.Clear();
					universalAdditionalCameraDataAR.cameraStack.AddRange(universalAdditionalCameraDataMain.cameraStack);
				}
			}
			else
			{
				if (self.ARSessoinGo != null)
				{
					self.ARSessoinGo.SetActive(false);
				}

				AudioListener audioListenerMain = GlobalComponent.Instance.MainCamera.gameObject.GetComponent<AudioListener>();
				audioListenerMain.enabled = true;
				GlobalComponent.Instance.MainCamera.enabled = true;
				if (self.ARCamera != null)
				{
					AudioListener audioListenerAR = self.ARCamera.gameObject.GetComponent<AudioListener>();
					audioListenerAR.enabled = false;
					self.ARCamera.enabled = false;
				}
			}

		}

		public static Camera SetScaleARCamera(this ARSessionComponent self, float arScale)
		{
			Log.Debug($"ARSessionComponent SetScaleARCamera arScale[{arScale}] self.ScaleARCameraGo[{self.ScaleARCameraGo}]");

			AudioListener audioListenerAR = self.ARCamera.gameObject.GetComponent<AudioListener>();
			audioListenerAR.enabled = false;
			AudioListener audioListenerARScale = self.ScaleARCamera.gameObject.GetComponent<AudioListener>();
			audioListenerARScale.enabled = false;
			if (self.ScaleARCameraGo == null)
			{
				self.CurARCamera = self.ARCamera;
				if (self.StaticMeshTran != null)
				{
					self.StaticMeshTran.localScale = Vector3.one;
				}
				return self.CurARCamera;
			}
			if (arScale == 1)
			{
				self.ScaleARCameraGo.GetComponent<FollowARCamera>().ResetARCamera();
				self.CurARCamera = self.ARCamera;
				self.StaticMeshTran.localScale = Vector3.one;
				return self.CurARCamera;
			}
			else
			{
				self.ScaleARCameraGo.GetComponent<FollowARCamera>().SetARCamera(self.ARCamera, arScale);
				self.CurARCamera = self.ScaleARCamera;
				self.StaticMeshTran.localScale = new Vector3(arScale, arScale, arScale);
				return self.CurARCamera;
			}
		}

		public static void OnMenuFinished(this ARSessionComponent self, GameObject go)
		{
			Log.Debug($"ARSessionComponent OnMenuFinished go[{go}]");
			foreach (var subTran in go.GetComponentsInChildren<Transform>(true))
			{
				Log.Debug($"subTran[{subTran}] [{LayerMask.NameToLayer("Map")}]");
				subTran.gameObject.layer = LayerMask.NameToLayer("Map");
			}

			self.OnMenuFinishedCallBack();
		}

		public static void SetMeshShow(this ARSessionComponent self)
		{
			Log.Debug($"ARSessionComponent SetMeshShow");
			Transform staticMeshTrans = self.StaticMeshTran;
			MirrorVerse.UI.Renderers.StaticMeshRenderer staticMeshRenderer = staticMeshTrans.gameObject.GetComponent<MirrorVerse.UI.Renderers.StaticMeshRenderer>();
			MirrorVerse.Options.StaticMeshRendererOptions staticMeshRendererOptions = staticMeshRenderer.options;
			staticMeshRendererOptions.visible = true;
			staticMeshRendererOptions.collidable = true;
			staticMeshRendererOptions.withOcclusion = false;
			staticMeshRendererOptions.castsShadow = false;
			staticMeshRendererOptions.receivesShadow = false;

		}

		public static void OnMenuCancel(this ARSessionComponent self)
		{
			Log.Debug($"ARSessionComponent OnMenuCancel");
			self.ResetMainCamera(false);
			self.OnMenuCancelCallBack();
		}

	}
}
