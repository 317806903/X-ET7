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
			DlgUpdate _DlgUpdate = scene.GetComponent<UIComponent>().GetDlgLogic<DlgUpdate>();
			_DlgUpdate.View.ELabel_TotalDownloadCountText.text = $"TotalDownloadCount={a.TotalDownloadCount}";
			_DlgUpdate.View.ELabel_CurrentDownloadCountText.text = $"CurrentDownloadCount={a.CurrentDownloadCount}";
			_DlgUpdate.View.ELabel_TotalDownloadSizeBytesText.text = $"TotalDownloadSizeBytes={a.TotalDownloadSizeBytes}";
			_DlgUpdate.View.ELabel_CurrentDownloadSizeBytesText.text = $"CurrentDownloadSizeBytes={a.CurrentDownloadSizeBytes}";
			_DlgUpdate.View.E_SliderSlider.value = (float)a.CurrentDownloadCount/a.TotalDownloadCount;
			await ETTask.CompletedTask;
		}
	}
	
	[Event(SceneType.Client)]
	public class OnPatchDownlodFailedEvent: AEvent<Scene, OnPatchDownlodFailed>
	{
		protected override async ETTask Run(Scene scene, OnPatchDownlodFailed a)
		{
			Log.Error($"下载资源失败: {a.FileName} {a.Error}");
			await ETTask.CompletedTask;
		}
	}
	
}