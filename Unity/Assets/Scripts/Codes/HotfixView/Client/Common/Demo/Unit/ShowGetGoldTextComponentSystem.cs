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
                    self.shootTextProManager.ClearPool();
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
            self.shootTextProManager.Init();
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
            self.shootTextProManager.UITextCamera = GlobalComponent.Instance.UICamera;

            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(self.DomainScene());
            _UIComponent.ShowWindow<DlgBattleTowerHUDShow>();
            DlgBattleTowerHUDShow _DlgBattleTowerHUDShow = _UIComponent.GetDlgLogic<DlgBattleTowerHUDShow>(true);
            self.shootTextProManager.ShootTextCanvas = _DlgBattleTowerHUDShow.View.EGShowCoinGetRootRectTransform;
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

            float clientResScale = ET.Ability.UnitHelper.GetClientGameResScale(self.DomainScene());
            int showValue = value;
            self.shootTextProManager.CreatShootText(clientResScale, true, TextMoveType.None, showValue, () =>
            {
                return self.GetShootTextTopPoint(unit);
            }, () =>
            {
                return self.GetShootTextButtomPoint(unit);
            });
        }

        public static Vector3 GetShootTextTopPoint(this ShowGetGoldTextComponent self, Unit unit)
        {
            if (self.IsDisposed || unit.IsDisposed)
            {

            }
            float clientResScale = ET.Ability.UnitHelper.GetClientGameResScale(self.DomainScene());

            return (Vector3)unit.GetUnitClientPos() + Vector3.up * (ET.Ability.UnitHelper.GetBodyHeight(unit) + clientResScale * 1.5f);
        }

        public static Vector3 GetShootTextButtomPoint(this ShowGetGoldTextComponent self, Unit unit)
        {
            float clientResScale = ET.Ability.UnitHelper.GetClientGameResScale(self.DomainScene());
            // return (Vector3)unit.GetUnitClientPos() + Vector3.up * (ET.Ability.UnitHelper.GetBodyHeight(unit) - clientResScale * 3);
            return (Vector3)unit.GetUnitClientPos();
        }
    }
}