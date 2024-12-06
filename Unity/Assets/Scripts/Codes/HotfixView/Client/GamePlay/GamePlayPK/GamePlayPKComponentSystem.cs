using ET.Ability;
using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;

namespace ET.Client
{
    [FriendOf(typeof(GamePlayPkComponent))]
    public static class GamePlayPKComponentSystem
	{
		[ObjectSystem]
		public class GamePlayPKComponentUpdateSystem : UpdateSystem<GamePlayPkComponent>
		{
			protected override void Update(GamePlayPkComponent self)
			{
				if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Current)
				{
					return;
				}

				self.InitClient();
				self.DoUpdate();
			}
		}

		public static void InitClient(this GamePlayPkComponent self)
		{
			if (self.isInitClient)
			{
				return;
			}
			self.isInitClient = true;

			ET.Client.ModelClickManagerHelper.SetModelClickCallBack(self.DomainScene(), (rayHit) =>
			{
				if (self.IsDisposed)
				{
					return;
				}
				self.DoClickModel(rayHit);
			});
			ET.Client.ModelClickManagerHelper.SetModelPressCallBack(self.DomainScene(), (rayHit) =>
			{
				if (self.IsDisposed)
				{
					return;
				}
				self.DoPressModel(rayHit);
			});
		}

		public static void DoUpdate(this GamePlayPkComponent self)
		{
			//self.ChkMouseRightClick();
			self.SendARCameraPos();
		}

		public static bool ChkIsHitMap(this GamePlayPkComponent self, RaycastHit hit)
		{
			bool isHitTower = ModelClickManagerHelper.ChkIsHitTowerClickInfo(self.DomainScene(), hit);
			if (isHitTower)
			{
				return false;
			}

			GameObject hitGo = hit.collider.gameObject;
			bool isHitMap = false;
			if (ET.Client.PathLineRendererComponent.Instance.ChkIsHitPath(hitGo))
			{
				isHitMap = true;
			}
			else if (hitGo.layer == LayerMask.NameToLayer("Map"))
			{
				isHitMap = true;
			}

			return isHitMap;
		}

		public static void DoClickModel(this GamePlayPkComponent self, RaycastHit hit)
		{
			self.DoCancelHitLast();
			bool isHitMap = self.ChkIsHitMap(hit);
			if (isHitMap)
			{
				self.OnHitMap(hit);
			}
			else
			{
				bool isHitTower = ET.Client.ModelClickManagerHelper.ChkIsHitTowerClickInfo(self.DomainScene(), hit);
				if (isHitTower)
				{
					self.DoHitTower(hit);
				}

				bool isHitHome = ET.Client.ModelClickManagerHelper.ChkIsHitHomeClickInfo(self.DomainScene(), hit);
				if (isHitHome)
				{
					self.DoHitHome(hit);
				}

				bool isHitPlayerUnit = ET.Client.ModelClickManagerHelper.ChkIsHitPlayerUnitClickInfo(self.DomainScene(), hit);
				if (isHitPlayerUnit)
				{
					self.DoHitPlayerUnit(hit);
				}
			}
		}

		public static void DoPressModel(this GamePlayPkComponent self, RaycastHit hit)
		{
			bool isHitMap = self.ChkIsHitMap(hit);
			if (isHitMap)
			{
			}
			else
			{
				bool isHitTower = ET.Client.ModelClickManagerHelper.ChkIsHitTowerClickInfo(self.DomainScene(), hit);
				if (isHitTower)
				{
					self.DoPressHitTower(hit);
				}

				bool isHitPlayerUnit = ET.Client.ModelClickManagerHelper.ChkIsHitPlayerUnitClickInfo(self.DomainScene(), hit);
				if (isHitPlayerUnit)
				{
					self.DoPressPlayerUnit(hit);
				}
			}
		}

		public static void OnHitMap(this GamePlayPkComponent self, RaycastHit hit)
		{
			UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

			self.OnPlayerMoveTarget(hit.point);
		}

		public static void OnPlayerMoveTarget(this GamePlayPkComponent self, float3 targetPos)
		{
			C2M_PathfindingResult c2MPathfindingResult = new ();
			c2MPathfindingResult.Position = targetPos - new float3(0, 0f, 0);
			ET.Client.SessionHelper.GetSession(self.DomainScene()).Send(c2MPathfindingResult);
		}

		public static void DoHitTower(this GamePlayPkComponent self, RaycastHit hit)
		{
			Log.Debug($" hit.collider.name[{hit.collider.name}]");
			TowerShowComponent curTowerShowComponent = ET.Client.ModelClickManagerHelper.GetTowerInfoFromClickInfo(self.DomainScene(), hit);
			if (curTowerShowComponent != null)
			{
				curTowerShowComponent.DoSelect().Coroutine();
			}
		}

		public static void DoHitHome(this GamePlayPkComponent self, RaycastHit hit)
		{
			Log.Debug($" hit.collider.name[{hit.collider.name}]");
			HomeShowComponent curHomeShowComponent = ET.Client.ModelClickManagerHelper.GetHomeInfoFromClickInfo(self.DomainScene(), hit);
			if (curHomeShowComponent != null)
			{
				curHomeShowComponent.DoSelect().Coroutine();
			}
		}

		public static void DoHitPlayerUnit(this GamePlayPkComponent self, RaycastHit hit)
		{
			Log.Debug($" hit.collider.name[{hit.collider.name}]");
			PlayerUnitShowComponent curPlayerUnitShowComponent = ET.Client.ModelClickManagerHelper.GetPlayerUnitInfoFromClickInfo(self.DomainScene(), hit);
			curPlayerUnitShowComponent?.DoSelect();
		}

