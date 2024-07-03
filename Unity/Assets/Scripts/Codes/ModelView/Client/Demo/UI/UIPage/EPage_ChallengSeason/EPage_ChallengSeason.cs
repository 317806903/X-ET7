using System.Collections.Generic;

namespace ET.Client
{
    public class EPage_ChallengSeason : Entity, IAwake<UnityEngine.Transform>, IDestroy
    {
        public EPage_ChallengSeasonViewComponent View { get => this.GetComponent<EPage_ChallengSeasonViewComponent>(); }

        //自定义数据字段
        public bool isAR;
        public Dictionary<int, Scroll_Item_ChallengeList> ScrollItemChallengeList;
        public Dictionary<int, Scroll_Item_TowerBuy> ScrollItemReward;
        public Dictionary<int, Scroll_Item_Monsters> ScrollItemMonster;
        public int seasonId;
        public int selectIndex;
    }
}