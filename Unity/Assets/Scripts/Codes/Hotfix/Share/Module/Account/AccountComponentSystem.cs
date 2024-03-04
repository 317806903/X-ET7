namespace ET
{
    [FriendOf(typeof(AccountComponent))]
    public static class AccountComponentSystem
    {
        [ObjectSystem]
        public class AccountComponentAwakeSystem : AwakeSystem<AccountComponent>
        {
            protected override void Awake(AccountComponent self)
            {
            }
        }

        public static void Init(this AccountComponent self, string accountId, string password, ET.LoginType loginType, string loginIP)
        {
            self.accountId = accountId;
            self.bindAccountId = accountId;
            self.password = password;
            self.accountType = loginType;
            self.loginType = loginType;
            self.loginIP = loginIP;
            self.playerId = IdGenerater.Instance.GenerateId();
            self.createTime = TimeHelper.ServerNow();
        }

        public static void UpdateLoginTime(this AccountComponent self)
        {
            self.loginTime = TimeHelper.ServerNow();
        }

        public static long GetPlayerId(this AccountComponent self)
        {
            return self.playerId;
        }
    }
}