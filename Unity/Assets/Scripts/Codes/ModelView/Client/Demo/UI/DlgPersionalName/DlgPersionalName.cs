using System.Collections.Generic;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgPersionalName : Entity, IAwake, IUILogic
	{
		public DlgPersionalNameViewComponent View { get => this.GetComponent<DlgPersionalNameViewComponent>(); }

        public string curName;

        public int NameMaxLength = 15;

        public string oldName;

    }
}
