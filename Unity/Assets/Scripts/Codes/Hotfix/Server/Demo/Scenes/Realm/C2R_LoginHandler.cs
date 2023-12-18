using System;
using System.Net;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ET.Server
{
    [MessageHandler(SceneType.Realm)]
    public class C2R_LoginHandler: AMRpcHandler<C2R_Login, R2C_Login>
    {
        protected override async ETTask Run(Session session, C2R_Login request, R2C_Login response)
        {
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