using System.Collections.Generic;
using UnityEngine;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgBattlePlayerSkill : Entity, IAwake, IUILogic, IUIDlg
	{
		public DlgBattlePlayerSkillViewComponent View { get => this.GetComponent<DlgBattlePlayerSkillViewComponent>(); }
		public long dlgShowTime;

		public Dictionary<int, Scroll_Item_SkillBattleInfo> ScrollItemSkills;
		public long Timer;

		public long unitId;
	}
}
