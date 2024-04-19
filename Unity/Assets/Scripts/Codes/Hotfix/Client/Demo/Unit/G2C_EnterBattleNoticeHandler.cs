using System.Collections.Generic;
using ET.Ability;

namespace ET.Client
{
	[MessageHandler(SceneType.Client)]
	public class G2C_EnterBattleNoticeHandler : AMHandler<G2C_EnterBattleNotice>
	{
		protected override async ETTask Run(Session session, G2C_EnterBattleNotice message)
		{
			Log.Debug("G2C_EnterBattleNotice 11");
			Scene clientScene = session.DomainScene();
			Scene currentScene = session.DomainScene().CurrentScene();

			PlayerStatusComponent playerStatusComponent = ET.Client.PlayerStatusHelper.GetMyPlayerStatusComponent(clientScene);
			playerStatusComponent.PlayerStatus = PlayerStatus.Battle;

			EventSystem.Instance.Publish(clientScene, new EventType.NoticeUIShowCommonLoading());

			// 等待场景切换完成
			await clientScene.GetComponent<ObjectWait>().Wait<Wait_SceneChangeFinish>();

			Log.Debug("G2C_EnterBattleNotice 22");
			EventSystem.Instance.Publish(clientScene, new EventType.EnterMapFinish());

			EventSystem.Instance.Publish(clientScene, new EventType.NoticeUIHideCommonLoading());

			Log.Debug("G2C_EnterBattleNotice 33");
			await ETTask.CompletedTask;
		}
	}
}
