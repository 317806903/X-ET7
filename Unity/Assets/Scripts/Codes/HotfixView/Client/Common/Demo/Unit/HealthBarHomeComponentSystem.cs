using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace ET.Client
{
    public static class HealthBarHomeComponentSystem
    {
        [ObjectSystem]
        public class AwakeSystem: AwakeSystem<HealthBarHomeComponent>
        {
            protected override void Awake(HealthBarHomeComponent self)
            {
            }
        }

        /*[ObjectSystem]
        public class UpdateSystem: UpdateSystem<HealthBarHomeComponent>
        {
            protected override void Update(HealthBarHomeComponent self)
            {
                self.Update();
            }
        }*/

        public static void UpdateHealth(this HealthBarHomeComponent self, bool isInit)
        {
            if (self.IsDisposed || self.go == null || self.go.activeSelf == false || self.mainCamera == null)
            {
                return;
            }

            if (self.GetUnit() == null)
            {
                return;
            }

            NumericComponent numericComponent = self.GetUnit().GetComponent<NumericComponent>();
            int curHp = math.max(numericComponent.GetAsInt(NumericType.Hp), 0);
            int maxHp = numericComponent.GetAsInt(NumericType.MaxHp);
            float normalizedHealth = (float)curHp / maxHp;

            if (self.hpValueShowTrans != null)
            {
                self.hpValueShowTrans.GetComponent<TextMeshProUGUI>().text = $"{curHp}/{maxHp}";
            }

            if (normalizedHealth > 0f && normalizedHealth < 1.0f)
            {
                if (self.go.activeSelf == false)
                {
                    self.go.SetActive(true);
                }
            }
            else if(normalizedHealth == 0)
            {
                if (self.go.activeSelf)
                {
                    self.go.SetActive(false);
                }
            }
            else if(normalizedHealth == 1)
            {
                if (self.go.activeSelf == false)
                {
                    self.go.SetActive(true);
                }
            }

            self.go.transform.GetComponent<Slider>().value = normalizedHealth;
        }

        public static void OnBeforeRenderUpdate(this HealthBarHomeComponent self)
        {
            if (++self.curFrame >= self.waitFrame)
            {
                self.curFrame = 0;

                self.SetHomeHealthBarPos();
            }
        }

        public static void SetHomeHealthBarPos(this HealthBarHomeComponent self)
        {
            if (self.IsDisposed || self.go == null || self.go.activeSelf == false || self.mainCamera == null)
            {
                return;
            }

            if (self.GetUnit() == null)
            {
                return;
            }

            float3 gameObjectPosition = self.GetUnit().Position + new float3(0,ET.Ability.UnitHelper.GetBodyHeight(self.GetUnit()),0);
            Vector3 dir = ((Vector3)gameObjectPosition - self.mainCamera.transform.position).normalized;
            float dot = Vector3.Dot(self.mainCamera.transform.forward, dir);

            if (dot > 0)
            {
                Vector2 screenPosition = self.mainCamera.WorldToScreenPoint(gameObjectPosition);

                // 将屏幕坐标转换为UI坐标
                Vector2 canvasPosition;
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(self.canvas, screenPosition, UIRootManagerComponent.Instance.UICamera, out canvasPosition))
                {
                    self.rectTrans.anchoredPosition = new Vector2(canvasPosition.x, canvasPosition.y + 15);
                    self.go.transform.localScale = Vector3.one;
                }

            }
            else
            {
                self.go.transform.localScale = Vector3.zero;
            }

            float distance = Vector3.SqrMagnitude((Vector3)gameObjectPosition - self.mainCamera.transform.position);
            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(self.DomainScene());
            DlgBattleTowerHUDShow _DlgBattleTowerHUDShow = _UIComponent.GetDlgLogic<DlgBattleTowerHUDShow>(true);
            if (_DlgBattleTowerHUDShow != null)
            {
                _DlgBattleTowerHUDShow.UpdateDistance(self.rectTrans, distance);
            }
        }


        [ObjectSystem]
        public class DestroySystem: DestroySystem<HealthBarHomeComponent>
        {
            protected override void Destroy(HealthBarHomeComponent self)
            {
                if (self.go != null)
                {
                    GameObjectPoolHelper.ReturnTransformToPool(self.go.transform);
                    self.go = null;
                }
                Application.onBeforeRender -= self.OnBeforeRenderUpdate;

                UIComponent _UIComponent = UIManagerHelper.GetUIComponent(self.DomainScene());
                if (_UIComponent != null)
                {
                    DlgBattleTowerHUDShow _DlgBattleTowerHUDShow = _UIComponent.GetDlgLogic<DlgBattleTowerHUDShow>(true);
                    if (_DlgBattleTowerHUDShow != null)
                    {
                        _DlgBattleTowerHUDShow.RemoveDic(self.rectTrans);
                    }
                }
            }
        }

        public static async ETTask Init(this HealthBarHomeComponent self)
        {
            await self._Init();
        }

        public static async ETTask _Init(this HealthBarHomeComponent self)
        {
            GameObject healthBarGo = GameObjectPoolHelper.GetObjectFromPool("HealthBarHome",true,1);

            self.go = healthBarGo;
            self.healthBar = self.go.transform.Find("Fill");
            self.backgroundBar = self.go.transform.Find("Backdroud");
            self.hpValueShowTrans = self.go.transform.Find("HpValueShow");

            self.mainCamera = null;
            self.canvas = null;

            Camera camera = CameraHelper.GetMainCamera(self.DomainScene());
            while (camera == null)
            {
                await TimerComponent.Instance.WaitFrameAsync();
                if (self.IsDisposed)
                {
                    return;
                }
                camera = CameraHelper.GetMainCamera(self.DomainScene());
                if (camera != null)
                {
                    await TimerComponent.Instance.WaitAsync(1000);
                    if (self.IsDisposed)
                    {
                        return;
                    }
                    camera = CameraHelper.GetMainCamera(self.DomainScene());
                }
            }
            self.mainCamera = camera;

            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(self.DomainScene());
            _UIComponent.ShowWindow<DlgBattleTowerHUDShow>();
            DlgBattleTowerHUDShow _DlgBattleTowerHUDShow = _UIComponent.GetDlgLogic<DlgBattleTowerHUDShow>(true);
            self.canvas = _DlgBattleTowerHUDShow.View.EGHPRootRectTransform;

            self.rectTrans = ((RectTransform)healthBarGo.transform);
            self.rectTrans.SetParent(_DlgBattleTowerHUDShow.View.EGRootRectTransform);
            self.rectTrans.localPosition = Vector3.zero;
            self.rectTrans.localScale = Vector3.one;

            self.UpdateHealth(true);
            Application.onBeforeRender += self.OnBeforeRenderUpdate;
        }

        public static Unit GetUnit(this HealthBarHomeComponent self)
        {
            return self.Parent.GetParent<Unit>();
        }

    }
}