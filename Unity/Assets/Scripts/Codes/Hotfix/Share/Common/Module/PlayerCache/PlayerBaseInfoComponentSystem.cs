using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;

namespace ET
{
    [FriendOf(typeof(PlayerBaseInfoComponent))]
    public static class PlayerBaseInfoComponentSystem
    {
        [ObjectSystem]
        public class PlayerBaseInfoComponentAwakeSystem : AwakeSystem<PlayerBaseInfoComponent>
        {
            protected override void Awake(PlayerBaseInfoComponent self)
            {
                self.PlayerName = $"player{RandomGenerator.RandomNumber(100000, 1000000)}";
                self.IconIndex = 0;
                self.EndlessChallengeScore = 0;

                self.pveLevelInfo = new();

                self.physicalStrength = GlobalSettingCfgCategory.Instance.InitialPhysicalStrength;
                self.nextRecoverPhysicalTime = TimeHelper.ServerNow() + (GlobalSettingCfgCategory.Instance.RecoverTimeOfPhysicalStrength * 1000);

                self.BindLoginType = LoginType.Editor;
                self.BindEmail = "";
            }
        }

        public static void Init(this PlayerBaseInfoComponent self)
        {
            if (string.IsNullOrEmpty(self.AvatarFrameItemCfgId))
            {
                self.AvatarFrameItemCfgId = ET.ItemHelper.GetAvatarFrameNoneCfgId();
                self.SetDataCacheAutoWrite();
            }
        }

        public static long GetPlayerId(this PlayerBaseInfoComponent self)
        {
            return self.Id;
        }

        public static string GetPlayerName(this PlayerBaseInfoComponent self)
        {
            return self.PlayerName;
        }

        public static void SetPlayerName(this PlayerBaseInfoComponent self, string playerName)
        {
            self.PlayerName = playerName;
        }

        public static int GetIconIndex(this PlayerBaseInfoComponent self)
        {
            return self.IconIndex;
        }

        public static void SetIconIndex(this PlayerBaseInfoComponent self, int iconIndex)
        {
            self.IconIndex = iconIndex;
        }

        public static int GetEndlessChallengeScore(this PlayerBaseInfoComponent self)
        {
            return self.EndlessChallengeScore;
        }

        public static void SetEndlessChallengeScore(this PlayerBaseInfoComponent self, int endlessChallengeScore)
        {
            self.EndlessChallengeScore = endlessChallengeScore;
        }

        public static bool ChkIsLockPVELevel(this PlayerBaseInfoComponent self, int pveLevel)
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

        public static bool ChkIsPassPVELevel(this PlayerBaseInfoComponent self, int pveLevel, PVELevelDifficulty pveLevelDifficulty)
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

