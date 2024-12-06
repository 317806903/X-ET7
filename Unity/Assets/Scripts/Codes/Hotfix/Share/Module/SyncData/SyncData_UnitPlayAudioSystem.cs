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
                self.unitId.Clear();
                self.playAudioActionId.Clear();
                self.isOnlySelfShow.Clear();
                self.playAudioActionId2Units.Clear();
                self.list.Clear();
            }
        }

        public static void Init(this SyncData_UnitPlayAudio self, HashSet<(Unit unit, string floatingTextActionId, bool isOnlySelfShow)> list)
        {
            self.unitId.Clear();
            self.playAudioActionId.Clear();
            self.isOnlySelfShow.Clear();
            if (list == null)
            {
                return;
            }
            foreach ((Unit unit, string playAudioActionId, bool isOnlySelfShow) in list)
            {
                if (unit == null)
                {
                    continue;
                }
                self.unitId.Add(unit.Id);
                self.playAudioActionId.Add(playAudioActionId);
                self.isOnlySelfShow.Add(isOnlySelfShow);
            }
        }

        public static void DealByBytes(this SyncData_UnitPlayAudio self, UnitComponent unitComponent)
        {
            self.playAudioActionId2Units.Clear();
            int count = self.unitId.Count;
            for (int i = 0; i < count; i++)
            {
                long unitId = self.unitId[i];
                string playAudioActionId = self.playAudioActionId[i];
                bool isOnlySelfShow = self.isOnlySelfShow[i];
                Unit unit = unitComponent.Get(unitId);
                if (unit == null)
                {
                    continue;
                }
                if(self.playAudioActionId2Units.TryGetValue(playAudioActionId, out var hashSet) == false)
                {
                    hashSet = HashSetComponent<Unit>.Create();
                    self.playAudioActionId2Units[playAudioActionId] = hashSet;
                }

                if (hashSet.Count <= 20)
                {
                    hashSet.Add(unit);
                }
            }

            self.list.Clear();
            foreach (var item in self.playAudioActionId2Units)
            {
                string playAudioActionId = item.Key;
                HashSet<Unit> units = item.Value;

                foreach (Unit unit in units)
                {
                    self.list.Add((unit, playAudioActionId, false));
                }
            }
            self.playAudioActionId2Units.Clear();

            EventType.SyncPlayAudio _SyncPlayAudio = new ()
            {
                list = self.list,
            };
            EventSystem.Instance.Publish(unitComponent.DomainScene(), _SyncPlayAudio);
        }
    }
}