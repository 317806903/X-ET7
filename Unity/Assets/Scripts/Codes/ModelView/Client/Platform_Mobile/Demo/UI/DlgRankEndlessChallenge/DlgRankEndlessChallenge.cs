using System.Collections.Generic;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgRankEndlessChallenge : Entity, IAwake, IUILogic, IUIDlg
	{
		public UnityEngine.Transform GetUITransform { get => View.uiTransform; }
		public DlgRankEndlessChallengeViewComponent View { get => this.GetComponent<DlgRankEndlessChallengeViewComponent>(); }
		
		public Dictionary<int, Scroll_Item_RankEndlessChallenge> ScrollItemRankEndlessChallenges;

	}
}
