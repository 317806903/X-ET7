namespace ET
{
	/// <summary>
	/// 同步属性
	/// </summary>
	[NumericWatcher(SceneType.Map, NumericType.Hp)]
	public class NumericWatcher_Hp_Sync : INumericWatcher
	{
		public void Run(Unit unit, EventType.NumbericChange args)
		{
			//Log.Debug($"==NumericWatcher_Hp_Sync {args.Unit.Id} {args.NumericType} {args.Old} {args.New}");
			Unit unitChg = args.Unit;
            
			Ability.UnitHelper.AddSyncNumericUnit(unitChg);
		}
	}
}
