namespace ET.Client
{
	[FriendOf(typeof(UIBaseWindow))]
	[AUIEvent(WindowID.WindowID_Platform_Mobile_DlgRankPowerupSeason)]
	public class DlgRankPowerupSeasonEventHandler : IAUIEventHandler
	{
		public void OnInitWindowCoreData(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.windowType = UIWindowType.NormalRoot;
		}

		public void OnInitComponent(UIBaseWindow uiBaseWindow)
		{
		  uiBaseWindow.AddComponent<DlgRankPowerupSeason>().AddComponent<DlgRankPowerupSeasonViewComponent>();
		}

		public void OnRegisterUIEvent(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.GetComponent<DlgRankPowerupSeason>().RegisterUIEvent();
		}

		public void OnShowWindow(UIBaseWindow uiBaseWindow, ShowWindowData contextData = null)
		{
			var dlg = uiBaseWindow.GetComponent<DlgRankPowerupSeason>();
			if (uiBaseWindow.IsFirstLoad)
			{
				EventSystem.Instance.Publish(dlg.DomainScene(), new ClientEventType.NoticeUIShowCommonLoading(){bForce = true});

				dlg.View.uiTransform.gameObject.SetActive(false);
				dlg.PreLoadWindow(contextData, () =>
				{
					EventSystem.Instance.Publish(dlg.DomainScene(), new ClientEventType.NoticeUIHideCommonLoading(){bForce = true});

					dlg.View.uiTransform.gameObject.SetActive(true);
					ET.Client.UIManagerHelper.ShowUIAnimation(dlg.View.EG_OpenAnimationRectTransform, () =>
					{
					});
					dlg.ShowWindow(contextData).Coroutine();
				}).Coroutine();
			}
			else
			{
				ET.Client.UIManagerHelper.ShowUIAnimation(dlg.View.EG_OpenAnimationRectTransform, () =>
				{
				});
				dlg.ShowWindow(contextData).Coroutine();
			}
		}

		public void OnHideWindow(UIBaseWindow uiBaseWindow, System.Action finished)
		{
			var dlg = uiBaseWindow.GetComponent<DlgRankPowerupSeason>();
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
