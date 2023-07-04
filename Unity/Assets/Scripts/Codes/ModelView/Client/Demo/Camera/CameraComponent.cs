using UnityEngine;

namespace ET.Client
{
	public class CameraComponent : Entity, IAwake, ILateUpdate
	{
		// 战斗摄像机
		private Camera mainCamera;

		private EntityRef<Unit> unit;

		public Unit Unit
		{
			get
			{
				return unit;
			}
			set
			{
				this.unit = value;
			}
		}

		public Camera MainCamera
		{
			set
			{
				this.mainCamera = value;
			}
			get
			{
				return this.mainCamera;
			}
		}
	}
}
