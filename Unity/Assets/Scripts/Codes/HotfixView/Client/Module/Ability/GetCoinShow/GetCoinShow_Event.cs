using System;
using ET.Ability.Client;
using UnityEngine.SceneManagement;

namespace ET.Client
{
    [Event(SceneType.Current)]
    public class GetCoinShow_Event: AEvent<Scene, EventType.SyncGetCoinShow>
    {
        protected override async ETTask Run(Scene scene, EventType.SyncGetCoinShow args)
        {
            Unit unit = args.unit;
            int chgValue = args.chgValue;
            ShowGetGoldTextComponent.Instance.ShowGetGold(unit, chgValue);
        }
    }
}