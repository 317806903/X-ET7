using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class EnterMapFinish_UI: AEvent<Scene, EventType.EnterMapFinish>
    {
        protected override async ETTask Run(Scene scene, EventType.EnterMapFinish args)
        {
            scene.GetComponent<UIComponent>().HideAllShownWindow();
            await scene.GetComponent<UIComponent>().ShowWindowAsync(WindowID.WindowID_Battle);

            await ETTask.CompletedTask;
        }
    }
}