using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using ET.Client;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ET.Client
{
    [FriendOf(typeof (ModelClickManagerComponent))]
    public static class ModelClickManagerComponentSystem
    {
        [ObjectSystem]
        public class ModelClickManagerComponentAwakeSystem: AwakeSystem<ModelClickManagerComponent>
        {
            protected override void Awake(ModelClickManagerComponent self)
            {
                self.Init();
            }
        }

        [ObjectSystem]
        public class ModelClickManagerComponentDestroySystem: DestroySystem<ModelClickManagerComponent>
        {
            protected override void Destroy(ModelClickManagerComponent self)
            {
                self.downObj = null;
                self.upObj = null;
                if (self.root != null)
                {
                    GameObject.Destroy(self.root);
                    self.root = null;
                }
            }
        }

        [ObjectSystem]
        public class ModelClickManagerComponentFixedUpdateSystem: UpdateSystem<ModelClickManagerComponent>
        {
            protected override void Update(ModelClickManagerComponent self)
            {
                self.Update();
            }
        }

        public static void Init(this ModelClickManagerComponent self)
        {
            self.ModelClickCallBackDic = new();
            self.ModelPressCallBackDic = new();

            GameObject go = new GameObject("ModelClickManagerComponent");
            self.root = go;
            go.transform.SetParent(GlobalComponent.Instance.ClientManagerRoot);
            go.transform.localPosition = UnityEngine.Vector3.zero;
            go.transform.localScale = UnityEngine.Vector3.one;

            self.needChkDownAgain = true;
            self.click = false;
        }

        public static void ClearClickAndPressInfo(this ModelClickManagerComponent self, Transform colliderTrans)
        {
            if (self.ModelClickCallBackDic.ContainsKey(colliderTrans.gameObject))
            {
                self.ModelClickCallBackDic[colliderTrans.gameObject] = null;
            }
            if (self.ModelPressCallBackDic.ContainsKey(colliderTrans.gameObject))
            {
                self.ModelPressCallBackDic[colliderTrans.gameObject] = null;
            }
        }

        public static void Update(this ModelClickManagerComponent self)
        {
            if (null != self.ModelClick || null != self.ModelPress)
            {
                bool bRet = false;
                Vector3 touchPosition = Vector3.zero;
                Vector3 touchForward = Vector3.zero;
                (bRet, touchPosition, touchForward) = ET.UGUIHelper.GetUserInputDown();
                if (bRet)
                {
                    self.PointDown(touchPosition, touchForward);
                }
                (bRet, touchPosition, touchForward) = ET.UGUIHelper.GetUserInputUp();
                if (bRet)
                {
                    self.PointUp(touchPosition, touchForward);
                }

                if (null != self.ModelPress)
                {
                    (bRet, touchPosition, touchForward) = ET.UGUIHelper.GetUserInputPress();
                    if (bRet)
                    {
                        self.ChkPressTrig(touchPosition, touchForward);
                    }
                }
            }

            if (self.chkClearTime == 0)
            {
                self.chkClearTime = Time.time + 20f;
            }

            if (self.chkClearTime <= Time.time)
            {
                self.chkClearTime = Time.time + 20f;
                self.ClearNull();
            }
        }

        public static void ClearNull(this ModelClickManagerComponent self)
        {
            self.keysToRemove.Clear();
            foreach (var kvp in self.ModelClickCallBackDic)
            {
                if (kvp.Key == null)
                {
                    self.keysToRemove.Add(kvp.Key);
                }
            }
            foreach (var key in self.keysToRemove)
            {
                self.ModelClickCallBackDic.Remove(key);
            }

            self.keysToRemove.Clear();
            foreach (var kvp in self.ModelPressCallBackDic)
            {
                if (kvp.Key == null)
                {
                    self.keysToRemove.Add(kvp.Key);
                }
            }
            foreach (var key in self.keysToRemove)
            {
                self.ModelPressCallBackDic.Remove(key);
            }
            self.keysToRemove.Clear();

        }

        /// <summary>
        /// 按下
        /// </summary>
        public static void PointDown(this ModelClickManagerComponent self, Vector3 touchPosition, Vector3 touchForward)
        {
            self.inputPosition = touchPosition;
            (self.downObj, _) = self.IsClick();
            if (null != self.downObj)
            {
                self.click = true;
                self.startTime = self.GetMilliseconds();
            }
            else if(self.needChkDownAgain)
            {
                self.needChkDownAgain = false;
                self.ChkPointDownNextFrame(touchPosition, touchForward).Coroutine();
            }
        }

        public static async ETTask ChkPointDownNextFrame(this ModelClickManagerComponent self, Vector3 touchPosition, Vector3 touchForward)
        {
            await TimerComponent.Instance.WaitFrameAsync();
            if (self.IsDisposed)
            {
                return;
            }
            self.PointDown(touchPosition, touchForward);
        }

        /// <summary>
        /// 抬起
        /// </summary>
        public static void PointUp(this ModelClickManagerComponent self, Vector3 touchPosition, Vector3 touchForward)
        {
            if (self.downObj == null)
            {
                return;
            }
            if (self.click == false)
            {
                return;
            }

            RaycastHit? rayHit = null;
            (self.upObj, rayHit) = self.IsClick();
            if (null != self.upObj && self.upObj.GetHashCode() == self.downObj.GetHashCode() && (self.GetMilliseconds() - self.startTime <= 220) && UnityEngine.Vector3.Distance(touchPosition, self.inputPosition) <= 10)
            {
                if (null != self.ModelClick && ET.UGUIHelper.IsClickUGUI() == false)
                {
                    self.ModelClick.Invoke(rayHit.Value);
                    self.RecordClickInfo(rayHit.Value);

                    if (self.ModelClickCallBackDic.ContainsKey(rayHit.Value.transform.gameObject))
                    {
                        self.ModelClickCallBackDic[rayHit.Value.transform.gameObject].Invoke();
                    }
                }
            }
            self.needChkDownAgain = true;
            self.click = false;
        }

        /// <summary>
        /// Press trig
        /// </summary>
        public static void ChkPressTrig(this ModelClickManagerComponent self, Vector3 touchPosition, Vector3 touchForward)
        {
            if (self.downObj == null)
            {
                return;
            }
            if (self.click == false)
            {
                return;
            }
            RaycastHit? rayHit = null;
            (self.upObj, rayHit) = self.IsClick();
            if (null != self.upObj && self.upObj.GetHashCode() == self.downObj.GetHashCode() && (self.GetMilliseconds() - self.startTime > 220) && UnityEngine.Vector3.Distance(touchPosition, self.inputPosition) <= 10)
            {
                self.needChkDownAgain = true;
                self.click = false;
                if (null != self.ModelPress && ET.UGUIHelper.IsClickUGUI() == false)
                {
                    self.ModelPress.Invoke(rayHit.Value);

                    if (self.ModelPressCallBackDic.ContainsKey(rayHit.Value.transform.gameObject))
                    {
                        self.ModelPressCallBackDic[rayHit.Value.transform.gameObject].Invoke();
                    }
                }
            }
        }

        /// <summary>
        /// 判断点击的对象是否为本体
        /// </summary>
        /// <param name="clickPos"></param>
        /// <returns></returns>
        public static (GameObject, RaycastHit?) IsClick(this ModelClickManagerComponent self)
        {
            var result = ET.UGUIHelper.ChkClickGameObject(self.DomainScene(), Vector3.zero, self.RayMaxDistance, -1);
            if (result.ret == false)
            {
                return (null, null);
            }

            return (result.go, result.raycastHit);
        }

        public static long GetMilliseconds(this ModelClickManagerComponent self)
        {
            DateTime now = DateTime.Now;
            if (self.lastCacheDateTime != now)
            {
                self.lastCacheDateTime = now;
                self.lastCacheMillis = (now.ToFileTimeUtc() / 10000) - self.unixBaseMillis;
            }
            return self.lastCacheMillis;
        }

        public static void RecordClickInfo(this ModelClickManagerComponent self, RaycastHit rayHit)
        {
            ReferenceSimpleData referenceSimpleData = rayHit.collider.gameObject.GetComponent<ReferenceSimpleData>();
            self.recordReferenceSimpleData = referenceSimpleData;
        }

        public static void SetTowerInfoToClickInfo(this ModelClickManagerComponent self, Transform colliderTrans, TowerShowComponent towerShowComponent, Action modelClickCallBack, Action modelPressCallBack)
        {
            Collider collider = colliderTrans.gameObject.GetComponent<Collider>();
            if (collider == null)
            {
                Log.Error($"SetTowerInfoToClickInfo collider == null");
                return;
            }
            ReferenceSimpleData referenceSimpleData = colliderTrans.gameObject.GetComponent<ReferenceSimpleData>();
            if (referenceSimpleData == null)
            {
                referenceSimpleData = colliderTrans.gameObject.AddComponent<ReferenceSimpleData>();
            }
            var dict = referenceSimpleData.dict;
            dict.Clear();
            dict.Add(ClickDataField.DataType.ToString(), ClickDataType.Tower.ToString());
            dict.Add(ClickDataField.PlayerId.ToString(), towerShowComponent.towerComponent.playerId.ToString());
            dict.Add(ClickDataField.UnitId.ToString(), towerShowComponent.GetUnit().Id.ToString());
            dict.Add(ClickDataField.UnitCfgId.ToString(), towerShowComponent.GetUnit().CfgId.ToString());
            dict.Add(ClickDataField.TowerCfgId.ToString(), towerShowComponent.towerComponent.towerCfgId.ToString());

            if (modelClickCallBack != null)
            {
                self.ModelClickCallBackDic[colliderTrans.gameObject] = modelClickCallBack;
            }
            if (modelPressCallBack != null)
            {
                self.ModelPressCallBackDic[colliderTrans.gameObject] = modelPressCallBack;
            }
        }

        public static bool ChkIsHitTowerClickInfo(this ModelClickManagerComponent self, RaycastHit raycastHit)
        {
            ReferenceSimpleData referenceSimpleData = raycastHit.collider.gameObject.GetComponent<ReferenceSimpleData>();
            if (referenceSimpleData == null)
            {
                return false;
            }
            var dict = referenceSimpleData.dict;
            if (dict.TryGetValue(ClickDataField.DataType.ToString(), out string value) == false)
            {
                return false;
            }

            if (value != ClickDataType.Tower.ToString())
            {
                return false;
            }

            return true;
        }

        public static TowerShowComponent GetTowerInfoFromClickInfo(this ModelClickManagerComponent self, RaycastHit raycastHit)
        {
            ReferenceSimpleData referenceSimpleData = raycastHit.collider.gameObject.GetComponent<ReferenceSimpleData>();
            if (referenceSimpleData == null)
            {
                return null;
            }

            return self._GetTowerInfoFromClickInfo(referenceSimpleData);
        }

        public static void SetHomeInfoToClickInfo(this ModelClickManagerComponent self, Transform colliderTrans, HomeShowComponent homeShowComponent, Action modelClickCallBack, Action modelPressCallBack)
        {
            Collider collider = colliderTrans.gameObject.GetComponent<Collider>();
            if (collider == null)
            {
                Log.Error($"SetHomeInfoToClickInfo collider == null");
                return;
            }
            ReferenceSimpleData referenceSimpleData = colliderTrans.gameObject.GetComponent<ReferenceSimpleData>();
            if (referenceSimpleData == null)
            {
                referenceSimpleData = colliderTrans.gameObject.AddComponent<ReferenceSimpleData>();
            }
            var dict = referenceSimpleData.dict;
            dict.Clear();
            dict.Add(ClickDataField.DataType.ToString(), ClickDataType.Home.ToString());
            dict.Add(ClickDataField.UnitId.ToString(), homeShowComponent.GetUnit().Id.ToString());
            dict.Add(ClickDataField.UnitCfgId.ToString(), homeShowComponent.GetUnit().CfgId.ToString());

            if (modelClickCallBack != null)
            {
                self.ModelClickCallBackDic[colliderTrans.gameObject] = modelClickCallBack;
            }
            if (modelPressCallBack != null)
            {
                self.ModelPressCallBackDic[colliderTrans.gameObject] = modelPressCallBack;
            }
        }

        public static bool ChkIsHitHomeClickInfo(this ModelClickManagerComponent self, RaycastHit raycastHit)
        {
            ReferenceSimpleData referenceSimpleData = raycastHit.collider.gameObject.GetComponent<ReferenceSimpleData>();
            if (referenceSimpleData == null)
            {
                return false;
            }
            var dict = referenceSimpleData.dict;
            if (dict.TryGetValue(ClickDataField.DataType.ToString(), out string value) == false)
            {
                return false;
            }

            if (value != ClickDataType.Home.ToString())
            {
                return false;
            }

            return true;
        }

        public static HomeShowComponent GetHomeInfoFromClickInfo(this ModelClickManagerComponent self, RaycastHit raycastHit)
        {
            ReferenceSimpleData referenceSimpleData = raycastHit.collider.gameObject.GetComponent<ReferenceSimpleData>();
            if (referenceSimpleData == null)
            {
                return null;
            }

            return self._GetHomeInfoFromClickInfo(referenceSimpleData);
        }

        public static void SetMonsterCallInfoToClickInfo(this ModelClickManagerComponent self, Transform colliderTrans, MonsterCallShowComponent monsterCallShowComponent, Action modelClickCallBack, Action modelPressCallBack)
        {
            Collider collider = colliderTrans.gameObject.GetComponent<Collider>();
            if (collider == null)
            {
                Log.Error($"SetMonsterCallInfoToClickInfo collider == null");
                return;
            }
            ReferenceSimpleData referenceSimpleData = colliderTrans.gameObject.GetComponent<ReferenceSimpleData>();
            if (referenceSimpleData == null)
            {
                referenceSimpleData = colliderTrans.gameObject.AddComponent<ReferenceSimpleData>();
            }
            var dict = referenceSimpleData.dict;
            dict.Clear();
            dict.Add(ClickDataField.DataType.ToString(), ClickDataType.MonsterCall.ToString());
            dict.Add(ClickDataField.UnitId.ToString(), monsterCallShowComponent.GetUnit().Id.ToString());
            dict.Add(ClickDataField.UnitCfgId.ToString(), monsterCallShowComponent.GetUnit().CfgId.ToString());

            if (modelClickCallBack != null)
            {
                self.ModelClickCallBackDic[colliderTrans.gameObject] = modelClickCallBack;
            }
            if (modelPressCallBack != null)
            {
                self.ModelPressCallBackDic[colliderTrans.gameObject] = modelPressCallBack;
            }
        }

        public static bool ChkIsHitMonsterCallClickInfo(this ModelClickManagerComponent self, RaycastHit raycastHit)
        {
            ReferenceSimpleData referenceSimpleData = raycastHit.collider.gameObject.GetComponent<ReferenceSimpleData>();
            if (referenceSimpleData == null)
            {
                return false;
            }
            var dict = referenceSimpleData.dict;
            if (dict.TryGetValue(ClickDataField.DataType.ToString(), out string value) == false)
            {
                return false;
            }

            if (value != ClickDataType.MonsterCall.ToString())
            {
                return false;
            }

            return true;
        }

        public static MonsterCallShowComponent GetMonsterCallInfoFromClickInfo(this ModelClickManagerComponent self, RaycastHit raycastHit)
        {
            ReferenceSimpleData referenceSimpleData = raycastHit.collider.gameObject.GetComponent<ReferenceSimpleData>();
            if (referenceSimpleData == null)
            {
                return null;
            }

            return self._GetMonsterCallInfoFromClickInfo(referenceSimpleData);
        }

        public static void SetPlayerUnitInfoToClickInfo(this ModelClickManagerComponent self, Transform colliderTrans, PlayerUnitShowComponent playerUnitShowComponent, Action modelClickCallBack, Action modelPressCallBack)
        {
            Collider collider = colliderTrans.gameObject.GetComponent<Collider>();
            if (collider == null)
            {
                Log.Error($"SetPlayerUnitInfoToClickInfo collider == null");
                return;
            }
            ReferenceSimpleData referenceSimpleData = colliderTrans.gameObject.GetComponent<ReferenceSimpleData>();
            if (referenceSimpleData == null)
            {
                referenceSimpleData = colliderTrans.gameObject.AddComponent<ReferenceSimpleData>();
            }
            var dict = referenceSimpleData.dict;
            dict.Clear();
            dict.Add(ClickDataField.DataType.ToString(), ClickDataType.PlayerUnit.ToString());
            dict.Add(ClickDataField.PlayerId.ToString(), playerUnitShowComponent.playerUnitComponent.playerId.ToString());
            dict.Add(ClickDataField.UnitId.ToString(), playerUnitShowComponent.GetUnit().Id.ToString());
            dict.Add(ClickDataField.UnitCfgId.ToString(), playerUnitShowComponent.GetUnit().CfgId.ToString());

            if (modelClickCallBack != null)
            {
                self.ModelClickCallBackDic[colliderTrans.gameObject] = modelClickCallBack;
            }
            if (modelPressCallBack != null)
            {
                self.ModelPressCallBackDic[colliderTrans.gameObject] = modelPressCallBack;
            }
        }

        public static bool ChkIsHitPlayerUnitClickInfo(this ModelClickManagerComponent self, RaycastHit raycastHit)
        {
            ReferenceSimpleData referenceSimpleData = raycastHit.collider.gameObject.GetComponent<ReferenceSimpleData>();
            if (referenceSimpleData == null)
            {
                return false;
            }
            var dict = referenceSimpleData.dict;
            if (dict.TryGetValue(ClickDataField.DataType.ToString(), out string value) == false)
            {
                return false;
            }

            if (value != ClickDataType.PlayerUnit.ToString())
            {
                return false;
            }

            return true;
        }

        public static PlayerUnitShowComponent GetPlayerUnitInfoFromClickInfo(this ModelClickManagerComponent self, RaycastHit raycastHit)
        {
            ReferenceSimpleData referenceSimpleData = raycastHit.collider.gameObject.GetComponent<ReferenceSimpleData>();
            if (referenceSimpleData == null)
            {
                return null;
            }

            return self._GetPlayerUnitInfoFromClickInfo(referenceSimpleData);
        }

        public static void DoCancelHitLast(this ModelClickManagerComponent self, RaycastHit hit)
        {
            ReferenceSimpleData recordReferenceSimpleData = self.recordReferenceSimpleData;
            if (recordReferenceSimpleData == null)
            {
                return;
            }

            ReferenceSimpleData referenceSimpleData = hit.collider.gameObject.GetComponent<ReferenceSimpleData>();
            if (recordReferenceSimpleData == referenceSimpleData)
            {
                return;
            }

            TowerShowComponent towerShowComponent = self._GetTowerInfoFromClickInfo(recordReferenceSimpleData);
            if (towerShowComponent != null)
            {
                towerShowComponent.CancelSelect();
            }

            PlayerUnitShowComponent playerUnitShowComponent = self._GetPlayerUnitInfoFromClickInfo(recordReferenceSimpleData);
            if (playerUnitShowComponent != null)
            {
                playerUnitShowComponent.CancelSelect();
            }

            HomeShowComponent homeShowComponent = self._GetHomeInfoFromClickInfo(recordReferenceSimpleData);
            if (homeShowComponent != null)
            {
                homeShowComponent.CancelSelect();
            }

            MonsterCallShowComponent monsterCallShowComponent = self._GetMonsterCallInfoFromClickInfo(recordReferenceSimpleData);
            if (monsterCallShowComponent != null)
            {
                monsterCallShowComponent.CancelSelect();
            }

        }

        public static TowerShowComponent _GetTowerInfoFromClickInfo(this ModelClickManagerComponent self, ReferenceSimpleData referenceSimpleData)
        {
            if (referenceSimpleData == null)
            {
                return null;
            }
            var dict = referenceSimpleData.dict;
            if (dict.TryGetValue(ClickDataField.DataType.ToString(), out string value) == false)
            {
                return null;
            }

            if (value != ClickDataType.Tower.ToString())
            {
                return null;
            }
            if (dict.TryGetValue(ClickDataField.UnitId.ToString(), out string unitIdValue) == false)
            {
                return null;
            }

            long unitId = long.Parse(unitIdValue);
            Unit unit = Ability.UnitHelper.GetUnit(self.DomainScene(), unitId);
            if (unit == null)
            {
                return null;
            }
            TowerShowComponent towerShowComponent = unit.GetComponent<TowerShowComponent>();
            return towerShowComponent;
        }

        public static HomeShowComponent _GetHomeInfoFromClickInfo(this ModelClickManagerComponent self, ReferenceSimpleData referenceSimpleData)
        {
            if (referenceSimpleData == null)
            {
                return null;
            }
            var dict = referenceSimpleData.dict;
            if (dict.TryGetValue(ClickDataField.DataType.ToString(), out string value) == false)
            {
                return null;
            }

            if (value != ClickDataType.Home.ToString())
            {
                return null;
            }
            if (dict.TryGetValue(ClickDataField.UnitId.ToString(), out string unitIdValue) == false)
            {
                return null;
            }

            long unitId = long.Parse(unitIdValue);
            Unit unit = Ability.UnitHelper.GetUnit(self.DomainScene(), unitId);
            if (unit == null)
            {
                return null;
            }
            HomeShowComponent homeShowComponent = unit.GetComponent<HomeShowComponent>();
            return homeShowComponent;
        }

        public static MonsterCallShowComponent _GetMonsterCallInfoFromClickInfo(this ModelClickManagerComponent self, ReferenceSimpleData referenceSimpleData)
        {
            if (referenceSimpleData == null)
            {
                return null;
            }
            var dict = referenceSimpleData.dict;
            if (dict.TryGetValue(ClickDataField.DataType.ToString(), out string value) == false)
            {
                return null;
            }

            if (value != ClickDataType.MonsterCall.ToString())
            {
                return null;
            }
            if (dict.TryGetValue(ClickDataField.UnitId.ToString(), out string unitIdValue) == false)
            {
                return null;
            }

            long unitId = long.Parse(unitIdValue);
            Unit unit = Ability.UnitHelper.GetUnit(self.DomainScene(), unitId);
            if (unit == null)
            {
                return null;
            }
            MonsterCallShowComponent MonsterCallShowComponent = unit.GetComponent<MonsterCallShowComponent>();
            return MonsterCallShowComponent;
        }

        public static PlayerUnitShowComponent _GetPlayerUnitInfoFromClickInfo(this ModelClickManagerComponent self, ReferenceSimpleData referenceSimpleData)
        {
            if (referenceSimpleData == null)
            {
                return null;
            }
            var dict = referenceSimpleData.dict;
            if (dict.TryGetValue(ClickDataField.DataType.ToString(), out string value) == false)
            {
                return null;
            }

            if (value != ClickDataType.PlayerUnit.ToString())
            {
                return null;
            }
            if (dict.TryGetValue(ClickDataField.UnitId.ToString(), out string unitIdValue) == false)
            {
                return null;
            }

            long unitId = long.Parse(unitIdValue);
            Unit unit = Ability.UnitHelper.GetUnit(self.DomainScene(), unitId);
            if (unit == null)
            {
                return null;
            }
            PlayerUnitShowComponent playerUnitShowComponent = unit.GetComponent<PlayerUnitShowComponent>();
            return playerUnitShowComponent;
        }

        public static void SetPutHomeInfoToClickInfo(this ModelClickManagerComponent self, Transform colliderTrans, Action modelClickCallBack, Action modelPressCallBack)
        {
            Collider collider = colliderTrans.gameObject.GetComponent<Collider>();
            if (collider == null)
            {
                Log.Error($"SetHomeInfoToClickInfo collider == null");
                return;
            }
            ReferenceSimpleData referenceSimpleData = colliderTrans.gameObject.GetComponent<ReferenceSimpleData>();
            if (referenceSimpleData == null)
            {
                referenceSimpleData = colliderTrans.gameObject.AddComponent<ReferenceSimpleData>();
            }
            var dict = referenceSimpleData.dict;
            dict.Clear();
            dict.Add(ClickDataField.DataType.ToString(), ClickDataType.HomeWhenPutting.ToString());

            if (modelClickCallBack != null)
            {
                self.ModelClickCallBackDic[colliderTrans.gameObject] = modelClickCallBack;
            }
            if (modelPressCallBack != null)
            {
                self.ModelPressCallBackDic[colliderTrans.gameObject] = modelPressCallBack;
            }
        }

        public static bool ChkIsHitPutHomeClickInfo(this ModelClickManagerComponent self, RaycastHit raycastHit)
        {
            ReferenceSimpleData referenceSimpleData = raycastHit.collider.gameObject.GetComponent<ReferenceSimpleData>();
            if (referenceSimpleData == null)
            {
                return false;
            }
            var dict = referenceSimpleData.dict;
            if (dict.TryGetValue(ClickDataField.DataType.ToString(), out string value) == false)
            {
                return false;
            }
            if (value != ClickDataType.HomeWhenPutting.ToString())
            {
                return false;
            }

            return true;
        }

        public static void SetPutMonsterCallInfoToClickInfo(this ModelClickManagerComponent self, Transform colliderTrans, Action modelClickCallBack, Action modelPressCallBack)
        {
            Collider collider = colliderTrans.gameObject.GetComponent<Collider>();
            if (collider == null)
            {
                Log.Error($"SetPutMonsterCallnfoToClickInfo collider == null");
                return;
            }
            ReferenceSimpleData referenceSimpleData = colliderTrans.gameObject.GetComponent<ReferenceSimpleData>();
            if (referenceSimpleData == null)
            {
                referenceSimpleData = colliderTrans.gameObject.AddComponent<ReferenceSimpleData>();
            }
            var dict = referenceSimpleData.dict;
            dict.Clear();
            dict.Add(ClickDataField.DataType.ToString(), ClickDataType.MonsterCallWhenPutting.ToString());

            if (modelClickCallBack != null)
            {
                self.ModelClickCallBackDic[colliderTrans.gameObject] = modelClickCallBack;
            }
            if (modelPressCallBack != null)
            {
                self.ModelPressCallBackDic[colliderTrans.gameObject] = modelPressCallBack;
            }
        }

        public static bool ChkIsHitPutMonsterCallClickInfo(this ModelClickManagerComponent self, RaycastHit raycastHit)
        {
            ReferenceSimpleData referenceSimpleData = raycastHit.collider.gameObject.GetComponent<ReferenceSimpleData>();
            if (referenceSimpleData == null)
            {
                return false;
            }
            var dict = referenceSimpleData.dict;
            if (dict.TryGetValue(ClickDataField.DataType.ToString(), out string value) == false)
            {
                return false;
            }
            if (value != ClickDataType.MonsterCallWhenPutting.ToString())
            {
                return false;
            }

            return true;
        }
    }
}