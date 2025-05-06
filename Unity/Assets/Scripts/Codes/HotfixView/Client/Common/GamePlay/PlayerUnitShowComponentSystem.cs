using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ET.Client
{
    public static class PlayerUnitShowComponentSystem
    {
        [ObjectSystem]
        public class AwakeSystem: AwakeSystem<PlayerUnitShowComponent>
        {
            protected override void Awake(PlayerUnitShowComponent self)
            {
            }
        }

        [ObjectSystem]
        public class DestroySystem: DestroySystem<PlayerUnitShowComponent>
        {
            protected override void Destroy(PlayerUnitShowComponent self)
            {
                if (self.transRoot != null)
                {
                    GameObjectPoolHelper.ReturnTransformToPool(self.transRoot);
                    self.transRoot = null;
                }

                self.playerUnitComponent = null;
            }
        }

        [ObjectSystem]
        public class UpdateSystem: UpdateSystem<PlayerUnitShowComponent>
        {
            protected override void Update(PlayerUnitShowComponent self)
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

        public static Unit GetUnit(this PlayerUnitShowComponent self)
        {
            return self.GetParent<Unit>();
        }

        public static void Init(this PlayerUnitShowComponent self, PlayerUnitComponent playerUnitComponent)
        {
            self.playerUnitComponent = playerUnitComponent;
            self.CreateShow().Coroutine();
        }

        public static async ETTask CreateShow(this PlayerUnitShowComponent self)
        {
            GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(self.DomainScene());
            while (gamePlayComponent == null)
            {
                if (self.IsDisposed)
                {
                    return;
                }
                await TimerComponent.Instance.WaitFrameAsync();
                gamePlayComponent = GamePlayHelper.GetGamePlay(self.DomainScene());
            }

            bool bRet = await ET.Client.UnitViewHelper.ChkGameObjectShowReady(self, self.GetUnit());
            if (bRet == false)
            {
                return;
            }
            GameObjectShowComponent gameObjectShowComponent = self.GetUnit().GetComponent<GameObjectShowComponent>();

            ResEffectCfg resEffectCfg = ResEffectCfgCategory.Instance.Get("ResEffect_PlayerUnitShow");
            GameObject PlayerUnitShowGo = GameObjectPoolHelper.GetObjectFromPool(resEffectCfg.ResName,true,1);
            self.transRoot = PlayerUnitShowGo.transform;
            PlayerUnitShowGo.transform.SetParent(gameObjectShowComponent.GetEffectResNoRotateRoot().transform);
            PlayerUnitShowGo.transform.localPosition = Vector3.zero;

            Unit unit = self.GetUnit();
            float parentScale = self.transRoot.parent.localScale.x;
            float radius = Ability.UnitHelper.GetBodyRadius(unit) / parentScale;
            float height = Ability.UnitHelper.GetBodyHeight(unit) / parentScale;
            PlayerUnitShowGo.transform.localScale = new Vector3(radius * 2, height, radius * 2);

            Transform  transCollider = self.transRoot.Find("ColliderRoot");
            transCollider.gameObject.SetActive(true);
            self.transCollider = self.transRoot.Find("ColliderRoot/Collider");
            ET.Client.ModelClickManagerHelper.SetPlayerUnitInfoToClickInfo(self.DomainScene(), self.transCollider, self, null, null);

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

        public static void ChgColor(this PlayerUnitShowComponent self, Transform trans)
        {
            GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(self.DomainScene());
            float3 colorValue = gamePlayComponent.GetPlayerColor(self.playerUnitComponent.playerId);
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

        public static async ETTask ChgAttackArea(this PlayerUnitShowComponent self)
        {
            Unit unit = self.GetUnit();
            float skillDis = ET.Client.UnitHelper.GetMaxSkillDis(unit);
            if (skillDis <= 0)
            {
                self.transAttackArea.gameObject.SetActive(false);
            }
            else
            {
                self.transAttackArea.gameObject.SetActive(true);

                bool bRet = await ET.Client.UnitViewHelper.ChkGameObjectShowReady(self, self.GetUnit());
                if (bRet == false)
                {
                    return;
                }
                float parentScale = self.transRoot.parent.localScale.x * self.transRoot.localScale.x;

                self.transAttackArea.transform.localScale = Vector3.one * skillDis*2 / parentScale;
            }
        }

        public static void DoSelect(this PlayerUnitShowComponent self)
        {
            self.transDefaultShow.gameObject.SetActive(false);
            self.transSelectShow.gameObject.SetActive(true);
            self.ChgAttackArea().Coroutine();
        }

        public static bool CancelSelect(this PlayerUnitShowComponent self)
        {
            bool bRet = false;
            if (self.transSelectShow.gameObject.activeSelf)
            {
                self.transDefaultShow.gameObject.SetActive(true);
                self.transSelectShow.gameObject.SetActive(false);
                self.transAttackArea.gameObject.SetActive(false);
                bRet = true;
            }
            return bRet;
        }
    }
}