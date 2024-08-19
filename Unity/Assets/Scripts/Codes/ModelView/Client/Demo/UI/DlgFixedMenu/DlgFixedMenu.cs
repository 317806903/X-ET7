using System.Collections.Generic;
using UnityEngine;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgFixedMenu : Entity, IAwake, IUILogic, IUIDlg
	{
		public DlgFixedMenuViewComponent View { get => this.GetComponent<DlgFixedMenuViewComponent>(); }

		public int waitFrame = 20;
		public int curFrame = 0;

		public long Timer;
	}
}
