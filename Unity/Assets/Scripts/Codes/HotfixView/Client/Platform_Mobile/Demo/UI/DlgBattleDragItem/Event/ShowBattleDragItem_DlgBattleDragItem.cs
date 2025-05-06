using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client | SceneType.Current)]
    public class ShowBattleDragItem_DlgBattleDragItem: AEvent<Scene, ClientEventType.ShowBattleDragItem>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.ShowBattleDragItem args)
        {
            DlgBattleDragItem_ShowWindowData showWindowData = new()
            {
                battleDragItemType = args.battleDragItemType,
                battleDragItemParam = args.battleDragItemParam,
                moveTowerUnitId = args.moveTowerUnitId,
                countOnce = args.countOnce,
                createActionIds = args.createActionIds,
                sceneIn = args.sceneIn,
                callBack = args.callBack,
            };
            await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgBattleDragItem>(showWindowData);
            await ETTask.CompletedTask;
        }
    }
}