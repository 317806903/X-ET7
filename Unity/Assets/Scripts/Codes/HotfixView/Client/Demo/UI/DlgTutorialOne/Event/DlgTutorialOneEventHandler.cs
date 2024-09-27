namespace ET.Client
{
	[FriendOf(typeof(UIBaseWindow))]
	[AUIEvent(WindowID.WindowID_TutorialOne)]
	public class DlgTutorialOneEventHandler : IAUIEventHandler
	{
		public void OnInitWindowCoreData(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.windowType = UIWindowType.PopUp;
		}

		public void OnInitComponent(UIBaseWindow uiBaseWindow)
		{
		  uiBaseWindow.AddComponent<DlgTutorialOne>().AddComponent<DlgTutorialOneViewComponent>();
		}

		public void OnRegisterUIEvent(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.GetComponent<DlgTutorialOne>().RegisterUIEvent();
		}

		public void OnShowWindow(UIBaseWindow uiBaseWindow, ShowWindowData contextData = null)
		{
			uiBaseWindow.GetComponent<DlgTutorialOne>().ShowWindow(contextData).Coroutine();
		}

		public void OnHideWindow(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.GetComponent<DlgTutorialOne>().HideWindow();
		}

		public void BeforeUnload(UIBaseWindow uiBaseWindow)
		{
		}

	}
}
