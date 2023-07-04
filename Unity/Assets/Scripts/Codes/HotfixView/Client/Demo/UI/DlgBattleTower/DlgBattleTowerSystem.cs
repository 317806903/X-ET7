using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

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
        }

        public static void ShowWindow(this DlgBattleTower self, Entity contextData = null)
        {
            
            self.SetStep();

            self.ShowMyTowers();

            self._groundLayerMask = LayerMask.GetMask("Map");

            self.Timer = TimerComponent.Instance.NewFrameTimer(TimerInvokeType.BattleTowerFrameTimer, self);
        }

        public static void ShowMyTowers(this DlgBattleTower self)
        {
            int countTower = self.GetOwnerTowerList().Count;
            self.AddUIScrollItems(ref self.ScrollItemTowers, countTower);
            self.View.ELoopScrollList_TowerLoopHorizontalScrollRect.SetVisible(true, countTower);

            self.CloseTowerBuyShow();
        }

        public static void HideWindow(this DlgBattleTower self)
        {
            TimerComponent.Instance?.Remove(ref self.Timer);
        }

        public static void SetStep(this DlgBattleTower self)
        {
            GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlayer(self.DomainScene());
            GamePlayStatus gamePlayStatus = gamePlayComponent.GamePlayStatus;
            long playerId = self.ClientScene().GetComponent<PlayerComponent>().MyId;
            if (gamePlayStatus == GamePlayStatus.PutHome)
            {
                self.View.E_PutHomeAndMonsterPointImage.gameObject.SetActive(true);
                self.View.E_BattleImage.gameObject.SetActive(false);

                self.View.EButton_PutHomeButton.gameObject.SetActive(true);
                self.View.EButton_PutMonsterPointButton.gameObject.SetActive(false);
                if (gamePlayComponent.ownerPlayerId == playerId)
                {
                    self.View.EButton_PutHomeButton.gameObject.GetComponent<SelectImage>().onPointerDown = self.OnSelectHeadQuarter;
                    self.View.ELabel_PutHomeText.text = $"请按住拖拉放置大本营";
                }
                else
                {
                    self.View.ELabel_PutHomeText.text = $"请等待房主放置大本营";
                }
            }
            else if (gamePlayStatus == GamePlayStatus.PutMonsterPoint)
            {
                self.View.E_PutHomeAndMonsterPointImage.gameObject.SetActive(true);
                self.View.E_BattleImage.gameObject.SetActive(false);
                
                self.View.EButton_PutHomeButton.gameObject.SetActive(false);
                self.View.EButton_PutMonsterPointButton.gameObject.SetActive(true);
                if (gamePlayComponent.ownerPlayerId == playerId)
                {
                    self.View.EButton_PutMonsterPointButton.gameObject.GetComponent<SelectImage>().onPointerDown = self.OnSelectMonsterCall;
                    self.View.ELabel_PutMonsterPointText.text = $"请按住拖拉放置刷怪点";
                }
                else
                {
                    self.View.ELabel_PutMonsterPointText.text = $"请等待房主放置刷怪点";
                }
            }
            else if (gamePlayStatus == GamePlayStatus.RestTime)
            {
                self.View.E_PutHomeAndMonsterPointImage.gameObject.SetActive(false);
                self.View.E_BattleImage.gameObject.SetActive(true);

                self.ShowMonsterWaveInfo();
                self.SetCurLeftTimeInfo();
                if (self.gamePlayStatus != GamePlayStatus.RestTime)
                {
                    self.TowerBuyShow();
                }
            }
            else if (gamePlayStatus == GamePlayStatus.InTheBattle)
            {
                self.View.E_PutHomeAndMonsterPointImage.gameObject.SetActive(false);
                self.View.E_BattleImage.gameObject.SetActive(true);

                self.ShowMonsterWaveInfo();
                self.SetCurLeftTimeInfo();
                self.NotTowerBuyShowWhenBattle();
            }
            else if (gamePlayStatus == GamePlayStatus.GameSuccess || gamePlayStatus == GamePlayStatus.GameFailed)
            {
                self.SetCurLeftTimeInfo();
                self.NotTowerBuyShowWhenBattle();
                
                self.ClientScene().GetComponent<UIComponent>().ShowWindowAsync(WindowID.WindowID_BattleTowerEnd).Coroutine();
            }
            else
            {
            }

            self.gamePlayStatus = gamePlayStatus;
        }
        
        public static void SetCurLeftTimeInfo(this DlgBattleTower self)
        {
            GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlayer(self.DomainScene());
            GamePlayStatus gamePlayStatus = gamePlayComponent.GamePlayStatus;
            if (gamePlayStatus == GamePlayStatus.RestTime)
            {
                RestTimeComponent restTimeComponent = gamePlayComponent.GetComponent<RestTimeComponent>();
                self.curLeftTime = (long)(restTimeComponent.duration*1000) + TimeHelper.ClientNow();
                self.curLeftTimeMsg = "下一波到来:{}s";
            }
            else if (gamePlayStatus == GamePlayStatus.InTheBattle)
            {
                MonsterWaveCallComponent monsterWaveCallComponent = gamePlayComponent.GetComponent<MonsterWaveCallComponent>();
                self.curLeftTime = (long)(monsterWaveCallComponent.duration*1000) + TimeHelper.ClientNow();
                self.curLeftTimeMsg = "当前波剩余时间:{}s";
            }
            else if (gamePlayStatus == GamePlayStatus.GameSuccess || gamePlayStatus == GamePlayStatus.GameFailed)
            {
                self.curLeftTimeMsg = "已结束";
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
            self.View.ELabel_LeftTimeText.text = self.curLeftTimeMsg.Replace("{}", leftTimeShow.ToString());
        }
        
        public static void ShowMonsterWaveInfo(this DlgBattleTower self)
        {
            GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlayer(self.DomainScene());
            MonsterWaveCallComponent monsterWaveCallComponent = gamePlayComponent.GetComponent<MonsterWaveCallComponent>();
            self.View.ELabel_LeftMonsterWaveText.text = $"波数:{monsterWaveCallComponent.curIndex + 1}/{monsterWaveCallComponent.totalCount}";
        }
        
        public static async ETTask RefreshUI(this DlgBattleTower self)
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
        }

        public static async ETTask QuitBattle(this DlgBattleTower self)
        {
            await RoomHelper.MemberQuitBattleAsync(self.ClientScene());
            await SceneHelper.EnterHall(self.ClientScene());
        }

        public static List<string> GetTowerBuyList(this DlgBattleTower self)
        {
            GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlayer(self.DomainScene());
            PlayerOwnerTowersComponent playerOwnerTowersComponent = gamePlayComponent.GetComponent<PlayerOwnerTowersComponent>();
            long playerId = self.ClientScene().GetComponent<PlayerComponent>().MyId;
            return playerOwnerTowersComponent.playerTowerBuyPools[playerId];
        }
        
        public static List<bool> GetTowerBoughtsList(this DlgBattleTower self)
        {
            GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlayer(self.DomainScene());
            PlayerOwnerTowersComponent playerOwnerTowersComponent = gamePlayComponent.GetComponent<PlayerOwnerTowersComponent>();
            long playerId = self.ClientScene().GetComponent<PlayerComponent>().MyId;
            return playerOwnerTowersComponent.playerTowerBuyPoolBoughts[playerId];
        }

        public static Dictionary<string, int> GetOwnerTowerList(this DlgBattleTower self)
        {
            GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlayer(self.DomainScene());
            PlayerOwnerTowersComponent playerOwnerTowersComponent = gamePlayComponent.GetComponent<PlayerOwnerTowersComponent>();
            long playerId = self.ClientScene().GetComponent<PlayerComponent>().MyId;
            return playerOwnerTowersComponent.playerOwnerTowerId[playerId];
        }

        public static async ETTask TowerBuyShow(this DlgBattleTower self)
        {
            self.View.E_TowerBuyShowImage.gameObject.SetActive(true);
            self.View.EButton_BuyButton.SetVisible(false);
            
            int countTank = self.GetTowerBuyList().Count;
            self.AddUIScrollItems(ref self.ScrollItemTowerBuy, countTank);
            self.View.ELoopScrollList_BuyLoopHorizontalScrollRect.SetVisible(true, countTank);

            self.View.EButton_RefreshButton.AddListenerAsync(self.RefreshBuyTower);
        }

        public static async ETTask CloseTowerBuyShow(this DlgBattleTower self)
        {
            self.View.E_TowerBuyShowImage.gameObject.SetActive(false);
            self.View.EButton_BuyButton.SetVisible(true);
        }
        
        public static async ETTask NotTowerBuyShowWhenBattle(this DlgBattleTower self)
        {
            self.View.E_TowerBuyShowImage.gameObject.SetActive(false);
            self.View.EButton_BuyButton.SetVisible(false);
        }

        public static string GetUnitPrefabName(this DlgBattleTower self, UnitCfg unitCfg)
        {
            ResUnitCfg resUnitCfg = ResUnitCfgCategory.Instance.Get(unitCfg.ResId);
            return resUnitCfg.ResName;
        }

        public static string GetUnitIcon(this DlgBattleTower self, UnitCfg unitCfg)
        {
            ResIconCfg resIconCfg = ResIconCfgCategory.Instance.Get(unitCfg.Icon);
            return resIconCfg.ResName;
        }

        public static void OnSelectTower(this DlgBattleTower self, string towerCfgId, UnitCfg unitCfg)
        {
            int ownTowerCount = self.GetOwnerTowerList()[towerCfgId];
            if (ownTowerCount <= 0)
            {
                return;
            }

            self.selectCfgType = UISelectCfgType.Tower;
            self.selectCfgId = towerCfgId;
            
            string pathName = self.GetUnitPrefabName(unitCfg);
            GameObject go = ResComponent.Instance.LoadAsset<GameObject>(pathName);

            self.currentPlaceObj = GameObject.Instantiate(go);
        }

        public static void AddTowerItemRefreshListener(this DlgBattleTower self, Transform transform, int index)
        {
            Scroll_Item_Tower itemTower = self.ScrollItemTowers[index].BindTrans(transform);

            List<string> list = self.GetOwnerTowerList().Keys.ToList();
            
            TowerCfg towerCfg = TowerCfgCategory.Instance.Get(list[index]);
            UnitCfg unitCfg = UnitCfgCategory.Instance.Get(towerCfg.UnitId);
            
            string icon = self.GetUnitIcon(unitCfg);
            Sprite sprite = ResComponent.Instance.LoadAsset<Sprite>(icon);
            itemTower.ELabel_ContentText.text = $"数量:{self.GetOwnerTowerList()[towerCfg.Id]}";
            itemTower.EButton_SelectImage.sprite = sprite;
            SelectImage selectImage = itemTower.EButton_SelectButton.GetComponent<SelectImage>();
            selectImage.onPointerDown = () => { self.OnSelectTower(towerCfg.Id, unitCfg); };
        }

        public static void AddTowerBuyListener(this DlgBattleTower self, Transform transform, int index)
        {
            Scroll_Item_TowerBuy itemTankBuy = self.ScrollItemTowerBuy[index].BindTrans(transform);

            List<string> list = self.GetTowerBuyList();
            List<bool> listBought = self.GetTowerBoughtsList();
            
            TowerCfg towerCfg = TowerCfgCategory.Instance.Get(list[index]);
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
                itemTankBuy.ELabel_ContentText.text = $"可买";
                itemTankBuy.EButton_BuyButton.gameObject.SetActive(true);
            }
            itemTankBuy.EButton_SelectImage.sprite = sprite;
            itemTankBuy.EButton_BuyButton.AddListener(() =>
            {
                self.BuyTower(index);
            });
        }

        public static async ETTask BuyTower(this DlgBattleTower self, int index)
        {
            ET.Client.GamePlayHelper.SendBuyPlayerTower(self.ClientScene(), index);
        }
        
        public static async ETTask RefreshBuyTower(this DlgBattleTower self)
        {
            ET.Client.GamePlayHelper.SendRefreshBuyPlayerTower(self.ClientScene());
        }

        public static void Update(this DlgBattleTower self)
        {
            self.ShowCurLeftTimeInfo();
            
            if (self.currentPlaceObj == null)
            {
                self.ChkPlayerMove();
                return;
            }

            if (self.CheckUserInput())
            {
                self.isDragging = true;
                self.isClickUGUI = self.IsClickUGUI();
                if (self.isClickUGUI)
                {
                    return;
                }

                self.MoveCurrentPlaceObj();
            }
            else if (self.isDragging)
            {
                if (self.isClickUGUI == false)
                {
                    var position = self.currentPlaceObj.transform.position;
                    if (self.selectCfgType == UISelectCfgType.HeadQuarter)
                    {
                        ET.Client.GamePlayHelper.SendPutHome(self.ClientScene(), self.selectCfgId, position);
                    }
                    else if (self.selectCfgType == UISelectCfgType.MonsterCall)
                    {
                        ET.Client.GamePlayHelper.SendPutMonsterCall(self.ClientScene(), self.selectCfgId, position);
                    }
                    else if (self.selectCfgType == UISelectCfgType.Tower)
                    {
                        ET.Client.GamePlayHelper.SendCallOwnTower(self.ClientScene(), self.selectCfgId, position);
                    }
                }

                self.CheckIfPlaceSuccess();
            }
            else if (self.currentPlaceObj != null)
            {
                if (self.selectCfgType == UISelectCfgType.Tower)
                {
                    self.CheckIfPlaceSuccess();
                }
            }
        }

        public static void ChkPlayerMove(this DlgBattleTower self)
        {
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(1))
#else
            if (Input.GetMouseButtonDown(0))
