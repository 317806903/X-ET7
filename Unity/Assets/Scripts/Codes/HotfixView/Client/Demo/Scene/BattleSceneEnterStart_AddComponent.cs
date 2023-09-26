using UnityEngine.SceneManagement;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class BattleSceneEnterStart_AddComponent: AEvent<Scene, EventType.BattleSceneEnterStart>
    {
        protected override async ETTask Run(Scene scene, EventType.BattleSceneEnterStart args)
        {
            Log.Debug("BattleSceneEnterStart_AddComponent 11");
            Scene currentScene = scene.CurrentScene();

            UIManagerHelper.GetUIComponent(scene).HideAllShownWindow();
            //zpb UIManagerHelper.GetUIComponent(scene).CloseAllWindow();

            await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgLoading>();
            DlgLoading _DlgLoading = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgLoading>(true);

            Log.Debug("BattleSceneEnterStart_AddComponent 22");
            await ResComponent.Instance.LoadSceneAsync(currentScene.Name, _DlgLoading.UpdateProcess);

            Log.Debug("BattleSceneEnterStart_AddComponent 33");
            if (ET.Define.IsEditor)
            {
                if (currentScene.GetComponent<ChkHotFixComponent>() == null)
                {
                    currentScene.AddComponent<ChkHotFixComponent>();
                }
            }

            PlayerComponent playerComponent = ET.Client.PlayerHelper.GetMyPlayerComponent(scene);
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
            Log.Debug("BattleSceneEnterStart_AddComponent 44");
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