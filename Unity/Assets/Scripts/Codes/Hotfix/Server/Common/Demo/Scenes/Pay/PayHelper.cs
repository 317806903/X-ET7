using System;
using ET.AbilityConfig;
using System.Collections.Generic;
using System.Text;
using Unity.Mathematics;

namespace ET.Server
{
    public static class PayHelper
    {
	    public static PayManagerComponent _GetPayManager(Scene scene)
	    {
		    PayManagerComponent payManagerComponent = scene.GetComponent<PayManagerComponent>();
		    if (payManagerComponent == null)
		    {
			    payManagerComponent = scene.AddComponent<PayManagerComponent>();
		    }
		    return payManagerComponent;
	    }

	    public static async ETTask<(bool bRet, string msg, PayComponent payComponent)> GetNewPayOrder(Scene scene, long playerId, int coinNum)
	    {
		    PayManagerComponent payManagerComponent = _GetPayManager(scene);
		    PayComponent payComponent = payManagerComponent.GetNewPayOrder(playerId, coinNum);

		    string tipMsg = string.Format("{0} money buy {1} coins", payComponent.moneyValue * 0.01f, payComponent.coinNum);
		    tipMsg = Uri.EscapeDataString(tipMsg);
		    string url = string.Format(GlobalSettingCfgCategory.Instance.GameModeArcadeCoin2WXUrl, payComponent.orderId, payComponent.moneyValue, tipMsg);

		    Log.Debug($"url[{url}]");
		    string getWXUrl = await HttpClientHelper.Get(url);
		    Log.Debug($"getWXUrl[{getWXUrl}]");

		    string msg;
		    string[] info = getWXUrl.Split("|");
		    if (bool.TryParse(info[0], out bool result) == false)
		    {
			    msg = $"ET.Server.PayHelper.GetNewPayOrder [{getWXUrl}] error";
			    Log.Error(msg);
			    return (false, msg, null);
		    }

		    if (result == false)
		    {
			    msg = $"ET.Server.PayHelper.GetNewPayOrder [{getWXUrl}] result == false";
			    Log.Error(msg);
			    return (false, msg, null);
		    }
		    //"true|d2VpeGluOi8vd3hwYXkvYml6cGF5dXJsP3ByPWdRYmRlWWx6MQ=="
		    string sWXUrl = info[1];
		    if (sWXUrl.EndsWith("==") == false)
		    {
			    sWXUrl = sWXUrl + "==";
		    }
		    byte[] outputb = Convert.FromBase64String(sWXUrl);
		    sWXUrl = Encoding.Default.GetString(outputb);
		    bool bRet;
		    (bRet, msg) = await payManagerComponent.GetWXUrlCallBack(payComponent.orderId, sWXUrl);
		    return (bRet, msg, payComponent);
	    }

	    public static async ETTask<(bool bRet, string msg, PayComponent payComponent)> GetNewPayOrder_Editor(Scene scene, long playerId, int coinNum)
	    {
		    PayManagerComponent payManagerComponent = _GetPayManager(scene);
		    PayComponent payComponent = payManagerComponent.GetNewPayOrder(playerId, coinNum);

		    string msg;
		    string sWXUrl = "d2VpeGluOi8vd3hwYXkvYml6cGF5dXJsP3ByPWdRYmRlWWx6MQ==";
		    bool bRet;
		    (bRet, msg) = await payManagerComponent.GetWXUrlCallBack(payComponent.orderId, sWXUrl);
		    return (bRet, msg, payComponent);
	    }

	    public static async ETTask<(bool bRet, string msg)> ConfirmCallBack(Scene scene, long orderId, bool paySucessed, string payResultMsg)
	    {
		    PayManagerComponent payManagerComponent = _GetPayManager(scene);
		    return await payManagerComponent.ConfirmCallBack(orderId, paySucessed, payResultMsg);
	    }

	    public static int GetMoneyValue(int coinNum)
	    {
		    return (int)(GlobalSettingCfgCategory.Instance.GameModeArcadeCoin2Money * coinNum * 100);
	    }

    }
}