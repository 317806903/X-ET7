using System.Net.Sockets;

namespace ET.Client
{
    public static class SceneFactory
    {
        public static async ETTask<Scene> CreateClientScene(int zone, string name)
        {
            Scene clientScene = EntitySceneFactory.CreateScene(zone, SceneType.Client, name, ClientSceneManagerComponent.Instance);
            clientScene.AddComponent<CurrentScenesComponent>();
            clientScene.AddComponent<ObjectWait>();
            clientScene.AddComponent<GameSettingComponent>();

            await EventSystem.Instance.PublishAsync(clientScene, new ClientEventType.AfterCreateClientScene());
            return clientScene;
        }

        public static Scene CreateCurrentScene(long id, int zone, string name, CurrentScenesComponent currentScenesComponent)
        {
            Scene currentScene = EntitySceneFactory.CreateScene(id, IdGenerater.Instance.GenerateInstanceId(), zone, SceneType.Current, name, currentScenesComponent);
            currentScenesComponent.Scene = currentScene;

            EventSystem.Instance.Publish(currentScene, new ClientEventType.AfterCreateCurrentScene());
            return currentScene;
        }


    }
}