﻿using System.Collections;
using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Linq;
using ET.AbilityConfig;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(DlgARRoomPVESeason))]
	public static class DlgARRoomPVESeasonSystem
	{
		public static void RegisterUIEvent(this DlgARRoomPVESeason self)
        {
            self.View.E_QuitRoomButton.AddListener(self.QuitRoom);
            self.View.E_ShowQrCodeButton.AddListenerAsync(self.ShowQrCode);
            self.View.E_ReScanButton.AddListenerAsync(self.ReScan);
            self.View.E_RoomMemberStatusButton.AddListenerAsync(self.ChgRoomMemberStatus);

            self.View.ELoopScrollList_RoomMemberLoopHorizontalScrollRect.prefabSource.prefabName = "Item_RoomMember";
            self.View.ELoopScrollList_RoomMemberLoopHorizontalScrollRect.prefabSource.poolSize = 4;
            self.View.ELoopScrollList_RoomMemberLoopHorizontalScrollRect.AddItemRefreshListener((transform, i) =>
            {
                self.AddMemberItemRefreshListener(transform, i).Coroutine();
                self.View.ELoopScrollList_RoomMemberLoopHorizontalScrollRect.SetSrcollMiddle();
            });

            self.View.ELoopScrollList_RewardLoopHorizontalScrollRect.prefabSource.prefabName = "Item_ItemShow";
            self.View.ELoopScrollList_RewardLoopHorizontalScrollRect.prefabSource.poolSize = 4;
            self.View.ELoopScrollList_RewardLoopHorizontalScrollRect.AddItemRefreshListener((transform, i) =>
            {
                self.AddRewardItemRefreshListener(transform, i).Coroutine();
                self.View.ELoopScrollList_RewardLoopHorizontalScrollRect.SetSrcollMiddle();
            });

            self.View.ELoopScrollList_MonsterLoopHorizontalScrollRect.prefabSource.prefabName = "Item_Monsters";
            self.View.ELoopScrollList_MonsterLoopHorizontalScrollRect.prefabSource.poolSize = 3;
            self.View.ELoopScrollList_MonsterLoopHorizontalScrollRect.AddItemRefreshListener((transform, i) =>
            {
                self.AddMonsterItemRefreshListener(transform, i).Coroutine();
                self.View.ELoopScrollList_MonsterLoopHorizontalScrollRect.SetSrcollMiddle();
            });
        }

        public static async ETTask ShowWindow(this DlgARRoomPVESeason self, ShowWindowData contextData = null)
        {
            UIAudioManagerHelper.PlayMusic(self.DomainScene(), MusicType.Main);

            ARSessionHelper.HideMirrorSceneUIMenu(self.DomainScene());

            self._ShowWindow().Coroutine();
        }

        public static async ETTask _ShowWindow(this DlgARRoomPVESeason self)
        {
            await self.GetRoomInfo();

            self.View.ELabel_LevelUITextLocalizeMonoView.DynamicSet(self.pveIndex);
            self.ShowTipNodes().Coroutine();
        }

        public static void HideWindow(this DlgARRoomPVESeason self)
        {

        }

        public static async ETTask ShowTipNodes(this DlgARRoomPVESeason self)
        {
            if (ET.SceneHelper.ChkIsGameModeArcade())
            {
                return;
            }
            int lastUnlockLevel = await self.GetLastUnLockPVELevel();

            if (lastUnlockLevel < self.pveIndex)
            {
                string txt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_Tip_NoReward");
                UIManagerHelper.ShowTipTopShow(self.DomainScene(), txt);
            }
        }

        public static async ETTask<bool> ChkRoomInfoChg(this DlgARRoomPVESeason self)
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

        public static async ETTask<bool> ChkARSceneIdChg(this DlgARRoomPVESeason self)
        {
            RoomComponent roomComponent = self.GetRoomComponent();
            string arSceneMeshId = ARSessionHelper.GetARSceneMeshId(self.DomainScene());
            if (roomComponent.arSceneMeshId != arSceneMeshId)
            {
                if (string.IsNullOrEmpty(roomComponent.arSceneMeshId) == false)
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

        public static async ETTask RefreshWhenRoomInfoChg(this DlgARRoomPVESeason self)
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

        public static async ETTask RefreshUI(this DlgARRoomPVESeason self)
        {
            RoomComponent roomComponent = self.GetRoomComponent();
            List<RoomMember> roomMemberList = roomComponent.GetRoomMemberList();
            int count = roomMemberList.Count;
            self.AddUIScrollItems(ref self.ScrollItemRoomMembers, count);
            self.View.ELoopScrollList_RoomMemberLoopHorizontalScrollRect.SetVisible(true, count);

            self.View.ELoopScrollList_RoomMemberLoopHorizontalScrollRect.RefreshCells();

            self.SetRoomMemberStatusText();

        }

        public static async ETTask RefreshWhenBaseInfoChg(this DlgARRoomPVESeason self)
        {
            await self.UpdatePhysical();
        }

        public static async ETTask UpdatePhysical(this DlgARRoomPVESeason self)
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
                int costValue = roomComponent.GetPhysicalCost();

                self.View.ELabel_TakePhysicalStrengthNumTextMeshProUGUI.ShowPhysicalCostText(self.DomainScene(), costValue).Coroutine();
            }
        }

        public static void SetRoomMemberStatusText(this DlgARRoomPVESeason self)
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

                //self.View.EButton_ChooseBattleCfgButton.gameObject.SetActive(true);
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

                //self.View.EButton_ChooseBattleCfgButton.gameObject.SetActive(false);
            }

            int memberCount = roomComponent.GetRoomMemberList().Count;
            //self.View.E_playerCountTextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Room_MemberCount", memberCount);
            GamePlayBattleLevelCfg gamePlayBattleLevelCfg = GamePlayBattleLevelCfgCategory.Instance.Get(roomComponent.roomTypeInfo.gamePlayBattleLevelCfgId);
            //self.View.ELabel_BattleCfgIdTextMeshProUGUI.text = gamePlayBattleLevelCfg.Name;

            bool isShowTeamChgButton = gamePlayBattleLevelCfg.TeamMode is PlayerTeam;
            //self.View.E_RoomMemberChgTeamButton.gameObject.SetActive(isShowTeamChgButton);
        }

        public static async ETTask<Dictionary<string, int>> GetAllDropItemDic(this DlgARRoomPVESeason self, int seasonCfgId, int pveLevel, PVELevelDifficulty pveLevelDifficulty)
        {
            PlayerSeasonInfoComponent playerSeasonInfoComponent =
                await ET.Client.PlayerCacheHelper.GetMyPlayerSeasonInfo(self.DomainScene());
            return playerSeasonInfoComponent.GetAllDropItemDic(seasonCfgId, pveLevel, pveLevelDifficulty, true);
        }

        public static async ETTask GetRoomInfo(this DlgARRoomPVESeason self)
        {
            self.seasonCfgId = ET.Client.SeasonHelper.GetSeasonCfgId(self.DomainScene());
            PlayerStatusComponent playerStatusComponent = ET.Client.PlayerStatusHelper.GetMyPlayerStatusComponent(self.DomainScene());
            long roomId = playerStatusComponent.RoomId;
            (bool roomExist, RoomComponent roomComponent) = await RoomHelper.GetRoomInfoAsync(self.DomainScene(), roomId);

            List<RoomMember> roomMemberList = roomComponent.GetRoomMemberList();
            int count = roomMemberList.Count;
            self.AddUIScrollItems(ref self.ScrollItemRoomMembers, count);
            self.View.ELoopScrollList_RoomMemberLoopHorizontalScrollRect.SetVisible(true, count);

            ChallengeLevelCfg challengeLevelCfg = SeasonChallengeLevelCfgCategory.Instance.GetChallenge(roomComponent.roomTypeInfo);
            self.pveIndex = roomComponent.roomTypeInfo.pveIndex;
            self.pveLevelDifficulty = roomComponent.roomTypeInfo.pveLevelDifficulty;
            self.isAR = roomComponent.roomTypeInfo.ChkIsAR();

            var allDropItemDic = await self.GetAllDropItemDic(roomComponent.roomTypeInfo.seasonCfgId, roomComponent.roomTypeInfo.pveIndex, roomComponent.roomTypeInfo.pveLevelDifficulty);
            self.itemList = new();
            foreach (var item in allDropItemDic)
            {
                self.itemList.Add((item.Key, item.Value));
            }

            self.AddUIScrollItems(ref self.ScrollItemReward, self.itemList.Count);
            self.View.ELoopScrollList_RewardLoopHorizontalScrollRect.SetVisible(true, self.itemList.Count);

            List<string> monsterList = challengeLevelCfg.MonsterListShow;
            self.AddUIScrollItems(ref self.ScrollItemMonsters, monsterList.Count);
            self.View.ELoopScrollList_MonsterLoopHorizontalScrollRect.SetVisible(true, monsterList.Count);
            self.SetRoomMemberStatusText();
        }

        public static RoomComponent GetRoomComponent(this DlgARRoomPVESeason self)
        {
            PlayerStatusComponent playerStatusComponent = ET.Client.PlayerStatusHelper.GetMyPlayerStatusComponent(self.DomainScene());
            long roomId = playerStatusComponent.RoomId;
            RoomManagerComponent roomManagerComponent = ET.Client.RoomHelper.GetRoomManager(self.DomainScene());
            RoomComponent roomComponent = roomManagerComponent.GetRoom(roomId);
            return roomComponent;
        }

        public static async ETTask ChgRoomMemberStatus(this DlgARRoomPVESeason self)
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

        public static async ETTask ChgRoomMemberStatus_Arcade(this DlgARRoomPVESeason self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);

            RoomComponent roomComponent = self.GetRoomComponent();
            if (roomComponent == null)
            {
                return;
            }
            long myPlayerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());

            int costValue = ET.GamePlayHelper.GetArcadeCoinCost(roomComponent.roomTypeInfo, false);
            bool bRet1 = await ET.Client.UIManagerHelper.ChkCoinEnoughOrShowtip(self.DomainScene(), costValue);
            if (bRet1 == false)
            {
                return;
            }

            if (myPlayerId == roomComponent.ownerRoomMemberId)
            {

                bool isLocked = await self.ChkIsLockPVELevel(self.pveIndex);

                if (isLocked)
                {
                    string txt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_ChallengeMode_Unlocked");
                    UIManagerHelper.ShowTip(self.DomainScene(), txt);
                    return;
                }

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

            EventSystem.Instance.Publish(self.DomainScene(), new ClientEventType.NoticeEventLogging()
            {
                eventName = "ReadyClicked",
            });
        }

        public static async ETTask ChgRoomMemberStatus_Normal(this DlgARRoomPVESeason self)
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
                bool isLocked = await self.ChkIsLockPVELevel(self.pveIndex);

                if (isLocked)
                {
                    string txt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_ChallengeMode_Unlocked");
                    UIManagerHelper.ShowTip(self.DomainScene(), txt);
                    return;
                }

                (bool bRet, string msg) = roomComponent.ChkOwnerStartGame();
                if (bRet == false)
                {
                    UIManagerHelper.ShowTip(self.DomainScene(), msg);
                    return;
                }
            }

            RoomMember myRoomMember = roomComponent.GetRoomMember(myPlayerId);
            bool isReady = myRoomMember.isReady;
            if (myPlayerId == roomComponent.ownerRoomMemberId || isReady == false)
            {
                int costValue = roomComponent.GetPhysicalCost();
                bool bRet = await ET.Client.UIManagerHelper.ChkPhsicalAndShowtip(self.DomainScene(), costValue);
                if (bRet == false)
                {
                    return;
                }
            }

            isReady = !isReady;
            await RoomHelper.ChgRoomMemberStatusAsync(self.ClientScene(), isReady);

            EventSystem.Instance.Publish(self.DomainScene(), new ClientEventType.NoticeEventLogging()
            {
                eventName = "ReadyClicked",
            });
        }

        public static async ETTask AddMemberItemRefreshListener(this DlgARRoomPVESeason self, Transform transform, int index)
        {
            RoomComponent roomComponent = self.GetRoomComponent();

            Scroll_Item_RoomMember itemRoom = self.ScrollItemRoomMembers[index].BindTrans(transform);

            itemRoom.InitItemRoom();


            List<RoomMember> roomMemberList = roomComponent.GetRoomMemberList();

            long roomMemberId = roomMemberList[index].Id;
            if (roomMemberId == -1)
            {
                itemRoom.uiTransform.SetVisible(false);

                await itemRoom.SetEmptyState(index);
            }
            else
            {
                itemRoom.uiTransform.SetVisible(true);

                await itemRoom.SetMemberState(roomComponent, roomMemberId);

                await itemRoom.SetAvatarFrame(roomMemberId);
            }
        }

        public static async ETTask AddRewardItemRefreshListener(this DlgARRoomPVESeason self, Transform transform, int index)
        {
            transform.name = $"Item_ItemShow_{index}";
            Scroll_Item_ItemShow itemTowerBuy = self.ScrollItemReward[index].BindTrans(transform);

            string itemCfgId = self.itemList[index].itemCfgId;
            int itemNum = self.itemList[index].itemNum;
            bool isPass = await self.ChkIsPassPVELevel(self.pveIndex, PVELevelDifficulty.Easy);
            await itemTowerBuy.Init(itemCfgId, true, itemNum, isPass);
        }

        public static async ETTask AddMonsterItemRefreshListener(this DlgARRoomPVESeason self, Transform transform, int index)
        {
            transform.name = $"Item_Monster_{index}";
            Scroll_Item_Monsters itemMonster = self.ScrollItemMonsters[index].BindTrans(transform);
            ChallengeLevelCfg challengeLevelCfg = SeasonChallengeLevelCfgCategory.Instance.GetChallengeByIndex(self.seasonCfgId, self.pveIndex, self.pveLevelDifficulty);
            List<string> monsterList = challengeLevelCfg.MonsterListShow;

            string itemCfgId = monsterList[index];
            itemMonster.ShowMonsterItem(itemCfgId, true).Coroutine();
        }

        public static void QuitRoom(this DlgARRoomPVESeason self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Quit);

            string msgTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_LeaveTheRoom_Des");
            string sureTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_LeaveTheRoom_Confirm");
            string cancelTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_LeaveTheRoom_Cancel");
            string titleTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_LeaveTheRoom_Title");
            ET.Client.UIManagerHelper.ShowConfirm(self.DomainScene(), msgTxt, () => { self._QuitRoom().Coroutine(); }, null, sureTxt, cancelTxt,
                titleTxt);
        }

        public static async ETTask _QuitRoom(this DlgARRoomPVESeason self)
        {
            RoomComponent roomComponent = self.GetRoomComponent();
            RoomTypeInfo roomTypeInfo = roomComponent.roomTypeInfo;
            await RoomHelper.QuitRoomAsync(self.ClientScene());

            ET.Client.ARSessionHelper.ResetMainCamera(self.DomainScene(), false);

            UIManagerHelper.GetUIComponent(self.DomainScene()).HideAllShownWindow();
            DlgChallengeMode_ShowWindowData showWindowData = new();
            showWindowData.pageIndex = 1;
            showWindowData.roomTypeInfo = roomTypeInfo;
            await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgChallengeMode>(showWindowData);

            EventSystem.Instance.Publish(self.DomainScene(), new ClientEventType.NoticeEventLogging()
            {
                eventName = "LeaveRoomClicked",
            });

        }

        public static async ETTask ShowQrCode(this DlgARRoomPVESeason self)
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

            EventSystem.Instance.Publish(self.DomainScene(), new ClientEventType.NoticeEventLogging()
            {
                eventName = "InviteClicked",
            });

            await ETTask.CompletedTask;
        }

        public static async ETTask ReScan(this DlgARRoomPVESeason self)
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

        public static async ETTask<bool> ChkIsLockPVELevel(this DlgARRoomPVESeason self, int pveLevel)
        {
            PlayerSeasonInfoComponent playerSeasonInfoComponent =
                await ET.Client.PlayerCacheHelper.GetMyPlayerSeasonInfo(self.DomainScene());

            return playerSeasonInfoComponent.ChkIsLockPVELevel(pveLevel);
        }

        public static async ETTask<bool> ChkIsPassPVELevel(this DlgARRoomPVESeason self, int pveLevel, PVELevelDifficulty pveLevelDifficulty)
        {
            PlayerSeasonInfoComponent playerSeasonInfoComponent =
                await ET.Client.PlayerCacheHelper.GetMyPlayerSeasonInfo(self.DomainScene());

            return playerSeasonInfoComponent.ChkIsPassPVELevel(pveLevel, pveLevelDifficulty);
        }

        public static async ETTask<int> GetLastUnLockPVELevel(this DlgARRoomPVESeason self)
        {
            PlayerSeasonInfoComponent playerSeasonInfoComponent =
                await ET.Client.PlayerCacheHelper.GetMyPlayerSeasonInfo(self.DomainScene());
            return playerSeasonInfoComponent.GetLastUnLockPVELevel();
        }
    }
}
