using System;
using System.Collections.Generic;
using UnityEngine;
using UIGuide;

namespace ET.Client
{
	[FriendOf(typeof(UIGuideComponent))]
	public static class UIGuideHelper
	{
		public static async ETTask DoUIGuide(Scene scene, string fileName, int priority, int startIndex, Action<Scene> finished, Action<Scene, int> stepFinished = null)
		{
			if (UIGuideComponent.Instance == null)
			{
				Scene currentScene = null;
				Scene clientScene = null;
				if (scene == scene.ClientScene())
				{
					currentScene = scene.GetComponent<CurrentScenesComponent>().Scene;
					clientScene = scene;
				}
				else
				{
					currentScene = scene;
					clientScene = scene.ClientScene();
				}

				CurrentScenesComponent currentScenesComponent = clientScene.GetComponent<CurrentScenesComponent>();
				currentScenesComponent.AddComponent<UIGuideComponent>();
			}
			await UIGuideComponent.Instance.DoUIGuideByName(fileName, priority, startIndex, finished, stepFinished);
		}

		public static async ETTask DoUIGuide(Scene scene, string guideFileName, int priority, UIGuidePathList _UIGuidePathList, int startIndex, Action<Scene> finished, Action<Scene, int> stepFinished = null)
		{
			if (UIGuideComponent.Instance == null)
			{
				Scene currentScene = null;
				Scene clientScene = null;
				if (scene == scene.ClientScene())
				{
					currentScene = scene.GetComponent<CurrentScenesComponent>().Scene;
					clientScene = scene;
				}
				else
				{
					currentScene = scene;
					clientScene = scene.ClientScene();
				}

				CurrentScenesComponent currentScenesComponent = clientScene.GetComponent<CurrentScenesComponent>();
				currentScenesComponent.AddComponent<UIGuideComponent>();
			}
			await UIGuideComponent.Instance.DoUIGuide(guideFileName, priority, _UIGuidePathList, startIndex, finished, stepFinished);
		}

		public static async ETTask StopUIGuide(Scene scene)
		{
			if (UIGuideComponent.Instance == null)
			{
				return;
			}
			await UIGuideComponent.Instance.StopUIGuide();
		}

		public static bool ChkIsUIGuideing(Scene scene, string guideFileName, bool needIsGuiding = false)
		{
			if (UIGuideComponent.Instance == null)
			{
				return false;
			}
			UIGuideStepComponent uiGuideStepComponent = UIGuideComponent.Instance.CurUIGuideComponent;
			if (uiGuideStepComponent == null)
			{
				return false;
			}

			if (UIGuideComponent.Instance.guideFileName != guideFileName)
			{
				return false;
			}

			if (needIsGuiding)
			{
				if (uiGuideStepComponent.isGuiding == false)
				{
					return false;
				}
			}
			return true;
		}

		public static bool ChkIsUIGuideing(Scene scene, bool needIsGuiding = false)
		{
			if (UIGuideComponent.Instance == null)
			{
				return false;
			}
			UIGuideStepComponent uiGuideStepComponent = UIGuideComponent.Instance.CurUIGuideComponent;
			if (uiGuideStepComponent == null)
			{
				return false;
			}

			if (needIsGuiding)
			{
				if (uiGuideStepComponent.isGuiding == false)
				{
					return false;
				}
			}
			return true;
		}

		public static bool ChkCanReplaceCurGuideing(Scene scene, int priority)
		{
			if (UIGuideComponent.Instance == null)
			{
				return true;
			}
			UIGuideStepComponent uiGuideStepComponent = UIGuideComponent.Instance.CurUIGuideComponent;
			if (uiGuideStepComponent == null)
			{
				return true;
			}

			if (UIGuideComponent.Instance.priority > priority)
			{
				return false;
			}
			return true;
		}

