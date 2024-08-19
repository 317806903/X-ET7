namespace ET.Client
{
    //主页设置面板
    [ComponentOf(typeof(UIBaseWindow))]
	public class DlgGameModeSetting : Entity, IAwake, IUILogic, IUIDlg
	{
		public DlgGameModeSettingViewComponent View { get => this.GetComponent<DlgGameModeSettingViewComponent>(); }

		public long dlgShowTime;
	}
}
