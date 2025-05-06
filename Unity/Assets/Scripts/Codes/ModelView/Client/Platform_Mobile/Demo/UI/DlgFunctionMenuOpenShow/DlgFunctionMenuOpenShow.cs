using System;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgFunctionMenuOpenShow : Entity, IAwake, IUILogic, IUIDlg
	{
		public UnityEngine.Transform GetUITransform { get => View.uiTransform; }
		public DlgFunctionMenuOpenShowViewComponent View { get => this.GetComponent<DlgFunctionMenuOpenShowViewComponent>(); }

		public string functionMenuCfgId;
		public Action<Scene> finished;
	}

	public class DlgFunctionMenuOpenShow_ShowWindowData : ShowWindowData
	{
		public string functionMenuCfgId;
		public Action<Scene> finished;
	}
}
