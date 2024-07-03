using System.Collections.Generic;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgRankPowerupSeason : Entity, IAwake, IUILogic
	{
		public DlgRankPowerupSeasonViewComponent View { get => this.GetComponent<DlgRankPowerupSeasonViewComponent>(); }

		//页面的索引，0代表排行榜，1代表养成
		public int pageIndex;

        public Dictionary<int, Scroll_Item_TowerBuy> ScrollItemReward;
        public Dictionary<int, Scroll_Item_Monsters> ScrollItemMonster;
        public Dictionary<int, Scroll_Item_Frame> ScrollItemFrameIcons;
        public List<string> avatarFrameList;
        public int seasonId;
        public int selectIndex;
    }
}
