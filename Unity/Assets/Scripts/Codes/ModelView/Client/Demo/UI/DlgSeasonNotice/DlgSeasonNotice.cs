using System.Collections.Generic;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgSeasonNotice : Entity, IAwake, IUILogic, IUIDlg
	{
		public DlgSeasonNoticeViewComponent View { get => this.GetComponent<DlgSeasonNoticeViewComponent>(); }

		public Dictionary<int, Scroll_Item_ItemShow> ScrollItemReward;
		public Dictionary<int, Scroll_Item_Monsters> ScrollItemMonster;
		public Dictionary<int, Scroll_Item_Frame> ScrollItemFrameIcons;
		public List<string> avatarFrameList;
		public int seasonCfgId;
		public int selectIndex;
	}
}
