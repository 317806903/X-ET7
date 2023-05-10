using System.Collections.Generic;

namespace ET.Ability
{
	[ComponentOf(typeof(Unit))]
	public class SkillComponent: Entity, IAwake, IDestroy
	{
		public MultiMap<SkillSlotType, int> skillList;
		public Dictionary<int, float> skillCDs;
		public Dictionary<int, float> skillOrgCDs;
		public Dictionary<int, int> skillLevels;
	}
}