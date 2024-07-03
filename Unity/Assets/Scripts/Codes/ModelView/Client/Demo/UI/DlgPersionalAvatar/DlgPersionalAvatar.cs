using System.Collections.Generic;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgPersionalAvatar : Entity, IAwake, IUILogic
	{
		public DlgPersionalAvatarViewComponent View { get => this.GetComponent<DlgPersionalAvatarViewComponent>(); }

        public Dictionary<int, Scroll_Item_AvatarIcon> ScrollItemAvatarIcons;

        public Dictionary<int, Scroll_Item_Frame> ScrollItemFrameIcons;

        public List<string> avatarIconList = new List<string>();
        public List<ItemComponent> avatarFrameList= new List<ItemComponent>();

        public int curSelectedAvatarIconIndex;
        public int oldAvatarIconIndex;

        public string curSelectedFrameIcon;
        public string oldFrameIcon;

    }
}
