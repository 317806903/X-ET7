using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgARRoomPVESeason : Entity, IAwake, IUILogic, IUIDlg
	{
		public UnityEngine.Transform GetUITransform { get => View.uiTransform; }
		public DlgARRoomPVESeasonViewComponent View { get => this.GetComponent<DlgARRoomPVESeasonViewComponent>(); }

		public Dictionary<int, Scroll_Item_RoomMember> ScrollItemRoomMembers;
		public Dictionary<int, Scroll_Item_Monsters> ScrollItemMonsters;
		public Dictionary<int, Scroll_Item_ItemShow> ScrollItemReward;

		public int seasonCfgId;
		public int pveIndex;
		public PVELevelDifficulty pveLevelDifficulty;
		public bool isAR;
		public List<(string itemCfgId, int itemNum)> itemList;
	}
}
