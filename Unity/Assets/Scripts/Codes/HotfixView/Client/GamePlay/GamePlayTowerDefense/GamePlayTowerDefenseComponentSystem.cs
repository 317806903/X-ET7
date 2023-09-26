using ET.Ability;
using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;

namespace ET.Client
{
    [FriendOf(typeof(GamePlayTowerDefenseComponent))]
    public static class GamePlayTowerDefenseComponentSystem
	{
		[ObjectSystem]
		public class GamePlayTowerDefenseComponentAwakeSystem : AwakeSystem<GamePlayTowerDefenseComponent>
		{
			protected override void Awake(GamePlayTowerDefenseComponent self)
			{
			}
		}

		[ObjectSystem]
		public class GamePlayTowerDefenseComponentDestroySystem : DestroySystem<GamePlayTowerDefenseComponent>
		{
			protected override void Destroy(GamePlayTowerDefenseComponent self)
			{
			}
		}

		[ObjectSystem]
		public class GamePlayTowerDefenseComponentUpdateSystem : UpdateSystem<GamePlayTowerDefenseComponent>
		{
			protected override void Update(GamePlayTowerDefenseComponent self)
			{
				if (self.DomainScene().SceneType != SceneType.Current)
				{
					return;
				}

				self.DoUpdate();
			}
		}

		public static void DoUpdate(this GamePlayTowerDefenseComponent self)
		{
			self.ChkMouseClick();
			self.DoDrawAllMonsterCall2HeadQuarter();
			self.ChkMouseRightClick();
			self.SendARCameraPos();
		}

		public static void ChkMouseClick(this GamePlayTowerDefenseComponent self)
		{
			if (Input.GetMouseButtonDown(0))
			{
				self.lastMouseDownTime = TimeHelper.ClientFrameTime();
				self.lastMousePosition = Input.mousePosition;
			}
			if (Input.GetMouseButtonUp(0))
			{
				long disTime = TimeHelper.ClientFrameTime() - self.lastMouseDownTime;
				if (disTime < 1000 * 0.3f)
				{
					self.DoMouseClick();
				}
			}
		}

		public static void DoMouseClick(this GamePlayTowerDefenseComponent self)
		{
			// if (ET.UGUIHelper.ChkMouseInput() == false)
			// {
			// 	return;
			// }

			bool bRet = ET.UGUIHelper.ChkMouseClick(self.DomainScene(), 1000, out RaycastHit hit);
			if (bRet == false)
			{
				return;
			}
			if (ET.UGUIHelper.IsClickUGUI())
			{
				return;
			}

			GameObject hitGo = hit.collider.gameObject;
			bool isHitMap = false;
			bool isHitTower = false;

			if (ET.Client.PathLineRendererComponent.Instance.ChkIsHitPath(hitGo))
			{
				isHitMap = true;
			}
			else if (hitGo.layer == LayerMask.NameToLayer("Map"))
			{
				isHitMap = true;
			}
			else
			{
				isHitTower = true;
			}

			if (isHitMap)
			{
				bool bRet1 = self.DoCancelSelectMyTower();
				if (bRet1 == false)
				{
					if(math.abs(self.lastMousePosition.x - Input.mousePosition.x) < 5f
					   && math.abs(self.lastMousePosition.y - Input.mousePosition.y) < 5f
					   && math.abs(self.lastMousePosition.z - Input.mousePosition.z) < 5f)
					{
						self.OnHitMap(hit);
					}
				}
			}
			if (isHitTower)
			{
				self.ChkHitTower(hit);
			}

		}

		public static void OnHitMap(this GamePlayTowerDefenseComponent self, RaycastHit hit)
		{
			ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioClick(self.DomainScene());

			self.OnPlayerMoveTarget(hit.point);
		}

		public static void OnPlayerMoveTarget(this GamePlayTowerDefenseComponent self, float3 targetPos)
		{
			C2M_PathfindingResult c2MPathfindingResult = new C2M_PathfindingResult();
			c2MPathfindingResult.Position = targetPos - new float3(0, 0f, 0);
			ET.Client.SessionHelper.GetSession(self.DomainScene()).Send(c2MPathfindingResult);
		}

