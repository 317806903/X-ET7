namespace ET
{
    [ChildOf(typeof(PlayerComponent))]
    public sealed class Player : Entity, IAwake
    {
        public LoginType LoginType { get; set; }
        public string AccountId { get; set; }
        public string AccountName { get; set; }
        public string AccountPwd { get; set; }

        public int Level { get; set; }
        public long UnitId { get; set; }
    }
}