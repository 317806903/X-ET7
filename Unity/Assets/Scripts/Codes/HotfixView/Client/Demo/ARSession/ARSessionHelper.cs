using UnityEngine;

namespace ET.Client
{
	[FriendOf(typeof(ARSessionComponent))]
	public static class ARSessionHelper
	{
		public static bool ChkARCameraEnable(Scene scene)
		{
			ARSessionComponent arSessionComponent = GetARSession(scene, false);
			if (arSessionComponent == null)
			{
				return false;
			}
			return arSessionComponent.ChkARCameraEnable();
		}

		public static ARSessionComponent GetARSession(Scene scene, bool create = true)
		{
			Scene currentScene = null;
			Scene clientScene = null;
			if (scene == scene.ClientScene())
			{
				currentScene = scene.GetComponent<CurrentScenesComponent>().Scene;
				clientScene = scene;
			}
			else
			{
				currentScene = scene;
				clientScene = scene.ClientScene();
			}
			if (clientScene == null)
			{
				return null;
			}

			CurrentScenesComponent currentScenesComponent = clientScene.GetComponent<CurrentScenesComponent>();
			ARSessionComponent arSessionComponent = currentScenesComponent.GetComponent<ARSessionComponent>();
			if (arSessionComponent == null)
			{
				if (create)
				{
					arSessionComponent = currentScenesComponent.AddComponent<ARSessionComponent>();
				}
			}
			return arSessionComponent;
		}

		public static Camera GetMainCamera(Scene scene)
		{
			ARSessionComponent arSessionComponent = GetARSession(scene);
			return arSessionComponent.CurARCamera;
		}

		public static bool ChkARSceneStatusCompleted(Scene scene)
		{
			ARSessionComponent arSessionComponent = GetARSession(scene);
			return arSessionComponent.ChkARSceneStatusCompleted();
		}

		public static void SetScaleARCamera(Scene scene, float fScaleAR)
		{
			ARSessionComponent arSessionComponent = GetARSession(scene);
			arSessionComponent.SetScaleARCamera(fScaleAR, true);
		}

		public static float GetScaleAR(Scene scene)
		{
			ARSessionComponent arSessionComponent = GetARSession(scene);
			float arScale = arSessionComponent.arScale;
			return arScale;
		}

		public static void ResetMainCamera(Scene scene, bool isARCamera, bool isQuickSet = true)
		{
			ARSessionComponent arSessionComponent = GetARSession(scene, false);
			if (arSessionComponent == null)
			{
				return;
			}
			arSessionComponent.ResetMainCamera(isARCamera, isQuickSet);
		}

		public static string GetARSceneId(Scene scene)
		{
			ARSessionComponent arSessionComponent = GetARSession(scene);
			if (arSessionComponent == null)
			{
				return "";
			}
			string arSceneId = arSessionComponent.GetARSceneId();
			return arSceneId;
		}

		public static string GetArcadeARSceneId()
		{
			string key = "ArcadeARSceneId";
			if (PlayerPrefs.HasKey(key) == false)
			{
				return "";
			}
			string arcadeARSceneId = PlayerPrefs.GetString(key);
			return arcadeARSceneId;
		}

		public static void SetArcadeARSceneId(string arcadeARSceneId)
		{
			string key = "ArcadeARSceneId";
			PlayerPrefs.SetString(key, arcadeARSceneId);
		}

		public static string GetARMeshDownLoadUrl(Scene scene)
		{
			ARSessionComponent arSessionComponent = GetARSession(scene);
			string _ARMeshDownLoadUrl = arSessionComponent.GetARMeshDownLoadUrl();
			return _ARMeshDownLoadUrl;
		}

		public static byte[] GetARMeshClientObj(Scene scene)
		{
			ARSessionComponent arSessionComponent = GetARSession(scene);
			byte[] _ARMeshClientObj = arSessionComponent.GetARMeshClientObj();
			return _ARMeshClientObj;
		}

		public static string GetAREntranceType(Scene scene)
		{
			ARSessionComponent arSessionComponent = GetARSession(scene);
			if (arSessionComponent == null)
			{
				return "";
			}
			string entranceType = arSessionComponent.EntranceType;
			return entranceType;
		}

		public static void ResetAREntranceType(Scene scene)
		{
			ARSessionComponent arSessionComponent = GetARSession(scene);
			if (arSessionComponent == null)
			{
				return;
			}
			arSessionComponent.EntranceType = "";
		}

		public static void TriggerShowQrCode(Scene scene)
		{
			ARSessionComponent arSessionComponent = GetARSession(scene);
			arSessionComponent.TriggerShowQrCode();
		}

		public static void TriggerReScan(Scene scene)
		{
			ARSessionComponent arSessionComponent = GetARSession(scene);
			arSessionComponent.TriggerReScan();
		}

		public static void ShowARMesh(Scene scene, bool show)
		{
			if (ChkARCameraEnable(scene) == false)
			{
				return;
			}
			ARSessionComponent arSessionComponent = GetARSession(scene);
			arSessionComponent.ShowARMesh(show);
		}

		public static BlurBackground.TranslucentImageSource GetTranslucentImageSource(Scene scene)
		{
			ARSessionComponent arSessionComponent = GetARSession(scene);
			if (arSessionComponent.ChkARCameraEnable())
			{
				return arSessionComponent.translucentImageSource;
			}
			return null;
		}

		public static bool ChkARMirrorSceneUIShow(Scene scene)
		{
			ARSessionComponent arSessionComponent = GetARSession(scene);
			if (arSessionComponent.ChkARCameraEnable())
			{
				return arSessionComponent.ChkARMirrorSceneUIShow();
			}
			return false;
		}

		public static async ETTask SetARRoomInfoAsync(Scene scene)
		{
			float arMapScale = ARSessionHelper.GetScaleAR(scene);
			if (arMapScale == 0)
			{
				arMapScale = 30;
			}

			bool isGetMeshFromClient = ChannelSettingComponent.Instance.ChkIsGetMeshFromClient();
			ARMeshType arMeshType = ARMeshType.FromRemoteURL;
			if (isGetMeshFromClient)
			{
				arMeshType = ARMeshType.FromClientObj;
			}
			else
			{
				arMeshType = ARMeshType.FromRemoteURL;
			}

			string arSceneId = ARSessionHelper.GetARSceneId(scene);
			string arMeshDownLoadUrl = "";
			byte[] arMeshBytes = null;

			arSceneId = ARSessionHelper.GetARSceneId(scene);
			if (arMeshType == ARMeshType.FromRemoteURL)
			{
				arMeshDownLoadUrl = ARSessionHelper.GetARMeshDownLoadUrl(scene);
			}
			else if (arMeshType == ARMeshType.FromClientObj)
			{
				arMeshBytes = ARSessionHelper.GetARMeshClientObj(scene);
			}

			bool result = await RoomHelper.SetARRoomInfoAsync(scene, arMapScale, arMeshType, arSceneId, arMeshDownLoadUrl, arMeshBytes);
			if (result)
			{
			}
		}
	}
}