        public static List<Dictionary<string, int>> GetAllDropItems(this PlayerBaseInfoComponent self, int pveLevel, PVELevelDifficulty curPveLevelDifficulty, bool isOnlyShowDropItem)
        {
            List<Dictionary<string, int>> allDropItems = ListComponent<Dictionary<string, int>>.Create();
            foreach (PVELevelDifficulty pveLevelDifficulty in System.Enum.GetValues(typeof(PVELevelDifficulty)))
            {
                if (pveLevelDifficulty <= curPveLevelDifficulty)
                {
                    ChallengeLevelCfg challengeLevelCfg = TowerDefense_ChallengeLevelCfgCategory.Instance.GetChallengeByIndex(pveLevel, pveLevelDifficulty);

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

        public static Dictionary<string, int> GetAllDropItemDic(this PlayerBaseInfoComponent self, int pveLevel, PVELevelDifficulty curPveLevelDifficulty, bool isOnlyShowDropItem)
        {
            Dictionary<string, int> dropItemsNew = DictionaryComponent<string, int>.Create();List<Dictionary<string, int>> allDropItems = self.GetAllDropItems(pveLevel, curPveLevelDifficulty, isOnlyShowDropItem);
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

        public static int GetLastUnLockPVELevel(this PlayerBaseInfoComponent self)
        {
            int pveLevel = 1;

            int count = TowerDefense_ChallengeLevelCfgCategory.Instance.GetChallengesCount();
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

        public static void UnLockPVELevel(this PlayerBaseInfoComponent self, int pveLevel, PVELevelDifficulty pveLevelDifficulty)
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

        public static void UpdatePhysicalStrength(this PlayerBaseInfoComponent self)
        {
            if (TimeHelper.ServerNow() < self.nextRecoverPhysicalTime)
            {
                return;
            }

            long recoverTime = GlobalSettingCfgCategory.Instance.RecoverTimeOfPhysicalStrength * 1000;
            int recoverPhysiacalStrength = GlobalSettingCfgCategory.Instance.RecoverIncreaseOfPhysicalStrength;
            int maxPysicalStrength = GlobalSettingCfgCategory.Instance.UpperLimitOfPhysicalStrength;

            // if (self.physicalStrength >= maxPysicalStrength)
            // {
            //     self.physicalStrength = maxPysicalStrength;
            //     self.nextRecoverTime = TimeHelper.ServerNow() + recoverTime;
            //     return;
            // }

            long elapsedTime = TimeHelper.ServerNow() - self.nextRecoverPhysicalTime;
            var create = ((float)elapsedTime / recoverTime + 1) * recoverPhysiacalStrength;
            if (create > maxPysicalStrength)
            {
                self.physicalStrength = maxPysicalStrength;
                self.nextRecoverPhysicalTime = TimeHelper.ServerNow() + recoverTime;
                return;
            }
            self.physicalStrength += (int)create;
            if (self.physicalStrength > maxPysicalStrength)
            {
                self.physicalStrength = maxPysicalStrength;
                self.nextRecoverPhysicalTime = TimeHelper.ServerNow() + recoverTime;
            }
            else
            {
                self.nextRecoverPhysicalTime = TimeHelper.ServerNow() + recoverTime - elapsedTime % recoverTime;
            }
        }

        public static int GetRecoverLeftTime(this PlayerBaseInfoComponent self)
        {
            //self.UpdatePhysicalStrength();
            if (self.physicalStrength == GlobalSettingCfgCategory.Instance.UpperLimitOfPhysicalStrength)
                return 0;
            return (int)((self.nextRecoverPhysicalTime - TimeHelper.ServerNow()) / 1000);
        }

        public static int GetPhysicalStrength(this PlayerBaseInfoComponent self)
        {
            self.UpdatePhysicalStrength();
            return self.physicalStrength;
        }

        public static bool _ChkPhysicalStrength(this PlayerBaseInfoComponent self, int chgValue)
        {
            if (self.GetPhysicalStrength() + chgValue < 0)
            {
                Log.Error($"Lack of physical strength, needPhysicalStrength:{chgValue}, curPhysicalStrength:{self.physicalStrength}");
                return false;
            }
            return true;
        }

        public static void ChgPhysicalStrength(this PlayerBaseInfoComponent self, int chgValue)
        {
            self.UpdatePhysicalStrength();
            self.physicalStrength += chgValue;
            if (self.physicalStrength < 0)
            {
                self.physicalStrength = 0;
            }
            int maxPysicalStrength = GlobalSettingCfgCategory.Instance.UpperLimitOfPhysicalStrength;
            if (self.physicalStrength > maxPysicalStrength)
            {
                self.physicalStrength = maxPysicalStrength;
            }
        }

        public static LoginType GetBindLoginType(this PlayerBaseInfoComponent self)
        {
            return self.BindLoginType;
        }

        public static void SetBindLoginType(this PlayerBaseInfoComponent self, LoginType loginType)
        {
            self.BindLoginType = loginType;
        }

        public static string GetBindEmail(this PlayerBaseInfoComponent self)
        {
            return self.BindEmail;
        }

        public static void SetBindEmail(this PlayerBaseInfoComponent self, string email)
        {
            self.BindEmail = email;
        }
    }
}