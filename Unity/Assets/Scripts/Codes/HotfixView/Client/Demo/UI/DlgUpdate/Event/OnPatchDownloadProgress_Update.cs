using System;
using ET.EventType;
using UnityEngine;

namespace ET.Client
{
	[Event(SceneType.Client)]
	public class OnPatchDownloadProgressEvent: AEvent<Scene, OnPatchDownloadProgress>
	{
		protected override async ETTask Run(Scene scene, OnPatchDownloadProgress a)
		{
			DlgUpdate _DlgUpdate = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgUpdate>();
			_DlgUpdate.UpdateUI(a);
			await ETTask.CompletedTask;
		}
	}

	[Event(SceneType.Client)]
	public class OnPatchDownlodFailedEvent: AEvent<Scene, OnPatchDownlodFailed>
	{
		protected override async ETTask Run(Scene scene, OnPatchDownlodFailed a)
		{
			string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Res_DownLoadResErr", a.FileName, a.Error);
			UIManagerHelper.ShowConfirmNoClose(scene, msg);
			Log.Error(msg);
			await ETTask.CompletedTask;
		}
	}

}