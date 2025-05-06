using System;
using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Server
{
    [MessageHandler(SceneType.Gate)]
    public class C2G_SetBattleNoticeFinishedHandle : AMRpcHandler<C2G_SetBattleNoticeFinished, G2C_SetBattleNoticeFinished>
    {
        protected override async ETTask Run(Session session, C2G_SetBattleNoticeFinished request, G2C_SetBattleNoticeFinished response)
        {
            Player player = session.GetComponent<SessionPlayerComponent>().Player;
            long playerId = player.Id;

            Scene scene = session.DomainScene();
            string battleNoticeCfgId = request.BattleNoticeCfgId;
            {
                PlayerOtherInfoComponent playerOtherInfoComponent = await PlayerCacheHelper.GetPlayerOtherInfoByPlayerId(scene, playerId, true);
                playerOtherInfoComponent.SetBattleNoticeFinished(battleNoticeCfgId);
                await ET.Server.PlayerCacheHelper.SavePlayerModel(scene, playerId, PlayerModelType.OtherInfo, new(){"battleNoticeStatus"}, PlayerModelChgType.PlayerOtherInfo_BattleNotice);
            }

            await ETTask.CompletedTask;
        }
    }
}