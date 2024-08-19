using System.Collections.Generic;
using UnityEngine;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgCommonTipTopShow : Entity, IAwake, IUILogic, IUIDlg
	{
		public DlgCommonTipTopShowViewComponent View { get => this.GetComponent<DlgCommonTipTopShowViewComponent>(); }

		public Transform transTipNode;
		public Stack<string> tips = new();
		public bool isDoing;
	}
}
