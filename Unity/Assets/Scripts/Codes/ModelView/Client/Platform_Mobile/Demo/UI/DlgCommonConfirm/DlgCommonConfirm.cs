﻿namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgCommonConfirm : Entity, IAwake, IUILogic, IUIDlg
	{
		public UnityEngine.Transform GetUITransform { get => View.uiTransform; }
		public DlgCommonConfirmViewComponent View { get => this.GetComponent<DlgCommonConfirmViewComponent>(); }

		public long dlgShowTime;
	}
}
