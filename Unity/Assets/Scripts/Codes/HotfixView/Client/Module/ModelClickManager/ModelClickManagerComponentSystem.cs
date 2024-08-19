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
            GameObject go = new GameObject("ModelClickManagerComponent");
            self.root = go;
            go.transform.SetParent(GlobalComponent.Instance.ClientManagerRoot);
            go.transform.localPosition = UnityEngine.Vector3.zero;
            go.transform.localScale = UnityEngine.Vector3.one;

            self.needChkDownAgain = true;
            self.click = false;
        }

        public static void Update(this ModelClickManagerComponent self)
        {
            if (null != self.ModelClick || null != self.ModelPress)
            {
                bool bRet = false;
                Vector3 pos = Vector3.zero;
                (bRet, pos) = ET.UGUIHelper.GetUserInputDown();
                if (bRet)
                {
                    self.PointDown(pos);
                }
                (bRet, pos) = ET.UGUIHelper.GetUserInputUp();
                if (bRet)
                {
                    self.PointUp(pos);
                }

                if (null != self.ModelPress)
                {
                    (bRet, pos) = ET.UGUIHelper.GetUserInputPress();
                    if (bRet)
                    {
                        self.ChkPressTrig(pos);
                    }
                }
            }
        }

        /// <summary>
        /// 按下
        /// </summary>
        public static void PointDown(this ModelClickManagerComponent self, UnityEngine.Vector3 position)
        {
            self.inputPosition = position;
            (self.downObj, _) = self.IsClick(self.inputPosition);
            if (null != self.downObj)
            {
                self.click = true;
                self.startTime = self.GetMilliseconds();
            }
            else if(self.needChkDownAgain)
            {
                self.needChkDownAgain = false;
                self.ChkPointDownNextFrame(position).Coroutine();
            }
        }

        public static async ETTask ChkPointDownNextFrame(this ModelClickManagerComponent self, UnityEngine.Vector3 position)
        {
            await TimerComponent.Instance.WaitFrameAsync();
            self.PointDown(position);
        }

        /// <summary>
        /// 抬起
        /// </summary>
        public static void PointUp(this ModelClickManagerComponent self, UnityEngine.Vector3 position)
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
            (self.upObj, rayHit) = self.IsClick(position);
            if (null != self.upObj && self.upObj.GetHashCode() == self.downObj.GetHashCode() && (self.GetMilliseconds() - self.startTime <= 220) && UnityEngine.Vector3.Distance(position, self.inputPosition) <= 10)
            {
                if (null != self.ModelClick)
                {
                    self.ModelClick.Invoke(rayHit.Value);
                    self.RecordClickInfo(rayHit.Value);
                }
            }
            self.needChkDownAgain = true;
            self.click = false;
        }

        /// <summary>
        /// Press trig
        /// </summary>
        public static void ChkPressTrig(this ModelClickManagerComponent self, UnityEngine.Vector3 position)
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
            (self.upObj, rayHit) = self.IsClick(position);
            if (null != self.upObj && self.upObj.GetHashCode() == self.downObj.GetHashCode() && (self.GetMilliseconds() - self.startTime > 220) && UnityEngine.Vector3.Distance(position, self.inputPosition) <= 10)
            {
                self.needChkDownAgain = true;
                self.click = false;
                if (null != self.ModelPress)
                {
                    self.ModelPress.Invoke(rayHit.Value);
                }
            }
        }

        /// <summary>
        /// 判断点击的对象是否为本体
        /// </summary>
        /// <param name="clickPos"></param>
        /// <returns></returns>
        public static (GameObject, RaycastHit?) IsClick(this ModelClickManagerComponent self, UnityEngine.Vector2 clickPos)
        {
            bool isClickUGUI = ET.UGUIHelper.IsClickUGUI();
            if (isClickUGUI)
            {
                return (null, null);
            }
            Ray ray = ET.Client.CameraHelper.GetMainCamera(self.DomainScene()).ScreenPointToRay(clickPos);
            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit, self.RayMaxDistance))
            {
                return (rayHit.collider.gameObject, rayHit);
            }
            return (null, null);
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

        public static void SetTowerInfoToClickInfo(this ModelClickManagerComponent self, Transform colliderTrans, TowerShowComponent towerShowComponent)
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

        public static void SetPlayerUnitInfoToClickInfo(this ModelClickManagerComponent self, Transform colliderTrans, PlayerUnitShowComponent playerUnitShowComponent)
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

        public static TowerShowComponent GetLastClickTowerInfo(this ModelClickManagerComponent self)
        {
            ReferenceSimpleData recordReferenceSimpleData = self.recordReferenceSimpleData;
            if (recordReferenceSimpleData == null)
            {
                return null;
            }
            TowerShowComponent towerShowComponent = self._GetTowerInfoFromClickInfo(recordReferenceSimpleData);
            return towerShowComponent;
        }

        public static PlayerUnitShowComponent GetLastClickPlayerUnitInfo(this ModelClickManagerComponent self)
        {
            ReferenceSimpleData recordReferenceSimpleData = self.recordReferenceSimpleData;
            if (recordReferenceSimpleData == null)
            {
                return null;
            }
            PlayerUnitShowComponent playerUnitShowComponent = self._GetPlayerUnitInfoFromClickInfo(recordReferenceSimpleData);
            return playerUnitShowComponent;
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
    }
}