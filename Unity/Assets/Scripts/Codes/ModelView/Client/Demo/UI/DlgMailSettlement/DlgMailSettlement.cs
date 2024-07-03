using System.Collections.Generic;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgMailSettlement : Entity, IAwake, IUILogic
	{
		public DlgMailSettlementViewComponent View { get => this.GetComponent<DlgMailSettlementViewComponent>(); }

        public List<KeyValuePair<string, int>> kvpItemCfgNumList;

        public Dictionary<int, Scroll_Item_Gifts> ScrollItemGiftDic;

    }

    public class DlgMailSettlement_ShowWindowData : ShowWindowData
    {
        public List<KeyValuePair<string, int>> kvpItemCfgNumList = new List<KeyValuePair<string, int>>();
    }

}
