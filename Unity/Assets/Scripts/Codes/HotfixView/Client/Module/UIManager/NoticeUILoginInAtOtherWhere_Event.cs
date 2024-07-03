using System;
using ET.Ability.Client;
using UnityEngine.SceneManagement;

namespace ET.Client
{
    [Event(SceneType.Current|SceneType.Client)]
    public class NoticeUILoginInAtOtherWhere_Event: AEvent<Scene, EventType.NoticeUILoginInAtOtherWhere>
    {
        protected override async ETTask Run(Scene scene, EventType.NoticeUILoginInAtOtherWhere args)
        {
            Scene clientScene = scene.ClientScene();
            string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Net_LoginInAtOtherWhere");
            UIManagerHelper.ShowOnlyConfirm(clientScene, tipMsg, () =>
            {
                LoginHelper.LoginOut(clientScene, true).Coroutine();
            });
        }
    }
}