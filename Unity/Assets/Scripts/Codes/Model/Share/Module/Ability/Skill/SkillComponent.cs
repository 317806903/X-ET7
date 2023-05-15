using System.Collections.Generic;

namespace ET.Ability
{
	[ComponentOf(typeof(Unit))]
	public class SkillComponent: Entity, IAwake, IDestroy
	{
		public MultiMap<SkillSlotType, string> skillList;
		public Dictionary<string, float> skillCDs;
		public Dictionary<string, float> skillOrgCDs;
		public Dictionary<string, int> skillLevels;
	}
}