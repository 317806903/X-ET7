using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class LoginSceneEnterStart_UI: AEvent<Scene, EventType.EnterLoginSceneStart>
    {
        protected override async ETTask Run(Scene scene, EventType.EnterLoginSceneStart args)
        {
            Scene clientScene = scene;

            clientScene.RemoveComponent<ARSessionComponent>();

            UIManagerHelper.GetUIComponent(scene).CloseAllWindow();

            UIAudioManagerHelper.PlayMusic(scene, MusicType.Login);

            await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgLoading>();
            DlgLoading _DlgLoading = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgLoading>(true);
            await ResComponent.Instance.LoadSceneAsync("Login", _DlgLoading.UpdateProcess);

            bool isFromInit = args.isFromInit;

            UIManagerHelper.GetUIComponent(clientScene).HideAllShownWindow();

            if (isFromInit)
            {
                await UIManagerHelper.GetUIComponent(clientScene).ShowWindowAsync<DlgLogin>();
            }
            else
            {
                UIComponent uiComponent = UIManagerHelper.GetUIComponent(clientScene);
                // 热更流程
                bool bRet = await EntryEvent3_InitClient.ChkHotUpdateAsync(clientScene, false);
                if (bRet == false)
                {
                }
                else
                {
                    uiComponent.HideAllShownWindow();
                    await uiComponent.ShowWindowAsync<DlgLogin>();
                }
            }
        }
    }
}