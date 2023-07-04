using ET.Ability;
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Gate)]
	public class R2G_StartBattleHandler : AMActorRpcHandler<Player, R2G_StartBattle, G2R_StartBattle>
	{
		protected override async ETTask Run(Player player, R2G_StartBattle request, G2R_StartBattle response)
		{
			// 在Gate上动态创建一个Map Scene，把Unit从DB中加载放进来，然后传送到真正的Map中，这样登陆跟传送的逻辑就完全一样了
			player.RemoveComponent<GateMapComponent>();
			GateMapComponent gateMapComponent = player.AddComponent<GateMapComponent>();
			gateMapComponent.Scene = await SceneFactory.CreateServerScene(gateMapComponent, player.Id, IdGenerater.Instance.GenerateInstanceId(), gateMapComponent.DomainZone(), "GateMap", SceneType.Map);
			
			Scene scene = gateMapComponent.Scene;
			// 这里可以从DB中加载Unit
			float3 position = new float3(-10, 0, -10);
			float3 forward = new float3(0, 0, 1);
			int roomSeatIndex = request.RoomSeatIndex;
			TeamFlagType teamFlagType = ET.Ability.TeamFlagHelper.GetTeamFlagTypeBySeatIndex(roomSeatIndex);
			Unit unit = ET.Ability.UnitHelper_Create.CreateWhenServer_ObserverUnit(scene, player.Id, teamFlagType, position, forward);
			
			PlayerStatusComponent playerStatusComponent = player.GetComponent<PlayerStatusComponent>();
			playerStatusComponent.PlayerStatus = PlayerStatus.Battle;
			
			G2C_EnterBattleNotice _G2C_EnterBattleNotice = new() { };
			player.GetComponent<PlayerSessionComponent>()?.Session?.Send(_G2C_EnterBattleNotice);
			
			// 等到一帧的最后面再传送，先让G2C_EnterMap返回，否则传送消息可能比G2C_EnterMap还早
			TransferHelper.TransferAtFrameFinish(unit, request.DynamicMapId, "").Coroutine();
			
			
		}
	}
}