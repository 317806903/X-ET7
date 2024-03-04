using System.Collections.Generic;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgARRoomPVE : Entity, IAwake, IUILogic
	{
		public DlgARRoomPVEViewComponent View { get => this.GetComponent<DlgARRoomPVEViewComponent>(); }
		
		public Dictionary<int, Scroll_Item_RoomMember> ScrollItemRoomMembers;

		public int level;
	}
}
