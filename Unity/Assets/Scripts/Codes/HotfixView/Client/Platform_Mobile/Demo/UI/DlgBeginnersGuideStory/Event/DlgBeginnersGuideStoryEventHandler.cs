namespace ET.Client
{
	[FriendOf(typeof(UIBaseWindow))]
	[AUIEvent(WindowID.WindowID_Platform_Mobile_DlgBeginnersGuideStory)]
	public class DlgBeginnersGuideStoryEventHandler : IAUIEventHandler
	{
		public void OnInitWindowCoreData(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.windowType = UIWindowType.PopUpRoot;
		}

		public void OnInitComponent(UIBaseWindow uiBaseWindow)
		{
		  uiBaseWindow.AddComponent<DlgBeginnersGuideStory>().AddComponent<DlgBeginnersGuideStoryViewComponent>();
		}

		public void OnRegisterUIEvent(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.GetComponent<DlgBeginnersGuideStory>().RegisterUIEvent();
		}

		public void OnShowWindow(UIBaseWindow uiBaseWindow, ShowWindowData contextData = null)
		{
			var dlg = uiBaseWindow.GetComponent<DlgBeginnersGuideStory>();
			ET.Client.UIManagerHelper.ShowUIAnimation(dlg.View.EG_OpenAnimationRectTransform, () =>
			{
			});
			dlg.ShowWindow(contextData).Coroutine();
		}

		public void OnHideWindow(UIBaseWindow uiBaseWindow, System.Action finished)
		{
			var dlg = uiBaseWindow.GetComponent<DlgBeginnersGuideStory>();
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
