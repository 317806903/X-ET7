using System;
using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Server
{
    [MessageHandler(SceneType.Gate)]
    public class C2G_GetUIRedDotTypeHandle : AMRpcHandler<C2G_GetUIRedDotType, G2C_GetUIRedDotType>
    {
        protected override async ETTask Run(Session session, C2G_GetUIRedDotType request, G2C_GetUIRedDotType response)
        {
            Player player = session.GetComponent<SessionPlayerComponent>().Player;
            long playerId = player.Id;

            Scene scene = session.DomainScene();

            await PlayerCacheHelper.DealPlayerUIRedDotType(scene, playerId, PlayerModelType.None);

            await ETTask.CompletedTask;
        }
    }
}