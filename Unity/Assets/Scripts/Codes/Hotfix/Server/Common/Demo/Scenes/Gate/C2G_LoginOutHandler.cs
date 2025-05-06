using System;


namespace ET.Server
{
	[MessageHandler(SceneType.Gate)]
	public class C2G_LoginOutHandler : AMRpcHandler<C2G_LoginOut, G2C_LoginOut>
	{
		protected override async ETTask Run(Session session, C2G_LoginOut request, G2C_LoginOut response)
		{
			Player player = session.GetComponent<SessionPlayerComponent>().Player;
			NextFrame(session, player).Coroutine();
			await ETTask.CompletedTask;
		}
		
		protected async ETTask NextFrame(Session session, Player player)
		{
			await TimerComponent.Instance.WaitFrameAsync();
			await player.RemoveLocation(LocationType.Player);
			player.Dispose();
			session.Dispose();
			
			await ETTask.CompletedTask;
		}
	}
}