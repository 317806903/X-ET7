namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgGameModeSetting : Entity, IAwake, IUILogic
	{
		public DlgGameModeSettingViewComponent View { get => this.GetComponent<DlgGameModeSettingViewComponent>(); }

	}
}
