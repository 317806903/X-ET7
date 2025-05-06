namespace ET.Client
{
	[FriendOf(typeof(UIBaseWindow))]
	[AUIEvent(WindowID.WindowID_Platform_Mobile_DlgARHall)]
	public class DlgARHallEventHandler : IAUIEventHandler
	{

		public void OnInitWindowCoreData(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.windowType = UIWindowType.NormalRoot;
		}

		public void OnInitComponent(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.AddComponent<DlgARHall>().AddComponent<DlgARHallViewComponent>();
		}

		public void OnRegisterUIEvent(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.GetComponent<DlgARHall>().RegisterUIEvent();
		}

		public void OnShowWindow(UIBaseWindow uiBaseWindow, ShowWindowData contextData = null)
		{
			var dlg = uiBaseWindow.GetComponent<DlgARHall>();
			ET.Client.UIManagerHelper.ShowUIAnimation(dlg.View.EG_OpenAnimationRectTransform, () =>
			{
			});
			dlg.ShowWindow(contextData).Coroutine();
		}

		public void OnHideWindow(UIBaseWindow uiBaseWindow, System.Action finished)
		{
			var dlg = uiBaseWindow.GetComponent<DlgARHall>();
			dlg.HideWindow();
			ET.Client.UIManagerHelper.ShowUIAnimation(dlg.View.EG_CloseAnimationRectTransform, () =>
			{
				finished?.Invoke();
			});
		}

		public void BeforeUnload(UIBaseWindow uiBaseWindow)
		{
		}

	}
}
