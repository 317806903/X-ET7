using UnityEngine.SceneManagement;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class BattleSceneEnterStart_AddComponent: AEvent<Scene, EventType.BattleSceneEnterStart>
    {
        protected override async ETTask Run(Scene scene, EventType.BattleSceneEnterStart args)
        {
            Scene currentScene = scene.CurrentScene();
            
            await ResComponent.Instance.LoadSceneAsync(currentScene.Name);

            if (ET.Define.IsEditor)
            {
                if (currentScene.GetComponent<ChkHotFixComponent>() == null)
                {
                    currentScene.AddComponent<ChkHotFixComponent>();
                }
            }
            
            PlayerComponent playerComponent = scene.GetComponent<PlayerComponent>();
            if (playerComponent.PlayerGameMode == PlayerGameMode.None)
            {
                Log.Error("BattleSceneEnterStart_AddComponent playerComponent.PlayerGameMode == PlayerGameMode.None");
            }
            else if (playerComponent.PlayerGameMode == PlayerGameMode.SingleMap)
            {
                await SetMainCamera(currentScene);
            }
            else if (playerComponent.PlayerGameMode == PlayerGameMode.Room)
            {
                await SetMainCamera(currentScene);
            }
            else if (playerComponent.PlayerGameMode == PlayerGameMode.ARRoom)
            {
                
            }
        }

        public async ETTask SetMainCamera(Scene currentScene)
        {
            if (currentScene.GetComponent<CameraComponent>() == null)
            {
                currentScene.AddComponent<CameraComponent>();
            }
            CameraComponent cameraComponent = currentScene.GetComponent<CameraComponent>();
            cameraComponent.SetMainCamera(GlobalComponent.Instance.MainCamera);
            await cameraComponent.SetFollowPlayer();
        }
    }
}