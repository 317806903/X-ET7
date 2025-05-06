using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ET.Server
{
    [HttpHandler(SceneType.ActionFromHttp, "/ActionFromHttpCallBack")]
    public class HttpActionFromHttpCallBackHandler : IHttpHandler
    {
        public async ETTask Handle(Scene scene, HttpListenerContext context)
        {
            Dictionary<string, string> resultDic = new();

            Dictionary<string, string> paramDic = UrlHelper.GetQueryDictionary(context.Request.Url.AbsoluteUri);
            paramDic.TryGetValue("actionFromHttpStatus", out string actionFromHttpStatusStr);
            if (string.IsNullOrEmpty(actionFromHttpStatusStr) == false)
            {
                bool result = Enum.TryParse<ActionFromHttpStatus>(actionFromHttpStatusStr, out var actionFromHttpStatus);
                if (result)
                {
                    (bool bRet, string msg) = await ET.Server.ActionFromHttpHelper.Run(scene, actionFromHttpStatus, paramDic);
                    resultDic["bRet"] = bRet.ToString();
                    resultDic["msg"] = msg;
                }
                else
                {
                    resultDic["bRet"] = false.ToString();
                    resultDic["msg"] = "actionFromHttpStatus is not ActionFromHttpStatus";
                }
            }
            else
            {
                resultDic["bRet"] = false.ToString();
                resultDic["msg"] = "actionFromHttpStatus==null";
            }

            HttpHelper.Response(context, ET.UrlHelper.GetUrlQueryString(resultDic));
            await ETTask.CompletedTask;
        }
    }
}
