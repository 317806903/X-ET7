using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class LoginSceneEnterStart_UI: AEvent<Scene, EventType.LoginSceneEnterStart>
    {
        protected override async ETTask Run(Scene scene, EventType.LoginSceneEnterStart args)
        {
            Scene clientScene = scene;

            clientScene.RemoveComponent<ARSessionComponent>();

            UIManagerHelper.GetUIComponent(scene).CloseAllWindow();

            ET.Ability.Client.UIAudioManagerHelper.PlayMusicLogin(scene);

            await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgLoading>();
            DlgLoading _DlgLoading = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgLoading>(true);
            await ResComponent.Instance.LoadSceneAsync("Login", _DlgLoading.UpdateProcess);

            bool isFromInit = args.isFromInit;

            UIManagerHelper.GetUIComponent(clientScene).HideAllShownWindow();
            await UIManagerHelper.GetUIComponent(clientScene).ShowWindowAsync<DlgLogin>();

        }
    }
}