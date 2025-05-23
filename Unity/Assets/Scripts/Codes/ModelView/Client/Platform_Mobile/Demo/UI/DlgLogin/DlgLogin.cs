﻿namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgLogin : Entity, IAwake, IUILogic, IUIDlg
	{
		public UnityEngine.Transform GetUITransform { get => View.uiTransform; }

		public DlgLoginViewComponent View { get => this.GetComponent<DlgLoginViewComponent>(); }

		public bool IsShowDebugMode;
		public bool IsShowEditorLoginMode;
		public bool IsDebugMode;
		public bool IsEditorLoginMode;

		public bool IsAutoLogining;

		public long Timer;
	}
}