		public static void ChkHitTower(this GamePlayTowerDefenseComponent self, RaycastHit hit)
		{
			PlayerOwnerTowersComponent playerOwnerTowersComponent = self.GetComponent<PlayerOwnerTowersComponent>();
			if (playerOwnerTowersComponent == null)
			{
				return;
			}
			GameObject hitGo = hit.collider.gameObject;

			long myPlayerId = PlayerHelper.GetMyPlayerId(self.DomainScene());
			bool isHitMyTower = false;
			if (playerOwnerTowersComponent.playerId2unitTowerId.TryGetValue(myPlayerId, out List<long> unitIds))
			{
				foreach (long unitId in unitIds)
				{
					Unit unit = ET.Ability.UnitHelper.GetUnit(self.DomainScene(), unitId);
					if (ET.Ability.UnitHelper.ChkUnitAlive(unit) == false)
					{
						continue;
					}

					TowerShowComponent towerShowComponent = unit.GetComponent<TowerShowComponent>();
					if (towerShowComponent != null)
					{
						if (towerShowComponent.transCollider.gameObject == hitGo)
						{
							self.OnHitTower(towerShowComponent, hit);
							isHitMyTower = true;
						}
						else
						{
							towerShowComponent.CancelSelect();
						}
					}
				}
			}

			if (isHitMyTower == false)
			{
				UIManagerHelper.ShowTip(self.DomainScene(), "这不是你的塔");
			}
			Log.Debug($" hit.collider.name[{hit.collider.name}]");
		}

		public static void OnHitTower(this GamePlayTowerDefenseComponent self, TowerShowComponent towerShowComponent, RaycastHit hit)
		{
			ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioClick(self.DomainScene());

			towerShowComponent.DoSelect();
		}

		public static void ChkAllMyTowerUpgrade(this GamePlayTowerDefenseComponent self)
		{
			PlayerOwnerTowersComponent playerOwnerTowersComponent = self.GetComponent<PlayerOwnerTowersComponent>();
			if (playerOwnerTowersComponent == null)
			{
				return;
			}
			long myPlayerId = PlayerHelper.GetMyPlayerId(self.DomainScene());
			if (playerOwnerTowersComponent.playerId2unitTowerId.TryGetValue(myPlayerId, out List<long> unitIds) == false)
			{
				return;
			}
			foreach (long unitId in unitIds)
			{
				Unit unit = ET.Ability.UnitHelper.GetUnit(self.DomainScene(), unitId);
				if (ET.Ability.UnitHelper.ChkUnitAlive(unit) == false)
				{
					continue;
				}

				TowerShowComponent towerShowComponent = unit.GetComponent<TowerShowComponent>();
				if (towerShowComponent != null)
				{
					towerShowComponent.ChkUpgradePlayerTower();
				}
			}
		}

		public static bool DoCancelSelectMyTower(this GamePlayTowerDefenseComponent self)
		{
			PlayerOwnerTowersComponent playerOwnerTowersComponent = self.GetComponent<PlayerOwnerTowersComponent>();
			if (playerOwnerTowersComponent == null)
			{
				return false;
			}
			long myPlayerId = PlayerHelper.GetMyPlayerId(self.DomainScene());
			if (playerOwnerTowersComponent.playerId2unitTowerId.TryGetValue(myPlayerId, out List<long> unitIds) == false)
			{
				return false;
			}

			bool bRet = false;
			foreach (long unitId in unitIds)
			{
				Unit unit = ET.Ability.UnitHelper.GetUnit(self.DomainScene(), unitId);
				if (ET.Ability.UnitHelper.ChkUnitAlive(unit) == false)
				{
					continue;
				}

				TowerShowComponent towerShowComponent = unit.GetComponent<TowerShowComponent>();
				if (towerShowComponent != null)
				{
					bool bRet1 = towerShowComponent.CancelSelect();
					if (bRet1)
					{
						bRet = true;
					}
				}
			}

			return bRet;
		}

		public static async ETTask<bool> DoDrawMyMonsterCall2HeadQuarter(this GamePlayTowerDefenseComponent self, float3 pos)
		{
			long myPlayerId = PlayerHelper.GetMyPlayerId(self.DomainScene());
			return await self.DoDrawMonsterCall2HeadQuarterByPos(myPlayerId, pos);
		}

		public static async ETTask<bool> DoDrawMonsterCall2HeadQuarterByPos(this GamePlayTowerDefenseComponent self, long playerId, float3 pos)
		{
			(float3 homePos, List<float3> points) = await ET.Client.GamePlayTowerDefenseHelper.SendGetMonsterCall2HeadQuarterPath(self.ClientScene(), pos);

			bool canArrive = false;
			if (points != null && points.Count > 0)
			{
				float3 lastPoint = points[points.Count - 1];
				if (math.abs(homePos.x - lastPoint.x) < 0.3f
				    && math.abs(homePos.y - lastPoint.y) < 0.3f
				    && math.abs(homePos.z - lastPoint.z) < 0.3f)
				{
					canArrive = true;
				}
			}

			await PathLineRendererComponent.Instance.ShowPath(playerId, canArrive, points);
			return canArrive;
		}

