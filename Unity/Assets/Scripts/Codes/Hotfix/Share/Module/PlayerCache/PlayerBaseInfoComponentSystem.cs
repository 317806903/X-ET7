using System.Collections.Generic;
using System.Linq;

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

    }
}