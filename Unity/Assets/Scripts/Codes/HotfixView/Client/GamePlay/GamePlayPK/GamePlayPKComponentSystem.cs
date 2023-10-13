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
		public class GamePlayTowerDefenseComponentAwakeSystem : AwakeSystem<GamePlayPKComponent>
		{
			protected override void Awake(GamePlayPKComponent self)
			{
			}
		}

		[ObjectSystem]
		public class GamePlayTowerDefenseComponentDestroySystem : DestroySystem<GamePlayPKComponent>
		{
			protected override void Destroy(GamePlayPKComponent self)
			{
			}
		}

		[ObjectSystem]
		public class GamePlayTowerDefenseComponentUpdateSystem : UpdateSystem<GamePlayPKComponent>
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
			self.ChkMouseClick();
			self.ChkMouseRightClick();
			self.SendARCameraPos();
		}

		public static void ChkMouseClick(this GamePlayPKComponent self)
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

		public static void DoMouseClick(this GamePlayPKComponent self)
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
			if (hitGo.layer == LayerMask.NameToLayer("Map"))
			{
				if(math.abs(self.lastMousePosition.x - Input.mousePosition.x) < 5f
				   && math.abs(self.lastMousePosition.y - Input.mousePosition.y) < 5f
				   && math.abs(self.lastMousePosition.z - Input.mousePosition.z) < 5f)
				{
					self.OnHitMap(hit);
				}
			}
			else
			{
				self.ChkHitTower(hit);
			}

		}

		public static void OnHitMap(this GamePlayPKComponent self, RaycastHit hit)
		{
			ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioClick(self.DomainScene());

			self.OnPlayerMoveTarget(hit.point);
		}

		public static void OnPlayerMoveTarget(this GamePlayPKComponent self, float3 targetPos)
		{
			C2M_PathfindingResult c2MPathfindingResult = new C2M_PathfindingResult();
			c2MPathfindingResult.Position = targetPos - new float3(0, 0f, 0);
			ET.Client.SessionHelper.GetSession(self.DomainScene()).Send(c2MPathfindingResult);
		}

		public static void ChkHitTower(this GamePlayPKComponent self, RaycastHit hit)
		{
			Log.Debug($" hit.collider.name[{hit.collider.name}]");
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