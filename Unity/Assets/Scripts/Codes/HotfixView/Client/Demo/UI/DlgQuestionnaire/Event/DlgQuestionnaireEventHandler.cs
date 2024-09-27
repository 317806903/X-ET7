namespace ET.Client
{
	[FriendOf(typeof(UIBaseWindow))]
	[AUIEvent(WindowID.WindowID_Questionnaire)]
	public class DlgQuestionnaireEventHandler : IAUIEventHandler
	{
		public void OnInitWindowCoreData(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.windowType = UIWindowType.PopUp;
		}

		public void OnInitComponent(UIBaseWindow uiBaseWindow)
		{
		  uiBaseWindow.AddComponent<DlgQuestionnaire>().AddComponent<DlgQuestionnaireViewComponent>();
		}

		public void OnRegisterUIEvent(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.GetComponent<DlgQuestionnaire>().RegisterUIEvent();
		}

		public void OnShowWindow(UIBaseWindow uiBaseWindow, ShowWindowData contextData = null)
		{
			uiBaseWindow.GetComponent<DlgQuestionnaire>().ShowWindow(contextData).Coroutine();
		}

		public void OnHideWindow(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.GetComponent<DlgQuestionnaire>().HideWindow();
		}

		public void BeforeUnload(UIBaseWindow uiBaseWindow)
		{
		}

	}
}
