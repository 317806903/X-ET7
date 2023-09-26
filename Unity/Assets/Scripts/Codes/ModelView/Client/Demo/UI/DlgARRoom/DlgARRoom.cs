using System.Collections.Generic;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgARRoom : Entity, IAwake, IUILogic
	{

		public DlgARRoomViewComponent View { get => this.GetComponent<DlgARRoomViewComponent>(); }

		 
		public Dictionary<int, Scroll_Item_RoomMember> ScrollItemRoomMembers;

	}
}
