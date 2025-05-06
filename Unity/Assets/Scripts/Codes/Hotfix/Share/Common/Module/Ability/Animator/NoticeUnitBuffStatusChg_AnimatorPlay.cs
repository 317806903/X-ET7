using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [Event(SceneType.Map)]
    public class NoticeUnitBuffStatusChg_AnimatorPlay: AEvent<Scene, EventType.NoticeUnitBuffStatusChg>
    {
        protected override async ETTask Run(Scene scene, EventType.NoticeUnitBuffStatusChg args)
        {
            Unit unit = args.Unit;
            ET.Ability.AnimatorHelper.ResetControlStateAnimatorMotion(unit);
            await ETTask.CompletedTask;
        }
    }
}