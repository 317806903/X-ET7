﻿using System.Collections.Generic;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgMail : Entity, IAwake, IUILogic, IUIDlg
	{
		public UnityEngine.Transform GetUITransform { get => View.uiTransform; }
		public DlgMailViewComponent View { get => this.GetComponent<DlgMailViewComponent>(); }

		public long dlgShowTime;

		public Dictionary<int, Scroll_Item_Mail_Inbox> ScrollMailDic;
		public List<(MailInfoComponent, MailStatus)> MailInfoAndStatus;

		public List<long> AllHavaGiftMailInfoId;
		public List<KeyValuePair<string, int>> kvpAllItemCfgNumList;

		public int DropDownIndex;
    }
}
