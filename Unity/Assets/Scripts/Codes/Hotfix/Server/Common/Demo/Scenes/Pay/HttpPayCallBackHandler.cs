using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ET.Server
{
    [HttpHandler(SceneType.Pay, "/PayCallBack")]
    public class HttpPayCallBackHandler : IHttpHandler
    {
        public async ETTask Handle(Scene scene, HttpListenerContext context)
        {
            Dictionary<string, string> paramDic = UrlHelper.GetQueryDictionary(context.Request.Url.AbsoluteUri);
            paramDic.TryGetValue("orderId", out string orderIdStr);
            paramDic.TryGetValue("orderSuccess", out string orderSuccessStr);
            paramDic.TryGetValue("orderMsg", out string orderMsgStr);

            Dictionary<string, string> resultDic = new();

            bool bRet = long.TryParse(orderIdStr, out long orderId);
            if (bRet == false)
            {
                resultDic["bRet"] = "false";
                resultDic["msg"] = $"OrderId[{orderIdStr}] Unable to convert to Long";
                HttpHelper.Response(context, ET.UrlHelper.GetUrlQueryString(resultDic));
                return;
            }
            bRet = bool.TryParse(orderSuccessStr, out bool orderSuccess);
            if (bRet == false)
            {
                resultDic["bRet"] = "false";
                resultDic["msg"] = $"OrderSuccess[{orderSuccessStr}] Unable to convert to Bool";
                HttpHelper.Response(context, ET.UrlHelper.GetUrlQueryString(resultDic));
                return;
            }

            string payResultMsg = orderMsgStr;
            (bool bRetConfirm, string msgConfirm) = await ET.Server.PayHelper.ConfirmCallBack(scene, orderId, orderSuccess, payResultMsg);
            resultDic["bRet"] = bRetConfirm.ToString();
            resultDic["msg"] = msgConfirm;

            HttpHelper.Response(context, ET.UrlHelper.GetUrlQueryString(resultDic));
            await ETTask.CompletedTask;
        }
    }
}
