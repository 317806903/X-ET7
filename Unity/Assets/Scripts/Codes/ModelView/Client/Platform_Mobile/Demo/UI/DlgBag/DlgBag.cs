using System.Collections.Generic;
using SuperScrollView;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgBag : Entity, IAwake, IUILogic, IUIDlg
	{
		public UnityEngine.Transform GetUITransform { get => View.uiTransform; }
		public DlgBagViewComponent View { get => this.GetComponent<DlgBagViewComponent>(); }

		public Dictionary<int, Scroll_Item_ItemShow> ScrollBagItem;
	}
}
