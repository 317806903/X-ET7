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

        }

        public static void ShowWindow(this DlgBattleTowerAR self, ShowWindowData contextData = null)
        {

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
            long playerId = PlayerHelper.GetMyPlayerId(self.DomainScene());
            if (gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.PutHome)
            {
                self.View.E_PutHomeAndMonsterPointImage.gameObject.SetActive(true);
                self.View.E_BattleImage.gameObject.SetActive(false);

                self.View.EButton_PutHomeButton.gameObject.SetActive(true);
                self.View.EButton_PutMonsterPointButton.gameObject.SetActive(false);
                if (gamePlayTowerDefenseComponent.ownerPlayerId == playerId)
                {
                    self.View.EButton_PutHomeButton.gameObject.GetComponent<SelectImage>().onPointerDown = self.OnSelectHeadQuarter;
                    self.View.ELabel_PutHomeText.text = $"请按住拖拉放置大本营";
                }
                else
                {
                    self.View.EButton_PutHomeButton.gameObject.GetComponent<SelectImage>().onPointerDown = null;
                    self.View.ELabel_PutHomeText.text = $"请等待房主放置大本营";
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
                if (putMonsterCallComponent != null && putMonsterCallComponent.MonsterCallUnitId != null && putMonsterCallComponent.MonsterCallUnitId.ContainsKey(playerId))
                {
                    self.View.EButton_PutMonsterPointButton.gameObject.GetComponent<SelectImage>().onPointerDown = null;
                    self.View.ELabel_PutMonsterPointText.text = $"请等待其他玩家放置刷怪点{putMonsterCallComponent.GetMonsterCallCount()}";
                }
                else
                {
                    self.View.EButton_PutMonsterPointButton.gameObject.GetComponent<SelectImage>().onPointerDown = self.OnSelectMonsterCall;
                    self.View.ELabel_PutMonsterPointText.text = $"请按住拖拉放置刷怪点";
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
            else if (gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.GameSuccess || gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.GameFailed)
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
            self.View.ELabel_TotalGoldText.text = $"{self.GetMyGold()}";
        }

        public static void SetCurLeftTimeInfo(this DlgBattleTowerAR self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
            GamePlayTowerDefenseStatus gamePlayTowerDefenseStatus = gamePlayTowerDefenseComponent.gamePlayTowerDefenseStatus;
            if (gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.RestTime)
            {
                RestTimeComponent restTimeComponent = gamePlayTowerDefenseComponent.GetComponent<RestTimeComponent>();
                self.curLeftTime = (long)(restTimeComponent.duration*1000) + TimeHelper.ClientNow();
                self.curLeftTimeMsg = "下一波到来:{}s";
            }
            else if (gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.InTheBattle)
            {
                MonsterWaveCallComponent monsterWaveCallComponent = gamePlayTowerDefenseComponent.GetComponent<MonsterWaveCallComponent>();
                self.curLeftTime = (long)(monsterWaveCallComponent.duration*1000) + TimeHelper.ClientNow();
                self.curLeftTimeMsg = "当前波剩余时间:{}s";
            }
            else if (gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.GameSuccess || gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.GameFailed)
            {
                self.curLeftTimeMsg = "已结束";
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
            self.View.ELabel_LeftTimeText.text = self.curLeftTimeMsg.Replace("{}", leftTimeShow.ToString());
        }

        public static void ShowMonsterWaveInfo(this DlgBattleTowerAR self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
            MonsterWaveCallComponent monsterWaveCallComponent = gamePlayTowerDefenseComponent.GetComponent<MonsterWaveCallComponent>();
            self.View.ELabel_LeftMonsterWaveText.text = $"波数:{monsterWaveCallComponent.curIndex + 1}/{monsterWaveCallComponent.totalCount}";
        }

        public static async ETTask RefreshUI(this DlgBattleTowerAR self)
        {
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
                msg = "已达到最大放置数量";
            }
            else
            {
                msg = $"还可放置{leftCount}个预防塔";
            }
            self.View.E_LeftCallPlayerTowerCountTextMeshProUGUI.text = msg;

        }

        public static async ETTask QuitBattle(this DlgBattleTowerAR self)
        {
            ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioBack(self.DomainScene());

            string msg = "是否确认退出战斗?";
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

        public static Dictionary<string, int> GetOwnerTowerList(this DlgBattleTowerAR self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
            PlayerOwnerTowersComponent playerOwnerTowersComponent = gamePlayTowerDefenseComponent.GetComponent<PlayerOwnerTowersComponent>();
            long playerId = PlayerHelper.GetMyPlayerId(self.DomainScene());
            self.myOwnTowList.Clear();
            foreach (var list in playerOwnerTowersComponent.playerOwnerTowerId[playerId])
            {
                if (list.Value > 0)
                {
                    self.myOwnTowList[list.Key] = list.Value;
                }
            }
            return self.myOwnTowList;
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
            self.View.ELabel_RefreshText.text = $"刷新(消耗金币:{gamePlayTowerDefenseComponent.model.RefreshBuyTowerCost})";

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

        public static void OnSelectTower(this DlgBattleTowerAR self, string towerCfgId, UnitCfg unitCfg)
        {
            int ownTowerCount = self.GetOwnerTowerList()[towerCfgId];
            if (ownTowerCount <= 0)
            {
                return;
            }

            self.selectCfgType = UISelectCfgType.Tower;
            self.selectCfgId = towerCfgId;

            self.currentPlaceObj = new GameObject("currentPlaceObj");

            float resScale = unitCfg.ResScale;

            string pathName = self.GetUnitPrefabName(unitCfg);
            GameObject go = ResComponent.Instance.LoadAsset<GameObject>(pathName);

            GameObject goTmp = GameObject.Instantiate(go);
            goTmp.transform.SetParent(self.currentPlaceObj.transform);
            goTmp.transform.localPosition = Vector3.zero;
            goTmp.transform.localScale = Vector3.one * resScale;

            ResEffectCfg resEffectCfg = ResEffectCfgCategory.Instance.Get("ResEffect_TowerShow");
            GameObject resEffectGo = ResComponent.Instance.LoadAsset<GameObject>(resEffectCfg.ResName);

            GameObject resEffectGoTmp = GameObject.Instantiate(resEffectGo);
            resEffectGoTmp.transform.SetParent(self.currentPlaceObj.transform);
            resEffectGoTmp.transform.localPosition = Vector3.zero;
            resEffectGoTmp.transform.localScale = Vector3.one * resScale;

            Transform attackAreaTran = resEffectGoTmp.transform.Find("AttackArea");
            Transform defaultShowTran = resEffectGoTmp.transform.Find("DefaultShow");
            for (int i = 0; i < resEffectGoTmp.transform.childCount; i++)
            {
                Transform child = resEffectGoTmp.transform.GetChild(i);
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

            self.currentPlaceObj.gameObject.SetActive(false);
        }

        public static void AddTowerItemRefreshListener(this DlgBattleTowerAR self, Transform transform, int index)
        {
            Scroll_Item_Tower itemTower = self.ScrollItemTowers[index].BindTrans(transform);

            List<string> list = self.GetOwnerTowerList().Keys.ToList();

            TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(list[index]);
            UnitCfg unitCfg = UnitCfgCategory.Instance.Get(towerCfg.UnitId);

            string icon = self.GetUnitIcon(unitCfg);
            Sprite sprite = ResComponent.Instance.LoadAsset<Sprite>(icon);
            itemTower.ELabel_NumTextMeshProUGUI.text = $"{self.GetOwnerTowerList()[towerCfg.Id]}";
            string towerName = towerCfg.Name;
            if (string.IsNullOrEmpty(towerName))
            {
                towerName = unitCfg.Name;
            }
            itemTower.ELabel_NameTextMeshProUGUI.text = $"{towerName}";
            itemTower.EButton_SelectImage.sprite = sprite;
            SelectImage selectImage = itemTower.EButton_SelectButton.GetComponent<SelectImage>();
            selectImage.onPointerDown = () => { self.OnSelectTower(towerCfg.Id, unitCfg); };
        }

        public static void AddTowerBuyListener(this DlgBattleTowerAR self, Transform transform, int index)
        {
            Scroll_Item_TowerBuy itemTankBuy = self.ScrollItemTowerBuy[index].BindTrans(transform);

            List<string> list = self.GetTowerBuyList();
            List<bool> listBought = self.GetTowerBoughtsList();

            TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(list[index]);
            UnitCfg unitCfg = UnitCfgCategory.Instance.Get(towerCfg.UnitId);

            string icon = self.GetUnitIcon(unitCfg);
            Sprite sprite = ResComponent.Instance.LoadAsset<Sprite>(icon);
            if (listBought[index])
            {
                itemTankBuy.ELabel_ContentText.text = $"已买";
                itemTankBuy.EButton_BuyButton.gameObject.SetActive(false);
            }
            else
            {
                itemTankBuy.ELabel_ContentText.text = $"{towerCfg.BuyTowerCostGold}";
                itemTankBuy.EButton_BuyButton.gameObject.SetActive(true);
            }
            itemTankBuy.EButton_SelectImage.sprite = sprite;
            itemTankBuy.EButton_BuyButton.AddListener(() =>
            {
                self.BuyTower(index).Coroutine();
            });
            string towerName = towerCfg.Name;
            if (string.IsNullOrEmpty(towerName))
            {
                towerName = unitCfg.Name;
            }
            itemTankBuy.EButton_nameTextMeshProUGUI.text = $"{towerName}";
            if (towerCfg.BuyTowerCostGold <= self.GetMyGold())
            {
                itemTankBuy.ELabel_BuyText.text = $"购买";
            }
            else
            {
                itemTankBuy.ELabel_BuyText.text = $"金币不足";
            }
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

                self.CheckCanPut(self.currentPlaceObj.transform.position);
            }
            else if (self.isDragging)
            {
                if (self.isClickUGUI == false)
                {
                    if (self.isRaycast == false)
                    {
                        string tipMsg = $"当前放置位置 没有投射点";
                        ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                        self.CheckIfPlaceSuccess();
                        return;
                    }
                    if (self.isCliffy)
                    {
                        string tipMsg = $"当前放置位置 太陡峭";
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

        public static bool CheckCanPut(this DlgBattleTowerAR self, Vector3 position)
        {
            long leftTime = self.curTipTime - TimeHelper.ClientNow();
            if (leftTime > 0)
            {
                return false;
            }
            self.curTipTime = TimeHelper.ClientNow() + 800;

            if (self.isClickUGUI)
            {
                string tipMsg = $"当前手指在UI上,请挪开";
                ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                return false;
            }

            if (self.isRaycast == false)
            {
                string tipMsg = $"当前放置位置 没有投射点";
                ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                return false;
            }

            if (self.isCliffy)
            {
                string tipMsg = $"当前放置位置 太陡峭";
                ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                return false;
            }

            if (self.selectCfgType == UISelectCfgType.MonsterCall)
            {
                if (self.canPutMonsterCall == false)
                {
                    string tipMsg = $"当前放置位置 没法到达大本营,请重新选位置";
                    ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                    return false;
                }
            }
            if (self.selectCfgType == UISelectCfgType.Tower)
            {
                if (self.ChkIsNearTower(self.selectCfgId, position))
                {
                    string tipMsg = $"当前放置位置 太靠近塔";
                    ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                    return false;
                }

                long myPlayerId = PlayerHelper.GetMyPlayerId(self.DomainScene());
                GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = ET.Client.GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
                (bool bRet, string msg) = gamePlayTowerDefenseComponent.ChkCallPlayerTower(myPlayerId, self.selectCfgId);
                if (bRet == false)
                {
                    string tipMsg = msg;
                    ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                    return false;
                }

                TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(self.selectCfgId);
                UnitCfg unitCfg = UnitCfgCategory.Instance.Get(towerCfg.UnitId);
                float radius = unitCfg.BodyRadius * unitCfg.ResScale;

                if (ET.Client.PathLineRendererComponent.Instance.ChkIsHitPath(position, radius))
                {
                    string tipMsg = "不能放置在路线上";
                    ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                    return false;
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

        public static async ETTask DoPutMonsterCall(this DlgBattleTowerAR self, float3 position)
        {
            ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioTowerPush(self.DomainScene());

            if (self.canPutMonsterCall == false)
            {
                string tipMsg = $"当前放置位置 没法到达大本营,请重新选位置";
                ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                self.CheckIfPlaceSuccess();
                return;
            }

            (bool bRet, string msg) = await ET.Client.GamePlayTowerDefenseHelper.SendPutMonsterCall(self.ClientScene(), self.selectCfgId, position);
        }

        public static async ETTask DoPutOwnTower(this DlgBattleTowerAR self, float3 position)
        {
            if (self.ChkIsNearTower(self.selectCfgId, position))
            {
                string tipMsg = $"当前放置位置 太靠近塔";
                ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                self.CheckIfPlaceSuccess();
                return;
            }

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
            UnitCfg unitCfg = UnitCfgCategory.Instance.Get(towerCfg.UnitId);
            float radius = unitCfg.BodyRadius * unitCfg.ResScale;

            if (ET.Client.PathLineRendererComponent.Instance.ChkIsHitPath(position, radius))
            {
                string tipMsg = "不能放置在路线上";
                ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                self.CheckIfPlaceSuccess();
                return;
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
            screenPosition += new Vector3(-160, 30, 0);

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
                self.currentPlaceObj.transform.localEulerAngles = new Vector3(0, 60, 0);

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
                long myPlayerId = PlayerHelper.GetMyPlayerId(self.DomainScene());
                await PathLineRendererComponent.Instance.ShowPath(myPlayerId, false, null);
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

        public static bool ChkIsNearTower(this DlgBattleTowerAR self, string towerId, float3 pos)
        {
            TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(towerId);
            float3 targetPos = pos;
            float targetUnitRadius = ET.Ability.UnitHelper.GetBodyRadius(towerCfg.UnitId, false, false);
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
            return gamePlayTowerDefenseComponent.ChkIsNearTower(targetPos, targetUnitRadius + 0.5f);
        }

        public static async ETTask ShowMesh(this DlgBattleTowerAR self)
        {
#if !UNITY_EDITOR
            return;
#endif
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