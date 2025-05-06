namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgZpbTest : Entity, IAwake, IUILogic, IUIDlg
	{
		public UnityEngine.Transform GetUITransform { get => View.uiTransform; }
		public DlgZpbTestViewComponent View { get => this.GetComponent<DlgZpbTestViewComponent>(); }
		public long dlgShowTime;
	}

	public class DlgZpbTest_ShowWindowData : ShowWindowData
	{
		public long unitId;
	}
}
