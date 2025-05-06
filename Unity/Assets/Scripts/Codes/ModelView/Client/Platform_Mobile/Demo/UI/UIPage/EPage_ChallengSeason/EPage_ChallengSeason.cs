using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Client
{
    public class EPage_ChallengSeason : Entity, IAwake<UnityEngine.Transform>, IDestroy, IUILogic
    {
        public EPage_ChallengSeasonViewComponent View { get => this.GetComponent<EPage_ChallengSeasonViewComponent>(); }

        //自定义数据字段
        public bool isAR;
        public Dictionary<int, Scroll_Item_ChallengeList> ScrollItemChallengeList;
        public Dictionary<int, Scroll_Item_ItemShow> ScrollItemReward;
        public int seasonCfgId;
        public PVELevelDifficulty pveLevelDifficulty;
        public int selectIndex;
        public List<(string itemCfgId, int itemNum)> itemList;
    }

    public class EPage_ChallengSeason_ShowWindowData : ShowWindowData
    {
        public RoomTypeInfo roomTypeInfo;
    }
}