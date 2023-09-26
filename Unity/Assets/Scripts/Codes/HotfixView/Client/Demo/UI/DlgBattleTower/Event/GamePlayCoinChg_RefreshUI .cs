using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class GamePlayCoinChg_RefreshUI: AEvent<Scene, EventType.GamePlayCoinChg>
    {
        protected override async ETTask Run(Scene scene, EventType.GamePlayCoinChg args)
        {
            if (ET.Client.GamePlayHelper.GetGamePlay(scene).IsAR())
            {
                DlgBattleTowerAR _DlgBattleTowerAR = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgBattleTowerAR>(true);
                if (_DlgBattleTowerAR != null)
                {
                    _DlgBattleTowerAR.RefreshCoin();

                    if (args.getCoinType == GetCoinType.Normal)
                    {

                    }
                    else if (args.getCoinType == GetCoinType.InterestOnDeposit)
                    {
                        foreach (var coinList in args.myCoinListChg)
                        {
                            Log.Debug($" GetCoinType.InterestOnDeposit myCoinListChg[{coinList.Key}] [{coinList.Value}]");

                            string tipMsg = $"获得波次结算存款利息:{coinList.Value}";
                            ET.Client.UIManagerHelper.ShowConfirm(scene, tipMsg, null);
                        }
                    }
                    else if (args.getCoinType == GetCoinType.WaveRewardGold)
                    {
                        foreach (var coinList in args.myCoinListChg)
                        {
                            Log.Debug($" GetCoinType.WaveRewardGold myCoinListChg[{coinList.Key}] [{coinList.Value}]");
                            string tipMsg = $"波次结束奖励金币:{coinList.Value}";
                            ET.Client.UIManagerHelper.ShowConfirm(scene, tipMsg, null);
                        }
                    }
                }
            }
            else
            {
                DlgBattleTower _DlgBattleTower = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgBattleTower>(true);
                if (_DlgBattleTower != null)
                {
                    _DlgBattleTower.RefreshCoin();

                    if (args.getCoinType == GetCoinType.Normal)
                    {

                    }
                    else if (args.getCoinType == GetCoinType.InterestOnDeposit)
                    {
                        foreach (var coinList in args.myCoinListChg)
                        {
                            Log.Debug($" GetCoinType.InterestOnDeposit myCoinListChg[{coinList.Key}] [{coinList.Value}]");

                            string tipMsg = $"获得波次结算存款利息:{coinList.Value}";
                            ET.Client.UIManagerHelper.ShowConfirm(scene, tipMsg, null);
                        }
                    }
                    else if (args.getCoinType == GetCoinType.WaveRewardGold)
                    {
                        foreach (var coinList in args.myCoinListChg)
                        {
                            Log.Debug($" GetCoinType.WaveRewardGold myCoinListChg[{coinList.Key}] [{coinList.Value}]");
                            string tipMsg = $"波次结束奖励金币:{coinList.Value}";
                            ET.Client.UIManagerHelper.ShowConfirm(scene, tipMsg, null);
                        }
                    }
                }
            }
            await ETTask.CompletedTask;
        }
    }
}