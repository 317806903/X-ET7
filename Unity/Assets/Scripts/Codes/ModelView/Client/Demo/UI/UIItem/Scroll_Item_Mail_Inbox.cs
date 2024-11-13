
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	public partial class Scroll_Item_Mail_Inbox
	{
		public Dictionary<int, Scroll_Item_ItemShow> ScrollGiftDic = new ();
		public MailInfoComponent mailInfoComponent = new MailInfoComponent();
		public MailStatus mailStatus;
		public List<KeyValuePair<string, int>> kvpItemCfgNumList = new List<KeyValuePair<string, int>>();
	}
}
