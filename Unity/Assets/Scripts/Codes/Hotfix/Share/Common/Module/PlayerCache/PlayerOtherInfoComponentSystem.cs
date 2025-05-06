using System;
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
                self.battleGuideConfigFileName = new();
                self.uiRedDotTypeDic = new();
            }
        }

        public static void Init(this PlayerOtherInfoComponent self)
        {
            if (self.battleGuideStatus == null)
            {
                self.battleGuideStatus = new();
            }
            if (self.battleGuideStepIndex == null)
            {
                self.battleGuideStepIndex = new();
            }
            if (self.battleGuideConfigFileName == null)
            {
                self.battleGuideConfigFileName = new();
            }
            if (self.uiRedDotTypeDic == null)
            {
                self.uiRedDotTypeDic = new();
            }
            if (self.questionnaireStatus == null)
            {
                self.questionnaireStatus = new();
            }
            if (self.battleNoticeStatus == null)
            {
                self.battleNoticeStatus = new();
            }
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

            if (self.battleGuideConfigFileName.Contains(gamePlayBattleLevelCfg.BattleGuideConfigFileName))
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

        public static void SetBattleGuideFinished(this PlayerOtherInfoComponent self, string battleCfgId, string battleGuideConfigFileName)
        {
            self.battleGuideStatus.Add(battleCfgId, true);
            self.battleGuideConfigFileName.Add(battleGuideConfigFileName);
        }

        public static void SetBattleGuideStepFinished(this PlayerOtherInfoComponent self, string battleCfgId, int stepIndex)
        {
            self.battleGuideStepIndex[battleCfgId] = stepIndex;
        }

        public static bool ChkUIRedDotType(this PlayerOtherInfoComponent self, UIRedDotType uiRedDotType)
        {
            if (self.uiRedDotTypeDic.ContainsKey(uiRedDotType))
            {
                return self.uiRedDotTypeDic[uiRedDotType];
            }
            return false;
        }

        public static void SetUIRedDotType(this PlayerOtherInfoComponent self, UIRedDotType uiRedDotType, bool isNeedShow)
        {
            self.uiRedDotTypeDic[uiRedDotType] = isNeedShow;
        }

        public static bool ChkNeedQuestionnaire(this PlayerOtherInfoComponent self, string questionnaireCfgId)
        {
            if (self.questionnaireStatus.Contains(questionnaireCfgId))
            {
                return false;
            }

            return true;
        }

        public static void SetQuestionnaireFinished(this PlayerOtherInfoComponent self, string questionnaireCfgId)
        {
            self.questionnaireStatus.Add(questionnaireCfgId);
        }

        public static bool ChkNeedBattleNotice(this PlayerOtherInfoComponent self, string battleNoticeCfgId)
        {
            if (self.battleNoticeStatus.Contains(battleNoticeCfgId))
            {
                return false;
            }

            return true;
        }

        public static void SetBattleNoticeFinished(this PlayerOtherInfoComponent self, string battleNoticeCfgId)
        {
            self.battleNoticeStatus.Add(battleNoticeCfgId);
        }


    }
}