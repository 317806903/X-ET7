using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class BeKickedRoom_RefreshUI: AEvent<Scene, ClientEventType.BeKickedRoom>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.BeKickedRoom args)
        {
            UIManagerHelper.GetUIComponent(scene).HideAllShownWindow();

            await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgHall>();

            await ETTask.CompletedTask;
        }
    }
}