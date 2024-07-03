using System;
using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;

namespace ET
{
    [FriendOf(typeof(PlayerSeasonInfoComponent))]
    public static class PlayerSeasonInfoComponentSystem
    {
        [ObjectSystem]
        public class PlayerSeasonInfoComponentAwakeSystem : AwakeSystem<PlayerSeasonInfoComponent>
        {
            protected override void Awake(PlayerSeasonInfoComponent self)
            {
                self.seasonBringUpDic = new();
                self.Init();
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
            int rewardSum=0;
            foreach (KeyValuePair<string, int> kvp in self.seasonBringUpDic)
            {
                if(kvp.Value == 0)
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
        public static int  GetReWardBringup(this PlayerSeasonInfoComponent self,string cfg)
        {
            int playerLevel = self.GetSeasonBringUpLevel(cfg);//当前养成的等级
            int rewardSum = 0;//单个养成所有的花费值
            for (int i = (--playerLevel);i >= 0; i--)
            {
                SeasonBringUpCfg seasonBringUpCfg = SeasonBringUpCfgCategory.Instance.GetSeasonBringUpCfg(cfg,i);
                rewardSum += seasonBringUpCfg.Cost;
            }
            return rewardSum;
        }

    }
}