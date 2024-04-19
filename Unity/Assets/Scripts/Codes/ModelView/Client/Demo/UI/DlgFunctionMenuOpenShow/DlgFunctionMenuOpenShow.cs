using System;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgFunctionMenuOpenShow : Entity, IAwake, IUILogic
	{
		public DlgFunctionMenuOpenShowViewComponent View { get => this.GetComponent<DlgFunctionMenuOpenShowViewComponent>(); }

		public string functionMenuCfgId;
		public Action finished;
	}

	public class DlgFunctionMenuOpenShow_ShowWindowData : ShowWindowData
	{
		public string functionMenuCfgId;
		public Action finished;
	}
}
