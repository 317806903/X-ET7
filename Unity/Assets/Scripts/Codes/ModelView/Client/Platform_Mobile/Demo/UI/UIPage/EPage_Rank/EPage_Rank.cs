using System.Collections.Generic;

namespace ET.Client
{
	public class EPage_Rank : Entity, IAwake<UnityEngine.Transform>, IDestroy, IUILogic
	{
		public EPage_RankViewComponent View { get => this.GetComponent<EPage_RankViewComponent>(); }

        public Dictionary<int, Scroll_Item_RankEndlessChallenge> ScrollItemRankEndlessChallenges;


    }
}
