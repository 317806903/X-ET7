﻿namespace ET.Client
{
	[Event(SceneType.Client)]
	public class AppStartInitFinish_CreateLoginUI: AEvent<Scene, EventType.AppStartInitFinish>
	{
		protected override async ETTask Run(Scene scene, EventType.AppStartInitFinish args)
		{
			await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgLogin>();
			await ETTask.CompletedTask;
		}
	}
}
