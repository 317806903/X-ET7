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

            clientScene.RemoveComponent<CurrentScenesComponent>();
            clientScene.AddComponent<CurrentScenesComponent>();

            //UIManagerHelper.GetUIComponent(scene).CloseAllWindow();

            TimerComponent.Instance.RemoveAll();

            UIAudioManagerHelper.PlayMusic(scene, MusicType.Login);

            //await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgLoading>();
            //DlgLoading _DlgLoading = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgLoading>(true);
            //await ResComponent.Instance.LoadSceneAsync("Login", _DlgLoading.UpdateProcess);
            await ResComponent.Instance.LoadSceneAsync("Login", null);

            bool isFromInit = args.isFromInit;

            UIComponent uiComponent = UIManagerHelper.GetUIComponent(clientScene);
            if (isFromInit)
            {
                uiComponent.HideAllShownWindow(true, true);
                await uiComponent.ShowWindowAsync<DlgLogin>();
            }
            else
            {
                // 热更流程
                bool bRet = await EntryEvent3_InitClient.ChkHotUpdateAsync(clientScene, true);
                if (bRet == false)
                {
                    uiComponent.HideAllShownWindow(true, false);
                    // 打开热更界面
                    await uiComponent.ShowWindowAsync<DlgUpdate>();
                }
                else
                {
                    uiComponent.HideAllShownWindow(true, true);
                    await uiComponent.ShowWindowAsync<DlgLogin>();
                }
            }
        }
    }
}