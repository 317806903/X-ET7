﻿namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgCommonConfirmHighest : Entity, IAwake, IUILogic, IUIDlg
	{
		public UnityEngine.Transform GetUITransform { get => View.uiTransform; }
		public DlgCommonConfirmHighestViewComponent View { get => this.GetComponent<DlgCommonConfirmHighestViewComponent>(); }

		public long dlgShowTime;
	}
}
