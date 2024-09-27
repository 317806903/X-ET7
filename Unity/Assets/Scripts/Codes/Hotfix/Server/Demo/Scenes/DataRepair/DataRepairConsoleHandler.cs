using System;
using System.Collections.Generic;
using System.Reflection;
using ET.AbilityConfig;

namespace ET.Server
{
    [ConsoleHandler(ConsoleMode.DataRepair)]
    public class DataRepairConsoleHandler: IConsoleHandler
    {
        public async ETTask Run(ModeContex contex, string content)
        {
            string[] ss = content.Split(" ");
            switch (ss[0])
            {
                case ConsoleMode.DataRepair:
                    break;

                case "BackPack":
                {
                    StartSceneConfig playerCacheSceneConfig = StartSceneConfigCategory.Instance.GetPlayerCacheManager(1);
                    if (playerCacheSceneConfig.Process != Options.Instance.Process)
                    {
                        return;
                    }
                    Scene scene = ServerSceneManagerComponent.Instance.Get(playerCacheSceneConfig.Id);

                    await DBHelper.DropCollection<ET.PlayerBackPackComponent>(scene);
                    await DBHelper.DropCollection<ET.PlayerBattleCardComponent>(scene);

                    List<PlayerBaseInfoComponent> entityDBList = await ET.Server.DBHelper._LoadDBList<PlayerBaseInfoComponent>(scene);
                    if (entityDBList == null || entityDBList.Count == 0)
                    {
                        return;
                    }

                    void GetFirstClearDropItems(PlayerBackPackComponent playerBackPackComponent, int pveIndex)
                    {
                        ChallengeLevelCfg challengeLevelCfg = TowerDefense_ChallengeLevelCfgCategory.Instance.GetChallengeByIndex(pveIndex);
                        //发放首通奖励
                        Dictionary<string, int> firstClearDropItems = ET.DropItemRuleHelper.Drop(challengeLevelCfg.FirstClearDropItem);

                        foreach (var item in firstClearDropItems)
                        {
                            string itemCfgId = item.Key;
                            int count = item.Value;

                            playerBackPackComponent.AddItem(itemCfgId, count);
                        }
                    }

                    foreach (PlayerBaseInfoComponent playerBaseInfoComponent in entityDBList)
                    {
                        long playerId = playerBaseInfoComponent.GetPlayerId();
                        ET.Server.PlayerCacheLocalHelper.ClearPlayerCache(scene, playerId);

                        PlayerBackPackComponent playerBackPackComponent = await PlayerCacheLocalHelper.GetPlayerModel(scene, playerId, PlayerModelType.BackPack) as PlayerBackPackComponent;

                        int pveIndex = playerBaseInfoComponent.ChallengeClearLevel;
                        for (int i = 1; i <= pveIndex; i++)
                        {
                            GetFirstClearDropItems(playerBackPackComponent, i);
                        }
                        playerBackPackComponent.SetDataCacheAutoWrite();
                        playerBaseInfoComponent.Dispose();
                    }
                    break;
                }
                case "ClearSeasonDB":
                {
                    StartSceneConfig playerCacheSceneConfig = StartSceneConfigCategory.Instance.GetPlayerCacheManager(1);
                    if (playerCacheSceneConfig.Process != Options.Instance.Process)
                    {
                        return;
                    }
                    Scene scene = ServerSceneManagerComponent.Instance.Get(playerCacheSceneConfig.Id);

                    await DBHelper.DropCollection<ET.PlayerMailComponent>(scene);
                    await DBHelper.DropCollection<ET.RankEndlessChallengeComponent>(scene);
                    await DBHelper.DropCollection<ET.RankEndlessChallengeItemComponent>(scene);
                    await DBHelper.DropCollection<ET.SeasonComponent>(scene);
                    await DBHelper.DropCollection<ET.PlayerSeasonInfoComponent>(scene);
                    await DBHelper.DropCollection<ET.MailToPlayersComponent>(scene);
                    break;
                }
                case "BackPackReplace":
                {
                    StartSceneConfig playerCacheSceneConfig = StartSceneConfigCategory.Instance.GetPlayerCacheManager(1);
                    if (playerCacheSceneConfig.Process != Options.Instance.Process)
                    {
                        return;
                    }
                    Scene scene = ServerSceneManagerComponent.Instance.Get(playerCacheSceneConfig.Id);

                    List<PlayerBaseInfoComponent> entityDBList = await ET.Server.DBHelper._LoadDBList<PlayerBaseInfoComponent>(scene);
                    if (entityDBList == null || entityDBList.Count == 0)
                    {
                        return;
                    }

                    void GetFirstClearDropItems(PlayerBackPackComponent playerBackPackComponent, int pveIndex)
                    {
                        ChallengeLevelCfg challengeLevelCfg = TowerDefense_ChallengeLevelCfgCategory.Instance.GetChallengeByIndex(pveIndex);
                        //发放首通奖励
                        Dictionary<string, int> firstClearDropItems = ET.DropItemRuleHelper.Drop(challengeLevelCfg.FirstClearDropItem);

                        foreach (var item in firstClearDropItems)
                        {
                            string itemCfgId = item.Key;
                            int count = item.Value;
                            if (ItemHelper.ChkIsTower(itemCfgId) == false)
                            {
                                continue;
                            }
                            playerBackPackComponent.SetItem(itemCfgId, count);
                        }
                    }

                    List<string> clearItemList = new() {"Tow1_1","Tow2_1","Tow3_1","Tow4_1","Tow5_1","Tow6_1","Tow7_1","Tow8_1","Tow10_1","Tow11_1","Tow21_1","Tow25_1"};
                    int index = 0;
                    int onceNum = 500;
                    int dealCount = 0;
                    int totalCount = entityDBList.Count;
                    foreach (PlayerBaseInfoComponent playerBaseInfoComponent in entityDBList)
                    {
                        index++;
                        dealCount++;
                        if (index > onceNum)
                        {
                            index = 0;
                            Log.Console($"--{dealCount}/{totalCount}");
                            await TimerComponent.Instance.WaitAsync(1000);
                        }
                        long playerId = playerBaseInfoComponent.GetPlayerId();
                        ET.Server.PlayerCacheLocalHelper.ClearPlayerCache(scene, playerId);

                        PlayerBackPackComponent playerBackPackComponent = await PlayerCacheLocalHelper.GetPlayerModel(scene, playerId, PlayerModelType.BackPack) as PlayerBackPackComponent;

                        PlayerBattleCardComponent playerBattleCardComponent = await PlayerCacheLocalHelper.GetPlayerModel(scene, playerId, PlayerModelType.BattleCard) as PlayerBattleCardComponent;

                        bool isRemoveBattleCard = false;
                        foreach (string itemCfgId in clearItemList)
                        {
                            playerBackPackComponent.RemoveItem(itemCfgId);
                            if (playerBattleCardComponent.itemCfgIdList.Contains(itemCfgId))
                            {
                                isRemoveBattleCard = true;
                                playerBattleCardComponent.itemCfgIdList.Remove(itemCfgId);
                            }
                        }

                        if (isRemoveBattleCard)
                        {
                            playerBattleCardComponent.SetDataCacheAutoWrite();
                        }

                        foreach (var itemCfgId in GlobalSettingCfgCategory.Instance.InitialBackpackItem)
                        {
                            int count = 1;
                            playerBackPackComponent.SetItem(itemCfgId, count);
                        }

                        int pveIndex = playerBaseInfoComponent.ChallengeClearLevel;
                        for (int i = 1; i <= pveIndex; i++)
                        {
                            GetFirstClearDropItems(playerBackPackComponent, i);
                        }
                        playerBackPackComponent.SetDataCacheAutoWrite();
                        playerBaseInfoComponent.Dispose();
                    }
                    break;
                }
            }
            await ETTask.CompletedTask;
        }
    }
}