using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class BattleCfgIdChoose_RefreshLobbyUI: AEvent<Scene, ClientEventType.BattleCfgIdChoose>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.BattleCfgIdChoose args)
        {
            DlgLobby _DlgLobby = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgLobby>(true);
            if (_DlgLobby != null)
            {
                _DlgLobby.RefreshBattleCfgIdChoose(args.gamePlayBattleLevelCfgId).Coroutine();
            }
            await ETTask.CompletedTask;
        }
    }
}