using System;
using ET.AbilityConfig;
using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Client
{
    public static class MailHelper
    {
	    public static string GetMailLeftTime(long timeStamp)
	    {
		    var tmp = TimeHelper.ToDateTime(timeStamp) - TimeHelper.DateTimeNow();
		    if (tmp.Days > 0)
		    {
			    string msgTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_MailRemaining_Time_Days", tmp.Days);
			    return msgTxt;
		    }
		    if (tmp.Hours > 0)
		    {
			    string msgTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_MailRemaining_Time_Hours", tmp.Hours);
			    return msgTxt;
		    }
		    if (tmp.Minutes > 0)
		    {
			    string msgTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_MailRemaining_Time_Minutes", tmp.Minutes);
			    return msgTxt;
		    }
		    else
		    {
			    string msgTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_MailRemaining_Time_Minutes", 1);
			    return msgTxt;
		    }
	    }

	    public static async ETTask<bool> DealMyMail(Scene scene, long mailId, DealMailType dealMailType)
	    {
		    try
		    {
			    scene = scene.ClientScene();

			    G2C_DealMyMail _G2C_DealMyMail = await ET.Client.SessionHelper.GetSession(scene).Call(new C2G_DealMyMail()
			    {
				    MailId = mailId,
				    DealMailType = (int)dealMailType,
			    }) as G2C_DealMyMail;

			    if (_G2C_DealMyMail.Error != ET.ErrorCode.ERR_Success)
			    {
				    Log.Error($"DealMyMail Error==1 msg={_G2C_DealMyMail.Message}");
				    return false;
			    }
			    else
			    {
				    Log.Info($"DealMyMail Success");

				    if (dealMailType == DealMailType.ReadOnly)
				    {
					    EventSystem.Instance.Publish(scene, new EventType.NoticeEventLogging()
					    {
						    eventName = "MailChecked",
						    properties = new()
						    {
							    {"mailId", mailId},
						    }
					    });
				    }
				    else
				    {
					    EventSystem.Instance.Publish(scene, new EventType.NoticeEventLogging()
					    {
						    eventName = "MailCollected",
						    properties = new()
						    {
							    {"mailId", mailId},
						    }
					    });
				    }

				    return true;
			    }
		    }
		    catch (Exception e)
		    {
			    Log.Error(e);
			    return false;
		    }
	    }

    }
}