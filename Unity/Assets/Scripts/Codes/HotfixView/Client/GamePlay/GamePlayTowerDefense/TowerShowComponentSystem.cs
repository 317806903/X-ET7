using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ET.Client
{
    public static class TowerShowComponentSystem
    {
        [ObjectSystem]
        public class AwakeSystem: AwakeSystem<TowerShowComponent>
        {
            protected override void Awake(TowerShowComponent self)
            {
            }
        }

        [ObjectSystem]
        public class DestroySystem: DestroySystem<TowerShowComponent>
        {
            protected override void Destroy(TowerShowComponent self)
            {
                if (self.transRoot != null)
                {
                    GameObjectPoolHelper.ReturnTransformToPool(self.transRoot);
                    self.transRoot = null;
                }
            }
        }

        [ObjectSystem]
        public class UpdateSystem: UpdateSystem<TowerShowComponent>
        {
            protected override void Update(TowerShowComponent self)
            {
                // Transform transform = self.go.transform;
                // Camera mainCamera = CameraHelper.GetMainCamera(self.DomainScene());
                // if (mainCamera == null)
                // {
                //     return;
                // }
                // Vector3 direction = mainCamera.transform.forward;
                // transform.forward = -direction;
            }
        }

        public static Unit GetUnit(this TowerShowComponent self)
        {
            return self.GetParent<Unit>();
        }

        public static void Init(this TowerShowComponent self, TowerComponent towerComponent)
        {
            self.towerComponent = towerComponent;
            self.CreateShow();
            self.ChkUpgradePlayerTower();
        }

        public static void CreateShow(this TowerShowComponent self)
        {
            GameObjectComponent gameObjectComponent = self.GetUnit().GetComponent<GameObjectComponent>();
            if (gameObjectComponent == null || gameObjectComponent.GetGo() == null)
            {
                return;
            }
            ResEffectCfg resEffectCfg = ResEffectCfgCategory.Instance.Get("ResEffect_TowerShow");
            GameObject TowerShowGo = GameObjectPoolHelper.GetObjectFromPool(resEffectCfg.ResName,true,1);
            self.transRoot = TowerShowGo.transform;
            TowerShowGo.transform.SetParent(gameObjectComponent.GetGo().transform);
            TowerShowGo.transform.localPosition = new float3(0, 0, 0);

            Unit unit = self.GetUnit();
            float radius = Ability.UnitHelper.GetBodyRadius(unit) / Ability.UnitHelper.GetResScale(unit);
            float height = Ability.UnitHelper.GetBodyHeight(unit) / Ability.UnitHelper.GetResScale(unit);
            TowerShowGo.transform.localScale = new Vector3(radius * 2, height, radius * 2);

            self.transCollider = self.transRoot.Find("ColliderRoot/Collider");
            self.transDefaultShow = self.transRoot.Find("DefaultShow");
            self.transSelectShow = self.transRoot.Find("SelectShow");
            self.transCanUpgradeShow = self.transRoot.Find("CanUpgradeShow");
            self.transAttackArea = self.transRoot.Find("AttackArea");

            self.transDefaultShow.gameObject.SetActive(true);
            self.transSelectShow.gameObject.SetActive(false);
            self.transCanUpgradeShow.gameObject.SetActive(false);
            self.transAttackArea.gameObject.SetActive(false);

            self.ChgColor(self.transDefaultShow);
            self.ChgColor(self.transSelectShow);
            self.ChgColor(self.transAttackArea);

        }

        public static void ChgColor(this TowerShowComponent self, Transform trans)
        {
            GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(self.DomainScene());
            float3 colorValue = gamePlayComponent.GetPlayerColor(self.towerComponent.playerId);
            Color color = new Color(colorValue.x, colorValue.y, colorValue.z);
            ParticleSystem[] psList = trans.gameObject.GetComponentsInChildren<ParticleSystem>(true);
            foreach (ParticleSystem particleSystem in psList)
            {
                ParticleSystem.MainModule mainModule = particleSystem.main;
                float alpha = mainModule.startColor.color.a;
                color.a = alpha;
                mainModule.startColor = new ParticleSystem.MinMaxGradient(color);
            }
        }

        public static void ChgAttackArea(this TowerShowComponent self)
        {
            Unit unit = self.GetUnit();
            NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
            float skillDis = numericComponent.GetAsFloat(NumericType.SkillDis);
            if (skillDis <= 0)
            {
                self.transAttackArea.gameObject.SetActive(false);
            }
            else
            {
                self.transAttackArea.gameObject.SetActive(true);
                float resScale = Ability.UnitHelper.GetResScale(unit);
                self.transAttackArea.transform.localScale = Vector3.one * skillDis*2 / resScale;
            }
        }

        public static void DoSelect(this TowerShowComponent self)
        {
            Log.Debug($"ET.Client.TowerShowComponentSystem.DoSelect unitId[{self.GetUnit().Id}] towerCfgId[{self.towerComponent.towerCfgId}]");

            self.transDefaultShow.gameObject.SetActive(false);
            self.transSelectShow.gameObject.SetActive(true);
            self.ChgAttackArea();

            UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgBattleTowerHUD>(new DlgBattleTowerHUD_ShowWindowData()
            {
                playerId = self.towerComponent.playerId,
                towerUnitId = self.GetUnit().Id,
                towerCfgId = self.towerComponent.towerCfgId,
            }).Coroutine();
        }

        public static bool CancelSelect(this TowerShowComponent self)
        {
            bool bRet = false;
            if (self.transSelectShow.gameObject.activeSelf)
            {
                self.transDefaultShow.gameObject.SetActive(true);
                self.transSelectShow.gameObject.SetActive(false);
                self.transAttackArea.gameObject.SetActive(false);
                bRet = true;

                Log.Debug($"ET.Client.TowerShowComponentSystem.CancelSelect unitId[{self.GetUnit().Id}] towerCfgId[{self.towerComponent.towerCfgId}]");

            }
            return bRet;
        }

        public static void ChkUpgradePlayerTower(this TowerShowComponent self)
        {
            if (self.transRoot == null)
            {
                return;
            }
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
            if (gamePlayTowerDefenseComponent == null)
            {
                return;
            }
            (bool bRet, string msg, Dictionary<string, int> costTowers) = gamePlayTowerDefenseComponent.ChkUpgradePlayerTower(self.towerComponent.playerId, self.GetUnit().Id);
            if (bRet)
            {
                self.transCanUpgradeShow.gameObject.SetActive(true);
            }
            else
            {
                self.transCanUpgradeShow.gameObject.SetActive(false);
            }
        }

    }
}