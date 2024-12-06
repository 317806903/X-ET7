using System.Collections;
using System.Collections.Generic;
using System;
using DG.Tweening;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;

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
            self.View.ELoopScrollList_TowerLoopHorizontalScrollRect.prefabSource.prefabName = "Item_TowerBattle";
            self.View.ELoopScrollList_TowerLoopHorizontalScrollRect.prefabSource.poolSize = 5;
            self.View.ELoopScrollList_TowerLoopHorizontalScrollRect.AddItemRefreshListener((transform, i) =>
                    self.AddTowerItemRefreshListener(transform, i));

            self.View.ELoopScrollList_BuyLoopHorizontalScrollRect.prefabSource.prefabName = "Item_TowerBattleBuy";
            self.View.ELoopScrollList_BuyLoopHorizontalScrollRect.prefabSource.poolSize = 5;
            self.View.ELoopScrollList_BuyLoopHorizontalScrollRect.AddItemRefreshListener((transform, i) =>
                    self.AddTowerBuyListener(transform, i));
            self.View.EButton_BuyButton.AddListenerAsync(self.TowerBuyShow);
            self.View.EButton_BuyCloseButton.AddListenerAsync(self.CloseTowerBuyShow);
            self.View.EButton_ReadyWhenRestTimeButton.AddListenerAsync(self.ReadyWhenRestTime);

            self.View.E_GameSettingButton.SetVisible(true);
            self.View.E_GameSettingButton.AddListenerAsync(self.GameSetting);

            if (Application.isMobilePlatform)
            {
                self.View.E_ForceGameEndWhenDebugButton.SetVisible(false);
                self.View.E_ForceAddGameGoldWhenDebugButton.SetVisible(false);
                self.View.E_ForceAddHomeHpWhenDebugButton.SetVisible(false);

            }
            else
            {
                self.View.E_ForceGameEndWhenDebugButton.SetVisible(true);
                self.View.E_ForceAddGameGoldWhenDebugButton.SetVisible(true);
                self.View.E_ForceAddHomeHpWhenDebugButton.SetVisible(true);
                self.View.E_ForceGameEndWhenDebugButton.AddListenerAsync(self.ForceGameEndWhenDebug);
                self.View.E_ForceAddGameGoldWhenDebugButton.AddListenerAsync(self.ForceAddGameGoldWhenDebug);
                self.View.E_ForceAddHomeHpWhenDebugButton.AddListenerAsync(self.ForceAddHomeHpWhenDebug);
            }
        }

        public static async ETTask ShowWindow(this DlgBattleTowerAR self, ShowWindowData contextData = null)
        {
            self.needResetMyOwnTowList = true;
            //self.ShowAvatar().Coroutine();
            self.View.ES_AvatarShow.View.E_AvatarIconImage.SetVisible(false);

            self.ShowPutTipMsg("");
            self.ResetUIBase();
            self.SetStep();
            self.RefreshCoin();

            self.ShowMyTowers();

            self.Timer = TimerComponent.Instance.NewFrameTimer(TimerInvokeType.BattleTowerARFrameTimer, self);

            self.ChkNeedBattleGuide().Coroutine();

            self.ResetScrollRectMoveWhenGuide();

            await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgBattleCameraPlayerSkill>();

            //self.PlayMusic();
        }

        public static async ETTask ChkNeedBattleGuide(this DlgBattleTowerAR self)
        {
            string battleCfgId = self.GetGamePlayTowerDefense().GetGamePlay().GetGamePlayBattleConfig().Id;

            GamePlayBattleLevelCfg gamePlayBattleLevelCfg = GamePlayBattleLevelCfgCategory.Instance.Get(battleCfgId);
            if (gamePlayBattleLevelCfg == null)
            {
                return;
            }
            if (string.IsNullOrEmpty(gamePlayBattleLevelCfg.BattleGuideConfigFileName))
            {
                return;
            }

            if (ET.Client.UIGuideHelper.ChkIsUIGuideing(self.DomainScene()))
            {
                if (ET.Client.UIGuideHelper.ChkIsUIGuideing(self.DomainScene(), gamePlayBattleLevelCfg.BattleGuideConfigFileName) == false)
                {
                    if (ET.Client.UIGuideHelper.ChkCanReplaceCurGuideing(self.DomainScene(), (int)ET.Client.GuidePriority.BattleGuide) == false)
                    {
                        return;
                    }
                }
            }

            PlayerOtherInfoComponent playerOtherInfoComponent = await ET.Client.PlayerCacheHelper.GetMyPlayerOtherInfo(self.DomainScene());
            (bool bNeedBattleGuide, string battleGuideConfigFileName) = playerOtherInfoComponent.ChkNeedBattleGuide(battleCfgId);
            if (bNeedBattleGuide)
            {
                int startIndex = playerOtherInfoComponent.GetBattleGuideStepIndex(battleCfgId);
                GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
                GamePlayTowerDefenseStatus gamePlayTowerDefenseStatus = gamePlayTowerDefenseComponent.gamePlayTowerDefenseStatus;
                if (gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.PutHome)
                {
                    startIndex = 0;
                    playerOtherInfoComponent.SetBattleGuideStepFinished(battleCfgId, 0);
                    await ET.Client.PlayerCacheHelper.SaveMyPlayerModel(self.DomainScene(), PlayerModelType.OtherInfo, new(){"battleGuideStepIndex"});
                }

                await ET.Client.UIGuideHelper.DoUIGuide(self.DomainScene(), battleGuideConfigFileName, (int)ET.Client.GuidePriority.BattleGuide, startIndex,async (scene) =>
                {
                    PlayerOtherInfoComponent playerOtherInfoComponent = await ET.Client.PlayerCacheHelper.GetMyPlayerOtherInfo(scene);
                    playerOtherInfoComponent.SetBattleGuideFinished(battleCfgId);
                    await ET.Client.PlayerCacheHelper.SaveMyPlayerModel(scene, PlayerModelType.OtherInfo, new(){"battleGuideStatus"});
                }, async (scene, stepIndex) =>
                {
                    PlayerOtherInfoComponent playerOtherInfoComponent = await ET.Client.PlayerCacheHelper.GetMyPlayerOtherInfo(scene);
                    playerOtherInfoComponent.SetBattleGuideStepFinished(battleCfgId, stepIndex+1);
                    await ET.Client.PlayerCacheHelper.SaveMyPlayerModel(scene, PlayerModelType.OtherInfo, new(){"battleGuideStepIndex"});
                });
            }
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

            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgBattleCameraPlayerSkill>();

        }

        public static void SetStep(this DlgBattleTowerAR self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
            GamePlayTowerDefenseStatus newGamePlayTowerDefenseStatus = gamePlayTowerDefenseComponent.gamePlayTowerDefenseStatus;
            long myPlayerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());

            ET.Client.UIManagerHelper.HideConfirm(self.DomainScene());
            ET.Client.UIManagerHelper.HideChoose(self.DomainScene());
            self.ResetScrollRectMoveWhenGuide();

            EventType.NoticeGamePlayTowerDefenseStatusWhenClient _NoticeGamePlayTowerDefenseStatusWhenClient = new()
            {
                gamePlayTowerDefenseStatus = newGamePlayTowerDefenseStatus,
            };
            EventSystem.Instance.Publish(self.DomainScene(), _NoticeGamePlayTowerDefenseStatusWhenClient);

            if (self.gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.PutHome && newGamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.WaitMeshFinished)
            {
                Log.Error($"--zpb self.gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.PutHome && gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.WaitMeshFinished");
                return;
            }
            if (newGamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.WaitMeshFinished)
            {
                self.View.E_PutHomeAndMonsterPointImage.gameObject.SetActive(false);
                self.View.E_BattleImage.gameObject.SetActive(false);
                string txt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_WaitMeshFinished");
                self.ShowPutTipMsg(txt);

                EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging() { eventName = "MeshLoadingStarted"});
                EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLoggingStart() { eventName = "MeshLoadingEnded"});

            }
            else if (newGamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.LoadMeshErr)
            {
                self.View.E_PutHomeAndMonsterPointImage.gameObject.SetActive(false);
                self.View.E_BattleImage.gameObject.SetActive(false);
                string txt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_LoadMeshErr");
                self.ShowPutTipMsg(txt);

                EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
                {
                    eventName = "MeshLoadingEnded",
                    properties = new()
                    {
                        {"success", false},
                    }
                });

            }
            else if (newGamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.PutHome)
            {
                self.View.E_PutHomeAndMonsterPointImage.gameObject.SetActive(true);
                self.View.E_BattleImage.gameObject.SetActive(false);

                self.View.EButton_PutHomeButton.gameObject.SetActive(true);
                self.View.EButton_PutMonsterPointButton.gameObject.SetActive(false);
                self.View.EButton_ResetHomeButton.gameObject.SetActive(false);

                PutHomeComponent putHomeComponent = gamePlayTowerDefenseComponent.GetComponent<PutHomeComponent>();
                if (putHomeComponent != null)
                {
                    (bool bCanPutHome, bool isDonePut) = putHomeComponent.ChkCanPutHome(myPlayerId);
                    if (bCanPutHome)
                    {
                        ET.EventTriggerListener.Get(self.View.EButton_PutHomeButton.gameObject).onDown.RemoveAllListeners();
                        ET.EventTriggerListener.Get(self.View.EButton_PutHomeButton.gameObject).onDown.AddListener((go, xx) =>
                        {
                            self.OnSelectHeadQuarter().Coroutine();
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


                ET.Client.UIManagerHelper.ShowARMesh(self.DomainScene()).Coroutine();

                EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
                {
                    eventName = "MeshLoadingEnded",
                    properties = new()
                    {
                        {"success", true},
                    }
                });

            }
            else if (newGamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.PutMonsterPoint)
            {
                self.View.E_PutHomeAndMonsterPointImage.gameObject.SetActive(true);
                self.View.E_BattleImage.gameObject.SetActive(false);

                self.View.EButton_PutHomeButton.gameObject.SetActive(false);
                self.View.EButton_PutMonsterPointButton.gameObject.SetActive(true);
                self.View.EButton_ResetHomeButton.gameObject.SetActive(false);

                PutHomeComponent putHomeComponent = gamePlayTowerDefenseComponent.GetComponent<PutHomeComponent>();
                if (putHomeComponent != null)
                {
                    bool isPutHomePlayer = putHomeComponent.ChkIsPutHomePlayer(myPlayerId);
                    if (isPutHomePlayer)
                    {
                        self.View.EButton_ResetHomeButton.gameObject.SetActive(true);
                        ET.EventTriggerListener.Get(self.View.EButton_ResetHomeButton.gameObject).onDown.RemoveAllListeners();
                        ET.EventTriggerListener.Get(self.View.EButton_ResetHomeButton.gameObject).onDown.AddListener((go, xx) =>
                        {
                            self.OnResetHeadQuarter().Coroutine();
                        });
                    }
                }

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
                    ET.EventTriggerListener.Get(self.View.EButton_PutMonsterPointButton.gameObject).onDown.RemoveAllListeners();
                    ET.EventTriggerListener.Get(self.View.EButton_PutMonsterPointButton.gameObject).onDown.AddListener((go, xx) =>
                    {
                        self.OnSelectMonsterCall().Coroutine();
                    });
                    string txt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_PutMonsterPoint2");
                    self.ShowPutTipMsg(txt);
                }
            }
            else if (newGamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.ShowStartEffect)
            {
                self.HidePutTipMsg();
                self.View.E_PutHomeAndMonsterPointImage.gameObject.SetActive(false);
                self.View.E_BattleImage.gameObject.SetActive(false);

                UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgBattleTowerBegin>().Coroutine();

                self.NoticeShowBattleNoticeWhenFirstShow().Coroutine();
            }
            else if (newGamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.RestTime)
            {
                if (self.gamePlayTowerDefenseStatus != newGamePlayTowerDefenseStatus)
                {
                    gamePlayTowerDefenseComponent.PlayRestTimeMusic();
                }

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
                UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgBattleHomeHUD>();

                self.ShowMonsterWaveInfo();
                self.SetCurLeftTimeInfo();
                if (self.gamePlayTowerDefenseStatus != GamePlayTowerDefenseStatus.RestTime)
                {
                    self.TowerBuyShow().Coroutine();
                }

                if (self.gamePlayTowerDefenseStatus != newGamePlayTowerDefenseStatus)
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
            else if (newGamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.InTheBattle)
            {
                if (self.gamePlayTowerDefenseStatus != newGamePlayTowerDefenseStatus)
                {
                    gamePlayTowerDefenseComponent.PlayBattleMusic();
                    UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.NextWave);
                }

                self.View.E_PutHomeAndMonsterPointImage.gameObject.SetActive(false);
                self.View.E_BattleImage.gameObject.SetActive(true);

                self.ShowMonsterWaveInfo();
                self.SetCurLeftTimeInfo();
                self.NotTowerBuyShowWhenBattle().Coroutine();

                if (self.gamePlayTowerDefenseStatus != newGamePlayTowerDefenseStatus)
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
            else if (newGamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.InTheBattleEnd)
            {
                UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.DefeatOneWave);

                GameObjectPoolHelper.ResetPoolDictCount(20);
                Resources.UnloadUnusedAssets();
                System.GC.Collect();

                if (self.gamePlayTowerDefenseStatus != newGamePlayTowerDefenseStatus)
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
            else if (newGamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.WaitRescan)
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
            else if (newGamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.GameEnd)
            {
                //UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgCommonLoading>();
                self.SetCurLeftTimeInfo();
                self.NotTowerBuyShowWhenBattle().Coroutine();

                UIManagerHelper.GetUIComponent(self.DomainScene()).HideAllShownWindow();
                UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgBattleTowerEnd>().Coroutine();
            }
            else if (newGamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.Recover)
            {
                self.SetCurLeftTimeInfo();

                GameRecoverComponent gameRecoverComponent = gamePlayTowerDefenseComponent.GetComponent<GameRecoverComponent>();
                GameRecoverOnceComponent gameRecoverOnceComponent = gamePlayTowerDefenseComponent.GetComponent<GameRecoverOnceComponent>();
                if (gameRecoverOnceComponent.gameRecoverType == GameRecoverType.Free)
                {
                    if (gameRecoverOnceComponent.ChkPlayerRecoverStatusIsDefault(myPlayerId))
                    {
                        string showMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_CommonChoose_RecoverFree_Msg", gameRecoverComponent.recoverFreeTimes+1);
                        string timeoutMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_CommonChoose_RecoverFree_TimeOutMsg");
                        float timeoutTime = (gameRecoverOnceComponent.timeOutTime - TimeHelper.ServerNow()) * 0.001f;
                        string confirmText = LocalizeComponent.Instance.GetTextValue("TextCode_Key_CommonChoose_RecoverFree_Confirm");
                        string cancelText = LocalizeComponent.Instance.GetTextValue("TextCode_Key_CommonChoose_RecoverFree_Cancel");
                        string titleText = null;
                        bool isTimeOutConfirm = false;
                        ET.Client.UIManagerHelper.ShowWhenTwoChoose(self.DomainScene(), showMsg, timeoutMsg, timeoutTime, async()=>{
                            EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
                            {
                                eventName = "Revived",
                                properties = new()
                                {
                                    {"type", "免费"},
                                    {"result", "复活"},
                                }
                            });
                            ET.Client.GamePlayTowerDefenseHelper.SendGameRecoverResult(self.DomainScene(), true).Coroutine();
                        }, async () =>
                        {
                            EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
                            {
                                eventName = "Revived",
                                properties = new()
                                {
                                    {"type", "免费"},
                                    {"result", "拒绝复活"},
                                }
                            });
                            await ET.Client.GamePlayTowerDefenseHelper.SendGameRecoverResult(self.DomainScene(), false);

                            gamePlayTowerDefenseComponent.gamePlayTowerDefenseStatus = GamePlayTowerDefenseStatus.GameEnd;
                            await self.RefreshUI();
                        }, confirmText, cancelText, titleText, isTimeOutConfirm, false);
                    }
                    else
                    {
                        string showMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_CommonChoose_RecoverFree_Confirm_Msg");
                        string timeoutMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_CommonChoose_RecoverFree_Confirm_TimeOutMsg");
                        float timeoutTime = (gameRecoverOnceComponent.timeOutTime - TimeHelper.ServerNow()) * 0.001f;
                        string titleText = null;
                        ET.Client.UIManagerHelper.ShowWhenNoChoose(self.DomainScene(), showMsg, timeoutMsg, timeoutTime, async()=>{

                        }, titleText);
                    }
                }
                else if (gameRecoverOnceComponent.gameRecoverType == GameRecoverType.ByWatchAd)
                {
                    // string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_Recover");
                    // ET.Client.UIManagerHelper.ShowConfirm(self.DomainScene(), msg, ()=>{
                    //     ET.Client.GamePlayTowerDefenseHelper.SendGameRecoverConfirmWatchAd(self.DomainScene(), false).Coroutine();
                    // }, () =>
                    // {
                    //     ET.Client.GamePlayTowerDefenseHelper.SendGameRecoverCancelWatchAd(self.DomainScene(), false).Coroutine();
                    // });


                    string showMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_CommonChoose_RecoverWatchAd_Msg", gameRecoverComponent.recoverByWatchAdTimes+1);
                    string timeoutMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_CommonChoose_RecoverWatchAd_TimeOutMsg");
                    float timeoutTime = (gameRecoverOnceComponent.timeOutTime - TimeHelper.ServerNow()) * 0.001f;
                    string confirmText = LocalizeComponent.Instance.GetTextValue("TextCode_Key_CommonChoose_RecoverWatchAd_Confirm");
                    string cancelText = LocalizeComponent.Instance.GetTextValue("TextCode_Key_CommonChoose_RecoverWatchAd_Cancel");
                    string titleText = null;
                    bool isTimeOutConfirm = false;
                    ET.Client.UIManagerHelper.ShowWhenTwoChoose(self.DomainScene(), showMsg, timeoutMsg, timeoutTime, async()=>{
                        ET.Client.GamePlayTowerDefenseHelper.SendGameRecoverConfirmWatchAd(self.DomainScene(), false).Coroutine();
                    }, async () =>
                    {
                        EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
                        {
                            eventName = "Revived",
                            properties = new()
                            {
                                {"type", "广告"},
                                {"result", "拒绝复活"},
                            }
                        });
                        ET.Client.GamePlayTowerDefenseHelper.SendGameRecoverCancelWatchAd(self.DomainScene(), false).Coroutine();
                    }, confirmText, cancelText, titleText, isTimeOutConfirm);
                }
                else if (gameRecoverOnceComponent.gameRecoverType == GameRecoverType.CostArcadeCoin)
                {
                    if (gameRecoverOnceComponent.ChkPlayerRecoverStatusIsDefault(myPlayerId))
                    {
                        string showMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_CommonChoose_RecoverArcadeCoin_Msg", gameRecoverOnceComponent.recoverCostArcadeCoinNum, gameRecoverComponent.recoverCostArcadeCoinTimes + 1);
                        string timeoutMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_CommonChoose_RecoverArcadeCoin_TimeOutMsg");
                        float timeoutTime = (gameRecoverOnceComponent.timeOutTime - TimeHelper.ServerNow()) * 0.001f;
                        string confirmText = LocalizeComponent.Instance.GetTextValue("TextCode_Key_CommonChoose_RecoverArcadeCoin_Confirm", gameRecoverOnceComponent.recoverCostArcadeCoinNum);
                        string cancelText = LocalizeComponent.Instance.GetTextValue("TextCode_Key_CommonChoose_RecoverArcadeCoin_Cancel");
                        string titleText = null;
                        bool isTimeOutConfirm = false;
                        ET.Client.UIManagerHelper.ShowWhenTwoChoose(self.DomainScene(), showMsg, timeoutMsg, timeoutTime, async()=>{
                            int costValue = gameRecoverOnceComponent.recoverCostArcadeCoinNum;
                            bool bRet1 = await ET.Client.UIManagerHelper.ChkCoinEnoughOrShowtip(self.DomainScene(), costValue, async () =>
                            {
                                bool bRet1 = await ET.Client.UIManagerHelper.ChkCoinEnoughOrShowtip(self.DomainScene(), costValue);
                                if (bRet1 == false)
                                {
                                    return;
                                }
                                EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
                                {
                                    eventName = "Revived",
                                    properties = new()
                                    {
                                        {"type", "街机付费"},
                                        {"result", "复活"},
                                    }
                                });
                                ET.Client.GamePlayTowerDefenseHelper.SendGameRecoverResult(self.DomainScene(), true).Coroutine();
                            });
                            if (bRet1 == false)
                            {
                                return;
                            }
                            EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
                            {
                                eventName = "Revived",
                                properties = new()
                                {
                                    {"type", "街机付费"},
                                    {"result", "复活"},
                                }
                            });
                            ET.Client.GamePlayTowerDefenseHelper.SendGameRecoverResult(self.DomainScene(), true).Coroutine();
                        }, async () =>
                        {
                            EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
                            {
                                eventName = "Revived",
                                properties = new()
                                {
                                    {"type", "街机付费"},
                                    {"result", "拒绝复活"},
                                }
                            });
                            await ET.Client.GamePlayTowerDefenseHelper.SendGameRecoverResult(self.DomainScene(), false);

                            gamePlayTowerDefenseComponent.gamePlayTowerDefenseStatus = GamePlayTowerDefenseStatus.GameEnd;
                            await self.RefreshUI();
                        }, confirmText, cancelText, titleText, isTimeOutConfirm, false);
                    }
                    else
                    {
                        string showMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_CommonChoose_RecoverArcadeCoin_Confirm_Msg");
                        string timeoutMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_CommonChoose_RecoverArcadeCoin_Confirm_TimeOutMsg");
                        float timeoutTime = (gameRecoverOnceComponent.timeOutTime - TimeHelper.ServerNow()) * 0.001f;
                        string titleText = null;
                        ET.Client.UIManagerHelper.ShowWhenNoChoose(self.DomainScene(), showMsg, timeoutMsg, timeoutTime, async()=>{

                        }, titleText);
                    }

                }
            }
            else if (newGamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.Recovering)
            {
                EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
                {
                    eventName = "AdClicked",
                    properties = new()
                        {
                            {"resource", "复活"},
                        }
                });
                ET.Client.AdmobSDKComponent.Instance.ShowRewardedAd(() =>
                {
                    EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
                    {
                        eventName = "Revived",
                        properties = new()
                        {
                            {"type", "广告"},
                            {"result", "复活"},
                        }
                    });
                    ET.Client.GamePlayTowerDefenseHelper.SendGameRecoverConfirmWatchAd(self.DomainScene(), true).Coroutine();
                },() =>
                {
                    string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Reward_Without_RewardAd");
            		ET.Client.UIManagerHelper.ShowOnlyConfirm(self.DomainScene(), msg, () => {
                        EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
                        {
                            eventName = "Revived",
                            properties = new()
                            {
                                {"type", "广告"},
                                {"result", "复活"},
                            }
                        });
                        ET.Client.GamePlayTowerDefenseHelper.SendGameRecoverConfirmWatchAd(self.DomainScene(), true).Coroutine();
                    });
                }).Coroutine();
            }
            else
            {
            }

            self.gamePlayTowerDefenseStatus = newGamePlayTowerDefenseStatus;
        }

        public static int GetCurMonsterWave(this DlgBattleTowerAR self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
            MonsterWaveCallComponent monsterWaveCallComponent = gamePlayTowerDefenseComponent.GetComponent<MonsterWaveCallComponent>();
            return monsterWaveCallComponent.GetWaveIndex();
        }

        public static List<long> GetMyTowerList(this DlgBattleTowerAR self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
            long myPlayerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());
            List<long> towerList = gamePlayTowerDefenseComponent.GetPutTowers(myPlayerId);
            return towerList;
        }

        public static int GetMyTowerCardCount(this DlgBattleTowerAR self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
            long myPlayerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());
            Dictionary<string, int> towerCardList = gamePlayTowerDefenseComponent.GetPlayerOwnerTowers(myPlayerId);
            int count = 0;
            foreach (var item in towerCardList)
            {
                count += item.Value;
            }
            return count;
        }

        public static int GetMyGold(this DlgBattleTowerAR self)
        {
            GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(self.DomainScene());
            long playerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());
            int curGoldValue = (int)gamePlayComponent.GetPlayerCoin(playerId, CoinTypeInGame.Gold);
            return curGoldValue;
        }

        public static void RefreshCoin(this DlgBattleTowerAR self)
        {
            self.ChkRefreshBuyTowerPannel();
            self.RefreshCoinAnimation();
        }

        public static void RefreshCoinAnimation(this DlgBattleTowerAR self)
        {
            // 动画时间
            float duration = 0.5f;

            int oldGold = self.oldGold;
            int newGold = self.GetMyGold();
            self.oldGold = newGold;
            if (oldGold == 0)
            {
                self.View.ELabel_TotalGoldTextMeshProUGUI.transform.GetComponent<UITextLocalizeMonoView>().DynamicSet(newGold);
                return;
            }
            if (newGold < oldGold)
            {
                duration = 0.2f;
            }

            // 记录当前金币数
            int startAmount = oldGold;

            // 使用 DOTween 进行数字渐变
            DOTween.To(() => startAmount, x =>
            {
                if (self.IsDisposed)
                {
                    return;
                }
                self.View.ELabel_TotalGoldTextMeshProUGUI.transform.GetComponent<UITextLocalizeMonoView>().DynamicSet(x);
            }, newGold, duration);

            // 可以添加额外的视觉效果，例如缩放动画
            self.View.ELabel_TotalGoldTextMeshProUGUI.transform.DOScale(1.2f, 0.2f).OnComplete(() =>
            {
                if (self.IsDisposed)
                {
                    return;
                }
                self.View.ELabel_TotalGoldTextMeshProUGUI.transform.DOScale(1f, 0.2f);
            });
        }

        public static void ChkRefreshBuyTowerPannel(this DlgBattleTowerAR self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
            if (gamePlayTowerDefenseComponent == null)
            {
                return;
            }
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

        public static void ResetScrollRectMoveWhenGuide(this DlgBattleTowerAR self)
        {
            if (ET.Client.UIGuideHelper.ChkIsUIGuideing(self.DomainScene(), true))
            {
                self.View.ELoopScrollList_TowerLoopHorizontalScrollRect.horizontal = false;
                self.View.ELoopScrollList_BuyLoopHorizontalScrollRect.horizontal = false;
            }
            else
            {
                self.View.ELoopScrollList_TowerLoopHorizontalScrollRect.horizontal = true;
                self.View.ELoopScrollList_BuyLoopHorizontalScrollRect.horizontal = true;
                self.View.ELoopScrollList_BuyLoopHorizontalScrollRect.SetSrcollMiddle();
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
                self.View.ELabel_LeftMonsterWaveTextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_CurMonsterWaveIndex_EndlessChallenge",monsterWaveCallComponent.GetWaveIndex(), monsterWaveCallComponent.totalCount);
            }
            else
            {
                self.View.ELabel_LeftMonsterWaveTextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_CurMonsterWaveIndex",monsterWaveCallComponent.GetWaveIndex(), monsterWaveCallComponent.totalCount);
            }
        }

        public static async ETTask RefreshUI(this DlgBattleTowerAR self)
        {
            self.needResetMyOwnTowList = true;

            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgBattleDragItem>();
            self.ResetUIBase();

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


            long myPlayerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = ET.Client.GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());

            int limitTowerCount = gamePlayTowerDefenseComponent.GetPutAttackTowerLimitCount(myPlayerId);
            int curTowerCount = gamePlayTowerDefenseComponent.GetPutAttackTowerCount(myPlayerId);

            string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_CurPutTowerCount", curTowerCount, limitTowerCount);
            if (self.View.E_LeftCallPlayerTowerCountTextMeshProUGUI.text != msg)
            {
                self.RefreshPlayerTowerAnimation();
            }
            self.View.E_LeftCallPlayerTowerCountTextMeshProUGUI.text = msg;
            if (limitTowerCount == curTowerCount)
            {
                self.View.E_LeftCallPlayerTowerCountTextMeshProUGUI.color = Color.red;
            }
            else
            {
                self.View.E_LeftCallPlayerTowerCountTextMeshProUGUI.color = Color.white;
            }
        }

        public static void RefreshPlayerTowerAnimation(this DlgBattleTowerAR self)
        {
            // 可以添加额外的视觉效果，例如缩放动画
            self.View.E_LeftCallPlayerTowerCountTextMeshProUGUI.transform.DOScale(1.2f, 0.2f).OnComplete(() =>
            {
                if (self.IsDisposed)
                {
                    return;
                }
                self.View.E_LeftCallPlayerTowerCountTextMeshProUGUI.transform.DOScale(1f, 0.2f);
            });
        }

        public static async ETTask QuitBattle(this DlgBattleTowerAR self)
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

        public static async ETTask _QuitBattle(this DlgBattleTowerAR self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
            if (gamePlayTowerDefenseComponent != null)
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

        public static async ETTask _ReScan(this DlgBattleTowerAR self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
            if (gamePlayTowerDefenseComponent != null)
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

        public static GamePlayTowerDefenseComponent GetGamePlayTowerDefense(this DlgBattleTowerAR self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
            return gamePlayTowerDefenseComponent;
        }

        public static List<string> GetTowerBuyList(this DlgBattleTowerAR self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
            PlayerOwnerTowersComponent playerOwnerTowersComponent = gamePlayTowerDefenseComponent.GetComponent<PlayerOwnerTowersComponent>();
            long playerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());
            return playerOwnerTowersComponent.playerTowerBuyPools[playerId];
        }

        public static List<bool> GetTowerBoughtsList(this DlgBattleTowerAR self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
            PlayerOwnerTowersComponent playerOwnerTowersComponent = gamePlayTowerDefenseComponent.GetComponent<PlayerOwnerTowersComponent>();
            long playerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());
            return playerOwnerTowersComponent.playerTowerBuyPoolBoughts[playerId];
        }

        public static int GetTowerBuyCost(this DlgBattleTowerAR self, int index)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
            PlayerOwnerTowersComponent playerOwnerTowersComponent = gamePlayTowerDefenseComponent.GetComponent<PlayerOwnerTowersComponent>();
            long playerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());
            return playerOwnerTowersComponent.playerTowerBuyPoolCosts[playerId][index];
        }

        public static List<string> GetOwnerTowerList(this DlgBattleTowerAR self)
        {
            if (self.needResetMyOwnTowList == false && self.myOwnTowerList.Count > 0)
            {
                return self.myOwnTowerList;
            }
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
            PlayerOwnerTowersComponent playerOwnerTowersComponent = gamePlayTowerDefenseComponent.GetComponent<PlayerOwnerTowersComponent>();
            long playerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());
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
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

            self.View.EG_TowerBuyShowRectTransform.gameObject.SetActive(true);
            self.View.EButton_BuyButton.gameObject.SetActive(false);

            int countBuy = self.GetTowerBuyList().Count;
            if (self.View.ELoopScrollList_BuyLoopHorizontalScrollRect.totalCount != countBuy)
            {
                self.AddUIScrollItems(ref self.ScrollItemTowerBuy, countBuy);
                self.View.ELoopScrollList_BuyLoopHorizontalScrollRect.SetVisible(true, countBuy);
            }
            self.View.ELoopScrollList_BuyLoopHorizontalScrollRect.RefreshCells();


            self.View.EButton_RefreshButton.AddListenerAsync(self.RefreshBuyTower);

            self.RefreshBtnShow();
        }

        public static async ETTask CloseTowerBuyShow(this DlgBattleTowerAR self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

            self.View.EG_TowerBuyShowRectTransform.gameObject.SetActive(false);
            self.View.EButton_BuyButton.gameObject.SetActive(true);
        }

        public static async ETTask NotTowerBuyShowWhenBattle(this DlgBattleTowerAR self)
        {
            self.View.EG_TowerBuyShowRectTransform.gameObject.SetActive(false);
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

            if (ET.ItemHelper.ChkIsAttackTower(towerCfgId))
            {
                long myPlayerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());
                GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = ET.Client.GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
                int limitTowerCount = gamePlayTowerDefenseComponent.GetPutAttackTowerLimitCount(myPlayerId);
                int curTowerCount = gamePlayTowerDefenseComponent.GetPutAttackTowerCount(myPlayerId);
                if (curTowerCount >= limitTowerCount)
                {
                    string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_MaxPutTowerCount", limitTowerCount);
                    UIManagerHelper.ShowTip(self.DomainScene(), msg);
                    UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);
                    self.RefreshPlayerTowerAnimation();
                    return;
                }
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

        public static void AddTowerItemRefreshListener(this DlgBattleTowerAR self, Transform transform, int index)
        {
            transform.name = $"Item_TowerBattle_{index}";
            Scroll_Item_TowerBattle itemTower = self.ScrollItemTowers[index].BindTrans(transform);

            List<string> list = self.GetOwnerTowerList();

            string itemCfgId = list[index];

            TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(itemCfgId);
            itemTower.Init(itemCfgId, false).Coroutine();
            itemTower.SetItemNum(self.myOwnTowerDic[itemCfgId]);

            ET.EventTriggerListener.Get(itemTower.GetActionButton()).onPress.AddListener((go, xx) =>
            {
                self.OnSelectTower(itemCfgId);
            });

            long myPlayerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());

            ET.EventTriggerListener.Get(itemTower.GetActionButton()).onClick.AddListener((go, xx) =>
            {
                if (ET.Client.UIGuideHelper.ChkIsUIGuideing(self.DomainScene(), true) == false)
                {
                    UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);
                    UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgBattleTowerHUD>(new DlgBattleTowerHUD_ShowWindowData()
                    {
                        playerId = myPlayerId,
                        towerCfgId = towerCfg.Id,
                    }).Coroutine();
                }
            });

        }

        public static void AddTowerBuyListener(this DlgBattleTowerAR self, Transform transform, int index)
        {
            transform.name = $"Item_TowerBattleBuy_{index}";
            Scroll_Item_TowerBattleBuy itemTowerBuy = self.ScrollItemTowerBuy[index].BindTrans(transform);

            List<string> list = self.GetTowerBuyList();
            List<bool> listBought = self.GetTowerBoughtsList();

            string towerCfgId = list[index];
            string itemCfgId = list[index];

            itemTowerBuy.Init(itemCfgId, false).Coroutine();

            TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(towerCfgId);
            int buyTowerCostGold = self.GetTowerBuyCost(index);
            itemTowerBuy.SetBuyStatus(listBought[index], buyTowerCostGold);

            ET.EventTriggerListener.Get(itemTowerBuy.GetActionButton()).onClick.AddListener((go, xx) =>
            {
                self.BuyTower(index, towerCfgId).Coroutine();
            });

            if (string.IsNullOrEmpty(towerCfg.TutorialCfgId) == false)
            {
                if (transform.gameObject.activeInHierarchy)
                {
                    EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeShowBattleNotice()
                    {
                        tutorialCfgId = towerCfg.TutorialCfgId,
                    });
                }
            }
        }

        public static async ETTask BuyTower(this DlgBattleTowerAR self, int index, string towerCfgId)
        {
            //UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);

            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = ET.Client.GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
            long playerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());
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

        public static async ETTask RefreshBuyTower(this DlgBattleTowerAR self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = ET.Client.GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
            long playerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());
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

        public static async ETTask ReadyWhenRestTime(this DlgBattleTowerAR self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);

            await ET.Client.GamePlayTowerDefenseHelper.SendReadyWhenRestTime(self.ClientScene());
        }

        public static async ETTask NoticeShowBattleNoticeWhenFirstShow(this DlgBattleTowerAR self)
        {
            await TimerComponent.Instance.WaitAsync(2000);
            if (self.IsDisposed)
            {
                return;
            }
            List<string> list = self.GetTowerBuyList();
            foreach (string towerCfgId in list)
            {
                TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(towerCfgId);
                if (string.IsNullOrEmpty(towerCfg.TutorialCfgId) == false)
                {
                    EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeShowBattleNotice()
                    {
                        tutorialCfgId = towerCfg.TutorialCfgId,
                    });
                }
            }
        }

        public static async ETTask ForceGameEndWhenDebug(this DlgBattleTowerAR self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(),SoundEffectType.Back);

            string msgTxt = LocalizeComponent.Instance.GetTextValue("是否快速胜利(需要已放置大本营)");
            ET.Client.UIManagerHelper.ShowConfirm(self.DomainScene(), msgTxt, () =>
            {
                ET.Client.GamePlayHelper.SendForceGameEndWhenDebug(self.DomainScene()).Coroutine();
            }, null);
        }

        public static async ETTask ForceAddGameGoldWhenDebug(this DlgBattleTowerAR self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(),SoundEffectType.Back);

            string msgTxt = LocalizeComponent.Instance.GetTextValue("是否增加金币(需要已放置大本营)");
            ET.Client.UIManagerHelper.ShowConfirm(self.DomainScene(), msgTxt, () =>
            {
                ET.Client.GamePlayHelper.SendForceAddGameGoldWhenDebug(self.DomainScene()).Coroutine();
            }, null);
        }

        public static async ETTask ForceAddHomeHpWhenDebug(this DlgBattleTowerAR self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(),SoundEffectType.Back);

            string msgTxt = LocalizeComponent.Instance.GetTextValue("是否增加大本营血量(需要已放置大本营)");
            ET.Client.UIManagerHelper.ShowConfirm(self.DomainScene(), msgTxt, () =>
            {
                ET.Client.GamePlayHelper.SendForceAddHomeHpWhenDebug(self.DomainScene()).Coroutine();
            }, null);
        }

        public static async ETTask GameSetting(this DlgBattleTowerAR self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(),SoundEffectType.Confirm);

            UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgBattleSetting>().Coroutine();
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

            self.ResetUIBase();

            self.SetStep();
        }

        public static void ResetUIBase(this DlgBattleTowerAR self)
        {
            self.View.E_TipNodeImage.SetVisible(false);
            self.View.EG_BuyNodeRectTransform.SetVisible(true);
            //self.View.E_QuitBattleButton.SetVisible(true);
            self.View.E_CoinsAndTowersTranslucentImage.SetVisible(true);
            self.View.ELabel_LeftMonsterWave_boxTranslucentImage.SetVisible(true);
            self.View.ELabel_LeftTimeTextMeshProUGUI.SetVisible(true);
            //self.CloseTowerBuyShow().Coroutine();

            // GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
            // if (gamePlayTowerDefenseComponent == null)
            // {
            //     return;
            // }
            // GamePlayTowerDefenseStatus gamePlayTowerDefenseStatus = gamePlayTowerDefenseComponent.gamePlayTowerDefenseStatus;
            // if (gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.RestTime)
            // {
            //     self.View.ELoopScrollList_TowerLoopHorizontalScrollRect.gameObject.SetActive(true);
            // }
            // else if (gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.InTheBattle)
            // {
            //     self.View.ELoopScrollList_TowerLoopHorizontalScrollRect.gameObject.SetActive(true);
            // }

            self.View.ELoopScrollList_TowerLoopHorizontalScrollRect.gameObject.SetActive(true);
            self.View.ELoopScrollList_TowerLoopHorizontalScrollRect.gameObject.SetActive(true);
        }

        public static async ETTask OnResetHeadQuarter(this DlgBattleTowerAR self)
        {
            await ET.Client.GamePlayTowerDefenseHelper.SendResetHome(self.ClientScene());
        }

        public static async ETTask OnSelectHeadQuarter(this DlgBattleTowerAR self)
        {
            await TimerComponent.Instance.WaitFrameAsync();
            await TimerComponent.Instance.WaitFrameAsync();
            self.View.E_PutHomeAndMonsterPointImage.gameObject.SetActive(false);
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

        public static async ETTask OnSelectMonsterCall(this DlgBattleTowerAR self)
        {
            await TimerComponent.Instance.WaitFrameAsync();
            await TimerComponent.Instance.WaitFrameAsync();
            self.View.E_PutHomeAndMonsterPointImage.gameObject.SetActive(false);
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

        public static async ETTask ShowAvatar(this DlgBattleTowerAR self)
        {
            await self.View.ES_AvatarShow.View.E_AvatarIconImage.SetMyIcon(self.DomainScene());

            long playerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());
            GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(self.DomainScene());
            float3 colorValue = gamePlayComponent.GetPlayerColor(playerId);
            Color color = new Color(colorValue.x, colorValue.y, colorValue.z);
            self.View.ES_AvatarShow.View.E_ImgLineImage.color = color;
        }

        public static void RefreshBtnShow(this DlgBattleTowerAR self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();

            self.View.ELabel_RefreshTextMeshProUGUI.ShowCoinCostTextInBattleTower(self.DomainScene(), gamePlayTowerDefenseComponent.model.RefreshBuyTowerCost).Coroutine();
        }
    }
}
