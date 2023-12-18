using System.Collections.Generic;

namespace ET.Ability
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
        }

        public static void InitWhenClient(Scene scene)
        {
            scene.AddComponent<UnitComponent>();
        }
    }
}