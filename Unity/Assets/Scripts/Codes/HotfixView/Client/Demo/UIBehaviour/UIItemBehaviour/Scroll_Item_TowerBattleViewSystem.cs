
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class Scroll_Item_TowerBattleAwakeSystem : AwakeSystem<Scroll_Item_TowerBattle> 
	{
		protected override void Awake( Scroll_Item_TowerBattle self )
		{
			self.RegisterUIEvent();
		}
	}
	[ObjectSystem]
	public class Scroll_Item_TowerBattleDestroySystem : DestroySystem<Scroll_Item_TowerBattle> 
	{
		protected override void Destroy( Scroll_Item_TowerBattle self )
		{
			self.HideItem();
			self.DestroyWidget();
		}
	}
}
