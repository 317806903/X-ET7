using UnityEngine;

namespace ET.Client
{
	[FriendOf(typeof(ARSessionComponent))]
	public static class ARSessionHelper
	{
		public static ARSessionComponent GetARSession(Scene scene)
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

			ARSessionComponent arSessionComponent = clientScene.GetComponent<ARSessionComponent>();
			if (arSessionComponent == null)
			{
				arSessionComponent = clientScene.AddComponent<ARSessionComponent>();
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

		public static string GetARSceneId(Scene scene)
		{
			ARSessionComponent arSessionComponent = GetARSession(scene);
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
	}
}
