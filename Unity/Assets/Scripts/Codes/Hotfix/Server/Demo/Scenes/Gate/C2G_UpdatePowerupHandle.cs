using System;
using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Server
{
    [MessageHandler(SceneType.Gate)]
    public class C2G_UpdatePowerupHandle : AMRpcHandler<C2G_UpdatePowerup, G2C_UpdatePowerup>
    {
        protected override async ETTask Run(Session session, C2G_UpdatePowerup request, G2C_UpdatePowerup response)
        {
            //�õ���ϢЭ�������
            string powerupCfg = request.PowerUpcfg;

            Player player = session.GetComponent<SessionPlayerComponent>().Player;
            long playerId = player.Id;

            //����ָ������
            bool isUPdateSucces = await PlayerCacheHelper.UpdateSeasonBringUp(session.DomainScene(), playerId, powerupCfg);

            if (!isUPdateSucces)
            {
                response.Error = ET.ErrorCode.ERR_LogicError;
            }

            await ETTask.CompletedTask;
        }
    }
}