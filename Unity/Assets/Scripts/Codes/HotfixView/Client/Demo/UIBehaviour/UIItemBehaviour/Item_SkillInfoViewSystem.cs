
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class Scroll_Item_SkillInfoDestroySystem : DestroySystem<Scroll_Item_SkillInfo> 
	{
		protected override void Destroy( Scroll_Item_SkillInfo self )
		{
			self.DestroyWidget();
		}
	}
}
