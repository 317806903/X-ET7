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
            long roomId = self.ClientScene().GetComponent<PlayerComponent>().RoomId;
            self.GetRoomInfo().Coroutine();
        }

        public static async ETTask RefreshUI(this DlgRoom self)
        {
            self.View.ELoopScrollList_MemberLoopHorizontalScrollRect.RefreshCells();
        }

        public static async ETTask GetRoomInfo(this DlgRoom self)
        {
            Scene clientScene = self.ClientScene();
            long roomId = clientScene.GetComponent<PlayerComponent>().RoomId;
            await RoomHelper.GetRoomInfoAsync(clientScene, roomId);

            RoomComponent roomComponent = clientScene.GetComponent<RoomManagerComponent>().Get(roomId);

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
                RoomMember roomMember = self.roomComponent.GetRoomMember(roomMemberId);
                if (self.roomComponent.ownerRoomMemberId == roomMemberId)
                {
                    itemRoom.ELabel_ContentText.text = $"房主:{roomMemberId} isReady={roomMember.isReady}";
                }
                else
                {
                    itemRoom.ELabel_ContentText.text = $"RoomId:{index}:{roomMemberId} isReady={roomMember.isReady}";
                }
                itemRoom.ELabel_OperatorText.text = $"查看信息";
                itemRoom.EButton_OperatorButton.AddListener(() => { });

                if (self.ClientScene().GetComponent<PlayerComponent>().MyId == roomMemberId)
                {
                    itemRoom.EButton_OperatorButton.SetVisible(false);
                    itemRoom.ELabel_ContentText.text = $"自己:{roomMemberId} isReady={roomMember.isReady}";
                }
            }

        }

        public static async ETTask QuitRoom(this DlgRoom self)
        {
            await RoomHelper.QuitRoomAsync(self.ClientScene());
            self.ClientScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_Room);
            await self.ClientScene().GetComponent<UIComponent>().ShowWindowAsync(WindowID.WindowID_Hall);
        }

        public static async ETTask ChgRoomMemberStatus(this DlgRoom self)
        {
            if (self.ClientScene().GetComponent<PlayerComponent>().MyId == self.roomComponent.ownerRoomMemberId)
            {
                if (self.roomComponent.ChkIsAllReady() == false)
                {
                    return;
                }
            }
            bool isReady = self.ClientScene().GetComponent<PlayerComponent>().IsRoomReady;
            isReady = !isReady;
            await RoomHelper.ChgRoomMemberStatusAsync(self.ClientScene(), isReady);
            
            if (self.ClientScene().GetComponent<PlayerComponent>().MyId == self.roomComponent.ownerRoomMemberId)
            {
                self.View.ELable_RoomMemberStatusText.text = "开始游戏";
            }
            else
            {
                self.View.ELable_RoomMemberStatusText.text = isReady? "取消准备" : "准备";
            }
        }
        
        public static async ETTask ChgRoomSeat(this DlgRoom self, int newSeat)
        {
            await RoomHelper.ChgRoomSeatAsync(self.ClientScene(), newSeat);
        }
    }
}