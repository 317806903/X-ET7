using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using ET.Ability.Client;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using SkillSlotType = ET.Ability.SkillSlotType;

namespace ET.Client
{
    [Invoke(TimerInvokeType.BattleTowerARFrameTimer)]
    public class DlgBattleTowerARTimer: ATimer<DlgBattleTowerAR>
    {
        protected override void Run(DlgBattleTowerAR self)
        {
            try
            {
                self.Update();
            }
            catch (Exception e)
            {
                Log.Error($"move timer error: {self.Id}\n{e}");
            }
        }
    }

    [FriendOf(typeof (DlgBattleTowerAR))]
    public static class DlgBattleTowerARSystem
    {
        public static void RegisterUIEvent(this DlgBattleTowerAR self)
        {
            self.View.ELoopScrollList_TowerLoopHorizontalScrollRect.prefabSource.prefabName = "Item_Tower";
            self.View.ELoopScrollList_TowerLoopHorizontalScrollRect.prefabSource.poolSize = 5;
            self.View.ELoopScrollList_TowerLoopHorizontalScrollRect.AddItemRefreshListener((transform, i) =>
                    self.AddTowerItemRefreshListener(transform, i));

            self.View.ELoopScrollList_BuyLoopHorizontalScrollRect.prefabSource.prefabName = "Item_TowerBuy";
            self.View.ELoopScrollList_BuyLoopHorizontalScrollRect.prefabSource.poolSize = 5;
            self.View.ELoopScrollList_BuyLoopHorizontalScrollRect.AddItemRefreshListener((transform, i) =>
                    self.AddTowerBuyListener(transform, i));
            self.View.E_QuitBattleButton.AddListenerAsync(self.QuitBattle);
            self.View.EButton_BuyButton.AddListenerAsync(self.TowerBuyShow);
            self.View.EButton_BuyCloseButton.AddListenerAsync(self.CloseTowerBuyShow);
            self.View.EButton_ReadyWhenRestTimeButton.AddListenerAsync(self.ReadyWhenRestTime);
        }

        public static void ShowWindow(this DlgBattleTowerAR self, ShowWindowData contextData = null)
        {
            self.needResetMyOwnTowList = true;
            //self.ShowAvatar().Coroutine();

            self.ShowPutTipMsg("");
            self.SetStep();
            self.RefreshCoin();

            self.ShowMyTowers();

            self.Timer = TimerComponent.Instance.NewFrameTimer(TimerInvokeType.BattleTowerARFrameTimer, self);

            self.PlayMusic();
        }

        public static void PlayMusic(this DlgBattleTowerAR self)
        {
            GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(self.DomainScene());
            List<string> musicList = gamePlayComponent.GetGamePlayBattleConfig().MusicList;
            UIAudioManagerHelper.PlayMusic(self.DomainScene(), musicList);
        }

        public static void ShowMyTowers(this DlgBattleTowerAR self)
        {
            int countTower = self.GetOwnerTowerList().Count;
            self.AddUIScrollItems(ref self.ScrollItemTowers, countTower);
            self.View.ELoopScrollList_TowerLoopHorizontalScrollRect.SetVisible(true, countTower);
        }

        public static void HideWindow(this DlgBattleTowerAR self)
        {
            TimerComponent.Instance?.Remove(ref self.Timer);
        }

