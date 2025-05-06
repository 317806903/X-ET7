using System.Collections.Generic;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgPersionalAvatar : Entity, IAwake, IUILogic, IUIDlg
	{
		public UnityEngine.Transform GetUITransform { get => View.uiTransform; }
		public DlgPersionalAvatarViewComponent View { get => this.GetComponent<DlgPersionalAvatarViewComponent>(); }

		public long dlgShowTime;

        public Dictionary<int, Scroll_Item_AvatarIcon> ScrollItemAvatarIcons;

        public Dictionary<int, Scroll_Item_Frame> ScrollItemFrameIcons;

        public List<string> avatarIconList = new List<string>();
        public List<ItemComponent> avatarFrameList= new List<ItemComponent>();

        public int curSelectedAvatarIconIndex;
        public int oldAvatarIconIndex;

        public int lastSelectFrameIndex;

        public string curSelectedFrameIcon;
        public string oldFrameIcon;

    }
}
