using System.Collections.Generic;
using ET.Ability;

namespace ET
{
    public static class SceneHelper
    {
        public static void InitWhenServer(Scene scene)
        {
            scene.AddComponent<UnitComponent>();
            scene.AddComponent<UnitDelayRemoveComponent>();
            scene.AddComponent<DamageComponent>();
            scene.AddComponent<ActionHandlerComponent>();
            scene.AddComponent<SceneEffectComponent>();
            scene.AddComponent<SelectHandleRecordManager>();
            scene.AddComponent<RecycleSelectHandleComponent>();
            scene.AddComponent<SyncDataManager>();
        }

        public static void InitWhenClient(Scene scene)
        {
            scene.AddComponent<UnitComponent>();
        }

        public static bool ChkIsGameModeArcade()
        {
            if (ServerSceneManagerComponent.Instance != null)
            {
                return ServerSceneManagerComponent.Instance.IsGameModeArcade;
            }
            if (ClientSceneManagerComponent.Instance != null)
            {
                return ClientSceneManagerComponent.Instance.IsGameModeArcade;
            }
            return false;
        }

        public static bool ChkIsDemoShow()
        {
            if (ClientSceneManagerComponent.Instance != null)
            {
                return ClientSceneManagerComponent.Instance.IsDemoShow;
            }
            return false;
        }
    }
}