namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgPhysicalStrengthTip : Entity, IAwake, IUILogic
	{
		public DlgPhysicalStrengthTipViewComponent View { get => this.GetComponent<DlgPhysicalStrengthTipViewComponent>(); }

	}
}
