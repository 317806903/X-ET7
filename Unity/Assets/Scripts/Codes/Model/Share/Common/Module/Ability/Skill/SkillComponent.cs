using System.Collections.Generic;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;

namespace ET.Ability
{
	[ComponentOf(typeof(Unit))]
	public class SkillComponent: Entity, IAwake, IDestroy, IFixedUpdate, ITransferClient
	{
		[BsonIgnore]
		public MultiMapSimple<SkillSlotType, long> skillSlotType2SkillObjs;
		[BsonIgnore]
		public MultiMapSimple<SkillSlotType, long> skillSlotTypeNone2SkillObjs;
		[BsonIgnore]
		public Dictionary<SkillGroupType, long> skillGroupType2SkillObjs;

		public Dictionary<string, long> skillCfgId2SkillObjs;
		[BsonIgnore]
		public Dictionary<long, SkillSlotType> skillObjs2SkillSlotType;
		[BsonIgnore]
		public List<long> sortPrioritySkillObjs;
		[BsonIgnore]
		public List<long> sortPrioritySkillObjsWhenBlock;

		public List<long> manualSkillObjs;

		public float curCommonEnergyNum;

		[BsonIgnore]
		private EntityRef<TimelineObj> _SkillTimeLineObj;
		[BsonIgnore]
		public TimelineObj CurSkillTimelineObj
		{
			get
			{
				return this._SkillTimeLineObj;
			}
			set
			{
				this._SkillTimeLineObj = value;
			}
		}

	}
}