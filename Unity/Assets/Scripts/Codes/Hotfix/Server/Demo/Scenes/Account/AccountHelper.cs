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

        public static async ETTask<(long, bool)> AccountLogin(Scene scene, string accountId, string password, ET.LoginType loginType)
        {
	        AccountManagerComponent accountManagerComponent = GetAccountManager(scene);
	        return await accountManagerComponent.AccountLogin(accountId, password, loginType);
        }

        public static AccountComponent GetAccountComponent(Scene scene, string accountId)
        {
	        AccountManagerComponent accountManagerComponent = GetAccountManager(scene);
	        AccountComponent accountComponent = accountManagerComponent.GetAccountComponent(accountId);
	        return accountComponent;
        }
    }
}