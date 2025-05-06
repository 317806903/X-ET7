using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ET.Server
{
    [MessageHandler(SceneType.Realm)]
    public class C2R_LoginWithAuthHandler: AMRpcHandler<C2R_LoginWithAuth, R2C_LoginWithAuth>
    {
        protected override async ETTask Run(Session session, C2R_LoginWithAuth request, R2C_LoginWithAuth response)
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
                        string decode = parts[1];
                        decode = decode.Replace('-', '+').Replace('_', '/');
                        var padLength = 4 - decode.Length % 4;
                        if (padLength < 4)
                        {
                            decode += new string('=', padLength);
                        }
                        var bytes = Convert.FromBase64String(decode);
                        var result = Encoding.UTF8.GetString(bytes);
                        Dictionary<string, object> authInfo = ET.JsonHelper.FromJson<Dictionary<string, object>>(result);
                        if (authInfo["sub"].ToString() != request.Account.Replace("GooglePlayer_", string.Empty))
                        {
                            response.Error = ErrorCode.ERR_LogicError;
                            response.Message = "auth fail";
                            return;
                        }
                    }else{
                        response.Error = ErrorCode.ERR_LogicError;
                        response.Message = $"auth error: token error";
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
                        if (authInfo["sub"].ToString() != request.Account.Replace("ApplePlayer_", string.Empty))
                        {
                            response.Error = ErrorCode.ERR_LogicError;
                            response.Message = "auth fail";
                            return;
                        }
                    }else{
                        response.Error = ErrorCode.ERR_LogicError;
                        response.Message = $"auth error: identityToken error";
                        return;
                    }
                }
            }

            StartSceneConfig config = session.DomainScene().GetComponent<RealmGetGatePlayerCountComponent>().GetMinCountGate();
            if (config == null)
            {
                config = StartSceneConfigCategory.Instance.GetRandomGate(session.DomainZone());;
            }
            // 向gate请求一个key,客户端可以拿着这个key连接gate
            G2R_GetLoginKey g2RGetLoginKey = (G2R_GetLoginKey)await ActorMessageSenderComponent.Instance.Call(
                config.InstanceId, new R2G_GetLoginKey()
                {
                    Account = request.Account,
                    Password = request.Password,
                    LoginType = request.LoginType,
                });

            if (g2RGetLoginKey.Error == ErrorCode.ERR_LogicError)
            {
                response.Error = ErrorCode.ERR_LogicError;
            }
            else
            {
                response.Address = config.InnerIPOutPort.ToString();
                response.Key = g2RGetLoginKey.Key;
                response.GateId = g2RGetLoginKey.GateId;
                response.IsFirstLogin = g2RGetLoginKey.IsFirstLogin;

                session.DomainScene().GetComponent<RealmGetGatePlayerCountComponent>().AddGatePlayerCount(config);
            }
        }
    }
}