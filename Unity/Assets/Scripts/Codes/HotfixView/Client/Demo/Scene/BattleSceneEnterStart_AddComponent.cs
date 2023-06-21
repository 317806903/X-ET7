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

            if (currentScene.GetComponent<OperaComponent>() == null)
            {
                currentScene.AddComponent<OperaComponent>();
            }
            if (currentScene.GetComponent<CameraComponent>() == null)
            {
                currentScene.AddComponent<CameraComponent>();
            }
            CameraComponent cameraComponent = currentScene.GetComponent<CameraComponent>();
            cameraComponent.SetMainCamera(GlobalComponent.Instance.MainCamera);
            while (true)
            {
                Unit myUnit = UnitHelper.GetMyUnit(currentScene);
                if (ET.Ability.UnitHelper.ChkUnitAlive(myUnit))
                {
                    cameraComponent.Unit = myUnit;
                    return;
                }
                else
                {
                    await TimerComponent.Instance.WaitAsync(200);
                }
            }
        }
    }
}