namespace ET.Server
{
    [FriendOf(typeof(Player))]
    public static class PlayerSystem
    {
        [ObjectSystem]
        public class PlayerAwakeSystem : AwakeSystem<Player>
        {
            protected override void Awake(Player self)
            {
            }
        }

        public static void Init(this Player self, string accountId, string password, LoginType loginType)
        {
            self.Level = 1;
            self.AccountId = accountId;
            self.AccountPwd = password;
            self.LoginType = loginType;
        }
    }
}