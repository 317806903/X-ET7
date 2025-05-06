using System;
using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Server
{
    [MessageHandler(SceneType.Gate)]
    public class C2G_RecordGameJudgeChooseHandler : AMRpcHandler<C2G_RecordGameJudgeChoose, G2C_RecordGameJudgeChoose>
    {
        protected override async ETTask Run(Session session, C2G_RecordGameJudgeChoose request, G2C_RecordGameJudgeChoose response)
        {
            Player player = session.GetComponent<SessionPlayerComponent>().Player;
            long playerId = player.Id;

            GameJudgeChooseType gameJudgeChooseType = (GameJudgeChooseType)request.GameJudgeChooseType;
            string complainMsg = request.ComplainMsg;
            await GameJudgeChooseHelper.RecordGameJudgeChoose(session.DomainScene(), playerId, gameJudgeChooseType, complainMsg);
            await ETTask.CompletedTask;
        }
    }
}