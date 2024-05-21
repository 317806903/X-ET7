using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;

namespace ET
{
    [FriendOf(typeof(PlayerOtherInfoComponent))]
    public static class PlayerOtherInfoComponentSystem
    {
        [ObjectSystem]
        public class PlayerOtherInfoComponentAwakeSystem : AwakeSystem<PlayerOtherInfoComponent>
        {
            protected override void Awake(PlayerOtherInfoComponent self)
            {
                self.battleGuideStatus = new();
                self.Init();
            }
        }

        public static void Init(this PlayerOtherInfoComponent self)
        {
        }

        public static long GetPlayerId(this PlayerOtherInfoComponent self)
        {
            return self.GetParent<PlayerDataComponent>().playerId;
        }

        public static (bool, string) ChkNeedBattleGuide(this PlayerOtherInfoComponent self, string battleCfgId)
        {
            if (self.battleGuideStatus.ContainsKey(battleCfgId))
            {
                return (false, "");
            }

            GamePlayBattleLevelCfg gamePlayBattleLevelCfg = GamePlayBattleLevelCfgCategory.Instance.Get(battleCfgId);
            if (gamePlayBattleLevelCfg == null)
            {
                return (false, "");
            }

            if (string.IsNullOrEmpty(gamePlayBattleLevelCfg.BattleGuideConfigFileName))
            {
                return (false, "");
            }

            return (true, gamePlayBattleLevelCfg.BattleGuideConfigFileName);
        }

        public static void SetBattleGuideFinished(this PlayerOtherInfoComponent self, string battleCfgId)
        {
            self.battleGuideStatus.Add(battleCfgId, true);
        }
    }
}