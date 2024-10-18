using BlurBackground;
using MirrorVerse;
using MirrorVerse.UI.MirrorSceneClassyUI;
using System;
using System.Reflection;
using ET.AbilityConfig;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace ET.Client
{
	[Invoke(TimerInvokeType.ARSessionComponentTimer)]
	public class ARSessionComponentTimer: ATimer<ARSessionComponent>
	{
		protected override void Run(ARSessionComponent self)
		{
			try
			{
				TimerComponent.Instance?.Remove(ref self.Timer);
				if (self.IsDisposed)
				{
					return;
				}
				self._ResetMainCamera(self.targetIsARCamera);
			}
			catch (Exception e)
			{
				Log.Error($"ARSessionComponent timer error: {self.Id}\n{e}");
			}
		}
	}

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
				TimerComponent.Instance?.Remove(ref self.Timer);
				self._ResetMainCamera(false);
				if (self.ARSessoinGo != null)
				{
					GameObject.Destroy(self.ARSessoinGo);
					self.ARSessoinGo = null;
				}

				self.MirrorSceneClassyUI = null;
			}
		}

		private static void Awake(this ARSessionComponent self)
		{
		}

		public static async ETTask Init(this ARSessionComponent self,
			Action OnMenuCancelCallBack,
			Action OnMenuFinishedCallBack,
			Action<bool> OnMenuCreateSceneCallBack,
			Action OnMenuExitSceneCallBack,
			Action<bool> OnMenuLoadRecentSceneCallBack,
			Action<string> OnMenuJoinSceneCallBack,
			Func<(bool, string)> OnRequestQRCodeExtraData,
			string arSceneId,
			ARHallType _ARHallType)
		{
			self.OnMenuCancelCallBack = OnMenuCancelCallBack;
			self.OnMenuFinishedCallBack = OnMenuFinishedCallBack;
			self.OnMenuCreateSceneCallBack = OnMenuCreateSceneCallBack;
			self.OnMenuExitSceneCallBack = OnMenuExitSceneCallBack;
			self.OnMenuLoadRecentSceneCallBack = OnMenuLoadRecentSceneCallBack;
			self.OnMenuJoinSceneCallBack = OnMenuJoinSceneCallBack;
			self.OnRequestQRCodeExtraData = OnRequestQRCodeExtraData;

			TimerComponent.Instance?.Remove(ref self.Timer);
			await self.InitCallBack(arSceneId, _ARHallType);
		}

		public static async ETTask<bool> ChkCameraAuthorization(this ARSessionComponent self)
		{
			return await AuthorizedPermissionManagerComponent.Instance.ChkCameraAuthorization();
		}

		public static async ETTask ChkCameraAuthorizationAndRequest(this ARSessionComponent self, Action<bool> callBack)
		{
			await AuthorizedPermissionManagerComponent.Instance.ChkCameraAuthorizationAndRequest(callBack);
		}

		public static async ETTask LoadARSession(this ARSessionComponent self, Action next, bool needShowTipBefore)
		{
			if (self.ARSessoinGo != null)
			{
				next();
				return;
			}

			bool isCameraAuthorization = await self.ChkCameraAuthorization();

			if (isCameraAuthorization || needShowTipBefore == false)
			{
				await self._LoadARSession(next);
			}
			else
			{
				string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_NeedCameraPermission_First_Des");
				ET.Client.UIManagerHelper.ShowOnlyConfirm(self.DomainScene(), tipMsg, async () =>
				{
					await self._LoadARSession(next);
				});
			}
		}

		public static async ETTask _LoadARSession(this ARSessionComponent self, Action next)
		{
			await self.ChkCameraAuthorizationAndRequest((bCameraAuthorization) =>
			{
				if (self.IsDisposed)
				{
					return;
				}
				EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
				{
					eventName = "CameraStarted",
					properties = new()
					{
						{"camera_success", bCameraAuthorization},
					}
				});
				if (bCameraAuthorization == false)
				{
					self.LoadARSessionErr(next).Coroutine();
				}
				else
				{
					self.LoadARSessionNext(next).Coroutine();
				}
			});
		}

		public static async ETTask LoadARSessionErr(this ARSessionComponent self, Action next)
		{
			await TimerComponent.Instance.WaitFrameAsync();
			while (true)
			{
				if (self.IsDisposed)
				{
					return;
				}
				if (ReLoginComponent.Instance != null && ReLoginComponent.Instance.isReCreateSessioning == false)
				{
					break;
				}
				await TimerComponent.Instance.WaitFrameAsync();
			}
			string titleText = LocalizeComponent.Instance.GetTextValue("TextCode_Key_NeedCameraPermission_Title");
			string message = LocalizeComponent.Instance.GetTextValue("TextCode_Key_NeedCameraPermission_Des");
			string sureText = LocalizeComponent.Instance.GetTextValue("TextCode_Key_NeedCameraPermission_Confirm");
			UIManagerHelper.ShowOnlyConfirm(self.DomainScene(), message, async () =>
			{
				EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeUIShowCommonLoading()
				{
					bForce = true,
				});
				await TimerComponent.Instance.WaitAsync(3000);
				EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeUIHideCommonLoading()
				{
					bForce = true,
				});

				self.LoadARSession(() =>
				{
					next();
				}, false).Coroutine();
			}, sureText, titleText);
		}

		public static bool ChkARMirrorSceneUIShow(this ARSessionComponent self)
		{
			if (self.MirrorSceneClassyUI == null)
			{
				return false;
			}
			if (self.MirrorSceneClassyUI.gameObject.activeSelf == false)
			{
				return false;
			}

			return true;
		}
		public static async ETTask LoadARSessionNext(this ARSessionComponent self, Action next)
		{
			MainQualitySettingComponent.Instance.ForceResetResoutionOrg();

			GameObject ARSessionPrefab = await ResComponent.Instance.LoadAssetAsync<GameObject>("ARSession");
			ARSessionPrefab = GameObject.Instantiate(ARSessionPrefab);
			ARSessionPrefab.name = ARSessionPrefab.name.Replace("(Clone)", "");
			self.ARSessoinGo = ARSessionPrefab;

			UnityEngine.Object.DontDestroyOnLoad(self.ARSessoinGo);

			MainQualitySettingComponent.Instance.ForceResetResoution();

			self.StaticMeshTran = self.ARSessoinGo.transform.Find("MirrorSceneAll_Classy/MirrorSceneRenderer/StaticMesh");
			Transform ARCameraTrans = self.ARSessoinGo.transform.Find("MirrorSceneAll_Classy/ArFoundationAdapter/ArFoundationCamera");
			self.ARCamera = ARCameraTrans.gameObject.GetComponent<Camera>();

			CanvasScaler[] canvasScalerList = self.ARSessoinGo.gameObject.GetComponentsInChildren<CanvasScaler>(true);
			foreach (var canvasScaler in canvasScalerList)
			{
				Canvas canvas = canvasScaler.gameObject.GetComponent<Canvas>();
				if (canvas != null)
				{
					UIRootManagerComponent.Instance.AddUIRootRotation(canvasScaler.transform);
				}

			}

			self.ScaleARCameraGo = self.ARSessoinGo.transform.Find("ArScaleCamera").gameObject;
			self.ScaleARCamera = self.ScaleARCameraGo.transform.GetComponent<Camera>();

			self.translucentImageSource = ARCameraTrans.gameObject.GetComponent<TranslucentImageSource>();

			self.MirrorSceneClassyUI = self.ARSessoinGo.transform.Find("MirrorSceneAll_Classy/MirrorSceneClassyUI");
			self.QrCodeImageTran = self.ARSessoinGo.transform.Find("MirrorSceneAll_Classy/MirrorSceneRenderer/CanvasRoot/MarkerCanvas/Canvas/Panel/QrCodeImage");

			// Set backend by Area.
			SetArSessionServiceEndpoint();

			SetArSessionAppAuth();

			self.RegisterCallBack();
			await TimerComponent.Instance.WaitFrameAsync();

			next();

			await TimerComponent.Instance.WaitFrameAsync();
			self.ARSessoinGo.SetActive(false);
			await TimerComponent.Instance.WaitFrameAsync();
			self.ARSessoinGo.SetActive(true);

		}

		private static void SetArSessionServiceEndpoint()
		{
			// Set endpoint by area type.
			string endpoint = ResConfig.Instance.areaType switch
			{
				AreaType.CN => "CN",
				AreaType.EN => "US",
				AreaType.GDC => "GDC",
				_ => "US"
			};

			const BindingFlags InstanceBindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
			var coreValue = GetCoreImplInternalObject();

			var setArSessionServiceEndpointMethod = coreValue.GetType().GetMethod("SetArSessionServiceEndpoint", InstanceBindFlags);
			Log.Debug($"SetArSessionServiceEndpoint {setArSessionServiceEndpointMethod}, is set to: [{endpoint}]");
			setArSessionServiceEndpointMethod.Invoke(coreValue, new object[] { endpoint });

			var SetAuthServiceEndpointMethod = coreValue.GetType().GetMethod("SetAuthServiceEndpoint", InstanceBindFlags);
			Log.Debug($"SetAuthServiceEndpoint {SetAuthServiceEndpointMethod}, is set to: [{endpoint}]");
			SetAuthServiceEndpointMethod.Invoke(coreValue, new object[] { endpoint });
		}

		private static void SetArSessionAppAuth()
		{
			const BindingFlags InstanceBindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
			var coreValue = GetCoreImplInternalObject();
			var setAppAuthOptionsMethod = coreValue.GetType().GetMethod("SetAppAuthOptions", InstanceBindFlags);
			Log.Debug($"SetAppAuthOptions {setAppAuthOptionsMethod}");
			setAppAuthOptionsMethod.Invoke(coreValue, new object[] { ResConfig.Instance.MirrorARSessionAuthAppKey, ResConfig.Instance.MirrorARSessionAuthAppSecret });
			Log.Debug($"SetAppAuthOptions succeeded.");
		}

		private static string GetXrPlatformInfo()
		{
			const BindingFlags InstanceBindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
			var coreValue = GetCoreImplInternalObject();
			var getXrPlatformInfo = coreValue.GetType().GetMethod("GetXrPlatformInfo", InstanceBindFlags);
			Log.Debug($"GetXrPlatformInfo {getXrPlatformInfo}");
			string info = (string)(getXrPlatformInfo.Invoke(coreValue, new object[] { }));
			Log.Debug($"GetXrPlatformInfo succeeded {info}");
			return info;
		}

		private static string GetObjBytesFromCurrentMesh()
		{
			const BindingFlags InstanceBindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
			var coreValue = GetCoreImplInternalObject();
			var getObjBytesFromCurrentMesh = coreValue.GetType().GetMethod("GetObjBytesFromCurrentMesh", InstanceBindFlags);
			Log.Debug($"GetObjBytesFromCurrentMesh {getObjBytesFromCurrentMesh}");
			string objContent = (string)(getObjBytesFromCurrentMesh.Invoke(coreValue, new object[] { }));
			Log.Debug($"GetObjBytesFromCurrentMesh succeeded {objContent.Length}");
			return objContent;
		}

		public static void RegisterCallBack(this ARSessionComponent self)
		{
			ClassyUI.Instance.onMenuFinish += () =>
			{
				self.IsReScanMeshing = false;

				ARSessionHelper.SetArcadeARSceneId(self.CurrentARSceneId);

				self.OnMenuFinished();
				self.OnMenuFinishedCallBack();
			};
			ClassyUI.Instance.onMenuCancel += () =>
			{
				if (self.IsReScanMeshing)
				{
					if (string.IsNullOrEmpty(self.CurrentARSceneId) == false)
					{
						if (self.ChkARSceneStatusCompleted())
						{
							self.IsReScanMeshing = false;
							self.OnMenuFinished();
							self.OnMenuFinishedCallBack();
							return;
						}
						else
						{
							self.ShowMenu();
							ProcessingMenu.Instance.UpdateProcessingText(ProcessingState.Downloading);
							ClassyUI.Instance.SwitchMenu(SystemMenuType.ProcessingMenu);
							ClassyUI.Instance.TriggerJoinScene(self.CurrentARSceneId, false);
							return;
						}
					}
					else
					{
						self.IsReScanMeshing = false;
						self.OnMenuFinished();
						self.OnMenuFinishedCallBack();
						return;
					}
				}
				self.IsReScanMeshing = false;

				self.OnMenuCancel();
				self.OnMenuCancelCallBack();
			};
			ClassyUI.Instance.onMenuCreateScene += () =>
			{
				self.OnMenuCreateScene();
				bool isNeedReCreateRoom = true;
				if (self.IsReScanMeshing)
				{
					isNeedReCreateRoom = false;
				}
				self.OnMenuCreateSceneCallBack(isNeedReCreateRoom);
			};
			ClassyUI.Instance.onMenuLoadRecentScene += () =>
			{
				self.OnMenuLoadRecentScene();
				bool isNeedReCreateRoom = true;
				if (self.IsReScanMeshing)
				{
					isNeedReCreateRoom = false;
				}
				self.OnMenuLoadRecentSceneCallBack(isNeedReCreateRoom);
			};
			ClassyUI.Instance.onMenuJoinScene += (sQRCodeInfo) =>
			{
				self.OnMenuJoinScene(sQRCodeInfo);
				self.OnMenuJoinSceneCallBack(sQRCodeInfo);
			};
			ClassyUI.Instance.onRequestQRCodeExtraData += () =>
			{
				return self.OnRequestQRCodeExtraData();
			};
			ClassyUI.Instance.onMenuExitScene += () =>
			{
				self.OnMenuExitScene();

				self.OnMenuExitSceneCallBack();
			};
			MirrorScene.Get().onMarkerDetected += (StatusOr<MarkerInfo> marker, StatusOr<Pose> markerPose, StatusOr<Pose> localizedPose) =>
			{
				self.OnMarkerDectected();
			};
			MirrorScene.Get().onSceneStreamUpdate += (StatusOr<FrameSelectionResult> frameSelectionResult, StatusOr<SceneStreamRenderable> streamRenderable) =>
			{
				self.OnSceneStreamFrameSelection(frameSelectionResult);
				self.OnSceneStreamUpdate(streamRenderable);
			};
			MirrorScene.Get().onSceneStreamFinish += (status) =>
			{
				self.OnSceneStreamFinish();
			};
			MirrorScene.Get().onSceneStandby += (status) =>
			{
				self.OnSceneStandby(status);
			};
			MirrorScene.Get().onSceneReady += (status) =>
			{
				self.OnSceneReady(status);
			};
			MirrorScene.Get().onLocalizationUpdate += (status) =>
			{
				self.OnLocalizationUpdate(status);
			};
			ClassyUI.Instance.onMenuReviewSceneOpen += () =>
			{
				self.OnMenuReviewSceneOpen();
				self.OnShowARSceneSlider();
			};
			ClassyUI.Instance.onMenuReviewSceneClose += (confirm) =>
			{
				self.OnMenuReviewSceneClose(confirm);
				if (confirm)
				{
					self.OnConfirmARSceneSlider();
				}
				else
				{
					self.OnCancelARSceneSlider();
				}
			};
			ClassyUI.Instance.onMenuBackClick += () =>
			{
				self.OnMenuBackClick();
			};
			ClassyUI.Instance.onMenuScanSceneTimeout += () =>
			{
				self.OnMenuScanSceneTimeout();
			};
			ClassyUI.Instance.onMenuScanSceneTimeoutRetry += () =>
			{
				self.OnMenuScanSceneTimeoutRetry();
			};
			ClassyUI.Instance.onMenuScanSceneTimeoutReport += () =>
			{
				self.OnMenuScanSceneTimeoutReport();
			};
		}

		public static void OnMenuReviewSceneOpen(this ARSessionComponent self)
		{
			EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
			{
				eventName = "FinishClicked",
				properties = new()
				{
					{"mesh_num", self.meshFaceCount},
				}
			});
		}

		public static void OnMenuReviewSceneClose(this ARSessionComponent self, bool confirm)
		{
			if (confirm)
			{
				EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
				{
					eventName = "ConfirmClicked",
					properties = new()
					{
						{"mesh_num", self.meshFaceCount},
					}
				});
			}
			else
			{
				EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
				{
					eventName = "ResumeClicked",
				});
			}
		}

		public static void OnMenuBackClick(this ARSessionComponent self)
		{
			EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
			{
				eventName = "BackClicked",
			});
		}

		public static void OnMenuScanSceneTimeout(this ARSessionComponent self)
		{
			EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
			{
				eventName = "OnMenuScanSceneTimeout",
			});
		}

		public static void OnMenuScanSceneTimeoutRetry(this ARSessionComponent self)
		{
			EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
			{
				eventName = "OnMenuScanSceneTimeoutRetry",
			});
		}

		public static void OnMenuScanSceneTimeoutReport(this ARSessionComponent self)
		{
			EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
			{
				eventName = "OnMenuScanSceneTimeoutReport",
			});

			UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

			UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgGameReport>().Coroutine();
		}

		public static async ETTask InitCallBack(this ARSessionComponent self, string arSceneId, ARHallType _ARHallType)
		{
#if UNITY_EDITOR
			self.OnMenuCancel();
			self.OnMenuCancelCallBack();
			return;
#endif
			await self.LoadARSession(() =>
			{
				self.InitCallBackNext(arSceneId, _ARHallType).Coroutine();
			}, true);

		}

		public static bool ChkARSceneIdReady(this ARSessionComponent self, string arSceneId)
		{
			if (string.IsNullOrEmpty(arSceneId) == false)
			{
				if (self.ChkARSceneStatusCompleted() && arSceneId == self.CurrentARSceneId)
				{
					return true;
				}
			}
			return false;
		}

		public static async ETTask InitCallBackNext(this ARSessionComponent self, string arSceneId, ARHallType _ARHallType)
		{
			if (MirrorScene.IsAvailable())
			{
				EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
				{
					eventName = "MirrorSceneInit",
					properties = new()
					{
						{"xr_platform_info", GetXrPlatformInfo() }
					}
				});

				self._ResetMainCamera(true);
				//self.SetMeshShow();

				// Skip one frame here in order to have ClassyUI Start() is called first
				// before the other calls here.
				await TimerComponent.Instance.WaitFrameAsync();

				self.ARScenePrepared = false;

				Log.Debug($"arSceneId [{arSceneId}]");
				bool isARSceneIdReady = self.ChkARSceneIdReady(arSceneId);
				if (_ARHallType == ARHallType.JoinTheRoom)
                {
	                if (string.IsNullOrEmpty(arSceneId))
	                {
		                self.ShowMenu();
		                ClassyUI.Instance.RestartToCreate();
	                }
	                else if (isARSceneIdReady)
	                {
		                self.OnMenuFinished();
		                self.OnMenuFinishedCallBack();
	                }
	                else
	                {
		                self.ShowMenu();
		                ProcessingMenu.Instance.UpdateProcessingText(ProcessingState.Downloading);
		                ClassyUI.Instance.SwitchMenu(SystemMenuType.ProcessingMenu);
		                ClassyUI.Instance.TriggerJoinScene(arSceneId, false);
		                self.CurrentARSceneId = arSceneId;
		                // 重连加载地图计时开始
		                self.EntranceType = "reconnect";
		                LogLoadSceneStartEvent(self);
	                }
                }
                else if (_ARHallType == ARHallType.KeepRoomAndRescan)
                {
	                if (isARSceneIdReady)
	                {
		                self.TriggerReScan();
	                }
	                else
	                {
		                self.ShowMenu();
		                ProcessingMenu.Instance.UpdateProcessingText(ProcessingState.Downloading);
		                ClassyUI.Instance.SwitchMenu(SystemMenuType.ProcessingMenu);
		                ClassyUI.Instance.TriggerJoinScene(arSceneId, true);
		                self.CurrentARSceneId = arSceneId;
		                // 重连加载地图计时开始
		                self.EntranceType = "reconnect";
		                LogLoadSceneStartEvent(self);
	                }
                }
                else if (_ARHallType == ARHallType.CreateRoomWithARSceneId)
                {
	                self.OnMenuLoadRecentSceneCallBack(true);
	                if (isARSceneIdReady)
	                {
		                self.OnMenuFinished();
		                self.OnMenuFinishedCallBack();
	                }
	                else
	                {
		                self.ShowMenu();
		                ProcessingMenu.Instance.UpdateProcessingText(ProcessingState.Downloading);
		                ClassyUI.Instance.SwitchMenu(SystemMenuType.ProcessingMenu);
		                ClassyUI.Instance.TriggerJoinScene(arSceneId, false);
		                self.CurrentARSceneId = arSceneId;
		                // 重连加载地图计时开始
		                self.EntranceType = "reconnect";
		                LogLoadSceneStartEvent(self);
	                }
                }
                else if (_ARHallType == ARHallType.CreateRoomWithOutARSceneId)
                {
	                self.ShowMenu();
	                ClassyUI.Instance.RestartToCreate();
                }
                else if (_ARHallType == ARHallType.ScanQRCode)
                {
	                ClassyUI.Instance.RestartToJoin();
	                self.OnMenuStartDetection();
                }

			}
			else
			{
				self.OnMenuCancel();
				self.OnMenuCancelCallBack();
			}
		}

		public static async ETTask ReStart(this ARSessionComponent self)
		{
			if (MirrorScene.IsAvailable())
			{
				self._ResetMainCamera(true);
				ClassyUI.Instance.Restart();
			}
			else
			{
				self.OnMenuCancel();
			}
		}

		public static void ShowMenu(this ARSessionComponent self)
		{
			if (MirrorScene.IsAvailable())
			{
				ClassyUI.Instance.ShowMenu();
			}
			else
			{
			}
		}

		public static void HideMenu(this ARSessionComponent self)
		{
			if (MirrorScene.IsAvailable())
			{
				ClassyUI.Instance.HideMenu();
			}
			else
			{
			}
		}

		public static void ShowQuit(this ARSessionComponent self, bool isShow)
		{
			if (MirrorScene.IsAvailable())
			{
				Transform transBackButton = self.ARSessoinGo.transform.Find("MirrorSceneAll_Classy/MirrorSceneClassyUI/ScanSceneMenu/Canvas/BackButton");
				Image image = transBackButton.gameObject.GetComponent<Image>();
				image.enabled = isShow;
			}
			else
			{
			}
		}

		public static string GetARSceneId(this ARSessionComponent self)
		{
			if (!String.IsNullOrEmpty(self.CurrentARSceneId))
			{
				return self.CurrentARSceneId;

			}
			if (MirrorScene.IsAvailable())
			{
				StatusOr<SceneInfo> sceneInfo = MirrorScene.Get().GetSceneInfo();
				if (sceneInfo.HasValue)
				{
					return sceneInfo.Value.sceneId;
				}
			}
			return "";
		}

		private static object GetCoreImplInternalObject()
		{
			//Log.Debug($"====zpb xxxxxxxxxxxxxx 0==========");
			IMirrorScene iMirrorScene = MirrorScene.Get();
			const BindingFlags InstanceBindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
			//Log.Debug($"==== 000 = iMirrorScene={iMirrorScene}");
			var coreField = iMirrorScene.GetType().GetField("_core", InstanceBindFlags);
			//Log.Debug($"==== 111 = coreField={coreField}");
			var coreValue = coreField.GetValue(iMirrorScene);
			//Log.Debug($"coreValue {coreValue}");
			return coreValue;
		}

		public static string GetARMeshDownLoadUrl(this ARSessionComponent self)
		{
			const BindingFlags InstanceBindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
			var coreValue = GetCoreImplInternalObject();
			var getSceneMeshUrlMethod = coreValue.GetType().GetMethod("GetSceneMeshUrl", InstanceBindFlags);
			//Log.Debug($"getSceneMeshUrlMethod {getSceneMeshUrlMethod}");
			var meshUrlValue = getSceneMeshUrlMethod.Invoke(coreValue, null);

			//Log.Debug($"meshUrlValue {meshUrlValue}");
			return meshUrlValue.ToString();
		}

		public static byte[] GetARMeshClientObj(this ARSessionComponent self)
		{
			// Retrieve obj bytes content and compress.
			string objContent = GetObjBytesFromCurrentMesh();
			byte[] zipped = ZipHelper.Compress(objContent.ToByteArray());
			//Log.Debug($"Obj content size = {objContent.Length}, zipped size = {zipped.Length}");
			return zipped;
		}

		public static void ResetQrCodeImageSize(this ARSessionComponent self)
		{
			self.QrCodeImageTran.localScale = Vector3.one / MainQualitySettingComponent.Instance.GetHeightScale();
		}

		public static void TriggerShowQrCode(this ARSessionComponent self)
		{
			ClassyUI.Instance.TriggerShowQrCode();
			//self.ResetQrCodeImageSize();
		}

		public static void TriggerReScan(this ARSessionComponent self)
		{
			self.IsReScanMeshing = true;
			ClassyUI.Instance.TriggerReviewScene();
		}

		public static bool ChkARSceneStatusCompleted(this ARSessionComponent self)
		{
			if (ClassyUI.Instance == null)
			{
				return false;
			}
			return MirrorScene.Get().GetSceneStatus() == SceneStatus.Completed;
		}

		public static void ResetMainCamera(this ARSessionComponent self, bool isARCamera, bool isQuickSet)
		{
			TimerComponent.Instance?.Remove(ref self.Timer);
			if (isQuickSet)
			{
				self._ResetMainCamera(isARCamera);
			}
			else
			{
				self.targetIsARCamera = isARCamera;
				self.Timer = TimerComponent.Instance.NewRepeatedTimer(500, TimerInvokeType.ARSessionComponentTimer, self);
			}
		}

		public static void _ResetMainCamera(this ARSessionComponent self, bool isARCamera)
		{
			if (self.ARSessoinGo == null)
			{
				return;
			}

			self.SetScaleARCamera(1, false);
			if (isARCamera)
			{
				Log.Debug("Turn on AR Camera");
				if (self.ARSessoinGo != null)
				{
					self.ARSessoinGo.SetActive(true);
					//self.ResetQrCodeImageSize();
				}

				AudioListener audioListenerMain = GlobalComponent.Instance.MainCamera.gameObject.GetComponent<AudioListener>();
				audioListenerMain.enabled = false;
				GlobalComponent.Instance.MainCamera.enabled = false;
				if (self.ARCamera != null)
				{
					self.ARCamera.enabled = true;
					UniversalAdditionalCameraData universalAdditionalCameraDataMain = GlobalComponent.Instance.MainCamera.gameObject.GetComponent<UniversalAdditionalCameraData>();
					UniversalAdditionalCameraData universalAdditionalCameraDataAR = self.ARCamera.gameObject.GetComponent<UniversalAdditionalCameraData>();
					universalAdditionalCameraDataAR.cameraStack.Clear();
					universalAdditionalCameraDataAR.cameraStack.AddRange(universalAdditionalCameraDataMain.cameraStack);
				}
				if (self.CurARCamera != null)
				{
					self.CurARCamera.enabled = true;
					AudioListener audioListenerAR = self.CurARCamera.gameObject.GetComponent<AudioListener>();
					audioListenerAR.enabled = true;
				}
			}
			else
			{
				Log.Debug("Turn off AR Camera");
				// 如果AR相机请求关闭，此时如还未退出AR Session，先退出。
				// if (MirrorScene.IsAvailable() && MirrorScene.Get().GetOperationState() != SceneOperationState.Idle)
				// {
				// 	MirrorScene.Get().ExitScene();
				// }
				if (self.ARSessoinGo != null)
				{
					self.ARSessoinGo.SetActive(false);
				}

				AudioListener audioListenerMain = GlobalComponent.Instance.MainCamera.gameObject.GetComponent<AudioListener>();
				audioListenerMain.enabled = true;
				GlobalComponent.Instance.MainCamera.enabled = true;
				if (self.ARCamera != null)
				{
					self.ARCamera.enabled = false;
				}
				if (self.CurARCamera != null)
				{
					self.CurARCamera.enabled = false;
					AudioListener audioListenerAR = self.CurARCamera.gameObject.GetComponent<AudioListener>();
					audioListenerAR.enabled = false;
				}
			}

		}

		public static Camera SetScaleARCamera(this ARSessionComponent self, float arScale, bool isNeedSetAudioListener)
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
					UpdateMeshMaterialARScale(self.StaticMeshTran, 1);
				}

				if (isNeedSetAudioListener)
				{
					audioListenerAR = self.CurARCamera.gameObject.GetComponent<AudioListener>();
					audioListenerAR.enabled = true;
				}

				return self.CurARCamera;
			}
			if (arScale == 1)
			{
				self.ScaleARCameraGo.GetComponent<FollowARCamera>().ResetARCamera();
				self.CurARCamera = self.ARCamera;
				self.StaticMeshTran.localScale = Vector3.one;
			}
			else
			{
				self.ScaleARCameraGo.GetComponent<FollowARCamera>().SetARCamera(self.ARCamera, arScale);
				self.CurARCamera = self.ScaleARCamera;
				self.StaticMeshTran.localScale = new Vector3(arScale, arScale, arScale);
				self.arScale = arScale;
			}

			if (isNeedSetAudioListener)
			{
				audioListenerAR = self.CurARCamera.gameObject.GetComponent<AudioListener>();
				audioListenerAR.enabled = true;
			}

			UpdateMeshMaterialARScale(self.StaticMeshTran, arScale);
			return self.CurARCamera;
		}

		private static void UpdateMeshMaterialARScale(Transform staticMeshTrans, float arScale)
		{
			MirrorVerse.UI.Renderers.StaticMeshRenderer staticMeshRenderer = staticMeshTrans.gameObject.GetComponent<MirrorVerse.UI.Renderers.StaticMeshRenderer>();
			MirrorVerse.Options.StaticMeshRendererOptions staticMeshRendererOptions = staticMeshRenderer.options;
			// Update material's scale that require depth computation.
			foreach (Material material in staticMeshRendererOptions.defaultMaterials)
			{
				// Note: expect materials assigned to this options are using the DepthAlpha shader:
				// MirrorVerse.UI/Renderers/Shaders/Terrain/DepthAlpha.cginc
				material.SetFloat("_CameraScale", arScale);
			}
		}

		public static void OnMenuFinished(this ARSessionComponent self)
		{
			// 用户成功得到AR地图和mesh，可以进入游戏
			GameObject go = null;
			StatusOr<GameObject> sceneMesh = MirrorScene.Get().GetSceneMeshWrapperObject();
			if (sceneMesh.HasValue && sceneMesh.Value != null)
			{
				go = sceneMesh.Value;
			}
			Log.Debug($"ARSessionComponent OnMenuFinished go[{go}]");
			foreach (var subTran in go.GetComponentsInChildren<Transform>(true))
			{
				Log.Debug($"subTran[{subTran}] [{LayerMask.NameToLayer("Map")}]");
				subTran.gameObject.layer = LayerMask.NameToLayer("Map");
			}
		}

		public static void OnMenuCancel(this ARSessionComponent self)
		{
			// 用户最终退出AR界面，回到外面主菜单

			UIAudioManagerHelper.PlayHighestMusic(self.DomainScene(), MusicType.None);

			EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
			{
				eventName = "AbandonClicked",
			});
			Log.Debug($"ARSessionComponent OnMenuCancel");
			self._ResetMainCamera(false);
		}

		public static void OnMenuExitScene(this ARSessionComponent self)
		{
			UIAudioManagerHelper.PlayHighestMusic(self.DomainScene(), MusicType.None);

			Log.Debug($"ARSessionComponent OnMenuExitScene");
			// 退出当前AR场景时从Kappa来的回调。
			if (self.ARScenePrepared)
			{
				// 如果AR场景已经成功创建后再退出，或者已经正常完成一局游戏退出房间，则不再记录失败事件。
			}
			else
			{
				// 如果扫图，扫码，下载，等过程中失败，或者用户选择取消，则记录失败事件。
				switch (self.EntranceType)
				{
					case "scan":
						// 扫图结束事件 - 失败/取消
						EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
						{
							eventName = "ScanEnded",
							properties = new()
								{
									{"success", false},
									{"session_id", self.GetARSceneId()},
								}
						});
						break;
					case "load":
					case "reconnect":
						// 重用或重连下载结束事件 - 失败/取消
						LogLoadSceneEndEvent(self, false);
						break;
					case "join":
						// 扫码结束事件
						EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
						{
							eventName = "JoinEnded",
						});
						// 扫码下载结束事件 - 失败/取消
						LogLoadSceneEndEvent(self, false);
						break;
					default:
						// 如果没有entrance type，说明只是一个简单的退出AR，没有用户操作，不打埋点
						break;
				}
			}
		}

		public static void OnMenuCreateScene(this ARSessionComponent self)
		{
			Log.Debug($"ARSessionComponent OnMenuCreateScene");

			UIAudioManagerHelper.PlayHighestMusic(self.DomainScene(), MusicType.ARScan);

			self.EntranceType = "scan";
			self.meshFaceCount = 0;
			self.frameCaptured = 0;
			self.lastScanWarning = 0;
		}

		public static void OnMenuLoadRecentScene(this ARSessionComponent self)
		{
			Log.Debug($"ARSessionComponent OnMenuLoadRecentScene");

			self.EntranceType = "load";

			// 加载读取上次地图计时开始
			LogLoadSceneStartEvent(self);
		}

		public static void OnMenuStartDetection(this ARSessionComponent self)
		{
			Log.Debug($"ARSessionComponent OnMenuStartDetection");

			self.EntranceType = "join";

			EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
			{
				// 点击扫码
				eventName = "JoinStarted",
			});
			EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLoggingStart()
			{
				// 扫到二维码计时开始
				eventName = "QRScanned",
			});
			EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLoggingStart()
			{
				// 扫码成功下载结束计时开始
				eventName = "JoinEnded",
			});
		}

		public static void OnMarkerDectected(this ARSessionComponent self)
		{
			Log.Debug($"ARSessionComponent OnMarkerDectected");
			EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
			{
				// 扫到二维码计时结束
				eventName = "QRScanned",
			});
		}

		public static void OnMenuJoinScene(this ARSessionComponent self, string sQRCodeInfo)
		{
			Log.Debug($"ARSessionComponent OnMenuJoinScene");

			// 加载扫码的到的地图计时开始
			LogLoadSceneStartEvent(self);
		}

		public static void OnSceneStandby(this ARSessionComponent self, StatusOr<SceneInfo> sceneInfo)
		{
			// Store the scene ID once standby. The scene preparation may fail later. So it's useful for logging.
			if (sceneInfo.HasValue)
			{
				self.CurrentARSceneId = sceneInfo.Value.sceneId;
			}

			if (self.EntranceType == "scan")
			{
				// Scen just created and standby for scan start.
				EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
				{
					// 扫图开始事件
					eventName = "ScanStarted",
					properties = new()
								{
									{"session_id", self.GetARSceneId()},
								}
				});
				EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLoggingStart()
				{
					// 扫图结束事件开始计时
					eventName = "ScanEnded",
				});
			}
		}

		public static void OnSceneReady(this ARSessionComponent self, StatusOr<SceneInfo> sceneInfo)
		{
			// Scene preparation done. Mesh processed or downloaded.
			if (sceneInfo.HasValue)
			{
				self.ARScenePrepared = true;
				switch (self.EntranceType)
				{
					case "scan":
						EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
						{
							eventName = "ProcessingEnded",
						});
						// 扫图结束事件 - 成功
						EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
						{
							eventName = "ScanEnded",
							properties = new()
								{
									{"success", true},
									{"session_id", self.GetARSceneId()},
								}
						});
						break;
					case "load":
					case "reconnect":
						// 重用或重连下载地图结束 - 成功
						LogLoadSceneEndEvent(self, true);
						break;
					case "join":
						// 扫码结束事件
						EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
						{
							eventName = "JoinEnded",
						});
						// 扫码下载地图结束 - 成功
						LogLoadSceneEndEvent(self, true);
						break;
					default:
						// 如果没有entrance type，不符合预期。按理不会发生
						Log.Warning($"Unhandled entrance type for menu finishing: {self.EntranceType}.");
						break;
				}
			}
			else
			{
				// failed
				self.ARScenePrepared = false;
			}
		}


		private static void OnLocalizationUpdate(this ARSessionComponent self, StatusOr<Pose> status)
		{
			DebugShowComponent debugShowComponent = DebugShowComponent.Instance;
			if (debugShowComponent != null)
			{
				if (status.HasValue)
				{
					debugShowComponent.SetDebugPose(status.Value);
				}
				else
				{
					debugShowComponent.SetDebugPose(null);
				}
			}

			if (MirrorScene.Get() != null && self.lastSendCameraPosTime < TimeHelper.ClientNow())
			{
				self.lastSendCameraPosTime = TimeHelper.ClientNow() + (long)(GlobalSettingCfgCategory.Instance.EventLogCameraPosHz * 1000);

				string operationState = MirrorScene.Get().GetOperationState().ToString();
				string camera_pose;
				if (status.HasValue)
				{
					camera_pose = $"{status.Value.position} {status.Value.rotation.eulerAngles}";
				}
				else
				{
					camera_pose = $"{Vector3.zero} {Vector3.zero}";
				}
				EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
				{
					eventName = "CameraPose",
					properties = new()
					{
						{"camera_pose", camera_pose},
						{"status", operationState}
					}
				});
			}

		}

		private static void LogLoadSceneStartEvent(this ARSessionComponent self)
		{
			// 加载地图计时开始。 trigger method: load/join/reconnect
			EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
			{
				// 下载开始
				eventName = "LoadStarted",
				properties = new()
				{
					{"session_id", self.GetARSceneId()},
					{"trigger_method", self.EntranceType }
				}
			});
			EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLoggingStart()
			{
				// 下载结束计时开始
				eventName = "LoadEnded",
			});
		}

		private static void LogLoadSceneEndEvent(this ARSessionComponent self, bool success)
		{
			// 下载结束事件
			EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
			{
				eventName = "LoadEnded",
				properties = new()
				{
					{"success", success},
					{"session_id", self.GetARSceneId()},
					{"trigger_method", self.EntranceType }
				}
			});
		}

		public static void ShowARMesh(this ARSessionComponent self, ARSessionComponent.ArMeshVisibility show)
		{
			//Log.Debug($"ARSessionComponent SetMeshShow");
			Transform staticMeshTrans = self.StaticMeshTran;
			MirrorVerse.UI.Renderers.StaticMeshRenderer staticMeshRenderer = staticMeshTrans.gameObject.GetComponent<MirrorVerse.UI.Renderers.StaticMeshRenderer>();
			MirrorVerse.Options.StaticMeshRendererOptions staticMeshRendererOptions = staticMeshRenderer.options;
			staticMeshRendererOptions.visible = true;
			staticMeshRendererOptions.collidable = true;
			staticMeshRendererOptions.withOcclusion = false;
			staticMeshRendererOptions.castsShadow = false;
			staticMeshRendererOptions.receivesShadow = false;
			bool enableMeshRenderer = true;
			switch (show)
			{
				case ARSessionComponent.ArMeshVisibility.ColliderOnly:
					enableMeshRenderer = false;
					break;
				case ARSessionComponent.ArMeshVisibility.Visible:
					break;
				case ARSessionComponent.ArMeshVisibility.TranslucentOcclusion:
				default:
					staticMeshRendererOptions.withOcclusion = true;
					staticMeshRendererOptions.receivesShadow = true;
					enableMeshRenderer = true;
					break;
			}
			MeshRenderer[] meshRenderers = staticMeshRenderer.GetComponentsInChildren<MeshRenderer>();
			foreach (MeshRenderer meshRenderer in meshRenderers)
			{
				meshRenderer.enabled = enableMeshRenderer;
			}
		}

		public static bool ChkARCameraEnable(this ARSessionComponent self)
		{
			if (self.ARCamera != null && self.ARCamera.enabled)
			{
				return true;
			}
			return false;
		}

		public static void OnSceneStreamFrameSelection(this ARSessionComponent self, StatusOr<FrameSelectionResult> frameSelectionResult)
		{
			if (frameSelectionResult.HasValue)
			{
				if (frameSelectionResult.Value.selectedFrameCount > 0)
				{
					if (self.frameCaptured == 0)
					{
						// first time captured.
						EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
						{
							eventName = "ScanCaptured",
						});
					}
					self.frameCaptured = frameSelectionResult.Value.selectedFrameCount;
				}

				if (frameSelectionResult.Value.warningType != FrameSelectionResult.WarningType.None &&
					frameSelectionResult.Value.warningType != FrameSelectionResult.WarningType.LowSelectionRate)
				// Do not log LowSelectionRate, because it could be very frequent.
				{
					if ((int)frameSelectionResult.Value.warningType == self.lastScanWarning)
					{
						// Same warning since last frame. Skip.
					}
					else
					{
						// Otherwise log the warning type.
						EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
						{
							eventName = "ScanWarning",
							properties = new()
							{
								{"warning_type", frameSelectionResult.Value.warningType},
							}
						});
					}
				}
				self.lastScanWarning = (int)frameSelectionResult.Value.warningType;
			}
		}

		public static void OnSceneStreamUpdate(this ARSessionComponent self, StatusOr<SceneStreamRenderable> streamRenderable)
		{
			if (streamRenderable.HasValue)
			{
				if (streamRenderable.Value.immediateMesh != null)
				{
					// Debug.Log($"Immediate meshes face count {meshFaceCount}");
					if (streamRenderable.Value.immediateMesh.triangles.Length / 3 != self.meshFaceCount)
					{
						self.meshFaceCount = streamRenderable.Value.immediateMesh.triangles.Length / 3;
						EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
						{
							eventName = "MeshUpdated",
							properties = new()
							{
								{"mesh_num", self.meshFaceCount},
							}
						});
					}

				}
			}
		}

		public static void OnSceneStreamFinish(this ARSessionComponent self)
		{
			EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
			{
				eventName = "ProcessingStarted",
			});
			EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLoggingStart()
			{
				eventName = "ProcessingEnded",
			});

			UIAudioManagerHelper.PlayHighestMusic(self.DomainScene(), MusicType.None);
		}

		public static void OnShowARSceneSlider(this ARSessionComponent self)
		{
			self._ShowARSceneSlider().Coroutine();
		}

		public static async ETTask _ShowARSceneSlider(this ARSessionComponent self)
		{
			bool bRet = await self.ChkCanShowARSceneSlider();
			if (bRet)
			{
				UIComponent _UIComponent = UIManagerHelper.GetUIComponent(self.DomainScene());
				if (ET.SceneHelper.ChkIsGameModeArcade())
				{
					_UIComponent.ShowWindow<DlgARSceneSlider>();
				}
				else
				{
					_UIComponent.ShowWindow<DlgARSceneSliderSimple>();
				}
			}
		}

		public static async ETTask<bool> ChkCanShowARSceneSlider(this ARSessionComponent self)
		{
			PlayerStatusComponent playerStatusComponent = ET.Client.PlayerStatusHelper.GetMyPlayerStatusComponent(self.DomainScene());
			if (playerStatusComponent.PlayerStatus != PlayerStatus.Room)
			{
				return false;
			}
			long myPlayerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());
			long roomId = playerStatusComponent.RoomId;
			(bool roomExist, RoomComponent roomComponent) = await RoomHelper.GetRoomInfoAsync(self.DomainScene(), roomId);
			if (roomExist == false)
			{
				return false;
			}
			if (roomComponent.ChkIsOwner(myPlayerId) == false)
			{
				return false;
			}

			return true;
		}

		public static void OnConfirmARSceneSlider(this ARSessionComponent self)
		{
			UIComponent _UIComponent = UIManagerHelper.GetUIComponent(self.DomainScene());
			DlgARSceneSlider _DlgARSceneSlider = _UIComponent.GetDlgLogic<DlgARSceneSlider>(true);
			if (_DlgARSceneSlider != null)
			{
				self.arScale = _DlgARSceneSlider.GetSceneScale();

				_UIComponent.HideWindow<DlgARSceneSlider>();
			}

			DlgARSceneSliderSimple _DlgARSceneSliderSimple = _UIComponent.GetDlgLogic<DlgARSceneSliderSimple>(true);
			if (_DlgARSceneSliderSimple != null)
			{
				self.arScale = _DlgARSceneSliderSimple.GetSceneScale();

				_UIComponent.HideWindow<DlgARSceneSliderSimple>();
			}
		}

		public static void OnCancelARSceneSlider(this ARSessionComponent self)
		{
			UIComponent _UIComponent = UIManagerHelper.GetUIComponent(self.DomainScene());
			DlgARSceneSlider _DlgARSceneSlider = _UIComponent.GetDlgLogic<DlgARSceneSlider>(true);
			if (_DlgARSceneSlider != null)
			{
				_UIComponent.HideWindow<DlgARSceneSlider>();
			}

			DlgARSceneSliderSimple _DlgARSceneSliderSimple = _UIComponent.GetDlgLogic<DlgARSceneSliderSimple>(true);
			if (_DlgARSceneSliderSimple != null)
			{
				_UIComponent.HideWindow<DlgARSceneSliderSimple>();
			}
		}

	}
}
