using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Client
{
    public static class UnitHelper
    {
        public static UnitComponent GetUnitComponent(Scene scene)
        {
            Scene currentScene = null;
            Scene clientScene = null;
            if (scene == scene.ClientScene())
            {
                if (scene.GetComponent<CurrentScenesComponent>() != null)
                {
                    currentScene = scene.GetComponent<CurrentScenesComponent>().Scene;
                }
                clientScene = scene;
            }
            else
            {
                currentScene = scene;
                clientScene = currentScene.Parent.GetParent<Scene>();
            }

            if (currentScene == null)
            {
                return null;
            }
            UnitComponent unitComponent = currentScene.GetComponent<UnitComponent>();
            return unitComponent;
        }

        public static Unit GetMyObserverUnit(Scene scene)
        {
            UnitComponent unitComponent = GetUnitComponent(scene);
            if (unitComponent == null)
            {
                return null;
            }
            long myPlayerId = ET.Client.PlayerStatusHelper.GetMyPlayerId(scene);
            Unit observerUnit = unitComponent.Get(myPlayerId);
            return observerUnit;
        }

        public static Unit GetMyPlayerUnit(Scene scene)
        {
            Unit observerUnit = GetMyObserverUnit(scene);
            if (observerUnit == null)
            {
                return null;
            }
            Unit myPlayerUnit = ET.GamePlayHelper.GetCurPlayerUnit(observerUnit);
            return myPlayerUnit;
        }

        public static Unit GetMyCameraPlayerUnit(Scene scene)
        {
            Unit observerUnit = GetMyObserverUnit(scene);
            if (observerUnit == null)
            {
                return null;
            }
            Unit myCameraPlayerUnit = ET.GamePlayHelper.GetCameraPlayerUnit(observerUnit);
            return myCameraPlayerUnit;
        }

        public static Unit GetUnit(Scene scene, long unitId)
        {
            UnitComponent unitComponent = GetUnitComponent(scene);
            if (unitComponent != null)
            {
                Unit unit = unitComponent.Get(unitId);
                if (unit != null)
                {
                    return unit;
                }
            }

            return null;
        }

        public static float GetMaxSkillDis(Unit unit)
        {
            NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
            float skillDis = numericComponent.GetAsFloat(NumericType.SkillDis);
            return skillDis;
        }

        public static async ETTask<bool> ChkUnitExist(Entity self, long unitId, int retryNum = 1000)
        {
            Unit unit = ET.Ability.UnitHelper.GetUnit(self.DomainScene(), unitId);
            while (unit == null)
            {
                await TimerComponent.Instance.WaitFrameAsync();
                if (self.IsDisposed)
                {
                    return false;
                }
                retryNum--;
                if (retryNum <= 0)
                {
                    return false;
                }
                unit = ET.Ability.UnitHelper.GetUnit(self.DomainScene(), unitId);
            }
            return true;
        }

    }
}