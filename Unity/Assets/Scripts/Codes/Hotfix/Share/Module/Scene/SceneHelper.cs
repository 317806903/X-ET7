using System.Collections.Generic;

namespace ET.Ability
{
    public static class SceneHelper
    {
        public static void InitWhenServer(Scene scene)
        {
            scene.AddComponent<UnitComponent>();
            scene.AddComponent<DamageComponent>();
            scene.AddComponent<ActionHandlerComponent>();
        }
        
        public static void InitWhenClient(Scene scene)
        {
            scene.AddComponent<UnitComponent>();
        }
    }
}