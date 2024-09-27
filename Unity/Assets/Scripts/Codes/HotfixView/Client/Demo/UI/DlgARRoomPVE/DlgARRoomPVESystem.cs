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
            self.View.E_RoomMemberStatusButton.AddListenerAsync(self.ChgRoomMemberStatus);

            self.View.ELoopScrollList_RoomMemberLoopHorizontalScrollRect.prefabSource.prefabName = "Item_RoomMember";
            self.View.ELoopScrollList_RoomMemberLoopHorizontalScrollRect.prefabSource.poolSize = 4;
            self.View.ELoopScrollList_RoomMemberLoopHorizontalScrollRect.AddItemRefreshListener((transform, i) =>
            {
                self.AddMemberItemRefreshListener(transform, i).Coroutine();
                self.View.ELoopScrollList_RoomMemberLoopHorizontalScrollRect.SetSrcollMiddle().Coroutine();
            });

            self.View.ELoopScrollList_RewardLoopHorizontalScrollRect.prefabSource.prefabName = "Item_TowerBuy";
            self.View.ELoopScrollList_RewardLoopHorizontalScrollRect.prefabSource.poolSize = 4;
            self.View.ELoopScrollList_RewardLoopHorizontalScrollRect.AddItemRefreshListener((transform, i) =>
            {
                self.AddRewardItemRefreshListener(transform, i).Coroutine();
                self.View.ELoopScrollList_RewardLoopHorizontalScrollRect.SetSrcollMiddle().Coroutine();
            });

            self.View.ELoopScrollList_MonsterLoopHorizontalScrollRect.prefabSource.prefabName = "Item_Monsters";
            self.View.ELoopScrollList_MonsterLoopHorizontalScrollRect.prefabSource.poolSize = 3;
            self.View.ELoopScrollList_MonsterLoopHorizontalScrollRect.AddItemRefreshListener((transform, i) =>
            {
                self.AddMonsterItemRefreshListener(transform, i).Coroutine();
                self.View.ELoopScrollList_MonsterLoopHorizontalScrollRect.SetSrcollMiddle().Coroutine();
            });
        }

        public static async ETTask ShowWindow(this DlgARRoomPVE self, ShowWindowData contextData = null)
        {
            UIAudioManagerHelper.PlayMusic(self.DomainScene(), MusicType.Main);

            MirrorVerse.UI.MirrorSceneClassyUI.ClassyUI.Instance?.HideMenu();

            self._ShowWindow().Coroutine();
        }

        public static async ETTask _ShowWindow(this DlgARRoomPVE self)
        {
            await self.GetRoomInfo();

            self.View.ELabel_LevelUITextLocalizeMonoView.DynamicSet(self.level);
            self.ShowTipNodes().Coroutine();
        }

        public static void HideWindow(this DlgARRoomPVE self)
        {

        }

        public static async ETTask ShowTipNodes(this DlgARRoomPVE self)
        {
            if (ET.SceneHelper.ChkIsGameModeArcade())
            {
                return;
            }
            int pveIndex = await self.GetCurPveIndex();

            if (pveIndex + 1 < self.level)
            {
                string txt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_Tip_NoReward");
                UIManagerHelper.ShowTipTopShow(self.DomainScene(), txt);
            }
        }

        public static async ETTask<bool> ChkRoomInfoChg(this DlgARRoomPVE self)
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

        public static async ETTask<bool> ChkARSceneIdChg(this DlgARRoomPVE self)
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

        public static async ETTask RefreshWhenRoomInfoChg(this DlgARRoomPVE self)
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

        public static async ETTask RefreshUI(this DlgARRoomPVE self)
        {

            RoomComponent roomComponent = self.GetRoomComponent();
            List<RoomMember> roomMemberList = roomComponent.GetRoomMemberList();
            int count = roomMemberList.Count;
            self.AddUIScrollItems(ref self.ScrollItemRoomMembers, count);
            self.View.ELoopScrollList_RoomMemberLoopHorizontalScrollRect.SetVisible(true, count);

            self.View.ELoopScrollList_RoomMemberLoopHorizontalScrollRect.RefreshCells();

            self.SetRoomMemberStatusText();

        }

        public static async ETTask RefreshWhenBaseInfoChg(this DlgARRoomPVE self)
        {
            await self.UpdatePhysical();
        }

        public static async ETTask UpdatePhysical(this DlgARRoomPVE self)
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

        public static void SetRoomMemberStatusText(this DlgARRoomPVE self)
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


        public static async ETTask GetRoomInfo(this DlgARRoomPVE self)
        {
            PlayerStatusComponent playerStatusComponent = ET.Client.PlayerStatusHelper.GetMyPlayerStatusComponent(self.DomainScene());
            long roomId = playerStatusComponent.RoomId;
            await RoomHelper.GetRoomInfoAsync(self.DomainScene(), roomId);

            RoomComponent roomComponent = self.GetRoomComponent();
            List<RoomMember> roomMemberList = roomComponent.GetRoomMemberList();
            int count = roomMemberList.Count;
            self.AddUIScrollItems(ref self.ScrollItemRoomMembers, count);
            self.View.ELoopScrollList_RoomMemberLoopHorizontalScrollRect.SetVisible(true, count);

            ChallengeLevelCfg challengeLevelCfg =
                TowerDefense_ChallengeLevelCfgCategory.Instance.GetChallengeByIndex(roomComponent.roomTypeInfo.pveIndex);
            self.level = roomComponent.roomTypeInfo.pveIndex;
            self.isAR = roomComponent.roomTypeInfo.ChkIsAR();

            int pveIndex = await self.GetCurPveIndex();

            self.itemList = new();
            if (pveIndex + 1 > self.level)
            {
                foreach (var item in challengeLevelCfg.RepeatRewardItemListShow)
                {
                    self.itemList.Add((item.Key, item.Value));
                }
            }
            else
            {
                foreach (var item in challengeLevelCfg.FirstRewardItemListShow)
                {
                    self.itemList.Add((item.Key, item.Value));
                }
            }

            self.AddUIScrollItems(ref self.ScrollItemReward, self.itemList.Count);
            self.View.ELoopScrollList_RewardLoopHorizontalScrollRect.SetVisible(true, self.itemList.Count);

            List<string> monsterList = challengeLevelCfg.MonsterListShow;
            self.AddUIScrollItems(ref self.ScrollItemMonsters, monsterList.Count);
            self.View.ELoopScrollList_MonsterLoopHorizontalScrollRect.SetVisible(true, monsterList.Count);
            self.SetRoomMemberStatusText();
        }

        public static RoomComponent GetRoomComponent(this DlgARRoomPVE self)
        {
            PlayerStatusComponent playerStatusComponent = ET.Client.PlayerStatusHelper.GetMyPlayerStatusComponent(self.DomainScene());
            long roomId = playerStatusComponent.RoomId;
            RoomManagerComponent roomManagerComponent = ET.Client.RoomHelper.GetRoomManager(self.DomainScene());
            RoomComponent roomComponent = roomManagerComponent.GetRoom(roomId);
            return roomComponent;
        }

        public static async ETTask ChgRoomMemberStatus(this DlgARRoomPVE self)
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

        public static async ETTask ChgRoomMemberStatus_Arcade(this DlgARRoomPVE self)
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
                int pveIndex = await self.GetCurPveIndex();

                if (pveIndex + 1 < self.level)
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

            EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
            {
                eventName = "ReadyClicked",
            });
        }

        public static async ETTask ChgRoomMemberStatus_Normal(this DlgARRoomPVE self)
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
                int pveIndex = await self.GetCurPveIndex();

                if (pveIndex + 1 < self.level)
                {
                    string txt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_ChallengeMode_Unlocked");
                    UIManagerHelper.ShowTip(self.DomainScene(), txt);
                    return;
                }

                int costValue = ET.GamePlayHelper.GetPhysicalCost(roomComponent.roomTypeInfo);
                bool bRet1 = await ET.Client.UIManagerHelper.ChkPhsicalAndShowtip(self.DomainScene(), costValue);
                if (bRet1 == false)
                {
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

            EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
            {
                eventName = "ReadyClicked",
            });
        }

        public static async ETTask AddMemberItemRefreshListener(this DlgARRoomPVE self, Transform transform, int index)
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

        public static async ETTask AddRewardItemRefreshListener(this DlgARRoomPVE self, Transform transform, int index)
        {
            transform.name = $"Item_TowerBuy_{index}";
            Scroll_Item_TowerBuy itemTowerBuy = self.ScrollItemReward[index].BindTrans(transform);

            string itemCfgId = self.itemList[index].itemCfgId;
            int itemNum = self.itemList[index].itemNum;
            await itemTowerBuy.ShowBagItem(itemCfgId, true, itemNum);
        }

        public static async ETTask AddMonsterItemRefreshListener(this DlgARRoomPVE self, Transform transform, int index)
        {
            transform.name = $"Item_Monster_{index}";
            Scroll_Item_Monsters itemMonster = self.ScrollItemMonsters[index].BindTrans(transform);
            ChallengeLevelCfg challengeLevelCfg =
                TowerDefense_ChallengeLevelCfgCategory.Instance.GetChallengeByIndex(self.level);
            List<string> monsterList = challengeLevelCfg.MonsterListShow;

            string itemCfgId = monsterList[index];
            itemMonster.ShowMonsterItem(itemCfgId, true).Coroutine();
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

            await UIManagerHelper.ExitRoomUI(self.DomainScene());

            EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
            {
                eventName = "LeaveRoomClicked",
            });

        }

        public static async ETTask ShowQrCode(this DlgARRoomPVE self)
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

        public static async ETTask ReScan(this DlgARRoomPVE self)
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

        public static async ETTask<int> GetCurPveIndex(this DlgARRoomPVE self)
        {
            PlayerBaseInfoComponent playerBaseInfoComponent =
                await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(self.DomainScene());
            int clearLevel = playerBaseInfoComponent.ChallengeClearLevel;
            return clearLevel;
        }
	}
}