        public static void SetStep(this DlgBattleTowerAR self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
            GamePlayTowerDefenseStatus gamePlayTowerDefenseStatus = gamePlayTowerDefenseComponent.gamePlayTowerDefenseStatus;
            long myPlayerId = PlayerHelper.GetMyPlayerId(self.DomainScene());
            if (gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.PutHome)
            {
                self.View.E_PutHomeAndMonsterPointImage.gameObject.SetActive(true);
                self.View.E_BattleImage.gameObject.SetActive(false);

                self.View.EButton_PutHomeButton.gameObject.SetActive(true);
                self.View.EButton_PutMonsterPointButton.gameObject.SetActive(false);

                PutHomeComponent putHomeComponent = gamePlayTowerDefenseComponent.GetComponent<PutHomeComponent>();
                if (putHomeComponent != null)
                {
                    (bool bCanPutHome, bool isDonePut) = putHomeComponent.ChkCanPutHome(myPlayerId);
                    if (bCanPutHome)
                    {
                        ET.EventTriggerListener.Get(self.View.EButton_PutHomeButton.gameObject).onDown.AddListener((go, xx) =>
                        {
                            self.OnSelectHeadQuarter();
                        });

                        string txt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_PutHomeText1");
                        self.ShowPutTipMsg(txt);
                    }
                    else
                    {
                        self.View.EButton_PutHomeButton.gameObject.SetActive(false);

                        ET.EventTriggerListener.Get(self.View.EButton_PutHomeButton.gameObject).onDown.RemoveAllListeners();

                        if (isDonePut)
                        {
                            string txt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_PutHomeText2");
                            self.ShowPutTipMsg(txt);
                        }
                        else
                        {
                            string txt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_PutHomeText3");
                            self.ShowPutTipMsg(txt);
                        }
                    }
                }

                self.ShowMesh().Coroutine();
            }
            else if (gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.PutMonsterPoint)
            {
                self.View.E_PutHomeAndMonsterPointImage.gameObject.SetActive(true);
                self.View.E_BattleImage.gameObject.SetActive(false);

                self.View.EButton_PutHomeButton.gameObject.SetActive(false);
                self.View.EButton_PutMonsterPointButton.gameObject.SetActive(true);

                PutMonsterCallComponent putMonsterCallComponent = gamePlayTowerDefenseComponent.GetComponent<PutMonsterCallComponent>();
                if (putMonsterCallComponent != null && putMonsterCallComponent.MonsterCallUnitId != null && putMonsterCallComponent.MonsterCallUnitId.ContainsKey(myPlayerId))
                {
                    self.View.EButton_PutMonsterPointButton.gameObject.SetActive(false);
                    ET.EventTriggerListener.Get(self.View.EButton_PutMonsterPointButton.gameObject).onDown.RemoveAllListeners();
                    string txt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_PutMonsterPoint1", putMonsterCallComponent.GetMonsterCallCount());
                    self.ShowPutTipMsg(txt);
                }
                else
                {
                    ET.EventTriggerListener.Get(self.View.EButton_PutMonsterPointButton.gameObject).onDown.AddListener((go, xx) =>
                    {
                        self.OnSelectMonsterCall();
                    });

                    string txt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_PutMonsterPoint2");
                    self.ShowPutTipMsg(txt);
                }
            }
            else if (gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.ShowStartEffect)
            {
                self.HidePutTipMsg();
                self.View.E_PutHomeAndMonsterPointImage.gameObject.SetActive(false);
                self.View.E_BattleImage.gameObject.SetActive(false);

                UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgBattleTowerBegin>().Coroutine();
            }
            else if (gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.RestTime)
            {
                UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgCommonLoading>();
                self.View.E_PutHomeAndMonsterPointImage.gameObject.SetActive(false);
                self.View.E_BattleImage.gameObject.SetActive(true);
                RestTimeComponent restTimeComponent = gamePlayTowerDefenseComponent.GetComponent<RestTimeComponent>();
                if (restTimeComponent.isPlayerReadyForBattle[myPlayerId])
                {
                    self.View.EButton_ReadyWhenRestTimeButton.gameObject.SetActive(false);
                }
                else
                {
                    self.View.EButton_ReadyWhenRestTimeButton.gameObject.SetActive(true);
                }

                UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgBattleTowerBegin>();
                UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgBattleTowerHUD>();

                self.ShowMonsterWaveInfo();
                self.SetCurLeftTimeInfo();
                if (self.gamePlayTowerDefenseStatus != GamePlayTowerDefenseStatus.RestTime)
                {
                    self.TowerBuyShow().Coroutine();
                }
            }
            else if (gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.InTheBattle)
            {
                self.View.E_PutHomeAndMonsterPointImage.gameObject.SetActive(false);
                self.View.E_BattleImage.gameObject.SetActive(true);

                self.ShowMonsterWaveInfo();
                self.SetCurLeftTimeInfo();
                self.NotTowerBuyShowWhenBattle().Coroutine();
            }
            else if (gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.GameEnd)
            {
                UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgCommonLoading>();
                self.SetCurLeftTimeInfo();
                self.NotTowerBuyShowWhenBattle().Coroutine();

                UIManagerHelper.GetUIComponent(self.DomainScene()).HideAllShownWindow();
                UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgBattleTowerEnd>().Coroutine();
            }
            else if (gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.Recover)
            {
                self.SetCurLeftTimeInfo();
                string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_Recover");
                ET.Client.UIManagerHelper.ShowConfirm(self.DomainScene(), msg, async ()=>{
                    await ET.Client.AdmobSDKComponent.Instance.ShowRewardedAd(() =>
                    {
                        ET.Client.GamePlayTowerDefenseHelper.SendGameRecoverConfirm(self.DomainScene()).Coroutine();
                    },() =>
                    {
                        ET.Client.GamePlayTowerDefenseHelper.SendGameRecoverCancel(self.DomainScene()).Coroutine();
                    });
                }, () =>
                {
                    ET.Client.GamePlayTowerDefenseHelper.SendGameRecoverCancel(self.DomainScene()).Coroutine();
                });
            }
            else
            {
            }

            self.gamePlayTowerDefenseStatus = gamePlayTowerDefenseStatus;
        }

