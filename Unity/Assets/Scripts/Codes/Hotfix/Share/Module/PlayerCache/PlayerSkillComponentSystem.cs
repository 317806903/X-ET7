using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;

namespace ET
{
    [FriendOf(typeof(PlayerSkillComponent))]
    public static class PlayerSkillComponentSystem
    {
        [ObjectSystem]
        public class PlayerSkillComponentAwakeSystem : AwakeSystem<PlayerSkillComponent>
        {
            protected override void Awake(PlayerSkillComponent self)
            {
                self.learnSkillCfgList = new();
                self.usingSkillCfgList = new();
                self.newSkillList = new();
            }
        }

        public static void Init(this PlayerSkillComponent self)
        {
        }

        public static long GetPlayerId(this PlayerSkillComponent self)
        {
            return self.GetParent<PlayerDataComponent>().playerId;
        }

        public static void ReplaceUsingSkillCfgId(this PlayerSkillComponent self, int index, string skillCfgId)
        {
            if (self.usingSkillCfgList.Count < GlobalSettingCfgCategory.Instance.MaxBattleSkillNum)
            {
                self.usingSkillCfgList.Add(skillCfgId);
            }
            else if (self.usingSkillCfgList.Count - 1 >= index)
            {
                self.usingSkillCfgList[index] = skillCfgId;
            }
            else
            {
                self.usingSkillCfgList.Add(skillCfgId);
            }
        }

        public static void LearnSkill(this PlayerSkillComponent self, string skillCfgId)
        {
            if (self.learnSkillCfgList.Contains(skillCfgId))
            {
                return;
            }

            self.learnSkillCfgList.Add(skillCfgId);

            if (self.usingSkillCfgList.Count < GlobalSettingCfgCategory.Instance.MaxBattleSkillNum)
            {
                self.usingSkillCfgList.Add(skillCfgId);
            }

        }

        public static void UpdateSkill(this PlayerSkillComponent self, string skillCfgId)
        {
            if (self.learnSkillCfgList.Contains(skillCfgId) == false)
            {
                return;
            }

            string nextSkillCfgId = ET.AbilityConfig.PlayerSkillCfgCategory.Instance.GetNextSkillCfgId(skillCfgId, 1);
            if (string.IsNullOrEmpty(nextSkillCfgId))
            {
                return;
            }

            self.learnSkillCfgList.Remove(skillCfgId);
            self.learnSkillCfgList.Add(nextSkillCfgId);

            for (int i = 0; i < self.usingSkillCfgList.Count; i++)
            {
                if (self.usingSkillCfgList[i] == skillCfgId)
                {
                    self.usingSkillCfgList[i] = nextSkillCfgId;
                    break;
                }
            }
        }

        public static List<string> GetUsingSkillCfgList(this PlayerSkillComponent self)
        {
            return self.usingSkillCfgList;
        }

        public static bool SetUsingSkillCfgList(this PlayerSkillComponent self)
        {
            bool isNeedChg = false;
            if (self.usingSkillCfgList.Count < GlobalSettingCfgCategory.Instance.MaxBattleSkillNum)
            {
                using HashSetComponent<string> skillCfgIdHashSet = HashSetComponent<string>.Create();
                foreach (string skillCfgId in self.usingSkillCfgList)
                {
                    skillCfgIdHashSet.Add(skillCfgId);
                }
                foreach (string learnSkillCfgId in self.learnSkillCfgList)
                {
                    if (skillCfgIdHashSet.Contains(learnSkillCfgId))
                    {
                        continue;
                    }

                    if (self.usingSkillCfgList.Count >= GlobalSettingCfgCategory.Instance.MaxBattleSkillNum)
                    {
                        continue;
                    }
                    self.usingSkillCfgList.Add(learnSkillCfgId);
                    isNeedChg = true;
                }
            }

            return isNeedChg;
        }

        public static List<(string skillCfgId, bool isLearn)> GetAllSkillCfgList(this PlayerSkillComponent self)
        {
            List<(string skillCfgId, bool isLearn)> skillCfgList = ListComponent<(string skillCfgId, bool isLearn)>.Create();

            using HashSetComponent<string> skillCfgIdHashSet = HashSetComponent<string>.Create();
            foreach (string skillCfgId in self.usingSkillCfgList)
            {
                skillCfgIdHashSet.Add(skillCfgId);
            }
            HashSet<string> baseSkillCfgIdList = ET.AbilityConfig.PlayerSkillCfgCategory.Instance.GetAllBaseSkillCfgIdList();
            foreach (string learnSkillCfgId in self.learnSkillCfgList)
            {
                string baseSkillCfgId = ET.AbilityConfig.PlayerSkillCfgCategory.Instance.GetBaseSkillCfgId(learnSkillCfgId);
                baseSkillCfgIdList.Remove(baseSkillCfgId);
                if (skillCfgIdHashSet.Contains(learnSkillCfgId) == false)
                {
                    skillCfgList.Add((learnSkillCfgId, true));
                }
            }
            foreach (string baseSkillCfgId in baseSkillCfgIdList)
            {
                skillCfgList.Add((baseSkillCfgId, false));
            }

            return skillCfgList;
        }

        public static void AddNewSkillRecord(this PlayerSkillComponent self, string skillCfgId)
        {
            if (self.learnSkillCfgList.Contains(skillCfgId) == false)
            {
                self.newSkillList.Add(skillCfgId);
            }
        }

        public static void RemoveNewSkillRecord(this PlayerSkillComponent self, string skillCfgId)
        {
            self.newSkillList.Remove(skillCfgId);
        }

        public static bool ChkIsNewSkill(this PlayerSkillComponent self, string skillCfgId)
        {
            return self.newSkillList.Contains(skillCfgId);
        }

        public static bool ChkIsNewSkill(this PlayerSkillComponent self)
        {
            return self.newSkillList.Count > 0;
        }

    }
}