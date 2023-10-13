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
            self.CheckIfPlaceSuccess();
            self.SetStep();
            self.RefreshCoin();

            self.ShowMyTowers();

            self._groundLayerMask = LayerMask.GetMask("Map");

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
                // if (gamePlayTowerDefenseComponent.ownerPlayerId == playerId)
                // {
                //     self.View.EButton_PutHomeButton.gameObject.GetComponent<SelectImage>().onPointerDown = self.OnSelectHeadQuarter;
                //     self.View.ELabel_PutHomeText.text = $"请按住拖拉放置大本营";
                // }
                // else
                // {
                //     self.View.EButton_PutHomeButton.gameObject.GetComponent<SelectImage>().onPointerDown = null;
                //     self.View.ELabel_PutHomeText.text = $"请等待房主放置大本营";
                // }


                PutHomeComponent putHomeComponent = gamePlayTowerDefenseComponent.GetComponent<PutHomeComponent>();
                if (putHomeComponent != null)
                {
                    (bool bCanPutHome, bool isDonePut) = putHomeComponent.ChkCanPutHome(myPlayerId);
                    if (bCanPutHome)
                    {
                        self.View.EButton_PutHomeButton.gameObject.GetComponent<SelectImage>().onPointerDown = self.OnSelectHeadQuarter;
                        self.View.ELabel_PutHomeText.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_PutHomeText1");
                    }
                    else
                    {
                        self.View.EButton_PutHomeButton.gameObject.GetComponent<SelectImage>().onPointerDown = null;
                        if (isDonePut)
                        {
                            self.View.ELabel_PutHomeText.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_PutHomeText2");
                        }
                        else
                        {
                            self.View.ELabel_PutHomeText.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_PutHomeText3");
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
                    self.View.EButton_PutMonsterPointButton.gameObject.GetComponent<SelectImage>().onPointerDown = null;
                    self.View.ELabel_PutMonsterPointText.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_PutMonsterPoint1", putMonsterCallComponent.GetMonsterCallCount());
                }
                else
                {
                    self.View.EButton_PutMonsterPointButton.gameObject.GetComponent<SelectImage>().onPointerDown = self.OnSelectMonsterCall;
                    self.View.ELabel_PutMonsterPointText.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_PutMonsterPoint2");
                }
            }
            else if (gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.ShowStartEffect)
            {
                self.View.E_PutHomeAndMonsterPointImage.gameObject.SetActive(false);
                self.View.E_BattleImage.gameObject.SetActive(false);

                UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgBattleTowerBegin>().Coroutine();
            }
            else if (gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.RestTime)
            {
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
                self.SetCurLeftTimeInfo();
                self.NotTowerBuyShowWhenBattle().Coroutine();

                UIManagerHelper.GetUIComponent(self.DomainScene()).HideAllShownWindow();
                UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgBattleTowerEnd>().Coroutine();
            }
            else
            {
            }

            self.gamePlayTowerDefenseStatus = gamePlayTowerDefenseStatus;
        }

        public static int GetMyGold(this DlgBattleTowerAR self)
        {
            GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(self.DomainScene());
            long playerId = PlayerHelper.GetMyPlayerId(self.DomainScene());
            int curGoldValue = gamePlayComponent.GetPlayerCoin(playerId, CoinType.Gold);
            return curGoldValue;
        }

        public static void RefreshCoin(this DlgBattleTowerAR self)
        {
            //self.View.ELabel_TotalGoldText.text = $"{self.GetMyGold()}";

            self.View.ELabel_TotalGoldText.transform.GetComponent<UITextLocalizeMonoView>().DynamicSet(self.GetMyGold());
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
            self.View.ELabel_LeftTimeText.text = self.curLeftTimeMsg.Replace("{0}", leftTimeShow.ToString());
        }

        public static void ShowMonsterWaveInfo(this DlgBattleTowerAR self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
            MonsterWaveCallComponent monsterWaveCallComponent = gamePlayTowerDefenseComponent.GetComponent<MonsterWaveCallComponent>();

            PlayerComponent playerComponent = ET.Client.PlayerHelper.GetMyPlayerComponent(self.DomainScene());
            PlayerGameMode playerGameMode = playerComponent.PlayerGameMode;
            ARRoomType arRoomType = playerComponent.ARRoomType;
            if (playerGameMode == PlayerGameMode.ARRoom && arRoomType == ARRoomType.EndlessChallenge)
            {
                self.View.ELabel_LeftMonsterWaveText.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_CurMonsterWaveIndex_EndlessChallenge",monsterWaveCallComponent.curIndex + 1, monsterWaveCallComponent.totalCount);
            }
            else
            {
                self.View.ELabel_LeftMonsterWaveText.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_CurMonsterWaveIndex",monsterWaveCallComponent.curIndex + 1, monsterWaveCallComponent.totalCount);
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

            int leftCount = gamePlayTowerDefenseComponent.GetLeftCallPlayerTowerCount(myPlayerId);
            string msg = "";
            if (leftCount == 0)
            {
                msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_MaxPutTowerCount");
            }
            else
            {
                msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_CurPutTowerCount", leftCount);
            }
            self.View.E_LeftCallPlayerTowerCountTextMeshProUGUI.text = msg;

        }

        public static async ETTask QuitBattle(this DlgBattleTowerAR self)
        {
            ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioBack(self.DomainScene());

            string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_IsQuitBattle");
            ET.Client.UIManagerHelper.ShowConfirm(self.DomainScene(), msg, () =>
            {
                self._QuitBattle().Coroutine();
            }, null);
        }

        public static async ETTask _QuitBattle(this DlgBattleTowerAR self)
        {
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
            ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioClick(self.DomainScene());

            self.View.E_TowerBuyShowImage.gameObject.SetActive(true);
            self.View.EButton_BuyButton.gameObject.SetActive(false);

            int countTank = self.GetTowerBuyList().Count;
            self.AddUIScrollItems(ref self.ScrollItemTowerBuy, countTank);
            self.View.ELoopScrollList_BuyLoopHorizontalScrollRect.SetVisible(true, countTank);

            self.View.EButton_RefreshButton.AddListenerAsync(self.RefreshBuyTower);
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();

            self.View.ELabel_RefreshText.transform.GetComponent<UITextLocalizeMonoView>().DynamicSet(gamePlayTowerDefenseComponent.model.RefreshBuyTowerCost);
        }

        public static async ETTask CloseTowerBuyShow(this DlgBattleTowerAR self)
        {
            ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioClick(self.DomainScene());

            self.View.E_TowerBuyShowImage.gameObject.SetActive(false);
            self.View.EButton_BuyButton.gameObject.SetActive(true);
        }

        public static async ETTask NotTowerBuyShowWhenBattle(this DlgBattleTowerAR self)
        {
            self.View.E_TowerBuyShowImage.gameObject.SetActive(false);
            self.View.EButton_BuyButton.gameObject.SetActive(false);
            self.View.EButton_ReadyWhenRestTimeButton.gameObject.SetActive(false);
        }

        public static string GetUnitPrefabName(this DlgBattleTowerAR self, UnitCfg unitCfg)
        {
            ResUnitCfg resUnitCfg = ResUnitCfgCategory.Instance.Get(unitCfg.ResId);
            return resUnitCfg.ResName;
        }

        public static string GetUnitIcon(this DlgBattleTowerAR self, UnitCfg unitCfg)
        {
            ResIconCfg resIconCfg = ResIconCfgCategory.Instance.Get(unitCfg.Icon);
            return resIconCfg.ResName;
        }

        public static void OnSelectTower(this DlgBattleTowerAR self, string towerCfgId)
        {
            int ownTowerCount = self.myOwnTowerDic[towerCfgId];
            if (ownTowerCount <= 0)
            {
                return;
            }

            self.selectCfgType = UISelectCfgType.Tower;
            self.selectCfgId = towerCfgId;

            self.currentPlaceObj = new GameObject("currentPlaceObj");

            TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(towerCfgId);
            bool isTower = towerCfg.Type is PlayerTowerType.Tower;

            for (int i = 0; i < towerCfg.UnitId.Count; i++)
            {
                string unitCfgId = towerCfg.UnitId[i];
                float3 releativePos = float3.zero;
                if (towerCfg.RelativePosition.Count > i)
                {
                    releativePos = new float3(towerCfg.RelativePosition[i].X, towerCfg.RelativePosition[i].Y, towerCfg.RelativePosition[i].Z);
                }
                UnitCfg unitCfg = UnitCfgCategory.Instance.Get(unitCfgId);
                float resScale = unitCfg.ResScale;

                float3 forward = new float3(0, 0, 1);
                string pathName = self.GetUnitPrefabName(unitCfg);
                GameObject go = ResComponent.Instance.LoadAsset<GameObject>(pathName);

                GameObject goTmp = GameObject.Instantiate(go);
                goTmp.transform.SetParent(self.currentPlaceObj.transform);
                goTmp.transform.localPosition = releativePos;
                goTmp.transform.localScale = Vector3.one * resScale;
                goTmp.transform.forward = forward;

                if (isTower)
                {
                    ResEffectCfg resEffectCfg = ResEffectCfgCategory.Instance.Get("ResEffect_TowerShow");
                    GameObject resEffectGo = ResComponent.Instance.LoadAsset<GameObject>(resEffectCfg.ResName);

                    GameObject resEffectGoTmp = GameObject.Instantiate(resEffectGo);
                    resEffectGoTmp.transform.SetParent(self.currentPlaceObj.transform);
                    resEffectGoTmp.transform.localPosition = Vector3.zero;
                    resEffectGoTmp.transform.localScale = Vector3.one * resScale;

                    Transform attackAreaTran = resEffectGoTmp.transform.Find("AttackArea");
                    Transform defaultShowTran = resEffectGoTmp.transform.Find("DefaultShow");
                    for (int i1 = 0; i1 < resEffectGoTmp.transform.childCount; i1++)
                    {
                        Transform child = resEffectGoTmp.transform.GetChild(i1);
                        if (child == attackAreaTran || child == defaultShowTran)
                        {
                            child.gameObject.SetActive(true);
                        }
                        else
                        {
                            child.gameObject.SetActive(false);
                        }
                    }
                    float skillDis = ET.Ability.UnitHelper.GetMaxSkillDis(unitCfg, SkillSlotType.NormalAttack);
                    attackAreaTran.localScale = Vector3.one * skillDis*2 / resScale;

                    long playerId = PlayerHelper.GetMyPlayerId(self.DomainScene());
                    GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(self.DomainScene());
                    float3 colorValue = gamePlayComponent.GetPlayerColor(playerId);
                    Color color = new Color(colorValue.x, colorValue.y, colorValue.z);
                    ParticleSystem[] psList = attackAreaTran.gameObject.GetComponentsInChildren<ParticleSystem>(true);
                    foreach (ParticleSystem particleSystem in psList)
                    {
                        ParticleSystem.MainModule mainModule = particleSystem.main;
                        float alpha = mainModule.startColor.color.a;
                        color.a = alpha;
                        mainModule.startColor = new ParticleSystem.MinMaxGradient(color);
                    }
                    psList = defaultShowTran.gameObject.GetComponentsInChildren<ParticleSystem>(true);
                    foreach (ParticleSystem particleSystem in psList)
                    {
                        ParticleSystem.MainModule mainModule = particleSystem.main;
                        float alpha = mainModule.startColor.color.a;
                        color.a = alpha;
                        mainModule.startColor = new ParticleSystem.MinMaxGradient(color);
                    }

                }
            }

            self.currentPlaceObj.gameObject.SetActive(false);
        }

        public static void AddTowerItemRefreshListener(this DlgBattleTowerAR self, Transform transform, int index)
        {
            Scroll_Item_Tower itemTower = self.ScrollItemTowers[index].BindTrans(transform);

            List<string> list = self.GetOwnerTowerList();

            TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(list[index]);

            string towerName = towerCfg.Name;
            if (string.IsNullOrEmpty(towerName))
            {
                UnitCfg unitCfg = UnitCfgCategory.Instance.Get(towerCfg.UnitId[0]);
                towerName = unitCfg.Name;
            }

            string icon = "";
            if (string.IsNullOrEmpty(towerCfg.Icon))
            {
                UnitCfg unitCfg = UnitCfgCategory.Instance.Get(towerCfg.UnitId[0]);
                icon = self.GetUnitIcon(unitCfg);
            }
            else
            {
                ResIconCfg resIconCfg = ResIconCfgCategory.Instance.Get(towerCfg.Icon);
                icon = resIconCfg.ResName;
            }

            Sprite sprite = ResComponent.Instance.LoadAsset<Sprite>(icon);
            itemTower.ELabel_NumTextMeshProUGUI.text = $"{self.myOwnTowerDic[towerCfg.Id]}";

            itemTower.ELabel_NameTextMeshProUGUI.text = $"{towerName}";
            itemTower.EButton_TowerIcoImage.sprite = sprite;
            SelectImage selectImage = itemTower.EButton_SelectButton.GetComponent<SelectImage>();
            selectImage.onPointerDown = () =>
            {
                self.OnSelectTower(towerCfg.Id);
            };

            itemTower.EButton_SelectButton.AddListener(() =>
            {
                itemTower.EButton_SelectButton.AddListener(() =>
                {
                    UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgBattleTowerHUD>(new DlgBattleTowerHUD_ShowWindowData()
                    {
                        towerCfgId = towerCfg.Id,
                    }).Coroutine();
                });
            });
			
            itemTower.EG_IconStarRectTransform.SetVisible(true);
            int starCount = towerCfg.Level[0];
            itemTower.E_IconStar1Image.gameObject.SetActive(starCount>=1);
            itemTower.E_IconStar2Image.gameObject.SetActive(starCount>=2);
            itemTower.E_IconStar3Image.gameObject.SetActive(starCount>=3);
        }

        public static void AddTowerBuyListener(this DlgBattleTowerAR self, Transform transform, int index)
        {
            Scroll_Item_TowerBuy itemTowerBuy = self.ScrollItemTowerBuy[index].BindTrans(transform);

            List<string> list = self.GetTowerBuyList();
            List<bool> listBought = self.GetTowerBoughtsList();

            string towerCfgId = list[index];
            TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(towerCfgId);

            string towerName = towerCfg.Name;
            if (string.IsNullOrEmpty(towerName))
            {
                UnitCfg unitCfg = UnitCfgCategory.Instance.Get(towerCfg.UnitId[0]);
                towerName = unitCfg.Name;
            }

            string icon = "";
            if (string.IsNullOrEmpty(towerCfg.Icon))
            {
                UnitCfg unitCfg = UnitCfgCategory.Instance.Get(towerCfg.UnitId[0]);
                icon = self.GetUnitIcon(unitCfg);
            }
            else
            {
                ResIconCfg resIconCfg = ResIconCfgCategory.Instance.Get(towerCfg.Icon);
                icon = resIconCfg.ResName;
            }

            Sprite sprite = ResComponent.Instance.LoadAsset<Sprite>(icon);
            if (listBought[index])
            {
                itemTowerBuy.ELabel_ContentText.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_TowerBuyDone");
                itemTowerBuy.EButton_BuyButton.gameObject.SetActive(false);
            }
            else
            {
                itemTowerBuy.ELabel_ContentText.text = $"{towerCfg.BuyTowerCostGold}";
                itemTowerBuy.EButton_BuyButton.gameObject.SetActive(true);
            }
            itemTowerBuy.EButton_IconImage.sprite = sprite;
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
            int starCount = towerCfg.Level[0];
            itemTowerBuy.E_IconStar1Image.gameObject.SetActive(starCount>=1);
            itemTowerBuy.E_IconStar2Image.gameObject.SetActive(starCount>=2);
            itemTowerBuy.E_IconStar3Image.gameObject.SetActive(starCount>=3);

            itemTowerBuy.EButton_SelectButton.AddListener(() =>
            {
                UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgBattleTowerHUD>(new DlgBattleTowerHUD_ShowWindowData()
                {
                    towerCfgId = towerCfgId,
                }).Coroutine();
            });
        }

        public static async ETTask BuyTower(this DlgBattleTowerAR self, int index)
        {
            ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioConfirm(self.DomainScene());

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
            ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioClick(self.DomainScene());

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
            ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioClick(self.DomainScene());

            await ET.Client.GamePlayTowerDefenseHelper.SendReadyWhenRestTime(self.ClientScene());
        }

        public static void ChgScrollRectMoveStatus(this DlgBattleTowerAR self, bool status)
        {
            self.View.ELoopScrollList_TowerLoopHorizontalScrollRect.enabled = status;
        }

        public static void Update(this DlgBattleTowerAR self)
        {
            self.ShowCurLeftTimeInfo();

            if (self.currentPlaceObj == null)
            {
                return;
            }

            //Log.Debug($"--- Update 44 self.currentPlaceObj[{self.currentPlaceObj}] self.isDragging[{self.isDragging}] self.isPlaceSuccess[{self.isPlaceSuccess}]");
            if (self.CheckUserInput())
            {
                self.isClickUGUI = ET.UGUIHelper.IsClickUGUI();
                if (self.isClickUGUI)
                {
                    return;
                }

                self.isDragging = true;
                self.ChgScrollRectMoveStatus(false);
                self.MoveCurrentPlaceObj();

                bool canPut = self.ChkCanPut(self.currentPlaceObj.transform.position);
                if (canPut)
                {
                    self.View.E_TipNodeImage.SetVisible(false);
                    self.ChgCurrentPlaceObj(true);
                }
                else
                {
                    //self.currentPlaceObj.gameObject.SetActive(false);
                    self.ChgCurrentPlaceObj(false);
                }
            }
            else if (self.isDragging)
            {
                if (self.isClickUGUI == false)
                {
                    if (self.isRaycast == false)
                    {
                        string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMesh_IsRaycast");
                        ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                        self.CheckIfPlaceSuccess();
                        return;
                    }
                    if (self.isCliffy)
                    {
                        string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMesh_IsCliffy");
                        ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                        self.CheckIfPlaceSuccess();
                        return;
                    }

                    var position = self.currentPlaceObj.transform.position;
                    if (self.selectCfgType == UISelectCfgType.HeadQuarter)
                    {
                        ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioTowerPush(self.DomainScene());

                        ET.Client.GamePlayTowerDefenseHelper.SendPutHome(self.ClientScene(), self.selectCfgId, position).Coroutine();
                    }
                    else if (self.selectCfgType == UISelectCfgType.MonsterCall)
                    {
                        self.DoPutMonsterCall(position).Coroutine();
                    }
                    else if (self.selectCfgType == UISelectCfgType.Tower)
                    {
                        self.DoPutOwnTower(position).Coroutine();
                    }
                }

                self.CheckIfPlaceSuccess();
            }
            else if (self.currentPlaceObj != null)
            {
                self.CheckIfPlaceSuccess();
            }
        }

        public static void ChgCurrentPlaceObj(this DlgBattleTowerAR self, bool canPut)
        {
            Color colorNew;
            if (canPut)
            {
                colorNew = Color.white;
            }
            else
            {
                colorNew = Color.red;
            }

            MeshRenderer[] rendererList = self.currentPlaceObj.gameObject.GetComponentsInChildren<MeshRenderer>(true);
            foreach (MeshRenderer renderer in rendererList)
            {
                foreach (var material in renderer.materials)
                {
                    Color color = material.color;
                    material.color = new Color(colorNew.r, colorNew.g, colorNew.b, color.a);
                }
            }
            SkinnedMeshRenderer[] rendererList2 = self.currentPlaceObj.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>(true);
            foreach (SkinnedMeshRenderer renderer in rendererList2)
            {
                foreach (var material in renderer.materials)
                {
                    Color color = material.color;
                    material.color = new Color(colorNew.r, colorNew.g, colorNew.b, color.a);
                }
            }
        }
        public static bool ChkCanPut(this DlgBattleTowerAR self, Vector3 position)
        {
            long leftTime = self.curTipTime - TimeHelper.ClientNow();
            if (leftTime > 0)
            {
                return false;
            }
            //self.curTipTime = TimeHelper.ClientNow() + 800;
            self.curTipTime = TimeHelper.ClientNow() + 0;

            if (self.isClickUGUI)
            {
                string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMesh_IsOnUI");
                self.ShowPutTipMsg(tipMsg);
                return false;
            }

            if (self.isRaycast == false)
            {
                string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMesh_IsRaycast");
                self.ShowPutTipMsg(tipMsg);
                return false;
            }

            if (self.isCliffy)
            {
                string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMesh_IsCliffy");
                self.ShowPutTipMsg(tipMsg);
                return false;
            }

            if (self.selectCfgType == UISelectCfgType.MonsterCall)
            {
                if (self.canPutMonsterCall == false)
                {
                    string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMesh_IsReachHome");
                    self.ShowPutTipMsg(tipMsg);
                    return false;
                }
            }
            if (self.selectCfgType == UISelectCfgType.Tower)
            {
                TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(self.selectCfgId);
                float radius = 0;
                if (towerCfg.Radius <= 0)
                {
                    UnitCfg unitCfg = UnitCfgCategory.Instance.Get(towerCfg.UnitId[0]);
                    radius = unitCfg.BodyRadius * unitCfg.ResScale;
                }
                else
                {
                    radius = towerCfg.Radius;
                }

                bool isTower = towerCfg.Type is PlayerTowerType.Tower;
                bool isCallMonster = towerCfg.Type is PlayerTowerType.CallMonster;

                long myPlayerId = PlayerHelper.GetMyPlayerId(self.DomainScene());
                GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = ET.Client.GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
                (bool bRet, string msg) = gamePlayTowerDefenseComponent.ChkCallPlayerTower(myPlayerId, self.selectCfgId);
                if (bRet == false)
                {
                    string tipMsg = msg;
                    self.ShowPutTipMsg(tipMsg);
                    return false;
                }

                if (isTower)
                {
                    if (self.ChkIsNearTower(self.selectCfgId, position))
                    {
                        string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMesh_IsNearTower");
                        self.ShowPutTipMsg(tipMsg);
                        return false;
                    }

                    if (ET.Client.PathLineRendererComponent.Instance.ChkIsHitPath(position, radius))
                    {
                        string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMesh_IsOnRoad");
                        self.ShowPutTipMsg(tipMsg);
                        return false;
                    }
                }
                if (isCallMonster)
                {
                    if (self.ChkIsNearTower(self.selectCfgId, position) == false)
                    {
                        string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMesh_IsNotNearTower");
                        self.ShowPutTipMsg(tipMsg);
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        ///检测用户当前输入
        /// </summary>
        /// <returns></returns>
        public static bool CheckUserInput(this DlgBattleTowerAR self)
        {
            return Input.GetMouseButton(0);
        }

        public static void ShowPutTipMsg(this DlgBattleTowerAR self, string tipMsg)
        {
            self.View.E_TipNodeImage.SetVisible(true);
            self.View.E_TipTextTextMeshProUGUI.text = tipMsg;
        }

        public static async ETTask DoPutMonsterCall(this DlgBattleTowerAR self, float3 position)
        {
            ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioTowerPush(self.DomainScene());

            if (self.canPutMonsterCall == false)
            {
                string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMesh_IsReachHome");
                ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                self.CheckIfPlaceSuccess();
                return;
            }

            (bool bRet, string msg) = await ET.Client.GamePlayTowerDefenseHelper.SendPutMonsterCall(self.ClientScene(), self.selectCfgId, position);
        }

        public static async ETTask DoPutOwnTower(this DlgBattleTowerAR self, float3 position)
        {
            ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioTowerPush(self.DomainScene());

            long myPlayerId = PlayerHelper.GetMyPlayerId(self.DomainScene());
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = ET.Client.GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
            (bool bRet, string msg) = gamePlayTowerDefenseComponent.ChkCallPlayerTower(myPlayerId, self.selectCfgId);
            if (bRet == false)
            {
                string tipMsg = msg;
                ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                self.CheckIfPlaceSuccess();
                return;
            }

            TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(self.selectCfgId);
            bool isTower = towerCfg.Type is PlayerTowerType.Tower;
            bool isCallMonster = towerCfg.Type is PlayerTowerType.CallMonster;

            float radius = 0;
            if (towerCfg.Radius <= 0)
            {
                UnitCfg unitCfg = UnitCfgCategory.Instance.Get(towerCfg.UnitId[0]);
                radius = unitCfg.BodyRadius * unitCfg.ResScale;
            }
            else
            {
                radius = towerCfg.Radius;
            }

            if (isTower)
            {
                if (self.ChkIsNearTower(self.selectCfgId, position))
                {
                    string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMesh_IsNearTower");
                    ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                    self.CheckIfPlaceSuccess();
                    return;
                }

                if (ET.Client.PathLineRendererComponent.Instance.ChkIsHitPath(position, radius))
                {
                    string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMesh_IsOnRoad");
                    ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                    self.CheckIfPlaceSuccess();
                    return;
                }
            }

            if (isCallMonster)
            {
                if (self.ChkIsNearTower(self.selectCfgId, position) == false)
                {
                    string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMesh_IsNotNearTower");
                    ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                    self.CheckIfPlaceSuccess();
                    return;
                }
            }
            ET.Client.GamePlayTowerDefenseHelper.SendCallOwnTower(self.ClientScene(), self.selectCfgId, position).Coroutine();
        }

        /// <summary>
        ///让当前对象跟随鼠标移动
        /// </summary>
        public static void MoveCurrentPlaceObj(this DlgBattleTowerAR self)
        {
            if (self.currentPlaceObj == null)
            {
                return;
            }
            Vector3 screenPosition = Input.mousePosition;
            screenPosition += new Vector3(-130, 30, 0);

            Ray ray = ET.Client.CameraHelper.GetMainCamera(self.DomainScene()).ScreenPointToRay(screenPosition);
            RaycastHit hitInfo;
            Vector3 point = Vector3.zero;
            self.isRaycast = false;
            self.isCliffy = false;
            if (Physics.Raycast(ray, out hitInfo, 10000, self._groundLayerMask))
            {
                self.isRaycast = true;
                point = hitInfo.point;

                Vector3 normal = hitInfo.normal;
                //大概是66.6度
                if (Vector3.Dot(normal, Vector3.up) < 0.5f)
                {
                    float totalHeight = 0;
                    int hitCount = 0;
                    float hitPointHeight = 0;
                    hitPointHeight = self.GetHitPointHeight(ray.origin + new Vector3(0.5f, 0, 0));
                    if (hitPointHeight != 0)
                    {
                        hitCount++;
                    }
                    hitPointHeight = self.GetHitPointHeight(ray.origin + new Vector3(-0.5f, 0, 0));
                    if (hitPointHeight != 0)
                    {
                        hitCount++;
                    }
                    hitPointHeight = self.GetHitPointHeight(ray.origin + new Vector3(0, 0, 0.5f));
                    if (hitPointHeight != 0)
                    {
                        hitCount++;
                    }
                    hitPointHeight = self.GetHitPointHeight(ray.origin + new Vector3(0, 0, -0.5f));
                    if (hitPointHeight != 0)
                    {
                        hitCount++;
                    }

                    if (math.abs(totalHeight / hitCount - point.y) > 0.5f)
                    {
                        self.isCliffy = true;
                    }
                }
            }

            if (self.isRaycast)
            {
                if (self.currentPlaceObj.gameObject.activeSelf == false)
                {
                    self.currentPlaceObj.gameObject.SetActive(true);
                }
                self.currentPlaceObj.transform.position = point + new Vector3(0, self._YOffset, 0);
                self.currentPlaceObj.transform.localEulerAngles = new Vector3(0, 0, 0);

            }
            else
            {
                if (self.currentPlaceObj.gameObject.activeSelf)
                {
                    self.currentPlaceObj.gameObject.SetActive(false);
                }
            }

            self.DrawMonsterCall2HeadQuarter().Coroutine();
        }

        public static float GetHitPointHeight(this DlgBattleTowerAR self, Vector3 startPos)
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(startPos, Vector3.down, out hitInfo, 10000, self._groundLayerMask))
            {
                return hitInfo.point.y;
            }
            return 0;
        }

        public static async ETTask DrawMonsterCall2HeadQuarter(this DlgBattleTowerAR self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
            GamePlayTowerDefenseStatus gamePlayTowerDefenseStatus = gamePlayTowerDefenseComponent.gamePlayTowerDefenseStatus;
            if (gamePlayTowerDefenseStatus != GamePlayTowerDefenseStatus.PutMonsterPoint)
            {
                return;
            }

            if (self.newShowLineRendererTime > TimeHelper.ClientNow())
            {
                return;
            }

            self.newShowLineRendererTime = TimeHelper.ClientNow() + 100;

            if (self.currentPlaceObj.gameObject.activeSelf == false)
            {
                await self._HideMonsterCall2HeadQuarter();
                return;
            }

            float disX = math.abs(self.lineRendererPos.x - self.currentPlaceObj.transform.position.x);
            float disZ = math.abs(self.lineRendererPos.z - self.currentPlaceObj.transform.position.z);
            if (disX > 0.3f || disZ > 0.3f)
            {
                self.lineRendererPos = self.currentPlaceObj.transform.position;
                self.canShowLineRendererNear = true;
                return;
            }
            if (disX < 0.1f && disZ < 0.1f && self.canShowLineRendererNear == false)
            {
                return;
            }
            self.lineRendererPos = self.currentPlaceObj.transform.position;
            self.canShowLineRendererNear = false;

            if (self.lineRendererReqing)
            {
                return;
            }

            self.lineRendererReqing = true;
            bool canArrive = await self._DrawMonsterCall2HeadQuarter(self.lineRendererPos);
            if (canArrive)
            {
                self.canPutMonsterCall = true;
            }
            else
            {
                self.canPutMonsterCall = false;
            }
            self.lineRendererReqing = false;
        }

        public static async ETTask<bool> _DrawMonsterCall2HeadQuarter(this DlgBattleTowerAR self, float3 pos)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = Client.GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
            return await gamePlayTowerDefenseComponent.DoDrawMyMonsterCall2HeadQuarter(pos);
        }

        public static async ETTask _HideMonsterCall2HeadQuarter(this DlgBattleTowerAR self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = Client.GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
            await gamePlayTowerDefenseComponent.DoHideMyMonsterCall2HeadQuarter();
        }

        /// <summary>
        ///检测是否放置成功
        /// </summary>
        public static void CheckIfPlaceSuccess(this DlgBattleTowerAR self)
        {
            self.isDragging = false;
            if (self.currentPlaceObj != null)
            {
                GameObject.Destroy(self.currentPlaceObj);
                self.currentPlaceObj = null;
            }
            self.View.E_TipNodeImage.SetVisible(false);
            self.ChgScrollRectMoveStatus(true);
        }

        public static void OnSelectHeadQuarter(this DlgBattleTowerAR self)
        {
            self.selectCfgType = UISelectCfgType.HeadQuarter;
            self.selectCfgId = "Unit_HeadQuarter";
            UnitCfg unitCfg = UnitCfgCategory.Instance.Get(self.selectCfgId);

            if (self.currentPlaceObj != null)
            {
                GameObject.DestroyImmediate(self.currentPlaceObj);
                self.currentPlaceObj = null;
            }
            string pathName = self.GetUnitPrefabName(unitCfg);
            GameObject go = ResComponent.Instance.LoadAsset<GameObject>(pathName);

            self.currentPlaceObj = GameObject.Instantiate(go);
            self.currentPlaceObj.SetActive(false);
        }

        public static void OnSelectMonsterCall(this DlgBattleTowerAR self)
        {
            self.selectCfgType = UISelectCfgType.MonsterCall;
            self.selectCfgId = "Unit_MonsterCall";
            UnitCfg unitCfg = UnitCfgCategory.Instance.Get(self.selectCfgId);

            if (self.currentPlaceObj != null)
            {
                GameObject.DestroyImmediate(self.currentPlaceObj);
                self.currentPlaceObj = null;
            }
            string pathName = self.GetUnitPrefabName(unitCfg);
            GameObject go = ResComponent.Instance.LoadAsset<GameObject>(pathName);

            self.currentPlaceObj = GameObject.Instantiate(go);
            self.currentPlaceObj.SetActive(false);
        }

        public static bool ChkIsNearTower(this DlgBattleTowerAR self, string towerCfgId, float3 pos)
        {
            TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(towerCfgId);
            float3 targetPos = pos;
            float radius = 0;
            if (towerCfg.Radius <= 0)
            {
                UnitCfg unitCfg = UnitCfgCategory.Instance.Get(towerCfg.UnitId[0]);
                radius = unitCfg.BodyRadius * unitCfg.ResScale;
            }
            else
            {
                radius = towerCfg.Radius;
            }

            bool isTower = towerCfg.Type is PlayerTowerType.Tower;
            bool isCallMonster = towerCfg.Type is PlayerTowerType.CallMonster;
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
            if (isTower)
            {
                return gamePlayTowerDefenseComponent.ChkIsNearTower(targetPos, radius + 0.3f, -1);
            }
            if (isCallMonster)
            {
                long myPlayerId = PlayerHelper.GetMyPlayerId(self.DomainScene());
                return gamePlayTowerDefenseComponent.ChkIsNearTower(targetPos, radius + 0.3f, myPlayerId);
            }

            return false;
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
    }
}