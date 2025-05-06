using System;
using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Server
{
    [MessageHandler(SceneType.Gate)]
    public class C2G_ChkGameJudgeChooseHandler : AMRpcHandler<C2G_ChkGameJudgeChoose, G2C_ChkGameJudgeChoose>
    {
        protected override async ETTask Run(Session session, C2G_ChkGameJudgeChoose request, G2C_ChkGameJudgeChoose response)
        {
            Player player = session.GetComponent<SessionPlayerComponent>().Player;
            long playerId = player.Id;
            Scene scene = session.DomainScene();
            PlayerBaseInfoComponent playerBaseInfoComponent = await PlayerCacheHelper.GetPlayerBaseInfoByPlayerId(scene, playerId, true);
            bool isNeed = false;
            if (playerBaseInfoComponent.AREndlessChallengeBattleCount + playerBaseInfoComponent.ARPVEBattleCount + playerBaseInfoComponent.ARPVPBattleCount >= 3)
            {
                isNeed = await GameJudgeChooseHelper.ChkNeedShow(scene, playerId);
            }
            response.IsNeed = isNeed?1:0;
            await ETTask.CompletedTask;
        }
    }
}