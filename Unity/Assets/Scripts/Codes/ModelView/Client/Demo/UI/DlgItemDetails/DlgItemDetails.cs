using System.Collections.Generic;
using SuperScrollView;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgItemDetails : Entity, IAwake, IUILogic, IUIDlg
	{
		public DlgItemDetailsViewComponent View { get => this.GetComponent<DlgItemDetailsViewComponent>(); }

		public long dlgShowTime;

		public Dictionary<int, Scroll_Item_TowerIcon> ScrollTowerIcon;

		public string baseItemCfgId;
		public int curItemIndex;
		public string curItemCfgId;
	}
}
