using System.Collections.Generic;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgPersonalInformation : Entity, IAwake, IUILogic
	{
		public DlgPersonalInformationViewComponent View { get => this.GetComponent<DlgPersonalInformationViewComponent>(); }
		
		public Dictionary<int, Scroll_Item_AvatarIcon> ScrollItemAvatarIcons;

		public int curSelectedIconIndex;

		public int NameMaxLength = 20;

		public  int tempSeletedIconIndex;
	}
}
