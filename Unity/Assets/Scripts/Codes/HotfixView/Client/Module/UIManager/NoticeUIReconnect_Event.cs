using System;
using UnityEngine.SceneManagement;

namespace ET.Client
{
    [Event(SceneType.Current|SceneType.Client)]
    public class NoticeUIReconnect_Event: AEvent<Scene, EventType.NoticeUIReconnect>
    {
        protected override async ETTask Run(Scene scene, EventType.NoticeUIReconnect args)
        {
            Scene clientScene = scene.ClientScene();
            string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Net_ChkDisconnect");
            UIManagerHelper.ShowOnlyConfirmHighest(clientScene, tipMsg, () =>
            {
                //SceneHelper.EnterLogin(clientScene).Coroutine();
                LoginHelper.LoginOut(clientScene).Coroutine();
            });
        }
    }
}