        public static int GetCurMonsterWave(this DlgBattleTowerAR self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
            MonsterWaveCallComponent monsterWaveCallComponent = gamePlayTowerDefenseComponent.GetComponent<MonsterWaveCallComponent>();
            return monsterWaveCallComponent.curIndex;
        }

        public static List<long> GetMyTowerList(this DlgBattleTowerAR self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
            long myPlayerId = PlayerHelper.GetMyPlayerId(self.DomainScene());
            List<long> towerList = gamePlayTowerDefenseComponent.GetPutTowers(myPlayerId);
            return towerList;
        }

        public static int GetMyGold(this DlgBattleTowerAR self)
        {
            GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(self.DomainScene());
            long playerId = PlayerHelper.GetMyPlayerId(self.DomainScene());
            int curGoldValue = (int)gamePlayComponent.GetPlayerCoin(playerId, CoinType.Gold);
            return curGoldValue;
        }

        public static void RefreshCoin(this DlgBattleTowerAR self)
        {
            //self.View.ELabel_TotalGoldTextMeshProUGUI.text = $"{self.GetMyGold()}";

            self.ChkRefreshBuyTowerPannel();
            self.View.ELabel_TotalGoldTextMeshProUGUI.transform.GetComponent<UITextLocalizeMonoView>().DynamicSet(self.GetMyGold());
        }

        public static void ChkRefreshBuyTowerPannel(this DlgBattleTowerAR self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
            GamePlayTowerDefenseStatus gamePlayTowerDefenseStatus = gamePlayTowerDefenseComponent.gamePlayTowerDefenseStatus;
            if (gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.RestTime)
            {
                self.View.ELoopScrollList_BuyLoopHorizontalScrollRect.RefreshCells();
                if (gamePlayTowerDefenseComponent.model.RefreshBuyTowerCost > self.GetMyGold())
                {
                    self.View.ELabel_RefreshTextMeshProUGUI.color = Color.red;
                }
                else
                {
                    self.View.ELabel_RefreshTextMeshProUGUI.color = Color.white;

                }
            }
        }

        public static void SetCurLeftTimeInfo(this DlgBattleTowerAR self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
            GamePlayTowerDefenseStatus gamePlayTowerDefenseStatus = gamePlayTowerDefenseComponent.gamePlayTowerDefenseStatus;
            if (gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.RestTime)
            {
                RestTimeComponent restTimeComponent = gamePlayTowerDefenseComponent.GetComponent<RestTimeComponent>();
                self.curLeftTime = (long)(restTimeComponent.duration*1000) + TimeHelper.ClientNow();

                self.curLeftTimeMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_NextMonsterWaveTime");
            }
            else if (gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.InTheBattle)
            {
                MonsterWaveCallComponent monsterWaveCallComponent = gamePlayTowerDefenseComponent.GetComponent<MonsterWaveCallComponent>();
                self.curLeftTime = (long)(monsterWaveCallComponent.duration*1000) + TimeHelper.ClientNow();
                self.curLeftTimeMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_CurMonsterWaveLeftTime");
            }
            else if (gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.GameEnd)
            {
                self.curLeftTimeMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_MonsterWaveOverTime");
            }
            else if (gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.Recover)
            {
                self.curLeftTimeMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_MonsterWaveOverTime");
            }
            else
            {
            }

        }

        public static void ShowCurLeftTimeInfo(this DlgBattleTowerAR self)
        {
            long leftTime = self.curLeftTime - TimeHelper.ClientNow();
            if (leftTime < 0)
            {
                return;
            }
            int leftTimeShow = (int)(leftTime / 1000f);
            self.View.ELabel_LeftTimeTextMeshProUGUI.text = self.curLeftTimeMsg.Replace("{0}", leftTimeShow.ToString());
        }

        public static void ShowMonsterWaveInfo(this DlgBattleTowerAR self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
            MonsterWaveCallComponent monsterWaveCallComponent = gamePlayTowerDefenseComponent.GetComponent<MonsterWaveCallComponent>();

            if (gamePlayTowerDefenseComponent.IsEndlessChallengeMonster())
            {
                self.View.ELabel_LeftMonsterWaveTextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_CurMonsterWaveIndex_EndlessChallenge",monsterWaveCallComponent.curIndex + 1, monsterWaveCallComponent.totalCount);
            }
            else
            {
                self.View.ELabel_LeftMonsterWaveTextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_CurMonsterWaveIndex",monsterWaveCallComponent.curIndex + 1, monsterWaveCallComponent.totalCount);
            }
        }

