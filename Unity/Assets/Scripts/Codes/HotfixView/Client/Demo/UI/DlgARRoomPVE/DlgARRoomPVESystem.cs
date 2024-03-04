using System.Collections;
using System.Collections.Generic;
using System;
using System.ComponentModel;
using ET.AbilityConfig;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(DlgARRoomPVE))]
	public static class DlgARRoomPVESystem
	{
		public static void RegisterUIEvent(this DlgARRoomPVE self)
        {
            self.View.E_QuitRoomButton.AddListener(self.QuitRoom);
            self.View.E_ShowQrCodeButton.AddListenerAsync(self.ShowQrCode);
            self.View.E_ReScanButton.AddListenerAsync(self.ReScan);
            self.View.E_StartButton.AddListenerAsync(self.OnStartButton);

            // self.View.ELoopScrollList_RewardLoopHorizontalScrollRect.prefabSource.prefabName = "Item_Reward";
            // self.View.ELoopScrollList_RewardLoopHorizontalScrollRect.prefabSource.poolSize = 4;
            // self.View.ELoopScrollList_RewardLoopHorizontalScrollRect.AddItemRefreshListener((transform, i) =>
            //     self.AddRewardItemRefreshListener(transform, i));
            //
            // self.View.ELoopScrollList_MonsterLoopHorizontalScrollRect.prefabSource.prefabName = "Item_Monsters";
            // self.View.ELoopScrollList_MonsterLoopHorizontalScrollRect.prefabSource.poolSize = 4;
            // self.View.ELoopScrollList_MonsterLoopHorizontalScrollRect.AddItemRefreshListener((transform, i) => 
            //      self.AddMonsterItemRefreshListener(transform, i));
        }

        public static void ShowWindow(this DlgARRoomPVE self, ShowWindowData contextData = null)
        {
            UIAudioManagerHelper.PlayMusic(self.DomainScene(), MusicType.Main);
            //MirrorVerse.UI.MirrorSceneClassyUI.ClassyUI.Instance.HideMenu();
            
            self._ShowWindow().Coroutine();
        }

        public static async ETTask _ShowWindow(this DlgARRoomPVE self)
        {
            await self.GetRoomInfo();
            PlayerBaseInfoComponent playerBaseInfoComponent =
                    await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(self.DomainScene());
            int maxPhysicalStrength = GlobalSettingCfgCategory.Instance.UpperLimitOfPhysicalStrength;
            int curPhysicalStrength = playerBaseInfoComponent.GetPhysicalStrength();
            string msg = curPhysicalStrength + "/" + maxPhysicalStrength;
            self.View.ELabel_PhysicalStrengthNumTextMeshProUGUI.text = msg;
            self.View.ELabel_LevelUITextLocalizeMonoView.DynamicSet(self.level);
            self.ShowTipNodes().Coroutine();
        }
        
        public static async ETTask ShowTipNodes(this DlgARRoomPVE self)
        {
            PlayerBaseInfoComponent playerBaseInfoComponent = await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(self.DomainScene());

            if (playerBaseInfoComponent.ChallengeClearLevel + 1 < self.level)
            {
                string txt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_Tip_NoReward"); 
                UIManagerHelper.ShowTipNode(self.DomainScene(), txt);
            }
        }

        // public static async ETTask RefreshUI(this DlgARRoomPVE self)
        // {
        //     self.View.ELoopScrollList_MemberLoopHorizontalScrollRect.RefreshCells();
        //
        //     self.SetRoomMemberStatusText();
        // }

        public static async ETTask GetRoomInfo(this DlgARRoomPVE self)
        {
            PlayerStatusComponent playerStatusComponent = ET.Client.PlayerHelper.GetMyPlayerStatusComponent(self.DomainScene());
            long roomId = playerStatusComponent.RoomId;
            await RoomHelper.GetRoomInfoAsync(self.DomainScene(), roomId);

            RoomComponent roomComponent = self.GetRoomComponent();
            TowerDefense_ChallengeLevelCfg challengeLevelCfg =
                    TowerDefense_ChallengeLevelCfgCategory.Instance.Get(roomComponent.gamePlayBattleLevelCfgId);
            self.level = challengeLevelCfg.Index;

            // int count = roomComponent.roomMemberSeat.Count;
            // self.AddUIScrollItems(ref self.ScrollItemRoomMembers, count);
            // self.View.ELoopScrollList_MemberLoopHorizontalScrollRect.SetVisible(true, count);
        }

        public static RoomComponent GetRoomComponent(this DlgARRoomPVE self)
        {
            PlayerStatusComponent playerStatusComponent = ET.Client.PlayerHelper.GetMyPlayerStatusComponent(self.DomainScene());
            long roomId = playerStatusComponent.RoomId;
            RoomManagerComponent roomManagerComponent = ET.Client.RoomHelper.GetRoomManager(self.DomainScene());
            RoomComponent roomComponent = roomManagerComponent.GetRoom(roomId);
            return roomComponent;
        }

        public static async ETTask OnStartButton(this DlgARRoomPVE self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);
            
            RoomComponent roomComponent = self.GetRoomComponent();
            if (roomComponent == null)
            {
                return;
            }
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
            
            EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
            {
                eventName = "ReadyClicked",
            });
        }

        // public static async ETTask AddMemberItemRefreshListener(this DlgARRoomPVE self, Transform transform, int index)
        // {
        //     RoomComponent roomComponent = self.GetRoomComponent();
        //
        //     Scroll_Item_RoomMember itemRoom = self.ScrollItemRoomMembers[index].BindTrans(transform);
        //
        //     itemRoom.ELabel_Content_LvTextMeshProUGUI.gameObject.SetActive(false);
        //     //itemRoom.EButton_OperatorButton.SetVisible(true);
        //     itemRoom.EImage_TeamImage.SetVisible(false);
        //     long roomMemberId = roomComponent.roomMemberSeat[index];
        //     if (roomMemberId == -1)
        //     {
        //         itemRoom.uiTransform.SetVisible(false);
        //         itemRoom.ELabel_Content_NameTextMeshProUGUI.text =
        //             LocalizeComponent.Instance.GetTextValue("TextCode_Key_RoomMemberStatus_SeatIndex", index);
        //         itemRoom.ELabel_OperatorText.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_RoomMemberStatus_ChgSeat");
        //         itemRoom.ELabel_Content_LeaderImage.gameObject.SetActive(false);
        //         itemRoom.EButton_IconImage.gameObject.SetActive(false);
        //         itemRoom.EG_ReadyRectTransform.gameObject.SetActive(false);
        //     }
        //     else
        //     {
        //         itemRoom.uiTransform.SetVisible(true);
        //         long myPlayerId = PlayerHelper.GetMyPlayerId(self.DomainScene());
        //         RoomMember roomMember = roomComponent.GetRoomMember(roomMemberId);
        //
        //         itemRoom.EG_ReadyRectTransform.gameObject.SetActive(roomMember.isReady);
        //         if (roomComponent.ownerRoomMemberId == roomMemberId)
        //         {
        //             itemRoom.ELabel_Content_LeaderImage.gameObject.SetActive(true);
        //             //itemRoom.ELabel_Content_NameTextMeshProUGUI.text = $"{roomMemberId}";
        //         }
        //         else
        //         {
        //             itemRoom.ELabel_Content_LeaderImage.gameObject.SetActive(false);
        //             //itemRoom.ELabel_Content_NameTextMeshProUGUI.text = $"{roomMemberId}";
        //         }
        //
        //         if (myPlayerId == roomComponent.ownerRoomMemberId)
        //         {
        //             itemRoom.ELabel_OperatorText.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_RoomMemberStatus_KickMember");
        //         }
        //         else
        //         {
        //             itemRoom.ELabel_OperatorText.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_RoomMemberStatus_SeeMemberInfo");
        //             itemRoom.EButton_OperatorButton.AddListener(() => { });
        //         }
        //
        //         if (myPlayerId == roomMemberId)
        //         {
        //             itemRoom.EButton_OperatorButton.SetVisible(false);
        //             //itemRoom.ELabel_Content_NameTextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_RoomMemberStatus_Self");
        //         }
        //
        //         GamePlayBattleLevelCfg gamePlayBattleLevelCfg = GamePlayBattleLevelCfgCategory.Instance.Get(roomComponent.gamePlayBattleLevelCfgId);
        //         bool isShowTeamChgButton = gamePlayBattleLevelCfg.TeamMode is PlayerTeam;
        //         if (isShowTeamChgButton)
        //         {
        //             itemRoom.EImage_TeamImage.SetVisible(true);
        //             if (roomMember.roomTeamId == RoomTeamId.Green)
        //             {
        //                 itemRoom.EImage_TeamImage.color = Color.green;
        //             }
        //             else if (roomMember.roomTeamId == RoomTeamId.Red)
        //             {
        //                 itemRoom.EImage_TeamImage.color = Color.red;
        //             }
        //             else if (roomMember.roomTeamId == RoomTeamId.Blue)
        //             {
        //                 itemRoom.EImage_TeamImage.color = Color.blue;
        //             }
        //         }
        //         PlayerBaseInfoComponent playerBaseInfoComponent =
        //                 await ET.Client.PlayerCacheHelper.GetOtherPlayerBaseInfo(self.DomainScene(),roomMemberId);
        //         itemRoom.ELabel_Content_NameTextMeshProUGUI.text = playerBaseInfoComponent.GetPlayerName();
        //         itemRoom.EButton_IconImage.gameObject.SetActive(true);
        //         await itemRoom.EButton_IconImage.SetPlayerIcon(self.DomainScene(), roomMemberId);
        //     }
        // }

        public static async ETTask AddRewardItemRefreshListener(this DlgARRoomPVE self, Transform transform, int index)
        {
            
        }
        
        public static async ETTask AddMonsterItemRefreshListener(this DlgARRoomPVE self, Transform transform, int index)
        {
            
        }

        public static void QuitRoom(this DlgARRoomPVE self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Quit);

            string msgTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_LeaveTheRoom_Des");
            string sureTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_LeaveTheRoom_Confirm");
            string cancelTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_LeaveTheRoom_Cancel");
            string titleTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_LeaveTheRoom_Title");
            ET.Client.UIManagerHelper.ShowConfirm(self.DomainScene(), msgTxt, () => { self._QuitRoom().Coroutine(); }, null, sureTxt, cancelTxt,
                titleTxt);
        }

        public static async ETTask _QuitRoom(this DlgARRoomPVE self)
        {
            await RoomHelper.QuitRoomAsync(self.ClientScene());

            ET.Client.ARSessionHelper.ResetMainCamera(self.DomainScene(), false);

            await UIManagerHelper.ExitRoom(self.DomainScene());

            EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
            {
                eventName = "LeaveRoomClicked",
            });

        }

        public static async ETTask ShowQrCode(this DlgARRoomPVE self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

            ARSessionHelper.TriggerShowQrCode(self.DomainScene());

            EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
            {
                eventName = "InviteClicked",
            });

            await ETTask.CompletedTask;
        }

        public static async ETTask ReScan(this DlgARRoomPVE self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

            ARSessionHelper.TriggerReScan(self.DomainScene());

            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(self.DomainScene());
            _UIComponent.HideWindow<DlgARRoomPVE>();

            await ETTask.CompletedTask;
        }
	}
}
