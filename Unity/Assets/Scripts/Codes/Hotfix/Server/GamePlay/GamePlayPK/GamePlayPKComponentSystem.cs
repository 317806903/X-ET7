using System.Collections.Generic;
using ET.Ability;
using Unity.Mathematics;

namespace ET.Server
{
    [FriendOf(typeof (GamePlayPkComponent))]
    [FriendOf(typeof (Unit))]
    public static class GamePlayPKComponentSystem
    {
        public static async ETTask GameBeginWhenServer(this GamePlayPkComponent self)
        {

            await ETTask.CompletedTask;
        }

        public static async ETTask GameEndWhenServer(this GamePlayPkComponent self)
        {

            await ETTask.CompletedTask;
        }

        public static async ETTask GameRecoverWhenServer(this GamePlayPkComponent self, long playerId)
        {

            await ETTask.CompletedTask;
        }

		public static List<Unit> GetTowerListWhenStackedOnTop(this GamePlayPkComponent self, Unit curUnit)
		{
			float3 curUnitPos = curUnit.Position;
			float curUnitHeight = Ability.UnitHelper.GetBodyHeight(curUnit);
			float curUnitRadius = Ability.UnitHelper.GetBodyRadius(curUnit);
			long ignoreTowerUnitId = curUnit.Id;

			return self.GetTowerListWhenStackedOnTop(curUnitPos, curUnitHeight, curUnitRadius, ignoreTowerUnitId);
		}

		public static List<Unit> GetTowerListWhenStackedOnTop(this GamePlayPkComponent self, float3 curUnitPos, float curUnitHeight, float curUnitRadius, long ignoreTowerUnitId)
		{
			List<Unit> list = ListComponent<Unit>.Create();
			do
			{
				Unit unit = self.GetTowerWhenStackedOnTop(curUnitPos, curUnitHeight, curUnitRadius, ignoreTowerUnitId);
				if (unit != null)
				{
					list.Add(unit);
					curUnitPos = unit.Position;
					curUnitHeight = Ability.UnitHelper.GetBodyHeight(unit);
					curUnitRadius = Ability.UnitHelper.GetBodyRadius(unit);
					ignoreTowerUnitId = unit.Id;
				}
				else
				{
					break;
				}
			}
			while (true);

			return list;
		}

		public static Unit GetTowerWhenStackedOnTop(this GamePlayPkComponent self, float3 curUnitPos, float curUnitHeight, float curUnitRadius, long ignoreTowerUnitId)
		{
			UnitComponent unitComponent = Ability.UnitHelper.GetUnitComponent(self.DomainScene());
			HashSet<Unit> list = unitComponent.GetRecordList(UnitType.ActorUnit);
			if (list == null)
			{
				return null;
			}
			foreach (Unit unit in list)
			{
				if (Ability.UnitHelper.ChkUnitAlive(unit, false) == false)
				{
					continue;
				}
				if (ignoreTowerUnitId != -1 && ignoreTowerUnitId == unit.Id)
				{
					continue;
				}
				bool isNear = Ability.UnitHelper.ChkIsStackedOnTop(curUnitPos, curUnitHeight, unit, curUnitRadius);
				if (isNear)
				{
					return unit;
				}
			}

			return null;
		}

		public static bool ChkMovePlayerTowerNeedDownTower(this GamePlayPkComponent self, long towerUnitId, float3 position)
		{
			Unit curUnit = Ability.UnitHelper.GetUnit(self.DomainScene(), towerUnitId);
			float3 curUnitPos = curUnit.Position;
			float curUnitHeight = Ability.UnitHelper.GetBodyHeight(curUnit);
			float curUnitRadius = Ability.UnitHelper.GetBodyRadius(curUnit);
			long ignoreTowerUnitId = curUnit.Id;

			Unit unit = self.GetTowerWhenStackedOnTop(curUnitPos, curUnitHeight, curUnitRadius, ignoreTowerUnitId);
			if (unit == null)
			{
				return false;
			}
			curUnitPos = position;
			Unit unitNew = self.GetTowerWhenStackedOnTop(curUnitPos, curUnitHeight, curUnitRadius, ignoreTowerUnitId);
			if (unitNew != null && unitNew == unit)
			{
				return false;
			}

			return true;
		}

    }
}