using System.Collections.Generic;
using UnityEngine;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgCommonTipTopShow : Entity, IAwake, IUILogic, IUIDlg
	{
		public DlgCommonTipTopShowViewComponent View { get => this.GetComponent<DlgCommonTipTopShowViewComponent>(); }

		public Transform transTipNode;
		public Queue<string> tips = new();
		public HashSet<GameObject> tipShowGoList = new();
		public bool isDoing;
	}
}
