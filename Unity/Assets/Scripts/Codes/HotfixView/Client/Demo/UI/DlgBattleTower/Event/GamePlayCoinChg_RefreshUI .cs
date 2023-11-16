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
                    else if (args.getCoinType == GetCoinType.DivideEquallyTeamCoin)
                    {
                        foreach (var coinList in args.myCoinListChg)
                        {
                            Log.Debug($" GetCoinType.DivideEquallyTeamCoin myCoinListChg[{coinList.Key}] [{coinList.Value}]");

                            string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_DivideEquallyTeamCoin", coinList.Value);
                            ET.Client.UIManagerHelper.ShowOnlyConfirm(scene, tipMsg, null);
                        }
                    }
                    else if (args.getCoinType == GetCoinType.InterestOnDeposit)
                    {
                        foreach (var coinList in args.myCoinListChg)
                        {
                            Log.Debug($" GetCoinType.InterestOnDeposit myCoinListChg[{coinList.Key}] [{coinList.Value}]");

                            string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_InterestOnDeposit", coinList.Value);
                            ET.Client.UIManagerHelper.ShowOnlyConfirm(scene, tipMsg, null);
                        }
                    }
                    else if (args.getCoinType == GetCoinType.WaveRewardGold)
                    {
                        foreach (var coinList in args.myCoinListChg)
                        {
                            Log.Debug($" GetCoinType.WaveRewardGold myCoinListChg[{coinList.Key}] [{coinList.Value}]");
                            string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_WaveRewardGold", coinList.Value);
                            ET.Client.UIManagerHelper.ShowOnlyConfirm(scene, tipMsg, null);
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
                    else if (args.getCoinType == GetCoinType.DivideEquallyTeamCoin)
                    {
                        foreach (var coinList in args.myCoinListChg)
                        {
                            Log.Debug($" GetCoinType.DivideEquallyTeamCoin myCoinListChg[{coinList.Key}] [{coinList.Value}]");

                            string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_DivideEquallyTeamCoin", coinList.Value);
                            ET.Client.UIManagerHelper.ShowOnlyConfirm(scene, tipMsg, null);
                        }
                    }
                    else if (args.getCoinType == GetCoinType.InterestOnDeposit)
                    {
                        foreach (var coinList in args.myCoinListChg)
                        {
                            Log.Debug($" GetCoinType.InterestOnDeposit myCoinListChg[{coinList.Key}] [{coinList.Value}]");

                            string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_InterestOnDeposit", coinList.Value);
                            ET.Client.UIManagerHelper.ShowOnlyConfirm(scene, tipMsg, null);
                        }
                    }
                    else if (args.getCoinType == GetCoinType.WaveRewardGold)
                    {
                        foreach (var coinList in args.myCoinListChg)
                        {
                            Log.Debug($" GetCoinType.WaveRewardGold myCoinListChg[{coinList.Key}] [{coinList.Value}]");
                            string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_WaveRewardGold", coinList.Value);
                            ET.Client.UIManagerHelper.ShowOnlyConfirm(scene, tipMsg, null);
                        }
                    }
                }
            }
            await ETTask.CompletedTask;
        }
    }
}