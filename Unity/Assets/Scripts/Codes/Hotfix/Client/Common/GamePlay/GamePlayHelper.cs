using System.Collections.Generic;
using ET.AbilityConfig;
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

		public static GamePlayPkComponent GetGamePlayPK(Scene scene)
		{
			return GetGamePlay(scene)?.GetComponent<GamePlayPkComponent>();
		}

		public static async ETTask SendARCameraPos(Scene scene, float3 ARCameraPosition, float3 ARCameraHitPosition)
		{
			ARCameraPosition = ET.Ability.UnitHelper.TranClientPos2ServerPos(scene, ARCameraPosition);
			ARCameraHitPosition = ET.Ability.UnitHelper.TranClientPos2ServerPos(scene, ARCameraHitPosition);
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

		public static async ETTask SendForceNextWaveWhenDebug(Scene scene)
		{
			if (ET.Client.SessionHelper.ChkSessionExist(scene) == false)
			{
				return;
			}

			C2M_ForceNextWaveWhenDebug _C2M_ForceNextWaveWhenDebug = new ();
			ET.Client.SessionHelper.GetSession(scene).Send(_C2M_ForceNextWaveWhenDebug);
			await ETTask.CompletedTask;
		}

		public static async ETTask SendForceAddGameGoldWhenDebug(Scene scene)
		{
			if (ET.Client.SessionHelper.ChkSessionExist(scene) == false)
			{
				return;
			}

			C2M_ForceAddGameGoldWhenDebug _C2M_ForceAddGameGoldWhenDebug = new ();
			ET.Client.SessionHelper.GetSession(scene).Send(_C2M_ForceAddGameGoldWhenDebug);
			await ETTask.CompletedTask;
		}

        public static async ETTask SendForceAddHomeHpWhenDebug(Scene scene)
		{
			if (ET.Client.SessionHelper.ChkSessionExist(scene) == false)
			{
				return;
			}

			C2M_ForceAddHomeHpWhenDebug _C2M_ForceAddHomeHpWhenDebug = new ();
			ET.Client.SessionHelper.GetSession(scene).Send(_C2M_ForceAddHomeHpWhenDebug);
			await ETTask.CompletedTask;
		}

		public static void SendPlayerMoveTarget(Scene scene, float3 position)
		{
			position = ET.Ability.UnitHelper.TranClientPos2ServerPos(scene, position);
			C2M_PathfindingResult c2MPathfindingResult = new();
			c2MPathfindingResult.Position = position;
			SessionHelper.GetSession(scene).Send(c2MPathfindingResult);
		}

		public static async ETTask<(bool, float3)> SendChkRay(Scene scene, float3 startPos, float3 endPos)
		{
			startPos = ET.Ability.UnitHelper.TranClientPos2ServerPos(scene, startPos);
			endPos = ET.Ability.UnitHelper.TranClientPos2ServerPos(scene, endPos);
			C2M_ChkRay _C2M_ChkRay = new();
			_C2M_ChkRay.StartPosition = startPos;
			_C2M_ChkRay.EndPosition = endPos;
			M2C_ChkRay _M2C_ChkRay = await SessionHelper.GetSession(scene).Call(_C2M_ChkRay) as M2C_ChkRay;
			bool bRet = false;
			float3 hitPos = float3.zero;
			if (_M2C_ChkRay.HitRet == 1)
			{
				bRet = true;
				hitPos = ET.Ability.UnitHelper.TranServerPos2ClientPos(scene, _M2C_ChkRay.HitPosition);
			}
			else
			{
				Log.Debug($"_M2C_ChkRay.HitRet != 1");
			}
			return (bRet, hitPos);
		}

		public static void SendResetAllUnitPos(Scene scene)
		{
			SessionHelper.GetSession(scene).Send(new C2M_ResetAllUnitPos());
		}
	}
}