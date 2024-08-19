using System.Collections.Generic;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgARRoomPVESeason : Entity, IAwake, IUILogic, IUIDlg
	{
		public DlgARRoomPVESeasonViewComponent View { get => this.GetComponent<DlgARRoomPVESeasonViewComponent>(); }

		public Dictionary<int, Scroll_Item_RoomMember> ScrollItemRoomMembers;
		public Dictionary<int, Scroll_Item_Monsters> ScrollItemMonsters;
		public Dictionary<int, Scroll_Item_TowerBuy> ScrollItemReward;

		public int seasonCfgId;
		public int level;
		public bool isAR;
		public List<(string itemCfgId, int itemNum)> itemList;
	}
}
