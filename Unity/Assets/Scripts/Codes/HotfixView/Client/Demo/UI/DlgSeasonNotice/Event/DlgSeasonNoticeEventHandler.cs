namespace ET.Client
{
	[FriendOf(typeof(UIBaseWindow))]
	[AUIEvent(WindowID.WindowID_SeasonNotice)]
	public class DlgSeasonNoticeEventHandler : IAUIEventHandler
	{
		public void OnInitWindowCoreData(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.windowType = UIWindowType.PopUp;
		}

		public void OnInitComponent(UIBaseWindow uiBaseWindow)
		{
		  uiBaseWindow.AddComponent<DlgSeasonNotice>().AddComponent<DlgSeasonNoticeViewComponent>();
		}

		public void OnRegisterUIEvent(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.GetComponent<DlgSeasonNotice>().RegisterUIEvent();
		}

		public void OnShowWindow(UIBaseWindow uiBaseWindow, ShowWindowData contextData = null)
		{
			uiBaseWindow.GetComponent<DlgSeasonNotice>().ShowWindow(contextData).Coroutine();
		}

		public void OnHideWindow(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.GetComponent<DlgSeasonNotice>().HideWindow();
		}

		public void BeforeUnload(UIBaseWindow uiBaseWindow)
		{
		}

	}
}
