using System;
using UnityEngine.SceneManagement;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class HallSceneEnterStart_UI: AEvent<Scene, EventType.HallSceneEnterStart>
    {
        protected override async ETTask Run(Scene scene, EventType.HallSceneEnterStart args)
        {
            Scene clientScene = scene;

            await ResComponent.Instance.LoadSceneAsync("Hall");
            
            clientScene.GetComponent<UIComponent>().HideAllShownWindow();
            //zpb clientScene.GetComponent<UIComponent>().CloseAllWindow();
			
            PlayerComponent playerComponent = clientScene.GetComponent<PlayerComponent>();
            if (playerComponent.PlayerGameMode == PlayerGameMode.None)
            {
                await scene.GetComponent<UIComponent>().ShowWindowAsync(WindowID.WindowID_GameMode);
            }
            else if (playerComponent.PlayerGameMode == PlayerGameMode.SingleMap)
            {
                //进入全局场景，所有人都进同个Map
                await scene.GetComponent<UIComponent>().ShowWindowAsync(WindowID.WindowID_Lobby);
            }
            else if (playerComponent.PlayerGameMode == PlayerGameMode.Room)
            {
                //进入动态场景，按房间都进同个Map
                PlayerStatus playerStatus = clientScene.GetComponent<PlayerComponent>().PlayerStatus;
                if (playerStatus == PlayerStatus.Room)
                {
                    await clientScene.GetComponent<UIComponent>().ShowWindowAsync(WindowID.WindowID_Room);
                }
                else if (playerStatus == PlayerStatus.Hall)
                {
                    await clientScene.GetComponent<UIComponent>().ShowWindowAsync(WindowID.WindowID_Hall);
                }
                else if (playerStatus == PlayerStatus.Battle)
                {
                    RoomHelper.ReturnBackBattle(clientScene);
                }
            }
            else if (playerComponent.PlayerGameMode == PlayerGameMode.ARRoom)
            {
                //进入AR动态场景，按房间都进同个Map
            }
            
        }
    }
}