		public static (string, UIGuidePath) GetCurUIGuide(Scene scene)
		{
			if (UIGuideComponent.Instance == null)
			{
				return (string.Empty, null);
			}
			UIGuideStepComponent uiGuideStepComponent = UIGuideComponent.Instance.CurUIGuideComponent;
			if (uiGuideStepComponent != null)
			{
				return (UIGuideComponent.Instance.guideFileName, uiGuideStepComponent.curUIGuidePath);
			}
			return (string.Empty, null);
		}

		public static async ETTask<bool> DoStaticMethodChk(Scene scene, GuideConditionStaticMethodType staticMethod, string param, UIGuideStepComponent guideStepComponent)
		{
			if (UIGuideComponent.Instance == null)
			{
				return false;
			}

			switch (staticMethod)
			{
				case GuideConditionStaticMethodType.None:
					return true;
					break;
				case GuideConditionStaticMethodType.ChkTowerPutSuccess:
					return await UIGuideHelper_StaticMethod.ChkTowerPutSuccess(guideStepComponent);
					break;
				case GuideConditionStaticMethodType.ChkTowerScaleSuccess:
					return await UIGuideHelper_StaticMethod.ChkTowerScaleSuccess(guideStepComponent);
					break;
				case GuideConditionStaticMethodType.ChkTowerReclaimSuccess:
					return await UIGuideHelper_StaticMethod.ChkTowerReclaimSuccess(guideStepComponent);
					break;
				case GuideConditionStaticMethodType.ChkTowerUpgradeSuccess:
					return await UIGuideHelper_StaticMethod.ChkTowerUpgradeSuccess(guideStepComponent);
					break;
				case GuideConditionStaticMethodType.ChkTowerMoveSuccess:
					return await UIGuideHelper_StaticMethod.ChkTowerMoveSuccess(guideStepComponent);
					break;
				case GuideConditionStaticMethodType.ChkIsNotShowStory:
					return await UIGuideHelper_StaticMethod.ChkIsNotShowStory(scene);
					break;
				case GuideConditionStaticMethodType.ChkIsNotShowVideo:
					return await UIGuideHelper_StaticMethod.ChkIsNotShowVideo(scene);
					break;
				case GuideConditionStaticMethodType.ChkWaitTime:
					return await UIGuideHelper_StaticMethod.ChkWaitTime(scene, param);
					break;
				case GuideConditionStaticMethodType.ChkARMeshShow:
					return await UIGuideHelper_StaticMethod.ChkARMeshShow(scene, param);
					break;
				default:
					break;
			}

			return false;
		}

		public static async ETTask DoStaticMethodExecute(Scene scene, GuideExecuteStaticMethodType staticMethod, string executeParam, UIGuideStepComponent guideStepComponent)
		{
			if (UIGuideComponent.Instance == null)
			{
				return;
			}

			switch (staticMethod)
			{
				case GuideExecuteStaticMethodType.None:
					break;
				case GuideExecuteStaticMethodType.ShowStory:
					await UIGuideHelper_StaticMethod.ShowStory(scene);
					break;
				case GuideExecuteStaticMethodType.ShowVideo:
					string tutorialCfgId = executeParam;
					await UIGuideHelper_StaticMethod.ShowVideo(scene, tutorialCfgId);
					break;
				case GuideExecuteStaticMethodType.EnterGuideBattleTutorialFirst:
					await UIGuideHelper_StaticMethod.EnterGuideBattleTutorialFirst(scene);
					break;
				case GuideExecuteStaticMethodType.EnterGuideBattlePVEFirst:
					await UIGuideHelper_StaticMethod.EnterGuideBattlePVEFirst(scene);
					break;
				case GuideExecuteStaticMethodType.ShowPointTower:
					await UIGuideHelper_StaticMethod.ShowPointTower(scene);
					break;
				case GuideExecuteStaticMethodType.HidePointTower:
					await UIGuideHelper_StaticMethod.HidePointTower(scene);
					break;
				case GuideExecuteStaticMethodType.HideTowerInfo:
					await UIGuideHelper_StaticMethod.HideTowerInfo(scene);
					break;
				case GuideExecuteStaticMethodType.ShowBattleTowerReady:
				{
					bool isShow = bool.Parse(executeParam);
					await UIGuideHelper_StaticMethod.ShowBattleTowerReady(scene, isShow);
					break;
				}
				case GuideExecuteStaticMethodType.ShowBattleTowerQuit:
				{
					bool isShow = bool.Parse(executeParam);
					await UIGuideHelper_StaticMethod.ShowBattleTowerQuit(scene, isShow);
					break;
				}
				case GuideExecuteStaticMethodType.ShowScanQuit:
				{
					bool isShow = bool.Parse(executeParam);
					UIGuideHelper_StaticMethod.ShowScanQuit(scene, isShow);
					break;
				}
				case GuideExecuteStaticMethodType.ShowScanVideo:
				{
					bool isShow = bool.Parse(executeParam);
					await UIGuideHelper_StaticMethod.ShowScanVideo(scene, isShow);
					break;
				}
				case GuideExecuteStaticMethodType.BackToGameModeAR:
				{
					await UIGuideHelper_StaticMethod.BackToGameModeAR(scene);
					break;
				}
				default:
					break;
			}

			return;
		}

