using System.Collections.Generic;
using UnityEngine;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgCommonTip : Entity, IAwake, IUILogic, IUIDlg
	{

		public DlgCommonTipViewComponent View { get => this.GetComponent<DlgCommonTipViewComponent>();}

		public Transform transTipNode;
		public Queue<string> tips = new();
		public HashSet<GameObject> tipShowGoList = new();
		public bool isDoing;
	}
}
