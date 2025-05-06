namespace ET.Client
{
	[FriendOf(typeof(UIBaseWindow))]
	[AUIEvent(WindowID.WindowID_Platform_Mobile_DlgUpdate)]
	public class DlgUpdateEventHandler : IAUIEventHandler
	{

		public void OnInitWindowCoreData(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.windowType = UIWindowType.NormalRoot;
		}

		public void OnInitComponent(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.AddComponent<DlgUpdate>().AddComponent<DlgUpdateViewComponent>();
		}

		public void OnRegisterUIEvent(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.GetComponent<DlgUpdate>().RegisterUIEvent();
		}

		public void OnShowWindow(UIBaseWindow uiBaseWindow, ShowWindowData contextData = null)
		{
			var dlg = uiBaseWindow.GetComponent<DlgUpdate>();
			ET.Client.UIManagerHelper.ShowUIAnimation(dlg.View.EG_OpenAnimationRectTransform, () =>
			{
			});
			dlg.ShowWindow(contextData).Coroutine();
		}

		public void OnHideWindow(UIBaseWindow uiBaseWindow, System.Action finished)
		{
			var dlg = uiBaseWindow.GetComponent<DlgUpdate>();
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
