using System.Collections.Generic;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgARRoomPVESeason : Entity, IAwake, IUILogic
	{
		public DlgARRoomPVESeasonViewComponent View { get => this.GetComponent<DlgARRoomPVESeasonViewComponent>(); }

		public Dictionary<int, Scroll_Item_RoomMember> ScrollItemRoomMembers;
		public Dictionary<int, Scroll_Item_Monsters> ScrollItemMonsters;
		public Dictionary<int, Scroll_Item_TowerBuy> ScrollItemReward;

		public int seasonId;
		public int level;
		public bool isAR;
	}
}
