using System;
using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Client
{
    [Event(SceneType.Current)]
    public class GetCoinShow_Event: AEvent<Scene, EventType.SyncGetCoinShow>
    {
        protected override async ETTask Run(Scene scene, EventType.SyncGetCoinShow args)
        {
            List<(Unit unit, CoinTypeInGame coinType, int chgValue)> list = args.list;
            if (list == null)
            {
                return;
            }
            foreach ((Unit unit, CoinTypeInGame coinType, int chgValue) in list)
            {
                ShowGetGoldTextComponent.Instance.ShowGetGold(unit, chgValue);
            }
        }
    }
}