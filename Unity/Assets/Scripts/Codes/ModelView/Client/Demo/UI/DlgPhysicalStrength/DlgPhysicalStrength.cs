using UnityEngine.UI;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgPhysicalStrength : Entity, IAwake, IUILogic, IUIDlg
	{
		public DlgPhysicalStrengthViewComponent View { get => this.GetComponent<DlgPhysicalStrengthViewComponent>(); }

		public long dlgShowTime;

		public long Timer;
	}
}
