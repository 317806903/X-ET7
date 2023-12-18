
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class Scroll_Item_RankEndlessChallengeDestroySystem : DestroySystem<Scroll_Item_RankEndlessChallenge> 
	{
		protected override void Destroy( Scroll_Item_RankEndlessChallenge self )
		{
			self.DestroyWidget();
		}
	}
}
