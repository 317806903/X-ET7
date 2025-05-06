namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgPassword : Entity, IAwake, IUILogic, IUIDlg
	{
		public UnityEngine.Transform GetUITransform { get => View.uiTransform; }
		public DlgPasswordViewComponent View { get => this.GetComponent<DlgPasswordViewComponent>(); }

		
    }
}
