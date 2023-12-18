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
                self.PlayerName = self.GetPlayerId().ToString();
                self.IconIndex = 0;
                self.EndlessChallengeScore = 0;

                self.ChallengeClearLevel = 0;

                self.physicalStrength = GlobalSettingCfgCategory.Instance.InitialPhysicalStrength;
                self.nextRecoverTime = TimeHelper.ServerNow() + (GlobalSettingCfgCategory.Instance.RecoverTimeOfPhysicalStrength * 1000);
            }
        }

        public static long GetPlayerId(this PlayerBaseInfoComponent self)
        {
            return self.GetParent<PlayerDataComponent>().playerId;
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

        public static int GetChallengeClearLevel(this PlayerBaseInfoComponent self)
        {
            return self.ChallengeClearLevel;
        }

        public static void SetChallengeClearLevel(this PlayerBaseInfoComponent self, int level)
        {
            self.ChallengeClearLevel = level;
        }
        
        public static void UpdatePhysicalStrength(this PlayerBaseInfoComponent self)
        {
            if (TimeHelper.ServerNow() < self.nextRecoverTime)
            {
                return;
            }
            int recoverTime = GlobalSettingCfgCategory.Instance.RecoverTimeOfPhysicalStrength * 1000;
            int recoverPhysiacalStrength = GlobalSettingCfgCategory.Instance.RecoverIncreaseOfPhysicalStrength;
            int maxPysicalStrength = GlobalSettingCfgCategory.Instance.UpperLimitOfPhysicalStrength;
            
            if (self.physicalStrength >= maxPysicalStrength)
            {
                self.physicalStrength = maxPysicalStrength;
                self.nextRecoverTime = TimeHelper.ServerNow() + recoverTime;
                return;
            }
            self.physicalStrength += ((int)(TimeHelper.ServerNow() - self.nextRecoverTime) / recoverTime + 1) * recoverPhysiacalStrength;
            if (self.physicalStrength > maxPysicalStrength)
            {
                self.physicalStrength = maxPysicalStrength;
                self.nextRecoverTime = TimeHelper.ServerNow() + recoverTime;
            }
            else
            {
                self.nextRecoverTime = TimeHelper.ServerNow() + recoverTime - (TimeHelper.ServerNow() - self.nextRecoverTime) % recoverTime;
            }
        }

        public static int GetRevoerLeftTime(this PlayerBaseInfoComponent self)
        {
            self.UpdatePhysicalStrength();
            if (self.physicalStrength == GlobalSettingCfgCategory.Instance.UpperLimitOfPhysicalStrength)
                return 0;
            return (int)((self.nextRecoverTime - TimeHelper.ServerNow()) / 1000);
        }

        public static int GetPhysicalStrength(this PlayerBaseInfoComponent self)
        {
            self.UpdatePhysicalStrength();
            return self.physicalStrength;
        }

        public static bool ChkPhysicalStrength(this PlayerBaseInfoComponent self, int chgValue)
        {
            if (self.physicalStrength + chgValue < 0)
            {
                return false;
            }
            return true;
        }

        public static void ChgPhysicalStrength(this PlayerBaseInfoComponent self, int chgValue)
        {
            self.physicalStrength += chgValue;
            self.UpdatePhysicalStrength();
        }
        
    }
}