using System.Collections.Generic;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgHall : Entity, IAwake, IUILogic
	{
		public DlgHallViewComponent View { get => this.GetComponent<DlgHallViewComponent>();}

		public List<RoomComponent> roomList;
		public Dictionary<int, Scroll_Item_Room> ScrollItemRooms;

		public long Timer;
	}
}
