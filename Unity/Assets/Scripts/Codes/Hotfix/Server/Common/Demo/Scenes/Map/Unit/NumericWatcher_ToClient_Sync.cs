namespace ET
{
	/// <summary>
	/// 同步属性
	/// </summary>
	[NumericWatcher(SceneType.Map, -1)]
	public class NumericWatcher_ToClient_Sync : INumericWatcher
	{
		public void Run(Unit unit, EventType.NumbericChange args)
		{
			Unit unitChg = args.Unit;

			Ability.UnitHelper.AddSyncData_UnitNumericInfoByKey(unitChg, args.NumericType);
		}
	}
}
