using System.Collections.Generic;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgBattleTowerNotice : Entity, IAwake, IUILogic, IUIDlg
	{
		public UnityEngine.Transform GetUITransform { get => View.uiTransform; }
		public DlgBattleTowerNoticeViewComponent View { get => this.GetComponent<DlgBattleTowerNoticeViewComponent>(); }
		public long dlgShowTime;

		public Dictionary<int, Scroll_Item_BattleNotice> ScrollItem;
		public List<string> tutorialCfgIdList = new();
	}
}
