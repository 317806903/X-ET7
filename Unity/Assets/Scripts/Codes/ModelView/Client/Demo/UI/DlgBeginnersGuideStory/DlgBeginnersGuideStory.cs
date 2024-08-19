using System;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgBeginnersGuideStory : Entity, IAwake, IUILogic, IUIDlg
	{
		public DlgBeginnersGuideStoryViewComponent View { get => this.GetComponent<DlgBeginnersGuideStoryViewComponent>(); }

		public int totalNum = 5;
		public int index;
		public Action finishCallBack;
	}

	public class DlgBeginnersGuideStory_ShowWindowData : ShowWindowData
	{
		public Action finishCallBack;
	}
}
