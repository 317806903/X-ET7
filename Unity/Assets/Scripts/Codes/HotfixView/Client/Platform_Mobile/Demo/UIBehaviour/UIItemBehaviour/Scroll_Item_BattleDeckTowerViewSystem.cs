
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class Scroll_Item_BattleDeckTowerAwakeSystem : AwakeSystem<Scroll_Item_BattleDeckTower> 
	{
		protected override void Awake( Scroll_Item_BattleDeckTower self )
		{
			self.RegisterUIEvent();
		}
	}
	[ObjectSystem]
	public class Scroll_Item_BattleDeckTowerDestroySystem : DestroySystem<Scroll_Item_BattleDeckTower> 
	{
		protected override void Destroy( Scroll_Item_BattleDeckTower self )
		{
			self.HideItem();
			self.DestroyWidget();
		}
	}
}
