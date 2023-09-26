namespace ET
{
    /// <summary>
    /// 客户端监视 speed数值变化
    /// </summary>
    [NumericWatcher(SceneType.Map, NumericType.Speed)]
    public class NumericWatcher_Speed_Sync : INumericWatcher
    {
        public void Run(Unit unit, EventType.NumbericChange args)
        {
            Unit unitChg = args.Unit;

            Ability.UnitHelper.AddSyncNumericUnit(unitChg);
        }
    }
}