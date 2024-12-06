using System.Collections.Generic;

namespace ET
{
    public class ActionFromHttpHandlerAttribute: BaseAttribute
    {
        public ActionFromHttpStatus actionFromHttpStatus { get; }
        public ActionFromHttpHandlerAttribute(ActionFromHttpStatus actionFromHttpStatus)
        {
            this.actionFromHttpStatus = actionFromHttpStatus;
        }
    }

    [ActionFromHttpHandler(ActionFromHttpStatus.SendMails)]
    public abstract class IActionFromHttpHandler
    {
        public abstract ETTask<(bool, string)> ChkParam(Scene scene, Dictionary<string, string> paramDic);
        public abstract ETTask<(bool bRet, string msg)> Run(Scene scene, Dictionary<string, string> paramDic);
    }
}