using UnityEngine;

namespace ET.Client
{
	[FriendOf(typeof(CameraComponent))]
	public static class CameraComponentSystem
	{
		[ObjectSystem]
		public class CameraComponentAwakeSystem : AwakeSystem<CameraComponent>
		{
			protected override void Awake(CameraComponent self)
			{
				self.Awake();
			}
		}

		[ObjectSystem]
		public class CameraComponentLateUpdateSystem : LateUpdateSystem<CameraComponent>
		{
			protected override void LateUpdate(CameraComponent self)
			{
				//self.LateUpdate();
			}
		}

		private static void Awake(this CameraComponent self)
		{
		}

		public static void SetMainCamera(this CameraComponent self, Camera camera)
		{
			self.MainCamera = camera;
		}

		public static async ETTask SetFollowPlayer(this CameraComponent self)
		{
			while (true)
			{
				Unit observerUnit = UnitHelper.GetMyObserverUnit(self.DomainScene());
				if (observerUnit != null)
				{
					self.Unit = observerUnit;
					break;
				}
				else
				{
					await TimerComponent.Instance.WaitAsync(200);
				}
			}
			bool bRet = await ET.Client.UnitViewHelper.ChkGameObjectShowReady(self, self.Unit);
			if (bRet == false)
			{
				return;
			}

			GameObjectShowComponent gameObjectShowComponent = self.Unit.GetComponent<GameObjectShowComponent>();
			WorldCameraController worldCameraController = self.MainCamera.gameObject.GetComponent<WorldCameraController>();
			worldCameraController.ForceSetPosition(gameObjectShowComponent.GetGo().transform, 1f, 30, new Vector3(30f, 116, 0));
		}

		private static void LateUpdate(this CameraComponent self)
		{
			// 摄像机每帧更新位置
			self.UpdatePosition();
		}

		private static void UpdatePosition(this CameraComponent self)
		{
			if (self.Unit == null)
			{
				return;
			}
			Vector3 cameraPos = self.MainCamera.transform.position;
			Vector3 targetPosition = new Vector3(self.Unit.Position.x, cameraPos.y, self.Unit.Position.z - 1);
			self.MainCamera.transform.position = Vector3.Lerp(cameraPos, targetPosition, 0.8f);
		}
	}
}
