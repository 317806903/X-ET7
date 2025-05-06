using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace ET.Client
{
    [Event(SceneType.Current)]
    public class FloatingText_Event: AEvent<Scene, EventType.SyncFloatingText>
    {
        protected override async ETTask Run(Scene scene, EventType.SyncFloatingText args)
        {
            List<(Unit unit, string floatingTextActionId, int showNum, bool isOnlySelfShow)> list = args.list;
            if (list == null)
            {
                return;
            }
            foreach ((Unit unit, string floatingTextActionId, int showNum, bool isOnlySelfShow) in list)
            {
                ET.Ability.Client.FloatingTextHelper.CreateFloatingText(unit, floatingTextActionId, showNum);
            }
        }
    }
}