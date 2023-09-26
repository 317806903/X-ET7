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

			clientScene.GetComponent<PlayerComponent>().PlayerStatus = PlayerStatus.Battle;

			// 等待场景切换完成
			await clientScene.GetComponent<ObjectWait>().Wait<Wait_SceneChangeFinish>();

			Log.Debug("G2C_EnterBattleNotice 22");
			EventSystem.Instance.Publish(clientScene, new EventType.EnterMapFinish());

			Log.Debug("G2C_EnterBattleNotice 33");
			await ETTask.CompletedTask;
		}
	}
}
