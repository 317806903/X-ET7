using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace ET.Client
{
    public static class HomeHealthBarComponentSystem
    {
        [ObjectSystem]
        public class AwakeSystem: AwakeSystem<HomeHealthBarComponent>
        {
            protected override void Awake(HomeHealthBarComponent self)
            {
                self.Init().Coroutine();
                self.UpdateHealth(true);
                Application.onBeforeRender += self.OnBeforeRenderUpdate;
            }
        }

        /*[ObjectSystem]
        public class UpdateSystem: UpdateSystem<HomeHealthBarComponent>
        {
            protected override void Update(HomeHealthBarComponent self)
            {
                self.Update();
            }
        }*/

        public static void UpdateHealth(this HomeHealthBarComponent self, bool isInit)
        {
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
                self.go.SetActive(true);
            }
            else
            {
                // self.go.SetActive(false);
                self.go.SetActive(true);
            }

            self.go.transform.GetComponent<Slider>().value = normalizedHealth;
        }

        public static void OnBeforeRenderUpdate(this HomeHealthBarComponent self)
        {
            if (self.IsDisposed || self.go == null || self.go.activeSelf == false || self.camera == null)
            {
                return;
            }

            float3 gameObjectPosition = self.GetUnit().Position + new float3(0,self.GetUnit().model.BodyHeight,0);
            Vector3 dir = ((Vector3)gameObjectPosition - self.camera.transform.position).normalized;
            float dot = Vector3.Dot(self.camera.transform.forward, dir);

            if (dot > 0)
            {
                Vector2 screenPosition = self.camera.WorldToScreenPoint(gameObjectPosition);

                // 将屏幕坐标转换为UI坐标
                Vector2 canvasPosition;
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(self.canvas, screenPosition, UIManagerComponent.Instance.UICamera, out canvasPosition))
                {
                    self.rectTrans.anchoredPosition = new Vector2(canvasPosition.x, canvasPosition.y + 15);
                    self.go.transform.localScale = Vector3.one;
                }

            }
            else
            {
                self.go.transform.localScale = Vector3.zero;
            }

            float distance = Vector3.SqrMagnitude((Vector3)gameObjectPosition - self.camera.transform.position);
            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(self.DomainScene());
            DlgBattleTowerHUDShow _DlgBattleTowerHUDShow = _UIComponent.GetDlgLogic<DlgBattleTowerHUDShow>(true);
            if (_DlgBattleTowerHUDShow != null)
            {
                _DlgBattleTowerHUDShow.UpdateDistance(self.rectTrans,distance);
            }
        }


        [ObjectSystem]
        public class DestroySystem: DestroySystem<HomeHealthBarComponent>
        {
            protected override void Destroy(HomeHealthBarComponent self)
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

        public static async ETTask Init(this HomeHealthBarComponent self)
        {
            GameObject healthBarGo = GameObjectPoolHelper.GetObjectFromPool("HealthBarHome",true,1);

            self.go = healthBarGo;
            self.healthBar = self.go.transform.Find("Fill");
            self.backgroundBar = self.go.transform.Find("Backdroud");
            self.hpValueShowTrans = self.go.transform.Find("HpValueShow");

            self.camera = null;
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
            self.camera = camera;

            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(self.DomainScene());
            _UIComponent.ShowWindow<DlgBattleTowerHUDShow>();
            DlgBattleTowerHUDShow _DlgBattleTowerHUDShow = _UIComponent.GetDlgLogic<DlgBattleTowerHUDShow>(true);
            self.canvas = _DlgBattleTowerHUDShow.View.EGHPRootRectTransform;

            self.rectTrans = ((RectTransform)healthBarGo.transform);
            self.rectTrans.SetParent(_DlgBattleTowerHUDShow.View.EGRootRectTransform);
            self.rectTrans.localPosition = Vector3.zero;
            self.rectTrans.localScale = Vector3.one;
        }

        public static Unit GetUnit(this HomeHealthBarComponent self)
        {
            return self.Parent.GetParent<Unit>();
        }

    }
}