using UnityEngine.SceneManagement;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class HallSceneEnterStart_Scene: AEvent<Scene, EventType.HallSceneEnterStart>
    {
        protected override async ETTask Run(Scene scene, EventType.HallSceneEnterStart args)
        {
            Scene currentScene = scene.CurrentScene();
            if (currentScene != null)
            {
                currentScene.Dispose();
            }
            await ResComponent.Instance.LoadSceneAsync("Hall");

        }
    }
}