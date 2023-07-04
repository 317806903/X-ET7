using System;
using System.Collections.Generic;
using ET.Ability;
using Unity.Mathematics;

namespace ET.Client
{
	public static class GamePlayHelper
	{
		public static GamePlayComponent GetGamePlayer(Scene scene)
		{
			Scene currentScene = scene.ClientScene().CurrentScene();
			GamePlayComponent gamePlayComponent = currentScene.GetComponent<GamePlayComponent>();
			return gamePlayComponent;
		}
		
		public static async ETTask SendPutHome(Scene scene, string unitCfgId, float3 pos)
		{
			M2C_PutHome _M2C_PutHomeAndMonsterCall = await scene.ClientScene().GetComponent<SessionComponent>().Session.Call(new C2M_PutHome()
			{
				UnitCfgId = unitCfgId,
				Position = pos,
			}) as M2C_PutHome;
		}
		
		public static async ETTask SendPutMonsterCall(Scene scene, string unitCfgId, float3 pos)
		{
			M2C_PutMonsterCall _M2C_PutHomeAndMonsterCall = await scene.ClientScene().GetComponent<SessionComponent>().Session.Call(new C2M_PutMonsterCall()
			{
				UnitCfgId = unitCfgId,
				Position = pos,
			}) as M2C_PutMonsterCall;
		}
		
		public static async ETTask SendBuyPlayerTower(Scene scene, int index)
		{
			M2C_BuyPlayerTower _M2C_BuyPlayerTower = await scene.ClientScene().GetComponent<SessionComponent>().Session.Call(new C2M_BuyPlayerTower()
			{
				Index = index,
			}) as M2C_BuyPlayerTower;
		}
		
		public static async ETTask SendRefreshBuyPlayerTower(Scene scene)
		{
			M2C_RefreshBuyPlayerTower _M2C_RefreshBuyPlayerTower = await scene.ClientScene().GetComponent<SessionComponent>().Session.Call(new C2M_RefreshBuyPlayerTower()) as
					M2C_RefreshBuyPlayerTower;
		}
		
		public static async ETTask SendCallOwnTower(Scene scene, string towerUnitCfgId, float3 position)
		{
			C2M_CallOwnTower _C2M_CallOwnTower = new ()
			{
				TowerUnitCfgId = towerUnitCfgId,
				Position = position,
			};
			M2C_CallOwnTower _M2C_CallOwnTower = await scene.ClientScene().GetComponent<SessionComponent>().Session.Call(_C2M_CallOwnTower) as M2C_CallOwnTower;
		}
		
		public static async ETTask SendCallTower(Scene scene, string towerUnitCfgId, float3 position)
		{
			C2M_CallTower _C2M_CallTower = new ()
			{
				TowerUnitCfgId = towerUnitCfgId,
				Position = position,
			};
			M2C_CallTower _M2C_CallTower = await scene.ClientScene().GetComponent<SessionComponent>().Session.Call(_C2M_CallTower) as M2C_CallTower;
		}
		
		public static async ETTask SendCallTank(Scene scene, string tankUnitCfgId, float3 position)
		{
			C2M_CallTank _C2M_CallTank = new ()
			{
				TankUnitCfgId = tankUnitCfgId,
				Position = position,
			};
			M2C_CallTank _M2C_CallTank = await scene.ClientScene().GetComponent<SessionComponent>().Session.Call(_C2M_CallTank) as M2C_CallTank;
		}
	}
}
