﻿using System.Collections.Generic;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgMailSettlement : Entity, IAwake, IUILogic, IUIDlg
	{
		public UnityEngine.Transform GetUITransform { get => View.uiTransform; }
		public DlgMailSettlementViewComponent View { get => this.GetComponent<DlgMailSettlementViewComponent>(); }

		public long dlgShowTime;

        public List<KeyValuePair<string, int>> kvpItemCfgNumList;

        public Dictionary<int, Scroll_Item_ItemShow> ScrollItemGiftDic;

    }

    public class DlgMailSettlement_ShowWindowData : ShowWindowData
    {
        public List<KeyValuePair<string, int>> kvpItemCfgNumList = new List<KeyValuePair<string, int>>();
    }

}
