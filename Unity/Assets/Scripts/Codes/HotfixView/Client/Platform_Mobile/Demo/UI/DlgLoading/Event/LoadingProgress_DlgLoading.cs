using System;
using ET.EventType;
using UnityEngine;

namespace ET.Client
{
	[Event(SceneType.Client | SceneType.Current)]
	public class LoadingProgress_DlgLoading: AEvent<Scene, ClientEventType.LoadingProgress>
	{
		protected override async ETTask Run(Scene scene, ClientEventType.LoadingProgress a)
		{
			DlgLoading _DlgLoading = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgLoading>();
			if (_DlgLoading != null)
			{
				_DlgLoading.UpdateProcess(a.curProgress);
			}
			await ETTask.CompletedTask;
		}
	}
}