using System.Collections.Generic;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgMailInfo : Entity, IAwake, IUILogic
	{
		public DlgMailInfoViewComponent View { get => this.GetComponent<DlgMailInfoViewComponent>(); }

        public List<KeyValuePair<string, int>> kvpItemCfgNumList = new List<KeyValuePair<string, int>>();
        public MailInfoComponent mailInfoComponent;
        public MailStatus mailStatus;
        public Dictionary<int, Scroll_Item_Gifts> ScrollGiftDic = new Dictionary<int, Scroll_Item_Gifts>();
    }

    public class DlgMailInfo_ShowWindowData : ShowWindowData
    {
		public (MailInfoComponent, MailStatus) mainInfo;
    }

}
