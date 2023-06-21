using System.Collections.Generic;
using ET.Ability;

namespace ET.Client
{
	[MessageHandler(SceneType.Client)]
	public class G2C_EnterBattleNoticeHandler : AMHandler<G2C_EnterBattleNotice>
	{
		protected override async ETTask Run(Session session, G2C_EnterBattleNotice message)
		{
			Scene clientScene = session.DomainScene();
			Scene currentScene = session.DomainScene().CurrentScene();

			clientScene.GetComponent<PlayerComponent>().PlayerStatus = PlayerStatus.Battle;
			
			// 等待场景切换完成
			await clientScene.GetComponent<ObjectWait>().Wait<Wait_SceneChangeFinish>();

			EventSystem.Instance.Publish(clientScene, new EventType.EnterMapFinish());

			await ETTask.CompletedTask;
		}
	}
}
