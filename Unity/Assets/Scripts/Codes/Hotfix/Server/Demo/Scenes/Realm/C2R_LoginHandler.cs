using System;
using System.Net;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ET.Server
{
    [MessageHandler(SceneType.Realm)]
    public class C2R_LoginHandler: AMRpcHandler<C2R_Login, R2C_Login>
    {
        protected bool ChkIp(Session session)
        {
            bool bRet = true;
            if(bRet)
            {
                return true;
            }
            else
            {
                var clientAddress = session.RemoteAddress;

                // var rangeA = NetTools.IPAddressRange.Parse("192.168.0.0/255.255.255.0");
                // rangeA.Contains(IPAddress.Parse("192.168.0.34")); // is True.
                // rangeA.Contains(IPAddress.Parse("192.168.10.1")); // is False.
                // rangeA.ToCidrString(); // is 192.168.0.0/24

                bool isLocalHost = clientAddress.Address.ToString() == "127.0.0.1";
                if (isLocalHost)
                {
                    return true;
                }

                var rangeA = NetTools.IPAddressRange.Parse("192.168.0.10 - 192.168.100.20");
                return rangeA.Contains(clientAddress.Address);
            }
        }

        protected override async ETTask Run(Session session, C2R_Login request, R2C_Login response)
        {
            if (ChkIp(session) == false)
            {
                response.Error = ErrorCode.ERR_LogicError;
                response.Message = "zpb test111";
                return;
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
                    LoginIP = session.RemoteAddress.ToString(),
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