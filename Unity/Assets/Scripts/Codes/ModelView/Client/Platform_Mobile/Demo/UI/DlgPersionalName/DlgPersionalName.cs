using System.Collections.Generic;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgPersionalName : Entity, IAwake, IUILogic, IUIDlg
	{
		public UnityEngine.Transform GetUITransform { get => View.uiTransform; }
		public DlgPersionalNameViewComponent View { get => this.GetComponent<DlgPersionalNameViewComponent>(); }

		public long dlgShowTime;

        public string curName;

        public int NameMaxLength = 15;

        public string oldName;

    }
}
