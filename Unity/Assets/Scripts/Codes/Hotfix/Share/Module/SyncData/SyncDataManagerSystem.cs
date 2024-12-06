using System.Collections.Generic;
using System.Diagnostics;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using Unity.Mathematics;

namespace ET
{
    [ComponentOf(typeof (SyncDataManager))]
    public static class SyncDataManagerSystem
    {

        public class AwakeSystem: AwakeSystem<SyncDataManager>
        {
            protected override void Awake(SyncDataManager self)
            {
                self.player2SyncDataList = new();
                self.playerSessionInfoList = new();
                self.syncData2Players = new();
                self.Init();
            }
        }

        [ObjectSystem]
        public class SyncDataManagerDestroySystem: DestroySystem<SyncDataManager>
        {
            protected override void Destroy(SyncDataManager self)
            {
                self.player2SyncDataList.Clear();
                self.playerSessionInfoList.Clear();
                self.syncData2Players.Clear();
            }
        }

        [ObjectSystem]
        public class SyncDataManagerFixedUpdateSystem: LateUpdateSystem<SyncDataManager>
        {
            protected override void LateUpdate(SyncDataManager self)
            {
                if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static void Init(this SyncDataManager self)
        {
            self.AddComponent<SyncDataManager_UnitPosInfo>();
            self.AddComponent<SyncDataManager_UnitNumericInfo>();
            self.AddComponent<SyncDataManager_UnitPlayAudio>();
            self.AddComponent<SyncDataManager_UnitFloatingText>();
            self.AddComponent<SyncDataManager_UnitComponent>();
            self.AddComponent<SyncDataManager_UnitGetCoinShow>();
            self.AddComponent<SyncDataManager_DamageShow>();
            self.AddComponent<SyncDataManager_UnitEffects>();
        }

        public static void FixedUpdate(this SyncDataManager self, float fixedDeltaTime)
        {
            self.GetComponent<SyncDataManager_UnitPosInfo>().FixedUpdate(fixedDeltaTime);
            self.GetComponent<SyncDataManager_UnitNumericInfo>().FixedUpdate(fixedDeltaTime);
            self.GetComponent<SyncDataManager_UnitPlayAudio>().FixedUpdate(fixedDeltaTime);
            self.GetComponent<SyncDataManager_UnitFloatingText>().FixedUpdate(fixedDeltaTime);
            self.GetComponent<SyncDataManager_UnitComponent>().FixedUpdate(fixedDeltaTime);
            self.GetComponent<SyncDataManager_UnitGetCoinShow>().FixedUpdate(fixedDeltaTime);
            self.GetComponent<SyncDataManager_DamageShow>().FixedUpdate(fixedDeltaTime);
            self.GetComponent<SyncDataManager_UnitEffects>().FixedUpdate(fixedDeltaTime);

            self.SendSync2Client();
        }

        public static void SendSync2Client(this SyncDataManager self)
        {
            foreach (var item in self.player2SyncDataList)
            {
                long playerId = item.Key;
                List<byte[]> syncDataList = item.Value;

                EventType.SyncDataList _SyncDataList = new ();
                _SyncDataList.playerId = playerId;
                _SyncDataList.syncDataList = syncDataList;
                EventSystem.Instance.Publish(self.DomainScene(), _SyncDataList);
            }

            self.player2SyncDataList.Clear();
        }

        public static void SyncData2Players(this SyncDataManager self, Unit unitSync, byte[] syncData)
        {
            var dict = unitSync.GetBeSeePlayers();
            if (dict == null)
            {
                return;
            }
            foreach (AOIEntity u in dict.Values)
            {
                long playerId = u.Unit.Id;
                self.player2SyncDataList.Add(playerId, syncData);
            }
        }

        public static List<long> GetSyncData2Players(this SyncDataManager self, Unit unitSync)
        {
            var dict = unitSync.GetBeSeePlayers();
            if (dict == null)
            {
                return null;
            }
            List<long> syncData2Players = self.syncData2Players;
            syncData2Players.Clear();
            foreach (AOIEntity u in dict.Values)
            {
                long playerId = u.Unit.Id;
                syncData2Players.Add(playerId);
            }

            return syncData2Players;
        }

        public static void SyncData2OnlyPlayer(this SyncDataManager self, long playerId, byte[] syncData)
        {
            self.player2SyncDataList.Add(playerId, syncData);
        }

        #region AddSyncData_UnitXXXX
        public static void AddSyncData_UnitPosInfo(this SyncDataManager self, Unit unit)
        {
            self.GetComponent<SyncDataManager_UnitPosInfo>().AddSyncPosUnit(unit);
        }

        public static void AddSyncData_UnitNumericInfo(this SyncDataManager self, Unit unit)
        {
            self.GetComponent<SyncDataManager_UnitNumericInfo>().AddSyncNumericUnit(unit);
        }

        public static void AddSyncData_UnitNumericInfo(this SyncDataManager self, Unit unit, int numericKey)
        {
            self.GetComponent<SyncDataManager_UnitNumericInfo>().AddSyncNumericUnitByKey(unit, numericKey);
        }

        public static void AddSyncData_UnitPlayAudio(this SyncDataManager self, Unit unit, string playAudioActionId, bool isOnlySelfShow)
        {
            self.GetComponent<SyncDataManager_UnitPlayAudio>().AddSyncPlayAudio(unit, playAudioActionId, isOnlySelfShow);
        }

        public static void AddSyncData_UnitFloatingText(this SyncDataManager self, Unit unit, string floatingTextId, int shownum, bool isOnlySelfShow)
        {
            self.GetComponent<SyncDataManager_UnitFloatingText>().AddSyncFloatingText(unit, floatingTextId, shownum, isOnlySelfShow);
        }

        public static void AddSyncData_UnitGetCoinShow(this SyncDataManager self, long playerId, Unit unit, CoinTypeInGame coinType, int chgValue)
        {
            self.GetComponent<SyncDataManager_UnitGetCoinShow>().AddSyncGetCoinShow(playerId, unit, coinType, chgValue);
        }

        public static void AddSyncData_DamageShow(this SyncDataManager self, Unit unit, int damageValue, bool isCrt)
        {
            self.GetComponent<SyncDataManager_DamageShow>().AddSyncDamageShow(unit, damageValue, isCrt);
        }

        public static void AddSyncData_UnitComponent(this SyncDataManager self, Unit unit, System.Type type)
        {
            self.GetComponent<SyncDataManager_UnitComponent>().AddSyncUnit(unit, type);
        }

        public static void AddSyncData_UnitEffects(this SyncDataManager self, Unit unit, long effectObjId, bool isOnlySelfShow)
        {
            self.GetComponent<SyncDataManager_UnitEffects>().AddSyncUnit(unit, effectObjId, isOnlySelfShow);
        }
        #endregion
    }
}