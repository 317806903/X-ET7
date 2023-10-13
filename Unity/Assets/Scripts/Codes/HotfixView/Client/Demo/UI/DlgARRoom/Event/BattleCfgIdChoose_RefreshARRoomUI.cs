using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class BattleCfgIdChoose_RefreshARRoomUI: AEvent<Scene, EventType.BattleCfgIdChoose>
    {
        protected override async ETTask Run(Scene scene, EventType.BattleCfgIdChoose args)
        {
            DlgARRoom _DlgARRoom = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgARRoom>(true);
            if (_DlgARRoom != null)
            {
                _DlgARRoom.RefreshBattleCfgIdChoose(args.gamePlayBattleLevelCfgId).Coroutine();
            }
            await ETTask.CompletedTask;
        }
    }
}