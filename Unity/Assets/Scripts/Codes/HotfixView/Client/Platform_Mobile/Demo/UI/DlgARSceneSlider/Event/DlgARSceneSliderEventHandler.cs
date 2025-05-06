namespace ET.Client
{
	[FriendOf(typeof(UIBaseWindow))]
	[AUIEvent(WindowID.WindowID_Platform_Mobile_DlgARSceneSlider)]
	public class DlgARSceneSliderEventHandler : IAUIEventHandler
	{
		public void OnInitWindowCoreData(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.windowType = UIWindowType.WorldHubRoot;
		}

		public void OnInitComponent(UIBaseWindow uiBaseWindow)
		{
		  uiBaseWindow.AddComponent<DlgARSceneSlider>().AddComponent<DlgARSceneSliderViewComponent>();
		}

		public void OnRegisterUIEvent(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.GetComponent<DlgARSceneSlider>().RegisterUIEvent();
		}

		public void OnShowWindow(UIBaseWindow uiBaseWindow, ShowWindowData contextData = null)
		{
			var dlg = uiBaseWindow.GetComponent<DlgARSceneSlider>();
			ET.Client.UIManagerHelper.ShowUIAnimation(dlg.View.EG_OpenAnimationRectTransform, () =>
			{
			});
			dlg.ShowWindow(contextData).Coroutine();
		}

		public void OnHideWindow(UIBaseWindow uiBaseWindow, System.Action finished)
		{
			var dlg = uiBaseWindow.GetComponent<DlgARSceneSlider>();
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
