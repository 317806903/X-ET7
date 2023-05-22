using System.Collections.Generic;

namespace ET.Ability
{
    public class Action_SummonUnit: IActionHandler
    {
        public override async ETTask Run(Unit unit, string actionId, SelectHandle selectHandle)
        {
            await ETTask.CompletedTask;
        }
    }
}