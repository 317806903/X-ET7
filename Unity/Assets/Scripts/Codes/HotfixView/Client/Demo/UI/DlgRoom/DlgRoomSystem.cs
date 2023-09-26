using System.Collections;
using System.Collections.Generic;
using System;
using ET.AbilityConfig;
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

        public static void ShowWindow(this DlgRoom self, ShowWindowData contextData = null)
        {
            self.GetRoomInfo().Coroutine();
        }

        public static async ETTask RefreshUI(this DlgRoom self)
        {
            self.View.ELoopScrollList_MemberLoopHorizontalScrollRect.RefreshCells();

            self.SetRoomMemberStatusText();
        }

        public static void SetRoomMemberStatusText(this DlgRoom self)
        {
            RoomComponent roomComponent = self.GetRoomComponent();

			if(roomComponent == null)
			{
				return;
			}

            long myPlayerId = PlayerHelper.GetMyPlayerId(self.DomainScene());

            if (myPlayerId == roomComponent.ownerRoomMemberId)
            {
                if (roomComponent.ChkIsAllReady())
                {
                    self.View.ELable_RoomMemberStatusText.text = "开始游戏";
                }
                else
                {
                    self.View.ELable_RoomMemberStatusText.text = "等待全部准备";
                }

                self.View.E_InputFieldBattleLevelCfgInputField.readOnly = false;
                self.View.E_InputFieldBattleLevelCfgInputField.onEndEdit.RemoveAllListeners();
                self.View.E_InputFieldBattleLevelCfgInputField.onEndEdit.AddListener(
                    (txt)=>
                    {
                        self.ChgRoomBattleLevelCfg().Coroutine();
                    });
            }
            else
            {
                RoomMember roomMember = roomComponent.GetRoomMember(myPlayerId);
                bool isReady = roomMember.isReady;

                self.View.ELable_RoomMemberStatusText.text = isReady? "取消准备" : "点击准备";

                self.View.E_InputFieldBattleLevelCfgInputField.readOnly = true;
            }

            self.View.E_InputFieldBattleLevelCfgInputField.text = roomComponent.gamePlayBattleLevelCfgId;
        }

        public static async ETTask GetRoomInfo(this DlgRoom self)
        {
            Scene clientScene = self.ClientScene();
            long roomId = clientScene.GetComponent<PlayerComponent>().RoomId;
            await RoomHelper.GetRoomInfoAsync(clientScene, roomId);

            RoomComponent roomComponent = self.GetRoomComponent();

            int count = roomComponent.roomMemberSeat.Count;
            self.AddUIScrollItems(ref self.ScrollItemRoomMembers, count);
            self.View.ELoopScrollList_MemberLoopHorizontalScrollRect.SetVisible(true, count);


            self.View.ELabel_RoomIdText.text = $"{roomId.ToString()} {roomComponent.roomStatus.ToString()}";
            self.SetRoomMemberStatusText();
        }

        public static RoomComponent GetRoomComponent(this DlgRoom self)
        {
            Scene clientScene = self.ClientScene();
            long roomId = clientScene.GetComponent<PlayerComponent>().RoomId;
            RoomManagerComponent roomManagerComponent = ET.Client.RoomHelper.GetRoomManager(self.DomainScene());
            RoomComponent roomComponent = roomManagerComponent.GetRoom(roomId);
            return roomComponent;
        }

        public static void AddMemberItemRefreshListener(this DlgRoom self, Transform transform, int index)
        {
            RoomComponent roomComponent = self.GetRoomComponent();

            Scroll_Item_RoomMember itemRoom = self.ScrollItemRoomMembers[index].BindTrans(transform);

            itemRoom.ELabel_Content_LvTextMeshProUGUI.gameObject.SetActive(false);
            itemRoom.EButton_OperatorButton.SetVisible(true);
            long roomMemberId = roomComponent.roomMemberSeat[index];
            if (roomMemberId == -1)
            {
                itemRoom.ELabel_Content_NameTextMeshProUGUI.text = $"空位:{index}";
                itemRoom.ELabel_OperatorText.text = $"换位";
                itemRoom.EButton_OperatorButton.AddListener(() =>
                {
                    self.ChgRoomSeat(index).Coroutine();
                });
                itemRoom.ELabel_Content_LeaderImage.gameObject.SetActive(false);
                itemRoom.EButton_IconImage.gameObject.SetActive(false);
                itemRoom.EG_ReadyRectTransform.gameObject.SetActive(false);
            }
            else
            {
                long myPlayerId = PlayerHelper.GetMyPlayerId(self.DomainScene());
                RoomMember roomMember = roomComponent.GetRoomMember(roomMemberId);
                itemRoom.EButton_IconImage.gameObject.SetActive(true);
                itemRoom.EG_ReadyRectTransform.gameObject.SetActive(roomMember.isReady);
                if (roomComponent.ownerRoomMemberId == roomMemberId)
                {
                    itemRoom.ELabel_Content_LeaderImage.gameObject.SetActive(true);
                    itemRoom.ELabel_Content_NameTextMeshProUGUI.text = $"{roomMemberId}";
                }
                else
                {
                    itemRoom.ELabel_Content_LeaderImage.gameObject.SetActive(false);
                    itemRoom.ELabel_Content_NameTextMeshProUGUI.text = $"{roomMemberId}";
                }

                if (myPlayerId == roomComponent.ownerRoomMemberId)
                {
                    itemRoom.ELabel_OperatorText.text = $"踢出房间";
                    itemRoom.EButton_OperatorButton.AddListener(() =>
                    {
                        self.KickOutRoom(roomMemberId).Coroutine();
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
                    itemRoom.ELabel_Content_NameTextMeshProUGUI.text = $"[自己]";
                }
            }

        }

        public static async ETTask KickOutRoom(this DlgRoom self, long beKickedPlayerId)
        {
            ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioClick(self.DomainScene());

            await RoomHelper.KickMemberOutRoomAsync(self.ClientScene(), beKickedPlayerId);
        }

        public static async ETTask QuitRoom(this DlgRoom self)
        {
            ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioBack(self.DomainScene());

            string msg = "是否确认退出房间?";
            ET.Client.UIManagerHelper.ShowConfirm(self.DomainScene(), msg, () =>
            {
                self._QuitRoom().Coroutine();
            }, null);
        }

        public static async ETTask _QuitRoom(this DlgRoom self)
        {
            await RoomHelper.QuitRoomAsync(self.ClientScene());
            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgRoom>();
            await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgHall>();
        }

        public static async ETTask ChgRoomMemberStatus(this DlgRoom self)
        {
            ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioClick(self.DomainScene());

            RoomComponent roomComponent = self.GetRoomComponent();
            long myPlayerId = PlayerHelper.GetMyPlayerId(self.DomainScene());
            if (myPlayerId == roomComponent.ownerRoomMemberId)
            {
                if (roomComponent.ChkIsAllReady() == false)
                {
                    UIManagerHelper.ShowTip(self.DomainScene(), "等待全部准备");
                    return;
                }
            }
            RoomMember roomMember = roomComponent.GetRoomMember(myPlayerId);
            bool isReady = roomMember.isReady;

            isReady = !isReady;
            await RoomHelper.ChgRoomMemberStatusAsync(self.ClientScene(), isReady);
        }

        public static async ETTask ChgRoomSeat(this DlgRoom self, int newSeat)
        {
            ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioClick(self.DomainScene());

            await RoomHelper.ChgRoomMemberSeatAsync(self.ClientScene(), newSeat);
        }

        public static async ETTask ChgRoomBattleLevelCfg(this DlgRoom self)
        {
            ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioClick(self.DomainScene());

            RoomComponent roomComponent = self.GetRoomComponent();
            if(roomComponent == null)
            {
                return;
            }

            long myPlayerId = PlayerHelper.GetMyPlayerId(self.DomainScene());
            if (myPlayerId == roomComponent.ownerRoomMemberId)
            {
                string newBattleLevelCfgId = self.View.E_InputFieldBattleLevelCfgInputField.text;
                if (GamePlayBattleLevelCfgCategory.Instance.Contain(newBattleLevelCfgId) == false)
                {
                    Log.Error($"GamePlayBattleLevelCfg not exist when newBattleLevelCfgId[{newBattleLevelCfgId}]");
                    self.View.E_InputFieldBattleLevelCfgInputField.text = roomComponent.gamePlayBattleLevelCfgId;
                    return;
                }
                GamePlayBattleLevelCfg gamePlayBattleLevelCfg = GamePlayBattleLevelCfgCategory.Instance.Get(newBattleLevelCfgId);
                if (gamePlayBattleLevelCfg.IsGlobalMode)
                {
                    Log.Error($"gamePlayBattleLevelCfg.IsGlobalMode == true when newBattleLevelCfgId[{newBattleLevelCfgId}]");
                    self.View.E_InputFieldBattleLevelCfgInputField.text = roomComponent.gamePlayBattleLevelCfgId;
                    return;
                }

                await RoomHelper.ChgRoomBattleLevelCfgAsync(self.ClientScene(), newBattleLevelCfgId);
            }
        }

    }
}