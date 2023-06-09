using ET.Client;

namespace ET
{
	/// <summary>
	/// 客户端监视hp数值变化，改变血条值
	/// </summary>
	[NumericWatcher(SceneType.Current, NumericType.Hp)]
	public class NumericWatcher_Hp_ShowUI : INumericWatcher
	{
		public void Run(Unit unit, EventType.NumbericChange args)
		{
			Log.Debug($"==NumericWatcher_Hp_ShowUI {args.Unit.Id} {args.NumericType} {args.Old} {args.New}");
			unit.GetComponent<HealthBarComponent>().UpdateHealth();
		}
	}
}
