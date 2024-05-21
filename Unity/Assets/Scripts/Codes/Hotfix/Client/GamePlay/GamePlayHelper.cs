using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Client
{
    public static class GamePlayHelper
	{
		public static GamePlayComponent GetGamePlay(Scene scene)
		{
			Scene currentScene = scene.ClientScene().CurrentScene();
			return ET.GamePlayHelper.GetGamePlay(currentScene);
		}

		public static GamePlayTowerDefenseComponent GetGamePlayTowerDefense(Scene scene)
		{
			return GetGamePlay(scene)?.GetComponent<GamePlayTowerDefenseComponent>();
		}

		public static GamePlayPKComponent GetGamePlayPK(Scene scene)
		{
			return GetGamePlay(scene)?.GetComponent<GamePlayPKComponent>();
		}

		public static async ETTask SendARCameraPos(Scene scene, float3 ARCameraPosition, float3 ARCameraHitPosition)
		{
			C2M_SendARCameraPos _C2M_SendARCameraPos = new ()
			{
				ARCameraPosition = ARCameraPosition,
				ARCameraHitPosition = ARCameraHitPosition,
			};
			ET.Client.SessionHelper.GetSession(scene).Send(_C2M_SendARCameraPos);
			await ETTask.CompletedTask;
		}

		public static async ETTask SendNeedReNoticeUnitIds(Scene scene, List<long> unitIds)
		{
			if (unitIds == null || unitIds.Count == 0)
			{
				return;
			}
			C2M_NeedReNoticeUnitIds _C2M_NeedReNoticeUnitIds = new ();
			_C2M_NeedReNoticeUnitIds.UnitIds = new();
			_C2M_NeedReNoticeUnitIds.UnitIds.AddRange(unitIds);
			ET.Client.SessionHelper.GetSession(scene).Send(_C2M_NeedReNoticeUnitIds);
			await ETTask.CompletedTask;
		}

		public static async ETTask SendNeedReNoticeTowerDefense(Scene scene)
		{
			C2M_NeedReNoticeTowerDefense _C2M_NeedReNoticeTowerDefense = new ();
			ET.Client.SessionHelper.GetSession(scene).Send(_C2M_NeedReNoticeTowerDefense);
			await ETTask.CompletedTask;
		}

		public static async ETTask SendSetStopActorMoveWhenDebug(Scene scene, bool isStopActorMove)
		{
			if (ET.Client.SessionHelper.ChkSessionExist(scene) == false)
			{
				return;
			}

			C2M_SetStopActorMoveWhenDebug _C2M_SetStopActorMoveWhenDebug = new ();
			_C2M_SetStopActorMoveWhenDebug.IsStopActorMove = isStopActorMove?1:0;
			ET.Client.SessionHelper.GetSession(scene).Send(_C2M_SetStopActorMoveWhenDebug);
			await ETTask.CompletedTask;
		}

		public static async ETTask SendForceGameEndWhenDebug(Scene scene)
		{
			if (ET.Client.SessionHelper.ChkSessionExist(scene) == false)
			{
				return;
			}

			C2M_ForceGameEndWhenDebug _C2M_ForceGameEndWhenDebug = new ();
			ET.Client.SessionHelper.GetSession(scene).Send(_C2M_ForceGameEndWhenDebug);
			await ETTask.CompletedTask;
		}
	}
}