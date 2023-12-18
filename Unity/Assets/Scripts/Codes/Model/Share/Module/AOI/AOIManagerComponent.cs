namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class AOIManagerComponent: Entity, IAwake
    {
        //public const int CellSize = 10 * 1000;
        public const int CellSize = 50 * 1000;
    }
}