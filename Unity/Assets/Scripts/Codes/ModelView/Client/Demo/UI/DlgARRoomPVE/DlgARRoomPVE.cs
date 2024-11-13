using System.Collections.Generic;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgARRoomPVE : Entity, IAwake, IUILogic, IUIDlg
	{
		public DlgARRoomPVEViewComponent View { get => this.GetComponent<DlgARRoomPVEViewComponent>(); }

		public Dictionary<int, Scroll_Item_RoomMember> ScrollItemRoomMembers;
		public Dictionary<int, Scroll_Item_Monsters> ScrollItemMonsters;
		public Dictionary<int, Scroll_Item_ItemShow> ScrollItemReward;

		public int level;
		public bool isAR;
		public List<(string itemCfgId, int itemNum)> itemList;
	}
}
