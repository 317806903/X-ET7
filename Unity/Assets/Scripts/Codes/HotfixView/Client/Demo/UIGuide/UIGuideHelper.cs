using System;
using System.Collections.Generic;
using UnityEngine;
using UIGuide;

namespace ET.Client
{
	[FriendOf(typeof(UIGuideComponent))]
	public static class UIGuideHelper
	{
		public static async ETTask DoUIGuide(Scene scene, string fileName, Action finished = null, Action firstFindCallBack = null)
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

				clientScene.AddComponent<UIGuideComponent>();
			}
			await UIGuideComponent.Instance.DoUIGuideByName(fileName, finished, firstFindCallBack);
		}

		public static async ETTask DoUIGuide(Scene scene, UIGuidePathList _UIGuidePathList, Action finished = null, Action firstFindCallBack = null)
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

				clientScene.AddComponent<UIGuideComponent>();
			}
			await UIGuideComponent.Instance.DoUIGuide(_UIGuidePathList, finished, firstFindCallBack);
		}

		public static async ETTask StopUIGuide(Scene scene)
		{
			if (UIGuideComponent.Instance == null)
			{
				return;
			}
			await UIGuideComponent.Instance.StopUIGuide();
		}

		public static UIGuidePath GetCurUIGuide(Scene scene)
		{
			if (UIGuideComponent.Instance == null)
			{
				return null;
			}
			UIGuideStepComponent uiGuideStepComponent = UIGuideComponent.Instance.CurUIGuideComponent;
			if (uiGuideStepComponent != null)
			{
				return uiGuideStepComponent.curUIGuidePath;
			}
			return null;
		}

		public static async ETTask<bool> DoStaticMethodChk(Scene scene, GuideConditionStaticMethodType staticMethod, string param)
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
				case GuideConditionStaticMethodType.ChkTowerPut:
					return await UIGuideHelper_StaticMethod.ChkTowerPut(scene);
					break;
				case GuideConditionStaticMethodType.ChkIsNotShowStory:
					return await UIGuideHelper_StaticMethod.ChkIsNotShowStory(scene);
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

		public static async ETTask DoStaticMethodExecute(Scene scene, GuideExecuteStaticMethodType staticMethod)
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
				case GuideExecuteStaticMethodType.EnterGuideBattle:
					await UIGuideHelper_StaticMethod.EnterGuideBattle(scene);
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
				default:
					break;
			}

			return;
		}
	}
}
