using System;
using ET.AbilityConfig;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

namespace ET.Client
{
    public static class HealthBarUpgradeComponentSystem
    {
        [ObjectSystem]
        public class AwakeSystem: AwakeSystem<HealthBarUpgradeComponent>
        {
            protected override void Awake(HealthBarUpgradeComponent self)
            {
            }
        }

        [ObjectSystem]
        public class DestroySystem: DestroySystem<HealthBarUpgradeComponent>
        {
            protected override void Destroy(HealthBarUpgradeComponent self)
            {
                
            }
        }

        [ObjectSystem]
        public class UpdateSystem: UpdateSystem<HealthBarUpgradeComponent>
        {
            protected override void Update(HealthBarUpgradeComponent self)
            {
                self.Update();
            }
        }

        public static async ETTask Init(this HealthBarUpgradeComponent self, bool isShowWhenFull)
        {
#if Platform_Quest
            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(self.DomainScene());
            DlgBattleCastle_ShowWindowData showWindowData = new()
            {
                homeUnitId = self.GetUnit().Id,
            };
            _UIComponent.ShowWindowAsync<DlgBattleCastle>(showWindowData);
            await TimerComponent.Instance.WaitAsync(500);
            self.UpdateHealth(true);
#endif
        }

        public static Unit GetUnit(this HealthBarUpgradeComponent self)
        {
            return self.Parent.GetParent<Unit>();
        }

        public static void UpdateHealth(this HealthBarUpgradeComponent self, bool isInit)
        {
            if (self.IsDisposed)
            {
                return;
            }
            
#if Platform_Quest            
            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(self.DomainScene());
            DlgBattleCastle _DlgBattleCastle = _UIComponent.GetDlgLogic<DlgBattleCastle>(true);
            _DlgBattleCastle.UpdateHealth(self.GetUnit().Id);
#endif
        }

        public static void Update(this HealthBarUpgradeComponent self)
        {
            
        }
    }
}