		public static void DoDrawAllMonsterCall2HeadQuarter(this GamePlayTowerDefenseComponent self)
		{
			PutMonsterCallComponent putMonsterCallComponent = self.GetComponent<PutMonsterCallComponent>();
			if (putMonsterCallComponent != null && putMonsterCallComponent.MonsterCallPos != null)
			{
				foreach (var monsterCallPos in putMonsterCallComponent.MonsterCallPos)
				{
					long playerId = monsterCallPos.Key;
					float3 pos = monsterCallPos.Value;
					if (ET.Client.PathLineRendererComponent.Instance.ChkIsShowPath(playerId, pos))
					{
						continue;
					}
					self.DoDrawMonsterCall2HeadQuarterByPos(playerId, pos).Coroutine();
				}
			}
		}


		public static void ChkMouseRightClick(this GamePlayTowerDefenseComponent self)
		{
#if !UNITY_EDITOR
			return;
#endif

			if (Input.GetMouseButtonUp(1))
			{
				try
				{
					Ray ray = ET.Client.CameraHelper.GetMainCamera(self.DomainScene()).ScreenPointToRay(Input.mousePosition);
					Vector3 startPos = ray.origin;
					Vector3 endPos = ray.GetPoint(10000);
					self.OnChkRay(startPos, endPos).Coroutine();
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
					throw;
				}
			}
		}

		public static async ETTask OnChkRay(this GamePlayTowerDefenseComponent self, float3 startPos, float3 endPos)
		{
			self.ShowRay(startPos, endPos);
			C2M_ChkRay _C2M_ChkRay = new ();
			_C2M_ChkRay.StartPosition = startPos;
			_C2M_ChkRay.EndPosition = endPos;
			M2C_ChkRay _M2C_ChkRay = await ET.Client.SessionHelper.GetSession(self.DomainScene()).Call(_C2M_ChkRay) as M2C_ChkRay;
			if (_M2C_ChkRay.HitRet == 1)
			{
				GameObject obj1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
				obj1.transform.position = _M2C_ChkRay.HitPosition;
			}
			else
			{
				Log.Debug($"_M2C_ChkRay.HitRet != 1");
			}
		}

		public static void ShowRay(this GamePlayTowerDefenseComponent self, float3 startPos, float3 endPos)
		{
			GameObject showRay = GameObject.Find("ShowRay");
			if (showRay != null)
			{
				GameObject.Destroy(showRay);
			}

			showRay = new GameObject("ShowRay");
			GameObject objStart = GameObject.CreatePrimitive(PrimitiveType.Cube);
			objStart.transform.SetParent(showRay.transform);
			objStart.transform.position = startPos;
			GameObject objEnd = GameObject.CreatePrimitive(PrimitiveType.Cube);
			objEnd.transform.SetParent(showRay.transform);
			objEnd.transform.position = endPos;

			GameObject objLineRenderer = new GameObject("LineRenderer");
			objLineRenderer.transform.SetParent(showRay.transform);
			LineRenderer lineRenderer = objLineRenderer.AddComponent<LineRenderer>();
			lineRenderer.useWorldSpace = true;
			lineRenderer.positionCount = 2;
			lineRenderer.SetPosition(0, startPos);
			lineRenderer.SetPosition(1, endPos);
		}

		public static void SendARCameraPos(this GamePlayTowerDefenseComponent self)
		{
			GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(self.DomainScene());
			if (gamePlayComponent.IsAR() == false)
			{
				return;
			}

			if (gamePlayComponent.gamePlayStatus != GamePlayStatus.Gaming)
			{
				return;
			}

			long leftTime = self.lastSendTime - TimeHelper.ClientNow();
			if (leftTime > 0)
			{
				return;
			}
			self.lastSendTime = TimeHelper.ClientNow() + 1000;

			Camera camera = CameraHelper.GetMainCamera(self.DomainScene());
			float3 cameraPos = camera.transform.position;
			float3 cameraHitPos = float3.zero;
			RaycastHit hitInfo;
			LayerMask _groundLayerMask = LayerMask.GetMask("Map");
			if (Physics.Raycast(cameraPos, camera.transform.forward, out hitInfo, 10000, _groundLayerMask))
			{
				cameraHitPos = hitInfo.point;
			}

			ET.Client.GamePlayHelper.SendARCameraPos(self.DomainScene(), cameraPos, cameraHitPos).Coroutine();
		}

	}
}