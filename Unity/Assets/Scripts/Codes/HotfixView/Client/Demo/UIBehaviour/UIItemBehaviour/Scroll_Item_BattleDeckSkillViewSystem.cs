
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class Scroll_Item_BattleDeckSkillAwakeSystem : AwakeSystem<Scroll_Item_BattleDeckSkill> 
	{
		protected override void Awake( Scroll_Item_BattleDeckSkill self )
		{
			self.RegisterUIEvent();
		}
	}
	[ObjectSystem]
	public class Scroll_Item_BattleDeckSkillDestroySystem : DestroySystem<Scroll_Item_BattleDeckSkill> 
	{
		protected override void Destroy( Scroll_Item_BattleDeckSkill self )
		{
			self.HideItem();
			self.DestroyWidget();
		}
	}
}
