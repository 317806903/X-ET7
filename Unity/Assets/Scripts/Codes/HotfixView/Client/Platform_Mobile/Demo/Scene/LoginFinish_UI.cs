using ET.AbilityConfig;
using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class LoginFinish_UI: AEvent<Scene, ClientEventType.LoginFinish>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.LoginFinish args)
        {
            SessionComponent sessionComponent = ET.Client.SessionHelper.GetSessionCompent(scene);

            if (sessionComponent.Session.GetComponent<ReLoginComponent>() == null)
            {
                sessionComponent.Session.AddComponent<ReLoginComponent>();
            }

            await ET.Client.UIManagerHelper.DealPlayerUIRedDotType(scene, true);
            await SceneHelper.EnterHall(scene, true, false);

            await ChkIsNeedTutorialFirst(scene);
        }

        protected async ETTask ChkIsNeedTutorialFirst(Scene scene)
        {
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
                    if (ET.Client.UIManagerHelper.ChkIsAR() == false)
                    {
                        string msg = LocalizeComponent.Instance.GetTextValue("非AR模式，立即完成新手指引");
                        ET.Client.UIManagerHelper.ShowOnlyConfirmHighest(scene, msg, () =>
                        {
                            FinishedCallBack(scene).Coroutine();
                        });
                    }
                    else
                    {
                        ET.Client.UIGuideHelper.DoUIGuide(scene, "TutorialFirst", (int)ET.Client.GuidePriority.TutorialFirst, 0, (scene) =>
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