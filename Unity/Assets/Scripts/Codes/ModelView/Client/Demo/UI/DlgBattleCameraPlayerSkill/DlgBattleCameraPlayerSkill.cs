using System.Collections.Generic;
using UnityEngine;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgBattleCameraPlayerSkill : Entity, IAwake, IUILogic, IUIDlg
	{
		public DlgBattleCameraPlayerSkillViewComponent View { get => this.GetComponent<DlgBattleCameraPlayerSkillViewComponent>(); }
		public long dlgShowTime;
		public Dictionary<int, Scroll_Item_SkillBattleInfo> ScrollItemSkills;

		public long Timer;

		public long unitId;
	}
}
