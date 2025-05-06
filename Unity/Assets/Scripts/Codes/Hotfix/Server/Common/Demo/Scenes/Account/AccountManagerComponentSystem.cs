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
            bool isFirstLogin = false;
            AccountComponent accountComponent;
            if (self.AccountId2AccountComponents.TryGetValue(accountId, out long accountComponentId) == false)
            {
                List<AccountComponent> accountComponents = await ET.Server.DBHelper._LoadDBList<AccountComponent>(self.DomainScene(), (accountComponent1) => (accountComponent1.accountId == accountId || accountComponent1.bindAccountId == accountId));
                if (self.IsDisposed)
                {
                    return (0, false);
                }

                if (self.AccountId2AccountComponents.TryGetValue(accountId, out accountComponentId))
                {
                    accountComponent = self.GetChild<AccountComponent>(accountComponentId);
                    if (accountComponent.password != password)
                    {
                        return (0, isFirstLogin);
                    }
                }
                else
                {
                    if (accountComponents != null && accountComponents.Count > 0)
                    {
                        if (accountComponents.Count > 1)
                        {
                            Log.Error($"dbComponent.Query<AccountComponent> accountComponents.Count[{accountComponents.Count}] > 1 when accountId == {accountId} || bindAccountId == {accountId}");
                        }
                        accountComponent = accountComponents[0];
                        AccountComponent accountComponentTmp = self.GetChild<AccountComponent>(accountComponent.Id);
                        if (accountComponentTmp == null)
                        {
                            self.AddChild(accountComponent);
                        }
                        else
                        {
                            accountComponent = accountComponentTmp;
                        }
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
            accountComponent.SetDataCacheAutoWrite();

            return (accountComponent.GetPlayerId(), isFirstLogin);
        }

        public static async ETTask<bool> AccountBind(this AccountManagerComponent self, string accountId, string bindAccountId, ET.LoginType loginType)
        {
            AccountComponent accountComponent;
            List<AccountComponent> accountComponents = await ET.Server.DBHelper._LoadDBList<AccountComponent>(self.DomainScene(), (accountComponent1) => (accountComponent1.accountId == bindAccountId || accountComponent1.bindAccountId == bindAccountId));
            if (accountComponents != null && accountComponents.Count > 0)
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
            accountComponent.SetDataCacheAutoWrite();

            return true;
        }
    }
}