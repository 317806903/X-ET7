using UnityEngine;
using UnityEngine.SceneManagement;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class BattleSceneEnterStart_AddComponent: AEvent<Scene, ClientEventType.BattleSceneEnterStart>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.BattleSceneEnterStart args)
        {
            Log.Debug("BattleSceneEnterStart_AddComponent 11");
            Scene currentScene = scene.CurrentScene();

            UIManagerHelper.GetUIComponent(scene).HideAllShownWindow();
            //zpb UIManagerHelper.GetUIComponent(scene).CloseAllWindow();

            await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgLoading>();

            Log.Debug("BattleSceneEnterStart_AddComponent 22");
            UnityEngine.SceneManagement.Scene sceneUnity = await ResComponent.Instance.LoadSceneAsync(currentScene.Name, (progress) =>
            {
                EventSystem.Instance.Publish(scene, new ClientEventType.LoadingProgress() { curProgress = progress });
            });

            foreach (GameObject go in sceneUnity.GetRootGameObjects())
            {
                go.transform.localScale *= ET.Ability.UnitHelper.GetClientGameMapScale(scene);
            }

            Log.Debug("BattleSceneEnterStart_AddComponent 33");
            if (ET.Define.IsEditor)
            {
                if (currentScene.GetComponent<ChkHotFixComponent>() == null)
                {
                    currentScene.AddComponent<ChkHotFixComponent>();
                }
            }

            bool isAR = false;
            PlayerStatusComponent playerStatusComponent = ET.Client.PlayerStatusHelper.GetMyPlayerStatusComponent(scene);
            if (playerStatusComponent.RoomTypeInfo.roomType == RoomType.Normal)
            {
                if (playerStatusComponent.RoomTypeInfo.subRoomType == SubRoomType.NormalARCreate
                    || playerStatusComponent.RoomTypeInfo.subRoomType == SubRoomType.NormalARScanCode)
                {
                    isAR = true;
                }
            }
            else if (playerStatusComponent.RoomTypeInfo.roomType == RoomType.AR)
            {
                isAR = true;
            }

            if (isAR == false)
            {
                await SetMainCamera(currentScene);
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