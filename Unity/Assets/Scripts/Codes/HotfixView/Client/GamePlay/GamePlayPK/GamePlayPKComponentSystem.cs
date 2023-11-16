using ET.Ability;
using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;

namespace ET.Client
{
    [FriendOf(typeof(GamePlayPKComponent))]
    public static class GamePlayPKComponentSystem
	{
		[ObjectSystem]
		public class GamePlayPKComponentUpdateSystem : UpdateSystem<GamePlayPKComponent>
		{
			protected override void Update(GamePlayPKComponent self)
			{
				if (self.DomainScene().SceneType != SceneType.Current)
				{
					return;
				}

				self.DoUpdate();
			}
		}

		public static void DoUpdate(this GamePlayPKComponent self)
		{
			//self.ChkMouseRightClick();
			self.SendARCameraPos();
		}

		public static (bool, bool) ChkIsHitMapOrTower(this GamePlayPKComponent self, RaycastHit hit)
		{
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

			return (isHitMap, isHitTower);
		}

		public static void DoClickModel(this GamePlayPKComponent self, RaycastHit hit)
		{
			(bool isHitMap, bool isHitTower) = self.ChkIsHitMapOrTower(hit);
			if (isHitMap)
			{
				self.OnHitMap(hit);
			}

			if (isHitTower)
			{
				self.DoHitTower(hit);
			}
		}

		public static void DoPressModel(this GamePlayPKComponent self, RaycastHit hit)
		{
			(bool isHitMap, bool isHitTower) = self.ChkIsHitMapOrTower(hit);
			if (isHitMap)
			{
			}

			if (isHitTower)
			{
				self.DoPressHitTower(hit);
			}
		}

		public static void OnHitMap(this GamePlayPKComponent self, RaycastHit hit)
		{
			UIAudioManagerHelper.PlayUIAudioClick(self.DomainScene());

			self.OnPlayerMoveTarget(hit.point);
		}

		public static void OnPlayerMoveTarget(this GamePlayPKComponent self, float3 targetPos)
		{
			C2M_PathfindingResult c2MPathfindingResult = new C2M_PathfindingResult();
			c2MPathfindingResult.Position = targetPos - new float3(0, 0f, 0);
			ET.Client.SessionHelper.GetSession(self.DomainScene()).Send(c2MPathfindingResult);
		}

		public static void DoHitTower(this GamePlayPKComponent self, RaycastHit hit)
		{
			Log.Debug($" hit.collider.name[{hit.collider.name}]");
		}

		public static void DoPressHitTower(this GamePlayPKComponent self, RaycastHit hit)
		{
			PlayerOwnerTowersComponent playerOwnerTowersComponent = self.GetComponent<PlayerOwnerTowersComponent>();
			if (playerOwnerTowersComponent == null)
			{
				return;
			}
			GameObject hitGo = hit.collider.gameObject;

			long myPlayerId = PlayerHelper.GetMyPlayerId(self.DomainScene());
			foreach (List<long> unitIds in playerOwnerTowersComponent.playerId2unitTowerId.Values)
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
							towerShowComponent.CancelSelect();
							self.DoMoveTower(towerShowComponent.towerComponent.towerCfgId, unitId);
						}
						else
						{
							towerShowComponent.CancelSelect();
						}
					}
				}
			}
		}

		public static void DoMoveTower(this GamePlayPKComponent self, string towerCfgId, long towerUnitId)
		{
			Handheld.Vibrate();

			DlgBattleDragItem_ShowWindowData showWindowData = new()
			{
				battleDragItemType = BattleDragItemType.MoveTower,
				battleDragItemParam = towerCfgId,
				moveTowerUnitId = towerUnitId,
				callBack = () =>
				{
				},
			};
			UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgBattleDragItem>(showWindowData).Coroutine();
		}

		public static void ChkMouseRightClick(this GamePlayPKComponent self)
		{
			if (Application.isEditor == false)
			{
				return;
			}

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

		public static async ETTask OnChkRay(this GamePlayPKComponent self, float3 startPos, float3 endPos)
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

		public static void ShowRay(this GamePlayPKComponent self, float3 startPos, float3 endPos)
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

		public static void SendARCameraPos(this GamePlayPKComponent self)
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
			self.lastSendTime = TimeHelper.ClientNow() + 2000;

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