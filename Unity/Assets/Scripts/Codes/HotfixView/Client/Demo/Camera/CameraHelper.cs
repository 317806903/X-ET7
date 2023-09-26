using UnityEngine;

namespace ET.Client
{
	[FriendOf(typeof(CameraComponent))]
	public static class CameraHelper
	{
		public static Camera GetMainCamera(Scene scene)
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

			GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(scene);
			if (gamePlayComponent == null)
			{
				return null;
			}
			if (gamePlayComponent.isAR)
			{
				Camera cameraAR = ET.Client.ARSessionHelper.GetMainCamera(scene);
				if (cameraAR != null)
				{
					return cameraAR;
				}

				return GlobalComponent.Instance.MainCamera;
			}

			if (currentScene.GetComponent<CameraComponent>() == null)
			{
				return null;
			}

			return currentScene.GetComponent<CameraComponent>().MainCamera;
		}
	}
}
