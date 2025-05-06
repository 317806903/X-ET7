using System;
using System.Collections.Generic;

namespace ET.Ability
{
    [FriendOf(typeof (SelectHandleObj))]
    public static class SelectHandleObjSystem
    {
        [ObjectSystem]
        public class SelectHandleObjAwakeSystem: AwakeSystem<SelectHandleObj>
        {
            protected override void Awake(SelectHandleObj self)
            {
            }
        }

        [ObjectSystem]
        public class SelectHandleObjDestroySystem: DestroySystem<SelectHandleObj>
        {
            protected override void Destroy(SelectHandleObj self)
            {
                if (self.selectHandle != null)
                {
                    self.selectHandle.SetHolding(false);
                    ET.Ability.UnitHelper.AddRecycleSelectHandles(self.DomainScene(), self.selectHandle);
                    self.selectHandle = null;
                }
            }
        }

        public static Unit GetUnit(this SelectHandleObj self)
        {
            Unit unit = self.GetParent<Unit>();
            return unit;
        }

        public static void SaveSelectHandle(this SelectHandleObj self, SelectHandle selectHandle, bool isOnce)
        {
            if (isOnce)
            {
                if (self.isOnce == false && self.selectHandle != null && self.selectHandle.isDisposed == false)
                {
#if UNITY_EDITOR
                    Log.Error($"SaveSelectHandle isOnce==true but self.isOnce == false");
#endif
                    //return;
                }
            }
            if (self.selectHandle != null && self.selectHandle.isDisposed == false)
            {
                EventSystem.Instance.Publish(self.DomainScene(), new AbilityTriggerEventType.UnitChgSaveSelectObj()
                {
                    unit = self.GetUnit(),
                    selectHandle = selectHandle,
                });

                self.selectHandle.SetHolding(false);
                ET.Ability.UnitHelper.AddRecycleSelectHandles(self.DomainScene(), self.selectHandle);
            }
            self.selectHandle = selectHandle;
            self.selectHandle.SetHolding(true);
            self.isOnce = isOnce;
        }

        public static void ClearOnceSelectHandle(this SelectHandleObj self)
        {
            if (self.isOnce == false)
            {
                return;
            }
            if (self.selectHandle != null && self.selectHandle.isDisposed == false)
            {
                self.selectHandle.SetHolding(false);
                ET.Ability.UnitHelper.AddRecycleSelectHandles(self.DomainScene(), self.selectHandle);
            }
            self.selectHandle = null;
        }

        public static SelectHandle GetSaveSelectHandle(this SelectHandleObj self)
        {
            if (self.selectHandle != null && self.selectHandle.isDisposed == false)
            {
                return self.selectHandle;
            }
            return null;
        }
    }
}