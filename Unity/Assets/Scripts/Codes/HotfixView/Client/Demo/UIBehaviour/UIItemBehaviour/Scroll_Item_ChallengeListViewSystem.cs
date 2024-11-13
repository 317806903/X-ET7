
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class Scroll_Item_ChallengeListAwakeSystem : AwakeSystem<Scroll_Item_ChallengeList> 
	{
		protected override void Awake( Scroll_Item_ChallengeList self )
		{
			self.RegisterUIEvent();
		}
	}
	[ObjectSystem]
	public class Scroll_Item_ChallengeListDestroySystem : DestroySystem<Scroll_Item_ChallengeList> 
	{
		protected override void Destroy( Scroll_Item_ChallengeList self )
		{
			self.HideItem();
			self.DestroyWidget();
		}
	}
}
