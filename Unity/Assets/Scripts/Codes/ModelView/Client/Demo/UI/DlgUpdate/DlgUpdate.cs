namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgUpdate : Entity, IAwake, IUILogic
	{

		public DlgUpdateViewComponent View { get => this.GetComponent<DlgUpdateViewComponent>(); }

		 

	}
}
