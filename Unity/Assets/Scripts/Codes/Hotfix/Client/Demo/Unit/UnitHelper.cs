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
            long myPlayerId = ET.Client.PlayerStatusHelper.GetMyPlayerId(scene);
            Unit observerUnit = unitComponent.Get(myPlayerId);
            return observerUnit;
        }

        public static Unit GetMyPlayerUnit(Scene scene)
        {
            Unit observerUnit = GetMyObserverUnit(scene);
            Unit myPlayerUnit = ET.GamePlayHelper.GetPlayerUnit(observerUnit);
            return myPlayerUnit;
        }

        public static void SendGetNumericUnit(Unit unit)
        {
            GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(unit.DomainScene());
            gamePlayComponent.RecordNeedGetNumericUnit(unit);
        }

    }
}