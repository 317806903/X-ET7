using BlurBackground;
using MirrorVerse;
using MirrorVerse.UI.MirrorSceneClassyUI;
using System;
using System.Reflection;
using UnityEngine;
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
		Action OnMenuExitSceneCallBack,
		Action OnMenuLoadRecentSceneCallBack,
		Action<string> OnMenuJoinSceneCallBack,
		Func<(bool, string)> OnRequestQRCodeExtraData,
		string arSceneId,
		bool bForceIntoCreate,
		bool bForceIntoScan)
		{
			self.OnMenuCancelCallBack = OnMenuCancelCallBack;
			self.OnMenuFinishedCallBack = OnMenuFinishedCallBack;
			self.OnMenuCreateSceneCallBack = OnMenuCreateSceneCallBack;
			self.OnMenuExitSceneCallBack = OnMenuExitSceneCallBack;
			self.OnMenuLoadRecentSceneCallBack = OnMenuLoadRecentSceneCallBack;
			self.OnMenuJoinSceneCallBack = OnMenuJoinSceneCallBack;
			self.OnRequestQRCodeExtraData = OnRequestQRCodeExtraData;

			await self.InitCallBack(arSceneId, bForceIntoCreate, bForceIntoScan);
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
			self.StaticMeshTran = self.ARSessoinGo.transform.Find("MirrorSceneAll_Classy/MirrorSceneRenderer/StaticMesh");
			Transform ARCameraTrans = self.ARSessoinGo.transform.Find("MirrorSceneAll_Classy/ArFoundationAdapter/ArFoundationCamera");
			self.ARCamera = ARCameraTrans.gameObject.GetComponent<Camera>();

			self.ScaleARCameraGo = self.ARSessoinGo.transform.Find("ArScaleCamera").gameObject;
			self.ScaleARCamera = self.ScaleARCameraGo.transform.GetComponent<Camera>();

			self.translucentImageSource = ARCameraTrans.gameObject.GetComponent<TranslucentImageSource>();

			self.QrCodeImageTran = self.ARSessoinGo.transform.Find("MirrorSceneAll_Classy/MirrorSceneRenderer/CanvasRoot/MarkerCanvas/Canvas/Panel/QrCodeImage");

			// Set backend by Language.
			SetArSessionServiceEndpoint();

			self.RegisterCallBack();
			await TimerComponent.Instance.WaitFrameAsync();
		}

		private static void SetArSessionServiceEndpoint()
		{
			// If not CN, set the backend to AWS North America.
			const BindingFlags InstanceBindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
			var coreValue = GetCoreImplInternalObject();
			var setArSessionServiceEndpointMethod = coreValue.GetType().GetMethod("SetArSessionServiceEndpoint", InstanceBindFlags);
			Log.Debug($"SetArSessionServiceEndpoint {setArSessionServiceEndpointMethod}");

			string endpoint = ResConfig.Instance.areaType switch
			{
				AreaType.CN => "CN",
				AreaType.EN => "US",
				_ => "US"
			};

			Log.Debug($"SetArSessionServiceEndpoint is set to: [{endpoint}]");
			setArSessionServiceEndpointMethod.Invoke(coreValue, new object[] { endpoint });
		}

		public static void RegisterCallBack(this ARSessionComponent self)
		{
			ClassyUI.Instance.onMenuFinish += ()=>
			{
				GameObject go = null;
				StatusOr<GameObject> sceneMesh = MirrorScene.Get().GetSceneMeshWrapperObject();
				if (sceneMesh.HasValue && sceneMesh.Value != null)
				{
					go = sceneMesh.Value;
				}

				self.OnMenuFinished(go);
			};
			ClassyUI.Instance.onMenuCancel += ()=>
			{
				self.OnMenuExitSceneCallBack();
				self.OnMenuCancel();
			};
			ClassyUI.Instance.onMenuCreateScene += ()=>
			{
				self.OnMenuCreateSceneCallBack();
			};
			ClassyUI.Instance.onMenuExitScene += ()=>
			{
			};
			ClassyUI.Instance.onMenuLoadRecentScene += ()=>
			{
				self.OnMenuLoadRecentSceneCallBack();
			};
			ClassyUI.Instance.onMenuJoinScene += (sQRCodeInfo)=>
			{
				self.OnMenuJoinSceneCallBack(sQRCodeInfo);
			};
			ClassyUI.Instance.onRequestQRCodeExtraData += ()=>
			{
				return self.OnRequestQRCodeExtraData();
			};

		}

		public static async ETTask InitCallBack(this ARSessionComponent self, string arSceneId, bool bForceIntoCreate, bool bForceIntoScan)
		{
#if UNITY_EDITOR
			self.OnMenuCancel();
			return;
#endif
			await self.LoadARSession();

			if (MirrorScene.IsAvailable())
			{
				self.ResetMainCamera(true);

				//self.SetMeshShow();

				if(bForceIntoCreate)
				{
					ClassyUI.Instance.RestartToCreate();
				}
				else if(bForceIntoScan)
				{
					Log.Debug($"---InitCallBack-- 43 111 {ClassyUI.Instance}");
					ClassyUI.Instance.RestartToJoin();
					Log.Debug($"---InitCallBack-- 43 222");
				}

				Log.Debug($"arSceneId [{arSceneId}]");
				if (string.IsNullOrEmpty(arSceneId) == false)
				{
					Log.Debug($"InitCallBack 00");
					ProcessingMenu.Instance.UpdateProcessingText(ProcessingState.Downloading);
					Log.Debug($"InitCallBack 11");
					ClassyUI.Instance.SwitchMenu(SystemMenuType.ProcessingMenu);
					Log.Debug($"InitCallBack 22");
					await TimerComponent.Instance.WaitFrameAsync();
					Log.Debug($"InitCallBack 33");
					ClassyUI.Instance.TriggerJoinScene(arSceneId);
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
				ClassyUI.Instance.Restart();
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
				ClassyUI.Instance.HideMenu();
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

		private static object GetCoreImplInternalObject()
		{
			Log.Debug($"====zpb xxxxxxxxxxxxxx 0==========");
			IMirrorScene iMirrorScene = MirrorScene.Get();
			const BindingFlags InstanceBindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
			Log.Debug($"==== 000 = iMirrorScene={iMirrorScene}");
			var coreField = iMirrorScene.GetType().GetField("_core", InstanceBindFlags);
			Log.Debug($"==== 111 = coreField={coreField}");
			var coreValue = coreField.GetValue(iMirrorScene);
			Log.Debug($"coreValue {coreValue}");
			return coreValue;
		}

		public static string GetARMeshDownLoadUrl(this ARSessionComponent self)
		{
			const BindingFlags InstanceBindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
			var coreValue = GetCoreImplInternalObject();
			var getSceneMeshUrlMethod = coreValue.GetType().GetMethod("GetSceneMeshUrl", InstanceBindFlags);
			Log.Debug($"getSceneMeshUrlMethod {getSceneMeshUrlMethod}");
			var meshUrlValue = getSceneMeshUrlMethod.Invoke(coreValue, null);

			Log.Debug($"meshUrlValue {meshUrlValue}");
			return meshUrlValue.ToString();
		}

		public static void ResetQrCodeImageSize(this ARSessionComponent self)
		{
			self.QrCodeImageTran.localScale = Vector3.one / MainQualitySettingComponent.Instance.GetHeightScale();
		}

		public static void TriggerShowQrCode(this ARSessionComponent self)
		{
			ClassyUI.Instance.TriggerShowQrCode();
			self.ResetQrCodeImageSize();
		}

		public static bool ChkARSceneStatusCompleted(this ARSessionComponent self)
		{
			if (ClassyUI.Instance == null)
			{
				return false;
			}
			return MirrorScene.Get().GetSceneStatus() == SceneStatus.Completed;
        }

		public static void ResetMainCamera(this ARSessionComponent self, bool isARCamera)
		{
			if (self.ARSessoinGo == null)
			{
				return;
			}

			self.SetScaleARCamera(1);
			if (isARCamera)
			{
				if (self.ARSessoinGo != null)
				{
					self.ARSessoinGo.SetActive(true);
					self.ResetQrCodeImageSize();
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

			if (self.ARCamera == null)
			{
				return null;
			}

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

		public static void ShowARMesh(this ARSessionComponent self, bool show)
		{
			Log.Debug($"ARSessionComponent SetMeshShow");
			Transform staticMeshTrans = self.StaticMeshTran;
			MirrorVerse.UI.Renderers.StaticMeshRenderer staticMeshRenderer = staticMeshTrans.gameObject.GetComponent<MirrorVerse.UI.Renderers.StaticMeshRenderer>();
			MirrorVerse.Options.StaticMeshRendererOptions staticMeshRendererOptions = staticMeshRenderer.options;
			if (show)
			{
				staticMeshRendererOptions.visible = true;
				staticMeshRendererOptions.collidable = true;
				staticMeshRendererOptions.withOcclusion = false;
				staticMeshRendererOptions.castsShadow = false;
				staticMeshRendererOptions.receivesShadow = false;
			}
			else
			{
				staticMeshRendererOptions.visible = true;
				staticMeshRendererOptions.collidable = true;
				staticMeshRendererOptions.withOcclusion = true;
				staticMeshRendererOptions.castsShadow = false;
				staticMeshRendererOptions.receivesShadow = true;
			}
		}

		public static void OnMenuCancel(this ARSessionComponent self)
		{
			Log.Debug($"ARSessionComponent OnMenuCancel");
			self.ResetMainCamera(false);
			self.OnMenuCancelCallBack();
		}

		public static bool ChkARCameraEnable(this ARSessionComponent self)
		{
			if (self.ARCamera != null && self.ARCamera.enabled)
			{
				return true;
			}
			return false;
		}

	}
}
