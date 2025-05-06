using System;
using UnityEngine.SceneManagement;

namespace ET.Client
{
    [Event(SceneType.Current|SceneType.Client)]
    public class NoticePlayerCacheChg_UIManagerHelper: AEvent<Scene, ClientEventType.NoticePlayerCacheChg>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.NoticePlayerCacheChg args)
        {
            if (args.playerModelType == PlayerModelType.TokenArcadeCoinAdd)
            {
                string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_ArcadeCoin_PaySuccessed");
                UIManagerHelper.ShowOnlyConfirm(scene, tipMsg, () =>
                {
                });
            }
        }
    }
}