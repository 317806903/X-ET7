using System.Collections.Generic;
using SuperScrollView;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgDetails : Entity, IAwake, IUILogic, IUIDlg
	{
		public DlgDetailsViewComponent View { get => this.GetComponent<DlgDetailsViewComponent>(); }

		public long dlgShowTime;

		public Dictionary<int, Scroll_Item_TowerIcon> ScrollTowerIcon;

		public string curItemCfgId;
	}
}
