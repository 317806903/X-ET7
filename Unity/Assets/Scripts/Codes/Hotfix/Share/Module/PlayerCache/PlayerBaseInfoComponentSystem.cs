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

                self.ChallengeClearLevel = 0;

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
                self.AvatarFrameItemCfgId = "AvatarFrame_None";
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