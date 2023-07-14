using System.Collections.Generic;

namespace ET.Client
{
    [ComponentOf(typeof (UIBaseWindow))]
    public class DlgRoom: Entity, IAwake, IUILogic
    {
        public DlgRoomViewComponent View
        {
            get => this.GetComponent<DlgRoomViewComponent>();
        }

        public Dictionary<int, Scroll_Item_RoomMember> ScrollItemRoomMembers;
    }
}