using System;
using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Server
{
    [MessageHandler(SceneType.Gate)]
    public class C2G_SetUIRedDotTypeHandle : AMRpcHandler<C2G_SetUIRedDotType, G2C_SetUIRedDotType>
    {
        protected override async ETTask Run(Session session, C2G_SetUIRedDotType request, G2C_SetUIRedDotType response)
        {
            Player player = session.GetComponent<SessionPlayerComponent>().Player;
            long playerId = player.Id;

            Scene scene = session.DomainScene();

            UIRedDotType uiRedDotType = (UIRedDotType)request.UIRedDotType;
            string itemCfgId = request.ItemCfgId;
            if (uiRedDotType == UIRedDotType.None)
            {
                if (string.IsNullOrEmpty(itemCfgId) == false)
                {
                    PlayerBackPackComponent playerBackPackComponent = await PlayerCacheHelper.GetPlayerBackPackByPlayerId(scene, playerId);
                    playerBackPackComponent.RemoveNewItemRecord(itemCfgId);

                    PlayerModelChgType playerModelChgType = PlayerModelChgType.PlayerBackPack_NewItemList;
                    await PlayerCacheHelper.SavePlayerModel(scene, playerId, PlayerModelType.BackPack, new() { "newItemList" }, playerModelChgType);

                    await PlayerCacheHelper.DealPlayerUIRedDotType(scene, playerId, PlayerModelType.BackPack);
                }
            }
            else
            {
                await PlayerCacheHelper.SetUIRedDotType(scene, playerId, uiRedDotType, false, true);
            }

            await ETTask.CompletedTask;
        }
    }
}