using System;
using System.Collections.Generic;

namespace ET.Server
{
    [ActorMessageHandler(SceneType.Mail)]
    public class G2M_ChkIsNewMailFromCenterHandler: AMActorRpcHandler<Scene, G2M_ChkIsNewMailFromCenter, M2G_ChkIsNewMailFromCenter>
    {
        protected override async ETTask Run(Scene scene, G2M_ChkIsNewMailFromCenter request, M2G_ChkIsNewMailFromCenter response)
        {
            long playerId = request.PlayerId;

            MailManagerComponent mailManagerComponent = ET.Server.MailHelper.GetMailManager(scene);
            bool isNew = mailManagerComponent.ChkIsNewPlayerMailFromCenter(playerId);
            response.IsNew = isNew? 1 : 0;

            await ETTask.CompletedTask;
        }
    }
}