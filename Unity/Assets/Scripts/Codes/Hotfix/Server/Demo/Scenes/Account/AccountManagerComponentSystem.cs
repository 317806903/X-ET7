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

        public static async ETTask<(long, bool)> AccountLogin(this AccountManagerComponent self, string accountId, string password, ET.LoginType loginType, string loginIP)
        {
            if (DBManagerComponent.Instance.NeedDB == false)
            {
                return await self.AccountLoginNoDB(accountId, password, loginType, loginIP);
            }
            else
            {
                return await self.AccountLoginWithDB(accountId, password, loginType, loginIP);
            }
        }

        public static async ETTask<(long, bool)> AccountLoginWithDB(this AccountManagerComponent self, string accountId, string password, ET.LoginType loginType, string loginIP)
        {
            bool isFirstLogin = false;
            DBComponent dbComponent = DBManagerComponent.Instance.GetZoneDB(self.DomainZone());
            AccountComponent accountComponent;
            if (self.AccountId2AccountComponents.TryGetValue(accountId, out long accountComponentId) == false)
            {
                List<AccountComponent> accountComponents =
                    await dbComponent.Query<AccountComponent>((accountComponent1) => (accountComponent1.accountId == accountId || accountComponent1.bindAccountId == accountId));
                if (accountComponents.Count > 0)
                {
                    accountComponent = accountComponents[0];
                    self.AddChild(accountComponent);
                }
                else
                {
                    accountComponent = self.AddChild<AccountComponent>();
                    accountComponent.Init(accountId, password, loginType, loginIP);
                    isFirstLogin = true;
                    accountComponent.createIP = loginIP;
                }
                self.AccountId2AccountComponents[accountId] = accountComponent.Id;
            }
            else
            {
                accountComponent = self.GetChild<AccountComponent>(accountComponentId);
                if (accountComponent.password != password)
                {
                    return (0, isFirstLogin);
                }
            }

            accountComponent.loginType = loginType;
            accountComponent.loginIP = loginIP;
            accountComponent.UpdateLoginTime();
            await dbComponent.SaveNotWait(accountComponent);

            return (accountComponent.GetPlayerId(), isFirstLogin);
        }

        public static async ETTask<(long, bool)> AccountLoginNoDB(this AccountManagerComponent self, string accountId, string password, ET.LoginType loginType, string loginIP)
        {
            await ETTask.CompletedTask;
            bool isFirstLogin = false;
            AccountComponent accountComponent;
            if (self.AccountId2AccountComponents.TryGetValue(accountId, out long accountComponentId) == false)
            {
                accountComponent = self.AddChild<AccountComponent>();
                accountComponent.Init(accountId, password, loginType, loginIP);
                self.AccountId2AccountComponents[accountId] = accountComponent.Id;
                isFirstLogin = true;
                accountComponent.createIP = loginIP;
            }
            else
            {
                accountComponent = self.GetChild<AccountComponent>(accountComponentId);
                if (accountComponent.password != password)
                {
                    return (0, isFirstLogin);
                }
            }

            accountComponent.loginType = loginType;
            accountComponent.loginIP = loginIP;
            accountComponent.UpdateLoginTime();

            return (accountComponent.GetPlayerId(), isFirstLogin);
        }

        public static async ETTask<bool> AccountBind(this AccountManagerComponent self, string accountId, string bindAccountId, ET.LoginType loginType)
        {
            if (DBManagerComponent.Instance.NeedDB == false)
            {
                return await self.AccountBindNoDB(accountId, bindAccountId, loginType);
            }
            else
            {
                return await self.AccountBindWithDB(accountId, bindAccountId, loginType);
            }
        }

        public static async ETTask<bool> AccountBindWithDB(this AccountManagerComponent self, string accountId, string bindAccountId, ET.LoginType loginType)
        {
            DBComponent dbComponent = DBManagerComponent.Instance.GetZoneDB(self.DomainZone());
            AccountComponent accountComponent;
            List<AccountComponent> accountComponents =
                await dbComponent.Query<AccountComponent>((accountComponent1) => (accountComponent1.accountId == bindAccountId || accountComponent1.bindAccountId == bindAccountId));
            if (accountComponents.Count > 0)
            {
                return false;
            }

            if (self.AccountId2AccountComponents.TryGetValue(accountId, out long accountComponentId) == false)
            {
                return false;
            }
            else
            {
                accountComponent = self.GetChild<AccountComponent>(accountComponentId);
                accountComponent.bindAccountId = bindAccountId;
                self.AccountId2AccountComponents[bindAccountId] = accountComponent.Id;
            }

            accountComponent.loginType = loginType;
            accountComponent.UpdateLoginTime();
            await dbComponent.SaveNotWait(accountComponent);

            return true;
        }

        public static async ETTask<bool> AccountBindNoDB(this AccountManagerComponent self, string accountId, string bindAccountId, ET.LoginType loginType)
        {
            await ETTask.CompletedTask;
            AccountComponent accountComponent;
            if (self.AccountId2AccountComponents.TryGetValue(bindAccountId, out long bindAccountComponentId) == false)
            {
                if (self.AccountId2AccountComponents.TryGetValue(accountId, out long accountComponentId) == false)
                {
                    return false;
                }
                else
                {
                    accountComponent = self.GetChild<AccountComponent>(accountComponentId);
                    accountComponent.bindAccountId = bindAccountId;
                    self.AccountId2AccountComponents[bindAccountId] = accountComponent.Id;
                }
            }
            else
            {
                return false;
            }

            accountComponent.loginType = loginType;
            accountComponent.UpdateLoginTime();

            return true;
        }
    }
}