		public static bool ChkStaticMethodParam(GuideConditionStaticMethodType staticMethod, string param)
		{
			switch (staticMethod)
			{
				case GuideConditionStaticMethodType.None:
					break;
				case GuideConditionStaticMethodType.ChkTowerPutSuccess:
					break;
				case GuideConditionStaticMethodType.ChkTowerScaleSuccess:
					break;
				case GuideConditionStaticMethodType.ChkTowerReclaimSuccess:
					break;
				case GuideConditionStaticMethodType.ChkTowerUpgradeSuccess:
					break;
				case GuideConditionStaticMethodType.ChkTowerMoveSuccess:
					break;
				case GuideConditionStaticMethodType.ChkIsNotShowStory:
					break;
				case GuideConditionStaticMethodType.ChkIsNotShowVideo:
					break;
				case GuideConditionStaticMethodType.ChkWaitTime:
					if (float.TryParse(param, out float waitTime) == false)
					{
						return false;
					}
					break;
				case GuideConditionStaticMethodType.ChkARMeshShow:
					break;
				default:
					break;
			}

			return true;
		}

		public static bool ChkStaticMethodExecuteParam(GuideExecuteStaticMethodType staticMethod, string executeParam)
		{
			switch (staticMethod)
			{
				case GuideExecuteStaticMethodType.None:
					break;
				case GuideExecuteStaticMethodType.ShowStory:
					break;
				case GuideExecuteStaticMethodType.ShowVideo:
					break;
				case GuideExecuteStaticMethodType.EnterGuideBattleTutorialFirst:
					break;
				case GuideExecuteStaticMethodType.EnterGuideBattlePVEFirst:
					break;
				case GuideExecuteStaticMethodType.ShowPointTower:
					break;
				case GuideExecuteStaticMethodType.HidePointTower:
					break;
				case GuideExecuteStaticMethodType.HideTowerInfo:
					break;
				case GuideExecuteStaticMethodType.ShowBattleTowerReady:
				{
					if (bool.TryParse(executeParam, out var vaule) == false)
					{
						return false;
					}
					break;
				}
				case GuideExecuteStaticMethodType.ShowBattleTowerQuit:
				{
					if (bool.TryParse(executeParam, out var vaule) == false)
					{
						return false;
					}
					break;
				}
				case GuideExecuteStaticMethodType.ShowScanQuit:
				{
					if (bool.TryParse(executeParam, out var vaule) == false)
					{
						return false;
					}
					break;
				}
				case GuideExecuteStaticMethodType.ShowScanVideo:
				{
					if (bool.TryParse(executeParam, out var vaule) == false)
					{
						return false;
					}
					break;
				}
				case GuideExecuteStaticMethodType.BackToGameModeAR:
				{
					break;
				}
				default:
					break;
			}

			return true;
		}
	}
}
