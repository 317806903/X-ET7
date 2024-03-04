using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;

namespace ET.Client
{
    public static class ShowGetGoldTextComponentSystem
    {
        [ObjectSystem]
        public class AwakeSystem: AwakeSystem<ShowGetGoldTextComponent>
        {
            protected override void Awake(ShowGetGoldTextComponent self)
            {
                ShowGetGoldTextComponent.Instance = self;
                self.Init().Coroutine();
            }
        }

        [ObjectSystem]
        public class DestroySystem: DestroySystem<ShowGetGoldTextComponent>
        {
            protected override void Destroy(ShowGetGoldTextComponent self)
            {
                if (self.shootTextRoot != null)
                {
                    self.shootTextProManager.Clear();
                    GameObjectPoolHelper.ReturnTransformToPool(self.shootTextRoot);
                    self.shootTextRoot = null;
                    self.shootTextProManager = null;
                }
                if (ShowGetGoldTextComponent.Instance == self)
                {
                    ShowGetGoldTextComponent.Instance = null;
                }

                UIComponent _UIComponent = UIManagerHelper.GetUIComponent(self.DomainScene());
                _UIComponent.HideWindow<DlgBattleTowerHUDShow>();
            }
        }

        public static async ETTask Init(this ShowGetGoldTextComponent self)
        {
#if false//UNITY_EDITOR
#else
            if (GlobalSettingCfgCategory.Instance.ShowGetGold == false)
            {
                return;
            }
#endif

            ResEffectCfg resEffectCfg = ResEffectCfgCategory.Instance.Get("ResEffect_ShowGetGoldTextPrefab");
            GameObject shootTextRootGo = GameObjectPoolHelper.GetObjectFromPool(resEffectCfg.ResName,true,1);
            self.shootTextRoot = shootTextRootGo.transform;
            shootTextRootGo.transform.SetParent(GlobalComponent.Instance.ClientManagerRoot);
            shootTextRootGo.transform.localPosition = Vector3.zero;
            shootTextRootGo.transform.localScale = Vector3.one;

            self.shootTextProManager = shootTextRootGo.GetComponentInChildren<ShootTextProManager>();
            self.shootTextProManager.ShootTextCamera = null;
            self.shootTextProManager.ShootTextCanvas = null;

            Camera camera = CameraHelper.GetMainCamera(self.DomainScene());
            while (camera == null)
            {
                await TimerComponent.Instance.WaitFrameAsync();
                camera = CameraHelper.GetMainCamera(self.DomainScene());
                if (camera != null)
                {
                    await TimerComponent.Instance.WaitAsync(1000);
                    camera = CameraHelper.GetMainCamera(self.DomainScene());
                }
            }
            self.shootTextProManager.ShootTextCamera = camera;

            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(self.DomainScene());
            _UIComponent.ShowWindow<DlgBattleTowerHUDShow>();
            DlgBattleTowerHUDShow _DlgBattleTowerHUDShow = _UIComponent.GetDlgLogic<DlgBattleTowerHUDShow>(true);
            self.shootTextProManager.ShootTextCanvas = _DlgBattleTowerHUDShow.View.EGShowCoinGetRootRectTransform.transform;
        }

        public static Unit GetUnit(this ShowGetGoldTextComponent self)
        {
            return self.GetParent<Unit>();
        }

        public static void ShowGetGold(this ShowGetGoldTextComponent self, Unit unit, int value)
        {
            if (self.shootTextRoot == null || self.shootTextRoot.gameObject.activeSelf == false)
            {
                return;
            }

            string showValue = "";
            if (value > 0)
            {
                showValue = $"+{math.abs(value)}";
            }
            else
            {
                showValue = $"-{math.abs(value)}";
            }
            self.shootTextProManager.CreatShootText(showValue, TextAnimationType.Gold, TextMoveType.LeftParabola, () =>
            {
                return self.GetShootTextTopPoint(unit);
            }, () =>
            {
                return self.GetShootTextButtomPoint(unit);
            });
        }

        public static Vector3 GetShootTextTopPoint(this ShowGetGoldTextComponent self, Unit unit)
        {
            GameObjectComponent gameObjectComponent = unit.GetComponent<GameObjectComponent>();
            if (gameObjectComponent == null || gameObjectComponent.gameObject == null)
            {
                return (Vector3)unit.Position + Vector3.up * ET.Ability.UnitHelper.GetBodyHeight(unit);
            }
            Vector3 curPos = gameObjectComponent.gameObject.transform.position;
            curPos += Vector3.up * ET.Ability.UnitHelper.GetBodyHeight(unit);
            return curPos;
        }

        public static Vector3 GetShootTextButtomPoint(this ShowGetGoldTextComponent self, Unit unit)
        {
            GameObjectComponent gameObjectComponent = unit.GetComponent<GameObjectComponent>();
            if (gameObjectComponent == null || gameObjectComponent.gameObject == null)
            {
                return (Vector3)unit.Position + Vector3.up * (ET.Ability.UnitHelper.GetBodyHeight(unit) - 3);
            }
            Vector3 curPos = gameObjectComponent.gameObject.transform.position;
            curPos += Vector3.up * (ET.Ability.UnitHelper.GetBodyHeight(unit) - 3);
            return curPos;
        }

    }
}