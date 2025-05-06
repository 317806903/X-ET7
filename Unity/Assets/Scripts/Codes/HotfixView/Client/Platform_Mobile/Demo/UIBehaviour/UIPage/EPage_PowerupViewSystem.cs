
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class EPage_PowerupViewComponentAwakeSystem : AwakeSystem<EPage_PowerupViewComponent,Transform> 
	{
		protected override void Awake(EPage_PowerupViewComponent self,Transform transform)
		{
			self.uiTransform = transform;
		}
	}

	[ObjectSystem]
	public class EPage_PowerupViewComponentDestroySystem : DestroySystem<EPage_PowerupViewComponent> 
	{
		protected override void Destroy(EPage_PowerupViewComponent self)
		{
			self.DestroyWidget();
		}
	}

	[ObjectSystem]
	public class EPage_PowerupAwakeSystem : AwakeSystem<EPage_Powerup,Transform> 
	{
		protected override void Awake(EPage_Powerup self,Transform transform)
		{
			self.AddComponent<EPage_PowerupViewComponent,Transform>(transform);
			self.RegisterUIEvent();
		}
	}


	[ObjectSystem]
	public class EPage_PowerupDestroySystem : DestroySystem<EPage_Powerup> 
	{
		protected override void Destroy(EPage_Powerup self)
		{
		}
	}
}
