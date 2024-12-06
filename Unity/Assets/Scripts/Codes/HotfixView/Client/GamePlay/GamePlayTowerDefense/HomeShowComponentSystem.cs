using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ET.Client
{
    public static class HomeShowComponentSystem
    {
        [ObjectSystem]
        public class AwakeSystem: AwakeSystem<HomeShowComponent>
        {
            protected override void Awake(HomeShowComponent self)
            {
            }
        }

        [ObjectSystem]
        public class DestroySystem: DestroySystem<HomeShowComponent>
        {
            protected override void Destroy(HomeShowComponent self)
            {
                if (self.transRoot != null)
                {
                    GameObjectPoolHelper.ReturnTransformToPool(self.transRoot);
                    self.transRoot = null;
                }

                self.homeComponent = null;
            }
        }

        [ObjectSystem]
        public class UpdateSystem: UpdateSystem<HomeShowComponent>
        {
            protected override void Update(HomeShowComponent self)
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

        public static Unit GetUnit(this HomeShowComponent self)
        {
            return self.GetParent<Unit>();
        }

        public static void Init(this HomeShowComponent self, HomeComponent homeComponent)
        {
            self.homeComponent = homeComponent;
            self.CreateShow().Coroutine();
        }

        public static async ETTask CreateShow(this HomeShowComponent self)
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

            ResEffectCfg resEffectCfg = ResEffectCfgCategory.Instance.Get("ResEffect_HomeShow");
            GameObject HomeShowGo = GameObjectPoolHelper.GetObjectFromPool(resEffectCfg.ResName,true,1);
            self.transRoot = HomeShowGo.transform;
            HomeShowGo.transform.SetParent(gameObjectShowComponent.GetGo().transform);
            HomeShowGo.transform.localPosition = new float3(0, 0, 0);

            Unit unit = self.GetUnit();
            float radius = Ability.UnitHelper.GetBodyRadius(unit) / Ability.UnitHelper.GetResScale(unit);
            float height = Ability.UnitHelper.GetBodyHeight(unit) / Ability.UnitHelper.GetResScale(unit);
            HomeShowGo.transform.localScale = new Vector3(radius * 2, height, radius * 2);

            Transform  transCollider = self.transRoot.Find("ColliderRoot");
            transCollider.gameObject.SetActive(true);
            self.transCollider = self.transRoot.Find("ColliderRoot/Collider");
            ET.Client.ModelClickManagerHelper.SetHomeInfoToClickInfo(self.DomainScene(), self.transCollider, self);

            float chgY = self.transRoot.localScale.x / self.transRoot.localScale.y;
            self.transDefaultShow = self.transRoot.Find("DefaultShow");
            self.transDefaultShow.localScale = new Vector3(1f, chgY, 1f);
            self.transSelectShow = self.transRoot.Find("SelectShow");
            self.transSelectShow.localScale = new Vector3(1f, chgY, 1f);

            // self.transDefaultShow.gameObject.SetActive(true);
            // self.transSelectShow.gameObject.SetActive(false);
            self.transDefaultShow.gameObject.SetActive(false);
            self.transSelectShow.gameObject.SetActive(false);

            self.ChgColor(self.transDefaultShow);
            self.ChgColor(self.transSelectShow);
        }

        public static void ChgColor(this HomeShowComponent self, Transform trans)
        {
        }

        public static async ETTask DoSelect(this HomeShowComponent self)
        {
            Log.Debug($"ET.Client.HomeShowComponentSystem.DoSelect unitId[{self.GetUnit().Id}] homeCfgId[{self.homeComponent.homeCfgId}]");

            return;
            // self.transDefaultShow.gameObject.SetActive(false);
            // self.transSelectShow.gameObject.SetActive(true);

            UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgBattleHomeHUD>(new DlgBattleHomeHUD_ShowWindowData()
            {
                homeUnitId = self.GetUnit().Id,
                homeCfgId = self.homeComponent.homeCfgId,
            }).Coroutine();
        }

        public static bool CancelSelect(this HomeShowComponent self)
        {
            bool bRet = false;
            if (self.transSelectShow.gameObject.activeSelf)
            {
                self.transDefaultShow.gameObject.SetActive(true);
                self.transSelectShow.gameObject.SetActive(false);
                bRet = true;

                Log.Debug($"ET.Client.HomeShowComponentSystem.CancelSelect unitId[{self.GetUnit().Id}] homeCfgId[{self.homeComponent.homeCfgId}]");

            }
            return bRet;
        }
    }
}