        public static async ETTask RefreshUI(this DlgBattleTowerAR self)
        {
            self.needResetMyOwnTowList = true;
            self.SetStep();

            int countBuy = self.GetTowerBuyList().Count;
            if (self.View.ELoopScrollList_BuyLoopHorizontalScrollRect.totalCount != countBuy)
            {
                self.AddUIScrollItems(ref self.ScrollItemTowerBuy, countBuy);
                self.View.ELoopScrollList_BuyLoopHorizontalScrollRect.SetVisible(true, countBuy);
            }
            self.View.ELoopScrollList_BuyLoopHorizontalScrollRect.RefreshCells();

            int countTower = self.GetOwnerTowerList().Count;
            if (self.View.ELoopScrollList_TowerLoopHorizontalScrollRect.totalCount != countTower)
            {
                self.AddUIScrollItems(ref self.ScrollItemTowers, countTower);
                self.View.ELoopScrollList_TowerLoopHorizontalScrollRect.SetVisible(true, countTower);
            }
            self.View.ELoopScrollList_TowerLoopHorizontalScrollRect.RefreshCells();


            long myPlayerId = PlayerHelper.GetMyPlayerId(self.DomainScene());
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = ET.Client.GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());

            int limitTowerCount = gamePlayTowerDefenseComponent.model.LimitTowerCount;
            int curTowerCount = gamePlayTowerDefenseComponent.GetPutTowers(myPlayerId).Count;

            string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_CurPutTowerCount", curTowerCount, limitTowerCount);
            /*if (leftCount == 0)
            {
                msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_MaxPutTowerCount");
            }
            else
            {
                msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_CurPutTowerCount", leftCount);
            }*/
            self.View.E_LeftCallPlayerTowerCountTextMeshProUGUI.text = msg;

        }

        public static async ETTask QuitBattle(this DlgBattleTowerAR self)
        {
            UIAudioManagerHelper.PlayUIAudioBack(self.DomainScene());

            string msgTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_LeaveTheGame_Des");
            string sureTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_LeaveTheGame_Confirm");
            string cancelTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_LeaveTheGame_Cancel");
            string titleTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_LeaveTheGame_Title");
            ET.Client.UIManagerHelper.ShowConfirm(self.DomainScene(), msgTxt, () =>
            {
                self._QuitBattle().Coroutine();
            }, null, sureTxt, cancelTxt, titleTxt);
        }

