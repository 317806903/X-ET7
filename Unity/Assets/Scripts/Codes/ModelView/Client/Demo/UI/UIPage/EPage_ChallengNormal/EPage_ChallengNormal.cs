using System.Collections.Generic;

namespace ET.Client
{
    public class EPage_ChallengNormal : Entity, IAwake<UnityEngine.Transform>, IDestroy, IUILogic
    {
        public EPage_ChallengNormalViewComponent View { get => this.GetComponent<EPage_ChallengNormalViewComponent>(); }

        //自定义数据字段
        public bool isAR;
        public Dictionary<int, Scroll_Item_ChallengeList> ScrollItemChallengeList;
        public Dictionary<int, Scroll_Item_ItemShow> ScrollItemReward;
        public Dictionary<int, Scroll_Item_Monsters> ScrollItemMonster;
        public int selectIndex;
        public List<(string itemCfgId, int itemNum)> itemList;
    }
}