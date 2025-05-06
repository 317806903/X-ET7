namespace ET.Client
{
	[FriendOf(typeof(UIBaseWindow))]
	[AUIEvent(WindowID.WindowID_Platform_Mobile_DlgGameModeArcade)]
	public class DlgGameModeArcadeEventHandler : IAUIEventHandler
	{
		public void OnInitWindowCoreData(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.windowType = UIWindowType.NormalRoot;
		}

		public void OnInitComponent(UIBaseWindow uiBaseWindow)
		{
		  uiBaseWindow.AddComponent<DlgGameModeArcade>().AddComponent<DlgGameModeArcadeViewComponent>();
		}

		public void OnRegisterUIEvent(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.GetComponent<DlgGameModeArcade>().RegisterUIEvent();
		}

		public void OnShowWindow(UIBaseWindow uiBaseWindow, ShowWindowData contextData = null)
		{
			var dlg = uiBaseWindow.GetComponent<DlgGameModeArcade>();
			ET.Client.UIManagerHelper.ShowUIAnimation(dlg.View.EG_OpenAnimationRectTransform, () =>
			{
			});
			dlg.ShowWindow(contextData).Coroutine();
		}

		public void OnHideWindow(UIBaseWindow uiBaseWindow, System.Action finished)
		{
			var dlg = uiBaseWindow.GetComponent<DlgGameModeArcade>();
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
