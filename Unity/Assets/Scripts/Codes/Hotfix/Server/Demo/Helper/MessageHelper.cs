using System.Collections.Generic;
using System.IO;

namespace ET.Server
{
    public static class MessageHelper
    {
        public static void NoticeUnitAdd(Unit unit, Unit sendUnit)
        {
            // M2C_CreateUnits createUnits = new() { Units = new() };
            // createUnits.Units.Add(ET.Ability.UnitHelper.CreateUnitInfo(sendUnit));
            // MessageHelper.SendToClient(unit, createUnits);

            ET.Ability.UnitHelper.AddSyncNoticeUnitAdd(unit, sendUnit);
        }

        public static void NoticeUnitRemove(Unit unit, Unit sendUnit)
        {
            // M2C_RemoveUnits removeUnits = new() {Units = new()};
            // removeUnits.Units.Add(sendUnit.Id);
            // MessageHelper.SendToClient(unit, removeUnits);

            ET.Ability.UnitHelper.AddSyncNoticeUnitRemove(unit, sendUnit.Id);
        }

        public static void Broadcast(Unit unit, IActorMessage message)
        {
            var dict = unit.GetBeSeePlayers();
            if (dict == null)
            {
                return;
            }
            // 网络底层做了优化，同一个消息不会多次序列化
            ActorLocationSenderOneType oneTypeLocationType = ActorLocationSenderComponent.Instance.Get(LocationType.Player);
            foreach (AOIEntity u in dict.Values)
            {
                long unitId = u.Unit.Id;
                if (oneTypeLocationType.GetChild<Entity>(unitId) == null)
                {
                    continue;
                }
                oneTypeLocationType.Send(unitId, message, unit.DomainScene().InstanceId);
            }
        }

        public static void SendToClient(Unit unit, IActorMessage message, bool chkPlayerExist = true)
        {
            if (unit == null || unit.IsDisposed)
            {
                return;
            }
            ActorLocationSenderOneType oneTypeLocationType = ActorLocationSenderComponent.Instance.Get(LocationType.Player);
            if (chkPlayerExist && oneTypeLocationType.GetChild<Entity>(unit.Id) == null)
            {
                return;
            }
            oneTypeLocationType.Send(unit.Id, message, unit.DomainScene().InstanceId);
        }

        public static void SendToClient(long actionId, IActorMessage message, long sceneInstanceId, bool chkPlayerExist = true, bool needWait = false)
        {
            ActorLocationSenderOneType oneTypeLocationType = ActorLocationSenderComponent.Instance.Get(LocationType.Player);
            if (chkPlayerExist && oneTypeLocationType.GetChild<Entity>(actionId) == null)
            {
                if (needWait)
                {
                    WaitToSendToClient(actionId, message, sceneInstanceId).Coroutine();
                }
                return;
            }
            oneTypeLocationType.Send(actionId, message, sceneInstanceId);
        }

        public static async ETTask WaitToSendToClient(long actionId, IActorMessage message, long sceneInstanceId)
        {
            bool bRet = await ChkPlayerConnected(actionId);
            if (bRet == false)
            {
                Log.Error($"A message of type [{message.GetType().Name}] should be sent to player [{actionId}] but failed to sent after all retries of waiting.");
                return;
            }
            ActorLocationSenderOneType oneTypeLocationType = ActorLocationSenderComponent.Instance.Get(LocationType.Player);
            oneTypeLocationType.Send(actionId, message, sceneInstanceId);
        }

        public static async ETTask<bool> ChkPlayerConnected(long playerId, int retryCount = 1000)
        {
            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.GamePlay, playerId))
            {
                ActorLocationSenderOneType oneTypeLocationType = ActorLocationSenderComponent.Instance.Get(LocationType.Player);
                while (oneTypeLocationType.GetChild<Entity>(playerId) == null)
                {
                    if (retryCount-- <= 0)
                    {
                        return false;
                    }
                    await TimerComponent.Instance.WaitFrameAsync();
                    if (oneTypeLocationType.IsDisposed)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public static void SendToLocationActor(int locationType, long id, IActorLocationMessage message, long sceneInstanceId, bool needChkExist = true)
        {
            ActorLocationSenderOneType oneTypeLocationType = ActorLocationSenderComponent.Instance.Get(locationType);
            if (needChkExist && oneTypeLocationType.GetChild<Entity>(id) == null)
            {
                return;
            }
            oneTypeLocationType.Send(id, message, sceneInstanceId);
        }

        /// <summary>
        /// 发送协议给Actor
        /// </summary>
        /// <param name="actorId">注册Actor的InstanceId</param>
        /// <param name="message"></param>
        public static void SendActor(long actorId, IActorMessage message)
        {
            ActorMessageSenderComponent.Instance.Send(actorId, message);
        }
    }
}