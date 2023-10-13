using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class BattleCfgIdChoose_RefreshRoomUI: AEvent<Scene, EventType.BattleCfgIdChoose>
    {
        protected override async ETTask Run(Scene scene, EventType.BattleCfgIdChoose args)
        {
            DlgRoom _DlgRoom = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgRoom>(true);
            if (_DlgRoom != null)
            {
                _DlgRoom.RefreshBattleCfgIdChoose(args.gamePlayBattleLevelCfgId).Coroutine();
            }
            await ETTask.CompletedTask;
        }
    }
}