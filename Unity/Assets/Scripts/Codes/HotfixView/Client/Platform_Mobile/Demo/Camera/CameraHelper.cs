using Unity.Mathematics;
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
				if (currentScene == null)
				{
					return null;
				}
				if (currentScene.GetComponent<CameraComponent>() == null)
				{
					return null;
				}

				return currentScene.GetComponent<CameraComponent>().MainCamera;
			}
			if (gamePlayComponent.IsAR())
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

		public static (float3, float3, float3) GetCameraHit(Scene scene)
		{
			Camera camera = CameraHelper.GetMainCamera(scene);

			return GetCameraHit(camera);
		}

		public static (float3, float3, float3) GetCameraHit(Camera camera)
		{
			float3 cameraPos = camera.transform.position;
			float3 cameraDirect = camera.transform.forward;
			float3 cameraHitPos = float3.zero;
			RaycastHit hitInfo;
			LayerMask _groundLayerMask = LayerMask.GetMask("Map");
			if (Physics.Raycast(cameraPos, camera.transform.forward, out hitInfo, 1000, _groundLayerMask))
			{
				cameraHitPos = hitInfo.point;
			}

			return (cameraPos, cameraDirect, cameraHitPos);
		}

	}
}
