namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgBattleTowerBegin : Entity, IAwake, IUILogic, IUIDlg
	{
		public UnityEngine.Transform GetUITransform { get => View.uiTransform; }
		public DlgBattleTowerBeginViewComponent View { get => this.GetComponent<DlgBattleTowerBeginViewComponent>(); }
	}
}
