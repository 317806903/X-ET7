using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Server
{
    public static class RoomHelper
    {
	    public static RoomManagerComponent GetRoomManager(Scene scene)
	    {
		    RoomManagerComponent roomManagerComponent = scene.GetComponent<RoomManagerComponent>();
		    return roomManagerComponent;
	    }

        public static async ETTask SendRoomInfoChgNotice(this RoomComponent self, bool waitOneFrame)
        {
	        if (waitOneFrame)
	        {
		        await TimerComponent.Instance.WaitFrameAsync();
	        }
			R2C_RoomInfoChgNotice _R2C_RoomInfoChgNotice = new();
			List<RoomMember> roomMemberList = self.GetRoomMemberList();
			for (int i = 0; i < roomMemberList.Count; i++)
			{
				RoomMember roomMember = roomMemberList[i];
				// if (playerId == roomMember.Id)
				// {
				// 	continue;
				// }
				MessageHelper.SendToClient(roomMember.Id, _R2C_RoomInfoChgNotice, false);
			}
			await ETTask.CompletedTask;
        }
    }
}