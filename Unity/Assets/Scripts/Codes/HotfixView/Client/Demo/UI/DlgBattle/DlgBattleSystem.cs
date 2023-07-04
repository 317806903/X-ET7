using System.Collections;
using System.Collections.Generic;
using System;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Invoke(TimerInvokeType.BattleFrameTimer)]
    public class DlgBattleTimer: ATimer<DlgBattle>
    {
        protected override void Run(DlgBattle self)
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

	[FriendOf(typeof(DlgBattle))]
	public static  class DlgBattleSystem
	{

		public static void RegisterUIEvent(this DlgBattle self)
		{
            self.View.ELoopScrollList_TowerLoopHorizontalScrollRect.prefabSource.prefabName = "Item_Tower";
            self.View.ELoopScrollList_TowerLoopHorizontalScrollRect.prefabSource.poolSize = 5;
			self.View.ELoopScrollList_TowerLoopHorizontalScrollRect.AddItemRefreshListener((transform, i) => self.AddTowerItemRefreshListener(transform, i));
			self.View.ELoopScrollList_TankLoopHorizontalScrollRect.AddItemRefreshListener((transform, i) => self.AddTankItemRefreshListener
			(transform, i));
            self.View.E_QuitBattleButton.AddListenerAsync(self.QuitBattle);
		}

		public static void ShowWindow(this DlgBattle self, Entity contextData = null)
		{
			int countTower = self.towerList.Count;
			self.AddUIScrollItems(ref self.ScrollItemTowers, countTower);
			self.View.ELoopScrollList_TowerLoopHorizontalScrollRect.SetVisible(true, countTower);
            
            int countTank = self.tankList.Count;
            self.AddUIScrollItems(ref self.ScrollItemTanks, countTank);
            self.View.ELoopScrollList_TankLoopHorizontalScrollRect.SetVisible(true, countTank);
            
            self._groundLayerMask = LayerMask.GetMask("Map");
            
            
            self.Timer = TimerComponent.Instance.NewFrameTimer(TimerInvokeType.BattleFrameTimer, self);
		}

        public static void HideWindow(this DlgBattle self)
        {
            TimerComponent.Instance?.Remove(ref self.Timer);
        }

        public static async ETTask QuitBattle(this DlgBattle self)
        {
            await RoomHelper.MemberQuitBattleAsync(self.ClientScene());
            await SceneHelper.EnterHall(self.ClientScene());
        }

        public static (string cfgId, UnitCfg unitCfg) GetCfgId(this DlgBattle self, bool isTower, int index)
        {
            string cfgId;
            string unitCfgId;
            if (isTower)
            {
                cfgId = self.towerList[index];
                TowerCfg towerCfg = TowerCfgCategory.Instance.Get(cfgId);
                unitCfgId = towerCfg.UnitId;
            }
            else
            {
                cfgId = self.tankList[index];
                MonsterCfg monsterCfg = MonsterCfgCategory.Instance.Get(cfgId);
                unitCfgId = monsterCfg.UnitId;
            }
            UnitCfg unitCfg = UnitCfgCategory.Instance.Get(unitCfgId);

            return (cfgId, unitCfg);
        }

        public static string GetUnitPrefabName(this DlgBattle self, UnitCfg unitCfg)
        {
            ResUnitCfg resUnitCfg = ResUnitCfgCategory.Instance.Get(unitCfg.ResId);
            return resUnitCfg.ResName;
        }
        
        public static string GetUnitIcon(this DlgBattle self, UnitCfg unitCfg)
        {
            ResIconCfg resIconCfg = ResIconCfgCategory.Instance.Get(unitCfg.Icon);
            return resIconCfg.ResName;
        }
        
		public static void OnSelectItem(this DlgBattle self, bool isTower, int index)
        {
            if (isTower)
            {
                self.selectCfgType = UISelectCfgType.Tower;
            }
            else
            {
                self.selectCfgType = UISelectCfgType.Tanker;
            }
            UnitCfg unitCfg;
            (self.selectCfgId, unitCfg) = self.GetCfgId(isTower, index);
            
            string pathName = self.GetUnitPrefabName(unitCfg);
            GameObject go = ResComponent.Instance.LoadAsset<GameObject>(pathName);
            
            self.currentPlaceObj = GameObject.Instantiate(go);
		}

		public static void AddTowerItemRefreshListener(this DlgBattle self, Transform transform, int index)
		{
			Scroll_Item_Tower itemTower = self.ScrollItemTowers[index].BindTrans(transform);
            UnitCfg unitCfg;
            (self.selectCfgId, unitCfg) = self.GetCfgId(true, index);
            string icon = self.GetUnitIcon(unitCfg);
            Sprite sprite = ResComponent.Instance.LoadAsset<Sprite>(icon);
			itemTower.ELabel_ContentText.text = $"Tower:{index}";
            itemTower.EButton_SelectImage.sprite = sprite;
			SelectImage selectImage = itemTower.EButton_SelectButton.GetComponent<SelectImage>();
			selectImage.onPointerDown = () => { self.OnSelectItem(true, index); };
		}

		public static void AddTankItemRefreshListener(this DlgBattle self, Transform transform, int index)
		{
			Scroll_Item_Tower itemTank = self.ScrollItemTanks[index].BindTrans(transform);
            UnitCfg unitCfg;
            (self.selectCfgId, unitCfg) = self.GetCfgId(false, index);
            string icon = self.GetUnitIcon(unitCfg);
            Sprite sprite = ResComponent.Instance.LoadAsset<Sprite>(icon);
            itemTank.ELabel_ContentText.text = $"Tank:{index}";
            itemTank.EButton_SelectImage.sprite = sprite;
			SelectImage selectImage = itemTank.EButton_SelectButton.GetComponent<SelectImage>();
			selectImage.onPointerDown = () => { self.OnSelectItem(false, index); };
		}

        public static void Update(this DlgBattle self)
        {
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
                    if (self.selectCfgType == UISelectCfgType.Tower)
                    {
                        ET.Client.GamePlayHelper.SendCallTower(self.ClientScene(), self.selectCfgId, position);
                    }
                    else if (self.selectCfgType == UISelectCfgType.Tanker)
                    {
                        ET.Client.GamePlayHelper.SendCallTank(self.ClientScene(), self.selectCfgId, position);
                    }
                }
                GameObject.Destroy(self.currentPlaceObj);
                self.CheckIfPlaceSuccess();
            }
            else if(self.currentPlaceObj != null)
            {
                GameObject.Destroy(self.currentPlaceObj);
                self.CheckIfPlaceSuccess();
            }
        }

        public static void ChkPlayerMove(this DlgBattle self)
        {
#if true||UNITY_EDITOR
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
                    c2MPathfindingResult.Position = (float3)hit.point - new float3(0, 0f, 0);
                    self.ClientScene().GetComponent<SessionComponent>().Session.Send(c2MPathfindingResult);
                }
            }
        }
        
        /// <summary>
        ///检测用户当前输入
        /// </summary>
        /// <returns></returns>
        public static bool CheckUserInput(this DlgBattle self)
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
        
        public static bool IsClickUGUI(this DlgBattle self)
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

        public static bool IsPointerOverUIObject(this DlgBattle self)
        {
            self.eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            self.results.Clear(); // Just in case
            UnityEngine.EventSystems.EventSystem.current.RaycastAll(self.eventDataCurrentPosition, self.results);
            return self.results.Count > 0;
        }
        
        /// <summary>
        ///让当前对象跟随鼠标移动
        /// </summary>
        public static void MoveCurrentPlaceObj(this DlgBattle self)
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
        public static void CheckIfPlaceSuccess(this DlgBattle self)
        {
            self.isDragging = false;
            self.currentPlaceObj = null;
        }
    }
}
