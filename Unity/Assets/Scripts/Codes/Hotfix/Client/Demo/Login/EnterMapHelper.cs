using System;

namespace ET.Client
{
    public static class EnterMapHelper
    {
        public static async ETTask EnterMapAsync(Scene clientScene, string gamePlayBattleLevelCfgId)
        {
            try
            {
                G2C_EnterMap g2CEnterMap = await ET.Client.SessionHelper.GetSession(clientScene).Call(new C2G_EnterMap()
                {
                    GamePlayBattleLevelCfgId = gamePlayBattleLevelCfgId,
                }) as G2C_EnterMap;
            }
            catch (Exception e)
            {
                Log.Error(e);
            }	
        }
        
    }
}