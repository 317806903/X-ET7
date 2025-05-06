using System;
using System.Collections.Generic;
using System.Xml.Schema;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Server
{
    [MessageHandler(SceneType.Gate)]
    public class C2G_ChkSeasonIndexChgHandler : AMHandler<C2G_ChkSeasonIndexChg>
    {
        protected override async ETTask Run(Session session, C2G_ChkSeasonIndexChg message)
        {
            Player player = session.GetComponent<SessionPlayerComponent>().Player;
            long playerId = player.Id;
            Scene scene = session.DomainScene();

            await ET.Server.PlayerCacheHelper.DealPlayerUIRedDotType(scene, playerId, PlayerModelType.SeasonInfo);

            await ETTask.CompletedTask;
        }
    }
}