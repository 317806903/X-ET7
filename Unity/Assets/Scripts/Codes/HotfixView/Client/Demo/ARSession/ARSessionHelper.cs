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

			ARSessionComponent arSessionComponent = clientScene.GetComponent<ARSessionComponent>();
			if (arSessionComponent == null)
			{
				if (create)
				{
					arSessionComponent = clientScene.AddComponent<ARSessionComponent>();
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
			arSessionComponent.SetScaleARCamera(fScaleAR);
		}

		public static void ResetMainCamera(Scene scene, bool isARCamera)
		{
			ARSessionComponent arSessionComponent = GetARSession(scene, false);
			if (arSessionComponent == null)
			{
				return;
			}
			arSessionComponent.ResetMainCamera(isARCamera);
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

		public static string GetARMeshDownLoadUrl(Scene scene)
		{
			ARSessionComponent arSessionComponent = GetARSession(scene);
			string _ARMeshDownLoadUrl = arSessionComponent.GetARMeshDownLoadUrl();
			return _ARMeshDownLoadUrl;
		}

		public static void TriggerShowQrCode(Scene scene)
		{
			ARSessionComponent arSessionComponent = GetARSession(scene);
			arSessionComponent.TriggerShowQrCode();
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
	}
}
