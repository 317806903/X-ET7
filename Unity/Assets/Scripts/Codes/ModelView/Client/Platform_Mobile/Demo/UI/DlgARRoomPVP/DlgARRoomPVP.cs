using System.Collections.Generic;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgARRoomPVP : Entity, IAwake, IUILogic, IUIDlg
	{
		public UnityEngine.Transform GetUITransform { get => View.uiTransform; }
		public DlgARRoomPVPViewComponent View { get => this.GetComponent<DlgARRoomPVPViewComponent>(); }

		public Dictionary<int, Scroll_Item_RoomMember> ScrollItemRoomMembersLeft;
		public Dictionary<int, Scroll_Item_RoomMember> ScrollItemRoomMembersRight;

		public List<int> roomMembersLeft = new();
		public List<int> roomMembersRight = new();
	}
}
