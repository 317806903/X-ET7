using System;
using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Server
{
    [MessageHandler(SceneType.Gate)]
    public class C2G_SetQuestionnaireFinishedHandle : AMRpcHandler<C2G_SetQuestionnaireFinished, G2C_SetQuestionnaireFinished>
    {
        protected override async ETTask Run(Session session, C2G_SetQuestionnaireFinished request, G2C_SetQuestionnaireFinished response)
        {
            Player player = session.GetComponent<SessionPlayerComponent>().Player;
            long playerId = player.Id;

            Scene scene = session.DomainScene();
            string questionnaireCfgId = request.QuestionnaireCfgId;
            {
                PlayerOtherInfoComponent playerOtherInfoComponent = await PlayerCacheHelper.GetPlayerOtherInfoByPlayerId(scene, playerId, true);
                if (playerOtherInfoComponent.ChkNeedQuestionnaire(questionnaireCfgId) == false)
                {
                    return;
                }
                playerOtherInfoComponent.SetQuestionnaireFinished(questionnaireCfgId);
                playerOtherInfoComponent.SetUIRedDotType(UIRedDotType.Questionnaire, false);
                await ET.Server.PlayerCacheHelper.SavePlayerModel(scene, playerId, PlayerModelType.OtherInfo, new(){"questionnaireStatus", "uiRedDotTypeDic"}, PlayerModelChgType.PlayerOtherInfo_RewardQuestionnaire);
            }
            {
                QuestionnaireCfg questionnaireCfg = QuestionnaireCfgCategory.Instance.Get(questionnaireCfgId);

                string mailCfgId = questionnaireCfg.RewardMail;

                MailToPlayerType mailToPlayerType = MailToPlayerType.PlayerList;
                List<long> waitSendPlayerList = new();
                waitSendPlayerList.Add(playerId);

                MailCfg mailCfg = MailCfgCategory.Instance.Get(mailCfgId);
                DateTime limitTimeTmp = TimeHelper.DateTimeNow().AddDays(mailCfg.EffectiveTime);
                long limitTime = TimeHelper.ToTimeStamp(limitTimeTmp);
                Dictionary<string, int> itemCfgList = ET.DropItemRuleHelper.Drop(mailCfg.DropRuleId);

                long receiveTime = TimeHelper.ServerNow();
                string mailTitle = mailCfg.MailTitle;
                string mailContent = mailCfg.MailContent;
                await ET.Server.MailHelper.InsertMailToCenter(scene, -1, mailCfg.MailType, mailTitle, mailContent, itemCfgList, receiveTime, limitTime, mailToPlayerType, waitSendPlayerList, null);
            }

            await ETTask.CompletedTask;
        }
    }
}