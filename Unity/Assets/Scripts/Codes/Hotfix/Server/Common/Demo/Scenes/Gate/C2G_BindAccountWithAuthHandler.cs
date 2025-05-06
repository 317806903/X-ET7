using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ET.Server
{
    [MessageHandler(SceneType.Gate)]
    public class C2G_BindAccountWithAuthHandler: AMRpcHandler<C2G_BindAccountWithAuth, G2C_BindAccountWithAuth>
    {
        protected override async ETTask Run(Session session, C2G_BindAccountWithAuth request, G2C_BindAccountWithAuth response)
        {
            //验证request.Token;
            if(request.LoginType == (int)LoginType.GoogleSDK || request.LoginType == (int)LoginType.AppleSDK)
            {
                if (request.LoginType == (int)LoginType.GoogleSDK)
                {
                    var token = request.Token;
                    var parts = token.Split('.');
                    if (parts.Length > 2)
                    {
                        var decode = parts[1];
                        decode = decode.Replace('-', '+').Replace('_', '/');
                        var padLength = 4 - decode.Length % 4;
                        if (padLength < 4)
                        {
                            decode += new string('=', padLength);
                        }
                        var bytes = Convert.FromBase64String(decode);
                        var result = Encoding.UTF8.GetString(bytes);
                        Dictionary<string, object> authInfo = ET.JsonHelper.FromJson<Dictionary<string, object>>(result);
                        if (authInfo["sub"].ToString() != request.BindAccount.Replace("GooglePlayer_", string.Empty))
                        {
                            response.Error = ErrorCode.ERR_LogicError;
                            response.Message = LocalizeComponent.Instance.GetTextValue("TextCode_Key_BindAccount_AuthFail");
                            response.IsBindSuccess = 0;
                            return;
                        }
                    }else{
                        response.Error = ErrorCode.ERR_LogicError;
                        response.Message = LocalizeComponent.Instance.GetTextValue("TextCode_Key_BindAccount_AuthFail");
                        response.IsBindSuccess = 0;
                        return;
                    }
                }
                else if (request.LoginType == (int)LoginType.AppleSDK)
                {
                    var token = request.Token;
                    var parts = token.Split('.');
                    if (parts.Length > 2)
                    {
                        var decode = parts[1];
                        decode = decode.Replace('-', '+').Replace('_', '/');
                        var padLength = 4 - decode.Length % 4;
                        if (padLength < 4)
                        {
                            decode += new string('=', padLength);
                        }
                        var bytes = Convert.FromBase64String(decode);
                        var result = Encoding.UTF8.GetString(bytes);
                        Dictionary<string, object> authInfo = ET.JsonHelper.FromJson<Dictionary<string, object>>(result);
                        if (authInfo["sub"].ToString() != request.BindAccount.Replace("ApplePlayer_", string.Empty))
                        {
                            response.Error = ErrorCode.ERR_LogicError;
                            response.Message = LocalizeComponent.Instance.GetTextValue("TextCode_Key_BindAccount_AuthFail");
                            response.IsBindSuccess = 0;
                            return;
                        }
                    }else{
                        response.Error = ErrorCode.ERR_LogicError;
                        response.Message = LocalizeComponent.Instance.GetTextValue("TextCode_Key_BindAccount_AuthFail");
                        response.IsBindSuccess = 0;
                        return;
                    }
                }
            }

            StartSceneConfig accountSceneConfig = StartSceneConfigCategory.Instance.GetAccountManager(session.DomainZone());
			A2G_BindAccountWithAuth _A2G_BindAccountWithAuth = (A2G_BindAccountWithAuth)await ActorMessageSenderComponent.Instance.Call(
				accountSceneConfig.InstanceId, new G2A_BindAccountWithAuth()
				{
					Account = request.Account,
					BindAccount = request.BindAccount,
					LoginType = request.LoginType,
				});
            response.IsBindSuccess = _A2G_BindAccountWithAuth.IsBindSuccess;
			if (_A2G_BindAccountWithAuth.Error == ErrorCode.ERR_LogicError)
			{
				response.Error = ErrorCode.ERR_LogicError;
                response.Message = LocalizeComponent.Instance.GetTextValue("TextCode_Key_BindAccount_BindFail");
				return;
			}

			await ETTask.CompletedTask;
        }
    }
}