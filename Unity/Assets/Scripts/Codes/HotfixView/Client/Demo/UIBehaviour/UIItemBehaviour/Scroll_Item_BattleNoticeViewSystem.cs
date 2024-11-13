
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class Scroll_Item_BattleNoticeAwakeSystem : AwakeSystem<Scroll_Item_BattleNotice> 
	{
		protected override void Awake( Scroll_Item_BattleNotice self )
		{
			self.RegisterUIEvent();
		}
	}
	[ObjectSystem]
	public class Scroll_Item_BattleNoticeDestroySystem : DestroySystem<Scroll_Item_BattleNotice> 
	{
		protected override void Destroy( Scroll_Item_BattleNotice self )
		{
			self.HideItem();
			self.DestroyWidget();
		}
	}
}