		public static void DoCancelHitLast(this GamePlayPkComponent self)
		{
			TowerShowComponent curTowerShowComponent = ET.Client.ModelClickManagerHelper.GetLastClickTowerInfo(self.DomainScene());
			curTowerShowComponent?.CancelSelect();
			PlayerUnitShowComponent curPlayerUnitShowComponent = ET.Client.ModelClickManagerHelper.GetLastClickPlayerUnitInfo(self.DomainScene());
			curPlayerUnitShowComponent?.CancelSelect();
		}

		public static void DoPressHitTower(this GamePlayPkComponent self, RaycastHit hit)
		{
			long myPlayerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());
			TowerShowComponent curTowerShowComponent = ET.Client.ModelClickManagerHelper.GetTowerInfoFromClickInfo(self.DomainScene(), hit);
			if (myPlayerId != curTowerShowComponent.towerComponent.playerId)
			{
				return;
			}
			self.DoMoveTower(curTowerShowComponent.towerComponent.towerCfgId, curTowerShowComponent.GetUnit().Id);
		}

		public static void DoMoveTower(this GamePlayPkComponent self, string towerCfgId, long towerUnitId)
		{
			ET.Ability.Client.AudioPlayHelper.PlayVibrate(MoreMountains.NiceVibrations.HapticTypes.Warning);

			Unit unitTower = Ability.UnitHelper.GetUnit(self.DomainScene(), towerUnitId);
			GameObjectShowComponent gameObjectShowComponent = unitTower.GetComponent<GameObjectShowComponent>();
			if (gameObjectShowComponent != null)
			{
				gameObjectShowComponent.ChgColor(true);
			}
			TowerShowComponent towerShowComponent = unitTower.GetComponent<TowerShowComponent>();
			if (towerShowComponent != null)
			{
				towerShowComponent.ShowOrHide(false);
			}
			DlgBattleDragItem_ShowWindowData showWindowData = new()
			{
				battleDragItemType = BattleDragItemType.PKMoveTower,
				battleDragItemParam = towerCfgId,
				moveTowerUnitId = towerUnitId,
				sceneIn = self.DomainScene(),
				callBack = (scene) =>
				{
					Unit unitTower = Ability.UnitHelper.GetUnit(scene, towerUnitId);
					GameObjectShowComponent gameObjectShowComponent = unitTower.GetComponent<GameObjectShowComponent>();
					if (gameObjectShowComponent != null)
					{
						gameObjectShowComponent.ChgColor(false);
					}
					TowerShowComponent towerShowComponent = unitTower.GetComponent<TowerShowComponent>();
					if (towerShowComponent != null)
					{
						towerShowComponent.ShowOrHide(true);
					}
				},
			};
			UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgBattleDragItem>(showWindowData).Coroutine();
		}

		public static void DoPressPlayerUnit(this GamePlayPkComponent self, RaycastHit hit)
		{
			long myPlayerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());
			PlayerUnitShowComponent curPlayerUnitShowComponent = ET.Client.ModelClickManagerHelper.GetPlayerUnitInfoFromClickInfo(self.DomainScene(), hit);
			if (myPlayerId != curPlayerUnitShowComponent.playerUnitComponent.playerId)
			{
				return;
			}
			self.DoMovePlayer(curPlayerUnitShowComponent.GetUnit().Id);
		}

		public static void DoMovePlayer(this GamePlayPkComponent self, long towerUnitId)
		{
			ET.Ability.Client.AudioPlayHelper.PlayVibrate(MoreMountains.NiceVibrations.HapticTypes.Warning);

			Unit unitTower = Ability.UnitHelper.GetUnit(self.DomainScene(), towerUnitId);
			GameObjectShowComponent gameObjectShowComponent = unitTower.GetComponent<GameObjectShowComponent>();
			if (gameObjectShowComponent != null)
			{
				gameObjectShowComponent.ChgColor(true);
			}
			TowerShowComponent towerShowComponent = unitTower.GetComponent<TowerShowComponent>();
			if (towerShowComponent != null)
			{
				towerShowComponent.ShowOrHide(false);
			}
			DlgBattleDragItem_ShowWindowData showWindowData = new()
			{
				battleDragItemType = BattleDragItemType.PKMovePlayer,
				moveTowerUnitId = towerUnitId,
				sceneIn = self.DomainScene(),
				callBack = (scene) =>
				{
					Unit unitTower = Ability.UnitHelper.GetUnit(scene, towerUnitId);
					GameObjectShowComponent gameObjectShowComponent = unitTower.GetComponent<GameObjectShowComponent>();
					if (gameObjectShowComponent != null)
					{
						gameObjectShowComponent.ChgColor(false);
					}
					TowerShowComponent towerShowComponent = unitTower.GetComponent<TowerShowComponent>();
					if (towerShowComponent != null)
					{
						towerShowComponent.ShowOrHide(true);
					}
				},
			};
			UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgBattleDragItem>(showWindowData).Coroutine();
		}

		public static void ChkMouseRightClick(this GamePlayPkComponent self)
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

		public static async ETTask OnChkRay(this GamePlayPkComponent self, float3 startPos, float3 endPos)
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

		public static void ShowRay(this GamePlayPkComponent self, float3 startPos, float3 endPos)
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

		public static void SendARCameraPos(this GamePlayPkComponent self)
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
			if (Physics.Raycast(cameraPos, camera.transform.forward, out hitInfo, 1000, _groundLayerMask))
			{
				cameraHitPos = hitInfo.point;
				ET.Client.GamePlayHelper.SendARCameraPos(self.DomainScene(), cameraPos, cameraHitPos).Coroutine();
			}

		}


	}
}