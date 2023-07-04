using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class EnterMapFinish_UI: AEvent<Scene, EventType.EnterMapFinish>
    {
        protected override async ETTask Run(Scene scene, EventType.EnterMapFinish args)
        {
            scene.GetComponent<UIComponent>().HideAllShownWindow();
            //zpb scene.GetComponent<UIComponent>().CloseAllWindow();
            
            //////
            
            PlayerComponent playerComponent = scene.GetComponent<PlayerComponent>();
            if (playerComponent.PlayerGameMode == PlayerGameMode.None)
            {
                Log.Error($"playerComponent.PlayerGameMode == PlayerGameMode.None");
            }
            else if (playerComponent.PlayerGameMode == PlayerGameMode.SingleMap)
            {
                await scene.GetComponent<UIComponent>().ShowWindowAsync(WindowID.WindowID_Battle);
            }
            else if (playerComponent.PlayerGameMode == PlayerGameMode.Room)
            {
                
                Scene currentScene = scene.CurrentScene();
                while (currentScene == null || currentScene.IsDisposed)
                {
                    await TimerComponent.Instance.WaitFrameAsync();
                    currentScene = scene.CurrentScene();
                }
                GamePlayComponent gamePlayComponent = currentScene.GetComponent<GamePlayComponent>();
                while (gamePlayComponent == null || gamePlayComponent.IsDisposed)
                {
                    await TimerComponent.Instance.WaitFrameAsync();
                    gamePlayComponent = currentScene.GetComponent<GamePlayComponent>();
                }
            
                await scene.GetComponent<UIComponent>().ShowWindowAsync(WindowID.WindowID_BattleTower);

            }
            else if (playerComponent.PlayerGameMode == PlayerGameMode.ARRoom)
            {
                Log.Error($"playerComponent.PlayerGameMode == PlayerGameMode.ARRoom");
            }
            
            
            await ETTask.CompletedTask;
        }
    }
}