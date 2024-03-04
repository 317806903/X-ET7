using System;


namespace ET.Server
{
	[MessageHandler(SceneType.Gate)]
	public class C2G_ReLoginGateHandler : AMRpcHandler<C2G_ReLoginGate, G2C_ReLoginGate>
	{
		protected override async ETTask Run(Session session, C2G_ReLoginGate request, G2C_ReLoginGate response)
		{
			Scene scene = session.DomainScene();

			long playerId = request.PlayerId;

			PlayerComponent playerComponent = scene.GetComponent<PlayerComponent>();
			Player player = playerComponent.GetChild<Player>(playerId);
			if (player == null)
			{
				Log.Error($"---zpb scene ReLogin player == null");
				response.Error = ErrorCode.ERR_LogicError;
				response.Message = "scene ReLogin player == null";
				return;
			}

			PlayerSessionComponent playerSessionComponent = player.GetComponent<PlayerSessionComponent>();
			if (playerSessionComponent == null)
			{
				Log.Error($"---zpb scene ReLogin playerSessionComponent == null");
				response.Error = ErrorCode.ERR_LogicError;
				response.Message = "scene ReLogin playerSessionComponent == null";
				return;
			}

			Session sessionOld = playerSessionComponent.Session;
			playerSessionComponent.Session = session;
            session.RemoveComponent<SessionAcceptTimeoutComponent>();

            // await player.AddLocation(LocationType.Player);
            // ActorLocationSenderOneType oneTypeLocationType = ActorLocationSenderComponent.Instance.Get(LocationType.Player);
            // oneTypeLocationType.GetOrCreate(playerId);

			session.AddComponent<SessionPlayerComponent>().Player = player;
			if (sessionOld != null)
			{
				sessionOld.Dispose();
			}

			await ETTask.CompletedTask;
		}
	}
}