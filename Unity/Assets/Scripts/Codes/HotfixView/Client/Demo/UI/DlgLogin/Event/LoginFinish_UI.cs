using ET.AbilityConfig;
using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class LoginFinish_UI: AEvent<Scene, EventType.LoginFinish>
    {
        protected override async ETTask Run(Scene scene, EventType.LoginFinish args)
        {
            SessionComponent sessionComponent = ET.Client.SessionHelper.GetSessionCompent(scene);
            PingComponent pingComponent = sessionComponent.Session.GetComponent<PingComponent>();
            DebugShowComponent.Instance.SetPing(pingComponent);

            sessionComponent.Session.AddComponent<ReLoginComponent>();

            await SceneHelper.EnterHall(scene, true, false);

            if (DebugConnectComponent.Instance.IsDebugMode == false)
            {
                if (ET.SceneHelper.ChkIsGameModeArcade())
                {
                    return;
                }
                PlayerBaseInfoComponent playerBaseInfoComponent =
                    await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(scene);
                //Log.Debug($"--LoginFinish_UI playerBaseInfoComponent[{playerBaseInfoComponent}]");
                if (playerBaseInfoComponent.isFinishTutorialFirst == false)
                {
                    if (Application.isMobilePlatform == false)
                    {
                        string msg = LocalizeComponent.Instance.GetTextValue("非手机模式，立即完成新手指引");
                        ET.Client.UIManagerHelper.ShowOnlyConfirm(scene, msg, () =>
                        {
                            FinishedCallBack(scene).Coroutine();
                        });
                    }
                    else
                    {
                        ET.Client.UIGuideHelper.DoUIGuide(scene, "TutorialFirst", 0, () =>
                        {
                            FinishedCallBack(scene).Coroutine();
                        }).Coroutine();
                    }
                }
            }

        }

        protected async ETTask FinishedCallBack(Scene scene)
        {
            if (scene.IsDisposed)
            {
                return;
            }
            PlayerBaseInfoComponent playerBaseInfoComponent = await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(scene);
            playerBaseInfoComponent.isFinishTutorialFirst = true;
            await ET.Client.PlayerCacheHelper.SaveMyPlayerModel(scene, PlayerModelType.BaseInfo, new (){"isFinishTutorialFirst"});
            await ET.Client.PlayerCacheHelper.ReDealMyFunctionMenu(scene);
        }
    }
}