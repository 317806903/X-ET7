using System.Collections.Generic;
using UnityEngine;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgCameraPlayerSkill : Entity, IAwake, IUILogic, IUIDlg
	{
		public DlgCameraPlayerSkillViewComponent View { get => this.GetComponent<DlgCameraPlayerSkillViewComponent>(); }
		public long dlgShowTime;
		public Dictionary<int, Scroll_Item_SkillInfo> skillBattleDeskDic;
		public Dictionary<int, Scroll_Item_SkillInfo> skillCardDic;
		public Scroll_Item_SkillInfo skillMoveItem;
		public Vector2 lastScreenPos;
		public string moveItemCfgId;
		public int replaceIndex;
		public long Timer;
	}
}
