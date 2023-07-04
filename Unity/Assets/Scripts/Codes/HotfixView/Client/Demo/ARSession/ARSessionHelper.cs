using UnityEngine;

namespace ET.Client
{
	[FriendOf(typeof(ARSessionComponent))]
	public static class ARSessionHelper
	{
		public static Camera GetMainCamera(Scene scene)
		{
			Scene currentScene = null;
			if (scene == scene.ClientScene())
			{
				currentScene = scene.GetComponent<CurrentScenesComponent>().Scene;
			}
			else
			{
				currentScene = scene;
			}

			if (currentScene.GetComponent<CameraComponent>() == null)
			{
				return null;
			}

			return currentScene.GetComponent<CameraComponent>().MainCamera;
		}
	}
}
