using System;
using ET.Ability.Client;
using UnityEngine.SceneManagement;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class NoticeUILoginInAtOtherWhere_Event: AEvent<Scene, EventType.NoticeUILoginInAtOtherWhere>
    {
        protected override async ETTask Run(Scene scene, EventType.NoticeUILoginInAtOtherWhere args)
        {
            string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Net_LoginInAtOtherWhere");
            UIManagerHelper.ShowOnlyConfirm(scene, tipMsg, () =>
            {
                LoginHelper.LoginOut(scene, true).Coroutine();
            });
        }
    }
}