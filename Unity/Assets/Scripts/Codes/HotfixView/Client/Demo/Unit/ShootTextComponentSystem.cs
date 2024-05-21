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
                self.unit2DamageShowList = new();
                self.Init().Coroutine();
            }
        }

        [ObjectSystem]
        public class DestroySystem: DestroySystem<ShootTextComponent>
        {
            protected override void Destroy(ShootTextComponent self)
            {
                self.unit2DamageShowList = null;
                if (self.shootTextRoot_Normal != null)
                {
                    self.shootTextProManager_Normal.Clear();
                    GameObjectPoolHelper.ReturnTransformToPool(self.shootTextRoot_Normal);
                    self.shootTextRoot_Normal = null;
                    self.shootTextProManager_Normal = null;
                }
                if (self.shootTextRoot_High != null)
                {
                    self.shootTextProManager_High.Clear();
                    GameObjectPoolHelper.ReturnTransformToPool(self.shootTextRoot_High);
                    self.shootTextRoot_High = null;
                    self.shootTextProManager_High = null;
                }
                if (self.shootTextRoot_Crt != null)
                {
                    self.shootTextProManager_Crt.Clear();
                    GameObjectPoolHelper.ReturnTransformToPool(self.shootTextRoot_Crt);
                    self.shootTextRoot_Crt = null;
                    self.shootTextProManager_Crt = null;
                }
                if (self.shootTextRoot_CrtAndHigh != null)
                {
                    self.shootTextProManager_CrtAndHigh.Clear();
                    GameObjectPoolHelper.ReturnTransformToPool(self.shootTextRoot_CrtAndHigh);
                    self.shootTextRoot_CrtAndHigh = null;
                    self.shootTextProManager_CrtAndHigh = null;
                }
                if (self.shootTextRoot_Cure != null)
                {
                    self.shootTextProManager_Cure.Clear();
                    GameObjectPoolHelper.ReturnTransformToPool(self.shootTextRoot_Cure);
                    self.shootTextRoot_Cure = null;
                    self.shootTextProManager_Cure = null;
                }
                if (ShootTextComponent.Instance == self)
                {
                    ShootTextComponent.Instance = null;
                }

                UIComponent _UIComponent = UIManagerHelper.GetUIComponent(self.DomainScene());
                _UIComponent.HideWindow<DlgBattleTowerHUDShow>();
            }
        }

        [ObjectSystem]
        public class UpdateSystem: UpdateSystem<ShootTextComponent>
        {
            protected override void Update(ShootTextComponent self)
            {
                self.Update();
            }
        }

        public static async ETTask Init(this ShootTextComponent self)
        {
            {
                ResEffectCfg resEffectCfg = ResEffectCfgCategory.Instance.Get("ResEffect_ShootDamageText_Normal");
                GameObject shootTextRootGo = GameObjectPoolHelper.GetObjectFromPool(resEffectCfg.ResName,true,1);
                self.shootTextRoot_Normal = shootTextRootGo.transform;
                shootTextRootGo.transform.SetParent(GlobalComponent.Instance.ClientManagerRoot);
                shootTextRootGo.transform.localPosition = Vector3.zero;
                shootTextRootGo.transform.localScale = Vector3.one;

                self.shootTextProManager_Normal = shootTextRootGo.GetComponentInChildren<ShootTextProManager>();
                self.shootTextProManager_Normal.ShootTextCamera = null;
                self.shootTextProManager_Normal.ShootTextCanvas = null;
            }

            {
                ResEffectCfg resEffectCfg = ResEffectCfgCategory.Instance.Get("ResEffect_ShootDamageText_High");
                GameObject shootTextRootGo = GameObjectPoolHelper.GetObjectFromPool(resEffectCfg.ResName,true,1);
                self.shootTextRoot_High = shootTextRootGo.transform;
                shootTextRootGo.transform.SetParent(GlobalComponent.Instance.ClientManagerRoot);
                shootTextRootGo.transform.localPosition = Vector3.zero;
                shootTextRootGo.transform.localScale = Vector3.one;

                self.shootTextProManager_High = shootTextRootGo.GetComponentInChildren<ShootTextProManager>();
                self.shootTextProManager_High.ShootTextCamera = null;
                self.shootTextProManager_High.ShootTextCanvas = null;
            }

            {
                ResEffectCfg resEffectCfg = ResEffectCfgCategory.Instance.Get("ResEffect_ShootDamageText_Crt");
                GameObject shootTextRootGo = GameObjectPoolHelper.GetObjectFromPool(resEffectCfg.ResName,true,1);
                self.shootTextRoot_Crt = shootTextRootGo.transform;
                shootTextRootGo.transform.SetParent(GlobalComponent.Instance.ClientManagerRoot);
                shootTextRootGo.transform.localPosition = Vector3.zero;
                shootTextRootGo.transform.localScale = Vector3.one;

                self.shootTextProManager_Crt = shootTextRootGo.GetComponentInChildren<ShootTextProManager>();
                self.shootTextProManager_Crt.ShootTextCamera = null;
                self.shootTextProManager_Crt.ShootTextCanvas = null;
            }

            {
                ResEffectCfg resEffectCfg = ResEffectCfgCategory.Instance.Get("ResEffect_ShootDamageText_CrtAndHigh");
                GameObject shootTextRootGo = GameObjectPoolHelper.GetObjectFromPool(resEffectCfg.ResName,true,1);
                self.shootTextRoot_CrtAndHigh = shootTextRootGo.transform;
                shootTextRootGo.transform.SetParent(GlobalComponent.Instance.ClientManagerRoot);
                shootTextRootGo.transform.localPosition = Vector3.zero;
                shootTextRootGo.transform.localScale = Vector3.one;

                self.shootTextProManager_CrtAndHigh = shootTextRootGo.GetComponentInChildren<ShootTextProManager>();
                self.shootTextProManager_CrtAndHigh.ShootTextCamera = null;
                self.shootTextProManager_CrtAndHigh.ShootTextCanvas = null;
            }

            {
                ResEffectCfg resEffectCfg = ResEffectCfgCategory.Instance.Get("ResEffect_ShootDamageText_Cure");
                GameObject shootTextRootGo = GameObjectPoolHelper.GetObjectFromPool(resEffectCfg.ResName,true,1);
                self.shootTextRoot_Cure = shootTextRootGo.transform;
                shootTextRootGo.transform.SetParent(GlobalComponent.Instance.ClientManagerRoot);
                shootTextRootGo.transform.localPosition = Vector3.zero;
                shootTextRootGo.transform.localScale = Vector3.one;

                self.shootTextProManager_Cure = shootTextRootGo.GetComponentInChildren<ShootTextProManager>();
                self.shootTextProManager_Cure.ShootTextCamera = null;
                self.shootTextProManager_Cure.ShootTextCanvas = null;
            }

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
            self.shootTextProManager_Normal.ShootTextCamera = camera;
            self.shootTextProManager_High.ShootTextCamera = camera;
            self.shootTextProManager_Crt.ShootTextCamera = camera;
            self.shootTextProManager_CrtAndHigh.ShootTextCamera = camera;
            self.shootTextProManager_Cure.ShootTextCamera = camera;

            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(self.DomainScene());
            _UIComponent.ShowWindow<DlgBattleTowerHUDShow>();
            DlgBattleTowerHUDShow _DlgBattleTowerHUDShow = _UIComponent.GetDlgLogic<DlgBattleTowerHUDShow>(true);
            self.shootTextProManager_Normal.ShootTextCanvas = _DlgBattleTowerHUDShow.View.EGDamageRootRectTransform.transform;
            self.shootTextProManager_High.ShootTextCanvas = _DlgBattleTowerHUDShow.View.EGDamageRootRectTransform.transform;
            self.shootTextProManager_Crt.ShootTextCanvas = _DlgBattleTowerHUDShow.View.EGDamageRootRectTransform.transform;
            self.shootTextProManager_CrtAndHigh.ShootTextCanvas = _DlgBattleTowerHUDShow.View.EGDamageRootRectTransform.transform;
            self.shootTextProManager_Cure.ShootTextCanvas = _DlgBattleTowerHUDShow.View.EGDamageRootRectTransform.transform;
        }

        public static Unit GetUnit(this ShootTextComponent self)
        {
            return self.GetParent<Unit>();
        }

        public static void AddShowShootDamage(this ShootTextComponent self, Unit unit, int value, bool isCrt)
        {
            if (GameSettingComponent.Instance.GetIsOn(GameSettingType.DamageShow) == false)
            {
                return;
            }
            if (self.unit2DamageShowList.TryGetValue(unit, out var queue) == false)
            {
                queue = new();
                self.unit2DamageShowList.Add(unit, queue);
            }
            queue.Enqueue((value, isCrt));
        }

        public static void Update(this ShootTextComponent self)
        {
            foreach (var item in self.unit2DamageShowList)
            {
                Unit unit = item.Key;
                if (item.Value.Count == 0)
                {
                    continue;
                }
                (int value, bool isCrt) = item.Value.Dequeue();
                self.ShowShootDamage(unit, value, isCrt);
            }

            if (self.curFrame++ > self.waitFrame)
            {
                self.curFrame = 0;

                using ListComponent<Unit> removeList = ListComponent<Unit>.Create();
                foreach (var item in self.unit2DamageShowList)
                {
                    Unit unit = item.Key;
                    if (item.Value.Count == 0)
                    {
                        removeList.Add(unit);
                    }
                }
                foreach (Unit unit in removeList)
                {
                    self.unit2DamageShowList.Remove(unit);
                }
                removeList.Dispose();
            }
        }

        public static void ShowShootDamage(this ShootTextComponent self, Unit unit, int value, bool isCrt)
        {

            if (GameSettingComponent.Instance.GetIsOn(GameSettingType.DamageShow) == false)
            {
                return;
            }

            if (self.shootTextRoot_Normal == null || self.shootTextRoot_Normal.gameObject.activeSelf == false)
            {
                return;
            }

            int showValue = value;
            bool isNeedShowPreOperator = false;
            ShootTextProManager shootTextProManager = null;
            if (showValue > 0)
            {
                isNeedShowPreOperator = true;
                shootTextProManager = self.shootTextProManager_Cure;
            }
            else
            {
                if (isCrt)
                {
                    isNeedShowPreOperator = true;
                    if (math.abs(showValue) > 1000)
                    {
                        shootTextProManager = self.shootTextProManager_CrtAndHigh;
                    }
                    else
                    {
                        shootTextProManager = self.shootTextProManager_Crt;
                    }
                }
                else if (math.abs(showValue) > 1000)
                {
                    shootTextProManager = self.shootTextProManager_High;
                }
                else
                {
                    shootTextProManager = self.shootTextProManager_Normal;
                }
            }

            shootTextProManager?.CreatShootText(isNeedShowPreOperator, TextMoveType.None, showValue, () =>
            {
                return self.GetShootTextTopPoint(unit);
            }, () =>
            {
                return self.GetShootTextButtomPoint(unit);
            });
        }

        public static Vector3 GetShootTextTopPoint(this ShootTextComponent self, Unit unit)
        {
            return (Vector3)unit.GetUnitClientPos() + Vector3.up * ET.Ability.UnitHelper.GetBodyHeight(unit);
        }

        public static Vector3 GetShootTextButtomPoint(this ShootTextComponent self, Unit unit)
        {
            return (Vector3)unit.GetUnitClientPos() + Vector3.up * (ET.Ability.UnitHelper.GetBodyHeight(unit) - 3);
        }

    }
}