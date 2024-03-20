using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;

namespace ET.Client
{
    public static class ShootTextComponentSystem
    {
        [ObjectSystem]
        public class AwakeSystem: AwakeSystem<ShootTextComponent>
        {
            protected override void Awake(ShootTextComponent self)
            {
                ShootTextComponent.Instance = self;
                self.Init().Coroutine();
            }
        }

        [ObjectSystem]
        public class DestroySystem: DestroySystem<ShootTextComponent>
        {
            protected override void Destroy(ShootTextComponent self)
            {
                if (self.shootTextRoot != null)
                {
                    self.shootTextProManager.Clear();
                    GameObjectPoolHelper.ReturnTransformToPool(self.shootTextRoot);
                    self.shootTextRoot = null;
                    self.shootTextProManager = null;
                }
                if (ShootTextComponent.Instance == self)
                {
                    ShootTextComponent.Instance = null;
                }

                UIComponent _UIComponent = UIManagerHelper.GetUIComponent(self.DomainScene());
                _UIComponent.HideWindow<DlgBattleTowerHUDShow>();
            }
        }

        public static async ETTask Init(this ShootTextComponent self)
        {
#if UNITY_EDITOR
#else
            if (GlobalSettingCfgCategory.Instance.ShowDamage == false)
            {
                return;
            }
#endif

            ResEffectCfg resEffectCfg = ResEffectCfgCategory.Instance.Get("ResEffect_ShootDamageTextPrefab");
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
            self.shootTextProManager.ShootTextCanvas = _DlgBattleTowerHUDShow.View.EGDamageRootRectTransform.transform;
        }

        public static Unit GetUnit(this ShootTextComponent self)
        {
            return self.GetParent<Unit>();
        }

        public static void ShowShootDamage(this ShootTextComponent self, Unit unit, int value)
        {
            if (self.shootTextRoot == null || self.shootTextRoot.gameObject.activeSelf == false)
            {
                return;
            }

#if UNITY_EDITOR
            if (ET.Client.DebugWhenEditorComponent.Instance.IsShowShootDamageNum == false)
            {
                if (GlobalSettingCfgCategory.Instance.ShowDamage == false)
                {
                    return;
                }
            }
#endif

            string showValue = "";
            if (value > 0)
            {
                showValue = $"-{value}";
            }
            else
            {
                showValue = $"+{math.abs(value)}";
            }
            self.shootTextProManager.CreatShootText(showValue, TextAnimationType.Normal, TextMoveType.LeftParabola, () =>
            {
                return self.GetShootTextTopPoint(unit);
            }, () =>
            {
                return self.GetShootTextButtomPoint(unit);
            });
        }

        public static Vector3 GetShootTextTopPoint(this ShootTextComponent self, Unit unit)
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

        public static Vector3 GetShootTextButtomPoint(this ShootTextComponent self, Unit unit)
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