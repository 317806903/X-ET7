using System;
using ET.AbilityConfig;
using System.Collections.Generic;
using System.Text;
using Unity.Mathematics;

namespace ET.Client
{
    public static class PayHelper
    {
	    public static float GetMoneyValue(int coinNum)
	    {
		    return GlobalSettingCfgCategory.Instance.GameModeArcadeCoin2Money * coinNum;
	    }

	    public static async ETTask<(bool bRet, string msg, PayComponent payComponent)> GetNewPayOrder(Scene clientScene, int coinNum)
	    {
		    string msg;
		    if (coinNum <= 0)
		    {
			    msg = "coinNum <= 0";
			    return (false, msg, null);
		    }
		    G2C_GetArcadeCoinQrCode _G2C_GetArcadeCoinQrCode = await ET.Client.SessionHelper.GetSession(clientScene).Call(new C2G_GetArcadeCoinQrCode()
			    {
				    ArcadeCoinNum = coinNum,
			    }) as
			    G2C_GetArcadeCoinQrCode;
		    if (_G2C_GetArcadeCoinQrCode.Error != ET.ErrorCode.ERR_Success)
		    {
			    Log.Error($"ET.Client.PayHelper.GetNewPayOrder Error==1 msg={_G2C_GetArcadeCoinQrCode.Message}");
			    return (false, _G2C_GetArcadeCoinQrCode.Message, null);
		    }
		    else
		    {
			    PayComponent payComponent = MongoHelper.Deserialize<PayComponent>(_G2C_GetArcadeCoinQrCode.PayComponentBytes);
			    return (true, "", payComponent);
		    }
	    }

    }
}