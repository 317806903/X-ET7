using System.Collections.Generic;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgBattleTowerEnd : Entity, IAwake, IUILogic, IUIDlg
	{
		public UnityEngine.Transform GetUITransform { get => View.uiTransform; }
		public DlgBattleTowerEndViewComponent View { get => this.GetComponent<DlgBattleTowerEndViewComponent>(); }
		public Dictionary<int, Scroll_Item_ItemShow> ScrollItemReward;
	}
}
