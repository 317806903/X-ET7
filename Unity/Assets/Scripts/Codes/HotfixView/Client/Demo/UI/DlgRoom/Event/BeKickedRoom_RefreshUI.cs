using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class BeKickedRoom_RefreshUI: AEvent<Scene, EventType.BeKickedRoom>
    {
        protected override async ETTask Run(Scene scene, EventType.BeKickedRoom args)
        {
            scene.GetComponent<UIComponent>().HideAllShownWindow();
            
            await scene.GetComponent<UIComponent>().ShowWindowAsync(WindowID.WindowID_Hall);
            
            await ETTask.CompletedTask;
        }
    }
}