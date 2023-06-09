using UnityEngine;

namespace ET.Client
{
	[FriendOf(typeof(CameraComponent))]
	public static class CameraHelper
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

			return currentScene.GetComponent<CameraComponent>().MainCamera;
		}
	}
}
