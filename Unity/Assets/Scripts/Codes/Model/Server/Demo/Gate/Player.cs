namespace ET.Server
{
    [ChildOf(typeof(PlayerComponent))]
    public sealed class Player : Entity, IAwake<string>
    {
        public string Account { get; set; }
		
        public int Level { get; set; }
        public long UnitId { get; set; }
    }
}