#endif
            {
                Ray ray = ET.Client.CameraHelper.GetMainCamera(self.DomainScene()).ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000, self._groundLayerMask))
                {
                    C2M_PathfindingResult c2MPathfindingResult = new C2M_PathfindingResult();
                    c2MPathfindingResult.Position = (float3) hit.point - new float3(0, 0f, 0);
                    self.ClientScene().GetComponent<SessionComponent>().Session.Send(c2MPathfindingResult);
                }
            }
        }

        /// <summary>
        ///检测用户当前输入
        /// </summary>
        /// <returns></returns>
        public static bool CheckUserInput(this DlgBattleTower self)
        {
#if true||UNITY_EDITOR
            return Input.GetMouseButton(0);
#elif UNITY_ANDROID || UNITY_IPHONE || UNITY_IOS
            if (Input.touches.Length > 0) {
                if (!self.isTouchInput) {
                    self.isTouchInput = true;
                    self.touchID = Input.touches[0].fingerId;
                    return true;
                } else if (Input.GetTouch (self.touchID).phase == TouchPhase.Ended) {
                    self.isTouchInput = false;
                    return false;
                } else {
                    return true;
                }
            }
            return false;
#else
            return Input.GetMouseButton(0);
#endif
        }

        public static bool IsClickUGUI(this DlgBattleTower self)
        {
            if (UnityEngine.EventSystems.EventSystem.current)
            {
#if true||UNITY_EDITOR
                return UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
#elif UNITY_ANDROID || UNITY_IPHONE || UNITY_IOS
                if (Input.touchCount > 0)
                {
                    return self.IsPointerOverUIObject();
                }

                return false;
#else
                return false;
#endif
            }

            return false;
        }

        public static bool IsPointerOverUIObject(this DlgBattleTower self)
        {
            self.eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            self.results.Clear(); // Just in case
            UnityEngine.EventSystems.EventSystem.current.RaycastAll(self.eventDataCurrentPosition, self.results);
            return self.results.Count > 0;
        }

        /// <summary>
        ///让当前对象跟随鼠标移动
        /// </summary>
        public static void MoveCurrentPlaceObj(this DlgBattleTower self)
        {
            Vector3 point;
            Vector3 screenPosition;
#if true||UNITY_EDITOR
            screenPosition = Input.mousePosition;
#elif UNITY_ANDROID || UNITY_IPHONE || UNITY_IOS
            Touch touch = Input.GetTouch (self.touchID);
            screenPosition = new Vector3 (touch.position.x, touch.position.y, 0);
#else
            screenPosition = Input.mousePosition;
#endif

            Ray ray = ET.Client.CameraHelper.GetMainCamera(self.DomainScene()).ScreenPointToRay(screenPosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, 1000, self._groundLayerMask))
            {
                point = hitInfo.point;
                self.isPlaceSuccess = true;
            }
            else
            {
                point = ray.GetPoint(self._zDistance);
                self.isPlaceSuccess = false;
            }

            self.currentPlaceObj.transform.position = point + new Vector3(0, self._YOffset, 0);
            self.currentPlaceObj.transform.localEulerAngles = new Vector3(0, 60, 0);
        }

        /// <summary>
        ///检测是否放置成功
        /// </summary>
        public static void CheckIfPlaceSuccess(this DlgBattleTower self)
        {
            self.isDragging = false;
            if (self.currentPlaceObj != null)
            {
                GameObject.Destroy(self.currentPlaceObj);
                self.currentPlaceObj = null;
            }
        }
        
        
        public static void OnSelectHeadQuarter(this DlgBattleTower self)
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
        }
        
        public static void OnSelectMonsterCall(this DlgBattleTower self)
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
        }
    }
}