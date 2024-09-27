namespace ET.Client
{
	[FriendOf(typeof(UIBaseWindow))]
	[AUIEvent(WindowID.WindowID_ItemDetails)]
	public class DlgItemDetailsEventHandler : IAUIEventHandler
	{
		public void OnInitWindowCoreData(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.windowType = UIWindowType.PopUp;
		}

		public void OnInitComponent(UIBaseWindow uiBaseWindow)
		{
		  uiBaseWindow.AddComponent<DlgItemDetails>().AddComponent<DlgItemDetailsViewComponent>();
		}

		public void OnRegisterUIEvent(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.GetComponent<DlgItemDetails>().RegisterUIEvent();
		}

		public void OnShowWindow(UIBaseWindow uiBaseWindow, ShowWindowData contextData = null)
		{
			uiBaseWindow.GetComponent<DlgItemDetails>().ShowWindow(contextData).Coroutine();
		}

		public void OnHideWindow(UIBaseWindow uiBaseWindow)
		{
		}

		public void BeforeUnload(UIBaseWindow uiBaseWindow)
		{
		}

	}
}
