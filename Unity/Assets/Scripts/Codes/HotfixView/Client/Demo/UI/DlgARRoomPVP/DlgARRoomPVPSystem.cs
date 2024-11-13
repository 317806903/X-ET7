using System.Collections;
using System.Collections.Generic;
using System;
using ET.AbilityConfig;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [FriendOf(typeof (DlgARRoomPVP))]
    public static class DlgARRoomPVPSystem
    {
        public static void RegisterUIEvent(this DlgARRoomPVP self)
        {
            self.View.E_QuitRoomButton.AddListenerAsync(self.QuitRoom);
            self.View.E_ShowQrCodeButton.AddListenerAsync(self.ShowQrCode);
            self.View.E_ReScanButton.AddListenerAsync(self.ReScan);
            self.View.E_RoomMemberStatusButton.AddListenerAsync(self.ChgRoomMemberStatus);
            self.View.E_RoomMemberChgTeamButton.AddListenerAsync(self.OnChgTeam);

            self.View.ELoopScrollList_Member_LeftLoopHorizontalScrollRect.prefabSource.prefabName = "Item_RoomMember";
            self.View.ELoopScrollList_Member_LeftLoopHorizontalScrollRect.prefabSource.poolSize = 4;
            self.View.ELoopScrollList_Member_LeftLoopHorizontalScrollRect.AddItemRefreshListener((transform, i) =>
            {
                self.AddMemberItemRefreshListener(transform, i, true).Coroutine();
                self.View.ELoopScrollList_Member_LeftLoopHorizontalScrollRect.SetSrcollMiddle();
            });

            self.View.ELoopScrollList_Member_RightLoopHorizontalScrollRect.prefabSource.prefabName = "Item_RoomMember";
            self.View.ELoopScrollList_Member_RightLoopHorizontalScrollRect.prefabSource.poolSize = 4;
            self.View.ELoopScrollList_Member_RightLoopHorizontalScrollRect.AddItemRefreshListener((transform, i) =>
            {
                self.AddMemberItemRefreshListener(transform, i, false).Coroutine();
                self.View.ELoopScrollList_Member_RightLoopHorizontalScrollRect.SetSrcollMiddle();
            });
        }

        public static async ETTask ShowWindow(this DlgARRoomPVP self, ShowWindowData contextData = null)
        {
            UIAudioManagerHelper.PlayMusic(self.DomainScene(), MusicType.Main);
            self.View.E_RoomMemberChgTeamButton.gameObject.SetActive(false);

            MirrorVerse.UI.MirrorSceneClassyUI.ClassyUI.Instance?.HideMenu();
            self.GetRoomInfo().Coroutine();
        }

        public static async ETTask<bool> ChkRoomInfoChg(this DlgARRoomPVP self)
        {
            RoomComponent roomComponent = self.GetRoomComponent();
            PlayerStatusComponent playerStatusComponent = ET.Client.PlayerStatusHelper.GetMyPlayerStatusComponent(self.DomainScene());
            PlayerStatus playerStatus = playerStatusComponent.PlayerStatus;
            RoomType roomType = playerStatusComponent.RoomTypeInfo.roomType;
            SubRoomType subRoomType = playerStatusComponent.RoomTypeInfo.subRoomType;

            if (playerStatus == PlayerStatus.Room)
            {
                return false;
            }
            else if (playerStatus == PlayerStatus.Battle)
            {
                return false;
            }
            else if (playerStatus == PlayerStatus.Hall)
            {
                return true;
            }

            if (roomType != roomComponent.roomTypeInfo.roomType)
            {
                return true;
            }
            if (subRoomType != roomComponent.roomTypeInfo.subRoomType)
            {
                return true;
            }

            return false;
        }

        public static async ETTask<bool> ChkARSceneIdChg(this DlgARRoomPVP self)
        {
            RoomComponent roomComponent = self.GetRoomComponent();
            string arSceneId = ARSessionHelper.GetARSceneId(self.DomainScene());
            if (roomComponent.arSceneId != arSceneId)
            {
                if (string.IsNullOrEmpty(roomComponent.arSceneId) == false)
                {
                    UIManagerHelper.GetUIComponent(self.DomainScene()).HideAllShownWindow();

                    DlgARHall_ShowWindowData _DlgARHall_ShowWindowData = new()
                    {
                        ARHallType = ARHallType.JoinTheRoom,
                        roomId = roomComponent.Id,
                        roomTypeInfo = roomComponent.roomTypeInfo,
                    };
                    UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgARHall>(_DlgARHall_ShowWindowData).Coroutine();
                    return true;
                }
            }

            return false;
        }

        public static async ETTask RefreshWhenRoomInfoChg(this DlgARRoomPVP self)
        {
            if (await self.ChkRoomInfoChg())
            {
                await RoomHelper.QuitRoomAsync(self.ClientScene());
                ET.Client.ARSessionHelper.ResetMainCamera(self.DomainScene(), false);
                await UIManagerHelper.ExitRoomUI(self.DomainScene());
                return;
            }

            if (await self.ChkARSceneIdChg())
            {
                return;
            }

            await self.RefreshUI();
        }

        public static async ETTask RefreshUI(this DlgARRoomPVP self)
        {
            self.ResetRoomMembers();

            self.View.ELoopScrollList_Member_LeftLoopHorizontalScrollRect.RefreshCells();
            self.View.ELoopScrollList_Member_RightLoopHorizontalScrollRect.RefreshCells();

            self.SetRoomMemberStatusText();
        }

        public static async ETTask RefreshWhenBaseInfoChg(this DlgARRoomPVP self)
        {
            await self.UpdatePhysical();
        }

        public static async ETTask UpdatePhysical(this DlgARRoomPVP self)
        {
            RoomComponent roomComponent = self.GetRoomComponent();

            if (roomComponent == null)
            {
                return;
            }

            if (ET.SceneHelper.ChkIsGameModeArcade())
            {
                int costValue = ET.GamePlayHelper.GetArcadeCoinCost(roomComponent.roomTypeInfo, false);
                self.View.ELabel_TakePhysicalStrengthNumTextMeshProUGUI.ShowTokenArcadeCoinCostText(self.DomainScene(), costValue).Coroutine();
            }
            else
            {
                int costValue = 0;
                long myPlayerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());

                if (myPlayerId == roomComponent.ownerRoomMemberId)
                {
                    costValue = ET.GamePlayHelper.GetPhysicalCost(roomComponent.roomTypeInfo);
                }
                else
                {
                    costValue = 0;
                }

                self.View.ELabel_TakePhysicalStrengthNumTextMeshProUGUI.ShowPhysicalCostText(self.DomainScene(), costValue).Coroutine();
            }
        }

        public static async ETTask ShowBattleCfgChoose(this DlgARRoomPVP self)
        {
            RoomComponent roomComponent = self.GetRoomComponent();
            if (roomComponent != null)
            {
                if (roomComponent.IsARRoom())
                {
                    self.View.E_ShowQrCodeButton.SetVisible(true);

                    self.View.E_BGImage.SetVisible(false);
                    self.View.E_BGARTranslucentImage.SetVisible(true);
                }
                else
                {
                    self.View.E_ShowQrCodeButton.SetVisible(false);
                    self.View.E_BGImage.SetVisible(true);
                    self.View.E_BGARTranslucentImage.SetVisible(false);
                }
            }

            await ETTask.CompletedTask;
        }

        public static void SetRoomMemberStatusText(this DlgARRoomPVP self)
        {
            RoomComponent roomComponent = self.GetRoomComponent();

            if (roomComponent == null)
            {
                return;
            }

            long myPlayerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());

            if (ET.SceneHelper.ChkIsGameModeArcade())
            {
                self.View.E_ReScanButton.SetVisible(false);
            }
            else
            {
                if (myPlayerId == roomComponent.ownerRoomMemberId)
                {
                    self.View.E_ReScanButton.SetVisible(true);
                }
                else
                {
                    self.View.E_ReScanButton.SetVisible(false);
                }
            }

            self.UpdatePhysical().Coroutine();

            if (myPlayerId == roomComponent.ownerRoomMemberId)
            {
                if (roomComponent.ChkIsAllReady())
                {
                    self.View.E_RoomMemberStatusImage.SetImageGray(false);
                    self.View.ELable_RoomMemberStatusTextMeshProUGUI.text =
                        LocalizeComponent.Instance.GetTextValue("TextCode_Key_RoomMemberStatus_StartGame");
                }
                else
                {
                    self.View.E_RoomMemberStatusImage.SetImageGray(true);
                    self.View.ELable_RoomMemberStatusTextMeshProUGUI.text =
                        LocalizeComponent.Instance.GetTextValue("TextCode_Key_RoomMemberStatus_WaitAllReady");
                }

            }
            else
            {
                RoomMember roomMember = roomComponent.GetRoomMember(myPlayerId);
                bool isReady = roomMember.isReady;

                if (isReady)
                {
                    self.View.E_RoomMemberStatusImage.SetImageGray(true);
                    self.View.ELable_RoomMemberStatusTextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_RoomMemberStatus_CancelReady");
                }
                else
                {
                    self.View.E_RoomMemberStatusImage.SetImageGray(false);
                    self.View.ELable_RoomMemberStatusTextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_RoomMemberStatus_Ready");
                }

            }

            int memberCountLeft = self.roomMembersLeft.Count;
            int memberCountRight = self.roomMembersRight.Count;
            self.View.E_PlayerCountVsTextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Room_MemberCountVs", memberCountLeft, memberCountRight);

            self.View.E_RoomMemberChgTeamButton.gameObject.SetActive(true);
        }

        public static async ETTask GetRoomInfo(this DlgARRoomPVP self)
        {
            PlayerStatusComponent playerStatusComponent = ET.Client.PlayerStatusHelper.GetMyPlayerStatusComponent(self.DomainScene());
            long roomId = playerStatusComponent.RoomId;
            (bool roomExist, RoomComponent roomComponent) = await RoomHelper.GetRoomInfoAsync(self.DomainScene(), roomId);

            self.ResetRoomMembers();

            int count = 3;
            self.AddUIScrollItems(ref self.ScrollItemRoomMembersLeft, count);
            self.View.ELoopScrollList_Member_LeftLoopHorizontalScrollRect.SetVisible(true, count);

            count = 3;
            self.AddUIScrollItems(ref self.ScrollItemRoomMembersRight, count);
            self.View.ELoopScrollList_Member_RightLoopHorizontalScrollRect.SetVisible(true, count);

            self.View.ELabel_RoomIdText.text = $"{roomId.ToString()} {roomComponent.roomStatus.ToString()}";
            self.SetRoomMemberStatusText();
            await self.ShowBattleCfgChoose();
        }

        public static RoomComponent GetRoomComponent(this DlgARRoomPVP self)
        {
            PlayerStatusComponent playerStatusComponent = ET.Client.PlayerStatusHelper.GetMyPlayerStatusComponent(self.DomainScene());
            long roomId = playerStatusComponent.RoomId;
            RoomManagerComponent roomManagerComponent = ET.Client.RoomHelper.GetRoomManager(self.DomainScene());
            RoomComponent roomComponent = roomManagerComponent.GetRoom(roomId);
            return roomComponent;
        }

        public static void ResetRoomMembers(this DlgARRoomPVP self)
        {
            self.roomMembersLeft.Clear();
            self.roomMembersRight.Clear();
            PlayerStatusComponent playerStatusComponent = ET.Client.PlayerStatusHelper.GetMyPlayerStatusComponent(self.DomainScene());
            long roomId = playerStatusComponent.RoomId;
            RoomManagerComponent roomManagerComponent = ET.Client.RoomHelper.GetRoomManager(self.DomainScene());
            RoomComponent roomComponent = roomManagerComponent.GetRoom(roomId);
            for (int i = 0; i < roomComponent.roomMemberSeat.Count; i++)
            {
                long roomMemberId = roomComponent.roomMemberSeat[i];
                if (roomMemberId != -1)
                {
                    RoomMember roomMember = roomComponent.GetRoomMember(roomMemberId);
                    if (roomMember.roomTeamId == RoomTeamId.Green)
                    {
                        self.roomMembersLeft.Add(i);
                    }
                    else if (roomMember.roomTeamId == RoomTeamId.Red)
                    {
                        self.roomMembersRight.Add(i);
                    }
                }
            }
        }

        public static async ETTask AddMemberItemRefreshListener(this DlgARRoomPVP self, Transform transform, int index, bool isLeft)
        {
            RoomComponent roomComponent = self.GetRoomComponent();

            Scroll_Item_RoomMember itemRoom;
            if (isLeft)
            {
                itemRoom = self.ScrollItemRoomMembersLeft[index].BindTrans(transform);
            }
            else
            {
                itemRoom = self.ScrollItemRoomMembersRight[index].BindTrans(transform);
            }

            // 初始化成员项的UI状态
            itemRoom.EButton_OperatorButton.SetVisible(true);
            itemRoom.InitItemRoom();

            long roomMemberId = -1;
            if (isLeft)
            {
                if (self.roomMembersLeft.Count <= index)
                {
                    roomMemberId = -1;
                }
                else
                {
                    index = self.roomMembersLeft[index];
                    roomMemberId = roomComponent.roomMemberSeat[index];
                }
            }
            else
            {
                if (self.roomMembersRight.Count <= index)
                {
                    roomMemberId = -1;
                }
                else
                {
                    index = self.roomMembersRight[index];
                    roomMemberId = roomComponent.roomMemberSeat[index];
                }
            }
            if (roomMemberId == -1)
            {
                itemRoom.EButton_OperatorButton.SetVisible(false);
                itemRoom.SetEmptyState(index).Coroutine();
            }
            else
            {
                await itemRoom.SetMemberState(roomComponent, index);
            }
        }

        public static async ETTask QuitRoom(this DlgARRoomPVP self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(),SoundEffectType.Back);

            string msgTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_LeaveTheRoom_Des");
            string sureTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_LeaveTheRoom_Confirm");
            string cancelTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_LeaveTheRoom_Cancel");
            string titleTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_LeaveTheRoom_Title");
            ET.Client.UIManagerHelper.ShowConfirm(self.DomainScene(), msgTxt, () => { self._QuitRoom().Coroutine(); }, null, sureTxt, cancelTxt,
                titleTxt);
        }

        public static async ETTask _QuitRoom(this DlgARRoomPVP self)
        {
            await RoomHelper.QuitRoomAsync(self.ClientScene());

            await UIManagerHelper.ExitRoomUI(self.DomainScene());

            EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
            {
                eventName = "LeaveRoomClicked",
            });

        }

        public static async ETTask ShowQrCode(this DlgARRoomPVP self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

            UIManagerHelper.HideUIRedDot(self.DomainScene(), UIRedDotType.MultPlayers).Coroutine();

            bool isFull = self.GetRoomComponent().ChkRoomMemberIsFull();
            if (isFull)
            {
                string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Hall_RoomMemberFull");
                ET.Client.UIManagerHelper.ShowOnlyConfirm(self.DomainScene(), msg, () =>
                {
                });
                return;
            }

            ARSessionHelper.TriggerShowQrCode(self.DomainScene());

            EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
            {
                eventName = "InviteClicked",
            });

            await ETTask.CompletedTask;
        }

        public static async ETTask ReScan(this DlgARRoomPVP self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

            UIManagerHelper.GetUIComponent(self.DomainScene()).HideAllShownWindow();
            RoomComponent roomComponent = self.GetRoomComponent();
            DlgARHall_ShowWindowData _DlgARHall_ShowWindowData = new()
            {
                ARHallType = ARHallType.KeepRoomAndRescan,
                roomId = roomComponent.Id,
                roomTypeInfo = roomComponent.roomTypeInfo,
            };
            UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgARHall>(_DlgARHall_ShowWindowData).Coroutine();

            ARSessionHelper.TriggerReScan(self.DomainScene());

            await ETTask.CompletedTask;
        }

        public static async ETTask ChgRoomMemberStatus(this DlgARRoomPVP self)
        {
            if (ET.SceneHelper.ChkIsGameModeArcade())
            {
                await self.ChgRoomMemberStatus_Arcade();
            }
            else
            {
                await self.ChgRoomMemberStatus_Normal();
            }
        }

        public static async ETTask ChgRoomMemberStatus_Arcade(this DlgARRoomPVP self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);

            RoomComponent roomComponent = self.GetRoomComponent();
            long myPlayerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());

            int costValue = ET.GamePlayHelper.GetArcadeCoinCost(roomComponent.roomTypeInfo, false);
            bool bRet1 = await ET.Client.UIManagerHelper.ChkCoinEnoughOrShowtip(self.DomainScene(), costValue);
            if (bRet1 == false)
            {
                return;
            }

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

            EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
            {
                eventName = "ReadyClicked",
            });

        }

        public static async ETTask ChgRoomMemberStatus_Normal(this DlgARRoomPVP self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);

            RoomComponent roomComponent = self.GetRoomComponent();
            long myPlayerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());
            if (myPlayerId == roomComponent.ownerRoomMemberId)
            {
                (bool bRet, string msg) = roomComponent.ChkOwnerStartGame();
                if (bRet == false)
                {
                    UIManagerHelper.ShowTip(self.DomainScene(), msg);
                    return;
                }

                int costValue = ET.GamePlayHelper.GetPhysicalCost(roomComponent.roomTypeInfo);
                bool bRet1 = await ET.Client.UIManagerHelper.ChkPhsicalAndShowtip(self.DomainScene(), costValue);
                if (bRet1 == false)
                {
                    return;
                }
            }

            RoomMember myRoomMember = roomComponent.GetRoomMember(myPlayerId);
            bool isReady = myRoomMember.isReady;

            isReady = !isReady;
            await RoomHelper.ChgRoomMemberStatusAsync(self.ClientScene(), isReady);

            EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
            {
                eventName = "ReadyClicked",
            });

        }

        public static async ETTask OnChooseBattleCfg(this DlgARRoomPVP self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

            await UIManagerHelper.GetUIComponent(self.DomainScene())
                .ShowWindowAsync<DlgBattleCfgChoose>(new DlgBattleCfgChoose_ShowWindowData() { isGlobalMode = false, isAR = true, });
        }

        public static async ETTask OnChgTeam(this DlgARRoomPVP self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

            RoomComponent roomComponent = self.GetRoomComponent();
            long myPlayerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());
            RoomMember roomMember = roomComponent.GetRoomMember(myPlayerId);
            RoomTeamId roomTeamId = roomMember.roomTeamId;

            GamePlayBattleLevelCfg gamePlayBattleLevelCfg = GamePlayBattleLevelCfgCategory.Instance.Get(roomComponent.roomTypeInfo.gamePlayBattleLevelCfgId);
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
            else if (playerTeam.MaxTeamCount >= 4)
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
                else if (roomTeamId == RoomTeamId.Green)
                {
                    roomTeamId = RoomTeamId.Yellow;
                }
            }

            await RoomHelper.ChgRoomMemberTeamAsync(self.ClientScene(), roomTeamId);
        }
    }
}