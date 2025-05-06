
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class Scroll_Item_SkillBattleInfoAwakeSystem : AwakeSystem<Scroll_Item_SkillBattleInfo> 
	{
		protected override void Awake( Scroll_Item_SkillBattleInfo self )
		{
			self.RegisterUIEvent();
		}
	}
	[ObjectSystem]
	public class Scroll_Item_SkillBattleInfoDestroySystem : DestroySystem<Scroll_Item_SkillBattleInfo> 
	{
		protected override void Destroy( Scroll_Item_SkillBattleInfo self )
		{
			self.HideItem();
			self.DestroyWidget();
		}
	}
}
