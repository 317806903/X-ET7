using System.Collections.Generic;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgChallengeMode : Entity, IAwake, IUILogic
	{
		public DlgChallengeModeViewComponent View { get => this.GetComponent<DlgChallengeModeViewComponent>(); }

		public Dictionary<int, Scroll_Item_ChallengeList> ScrollItemChallengeList;
		public Dictionary<int, Scroll_Item_TowerBuy> ScrollItemReward;
		public Dictionary<int, Scroll_Item_Monsters> ScrollItemMonster;
		public int selectIndex;
	}
}
