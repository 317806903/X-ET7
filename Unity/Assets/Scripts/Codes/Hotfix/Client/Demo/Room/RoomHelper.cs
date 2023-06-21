using System;

namespace ET.Client
{
    public static class RoomHelper
    {
        public static async ETTask GetRoomListAsync(Scene clientScene)
        {
            try
            {
                G2C_GetRoomList _G2C_GetRoomList = await clientScene.GetComponent<SessionComponent>().Session.Call(new C2G_GetRoomList()) as G2C_GetRoomList;
                
                clientScene.GetComponent<RoomManagerComponent>().Init(_G2C_GetRoomList.RoomInfos);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }	
        }

        public static async ETTask GetRoomInfoAsync(Scene clientScene, long roomId)
        {
            try
            {
                G2C_GetRoomInfo _G2C_GetRoomInfo = await clientScene.GetComponent<SessionComponent>().Session.Call(new C2G_GetRoomInfo()
                        {
                            RoomId = roomId,
                        }) as 
                G2C_GetRoomInfo;
                clientScene.GetComponent<RoomManagerComponent>().Init(roomId, _G2C_GetRoomInfo.RoomInfo, _G2C_GetRoomInfo.RoomMemberInfos);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }	
        }

        public static async ETTask CreateRoomAsync(Scene clientScene)
        {
            try
            {
                G2C_CreateRoom _G2C_CreateRoom = await clientScene.GetComponent<SessionComponent>().Session.Call(new C2G_CreateRoom()) as G2C_CreateRoom;
                long roomId = _G2C_CreateRoom.RoomId;
                clientScene.GetComponent<RoomManagerComponent>().AddChildWithId<RoomComponent>(roomId);
                clientScene.GetComponent<PlayerComponent>().PlayerStatus = PlayerStatus.Room;
                clientScene.GetComponent<PlayerComponent>().RoomId = roomId;
                clientScene.GetComponent<PlayerComponent>().IsRoomReady = false;
            }
            catch (Exception e)
            {
                Log.Error(e);
            }	
        }
        
        public static async ETTask JoinRoomAsync(Scene clientScene, long roomId)
        {
            try
            {
                G2C_JoinRoom _G2C_JoinRoom = await clientScene.GetComponent<SessionComponent>().Session.Call(new C2G_JoinRoom()
                {
                    RoomId = roomId,
                }) as G2C_JoinRoom;
                clientScene.GetComponent<PlayerComponent>().PlayerStatus = PlayerStatus.Room;
                clientScene.GetComponent<PlayerComponent>().RoomId = roomId;
                clientScene.GetComponent<PlayerComponent>().IsRoomReady = false;
            }
            catch (Exception e)
            {
                Log.Error(e);
            }	
        }
        
        public static async ETTask QuitRoomAsync(Scene clientScene)
        {
            try
            {
                G2C_QuitRoom _G2C_QuitRoom = await clientScene.GetComponent<SessionComponent>().Session.Call(new C2G_QuitRoom()) as G2C_QuitRoom;
                clientScene.GetComponent<PlayerComponent>().PlayerStatus = PlayerStatus.Hall;
                clientScene.GetComponent<PlayerComponent>().RoomId = 0;
                clientScene.GetComponent<PlayerComponent>().IsRoomReady = false;
            }
            catch (Exception e)
            {
                Log.Error(e);
            }	
        }
        
        public static async ETTask ChgRoomMemberStatusAsync(Scene clientScene, bool isReady)
        {
            try
            {
                G2C_ChgRoomMemberStatus _G2C_ChgRoomMemberStatus = await clientScene.GetComponent<SessionComponent>().Session.Call(new C2G_ChgRoomMemberStatus()
                {
                    IsReady = isReady?1:0,
                }) as G2C_ChgRoomMemberStatus;
                clientScene.GetComponent<PlayerComponent>().IsRoomReady = _G2C_ChgRoomMemberStatus.IsReady == 1?true :false;
            }
            catch (Exception e)
            {
                Log.Error(e);
            }	
        }
        
        public static async ETTask ChgRoomSeatAsync(Scene clientScene, int newSeat)
        {
            try
            {
                G2C_ChgRoomMemberSeat _G2C_ChgRoomMemberSeat = await clientScene.GetComponent<SessionComponent>().Session.Call(new 
                C2G_ChgRoomMemberSeat()
                {
                    NewSeat = newSeat,
                }) as G2C_ChgRoomMemberSeat;
            }
            catch (Exception e)
            {
                Log.Error(e);
            }	
        }
        
        public static async ETTask MemberQuitBattleAsync(Scene clientScene)
        {
            try
            {
                M2C_MemberQuitBattle _M2C_MemberQuitBattle = await clientScene.GetComponent<SessionComponent>().Session.Call(new C2M_MemberQuitBattle()) as 
                M2C_MemberQuitBattle;
            }
            catch (Exception e)
            {
                Log.Error(e);
            }	
        }

        public static async ETTask ReturnBackBattle(Scene clientScene)
        {
            try
            {
                G2C_ReturnBackBattle _G2C_ReturnBackBattle = await clientScene.GetComponent<SessionComponent>().Session.Call(new C2G_ReturnBackBattle()) as G2C_ReturnBackBattle;
            }
            catch (Exception e)
            {
                Log.Error(e);
            }	
        }

    }
}