
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class Scroll_Item_SkillBattleInfoDestroySystem : DestroySystem<Scroll_Item_SkillBattleInfo> 
	{
		protected override void Destroy( Scroll_Item_SkillBattleInfo self )
		{
			self.DestroyWidget();
		}
	}
}
