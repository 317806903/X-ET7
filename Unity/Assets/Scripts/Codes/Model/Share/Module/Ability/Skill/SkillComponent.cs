using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
	[ComponentOf(typeof(Unit))]
	public class SkillComponent: Entity, IAwake, IDestroy, IFixedUpdate
	{
		public MultiMapSimple<SkillSlotType, long> skillSlotType2SkillObjs;
		public MultiMapSimple<SkillSlotType, long> skillSlotTypeNone2SkillObjs;
		public Dictionary<SkillGroupType, long> skillGroupType2SkillObjs;
		public Dictionary<string, long> skillCfgId2SkillObjs;
		public Dictionary<long, SkillSlotType> skillObjs2SkillSlotType;
		public List<long> sortPrioritySkillObjs;

		private EntityRef<TimelineObj> _SkillTimeLineObj;
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