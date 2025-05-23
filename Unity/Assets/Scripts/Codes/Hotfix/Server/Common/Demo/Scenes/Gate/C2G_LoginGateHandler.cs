﻿using System;


namespace ET.Server
{
	[MessageHandler(SceneType.Gate)]
	public class C2G_LoginGateHandler : AMRpcHandler<C2G_LoginGate, G2C_LoginGate>
	{
		protected override async ETTask Run(Session session, C2G_LoginGate request, G2C_LoginGate response)
		{
			Scene scene = session.DomainScene();
			bool IsFirstLogin = request.IsFirstLogin == 1? true : false;
			string accountId = scene.GetComponent<GateSessionKeyComponent>().GetAccountId(request.Key);
			if (accountId == null)
			{
				response.Error = ErrorCore.ERR_ConnectGateKeyError;
				response.Message = "Gate key验证失败!";
				return;
			}
			StartSceneConfig accountSceneConfig = StartSceneConfigCategory.Instance.GetAccountManager(session.DomainZone());
			A2G_GetAccountInfo _A2G_GetAccountInfo = (A2G_GetAccountInfo)await ActorMessageSenderComponent.Instance.Call(
				accountSceneConfig.InstanceId, new G2A_GetAccountInfo() { AccountId = accountId });
			if (_A2G_GetAccountInfo.Error == ErrorCore.ERR_ConnectGateKeyError)
			{
				response.Error = ErrorCore.ERR_ConnectGateKeyError;
				response.Message = "获取账号信息失败!";
				return;
			}
			byte[] AccountComponentBytes = _A2G_GetAccountInfo.AccountComponentBytes;
			AccountComponent accountComponent = MongoHelper.Deserialize<AccountComponent>(AccountComponentBytes);
			long playerId = accountComponent.playerId;
			LoginType loginType = (LoginType)accountComponent.loginType;
			accountComponent.Dispose();

			session.RemoveComponent<SessionAcceptTimeoutComponent>();

			PlayerComponent playerComponent = scene.GetComponent<PlayerComponent>();

			long locationActorId = await LocationProxyComponent.Instance.Get(LocationType.Player, playerId, scene.InstanceId);
			if (locationActorId != 0)
			{
				G2OG_LoginInAtOtherWhere _G2OG_LoginInAtOtherWhere = new();
				ActorLocationSenderOneType oneTypeLocationTypeTmp = ActorLocationSenderComponent.Instance.Get(LocationType.Player);
				await oneTypeLocationTypeTmp.Call(playerId, _G2OG_LoginInAtOtherWhere, scene.InstanceId);
			}
			await LocationProxyComponent.Instance.RemoveLocation(playerId, LocationType.Player);
			Player player = playerComponent.GetChild<Player>(playerId);
			if (player != null)
			{
				player.GetComponent<PlayerSessionComponent>()?.Session?.Dispose();
				player.RemoveComponent<PlayerSessionComponent>();
				player.RemoveComponent<MailBoxComponent>();
				player.RemoveComponent<GateMapComponent>();
				player.Init(accountId, "", loginType);
			}
			else
			{
				player = playerComponent.AddChildWithId<Player>(playerId);
				player.Init(accountId, "", loginType);
			}

			PlayerStatusComponent playerStatusComponent = player.GetComponent<PlayerStatusComponent>();
			if (playerStatusComponent == null)
			{
				playerStatusComponent = player.AddComponent<PlayerStatusComponent>();
			}


			StartSceneConfig roomSceneConfig = StartSceneConfigCategory.Instance.GetRoomManager(session.DomainZone());

			R2G_GetRoomIdByPlayer _R2G_GetRoomIdByPlayer = (R2G_GetRoomIdByPlayer) await ActorMessageSenderComponent.Instance.Call(roomSceneConfig.InstanceId, new G2R_GetRoomIdByPlayer()
			{
				PlayerId = playerId,
			});

			RoomTypeInfo roomTypeInfo = ET.RoomTypeInfo.GetFromBytes(_R2G_GetRoomIdByPlayer.RoomTypeInfo);
			playerStatusComponent.RoomTypeInfo = roomTypeInfo;
			playerStatusComponent.RoomId = _R2G_GetRoomIdByPlayer.RoomId;
			playerStatusComponent.MapScale = _R2G_GetRoomIdByPlayer.MapScale * 0.01f;
			playerStatusComponent.LastBattleCfgId = "";
			playerStatusComponent.LastBattleResult = BattleResult.Default;
			if (playerStatusComponent.RoomId == 0)
			{
				playerStatusComponent.PlayerStatus = PlayerStatus.Hall;
			}
			else
			{
				RoomStatus roomStatus = (RoomStatus)_R2G_GetRoomIdByPlayer.RoomStatus;
				if (roomStatus == RoomStatus.Idle)
				{
					playerStatusComponent.PlayerStatus = PlayerStatus.Room;
				}
				else
				{
					playerStatusComponent.PlayerStatus = PlayerStatus.Battle;
				}
			}

            player.AddComponent<PlayerSessionComponent>().Session = session;
			player.AddComponent<MailBoxComponent, MailboxType>(MailboxType.GateSession);
			await player.AddLocation(LocationType.Player);
			ActorLocationSenderOneType oneTypeLocationType = ActorLocationSenderComponent.Instance.Get(LocationType.Player);
			oneTypeLocationType.GetOrCreate(playerId);

			session.AddComponent<SessionPlayerComponent>().Player = player;

			response.PlayerId = playerId;
			response.PlayerComponentBytes = player.ToBson();
			response.PlayerStatusComponentBytes = playerStatusComponent.ToBson();

			await PlayerCacheHelper.DealPlayerFunctionMenuWaitChk(scene, playerId);
			await PlayerCacheHelper.DealPlayerUIRedDotType(scene, playerId, PlayerModelType.None);

			if (IsFirstLogin)
			{
				await PlayerCacheHelper.SetUIRedDotTypeList(scene, playerId, new()
				{
					UIRedDotType.PVESeason, UIRedDotType.Tutorial, UIRedDotType.Questionnaire, UIRedDotType.MultPlayers,
				}, true, true);
			}
			await ETTask.CompletedTask;
		}
	}
}