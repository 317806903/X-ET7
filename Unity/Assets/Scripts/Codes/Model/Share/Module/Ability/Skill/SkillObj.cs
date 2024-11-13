using System.Collections.Generic;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;

namespace ET.Ability
{
    [ChildOf(typeof (SkillComponent))]
    public class SkillObj: Entity, IAwake, IDestroy, IFixedUpdate, ISerializeToEntity
    {
        public string skillCfgId;
        [BsonIgnore]
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
        public float cdTotal;

        public float curEnergyNum;

        public float skillDis;
        public int skillLevel;
        public SkillSlotType skillSlotType;
        public int skillSlotIndex;
    }
}