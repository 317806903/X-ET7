using System;
using ET.EventType;
using UnityEngine;

namespace ET.Client
{
	[Event(SceneType.Client)]
	public class OnPatchDownloadProgressEvent: AEvent<Scene, ClientEventType.OnPatchDownloadProgress>
	{
		protected override async ETTask Run(Scene scene, ClientEventType.OnPatchDownloadProgress a)
		{
			DlgUpdate _DlgUpdate = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgUpdate>();
			_DlgUpdate.UpdateUI(a);
			await ETTask.CompletedTask;
		}
	}

	[Event(SceneType.Client)]
	public class OnPatchDownlodFailedEvent: AEvent<Scene, ClientEventType.OnPatchDownlodFailed>
	{
		protected override async ETTask Run(Scene scene, ClientEventType.OnPatchDownlodFailed a)
		{
			string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Res_DownLoadResErr", a.FileName, a.Error);
			UIManagerHelper.ShowConfirmNoClose(scene, msg);
			Log.Error(msg);
			await ETTask.CompletedTask;
		}
	}

}