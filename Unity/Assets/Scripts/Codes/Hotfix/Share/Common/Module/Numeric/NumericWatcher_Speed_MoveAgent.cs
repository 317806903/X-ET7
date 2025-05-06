namespace ET
{
    /// <summary>
    /// 客户端监视 speed数值变化
    /// </summary>
    [NumericWatcher(SceneType.Map, NumericType.Speed)]
    public class NumericWatcher_Speed_MoveAgent : INumericWatcher
    {
        public void Run(Unit unit, EventType.NumbericChange args)
        {
            if (unit == null || unit.IsDisposed)
            {
                return;
            }
            PathfindingComponent pathfindingComponent = unit.GetComponent<PathfindingComponent>();
            if (pathfindingComponent == null || pathfindingComponent.IsDisposed)
            {
                return;
            }
            pathfindingComponent.ResetAgentSpeed();
        }
    }
}