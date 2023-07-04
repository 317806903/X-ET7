using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [FriendOf(typeof (DlgRoom))]
    public static class DlgRoomSystem
    {
        public static void RegisterUIEvent(this DlgRoom self)
        {
            self.View.E_QuitRoomButton.AddListenerAsync(self.QuitRoom);
            self.View.E_RoomMemberStatusButton.AddListenerAsync(self.ChgRoomMemberStatus);

            self.View.ELoopScrollList_MemberLoopHorizontalScrollRect.prefabSource.prefabName = "Item_RoomMember";
            self.View.ELoopScrollList_MemberLoopHorizontalScrollRect.prefabSource.poolSize = 4;
            self.View.ELoopScrollList_MemberLoopHorizontalScrollRect.AddItemRefreshListener((transform, i) =>
                    self.AddMemberItemRefreshListener(transform, i));
        }

        public static void ShowWindow(this DlgRoom self, Entity contextData = null)
        {
            self.GetRoomInfo().Coroutine();
        }

        public static async ETTask RefreshUI(this DlgRoom self)
        {
			if(self.roomComponent == null)
			{
				return;
			}
            self.View.ELoopScrollList_MemberLoopHorizontalScrollRect.RefreshCells();
            
            long myPlayerId = self.ClientScene().GetComponent<PlayerComponent>().MyId;
            
            if (myPlayerId == self.roomComponent.ownerRoomMemberId)
            {
                self.View.ELable_RoomMemberStatusText.text = "开始游戏";
            }
            else
            {
                RoomMember roomMember = self.roomComponent.GetRoomMember(myPlayerId);
                bool isReady = roomMember.isReady;

                self.View.ELable_RoomMemberStatusText.text = isReady? "取消准备" : "准备";
            }
        }

        public static async ETTask GetRoomInfo(this DlgRoom self)
        {
            Scene clientScene = self.ClientScene();
            long roomId = clientScene.GetComponent<PlayerComponent>().RoomId;
            await RoomHelper.GetRoomInfoAsync(clientScene, roomId);

            RoomComponent roomComponent = clientScene.GetComponent<RoomManagerComponent>().GetRoom(roomId);

            self.roomComponent = roomComponent;

            int count = self.roomComponent.roomMemberSeat.Count;
            self.AddUIScrollItems(ref self.ScrollItemRoomMembers, count);
            self.View.ELoopScrollList_MemberLoopHorizontalScrollRect.SetVisible(true, count);
            
            
            self.View.ELabel_RoomIdText.text = $"{roomId.ToString()} {self.roomComponent.roomStatus.ToString()}";
            
        }

        public static void AddMemberItemRefreshListener(this DlgRoom self, Transform transform, int index)
        {
            Scroll_Item_RoomMember itemRoom = self.ScrollItemRoomMembers[index].BindTrans(transform);

            itemRoom.EButton_OperatorButton.SetVisible(true);
            long roomMemberId = self.roomComponent.roomMemberSeat[index];
            if (roomMemberId == -1)
            {
                itemRoom.ELabel_ContentText.text = $"RoomId:{index}: 空位";
                itemRoom.ELabel_OperatorText.text = $"换位";
                itemRoom.EButton_OperatorButton.AddListener(() =>
                {
                    self.ChgRoomSeat(index);
                });
            }
            else
            {
                long myPlayerId = self.ClientScene().GetComponent<PlayerComponent>().MyId;
                RoomMember roomMember = self.roomComponent.GetRoomMember(roomMemberId);
                if (self.roomComponent.ownerRoomMemberId == roomMemberId)
                {
                    itemRoom.ELabel_ContentText.text = $"房主:{roomMemberId} isReady={roomMember.isReady}";
                }
                else
                {
                    itemRoom.ELabel_ContentText.text = $"RoomId:{index}:{roomMemberId} isReady={roomMember.isReady}";
                }

                if (myPlayerId == self.roomComponent.ownerRoomMemberId)
                {
                    itemRoom.ELabel_OperatorText.text = $"踢出房间";
                    itemRoom.EButton_OperatorButton.AddListener(() =>
                    {
                        self.KickOutRoom(roomMemberId);
                    });
                }
                else
                {
                    itemRoom.ELabel_OperatorText.text = $"查看信息";
                    itemRoom.EButton_OperatorButton.AddListener(() =>
                    {
                    });
                }

                if (myPlayerId == roomMemberId)
                {
                    itemRoom.EButton_OperatorButton.SetVisible(false);
                    itemRoom.ELabel_ContentText.text = $"[自己]{itemRoom.ELabel_ContentText.text}";
                }
            }

        }

        public static async ETTask KickOutRoom(this DlgRoom self, long beKickedPlayerId)
        {
            await RoomHelper.BeKickedOutRoomAsync(self.ClientScene(), beKickedPlayerId);
        }

        public static async ETTask QuitRoom(this DlgRoom self)
        {
            await RoomHelper.QuitRoomAsync(self.ClientScene());
            self.ClientScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_Room);
            await self.ClientScene().GetComponent<UIComponent>().ShowWindowAsync(WindowID.WindowID_Hall);
        }

        public static async ETTask ChgRoomMemberStatus(this DlgRoom self)
        {
            long myPlayerId = self.ClientScene().GetComponent<PlayerComponent>().MyId;
            if (myPlayerId == self.roomComponent.ownerRoomMemberId)
            {
                if (self.roomComponent.ChkIsAllReady() == false)
                {
                    return;
                }
            }
            RoomMember roomMember = self.roomComponent.GetRoomMember(myPlayerId);
            bool isReady = roomMember.isReady;
            
            isReady = !isReady;
            await RoomHelper.ChgRoomMemberStatusAsync(self.ClientScene(), isReady);
        }
        
        public static async ETTask ChgRoomSeat(this DlgRoom self, int newSeat)
        {
            await RoomHelper.ChgRoomSeatAsync(self.ClientScene(), newSeat);
        }
    }
}