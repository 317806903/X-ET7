using System;
using UnityEngine.SceneManagement;

namespace ET.Client
{
    [Event(SceneType.Current|SceneType.Client)]
    public class NoticeUILoginInAtOtherWhere_UIManagerHelper: AEvent<Scene, ClientEventType.NoticeUILoginInAtOtherWhere>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.NoticeUILoginInAtOtherWhere args)
        {
            Scene clientScene = scene.ClientScene();
            string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Net_LoginInAtOtherWhere");
            UIManagerHelper.ShowOnlyConfirmHighest(clientScene, tipMsg, () =>
            {
                LoginHelper.LoginOut(clientScene, true).Coroutine();
            });
        }
    }
}