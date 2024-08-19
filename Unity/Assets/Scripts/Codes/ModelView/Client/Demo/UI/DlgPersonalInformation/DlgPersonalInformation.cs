using System.Collections.Generic;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgPersonalInformation : Entity, IAwake, IUILogic, IUIDlg
	{
		public DlgPersonalInformationViewComponent View { get => this.GetComponent<DlgPersonalInformationViewComponent>(); }

		public long dlgShowTime;
	}
}
