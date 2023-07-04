using ET.Ability;
using System;
using System.Collections.Generic;
using Unity.Mathematics;

namespace ET
{
    [FriendOf(typeof(GamePlayComponent))]
    [FriendOf(typeof(Unit))]
    public static class GamePlayHelper
	{
		public static void BuyPlayerTower(GamePlayComponent gamePlayComponent, Unit unit, int index)
		{
			gamePlayComponent.GetComponent<PlayerOwnerTowersComponent>().BuyPlayerTower(unit.Id, index);
		}
		
		public static void RefreshBuyPlayerTower(GamePlayComponent gamePlayComponent, Unit unit)
		{
			gamePlayComponent.GetComponent<PlayerOwnerTowersComponent>().RefreshTowerPool(unit.Id);
		}
		
		public static void CallPlayerTower(GamePlayComponent gamePlayComponent, Unit unit, string towerId, float3 position)
		{
			gamePlayComponent.GetComponent<PlayerOwnerTowersComponent>().CallPlayerTower(unit.Id, towerId, position);
		}
		
		public static Unit GetHomeUnit(GamePlayComponent gamePlayComponent)
		{
			return gamePlayComponent.GetComponent<PutHomeComponent>().GetHomeUnit();
		}

	}
}