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
            self.View.EButton_ChooseBattleCfgButton.AddListenerAsync(self.OnChooseBattleCfg);
            self.View.E_RoomMemberChgTeamButton.AddListenerAsync(self.OnChgTeam);

            self.View.ELoopScrollList_MemberLoopHorizontalScrollRect.prefabSource.prefabName = "Item_RoomMember";
            self.View.ELoopScrollList_MemberLoopHorizontalScrollRect.prefabSource.poolSize = 4;
            self.View.ELoopScrollList_MemberLoopHorizontalScrollRect.AddItemRefreshListener((transform, i) =>
                    self.AddMemberItemRefreshListener(transform, i));
        }

        public static void ShowWindow(this DlgRoom self, ShowWindowData contextData = null)
        {
            self.GetRoomInfo().Coroutine();
            self.ShowBattleCfgChoose().Coroutine();
        }

        public static async ETTask RefreshUI(this DlgRoom self)
        {
            self.View.ELoopScrollList_MemberLoopHorizontalScrollRect.RefreshCells();

            self.SetRoomMemberStatusText();
        }

        public static async ETTask ShowBattleCfgChoose(this DlgRoom self)
        {
            PlayerComponent playerComponent = ET.Client.PlayerHelper.GetMyPlayerComponent(self.DomainScene());
            if (playerComponent.IsDebugMode)
            {
                self.View.EG_ChooseBattleCfgRectTransform.gameObject.SetActive(true);
            }
            else
            {
                self.View.EG_ChooseBattleCfgRectTransform.gameObject.SetActive(false);
            }

            await ETTask.CompletedTask;
        }

        public static async ETTask RefreshBattleCfgIdChoose(this DlgRoom self, string gamePlayBattleLevelCfgId)
        {
            await RoomHelper.ChgRoomBattleLevelCfgAsync(self.ClientScene(), gamePlayBattleLevelCfgId);
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
                    self.View.ELable_RoomMemberStatusText.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_RoomMemberStatus_StartGame");
                }
                else
                {
                    self.View.ELable_RoomMemberStatusText.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_RoomMemberStatus_WaitAllReady");
                }

                self.View.EButton_ChooseBattleCfgButton.gameObject.SetActive(true);
            }
            else
            {
                RoomMember roomMember = roomComponent.GetRoomMember(myPlayerId);
                bool isReady = roomMember.isReady;

                string textReady = LocalizeComponent.Instance.GetTextValue("TextCode_Key_RoomMemberStatus_Ready");
                string textCancelReady = LocalizeComponent.Instance.GetTextValue("TextCode_Key_RoomMemberStatus_CancelReady");
                self.View.ELable_RoomMemberStatusText.text = isReady? textCancelReady : textReady;

                self.View.EButton_ChooseBattleCfgButton.gameObject.SetActive(false);
            }

            GamePlayBattleLevelCfg gamePlayBattleLevelCfg = GamePlayBattleLevelCfgCategory.Instance.Get(roomComponent.gamePlayBattleLevelCfgId);
            self.View.ELabel_BattleCfgIdTextMeshProUGUI.text = gamePlayBattleLevelCfg.Name;

            bool isShowTeamChgButton = gamePlayBattleLevelCfg.TeamMode is PlayerTeam;
            self.View.E_RoomMemberChgTeamButton.gameObject.SetActive(isShowTeamChgButton);
        }

        public static async ETTask GetRoomInfo(this DlgRoom self)
        {
            PlayerComponent playerComponent = ET.Client.PlayerHelper.GetMyPlayerComponent(self.DomainScene());
            long roomId = playerComponent.RoomId;
            await RoomHelper.GetRoomInfoAsync(self.DomainScene(), roomId);

            RoomComponent roomComponent = self.GetRoomComponent();

            int count = roomComponent.roomMemberSeat.Count;
            self.AddUIScrollItems(ref self.ScrollItemRoomMembers, count);
            self.View.ELoopScrollList_MemberLoopHorizontalScrollRect.SetVisible(true, count);


            self.View.ELabel_RoomIdText.text = $"{roomId.ToString()} {roomComponent.roomStatus.ToString()}";
            self.SetRoomMemberStatusText();
        }

        public static RoomComponent GetRoomComponent(this DlgRoom self)
        {
            PlayerComponent playerComponent = ET.Client.PlayerHelper.GetMyPlayerComponent(self.DomainScene());
            long roomId = playerComponent.RoomId;
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
            itemRoom.EImage_TeamImage.SetVisible(false);
            long roomMemberId = roomComponent.roomMemberSeat[index];
            if (roomMemberId == -1)
            {
                itemRoom.ELabel_Content_NameTextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_RoomMemberStatus_SeatIndex", index);
                itemRoom.ELabel_OperatorText.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_RoomMemberStatus_ChgSeat");
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
                    itemRoom.ELabel_OperatorText.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_RoomMemberStatus_KickMember");
                    itemRoom.EButton_OperatorButton.AddListener(() =>
                    {
                        self.KickOutRoom(roomMemberId).Coroutine();
                    });
                }
                else
                {
                    itemRoom.ELabel_OperatorText.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_RoomMemberStatus_SeeMemberInfo");
                    itemRoom.EButton_OperatorButton.AddListener(() =>
                    {
                    });
                }

                if (myPlayerId == roomMemberId)
                {
                    itemRoom.EButton_OperatorButton.SetVisible(false);
                    itemRoom.ELabel_Content_NameTextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_RoomMemberStatus_Self");
                }

                GamePlayBattleLevelCfg gamePlayBattleLevelCfg = GamePlayBattleLevelCfgCategory.Instance.Get(roomComponent.gamePlayBattleLevelCfgId);
                bool isShowTeamChgButton = gamePlayBattleLevelCfg.TeamMode is PlayerTeam;
                if (isShowTeamChgButton)
                {
                    itemRoom.EImage_TeamImage.SetVisible(true);
                    if (roomMember.roomTeamId == RoomTeamId.Green)
                    {
                        itemRoom.EImage_TeamImage.color = Color.green;
                    }
                    else if (roomMember.roomTeamId == RoomTeamId.Red)
                    {
                        itemRoom.EImage_TeamImage.color = Color.red;
                    }
                    else if (roomMember.roomTeamId == RoomTeamId.Blue)
                    {
                        itemRoom.EImage_TeamImage.color = Color.blue;
                    }
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

            string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_IsQuitRoom");
            ET.Client.UIManagerHelper.ShowConfirm(self.DomainScene(), msg, () =>
            {
                self._QuitRoom().Coroutine();
            }, null);
        }

        public static async ETTask _QuitRoom(this DlgRoom self)
        {
            await RoomHelper.QuitRoomAsync(self.ClientScene());
            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgRoom>();
            PlayerComponent playerComponent = ET.Client.PlayerHelper.GetMyPlayerComponent(self.DomainScene());
            if (playerComponent.IsDebugMode)
            {
                await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgHall>();
            }
            else
            {
                await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgGameMode>();
            }
        }

        public static async ETTask ChgRoomMemberStatus(this DlgRoom self)
        {
            ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioClick(self.DomainScene());

            RoomComponent roomComponent = self.GetRoomComponent();
            long myPlayerId = PlayerHelper.GetMyPlayerId(self.DomainScene());
            if (myPlayerId == roomComponent.ownerRoomMemberId)
            {
                (bool bRet, string msg) = roomComponent.ChkOwnerStartGame();
                if (bRet == false)
                {
                    UIManagerHelper.ShowTip(self.DomainScene(), msg);
                    return;
                }
            }
            RoomMember myRoomMember = roomComponent.GetRoomMember(myPlayerId);
            bool isReady = myRoomMember.isReady;

            isReady = !isReady;
            await RoomHelper.ChgRoomMemberStatusAsync(self.ClientScene(), isReady);
        }

        public static async ETTask ChgRoomSeat(this DlgRoom self, int newSeat)
        {
            ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioClick(self.DomainScene());

            await RoomHelper.ChgRoomMemberSeatAsync(self.ClientScene(), newSeat);
        }

        public static async ETTask OnChooseBattleCfg(this DlgRoom self)
        {
            ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioClick(self.DomainScene());

            await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgBattleCfgChoose>(new DlgBattleCfgChoose_ShowWindowData()
            {
                isGlobalMode = false,
                isAR = false,
            });
        }

        public static async ETTask OnChgTeam(this DlgRoom self)
        {
            ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioClick(self.DomainScene());

            RoomComponent roomComponent = self.GetRoomComponent();
            long myPlayerId = PlayerHelper.GetMyPlayerId(self.DomainScene());
            RoomMember roomMember = roomComponent.GetRoomMember(myPlayerId);
            RoomTeamId roomTeamId = roomMember.roomTeamId;

            GamePlayBattleLevelCfg gamePlayBattleLevelCfg = GamePlayBattleLevelCfgCategory.Instance.Get(roomComponent.gamePlayBattleLevelCfgId);
            PlayerTeam playerTeam = gamePlayBattleLevelCfg.TeamMode as PlayerTeam;
            if (playerTeam.MaxTeamCount == 2)
            {
                if (roomTeamId == RoomTeamId.Green)
                {
                    roomTeamId = RoomTeamId.Red;
                }
                else if (roomTeamId == RoomTeamId.Red)
                {
                    roomTeamId = RoomTeamId.Green;
                }
            }
            else if (playerTeam.MaxTeamCount == 3)
            {
                if (roomTeamId == RoomTeamId.Green)
                {
                    roomTeamId = RoomTeamId.Red;
                }
                else if (roomTeamId == RoomTeamId.Red)
                {
                    roomTeamId = RoomTeamId.Blue;
                }
                else if (roomTeamId == RoomTeamId.Blue)
                {
                    roomTeamId = RoomTeamId.Green;
                }
            }

            await RoomHelper.ChgRoomMemberTeamAsync(self.ClientScene(), roomTeamId);
        }

    }
}