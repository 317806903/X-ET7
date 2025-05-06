using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Server
{
    public static class AccountHelper
    {
	    public static AccountManagerComponent GetAccountManager(Scene scene)
	    {
		    AccountManagerComponent AccountManagerComponent = scene.GetComponent<AccountManagerComponent>();
		    return AccountManagerComponent;
	    }

        public static async ETTask<(long, bool)> AccountLogin(Scene scene, string accountId, string password, ET.LoginType loginType, string loginIP)
        {
	        AccountManagerComponent accountManagerComponent = GetAccountManager(scene);
	        return await accountManagerComponent.AccountLogin(accountId, password, loginType, loginIP);
        }

		public static async ETTask<bool> AccountBind(Scene scene, string accountId, string bindAccountId, ET.LoginType loginType)
        {
	        AccountManagerComponent accountManagerComponent = GetAccountManager(scene);
	        return await accountManagerComponent.AccountBind(accountId, bindAccountId, loginType);
        }

        public static AccountComponent GetAccountComponent(Scene scene, string accountId)
        {
	        AccountManagerComponent accountManagerComponent = GetAccountManager(scene);
	        AccountComponent accountComponent = accountManagerComponent.GetAccountComponent(accountId);
	        return accountComponent;
        }
    }
}