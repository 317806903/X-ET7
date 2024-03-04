using System.Collections.Generic;
using UnityEngine;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgCommonTipNode : Entity, IAwake, IUILogic
	{
		public DlgCommonTipNodeViewComponent View { get => this.GetComponent<DlgCommonTipNodeViewComponent>(); }

		public Transform transTipNode;
		public Stack<string> tips = new();
		public bool isDoing;
	}
}
