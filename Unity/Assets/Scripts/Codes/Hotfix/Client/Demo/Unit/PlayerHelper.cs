using Unity.Mathematics;

namespace ET.Client
{
    public static class PlayerHelper
    {
        public static long GetMyPlayerId(Scene scene)
        {
            Scene currentScene = null;
            Scene clientScene = null;
            if (scene == scene.ClientScene())
            {
                currentScene = scene.GetComponent<CurrentScenesComponent>().Scene;
                clientScene = scene;
            }
            else
            {
                currentScene = scene;
                clientScene = currentScene.Parent.GetParent<Scene>();
            }

            PlayerComponent playerComponent = clientScene.GetComponent<PlayerComponent>();
            return playerComponent.MyId;
        }

    }
}