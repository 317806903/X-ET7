
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class Scroll_Item_TowerBattleBuyAwakeSystem : AwakeSystem<Scroll_Item_TowerBattleBuy> 
	{
		protected override void Awake( Scroll_Item_TowerBattleBuy self )
		{
			self.RegisterUIEvent();
		}
	}
	[ObjectSystem]
	public class Scroll_Item_TowerBattleBuyDestroySystem : DestroySystem<Scroll_Item_TowerBattleBuy> 
	{
		protected override void Destroy( Scroll_Item_TowerBattleBuy self )
		{
			self.HideItem();
			self.DestroyWidget();
		}
	}
}
