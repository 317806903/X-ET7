
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class EPage_BattleDeckTowerViewComponentAwakeSystem : AwakeSystem<EPage_BattleDeckTowerViewComponent,Transform> 
	{
		protected override void Awake(EPage_BattleDeckTowerViewComponent self,Transform transform)
		{
			self.uiTransform = transform;
		}
	}

	[ObjectSystem]
	public class EPage_BattleDeckTowerViewComponentDestroySystem : DestroySystem<EPage_BattleDeckTowerViewComponent> 
	{
		protected override void Destroy(EPage_BattleDeckTowerViewComponent self)
		{
			self.DestroyWidget();
		}
	}

	[ObjectSystem]
	public class EPage_BattleDeckTowerAwakeSystem : AwakeSystem<EPage_BattleDeckTower,Transform> 
	{
		protected override void Awake(EPage_BattleDeckTower self,Transform transform)
		{
			self.AddComponent<EPage_BattleDeckTowerViewComponent,Transform>(transform);
			self.RegisterUIEvent();
		}
	}


	[ObjectSystem]
	public class EPage_BattleDeckTowerDestroySystem : DestroySystem<EPage_BattleDeckTower> 
	{
		protected override void Destroy(EPage_BattleDeckTower self)
		{
		}
	}
}
