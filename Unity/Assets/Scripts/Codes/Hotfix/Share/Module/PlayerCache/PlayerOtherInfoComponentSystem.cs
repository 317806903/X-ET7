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
                self.battleGuideStepIndex = new();
                self.uiRedDotTypes = new();
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

        public static int GetBattleGuideStepIndex(this PlayerOtherInfoComponent self, string battleCfgId)
        {
            if (self.battleGuideStepIndex.TryGetValue(battleCfgId, out var stepIndex))
            {
                return stepIndex;
            }
            return 0;
        }

        public static void SetBattleGuideFinished(this PlayerOtherInfoComponent self, string battleCfgId)
        {
            self.battleGuideStatus.Add(battleCfgId, true);
        }

        public static void SetBattleGuideStepFinished(this PlayerOtherInfoComponent self, string battleCfgId, int stepIndex)
        {
            self.battleGuideStepIndex[battleCfgId] = stepIndex;
        }

        public static bool ChkUIRedDotType(this PlayerOtherInfoComponent self, UIRedDotType uiRedDotType)
        {
            if (self.uiRedDotTypes.Contains(uiRedDotType))
            {
                return true;
            }
            return false;
        }

        public static void SetUIRedDotType(this PlayerOtherInfoComponent self, UIRedDotType uiRedDotType, bool isNeedShow)
        {
            if (isNeedShow)
            {
                self.uiRedDotTypes.Add(uiRedDotType);
            }
            else
            {
                self.uiRedDotTypes.Remove(uiRedDotType);
            }
        }

    }
}