        public static async ETTask _QuitBattle(this DlgBattleTowerAR self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
            if (gamePlayTowerDefenseComponent.IsEndlessChallengeMode())
            {
                EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
                {
                    eventName = "InfinityEnded",
                    properties = new()
                    {
                        {"finished", false},
                        {"max_wave_num", self.GetCurMonsterWave()},
                        {"tower_num", self.GetMyTowerList().Count},
                        {"coin_num", self.GetMyGold()},
                    }
                });
            }
            await RoomHelper.MemberQuitBattleAsync(self.ClientScene());
            await SceneHelper.EnterHall(self.ClientScene());
        }

        public static GamePlayTowerDefenseComponent GetGamePlayTowerDefense(this DlgBattleTowerAR self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
            return gamePlayTowerDefenseComponent;
        }

        public static List<string> GetTowerBuyList(this DlgBattleTowerAR self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
            PlayerOwnerTowersComponent playerOwnerTowersComponent = gamePlayTowerDefenseComponent.GetComponent<PlayerOwnerTowersComponent>();
            long playerId = PlayerHelper.GetMyPlayerId(self.DomainScene());
            return playerOwnerTowersComponent.playerTowerBuyPools[playerId];
        }

        public static List<bool> GetTowerBoughtsList(this DlgBattleTowerAR self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
            PlayerOwnerTowersComponent playerOwnerTowersComponent = gamePlayTowerDefenseComponent.GetComponent<PlayerOwnerTowersComponent>();
            long playerId = PlayerHelper.GetMyPlayerId(self.DomainScene());
            return playerOwnerTowersComponent.playerTowerBuyPoolBoughts[playerId];
        }

        public static List<string> GetOwnerTowerList(this DlgBattleTowerAR self)
        {
            if (self.needResetMyOwnTowList == false && self.myOwnTowerList.Count > 0)
            {
                return self.myOwnTowerList;
            }
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
            PlayerOwnerTowersComponent playerOwnerTowersComponent = gamePlayTowerDefenseComponent.GetComponent<PlayerOwnerTowersComponent>();
            long playerId = PlayerHelper.GetMyPlayerId(self.DomainScene());
            self.myOwnTowerDic.Clear();
            foreach (var list in playerOwnerTowersComponent.playerOwnerTowerId[playerId])
            {
                if (list.Value > 0)
                {
                    self.myOwnTowerDic[list.Key] = list.Value;
                }
            }

            self.myOwnTowerList.Clear();
            foreach (var myOwnTower in self.myOwnTowerDic)
            {
                self.myOwnTowerList.Add(myOwnTower.Key);
            }
            self.myOwnTowerList.Sort((x, y) =>
            {
                TowerDefense_TowerCfg towerCfg1 = TowerDefense_TowerCfgCategory.Instance.Get(x);
                TowerDefense_TowerCfg towerCfg2 = TowerDefense_TowerCfgCategory.Instance.Get(y);
                if (towerCfg1.Type == towerCfg2.Type)
                {
                    return towerCfg2.Level[0].CompareTo(towerCfg1.Level[0]);
                }
                else
                {
                    return towerCfg2.Type.CompareTo(towerCfg1.Type);
                }
            });
            self.needResetMyOwnTowList = false;
            return self.myOwnTowerList;
        }

        public static async ETTask TowerBuyShow(this DlgBattleTowerAR self)
        {
            UIAudioManagerHelper.PlayUIAudioClick(self.DomainScene());

            self.View.E_TowerBuyShowImage.gameObject.SetActive(true);
            self.View.EButton_BuyButton.gameObject.SetActive(false);

            int countTank = self.GetTowerBuyList().Count;
            self.AddUIScrollItems(ref self.ScrollItemTowerBuy, countTank);
            self.View.ELoopScrollList_BuyLoopHorizontalScrollRect.SetVisible(true, countTank);

            self.View.EButton_RefreshButton.AddListenerAsync(self.RefreshBuyTower);
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();

            self.View.ELabel_RefreshTextMeshProUGUI.transform.GetComponent<UITextLocalizeMonoView>().DynamicSet(gamePlayTowerDefenseComponent.model.RefreshBuyTowerCost);
            self.RefreshBtnShow();
        }

        public static async ETTask CloseTowerBuyShow(this DlgBattleTowerAR self)
        {
            UIAudioManagerHelper.PlayUIAudioClick(self.DomainScene());

            self.View.E_TowerBuyShowImage.gameObject.SetActive(false);
            self.View.EButton_BuyButton.gameObject.SetActive(true);
        }

        public static async ETTask NotTowerBuyShowWhenBattle(this DlgBattleTowerAR self)
        {
            self.View.E_TowerBuyShowImage.gameObject.SetActive(false);
            self.View.EButton_BuyButton.gameObject.SetActive(false);
            self.View.EButton_ReadyWhenRestTimeButton.gameObject.SetActive(false);
        }

        public static void OnSelectTower(this DlgBattleTowerAR self, string towerCfgId)
        {
            int ownTowerCount = self.myOwnTowerDic[towerCfgId];
            if (ownTowerCount <= 0)
            {
                return;
            }

            self.View.ELoopScrollList_TowerLoopHorizontalScrollRect.gameObject.SetActive(false);
            self.View.EG_BuyNodeRectTransform.SetVisible(false);

            DlgBattleDragItem_ShowWindowData showWindowData = new()
            {
                battleDragItemType = BattleDragItemType.Tower,
                battleDragItemParam = towerCfgId,
                callBack = (scene) =>
                {
                    self.ResetWhenPutFinish();
                },
            };
            UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgBattleDragItem>(showWindowData).Coroutine();
        }

        public static void AddTowerItemRefreshListener(this DlgBattleTowerAR self, Transform transform, int index)
        {
            Scroll_Item_Tower itemTower = self.ScrollItemTowers[index].BindTrans(transform);

            List<string> list = self.GetOwnerTowerList();

            string itemCfgId = list[index];
            string towerName = ItemHelper.GetItemName(itemCfgId);
            //TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(list[index]);

            string icon = ItemHelper.GetItemIcon(itemCfgId);
            if (string.IsNullOrEmpty(icon) == false)
            {
                Sprite sprite = ResComponent.Instance.LoadAsset<Sprite>(icon);
                itemTower.EButton_TowerIcoImage.sprite = sprite;
            }
            itemTower.ELabel_NumTextMeshProUGUI.text = $"{self.myOwnTowerDic[itemCfgId]}";

            itemTower.ELabel_NameTextMeshProUGUI.text = $"{towerName}";

            ET.EventTriggerListener.Get(itemTower.EButton_SelectButton.gameObject).onPress.AddListener((go, xx) =>
            {
                self.OnSelectTower(itemCfgId);
            });

            long myPlayerId = PlayerHelper.GetMyPlayerId(self.DomainScene());

            //TODO 暂时取消
            /*itemTower.EButton_SelectButton.AddListener(() =>
            {
                UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgBattleTowerHUD>(new DlgBattleTowerHUD_ShowWindowData()
                {
                    playerId = myPlayerId,
                    towerCfgId = towerCfg.Id,
                }).Coroutine();
            });*/

            itemTower.EG_IconStarRectTransform.SetVisible(true);
            int starCount = (int)ItemHelper.GetTowerItemQualityRank(itemCfgId);
            itemTower.E_IconStar1Image.gameObject.SetActive(starCount>=1);
            itemTower.E_IconStar2Image.gameObject.SetActive(starCount>=2);
            itemTower.E_IconStar3Image.gameObject.SetActive(starCount>=3);

            List<string> labels = ItemHelper.GetTowerItemLabels(itemCfgId);
            int labelCount = labels.Count;
            itemTower.EImage_Label1Image.gameObject.SetActive((labelCount>=1));
            itemTower.EImage_Label2Image.gameObject.SetActive((labelCount>=2));
            if (labelCount >= 1)
            {
                itemTower.ELabel_Label1TextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValue(labels[0]);
            }
            if (labelCount >= 2)
            {
                itemTower.ELabel_Label2TextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValue(labels[1]);
            }

            int towerQuality = (int)ItemHelper.GetItemQualityType(itemCfgId);
            itemTower.EImage_LowImage.SetVisible(towerQuality == 0);
            itemTower.EImage_MiddleImage.SetVisible(towerQuality == 1);
            itemTower.EImage_HighImage.SetVisible(towerQuality == 2);
        }

        public static void AddTowerBuyListener(this DlgBattleTowerAR self, Transform transform, int index)
        {
            Scroll_Item_TowerBuy itemTowerBuy = self.ScrollItemTowerBuy[index].BindTrans(transform);

            List<string> list = self.GetTowerBuyList();
            List<bool> listBought = self.GetTowerBoughtsList();

            string towerCfgId = list[index];
            string itemCfgId = list[index];
            string towerName = ItemHelper.GetItemName(itemCfgId);
            TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(towerCfgId);

            string icon = ItemHelper.GetItemIcon(itemCfgId);
            if (string.IsNullOrEmpty(icon) == false)
            {
                Sprite sprite = ResComponent.Instance.LoadAsset<Sprite>(icon);
                itemTowerBuy.EButton_IconImage.sprite = sprite;
            }
            if (listBought[index])
            {
                /*itemTowerBuy.ELabel_ContentText.color = Color.white;
                itemTowerBuy.ELabel_ContentText.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_TowerBuyDone");
                itemTowerBuy.EButton_BuyButton.gameObject.SetActive(false);*/
                itemTowerBuy.EImage_PurchasedImage.SetVisible(true);
            }
            else
            {
                itemTowerBuy.EImage_PurchasedImage.SetVisible(false);
                itemTowerBuy.ELabel_ContentText.text = $"{towerCfg.BuyTowerCostGold}";
                itemTowerBuy.EButton_BuyButton.gameObject.SetActive(true);
                if (towerCfg.BuyTowerCostGold <= self.GetMyGold())
                {
                    itemTowerBuy.ELabel_ContentText.color = Color.white;
                }
                else
                {
                    itemTowerBuy.ELabel_ContentText.color = Color.red;
                }
            }
            itemTowerBuy.EButton_BuyButton.AddListener(() =>
            {
                self.BuyTower(index).Coroutine();
            });

            itemTowerBuy.EButton_nameTextMeshProUGUI.text = $"{towerName}";
            if (towerCfg.BuyTowerCostGold <= self.GetMyGold())
            {
                itemTowerBuy.ELabel_BuyText.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_TowerBuyButton");
            }
            else
            {
                itemTowerBuy.ELabel_BuyText.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_TowerBuy_NotEnough");
            }

            itemTowerBuy.EG_IconStarRectTransform.SetVisible(true);
            int starCount = (int)ItemHelper.GetTowerItemQualityRank(itemCfgId);
            itemTowerBuy.E_IconStar1Image.gameObject.SetActive(starCount>=1);
            itemTowerBuy.E_IconStar2Image.gameObject.SetActive(starCount>=2);
            itemTowerBuy.E_IconStar3Image.gameObject.SetActive(starCount>=3);


            //TODO 暂时取消
            /*itemTowerBuy.EButton_SelectButton.AddListener(() =>
            {
                UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgBattleTowerHUD>(new DlgBattleTowerHUD_ShowWindowData()
                {
                    towerCfgId = towerCfgId,
                }).Coroutine();
            });*/

            List<string> labels = ItemHelper.GetTowerItemLabels(itemCfgId);
            int labelCount = labels.Count;
            itemTowerBuy.EImage_Label1Image.gameObject.SetActive((labelCount>=1));
            itemTowerBuy.EImage_Label2Image.gameObject.SetActive((labelCount>=2));
            if (labelCount >= 1)
            {
                itemTowerBuy.ELabel_Label1TextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValue(labels[0]);
            }
            if (labelCount >= 2)
            {
                itemTowerBuy.ELabel_Label2TextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValue(labels[1]);
            }

            int towerQuality = (int)ItemHelper.GetItemQualityType(itemCfgId);
            itemTowerBuy.EImage_LowImage.SetVisible(towerQuality == 0);
            itemTowerBuy.EImage_MiddleImage.SetVisible(towerQuality == 1);
            itemTowerBuy.EImage_HighImage.SetVisible(towerQuality == 2);
        }

        public static async ETTask BuyTower(this DlgBattleTowerAR self, int index)
        {
            UIAudioManagerHelper.PlayUIAudioConfirm(self.DomainScene());

            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = ET.Client.GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
            long playerId = PlayerHelper.GetMyPlayerId(self.DomainScene());
            (bool bRet, string msg) = gamePlayTowerDefenseComponent.ChkBuyPlayerTower(playerId, index);
            if (bRet == false)
            {
                string tipMsg = msg;
                ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                return;
            }

            await ET.Client.GamePlayTowerDefenseHelper.SendBuyPlayerTower(self.ClientScene(), index);
        }

        public static async ETTask RefreshBuyTower(this DlgBattleTowerAR self)
        {
            UIAudioManagerHelper.PlayUIAudioClick(self.DomainScene());

            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = ET.Client.GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
            long playerId = PlayerHelper.GetMyPlayerId(self.DomainScene());
            (bool bRet, string msg) = gamePlayTowerDefenseComponent.ChkRefreshBuyPlayerTower(playerId);
            if (bRet == false)
            {
                string tipMsg = msg;
                ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                return;
            }

            await ET.Client.GamePlayTowerDefenseHelper.SendRefreshBuyPlayerTower(self.ClientScene());
        }

        public static async ETTask ReadyWhenRestTime(this DlgBattleTowerAR self)
        {
            UIAudioManagerHelper.PlayUIAudioClick(self.DomainScene());

            await ET.Client.GamePlayTowerDefenseHelper.SendReadyWhenRestTime(self.ClientScene());
        }

        public static void Update(this DlgBattleTowerAR self)
        {
            self.ShowCurLeftTimeInfo();

        }

        public static void ShowPutTipMsg(this DlgBattleTowerAR self, string tipMsg)
        {
            if (string.IsNullOrEmpty(tipMsg))
            {
                self.HidePutTipMsg();
                return;
            }
            self.View.E_TipNodeImage.SetVisible(true);
            self.View.E_TipTextTextMeshProUGUI.text = tipMsg;
        }

        public static void HidePutTipMsg(this DlgBattleTowerAR self)
        {
            self.View.E_TipNodeImage.SetVisible(false);
        }

        public static void ResetWhenPutFinish(this DlgBattleTowerAR self)
        {
            if (self.IsDisposed)
            {
                return;
            }
            self.View.E_TipNodeImage.SetVisible(false);
            self.View.EG_BuyNodeRectTransform.SetVisible(true);

            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
            GamePlayTowerDefenseStatus gamePlayTowerDefenseStatus = gamePlayTowerDefenseComponent.gamePlayTowerDefenseStatus;
            if (gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.RestTime)
            {
                self.View.ELoopScrollList_TowerLoopHorizontalScrollRect.gameObject.SetActive(true);
            }
            else if (gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.InTheBattle)
            {
                self.View.ELoopScrollList_TowerLoopHorizontalScrollRect.gameObject.SetActive(true);
            }
            else
            {
                self.SetStep();
            }
        }

        public static void OnSelectHeadQuarter(this DlgBattleTowerAR self)
        {
            self.View.EButton_PutHomeButton.gameObject.SetActive(false);
            DlgBattleDragItem_ShowWindowData showWindowData = new()
            {
                battleDragItemType = BattleDragItemType.HeadQuarter,
                battleDragItemParam = "",
                callBack = (scene) =>
                {
                    self.ResetWhenPutFinish();
                },
            };
            UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgBattleDragItem>(showWindowData).Coroutine();

        }
        public static void OnSelectMonsterCall(this DlgBattleTowerAR self)
        {
            self.View.EButton_PutMonsterPointButton.gameObject.SetActive(false);
            DlgBattleDragItem_ShowWindowData showWindowData = new()
            {
                battleDragItemType = BattleDragItemType.MonsterCall,
                battleDragItemParam = "",
                callBack = (scene) =>
                {
                    self.ResetWhenPutFinish();
                },
            };
            UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgBattleDragItem>(showWindowData).Coroutine();
        }

        public static async ETTask ShowMesh(this DlgBattleTowerAR self)
        {

            if (Application.isEditor == false)
            {
                return;
            }

            GamePlayComponent gamePlayComponent = ET.Client.GamePlayHelper.GetGamePlay(self.DomainScene());
            if (gamePlayComponent == null)
            {
                return;
            }

            if (gamePlayComponent.isTestARMesh)
            {
                gamePlayComponent._ARMeshDownLoadUrl = gamePlayComponent.isTestARMeshUrl;
            }
            else if(gamePlayComponent.ChkIsAR() == false)
            {
                return;
            }

            // Draco bytes
            byte[] bytes = await gamePlayComponent.DownloadFileAsync(gamePlayComponent._ARMeshDownLoadUrl);
            MeshHelper.MeshData meshData = MeshHelper.GetMeshDataFromBytes(bytes);
            CreateMeshFromData(meshData, gamePlayComponent.GetARScale());
        }

        public static Mesh CreateMeshFromData(MeshHelper.MeshData meshData, float3 scale)
        {
            Mesh mesh = new Mesh();
            mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
            if (meshData.vertices != null)
            {
                var vertices = new Vector3[meshData.vertices.Length];
                for (int i = 0; i < meshData.vertices.Length; i++)
                {
                    vertices[i] = meshData.vertices[i] * scale;
                }
                mesh.vertices = vertices;
            }
            if (meshData.triangles != null)
            {
                mesh.triangles = meshData.triangles;
            }
            if (meshData.normals != null)
            {
                var normals = new Vector3[meshData.normals.Length];
                for (int i = 0; i < meshData.normals.Length; i++)
                {
                    normals[i] = meshData.normals[i];
                }

                mesh.normals = normals;
            }
            if (meshData.uv != null)
            {
                var uv = new Vector2[meshData.uv.Length];
                for (int i = 0; i < meshData.uv.Length; i++)
                {
                    uv[i] = meshData.uv[i];
                }
                mesh.uv = uv;
            }

            mesh.RecalculateNormals();
            mesh.RecalculateUVDistributionMetric(0);
            mesh.RecalculateBounds();
            mesh.Optimize();
            mesh.UploadMeshData(false);

            GameObject go = new GameObject("zpb");
            GameObject.DontDestroyOnLoad(go);
            go.transform.localScale = new Vector3(1, 1, 1);
            go.layer = LayerMask.NameToLayer("Map");
            go.AddComponent<MeshCollider>().sharedMesh = mesh;
            go.AddComponent<MeshRenderer>();
            go.AddComponent<MeshFilter>().sharedMesh = mesh;
            // Mesh without wireframe data.
            return mesh;
        }

        public static async ETTask ShowAvatar(this DlgBattleTowerAR self)
        {
            await self.View.ES_AvatarShow.E_AvatarIconImage.SetMyIcon(self.DomainScene());

            long playerId = PlayerHelper.GetMyPlayerId(self.DomainScene());
            GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(self.DomainScene());
            float3 colorValue = gamePlayComponent.GetPlayerColor(playerId);
            Color color = new Color(colorValue.x, colorValue.y, colorValue.z);
            self.View.ES_AvatarShow.E_ImgLineImage.color = color;
        }

        public static void RefreshBtnShow(this DlgBattleTowerAR self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
            if (gamePlayTowerDefenseComponent.model.RefreshBuyTowerCost > self.GetMyGold())
            {
                self.View.ELabel_RefreshTextMeshProUGUI.color = Color.red;
            }
            else
            {
                self.View.ELabel_RefreshTextMeshProUGUI.color = Color.white;
            }
        }
    }
}