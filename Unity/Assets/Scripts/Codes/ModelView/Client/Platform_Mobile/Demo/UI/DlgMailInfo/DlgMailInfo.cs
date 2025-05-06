using System.Collections.Generic;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgMailInfo : Entity, IAwake, IUILogic, IUIDlg
	{
		public UnityEngine.Transform GetUITransform { get => View.uiTransform; }
		public DlgMailInfoViewComponent View { get => this.GetComponent<DlgMailInfoViewComponent>(); }

		public long dlgShowTime;

        public List<KeyValuePair<string, int>> kvpItemCfgNumList = new List<KeyValuePair<string, int>>();
        public MailInfoComponent mailInfoComponent;
        public MailStatus mailStatus;
        public Dictionary<int, Scroll_Item_ItemShow> ScrollGiftDic = new ();
    }

    public class DlgMailInfo_ShowWindowData : ShowWindowData
    {
		public (MailInfoComponent, MailStatus) mainInfo;
    }

}
