using System;
using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;

namespace ET
{
    [FriendOf(typeof (PlayerSeasonInfoComponent))]
    public static class PlayerSeasonInfoComponentSystem
    {
        [ObjectSystem]
        public class PlayerSeasonInfoComponentAwakeSystem: AwakeSystem<PlayerSeasonInfoComponent>
        {
            protected override void Awake(PlayerSeasonInfoComponent self)
            {
                self.seasonBringUpDic = new();
                self.pveLevelInfo = new();
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="self"></param>
        public static void Init(this PlayerSeasonInfoComponent self)
        {
        }

        /// <summary>
        /// 获取玩家的ID
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static long GetPlayerId(this PlayerSeasonInfoComponent self)
        {
            return self.GetParent<PlayerDataComponent>().playerId;
        }

        /// <summary>
        /// 获取PlayerSeasonInfoComponent身上的字典
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Dictionary<string, int> GetSeasonBringUpDic(this PlayerSeasonInfoComponent self)
        {
            return self.seasonBringUpDic;
        }

        /// <summary>
        ///获取当前养成配置的等级
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static int GetSeasonBringUpLevel(this PlayerSeasonInfoComponent self, string seasonBringUpCfgId)
        {
            if (self.seasonBringUpDic.TryGetValue(seasonBringUpCfgId, out int bringUpLevel) == false)
            {
                bringUpLevel = 0;
            }

            return bringUpLevel;
        }

        /// <summary>
        /// 升级
        /// </summary>
        /// <param name="self"></param>
        /// <param name="seasonBringUpCfgId">Item的名字</param>
        /// <param name="level">升级到的等级</param>
        /// <returns></returns>
        public static bool ChgSeasonBringUpDic(this PlayerSeasonInfoComponent self, string seasonBringUpCfgId, int level)
        {
            if (self.seasonBringUpDic.TryGetValue(seasonBringUpCfgId, out var curLevel))
            {
                if (level <= curLevel)
                {
                    return false;
                }
            }

            self.seasonBringUpDic[seasonBringUpCfgId] = level;
            return true;
        }

        /// <summary>
        /// 重置所有的等级
        /// </summary>
        /// <param name="self"></param>
        public static async ETTask ResetSeasonBringUpDic(this PlayerSeasonInfoComponent self)
        {
            self.seasonBringUpDic.Clear();
            await ETTask.CompletedTask;
        }

        /// <summary>
        /// 获取玩家养成的回收价值
        /// </summary>
        /// <param name="self"></param>
        public static async ETTask<int> GetSeasonBringupReward(this PlayerSeasonInfoComponent self)
        {
            int rewardSum = 0;
            foreach (KeyValuePair<string, int> kvp in self.seasonBringUpDic)
            {
                if (kvp.Value == 0)
                {
                    continue;
                }

                rewardSum += self.GetReWardBringup(kvp.Key);
            }

            await ETTask.CompletedTask;
            return rewardSum;
        }

        /// <summary>
        /// 玩家单个养成升到当前等级需要花费的钻石
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static int GetReWardBringup(this PlayerSeasonInfoComponent self, string cfgId)
        {
            int playerLevel = self.GetSeasonBringUpLevel(cfgId); //当前养成的等级
            int rewardSum = 0; //单个养成所有的花费值
            for (int i = (--playerLevel); i >= 0; i--)
            {
                SeasonBringUpCfg seasonBringUpCfg = SeasonBringUpCfgCategory.Instance.GetSeasonBringUpCfg(cfgId, i);
                rewardSum += seasonBringUpCfg.Cost;
            }

            return rewardSum;
        }

        public static bool ChkIsLockPVELevel(this PlayerSeasonInfoComponent self, int pveLevel)
        {
            int curLevel = self.GetLastUnLockPVELevel();
            if (curLevel >= pveLevel)
            {
                return false;
            }

            if(self.pveLevelInfo.TryGetValue(pveLevel, out PVELevelDifficulty pveLevelDifficulty))
            {
                if (pveLevelDifficulty >= PVELevelDifficulty.Easy)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool ChkIsPassPVELevel(this PlayerSeasonInfoComponent self, int pveLevel, PVELevelDifficulty pveLevelDifficulty)
        {
            if(self.pveLevelInfo.TryGetValue(pveLevel, out PVELevelDifficulty curPveLevelDifficulty))
            {
                if (curPveLevelDifficulty >= pveLevelDifficulty)
                {
                    return true;
                }
            }
            return false;
        }

        public static List<Dictionary<string, int>> GetAllDropItems(this PlayerSeasonInfoComponent self, int seasonCfgId, int pveLevel, PVELevelDifficulty curPveLevelDifficulty, bool isOnlyShowDropItem)
        {
            List<Dictionary<string, int>> allDropItems = ListComponent<Dictionary<string, int>>.Create();
            foreach (PVELevelDifficulty pveLevelDifficulty in System.Enum.GetValues(typeof(PVELevelDifficulty)))
            {
                if (pveLevelDifficulty <= curPveLevelDifficulty)
                {
                    ChallengeLevelCfg challengeLevelCfg = SeasonChallengeLevelCfgCategory.Instance.GetChallengeByIndex(seasonCfgId, pveLevel, pveLevelDifficulty);

                    bool isPassPveLevel = self.ChkIsPassPVELevel(pveLevel, pveLevelDifficulty);
                    if (isPassPveLevel)
                    {
                        if (isOnlyShowDropItem)
                        {
                            allDropItems.Add(challengeLevelCfg.RepeatRewardItemListShow);
                        }
                        else
                        {
                            //重复通关奖励
                            Dictionary<string, int> repeatClearDropItems = ET.DropItemRuleHelper.Drop(challengeLevelCfg.RepeatClearDropItem);
                            allDropItems.Add(repeatClearDropItems);
                        }
                    }
                    else
                    {
                        if (isOnlyShowDropItem)
                        {
                            allDropItems.Add(challengeLevelCfg.FirstRewardItemListShow);
                        }
                        else
                        {
                            //发放首通奖励
                            Dictionary<string, int> firstClearDropItems = ET.DropItemRuleHelper.Drop(challengeLevelCfg.FirstClearDropItem);
                            allDropItems.Add(firstClearDropItems);
                        }
                    }
                }
            }
            return allDropItems;
        }

        public static Dictionary<string, int> GetAllDropItemDic(this PlayerSeasonInfoComponent self, int seasonCfgId, int pveLevel, PVELevelDifficulty curPveLevelDifficulty, bool isOnlyShowDropItem)
        {
            Dictionary<string, int> dropItemsNew = DictionaryComponent<string, int>.Create();List<Dictionary<string, int>> allDropItems = self.GetAllDropItems(seasonCfgId, pveLevel, curPveLevelDifficulty, isOnlyShowDropItem);
            foreach (var dropItems in allDropItems)
            {
                foreach (var item in dropItems)
                {
                    string itemCfgId = item.Key;
                    int count = item.Value;
                    if (ET.ItemHelper.ChkIsToken(itemCfgId) == false)
                    {
                        dropItemsNew[itemCfgId] = count;
                        continue;
                    }

                    if (dropItemsNew.ContainsKey(itemCfgId))
                    {
                        dropItemsNew[itemCfgId] += count;
                    }
                    else
                    {
                        dropItemsNew[itemCfgId] = count;
                    }
                }
            }

            return dropItemsNew;
        }

        public static int GetLastUnLockPVELevel(this PlayerSeasonInfoComponent self)
        {
            int pveLevel = 1;

            int count = SeasonChallengeLevelCfgCategory.Instance.GetChallengesCount(self.seasonCfgId);
            for (int i = 1; i < count; i++)
            {
                if(self.pveLevelInfo.TryGetValue(i, out PVELevelDifficulty curPveLevelDifficulty))
                {
                    if (curPveLevelDifficulty >= PVELevelDifficulty.Easy)
                    {
                        pveLevel = i + 1;
                        continue;
                    }
                }
                break;
            }
            return pveLevel;
        }

        public static void UnLockPVELevel(this PlayerSeasonInfoComponent self, int pveLevel, PVELevelDifficulty pveLevelDifficulty)
        {
            if(self.pveLevelInfo.TryGetValue(pveLevel, out PVELevelDifficulty curPveLevelDifficulty) == false)
            {
                self.pveLevelInfo[pveLevel] = pveLevelDifficulty;
            }
            else
            {
                if (curPveLevelDifficulty < pveLevelDifficulty)
                {
                    self.pveLevelInfo[pveLevel] = pveLevelDifficulty;
                }
            }
        }

    }
}