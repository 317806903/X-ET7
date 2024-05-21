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
                self.unitId = new();
                self.playAudioActionId = new();
                self.isOnlySelfShow = new();
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
            }
        }

        public static void Init(this SyncData_UnitPlayAudio self, HashSet<(Unit unit, string playAudioActionId, bool isOnlySelfShow)> list)
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

        public static async ETTask DealByBytes(this SyncData_UnitPlayAudio self, UnitComponent unitComponent)
        {
            DictionaryComponent<string, HashSetComponent<Unit>> playAudioActionId2Units = DictionaryComponent<string, HashSetComponent<Unit>>.Create();
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
                if(playAudioActionId2Units.TryGetValue(playAudioActionId, out var hashSet) == false)
                {
                    hashSet = HashSetComponent<Unit>.Create();
                    playAudioActionId2Units[playAudioActionId] = hashSet;
                }

                if (hashSet.Count <= 20)
                {
                    hashSet.Add(unit);
                }
            }

            ListComponent<(Unit unit, string playAudioActionId, bool isOnlySelfShow)> list = ListComponent<(Unit unit, string playAudioActionId, bool isOnlySelfShow)>.Create();
            foreach (var item in playAudioActionId2Units)
            {
                string playAudioActionId = item.Key;
                HashSet<Unit> units = item.Value;

                foreach (Unit unit in units)
                {
                    list.Add((unit, playAudioActionId, false));
                }
            }
            playAudioActionId2Units.Dispose();

            EventType.SyncPlayAudio _SyncPlayAudio = new ()
            {
                list = list,
            };
            EventSystem.Instance.Publish(unitComponent.DomainScene(), _SyncPlayAudio);

            await ETTask.CompletedTask;
        }
    }
}