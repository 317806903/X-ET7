namespace ET.Client
{
    //主页设置面板
    [ComponentOf(typeof(UIBaseWindow))]
	public class DlgGameModeSetting : Entity, IAwake, IUILogic
	{
		public DlgGameModeSettingViewComponent View { get => this.GetComponent<DlgGameModeSettingViewComponent>(); }

	}
}
