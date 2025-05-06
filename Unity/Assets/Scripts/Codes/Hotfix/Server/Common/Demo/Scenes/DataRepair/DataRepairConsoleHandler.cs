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
                    Log.Console($"--finished");
                    break;
                }
                case "BackPackRepair":
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

                    void GetFirstClearDropItems(PlayerBackPackComponent playerBackPackComponent, int pveIndex, PVELevelDifficulty pveLevelDifficulty)
                    {
                        ChallengeLevelCfg challengeLevelCfg = TowerDefense_ChallengeLevelCfgCategory.Instance.GetChallengeByIndex(pveIndex, pveLevelDifficulty);
                        //发放首通奖励
                        Dictionary<string, int> firstClearDropItems = ET.DropItemRuleHelper.Drop(challengeLevelCfg.FirstClearDropItem);

                        foreach (var item in firstClearDropItems)
                        {
                            string itemCfgId = item.Key;
                            int count = item.Value;
                            if (ET.ItemHelper.ChkIsTower(itemCfgId) || ET.ItemHelper.ChkIsSkill(itemCfgId))
                            {
                                playerBackPackComponent.SetItem(itemCfgId, count);
                            }
                        }
                    }

                    //List<string> clearItemList = new() {"Tow1_1","Tow2_1","Tow3_1","Tow4_1","Tow5_1","Tow6_1","Tow7_1","Tow8_1","Tow10_1","Tow11_1","Tow21_1","Tow25_1"};
                    List<string> clearItemList = new() {};
                    int index = 0;
                    int onceNum = 1000;
                    int dealCount = 0;
                    int totalCount = entityDBList.Count;
                    Log.Console($"--entityDBList.Count={totalCount}");
                    foreach (PlayerBaseInfoComponent playerBaseInfoComponentDB in entityDBList)
                    {
                        index++;
                        dealCount++;
                        if (index > onceNum)
                        {
                            index = 0;
                            Log.Console($"--{dealCount}/{totalCount}");
                            await TimerComponent.Instance.WaitAsync(300);
                        }
                        long playerId = playerBaseInfoComponentDB.GetPlayerId();
                        ET.Server.PlayerCacheLocalHelper.ClearPlayerCache(scene, playerId);

                        PlayerBackPackComponent playerBackPackComponent = await PlayerCacheLocalHelper.GetPlayerModel(scene, playerId, PlayerModelType.BackPack) as PlayerBackPackComponent;

                        PlayerBattleCardComponent playerBattleCardComponent = await PlayerCacheLocalHelper.GetPlayerModel(scene, playerId, PlayerModelType.BattleCard) as PlayerBattleCardComponent;

                        PlayerBattleSkillComponent playerBattleSkillComponent = await PlayerCacheLocalHelper.GetPlayerModel(scene, playerId, PlayerModelType.BattleSkill) as PlayerBattleSkillComponent;

                        bool isRemoveBattleCard = false;
                        bool isRemoveBattleSkill = false;
                        foreach (string itemCfgId in clearItemList)
                        {
                            playerBackPackComponent.RemoveItem(itemCfgId);
                            if (playerBattleCardComponent.itemCfgIdList.Contains(itemCfgId))
                            {
                                isRemoveBattleCard = true;
                                playerBattleCardComponent.itemCfgIdList.Remove(itemCfgId);
                            }
                            if (playerBattleSkillComponent.skillCfgIdList.Contains(itemCfgId))
                            {
                                isRemoveBattleSkill = true;
                                playerBattleSkillComponent.skillCfgIdList.Remove(itemCfgId);
                            }
                        }

                        if (isRemoveBattleCard)
                        {
                            playerBattleCardComponent.SetDataCacheAutoWrite();
                        }
                        if (isRemoveBattleSkill)
                        {
                            playerBattleSkillComponent.SetDataCacheAutoWrite();
                        }

                        foreach (var itemCfgId in GlobalSettingCfgCategory.Instance.InitialBackpackItem)
                        {
                            int count = 1;
                            playerBackPackComponent.SetItem(itemCfgId, count);
                        }

                        {
                            PlayerSeasonInfoComponent playerSeasonInfoComponent = await PlayerCacheLocalHelper.GetPlayerModel(scene, playerId, PlayerModelType.SeasonInfo) as PlayerSeasonInfoComponent;
                            if (playerSeasonInfoComponent.pveLevelInfo == null)
                            {
                                playerSeasonInfoComponent.pveLevelInfo = new();
                                playerSeasonInfoComponent.SetDataCacheAutoWrite();
                            }
                        }

                        {

                            if (playerBaseInfoComponentDB.pveLevelInfo == null)
                            {
                                playerBaseInfoComponentDB.pveLevelInfo = new();
                            }

                            PlayerBaseInfoComponent playerBaseInfoComponent = await PlayerCacheLocalHelper.GetPlayerModel(scene, playerId, PlayerModelType.BaseInfo) as PlayerBaseInfoComponent;
                            playerBaseInfoComponent.pveLevelInfo = playerBaseInfoComponentDB.pveLevelInfo;

                            playerBaseInfoComponent.SetDataCacheAutoWrite();

                        }

                        foreach (var item in playerBaseInfoComponentDB.pveLevelInfo)
                        {
                            int curPveIndex = item.Key;
                            PVELevelDifficulty curPveLevelDifficulty = item.Value;
                            foreach (PVELevelDifficulty pveLevelDifficulty in Enum.GetValues(typeof(PVELevelDifficulty)))
                            {
                                if (pveLevelDifficulty <= curPveLevelDifficulty)
                                {
                                    GetFirstClearDropItems(playerBackPackComponent, curPveIndex, pveLevelDifficulty);
                                }
                            }
                        }

                        playerBackPackComponent.SetDataCacheAutoWrite();

                        playerBaseInfoComponentDB.Dispose();
                    }
                    Log.Console($"--finished");
                    break;
                }
                case "Item2NewRepair":
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

                    Dictionary<string, string> Item2NewDic = new()
                    {
                        {"Tower_Collider_1",	"Tower_Box"},
                        {"Tower_Collider_2",	"Tower_BoostBox"},
                        {"Tow1_1",	"Tower_XBow1"},
                        {"Tow1_2",	"Tower_XBow2"},
                        {"Tow1_3",	"Tower_XBow3"},
                        {"Tow2_1",	"Tower_Cannon1"},
                        {"Tow2_2",	"Tower_Cannon2"},
                        {"Tow2_3",	"Tower_Cannon3"},
                        {"Tow3_1",	"Tower_Flame1"},
                        {"Tow3_2",	"Tower_Flame2"},
                        {"Tow3_3",	"Tower_Flame3"},
                        {"Tow4_1",	"Tower_AcidMist1"},
                        {"Tow4_2",	"Tower_AcidMist2"},
                        {"Tow4_3",	"Tower_AcidMist3"},
                        {"Tow5_1",	"Tower_Draco1"},
                        {"Tow5_2",	"Tower_Draco2"},
                        {"Tow5_3",	"Tower_Draco3"},
                        {"Tow6_1",	"Tower_Thunder1"},
                        {"Tow6_2",	"Tower_Thunder2"},
                        {"Tow6_3",	"Tower_Thunder3"},
                        {"Tow7_1",	"Tower_IceTower1"},
                        {"Tow7_2",	"Tower_IceTower2"},
                        {"Tow7_3",	"Tower_IceTower3"},
                        {"Tow8_1",	"Tower_SpeedTower1"},
                        {"Tow8_2",	"Tower_SpeedTower2"},
                        {"Tow8_3",	"Tower_SpeedTower3"},
                        {"Tow9_1",	"Tower_MystOrb1"},
                        {"Tow9_2",	"Tower_MystOrb2"},
                        {"Tow9_3",	"Tower_MystOrb3"},
                        {"Tow10_1",	"Tower_Alchemy1"},
                        {"Tow10_2",	"Tower_Alchemy2"},
                        {"Tow10_3",	"Tower_Alchemy3"},
                        {"Tow11_1",	"Tower_Scorpio1"},
                        {"Tow11_2",	"Tower_Scorpio2"},
                        {"Tow11_3",	"Tower_Scorpio3"},
                        {"Tow17_1",	"Tower_Crystal1"},
                        {"Tow17_2",	"Tower_Crystal2"},
                        {"Tow17_3",	"Tower_Crystal3"},
                        {"Tow21_1",	"Tower_Goblin1"},
                        {"Tow21_2",	"Tower_Goblin2"},
                        {"Tow21_3",	"Tower_Goblin3"},
                        {"Tow23_1",	"Tower_Rocket1"},
                        {"Tow23_2",	"Tower_Rocket2"},
                        {"Tow23_3",	"Tower_Rocket3"},
                        {"Tow25_1",	"Tower_Bomb1"},
                        {"Tow25_2",	"Tower_Bomb2"},
                        {"Tow25_3",	"Tower_Bomb3"},
                        {"Tow26_1",	"Tower_Golem1"},
                        {"Tow26_2",	"Tower_Golem2"},
                        {"Tow26_3",	"Tower_Golem3"},
                    };
                    int index = 0;
                    int onceNum = 1000;
                    int dealCount = 0;
                    int totalCount = entityDBList.Count;
                    Log.Console($"--entityDBList.Count={totalCount}");
                    foreach (PlayerBaseInfoComponent playerBaseInfoComponentDB in entityDBList)
                    {
                        index++;
                        dealCount++;
                        if (index > onceNum)
                        {
                            index = 0;
                            Log.Console($"--{dealCount}/{totalCount}");
                            await TimerComponent.Instance.WaitAsync(300);
                        }
                        long playerId = playerBaseInfoComponentDB.GetPlayerId();
                        ET.Server.PlayerCacheLocalHelper.ClearPlayerCache(scene, playerId);

                        PlayerBackPackComponent playerBackPackComponent = await PlayerCacheLocalHelper.GetPlayerModel(scene, playerId, PlayerModelType.BackPack) as PlayerBackPackComponent;

                        PlayerBattleCardComponent playerBattleCardComponent = await PlayerCacheLocalHelper.GetPlayerModel(scene, playerId, PlayerModelType.BattleCard) as PlayerBattleCardComponent;

                        PlayerBattleSkillComponent playerBattleSkillComponent = await PlayerCacheLocalHelper.GetPlayerModel(scene, playerId, PlayerModelType.BattleSkill) as PlayerBattleSkillComponent;

                        bool isChgBackPack = false;
                        bool isChgBattleCard = false;
                        bool isChgBattleSkill = false;
                        foreach (var item in Item2NewDic)
                        {
                            string itemCfgId = item.Key;
                            string itemCfgIdNew = item.Value;
                            if (playerBackPackComponent.ChkItemExist(itemCfgId))
                            {
                                isChgBackPack = true;
                                playerBackPackComponent.RemoveItem(itemCfgId);
                                playerBackPackComponent.SetItem(itemCfgIdNew, 1);
                            };
                            if (playerBackPackComponent.newItemList.Contains(itemCfgId))
                            {
                                isChgBackPack = true;
                                playerBackPackComponent.newItemList.Remove(itemCfgId);
                                playerBackPackComponent.newItemList.Add(itemCfgIdNew);
                            }

                            for (int i = 0; i < playerBattleCardComponent.itemCfgIdList.Count; i++)
                            {
                                if (playerBattleCardComponent.itemCfgIdList[i] == itemCfgId)
                                {
                                    isChgBattleCard = true;
                                    playerBattleCardComponent.itemCfgIdList[i] = itemCfgIdNew;
                                    break;
                                }
                            }

                            for (int i = 0; i < playerBattleSkillComponent.skillCfgIdList.Count; i++)
                            {
                                if (playerBattleSkillComponent.skillCfgIdList[i] == itemCfgId)
                                {
                                    isChgBattleSkill = true;
                                    playerBattleSkillComponent.skillCfgIdList[i] = itemCfgIdNew;
                                    break;
                                }
                            }
                        }

                        List<ItemComponent> itemList = playerBackPackComponent.GetItemList();
                        foreach (ItemComponent itemComponent in itemList)
                        {
                            if (itemComponent.GetComponent<ItemSkillComponent>() != null)
                            {
                                int level = itemComponent.GetComponent<ItemSkillComponent>().level;
                                if (level < GlobalSettingCfgCategory.Instance.SkillLevelWhenGot)
                                {
                                    itemComponent.GetComponent<ItemSkillComponent>().level = GlobalSettingCfgCategory.Instance.SkillLevelWhenGot;
                                    isChgBackPack = true;
                                }
                            }
                            else if (itemComponent.GetComponent<ItemTowerComponent>() != null)
                            {
                                int level = itemComponent.GetComponent<ItemTowerComponent>().level;
                                if (level < GlobalSettingCfgCategory.Instance.TowerLevelWhenGot)
                                {
                                    itemComponent.GetComponent<ItemTowerComponent>().level = GlobalSettingCfgCategory.Instance.TowerLevelWhenGot;
                                    isChgBackPack = true;
                                }
                            }
                        }

                        if (isChgBackPack)
                        {
                            playerBackPackComponent.SetDataCacheAutoWrite();
                        }

                        if (playerBattleCardComponent.itemCfgIdList_MonsterCall == null)
                        {
                            playerBattleCardComponent.itemCfgIdList_MonsterCall = new();
                            isChgBattleCard = true;
                        }
                        if (isChgBattleCard)
                        {
                            playerBattleCardComponent.SetDataCacheAutoWrite();
                        }
                        if (isChgBattleSkill)
                        {
                            playerBattleSkillComponent.SetDataCacheAutoWrite();
                        }

                        playerBaseInfoComponentDB.Dispose();
                    }
                    Log.Console($"--finished");
                    break;
                }
                default:
                    Log.Console($"--Error default");
                    break;
            }
            await ETTask.CompletedTask;
        }
    }
}