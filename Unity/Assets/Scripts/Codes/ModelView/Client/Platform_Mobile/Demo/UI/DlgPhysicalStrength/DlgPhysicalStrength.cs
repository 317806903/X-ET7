using UnityEngine.UI;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgPhysicalStrength : Entity, IAwake, IUILogic, IUIDlg
	{
		public UnityEngine.Transform GetUITransform { get => View.uiTransform; }
		public DlgPhysicalStrengthViewComponent View { get => this.GetComponent<DlgPhysicalStrengthViewComponent>(); }

		public long dlgShowTime;

		public long Timer;
	}
}
