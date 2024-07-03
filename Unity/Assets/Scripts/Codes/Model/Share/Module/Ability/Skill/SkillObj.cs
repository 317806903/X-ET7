using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
    [ChildOf(typeof (SkillComponent))]
    public class SkillObj: Entity, IAwake, IDestroy, IFixedUpdate
    {
        public string skillCfgId;
        public SkillCfg model
        {
            get
            {
                return SkillCfgCategory.Instance.Get(this.skillCfgId);
            }
        }

        /// <summary>
        /// 倒计时的cd
        /// </summary>
        public float cdCountDown;
        public float skillDis;
        public int skillLevel;
        public SkillSlotType skillSlotType;
        public int skillSlotIndex;
    }
}