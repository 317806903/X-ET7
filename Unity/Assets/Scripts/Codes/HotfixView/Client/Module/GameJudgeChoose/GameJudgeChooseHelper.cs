using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Client
{
    public static class GameJudgeChooseHelper
    {
	    public static async ETTask ShowGameJudgeChoose(Scene scene)
	    {
		    bool isNeed = await SendChkGameJudgeChooseAsync(scene);
		    if (isNeed)
		    {
			    await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgGameJudgeChoose>();
		    }
	    }

        public static async ETTask<bool> SendChkGameJudgeChooseAsync(Scene clientScene)
        {
	        G2C_ChkGameJudgeChoose _G2C_ChkGameJudgeChoose = await ET.Client.SessionHelper.GetSession(clientScene).Call(new C2G_ChkGameJudgeChoose()
		        {
		        }) as
		        G2C_ChkGameJudgeChoose;
	        if (_G2C_ChkGameJudgeChoose.Error != ET.ErrorCode.ERR_Success)
	        {
		        Log.Error($"SendChkGameJudgeChooseAsync Error==1 msg={_G2C_ChkGameJudgeChoose.Message}");
		        return false;
	        }
	        else
	        {
		        bool isNeed = _G2C_ChkGameJudgeChoose.IsNeed == 1? true : false;
		        return isNeed;
	        }
        }

        public static async ETTask SendRecordGameJudgeChooseAsync(Scene clientScene, GameJudgeChooseType gameJudgeChooseType, string complainMsg)
        {
	        G2C_RecordGameJudgeChoose _G2C_RecordGameJudgeChoose = await ET.Client.SessionHelper.GetSession(clientScene).Call(new C2G_RecordGameJudgeChoose()
		        {
			        GameJudgeChooseType = (int)gameJudgeChooseType,
			        ComplainMsg = complainMsg,
		        }) as
		        G2C_RecordGameJudgeChoose;
	        if (_G2C_RecordGameJudgeChoose.Error != ET.ErrorCode.ERR_Success)
	        {
		        Log.Error($"SendRecordGameJudgeChooseAsync Error==1 msg={_G2C_RecordGameJudgeChoose.Message}");
	        }
	        else
	        {
		        UIManagerHelper.ShowTip(clientScene, "Success");
	        }
        }

    }
}