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
            self.View.E_ShowQrCodeButton.AddListenerAsync(self.ShowQrCode);
            self.View.E_RoomMemberStatusButton.AddListenerAsync(self.ChgRoomMemberStatus);
            self.View.EButton_ChooseBattleCfgButton.AddListenerAsync(self.OnChooseBattleCfg);
            self.View.EButton_ChgBattleDeckButton.AddListenerAsync(self.OnChgBattleDeck);
            self.View.EButton_ChgBattleSkillButton.AddListenerAsync(self.OnChgBattleSkill);
            self.View.E_RoomMemberChgTeamButton.AddListenerAsync(self.OnChgTeam);

            self.View.ELoopScrollList_MemberLoopHorizontalScrollRect.prefabSource.prefabName = "Item_RoomMember";
            self.View.ELoopScrollList_MemberLoopHorizontalScrollRect.prefabSource.poolSize = 8;
            self.View.ELoopScrollList_MemberLoopHorizontalScrollRect.AddItemRefreshListener(async(transform, i) =>
                await self.AddMemberItemRefreshListener(transform, i));
        }

        public static async ETTask ShowWindow(this DlgRoom self, ShowWindowData contextData = null)
        {
            UIAudioManagerHelper.PlayMusic(self.DomainScene(), MusicType.Main);
            self.View.EG_ChooseBattleCfgRectTransform.gameObject.SetActive(false);
            self.View.E_RoomMemberChgTeamButton.gameObject.SetActive(false);

            self.GetRoomInfo().Coroutine();
        }

        public static async ETTask<bool> ChkRoomInfoChg(this DlgRoom self)
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

        public static async ETTask<bool> ChkARSceneIdChg(this DlgRoom self)
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

        public static async ETTask RefreshWhenRoomInfoChg(this DlgRoom self)
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

        public static async ETTask RefreshUI(this DlgRoom self)
        {
            RoomComponent roomComponent = self.GetRoomComponent();

            int count = roomComponent.roomMemberSeat.Count;
            self.AddUIScrollItems(ref self.ScrollItemRoomMembers, count);
            self.View.ELoopScrollList_MemberLoopHorizontalScrollRect.SetVisible(true, count);

            self.View.ELoopScrollList_MemberLoopHorizontalScrollRect.RefreshCells();

            self.SetRoomMemberStatusText();
        }

        public static async ETTask RefreshWhenBaseInfoChg(this DlgRoom self)
        {
            await self.UpdatePhysical();
        }

        public static async ETTask UpdatePhysical(this DlgRoom self)
        {
            RoomComponent roomComponent = self.GetRoomComponent();

            if (roomComponent == null)
            {
                return;
            }

            int costValue = 0;
            long myPlayerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());

            if (myPlayerId == roomComponent.ownerRoomMemberId)
            {
                costValue = -99;
            }
            else
            {
                costValue = 0;
            }

            self.View.ELabel_TakePhysicalStrengthNumTextMeshProUGUI.ShowPhysicalCostText(self.DomainScene(), costValue).Coroutine();
        }

        public static async ETTask ShowBattleCfgChoose(this DlgRoom self)
        {
            if (DebugConnectComponent.Instance.IsDebugMode)
            {
                self.View.EG_ChooseBattleCfgRectTransform.gameObject.SetActive(true);
                self.View.EG_ChgBattleDeckRectTransform.gameObject.SetActive(true);
                self.View.EG_ChgBattleSkillRectTransform.gameObject.SetActive(true);
            }
            else
            {
                self.View.EG_ChooseBattleCfgRectTransform.gameObject.SetActive(false);
                self.View.EG_ChgBattleDeckRectTransform.gameObject.SetActive(false);
                self.View.EG_ChgBattleSkillRectTransform.gameObject.SetActive(false);
            }

            RoomComponent roomComponent = self.GetRoomComponent();
            if (roomComponent != null)
            {
                if (roomComponent.IsARRoom())
                {
                    self.View.E_ShowQrCodeButton.SetVisible(true);
                    long myPlayerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());
                    if (myPlayerId == roomComponent.ownerRoomMemberId)
                    {
                        self.View.E_ReScanButton.SetVisible(true);
                    }
                    else
                    {
                        self.View.E_ReScanButton.SetVisible(false);
                    }
                    self.View.E_BGImage.SetVisible(false);
                    self.View.E_BGARTranslucentImage.SetVisible(true);
                }
                else
                {
                    self.View.E_ShowQrCodeButton.SetVisible(false);
                    self.View.E_ReScanButton.SetVisible(false);
                    self.View.E_BGImage.SetVisible(true);
                    self.View.E_BGARTranslucentImage.SetVisible(false);
                }
            }

            await ETTask.CompletedTask;
        }

        public static async ETTask RefreshBattleCfgIdChoose(this DlgRoom self, string gamePlayBattleLevelCfgId)
        {
            RoomComponent roomComponent = self.GetRoomComponent();

            if (roomComponent == null)
            {
                return;
            }

            roomComponent.roomTypeInfo.gamePlayBattleLevelCfgId = gamePlayBattleLevelCfgId;

            await RoomHelper.ChgRoomBattleLevelCfgAsync(self.ClientScene(), roomComponent.roomTypeInfo);
        }

        public static void SetRoomMemberStatusText(this DlgRoom self)
        {
            RoomComponent roomComponent = self.GetRoomComponent();

            if (roomComponent == null)
            {
                return;
            }

            long myPlayerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());

            if (myPlayerId == roomComponent.ownerRoomMemberId)
            {
                self.View.ELable_RoomMemberStatusTextMeshProUGUI.text =
                        LocalizeComponent.Instance.GetTextValue("TextCode_Key_RoomMemberStatus_StartGame");
                if (roomComponent.ChkIsAllReady())
                {
                    self.View.E_RoomMemberStatusImage.SetImageGray(false);
                }
                else
                {
                    self.View.E_RoomMemberStatusImage.SetImageGray(true);
                }

                self.View.EButton_ChooseBattleCfgButton.gameObject.SetActive(true);
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

                self.View.EButton_ChooseBattleCfgButton.gameObject.SetActive(false);
            }

            int memberCount = roomComponent.GetRoomMemberList().Count;
            self.View.E_playerCountTextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Room_MemberCount", memberCount);

            GamePlayBattleLevelCfg gamePlayBattleLevelCfg = GamePlayBattleLevelCfgCategory.Instance.Get(roomComponent.roomTypeInfo.gamePlayBattleLevelCfgId);
            self.View.ELabel_BattleCfgIdTextMeshProUGUI.text = gamePlayBattleLevelCfg.Name;

            bool isShowTeamChgButton = gamePlayBattleLevelCfg.TeamMode is PlayerTeam;
            self.View.E_RoomMemberChgTeamButton.gameObject.SetActive(isShowTeamChgButton);
        }

        public static async ETTask GetRoomInfo(this DlgRoom self)
        {
            PlayerStatusComponent playerStatusComponent = ET.Client.PlayerStatusHelper.GetMyPlayerStatusComponent(self.DomainScene());
            long roomId = playerStatusComponent.RoomId;
            (bool roomExist, RoomComponent roomComponent) = await RoomHelper.GetRoomInfoAsync(self.DomainScene(), roomId);

            int count = roomComponent.roomMemberSeat.Count;
            self.AddUIScrollItems(ref self.ScrollItemRoomMembers, count);
            self.View.ELoopScrollList_MemberLoopHorizontalScrollRect.SetVisible(true, count);

            self.View.ELabel_RoomIdText.text = $"{roomId.ToString()} {roomComponent.roomStatus.ToString()}";
            self.SetRoomMemberStatusText();

            await self.ShowBattleCfgChoose();
        }

        public static RoomComponent GetRoomComponent(this DlgRoom self)
        {
            PlayerStatusComponent playerStatusComponent = ET.Client.PlayerStatusHelper.GetMyPlayerStatusComponent(self.DomainScene());
            long roomId = playerStatusComponent.RoomId;
            RoomManagerComponent roomManagerComponent = ET.Client.RoomHelper.GetRoomManager(self.DomainScene());
            RoomComponent roomComponent = roomManagerComponent.GetRoom(roomId);
            return roomComponent;
        }

        public static async ETTask AddMemberItemRefreshListener(this DlgRoom self, Transform transform, int index)
        {
            // 获取房间组件
            RoomComponent roomComponent = self.GetRoomComponent();

            // 绑定成员项到对应的Transform
            Scroll_Item_RoomMember itemRoom = self.ScrollItemRoomMembers[index].BindTrans(transform);

            // 初始化成员项的UI状态
            itemRoom.EButton_OperatorButton.SetVisible(true);
            itemRoom.InitItemRoom();

            // 获取房间成员ID
            long roomMemberId = roomComponent.roomMemberSeat[index];

            // 如果没有成员
            if (roomMemberId == -1)
            {
                itemRoom.uiTransform.SetVisible(true);
                await itemRoom.SetEmptyState(index);
            }
            // 如果有成员
            else
            {
                itemRoom.uiTransform.SetVisible(true);
                await itemRoom.SetMemberState(roomComponent, roomMemberId);
                await itemRoom.SetAvatarFrame(roomMemberId);
            }
        }

        public static async ETTask QuitRoom(this DlgRoom self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Quit);

            string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_IsQuitRoom");
            ET.Client.UIManagerHelper.ShowConfirm(self.DomainScene(), msg, () => { self._QuitRoom().Coroutine(); }, null);
        }

        public static async ETTask _QuitRoom(this DlgRoom self)
        {
            await RoomHelper.QuitRoomAsync(self.ClientScene());
            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgRoom>();
            await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgHall>();

            EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
            {
                eventName = "LeaveRoomClicked",
            });
        }

        public static async ETTask ShowQrCode(this DlgRoom self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

            UIManagerHelper.HideUIRedDot(self.DomainScene(), UIRedDotType.MultPlayers).Coroutine();

            ARSessionHelper.TriggerShowQrCode(self.DomainScene());

            EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
            {
                eventName = "InviteClicked",
            });
            await ETTask.CompletedTask;
        }

        public static async ETTask ChgRoomMemberStatus(this DlgRoom self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);

            RoomComponent roomComponent = self.GetRoomComponent();
            if (roomComponent == null)
            {
                return;
            }
            long myPlayerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());
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


        public static async ETTask OnChooseBattleCfg(this DlgRoom self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

            RoomComponent roomComponent = self.GetRoomComponent();
            bool isAR = roomComponent.IsARRoom();
            Log.Debug($"OnChooseBattleCfg isAR[{isAR}]");
            await UIManagerHelper.GetUIComponent(self.DomainScene())
                .ShowWindowAsync<DlgBattleCfgChoose>(new DlgBattleCfgChoose_ShowWindowData() { isGlobalMode = false, isAR = isAR, });
        }

        public static async ETTask OnChgBattleDeck(this DlgRoom self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

            await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgBattleDeck>();
        }

        public static async ETTask OnChgBattleSkill(this DlgRoom self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

            await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgCameraPlayerSkill>();
        }

        public static async ETTask OnChgTeam(this DlgRoom self)
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