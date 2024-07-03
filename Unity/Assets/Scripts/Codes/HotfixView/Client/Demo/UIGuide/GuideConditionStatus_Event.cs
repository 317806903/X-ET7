using System;
using System.Collections.Generic;
using ET.Ability.Client;
using UnityEngine.SceneManagement;

namespace ET.Client
{
    [Event(SceneType.Current|SceneType.Client)]
    public class GuideConditionStatus_Event: AEvent<Scene, EventType.NoticeGuideConditionStatus>
    {
        protected override async ETTask Run(Scene scene, EventType.NoticeGuideConditionStatus args)
        {
            if (UIGuideComponent.Instance == null)
            {
                return;
            }
            if (UIGuideComponent.Instance.CurUIGuideComponent == null || UIGuideComponent.Instance.CurUIGuideComponent.IsDisposed)
            {
                return;
            }
            GuideConditionStaticMethodType guideConditionStaticMethodType = (GuideConditionStaticMethodType)Enum.Parse(typeof(GuideConditionStaticMethodType), args.guideConditionStaticMethodType);
            UIGuideComponent.Instance.CurUIGuideComponent.guideConditionStatus[guideConditionStaticMethodType] = true;
        }
    }
}