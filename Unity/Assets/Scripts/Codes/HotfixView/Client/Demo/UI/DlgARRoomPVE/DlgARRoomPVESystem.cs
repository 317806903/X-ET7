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
                    self.AddMemberItemRefreshListener(transform, i));

            self.View.ELoopScrollList_RewardLoopHorizontalScrollRect.prefabSource.prefabName = "Item_TowerBuy";
            self.View.ELoopScrollList_RewardLoopHorizontalScrollRect.prefabSource.poolSize = 4;
            self.View.ELoopScrollList_RewardLoopHorizontalScrollRect.AddItemRefreshListener((transform, i) =>
                self.AddRewardItemRefreshListener(transform, i));
            
            self.View.ELoopScrollList_MonsterLoopHorizontalScrollRect.prefabSource.prefabName = "Item_Monsters";
            self.View.ELoopScrollList_MonsterLoopHorizontalScrollRect.prefabSource.poolSize = 3;
            self.View.ELoopScrollList_MonsterLoopHorizontalScrollRect.AddItemRefreshListener((transform, i) => 
                 self.AddMonsterItemRefreshListener(transform, i));
        }

        public static void ShowWindow(this DlgARRoomPVE self, ShowWindowData contextData = null)
        {
            UIAudioManagerHelper.PlayMusic(self.DomainScene(), MusicType.Main);
            MirrorVerse.UI.MirrorSceneClassyUI.ClassyUI.Instance.HideMenu();
            
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

        public static async ETTask RefreshUI(this DlgARRoomPVE self)
        {
            self.View.ELoopScrollList_RoomMemberLoopHorizontalScrollRect.RefreshCells();
        
            self.SetRoomMemberStatusText();
        }
        
        public static void SetRoomMemberStatusText(this DlgARRoomPVE self)
        {
            RoomComponent roomComponent = self.GetRoomComponent();

            if (roomComponent == null)
            {
                return;
            }

            long myPlayerId = PlayerHelper.GetMyPlayerId(self.DomainScene());

            if (myPlayerId == roomComponent.ownerRoomMemberId)
            {
                self.View.ELable_RoomMemberStatusTextMeshProUGUI.text =
                        LocalizeComponent.Instance.GetTextValue("TextCode_Key_RoomMemberStatus_StartGame");
                self.View.ELabel_TakePhysicalStrengthNumTextMeshProUGUI.text =
                        GlobalSettingCfgCategory.Instance.ARPVECfgTakePhsicalStrength.ToString();
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
                self.View.ELabel_TakePhysicalStrengthNumTextMeshProUGUI.text = "0";
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
            GamePlayBattleLevelCfg gamePlayBattleLevelCfg = GamePlayBattleLevelCfgCategory.Instance.Get(roomComponent.gamePlayBattleLevelCfgId);
            //self.View.ELabel_BattleCfgIdTextMeshProUGUI.text = gamePlayBattleLevelCfg.Name;

            bool isShowTeamChgButton = gamePlayBattleLevelCfg.TeamMode is PlayerTeam;
            //self.View.E_RoomMemberChgTeamButton.gameObject.SetActive(isShowTeamChgButton);
        }


        public static async ETTask GetRoomInfo(this DlgARRoomPVE self)
        {
            PlayerStatusComponent playerStatusComponent = ET.Client.PlayerHelper.GetMyPlayerStatusComponent(self.DomainScene());
            long roomId = playerStatusComponent.RoomId;
            await RoomHelper.GetRoomInfoAsync(self.DomainScene(), roomId);

            RoomComponent roomComponent = self.GetRoomComponent();
            int count = roomComponent.roomMemberSeat.Count;
            self.AddUIScrollItems(ref self.ScrollItemRoomMembers, count);
            self.View.ELoopScrollList_RoomMemberLoopHorizontalScrollRect.SetVisible(true, count);

            TowerDefense_ChallengeLevelCfg challengeLevelCfg =
                    TowerDefense_ChallengeLevelCfgCategory.Instance.Get(roomComponent.gamePlayBattleLevelCfgId);
            self.level = challengeLevelCfg.Index;
            List<string> list;
            PlayerBaseInfoComponent playerBaseInfoComponent = await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(self.DomainScene());

            if (playerBaseInfoComponent.ChallengeClearLevel + 1 > self.level){
                list = ET.DropItemRuleHelper.GetPreviewDropItems(challengeLevelCfg.RepeatClearDropItem);
            }else{
                list = ET.DropItemRuleHelper.GetPreviewDropItems(challengeLevelCfg.FirstClearDropItem);
            }
            self.AddUIScrollItems(ref self.ScrollItemReward, list.Count);
            self.View.ELoopScrollList_RewardLoopHorizontalScrollRect.SetVisible(true, list.Count);

            List<string> monsterList = challengeLevelCfg.MonsterList;
            self.AddUIScrollItems(ref self.ScrollItemMonsters, monsterList.Count);
            self.View.ELoopScrollList_MonsterLoopHorizontalScrollRect.SetVisible(true, monsterList.Count);
            self.SetRoomMemberStatusText();
        }

        public static RoomComponent GetRoomComponent(this DlgARRoomPVE self)
        {
            PlayerStatusComponent playerStatusComponent = ET.Client.PlayerHelper.GetMyPlayerStatusComponent(self.DomainScene());
            long roomId = playerStatusComponent.RoomId;
            RoomManagerComponent roomManagerComponent = ET.Client.RoomHelper.GetRoomManager(self.DomainScene());
            RoomComponent roomComponent = roomManagerComponent.GetRoom(roomId);
            return roomComponent;
        }

        public static async ETTask ChgRoomMemberStatus(this DlgARRoomPVE self)
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
        
        public static async ETTask AddMemberItemRefreshListener(this DlgARRoomPVE self, Transform transform, int index)
        {
            self.SetSrcollOffset();
            RoomComponent roomComponent = self.GetRoomComponent();

            Scroll_Item_RoomMember itemRoom = self.ScrollItemRoomMembers[index].BindTrans(transform);

            itemRoom.ELabel_Content_LvTextMeshProUGUI.gameObject.SetActive(false);
            //itemRoom.EButton_OperatorButton.SetVisible(true);
            itemRoom.EImage_TeamImage.SetVisible(false);
            long roomMemberId = roomComponent.roomMemberSeat[index];
            if (roomMemberId == -1)
            {
                itemRoom.uiTransform.SetVisible(false);
                itemRoom.ELabel_Content_NameTextMeshProUGUI.text =
                    LocalizeComponent.Instance.GetTextValue("TextCode_Key_RoomMemberStatus_SeatIndex", index);
                itemRoom.ELabel_OperatorText.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_RoomMemberStatus_ChgSeat");
                itemRoom.EButton_OperatorButton.AddListener(() => { self.ChgRoomSeat(index).Coroutine(); });
                itemRoom.ELabel_Content_LeaderImage.gameObject.SetActive(false);
                itemRoom.EButton_IconImage.gameObject.SetActive(false);
                itemRoom.EG_ReadyRectTransform.gameObject.SetActive(false);
            }
            else
            {
                itemRoom.uiTransform.SetVisible(true);
                long myPlayerId = PlayerHelper.GetMyPlayerId(self.DomainScene());
                RoomMember roomMember = roomComponent.GetRoomMember(roomMemberId);

                itemRoom.EG_ReadyRectTransform.gameObject.SetActive(roomMember.isReady);
                if (roomComponent.ownerRoomMemberId == roomMemberId)
                {
                    itemRoom.ELabel_Content_LeaderImage.gameObject.SetActive(true);
                    //itemRoom.ELabel_Content_NameTextMeshProUGUI.text = $"{roomMemberId}";
                }
                else
                {
                    itemRoom.ELabel_Content_LeaderImage.gameObject.SetActive(false);
                    //itemRoom.ELabel_Content_NameTextMeshProUGUI.text = $"{roomMemberId}";
                }

                if (myPlayerId == roomComponent.ownerRoomMemberId)
                {
                    itemRoom.ELabel_OperatorText.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_RoomMemberStatus_KickMember");
                    itemRoom.EButton_OperatorButton.AddListener(() => { self.KickOutRoom(roomMemberId).Coroutine(); });
                }
                else
                {
                    itemRoom.ELabel_OperatorText.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_RoomMemberStatus_SeeMemberInfo");
                    itemRoom.EButton_OperatorButton.AddListener(() => { });
                }

                if (myPlayerId == roomMemberId)
                {
                    itemRoom.EButton_OperatorButton.SetVisible(false);
                    //itemRoom.ELabel_Content_NameTextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_RoomMemberStatus_Self");
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
                PlayerBaseInfoComponent playerBaseInfoComponent =
                        await ET.Client.PlayerCacheHelper.GetOtherPlayerBaseInfo(self.DomainScene(),roomMemberId);
                itemRoom.ELabel_Content_NameTextMeshProUGUI.text = playerBaseInfoComponent.GetPlayerName();
                itemRoom.EButton_IconImage.gameObject.SetActive(true);
                await itemRoom.EButton_IconImage.SetPlayerIcon(self.DomainScene(), roomMemberId);
            }
        }
        
        public static async ETTask AddRewardItemRefreshListener(this DlgARRoomPVE self, Transform transform, int index)
        {
            transform.name = $"Item_TowerBuy_{index}";
            Scroll_Item_TowerBuy itemTowerBuy = self.ScrollItemReward[index].BindTrans(transform);
            itemTowerBuy.EImage_TowerBuyShowImage.SetVisible(true);

            TowerDefense_ChallengeLevelCfg challengeLevelCfg =
                    TowerDefense_ChallengeLevelCfgCategory.Instance.GetChallengeByIndex(true, self.level);
            List<string> list = ET.DropItemRuleHelper.GetPreviewDropItems(challengeLevelCfg.FirstClearDropItem);

            string towerCfgId = list[index];
            string itemCfgId = list[index];
            string towerName = ItemHelper.GetItemName(itemCfgId);
            TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(towerCfgId);

            string iconPath = ItemHelper.GetItemIcon(itemCfgId);
            if (string.IsNullOrEmpty(iconPath) == false)
            {
                await itemTowerBuy.EButton_IconImage.SetImageByPath(iconPath);
            }

            itemTowerBuy.EImage_PurchasedImage.SetVisible(false);
            itemTowerBuy.ELabel_ContentText.gameObject.SetActive(false);
            itemTowerBuy.EImage_BuyBGImage.gameObject.SetActive(false);
            itemTowerBuy.EButton_SelectButton.AddListener(()=>
            {
                self.ShowDetails(itemCfgId);
            });

            itemTowerBuy.EButton_nameTextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValue(towerName);
            itemTowerBuy.SetLevel(itemCfgId);
            itemTowerBuy.SetLabels(itemCfgId);
            itemTowerBuy.SetQuality(itemCfgId);
            itemTowerBuy.SetCheckMark(false);
        }
        
        public static async ETTask AddMonsterItemRefreshListener(this DlgARRoomPVE self, Transform transform, int index)
        {
            transform.name = $"Item_Monster_{index}";
            Scroll_Item_Monsters itemMonster = self.ScrollItemMonsters[index].BindTrans(transform);
            TowerDefense_ChallengeLevelCfg challengeLevelCfg =
                    TowerDefense_ChallengeLevelCfgCategory.Instance.GetChallengeByIndex(true, self.level);
            List<string> monsterList = challengeLevelCfg.MonsterList;

            string monsterCfgId = monsterList[index];
            string itemCfgId = monsterList[index];
            string iconPath = ItemHelper.GetItemIcon(itemCfgId);
            if (string.IsNullOrEmpty(iconPath) == false)
            {
                await itemMonster.EImage_MonsterImage.SetImageByPath(iconPath);
            }
            itemMonster.EButton_SelectButton.AddListener(() => { 
                TowerDefense_MonsterCfg monsterCfg = TowerDefense_MonsterCfgCategory.Instance.Get(monsterCfgId);
                self.View.ELabel_Label1TextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValue(monsterCfg.Desc);			
                // self.View.E_TipsButton.gameObject.transform.Find("tips").position = transform.position + Vector3.up;
                // self.View.E_TipsButton.SetVisible(true);
            });
            ET.EventTriggerListener.Get(itemMonster.EButton_SelectButton.gameObject).onPress.AddListener(async (go, eventData) =>
            {
                TowerDefense_MonsterCfg monsterCfg = TowerDefense_MonsterCfgCategory.Instance.Get(monsterCfgId);
                DlgDescTips_ShowWindowData _DlgDescTips_ShowWindowData = new()
                {
                    Pos = transform.position + Vector3.up,
                    Desc = LocalizeComponent.Instance.GetTextValue(monsterCfg.Desc),
                };
                await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgDescTips>(_DlgDescTips_ShowWindowData);
            });
            ET.EventTriggerListener.Get(itemMonster.EButton_SelectButton.gameObject).onExit.AddListener(async (go, eventData) =>
            {
                UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgDescTips>();
            });
        }

        public static void ShowDetails(this DlgARRoomPVE self, string itemCfgId)
        {
            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(self.DomainScene());
            _UIComponent.ShowWindow<DlgDetails>();
            DlgDetails _DlgDetails = _UIComponent.GetDlgLogic<DlgDetails>(true);
            if (_DlgDetails != null)
            {
                _DlgDetails.SetCurItemCfgId(itemCfgId);
            }
        }
        
        public static async ETTask ChgRoomSeat(this DlgARRoomPVE self, int newSeat)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

            await RoomHelper.ChgRoomMemberSeatAsync(self.ClientScene(), newSeat);
        }
        
        public static async ETTask KickOutRoom(this DlgARRoomPVE self, long beKickedPlayerId)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

            await RoomHelper.KickMemberOutRoomAsync(self.ClientScene(), beKickedPlayerId);
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
        
        public static void SetSrcollOffset(this DlgARRoomPVE self)
        {
            int width = (int)self.View.ELoopScrollList_RoomMemberLoopHorizontalScrollRect.transform.GetComponent<RectTransform>().rect.width / 2;
            int offset = width - (148 * 3);
            RoomComponent roomComponent = self.GetRoomComponent();
            int count = roomComponent.GetRoomMemberList().Count;
            if (count < 3)
            {
                offset = width - (148 * count);
            }
            self.View.ELoopScrollList_RoomMemberLoopHorizontalScrollRect.content.GetComponent<HorizontalLayoutGroup>().padding.left = offset;
        }
	}
}
