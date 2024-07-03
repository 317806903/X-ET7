using System;
using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Server
{
    [MessageHandler(SceneType.Gate)]
    public class C2G_ResetPowerupHandle : AMRpcHandler<C2G_ResetPowerup, G2C_ResetPowerup>
    {
        protected override async ETTask Run(Session session, C2G_ResetPowerup request, G2C_ResetPowerup response)
        {
            Player player = session.GetComponent<SessionPlayerComponent>().Player;
            long playerId = player.Id;

            bool isPlayerCanReset = await PlayerCacheHelper.IsPlayerCanReset(session.DomainScene(), playerId);
            if(isPlayerCanReset)
            {
                SeasonComponent seasonComponent = await SeasonHelper.GetSeasonComponent(session.DomainScene(), false);
                int resetCost = seasonComponent.cfg.BringUpResetCost;
                await PlayerCacheHelper.ReduceTokenDiamond(session.DomainScene(), playerId, resetCost);

                int rewardDiamond = await PlayerCacheHelper.ResetAllPowerup(session.DomainScene(), playerId);

                await PlayerCacheHelper.AddTokenDiamond(session.DomainScene(), playerId, rewardDiamond);
            }
            else
            {
                response.Error = ET.ErrorCode.ERR_LogicError;
            }       
        }
    }
}