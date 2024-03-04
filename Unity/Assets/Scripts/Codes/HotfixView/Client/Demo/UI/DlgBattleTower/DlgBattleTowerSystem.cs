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
    [Invoke(TimerInvokeType.BattleTowerFrameTimer)]
    public class DlgBattleTowerTimer: ATimer<DlgBattleTower>
    {
        protected override void Run(DlgBattleTower self)
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

    [FriendOf(typeof (DlgBattleTower))]
    public static class DlgBattleTowerSystem
    {
        public static void RegisterUIEvent(this DlgBattleTower self)
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
            self.View.E_ReScanButton.AddListenerAsync(self.ReScan);

        }

        public static void ShowWindow(this DlgBattleTower self, ShowWindowData contextData = null)
        {
            UIAudioManagerHelper.PlayMusic(self.DomainScene(), MusicType.Game);
            self.needResetMyOwnTowList = true;
            //self.ShowAvatar().Coroutine();
            self.View.ES_AvatarShow.E_AvatarIconImage.SetVisible(false);

            self.ShowPutTipMsg("");
            self.SetStep();
            self.RefreshCoin();

            self.ShowMyTowers();

            self.Timer = TimerComponent.Instance.NewFrameTimer(TimerInvokeType.BattleTowerFrameTimer, self);

            //self.PlayMusic();
        }

        public static void ShowMyTowers(this DlgBattleTower self)
        {
            int countTower = self.GetOwnerTowerList().Count;
            self.AddUIScrollItems(ref self.ScrollItemTowers, countTower);
            self.View.ELoopScrollList_TowerLoopHorizontalScrollRect.SetVisible(true, countTower);
        }

        public static void HideWindow(this DlgBattleTower self)
        {
            TimerComponent.Instance?.Remove(ref self.Timer);
        }

        public static void SetStep(this DlgBattleTower self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
            GamePlayTowerDefenseStatus gamePlayTowerDefenseStatus = gamePlayTowerDefenseComponent.gamePlayTowerDefenseStatus;
            long myPlayerId = PlayerHelper.GetMyPlayerId(self.DomainScene());

            if (gamePlayTowerDefenseComponent.ownerPlayerId == myPlayerId)
            {
                self.View.E_ReScanButton.SetVisible(true);
            }
            else
            {
                self.View.E_ReScanButton.SetVisible(false);
            }

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
                //UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgCommonLoading>();
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

                if (self.gamePlayTowerDefenseStatus != gamePlayTowerDefenseStatus)
                {
                    float homeHp = 0;
                    PutHomeComponent putHomeComponent = gamePlayTowerDefenseComponent.GetComponent<PutHomeComponent>();
                    if (putHomeComponent != null)
                    {
                        Unit homeUnit = putHomeComponent.GetHomeUnit(myPlayerId);
                        if (homeUnit != null && homeUnit.GetComponent<NumericComponent>() != null)
                        {
                            homeHp = homeUnit.GetComponent<NumericComponent>().GetAsInt(NumericType.Hp);
                        }
                    }
                    EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
                    {
                        eventName = "WaveStarted",
                        properties = new()
                        {
                            {"hp_num", homeHp},
                            {"coins_num", self.GetMyGold()},
                            {"cards_num", self.GetMyTowerCardCount()},
                            {"towers_num", self.GetMyTowerList().Count},
                            {"wave_num", self.GetCurMonsterWave() + 1},
                        }
                    });

                    EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLoggingStart() { eventName = "ShoppingEnded"});
                }
            }
            else if (gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.InTheBattle)
            {
                self.View.E_PutHomeAndMonsterPointImage.gameObject.SetActive(false);
                self.View.E_BattleImage.gameObject.SetActive(true);

                self.ShowMonsterWaveInfo();
                self.SetCurLeftTimeInfo();
                self.NotTowerBuyShowWhenBattle().Coroutine();

                if (self.gamePlayTowerDefenseStatus != gamePlayTowerDefenseStatus)
                {
                    float homeHp = 0;
                    PutHomeComponent putHomeComponent = gamePlayTowerDefenseComponent.GetComponent<PutHomeComponent>();
                    if (putHomeComponent != null)
                    {
                        Unit homeUnit = putHomeComponent.GetHomeUnit(myPlayerId);
                        if (homeUnit != null && homeUnit.GetComponent<NumericComponent>() != null)
                        {
                            homeHp = homeUnit.GetComponent<NumericComponent>().GetAsInt(NumericType.Hp);
                        }
                    }
                    EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
                    {
                        eventName = "ShoppingEnded",
                        properties = new()
                        {
                            {"hp_num", homeHp},
                            {"coins_num", self.GetMyGold()},
                            {"cards_num", self.GetMyTowerCardCount()},
                            {"towers_num", self.GetMyTowerList().Count},
                            {"wave_num", self.GetCurMonsterWave()},
                        }
                    });

                    EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLoggingStart() { eventName = "WaveEnded"});
                }
            }
            else if (gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.InTheBattleEnd)
            {
                UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.DefeatOneWave);

                if (self.gamePlayTowerDefenseStatus != gamePlayTowerDefenseStatus)
                {
                    float homeHp = 0;
                    PutHomeComponent putHomeComponent = gamePlayTowerDefenseComponent.GetComponent<PutHomeComponent>();
                    if (putHomeComponent != null)
                    {
                        Unit homeUnit = putHomeComponent.GetHomeUnit(myPlayerId);
                        if (homeUnit != null && homeUnit.GetComponent<NumericComponent>() != null)
                        {
                            homeHp = homeUnit.GetComponent<NumericComponent>().GetAsInt(NumericType.Hp);
                        }
                    }
                    EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
                    {
                        eventName = "WaveEnded",
                        properties = new()
                        {
                            {"hp_num", homeHp},
                            {"coins_num", self.GetMyGold()},
                            {"cards_num", self.GetMyTowerCardCount()},
                            {"towers_num", self.GetMyTowerList().Count},
                            {"wave_num", self.GetCurMonsterWave()},
                        }
                    });
                }
            }
            else if (gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.WaitRescan)
            {
                if (gamePlayTowerDefenseComponent.ownerPlayerId == myPlayerId)
                {
                    self._ReScan().Coroutine();
                }
                else
                {
                    string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_OwnerRescan_Member");
                    ET.Client.UIManagerHelper.ShowOnlyConfirm(self.DomainScene(), msg, () =>
                    {
                        self._ReScan().Coroutine();
                    });
                }
            }
            else if (gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.GameEnd)
            {
                //UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgCommonLoading>();
                self.SetCurLeftTimeInfo();
                self.NotTowerBuyShowWhenBattle().Coroutine();

                UIManagerHelper.GetUIComponent(self.DomainScene()).HideAllShownWindow();
                UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgBattleTowerEnd>().Coroutine();
            }
            else if (gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.Recover)
            {
                self.SetCurLeftTimeInfo();
                string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_Recover");
                ET.Client.UIManagerHelper.ShowConfirm(self.DomainScene(), msg, ()=>{
                    ET.Client.GamePlayTowerDefenseHelper.SendGameRecoverConfirm(self.DomainScene(), false).Coroutine();
                }, () =>
                {
                    ET.Client.GamePlayTowerDefenseHelper.SendGameRecoverCancel(self.DomainScene(), false).Coroutine();
                });
            }
            else if (gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.Recovering)
            {
                ET.Client.UIManagerHelper.HideConfirm(self.DomainScene());
                ET.Client.AdmobSDKComponent.Instance.ShowRewardedAd(() =>
                {
                    ET.Client.GamePlayTowerDefenseHelper.SendGameRecoverConfirm(self.DomainScene(), true).Coroutine();
                },() =>
                {
                    string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Reward_Without_RewardAd");
            		ET.Client.UIManagerHelper.ShowOnlyConfirm(self.DomainScene(), msg, () => {
                        ET.Client.GamePlayTowerDefenseHelper.SendGameRecoverConfirm(self.DomainScene(), true).Coroutine();
                    });
                }).Coroutine();
            }
            else
            {
            }

            self.gamePlayTowerDefenseStatus = gamePlayTowerDefenseStatus;
        }

        public static int GetCurMonsterWave(this DlgBattleTower self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
            MonsterWaveCallComponent monsterWaveCallComponent = gamePlayTowerDefenseComponent.GetComponent<MonsterWaveCallComponent>();
            return monsterWaveCallComponent.curIndex + 1;
        }

        public static List<long> GetMyTowerList(this DlgBattleTower self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
            long myPlayerId = PlayerHelper.GetMyPlayerId(self.DomainScene());
            List<long> towerList = gamePlayTowerDefenseComponent.GetPutTowers(myPlayerId);
            return towerList;
        }

        public static int GetMyTowerCardCount(this DlgBattleTower self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
            long myPlayerId = PlayerHelper.GetMyPlayerId(self.DomainScene());
            Dictionary<string, int> towerCardList = gamePlayTowerDefenseComponent.GetPlayerOwnerTowers(myPlayerId);
            int count = 0;
            foreach (var item in towerCardList)
            {
                count += item.Value;
            }
            return count;
        }

        public static int GetMyGold(this DlgBattleTower self)
        {
            GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(self.DomainScene());
            long playerId = PlayerHelper.GetMyPlayerId(self.DomainScene());
            int curGoldValue = (int)gamePlayComponent.GetPlayerCoin(playerId, CoinType.Gold);
            return curGoldValue;
        }

        public static void RefreshCoin(this DlgBattleTower self)
        {
            //self.View.ELabel_TotalGoldTextMeshProUGUI.text = $"{self.GetMyGold()}";

            self.ChkRefreshBuyTowerPannel();
            self.View.ELabel_TotalGoldTextMeshProUGUI.transform.GetComponent<UITextLocalizeMonoView>().DynamicSet(self.GetMyGold());
        }

        public static void ChkRefreshBuyTowerPannel(this DlgBattleTower self)
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

        public static void SetCurLeftTimeInfo(this DlgBattleTower self)
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

        public static void ShowCurLeftTimeInfo(this DlgBattleTower self)
        {
            long leftTime = self.curLeftTime - TimeHelper.ClientNow();
            if (leftTime < 0)
            {
                return;
            }
            int leftTimeShow = (int)(leftTime / 1000f);
            self.View.ELabel_LeftTimeTextMeshProUGUI.text = self.curLeftTimeMsg.Replace("{0}", leftTimeShow.ToString());
        }

        public static void ShowMonsterWaveInfo(this DlgBattleTower self)
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

        public static async ETTask RefreshUI(this DlgBattleTower self)
        {
            self.needResetMyOwnTowList = true;
            self.SetStep();

            if (self.gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.WaitRescan)
            {
                return;
            }
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

        public static async ETTask QuitBattle(this DlgBattleTower self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(),SoundEffectType.Back);

            string msgTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_LeaveTheGame_Des");
            string sureTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_LeaveTheGame_Confirm");
            string cancelTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_LeaveTheGame_Cancel");
            string titleTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_LeaveTheGame_Title");
            ET.Client.UIManagerHelper.ShowConfirm(self.DomainScene(), msgTxt, () =>
            {
                self._QuitBattle().Coroutine();
            }, null, sureTxt, cancelTxt, titleTxt);
        }

        public static async ETTask _QuitBattle(this DlgBattleTower self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
            if (gamePlayTowerDefenseComponent.IsEndlessChallengeMode())
            {
                EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
                {
                    eventName = "LevelEnded",
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

        public static async ETTask _ReScan(this DlgBattleTower self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
            if (gamePlayTowerDefenseComponent.IsEndlessChallengeMode())
            {
                EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
                {
                    eventName = "LevelEnded",
                    properties = new()
                    {
                        {"finished", false},
                        {"max_wave_num", self.GetCurMonsterWave()},
                        {"tower_num", self.GetMyTowerList().Count},
                        {"coin_num", self.GetMyGold()},
                    }
                });
            }

            await RoomHelper.MemberReturnRoomFromBattleAsync(self.ClientScene());
            await SceneHelper.EnterHall(self.ClientScene());
        }

        public static GamePlayTowerDefenseComponent GetGamePlayTowerDefense(this DlgBattleTower self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
            return gamePlayTowerDefenseComponent;
        }

        public static List<string> GetTowerBuyList(this DlgBattleTower self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
            PlayerOwnerTowersComponent playerOwnerTowersComponent = gamePlayTowerDefenseComponent.GetComponent<PlayerOwnerTowersComponent>();
            long playerId = PlayerHelper.GetMyPlayerId(self.DomainScene());
            return playerOwnerTowersComponent.playerTowerBuyPools[playerId];
        }

        public static List<bool> GetTowerBoughtsList(this DlgBattleTower self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
            PlayerOwnerTowersComponent playerOwnerTowersComponent = gamePlayTowerDefenseComponent.GetComponent<PlayerOwnerTowersComponent>();
            long playerId = PlayerHelper.GetMyPlayerId(self.DomainScene());
            return playerOwnerTowersComponent.playerTowerBuyPoolBoughts[playerId];
        }

        public static List<string> GetOwnerTowerList(this DlgBattleTower self)
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

        public static async ETTask TowerBuyShow(this DlgBattleTower self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

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

        public static async ETTask CloseTowerBuyShow(this DlgBattleTower self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

            self.View.E_TowerBuyShowImage.gameObject.SetActive(false);
            self.View.EButton_BuyButton.gameObject.SetActive(true);
        }

        public static async ETTask NotTowerBuyShowWhenBattle(this DlgBattleTower self)
        {
            self.View.E_TowerBuyShowImage.gameObject.SetActive(false);
            self.View.EButton_BuyButton.gameObject.SetActive(false);
            self.View.EButton_ReadyWhenRestTimeButton.gameObject.SetActive(false);
        }

        public static void OnSelectTower(this DlgBattleTower self, string towerCfgId)
        {
            int ownTowerCount = self.myOwnTowerDic[towerCfgId];
            if (ownTowerCount <= 0)
            {
                return;
            }

            self.View.ELoopScrollList_TowerLoopHorizontalScrollRect.gameObject.SetActive(false);
            self.View.EG_BuyNodeRectTransform.SetVisible(false);
            //self.View.E_QuitBattleButton.SetVisible(false);
            self.View.E_CoinsAndTowersTranslucentImage.SetVisible(false);
            self.View.ELabel_LeftMonsterWave_boxTranslucentImage.SetVisible(false);
            self.View.ELabel_LeftTimeTextMeshProUGUI.SetVisible(false);

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

        public static void OnSaleTower(this DlgBattleTower self, string towerCfgId)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Sell);
            ET.Client.GamePlayTowerDefenseHelper.SendScalePlayerTowerCard(self.ClientScene(), towerCfgId).Coroutine();
        }

        public static void AddTowerItemRefreshListener(this DlgBattleTower self, Transform transform, int index)
        {
            transform.name = $"Item_Tower_{index}";
            Scroll_Item_Tower itemTower = self.ScrollItemTowers[index].BindTrans(transform);

            List<string> list = self.GetOwnerTowerList();

            string itemCfgId = list[index];
            string towerName = ItemHelper.GetItemName(itemCfgId);
            TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(itemCfgId);

            string icon = ItemHelper.GetItemIcon(itemCfgId);
            if (string.IsNullOrEmpty(icon) == false)
            {
                Sprite sprite = ResComponent.Instance.LoadAsset<Sprite>(icon);
                itemTower.EButton_TowerIcoImage.sprite = sprite;
            }
            itemTower.ELabel_NumTextMeshProUGUI.text = $"{self.myOwnTowerDic[itemCfgId]}";
            itemTower.ELabel_NameTextMeshProUGUI.text = $"{towerName}";
            itemTower.ELabel_SellTextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_SaleTower_Confirm");

            ET.EventTriggerListener.Get(itemTower.EButton_SelectButton.gameObject).onPress.AddListener((go, xx) =>
            {
                self.View.E_Sprite_BGImage.SetVisible(false);
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
            });
            */
            itemTower.EButton_SaleButton.AddListener(() =>
            {
                self.OnSaleTower(itemCfgId);
            });

            itemTower.EImage_SellImage.SetVisible(false);
            self.View.E_Sprite_BGImage.SetVisible(false);
            itemTower.EButton_SelectButton.AddListener(() =>
            {
                if (self.View.E_QuitBattleButton.IsActive())
                {
                    UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);
                    self.View.ELoopScrollList_TowerLoopHorizontalScrollRect.RefreshCells();
                    //int scaleTowerCostGold =  (int)ItemHelper.get
                    itemTower.E_SaleMoney_textTextMeshProUGUI.text = $"{towerCfg.ScaleTowerCostGold}";
                    itemTower.EImage_SellImage.SetVisible(true);
                    self.View.E_Sprite_BGImage.SetVisible(true);

                    self.View.E_Sprite_BGButton.AddListener(() =>
                    {
                        itemTower.EImage_SellImage.SetVisible(false);
                        self.View.E_Sprite_BGImage.SetVisible(false);
                    });
                }
            });

            itemTower.SetLevel(itemCfgId);
            itemTower.SetLabels(itemCfgId);
            itemTower.SetQuality(itemCfgId);
        }

        public static void AddTowerBuyListener(this DlgBattleTower self, Transform transform, int index)
        {
            transform.name = $"Item_TowerBuy_{index}";
            Scroll_Item_TowerBuy itemTowerBuy = self.ScrollItemTowerBuy[index].BindTrans(transform);
            itemTowerBuy.EImage_TowerBuyShowImage.SetVisible(true);

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
                self.BuyTower(index, towerCfgId).Coroutine();
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

           itemTowerBuy.SetLevel(itemCfgId);

            // TODO 暂时取消
            /*itemTowerBuy.EButton_SelectButton.AddListener(() =>
            {
                UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgBattleTowerHUD>(new DlgBattleTowerHUD_ShowWindowData()
                {
                    towerCfgId = towerCfgId,
                }).Coroutine();
            });*/

            itemTowerBuy.SetLabels(itemCfgId);
            itemTowerBuy.SetQuality(itemCfgId);
        }

        public static async ETTask BuyTower(this DlgBattleTower self, int index, string towerCfgId)
        {
            //UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);

            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = ET.Client.GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
            long playerId = PlayerHelper.GetMyPlayerId(self.DomainScene());
            (bool bRet, string msg) = gamePlayTowerDefenseComponent.ChkBuyPlayerTower(playerId, index);
            if (bRet == false)
            {
                UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.BattleForbidden);
                string tipMsg = msg;
                ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                return;
            }
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Buy);
            await ET.Client.GamePlayTowerDefenseHelper.SendBuyPlayerTower(self.ClientScene(), index, towerCfgId);

        }

        public static async ETTask RefreshBuyTower(this DlgBattleTower self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = ET.Client.GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
            long playerId = PlayerHelper.GetMyPlayerId(self.DomainScene());
            (bool bRet, string msg) = gamePlayTowerDefenseComponent.ChkRefreshBuyPlayerTower(playerId);
            if (bRet == false)
            {
                UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.BattleForbidden);
                string tipMsg = msg;
                ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                return;
            }
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);
            await ET.Client.GamePlayTowerDefenseHelper.SendRefreshBuyPlayerTower(self.ClientScene());
        }

        public static async ETTask ReadyWhenRestTime(this DlgBattleTower self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);

            await ET.Client.GamePlayTowerDefenseHelper.SendReadyWhenRestTime(self.ClientScene());
        }

        public static async ETTask ReScan(this DlgBattleTower self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(),SoundEffectType.Back);

            string msgTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_RescanTheGame_Des");
            string sureTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_RescanTheGame_Confirm");
            string cancelTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_RescanTheGame_Cancel");
            string titleTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_RescanTheGame_Title");
            ET.Client.UIManagerHelper.ShowConfirm(self.DomainScene(), msgTxt, () =>
            {
                ET.Client.GamePlayTowerDefenseHelper.SendReScan(self.ClientScene()).Coroutine();
            }, null, sureTxt, cancelTxt, titleTxt);
        }

        public static void Update(this DlgBattleTower self)
        {
            self.ShowCurLeftTimeInfo();

        }

        public static void ShowPutTipMsg(this DlgBattleTower self, string tipMsg)
        {
            if (string.IsNullOrEmpty(tipMsg))
            {
                self.HidePutTipMsg();
                return;
            }
            self.View.E_TipNodeImage.SetVisible(true);
            self.View.E_TipTextTextMeshProUGUI.text = tipMsg;
        }

        public static void HidePutTipMsg(this DlgBattleTower self)
        {
            self.View.E_TipNodeImage.SetVisible(false);
        }

        public static void ResetWhenPutFinish(this DlgBattleTower self)
        {
            if (self.IsDisposed)
            {
                return;
            }

            self.View.E_TipNodeImage.SetVisible(false);
            self.View.EG_BuyNodeRectTransform.SetVisible(true);
            //self.View.E_QuitBattleButton.SetVisible(true);
            self.View.E_CoinsAndTowersTranslucentImage.SetVisible(true);
            self.View.ELabel_LeftMonsterWave_boxTranslucentImage.SetVisible(true);
            self.View.ELabel_LeftTimeTextMeshProUGUI.SetVisible(true);
            //self.CloseTowerBuyShow().Coroutine();

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

        public static void OnSelectHeadQuarter(this DlgBattleTower self)
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

        public static void OnSelectMonsterCall(this DlgBattleTower self)
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

        public static async ETTask ShowMesh(this DlgBattleTower self)
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

                // Draco bytes
                byte[] bytes = await gamePlayComponent.DownloadFileBytesAsync(gamePlayComponent._ARMeshDownLoadUrl);
                MeshHelper.MeshData meshData = MeshHelper.GetMeshDataFromBytes(bytes);
                CreateMeshFromData(meshData, gamePlayComponent.GetARScale());
            }
            else if (gamePlayComponent.isTestARObj)
            {
                gamePlayComponent._ARMeshDownLoadUrl = gamePlayComponent.isTestARObjUrl;

                string content = await gamePlayComponent.DownloadFileTextAsync(gamePlayComponent._ARMeshDownLoadUrl);
                ET.LoadMesh.ObjMesh objInstace = new ET.LoadMesh.ObjMesh();
                objInstace = objInstace.LoadFromObj(content);

                CreateMesh(objInstace.VertexArray, objInstace.TriangleArray, objInstace.NormalArray, objInstace.UVArray, gamePlayComponent.GetARScale());
            }
            else if(gamePlayComponent.ChkIsAR() == false)
            {
                return;
            }

        }

        public static Mesh CreateMeshFromData(MeshHelper.MeshData meshData, float3 scale)
        {
            var vertices = new Vector3[meshData.vertices.Length];
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] = meshData.vertices[i];
            }
            var normals = new Vector3[meshData.normals.Length];
            for (int i = 0; i < meshData.normals.Length; i++)
            {
                normals[i] = meshData.normals[i];
            }
            var uv = new Vector2[meshData.uv.Length];
            for (int i = 0; i < meshData.uv.Length; i++)
            {
                uv[i] = meshData.uv[i];
            }

            return CreateMesh(vertices, meshData.triangles, normals, uv, scale);
        }

        public static Mesh CreateMesh(Vector3[] verticesIn, int[] trianglesIn, Vector3[] normalsIn, Vector2[] uvIn, float3 scale)
        {
            Mesh mesh = new Mesh();
            mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
            if (verticesIn != null)
            {
                var vertices = new Vector3[verticesIn.Length];
                for (int i = 0; i < vertices.Length; i++)
                {
                    vertices[i] = verticesIn[i] * scale;
                }
                mesh.vertices = vertices;
            }
            if (trianglesIn != null)
            {
                mesh.triangles = trianglesIn;
            }
            if (normalsIn != null)
            {
                var normals = new Vector3[normalsIn.Length];
                for (int i = 0; i < normalsIn.Length; i++)
                {
                    normals[i] = normalsIn[i];
                }

                mesh.normals = normals;
            }
            if (uvIn != null)
            {
                var uv = new Vector2[uvIn.Length];
                for (int i = 0; i < uvIn.Length; i++)
                {
                    uv[i] = uvIn[i];
                }
                mesh.uv = uv;
            }

            mesh.RecalculateNormals();
            mesh.RecalculateUVDistributionMetric(0);
            mesh.RecalculateBounds();
            mesh.Optimize();
            mesh.UploadMeshData(false);

            GameObject zpbTestObj = GameObject.Find("zpbTestObj");
            if (zpbTestObj != null)
            {
                GameObject.Destroy(zpbTestObj);
            }
            zpbTestObj = new GameObject("zpbTestObj");
            GameObject.DontDestroyOnLoad(zpbTestObj);
            zpbTestObj.transform.localScale = new Vector3(1, 1, 1);
            zpbTestObj.layer = LayerMask.NameToLayer("Map");
            zpbTestObj.AddComponent<MeshCollider>().sharedMesh = mesh;
            zpbTestObj.AddComponent<MeshRenderer>();
            zpbTestObj.AddComponent<MeshFilter>().sharedMesh = mesh;
            // Mesh without wireframe data.
            return mesh;
        }

        public static async ETTask ShowAvatar(this DlgBattleTower self)
        {
            await self.View.ES_AvatarShow.E_AvatarIconImage.SetMyIcon(self.DomainScene());

            long playerId = PlayerHelper.GetMyPlayerId(self.DomainScene());
            GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(self.DomainScene());
            float3 colorValue = gamePlayComponent.GetPlayerColor(playerId);
            Color color = new Color(colorValue.x, colorValue.y, colorValue.z);
            self.View.ES_AvatarShow.E_ImgLineImage.color = color;
        }

        public static void RefreshBtnShow(this DlgBattleTower self)
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