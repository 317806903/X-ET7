using System;


namespace ET.Server
{
	[MessageHandler(SceneType.Gate)]
	public class C2G_ReDealMyFunctionMenuHandler : AMHandler<C2G_ReDealMyFunctionMenu>
	{
		protected override async ETTask Run(Session session, C2G_ReDealMyFunctionMenu message)
		{
			Player player = session.GetComponent<SessionPlayerComponent>().Player;
			long playerId = player.Id;
			Scene scene = session.DomainScene();

			await ET.Server.PlayerCacheHelper.DealPlayerFunctionMenu(scene, playerId, null);

            await ETTask.CompletedTask;
		}
	}
}