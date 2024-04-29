using System;
using ET.Ability.Client;
using UnityEngine.SceneManagement;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class NoticePlayerCacheChg_Event: AEvent<Scene, EventType.NoticePlayerCacheChg>
    {
        protected override async ETTask Run(Scene scene, EventType.NoticePlayerCacheChg args)
        {
            if (args.playerModelType == PlayerModelType.ArcadeCoinAdd)
            {
                string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_ArcadeCoin_PaySuccessed");
                UIManagerHelper.ShowOnlyConfirm(scene, tipMsg, () =>
                {
                });
            }
        }
    }
}