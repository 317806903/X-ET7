

using System.Collections.Generic;
using System.IO;

namespace ET.Server
{
    public static class MessageHelper
    {
        public static void NoticeUnitAdd(Unit unit, Unit sendUnit)
        {
            M2C_CreateUnits createUnits = new() { Units = ListComponent<UnitInfo>.Create() };
            createUnits.Units.Add(ET.Ability.UnitHelper.CreateUnitInfo(sendUnit));
            MessageHelper.SendToClient(unit, createUnits);
        }

        public static void NoticeUnitRemove(Unit unit, Unit sendUnit)
        {
            M2C_RemoveUnits removeUnits = new() {Units = ListComponent<long>.Create()};
            removeUnits.Units.Add(sendUnit.Id);
            MessageHelper.SendToClient(unit, removeUnits);
        }

        private static MultiMap<long, Unit> playerSeeUnits = new();
        public static MultiMap<long, Unit> GetUnitBeSeePlayers(List<Unit> units)
        {
            playerSeeUnits.Clear();
            for (int i = 0; i < units.Count; i++)
            {
                Unit unit = units[i];
                var dict = unit.GetBeSeePlayers();
                if (dict == null)
                {
                    continue;
                }
                foreach (AOIEntity u in dict.Values)
                {
                    long playerId = u.Unit.Id;
                    playerSeeUnits.Add(playerId, unit);
                }
            }

            return playerSeeUnits;
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
                oneTypeLocationType.Send(unitId, message);
            }
        }

        public static void SendToClient(Unit unit, IActorMessage message, bool chkPlayerExist = true)
        {
            ActorLocationSenderOneType oneTypeLocationType = ActorLocationSenderComponent.Instance.Get(LocationType.Player);
            if (chkPlayerExist && oneTypeLocationType.GetChild<Entity>(unit.Id) == null)
            {
                return;
            }
            oneTypeLocationType.Send(unit.Id, message);
        }

        public static void SendToClient(long actionId, IActorMessage message, bool chkPlayerExist = true)
        {
            ActorLocationSenderOneType oneTypeLocationType = ActorLocationSenderComponent.Instance.Get(LocationType.Player);
            if (chkPlayerExist && oneTypeLocationType.GetChild<Entity>(actionId) == null)
            {
                return;
            }
            oneTypeLocationType.Send(actionId, message);
        }

        public static void SendToLocationActor(int locationType, long id, IActorLocationMessage message, bool needChkExist = true)
        {
            ActorLocationSenderOneType oneTypeLocationType = ActorLocationSenderComponent.Instance.Get(locationType);
            if (needChkExist && oneTypeLocationType.GetChild<Entity>(id) == null)
            {
                return;
            }
            oneTypeLocationType.Send(id, message);
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