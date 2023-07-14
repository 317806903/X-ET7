using System.Collections.Generic;
using MongoDB.Bson;

namespace ET.Server
{
    public static class TransferHelper
    {
        public static async ETTask TransferAtFrameFinish(bool isDestroyScene, Scene scene, Unit unit, long sceneInstanceId, string sceneName)
        {
            await Game.WaitFrameFinish();

            await TransferHelper.Transfer(unit, sceneInstanceId, sceneName);

            if (isDestroyScene)
            {
                scene.Dispose();
            }
        }
        
        public static async ETTask EnterMap(long sceneInstanceId, long playerId, int playerLevel, string gamePlayBattleLevelCfgId)
        {
            await Game.WaitFrameFinish();

			M2M_EnterMapRequest _M2M_EnterMapRequest = new() { PlayerId = playerId, PlayerLevel = playerLevel, GamePlayBattleLevelCfgId = gamePlayBattleLevelCfgId, };
			
            await ActorMessageSenderComponent.Instance.Call(sceneInstanceId, _M2M_EnterMapRequest);

        }
        

        public static async ETTask Transfer(Unit unit, long sceneInstanceId, string sceneName)
        {
            // location加锁
            long unitId = unit.Id;
            long unitInstanceId = unit.InstanceId;
            
            M2M_UnitTransferRequest request = new M2M_UnitTransferRequest() {Entitys = new List<byte[]>()};
            request.OldInstanceId = unitInstanceId;
            request.Unit = unit.ToBson();
            foreach (Entity entity in unit.Components.Values)
            {
                if (entity is ITransfer)
                {
                    request.Entitys.Add(entity.ToBson());
                }
            }
            unit.Dispose();
            
            await LocationProxyComponent.Instance.Lock(LocationType.Unit, unitId, unitInstanceId);
            await ActorMessageSenderComponent.Instance.Call(sceneInstanceId, request);
        }
    }
}