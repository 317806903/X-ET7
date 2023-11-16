using System.Collections.Generic;
using System.Linq;

namespace ET.Server
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

        public static async ETTask<long> AccountLogin(this AccountManagerComponent self, string accountId, string password, ET.LoginType loginType)
        {
            if (DBManagerComponent.Instance.NeedDB == false)
            {
                return await self.AccountLoginNoDB(accountId, password, loginType);
            }
            else
            {
                return await self.AccountLoginWithDB(accountId, password, loginType);
            }
        }

        public static async ETTask<long> AccountLoginWithDB(this AccountManagerComponent self, string accountId, string password, ET.LoginType loginType)
        {
            DBComponent dbComponent = DBManagerComponent.Instance.GetZoneDB(self.DomainZone());
            AccountComponent accountComponent;
            if (self.AccountId2AccountComponents.TryGetValue(accountId, out long accountComponentId) == false)
            {
                List<AccountComponent> accountComponents =
                    await dbComponent.Query<AccountComponent>((accountComponent1) => accountComponent1.accountId == accountId);
                if (accountComponents.Count > 0)
                {
                    accountComponent = accountComponents[0];
                    self.AddChild(accountComponent);
                }
                else
                {
                    accountComponent = self.AddChild<AccountComponent>();
                    accountComponent.Init(accountId, password, loginType);
                }
                self.AccountId2AccountComponents[accountId] = accountComponent.Id;
            }
            else
            {
                accountComponent = self.GetChild<AccountComponent>(accountComponentId);
                if (accountComponent.password != password)
                {
                    return 0;
                }
            }

            accountComponent.loginType = loginType;
            accountComponent.UpdateLoginTime();
            await dbComponent.SaveNotWait(accountComponent);

            return accountComponent.GetPlayerId();
        }

        public static async ETTask<long> AccountLoginNoDB(this AccountManagerComponent self, string accountId, string password, ET.LoginType loginType)
        {
            await ETTask.CompletedTask;
            AccountComponent accountComponent;
            if (self.AccountId2AccountComponents.TryGetValue(accountId, out long accountComponentId) == false)
            {
                accountComponent = self.AddChild<AccountComponent>();
                accountComponent.Init(accountId, password, loginType);
                self.AccountId2AccountComponents[accountId] = accountComponent.Id;
            }
            else
            {
                accountComponent = self.GetChild<AccountComponent>(accountComponentId);
                if (accountComponent.password != password)
                {
                    return 0;
                }
            }

            accountComponent.loginType = loginType;
            accountComponent.UpdateLoginTime();

            return accountComponent.GetPlayerId();
        }
    }
}