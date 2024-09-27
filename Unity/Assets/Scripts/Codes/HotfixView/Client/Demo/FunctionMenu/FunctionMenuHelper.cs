using System;
using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;

namespace ET.Client
{
	public static class FunctionMenu
	{
        public static async ETTask ChkNeedShowFunctionMenuGuide(Scene scene, bool needWaitFrame = false)
        {
            if (ET.SceneHelper.ChkIsGameModeArcade())
            {
                return;
            }
            if (ET.SceneHelper.ChkIsDemoShow())
            {
                return;
            }

            if (needWaitFrame)
            {
                await TimerComponent.Instance.WaitFrameAsync();
            }
            PlayerFunctionMenuComponent playerFunctionMenuComponent = await ET.Client.PlayerCacheHelper.GetMyPlayerFunctionMenu(scene);
            List<string> openningList = playerFunctionMenuComponent.GetOpenningFunctionMenuList();
            if (openningList.Count > 0)
            {
                string functionMenuCfgId;
                if (openningList.Count > 1)
                {
                    functionMenuCfgId = openningList.OrderByDescending(cfgId => FunctionMenuCfgCategory.Instance.Get(cfgId).Priority).FirstOrDefault();
                }
                else
                {
                    functionMenuCfgId = openningList[0];
                }
                FunctionMenuCfg functionMenuCfg = FunctionMenuCfgCategory.Instance.Get(functionMenuCfgId);

                bool isUIGuideing = ET.Client.UIGuideHelper.ChkIsUIGuideing(scene, functionMenuCfg.UIGuideConfigFileName);
                if (isUIGuideing)
                {
                    return;
                }
                await ET.Client.UIGuideHelper.StopUIGuide(scene);

                Action<Scene> doGuile = async (scene) =>
                {
                    PlayerFunctionMenuComponent playerFunctionMenuComponent = await ET.Client.PlayerCacheHelper.GetMyPlayerFunctionMenu(scene);
                    if (string.IsNullOrEmpty(functionMenuCfg.UIGuideConfigFileName))
                    {
                        playerFunctionMenuComponent.ChgStatus(functionMenuCfgId, FunctionMenuStatus.Openned);
                        await ET.Client.PlayerCacheHelper.SaveMyPlayerModel(scene, PlayerModelType.FunctionMenu, null);

                        await ET.Client.FunctionMenu.ChkNeedShowFunctionMenuGuide(scene);
                    }
                    else
                    {
                        await ET.Client.UIGuideHelper.DoUIGuide(scene, functionMenuCfg.UIGuideConfigFileName, functionMenuCfg.Priority, 0, async (scene) =>
                        {
                            PlayerFunctionMenuComponent playerFunctionMenuComponent = await ET.Client.PlayerCacheHelper.GetMyPlayerFunctionMenu(scene);
                            playerFunctionMenuComponent.ChgStatus(functionMenuCfgId, FunctionMenuStatus.Openned);
                            await ET.Client.PlayerCacheHelper.SaveMyPlayerModel(scene, PlayerModelType.FunctionMenu, null);

                            ET.Client.FunctionMenu.ChkNeedShowFunctionMenuGuide(scene, true).Coroutine();
                        });
                    }
                };

                if (string.IsNullOrEmpty(functionMenuCfg.Icon))
                {
                    doGuile(scene);
                }
                else
                {
                    DlgFunctionMenuOpenShow_ShowWindowData _DlgFunctionMenuOpenShow_ShowWindowData = new()
                    {
                        functionMenuCfgId = functionMenuCfgId,
                        finished = doGuile,
                    };
                    UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgFunctionMenuOpenShow>(_DlgFunctionMenuOpenShow_ShowWindowData).Coroutine();
                }

            }
        }

        public static async ETTask ChkNeedShowGuideWhenBattleEnd(Scene scene)
        {
            if (ET.SceneHelper.ChkIsGameModeArcade())
            {
                return;
            }
            if (ET.SceneHelper.ChkIsDemoShow())
            {
                return;
            }
            PlayerFunctionMenuComponent playerFunctionMenuComponent = await ET.Client.PlayerCacheHelper.GetMyPlayerFunctionMenu(scene);
            List<string> openningList = playerFunctionMenuComponent.GetOpenningFunctionMenuList();
            if (openningList.Count > 0)
            {
                string functionMenuCfgId;
                if (openningList.Count > 1)
                {
                    functionMenuCfgId = openningList.OrderByDescending(cfgId => FunctionMenuCfgCategory.Instance.Get(cfgId).Priority).FirstOrDefault();
                }
                else
                {
                    functionMenuCfgId = openningList[0];
                }
                FunctionMenuCfg functionMenuCfg = FunctionMenuCfgCategory.Instance.Get(functionMenuCfgId);
                if (functionMenuCfg.IsGuideWhenBattleEnd)
                {
                    await ET.Client.UIGuideHelper.DoUIGuide(scene, "BattleTowerEndGuide", (int)ET.Client.GuidePriority.BattleTowerEndGuide, 0, (scene) =>
                    {

                    });
                }
            }
        }
	}
}
