using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using UnityEngine;
using UIGuide;

namespace ET.Client
{
	public static class UIGuideHelper_StaticMethod
	{
		public static long recordTime;

		public static async ETTask<bool> ChkTowerPut(Scene scene)
		{
			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(scene);
			if (gamePlayTowerDefenseComponent == null)
			{
				return false;
			}
			PlayerOwnerTowersComponent playerOwnerTowersComponent = gamePlayTowerDefenseComponent.GetComponent<PlayerOwnerTowersComponent>();
			if (playerOwnerTowersComponent == null)
			{
				return false;
			}

			long myPlayerId = PlayerHelper.GetMyPlayerId(scene);
			if (playerOwnerTowersComponent.playerId2unitTowerId.ContainsKey(myPlayerId) == false)
			{
				return false;
			}
			foreach (var unitIds in playerOwnerTowersComponent.playerId2unitTowerId[myPlayerId])
			{
				return true;
			}

			return false;
		}

		public static async ETTask<bool> ChkWaitTime(Scene scene, string param)
		{
			await ETTask.CompletedTask;
			if (!float.TryParse(param, out float waitTime))
			{
				return true;
			}

			if (recordTime == 0)
			{
				recordTime = TimeHelper.ServerNow() + (long)(waitTime * 1000);
			}

			if (recordTime > TimeHelper.ServerNow())
			{
				return false;
			}
			recordTime = 0;

			return true;
		}

		public static async ETTask<bool> ChkARMeshShow(Scene scene, string param)
		{
			await ETTask.CompletedTask;


			return true;
		}

		public static async ETTask<bool> ChkIsNotShowStory(Scene scene)
		{
			DlgBeginnersGuideStory _DlgBeginnersGuideStory = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgBeginnersGuideStory>(true);
			if (_DlgBeginnersGuideStory == null)
			{
				return true;
			}
			return false;
		}

		public static async ETTask<bool> ChkIsNotShowVideo(Scene scene)
		{
			DlgVideoShow _DlgVideoShow = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgVideoShow>(true);
			if (_DlgVideoShow == null)
			{
				return true;
			}
			return false;
		}

		//-----------------------------------------------------------------------------------------------

		public static async ETTask ShowStory(Scene scene)
		{
			await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgBeginnersGuideStory>(new DlgBeginnersGuideStory_ShowWindowData()
			{
				finishCallBack = null,
			});

		}

		public static async ETTask ShowVideo(Scene scene)
		{
			await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgVideoShow>();

		}

		public static async ETTask EnterGuideBattle(Scene scene)
		{

			UIManagerHelper.GetUIComponent(scene).HideAllShownWindow();

			DlgARHall_ShowWindowData _DlgARHall_ShowWindowData = new()
			{
				playerStatus = PlayerStatus.Hall,
				RoomType = RoomType.AR,
				SubRoomType = SubRoomType.ARTutorialFirst,
				arRoomId = 0,
			};
			await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgARHall>(_DlgARHall_ShowWindowData);
		}

		public static async ETTask ShowPointTower(Scene scene)
		{
			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(scene);
			if (gamePlayTowerDefenseComponent == null)
			{
				return;
			}
			PlayerOwnerTowersComponent playerOwnerTowersComponent = gamePlayTowerDefenseComponent.GetComponent<PlayerOwnerTowersComponent>();
			if (playerOwnerTowersComponent == null)
			{
				return;
			}

			long myPlayerId = PlayerHelper.GetMyPlayerId(scene);
			if (playerOwnerTowersComponent.playerId2unitTowerId.ContainsKey(myPlayerId) == false)
			{
				return;
			}
			UnitComponent unitComponent = ET.Client.UnitHelper.GetUnitComponent(scene);
			foreach (var unitId in playerOwnerTowersComponent.playerId2unitTowerId[myPlayerId])
			{
				Unit unit = unitComponent.Get(unitId);
				while (ET.Ability.UnitHelper.ChkUnitAlive(unit) == false)
				{
					await TimerComponent.Instance.WaitFrameAsync();
					unit = unitComponent.Get(unitId);
				}

				GameObjectComponent gameObjectComponent = unit.GetComponent<GameObjectComponent>();
				while (gameObjectComponent == null || gameObjectComponent.GetGo() == null)
				{
					await TimerComponent.Instance.WaitFrameAsync();
				}

				unit.AddComponent<PointTowerComponent>();
				break;
			}
			await ETTask.CompletedTask;
		}

		public static async ETTask HidePointTower(Scene scene)
		{

			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(scene);
			if (gamePlayTowerDefenseComponent == null)
			{
				return;
			}
			PlayerOwnerTowersComponent playerOwnerTowersComponent = gamePlayTowerDefenseComponent.GetComponent<PlayerOwnerTowersComponent>();
			if (playerOwnerTowersComponent == null)
			{
				return;
			}

			long myPlayerId = PlayerHelper.GetMyPlayerId(scene);
			if (playerOwnerTowersComponent.playerId2unitTowerId.ContainsKey(myPlayerId) == false)
			{
				return;
			}
			UnitComponent unitComponent = ET.Client.UnitHelper.GetUnitComponent(scene);
			foreach (var unitId in playerOwnerTowersComponent.playerId2unitTowerId[myPlayerId])
			{
				Unit unit = unitComponent.Get(unitId);
				while (ET.Ability.UnitHelper.ChkUnitAlive(unit) == false)
				{
					await TimerComponent.Instance.WaitFrameAsync();
					unit = unitComponent.Get(unitId);
				}

				GameObjectComponent gameObjectComponent = unit.GetComponent<GameObjectComponent>();
				while (gameObjectComponent == null || gameObjectComponent.GetGo() == null)
				{
					await TimerComponent.Instance.WaitFrameAsync();
				}

				unit.RemoveComponent<PointTowerComponent>();
				break;
			}
			await ETTask.CompletedTask;
		}

		public static async ETTask HideTowerInfo(Scene scene)
		{
			UIManagerHelper.GetUIComponent(scene).HideWindow<DlgBattleTowerHUD>();
			await ETTask.CompletedTask;
		}

		public static async ETTask ShowBattleTowerReady(Scene scene, bool isShow)
		{
			DlgBattleTowerAR dlgBattleTowerAR = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgBattleTowerAR>();
			if (dlgBattleTowerAR != null)
			{
				dlgBattleTowerAR.View.EG_ReadyRectTransform.SetVisible(isShow);
			}

			DlgBattleTower dlgBattleTower = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgBattleTower>();
			if (dlgBattleTower != null)
			{
				dlgBattleTower.View.EG_ReadyRectTransform.SetVisible(isShow);
			}

			await ETTask.CompletedTask;
		}

		public static async ETTask ShowBattleTowerQuit(Scene scene, bool isShow)
		{
			DlgBattleTowerAR dlgBattleTowerAR = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgBattleTowerAR>();
			if (dlgBattleTowerAR != null)
			{
				dlgBattleTowerAR.View.E_QuitBattleButton.SetVisible(isShow);
			}

			DlgBattleTower dlgBattleTower = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgBattleTower>();
			if (dlgBattleTower != null)
			{
				dlgBattleTower.View.E_QuitBattleButton.SetVisible(isShow);
			}

			await ETTask.CompletedTask;
		}

		public static void ShowScanQuit(Scene scene, bool isShow)
		{
			ARSessionComponent arSessionComponent = ET.Client.ARSessionHelper.GetARSession(scene);
			arSessionComponent.ShowQuit(isShow);
		}
	}
}
