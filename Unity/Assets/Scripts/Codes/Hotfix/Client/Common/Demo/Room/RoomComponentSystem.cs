using System.Collections.Generic;
using System.Linq;

namespace ET.Client
{
    [FriendOf(typeof(RoomComponent))]
    public static class RoomComponentSystem
    {
        public static int GetPhysicalCost(this RoomComponent self)
        {
            long myPlayerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());
            bool isOwner = myPlayerId == self.ownerRoomMemberId;
            int costValue = ET.GamePlayHelper.GetPhysicalCost(self.roomTypeInfo, isOwner);
            return costValue;
        }

    }
}