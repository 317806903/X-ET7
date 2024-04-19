using System.Collections.Generic;
using System.Diagnostics;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using Unity.Mathematics;

namespace ET
{
    public static class SyncData_UnitPlayAudioSystem
    {
        public class AwakeSystem: AwakeSystem<SyncData_UnitPlayAudio>
        {
            protected override void Awake(SyncData_UnitPlayAudio self)
            {
            }
        }

        [ObjectSystem]
        public class SyncData_UnitPlayAudioDestroySystem: DestroySystem<SyncData_UnitPlayAudio>
        {
            protected override void Destroy(SyncData_UnitPlayAudio self)
            {
            }
        }

        public static void Init(this SyncData_UnitPlayAudio self, Unit unit, string playAudioActionId, bool isOnlySelfShow)
        {
            self.unitId = unit.Id;
            self.playAudioActionId = playAudioActionId;
        }

        public static void DealByBytes(this SyncData_UnitPlayAudio self, Unit unit)
        {
            EventType.SyncPlayAudio _SyncPlayAudio = new ()
            {
                unit = unit,
                playAudioActionId = self.playAudioActionId,
            };
            EventSystem.Instance.Publish(unit.DomainScene(), _SyncPlayAudio);
        }
    }
}