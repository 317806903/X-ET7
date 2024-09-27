using System.Collections.Generic;
using UnityEngine;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgFixedMenu : Entity, IAwake, IUILogic, IUIDlg
	{
		public DlgFixedMenuViewComponent View { get => this.GetComponent<DlgFixedMenuViewComponent>(); }

		public int waitFrame = 15;
		public int curFrame = 0;

		public int waitChkUpdateFrame = 300;
		public int curChkUpdateFrame = 0;

		public bool IsShowCoinList;
		public long Timer;
	}
}
