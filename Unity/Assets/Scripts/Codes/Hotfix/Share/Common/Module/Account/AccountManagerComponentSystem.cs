using System.Collections.Generic;
using System.Linq;

namespace ET
{
    [FriendOf(typeof(AccountManagerComponent))]
    public static class AccountManagerComponentSystem
    {
        [ObjectSystem]
        public class AccountManagerComponentAwakeSystem : AwakeSystem<AccountManagerComponent>
        {
            protected override void Awake(AccountManagerComponent self)
            {
            }
        }

        public static AccountComponent GetAccountComponent(this AccountManagerComponent self, string accountId)
        {
            AccountComponent accountComponent;
            if (self.AccountId2AccountComponents.TryGetValue(accountId, out long accountComponentId) == false)
            {
                return null;
            }
            else
            {
                accountComponent = self.GetChild<AccountComponent>(accountComponentId);
            }
            return accountComponent;
        }

        public static long GetPlayerId(this AccountManagerComponent self, string accountId)
        {
            AccountComponent accountComponent = self.GetAccountComponent(accountId);
            if (accountComponent == null)
            {
                return (long)PlayerId.PlayerNone;
            }
            return accountComponent.GetPlayerId();
        }
    }
}