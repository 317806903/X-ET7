using System.Collections.Generic;
using System.Linq;

namespace ET.Client
{
    [FriendOf(typeof(GamePlayComponent))]
    public static class GamePlayComponentSystem
	{
		[ObjectSystem]
		public class GamePlayComponentUpdateSystem : FixedUpdateSystem<GamePlayComponent>
		{
			protected override void FixedUpdate(GamePlayComponent self)
			{
				if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Current)
				{
					return;
				}

				self.DoUpdate();
			}
		}

		public static void DoUpdate(this GamePlayComponent self)
		{
			if (++self.curFrameSyncPos >= self.waitFrameSyncPos)
			{
				self.curFrameSyncPos = 0;

				self.SendGetNumericUnit().Coroutine();
			}
		}

		public static void RecordNeedGetNumericUnit(this GamePlayComponent self, Unit unit)
		{
			if (self.RecordSendGetNumericUnit == null)
			{
				self.RecordSendGetNumericUnit = new();
			}

			if (self.RecordSendGetNumericUnit.Contains(unit.Id))
			{
				return;
			}
			self.RecordSendGetNumericUnit.Add(unit.Id);
		}

		public static async ETTask SendGetNumericUnit(this GamePlayComponent self)
		{
			if (self.RecordSendGetNumericUnit == null || self.RecordSendGetNumericUnit.Count == 0)
			{
				return;
			}
			C2M_GetNumericUnit _C2M_GetNumericUnit = new ()
			{
				UnitIdList = self.RecordSendGetNumericUnit.ToList(),
				NumericKeyList = new List<int>(){NumericType.Speed},
			};
			self.RecordSendGetNumericUnit.Clear();
			M2C_GetNumericUnit _M2C_GetNumericUnit = await ET.Client.SessionHelper.GetSession(self.DomainScene()).Call(_C2M_GetNumericUnit) as M2C_GetNumericUnit;
			if (_M2C_GetNumericUnit.Error != ET.ErrorCode.ERR_Success)
			{
			}
		}

	}
}