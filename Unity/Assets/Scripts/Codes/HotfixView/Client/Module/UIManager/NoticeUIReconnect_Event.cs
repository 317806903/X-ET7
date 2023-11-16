using System;
using ET.Ability.Client;
using UnityEngine.SceneManagement;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class NoticeUIReconnect_Event: AEvent<Scene, EventType.NoticeUIReconnect>
    {
        protected override async ETTask Run(Scene scene, EventType.NoticeUIReconnect args)
        {
            string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Net_ChkDisconnect");
            UIManagerHelper.ShowOnlyConfirm(scene, tipMsg, () =>
            {
                //SceneHelper.EnterLogin(scene).Coroutine();
                LoginHelper.LoginOut(scene).Coroutine();
            });
        }
    }
}