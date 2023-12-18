using System.Collections.Generic;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgChallengeMode : Entity, IAwake, IUILogic
	{
		public DlgChallengeModeViewComponent View { get => this.GetComponent<DlgChallengeModeViewComponent>(); }

		public Dictionary<int, Scroll_Item_ChallengeList> ScrollItemChallengeList;
		public int selectIndex;
	}
}
