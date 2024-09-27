using System;
using System.IO;

namespace ET.Server
{
    [FriendOf(typeof(DBManagerComponent))]
    public static class DBManagerComponentSystem
    {
        [ObjectSystem]
        public class DBManagerComponentAwakeSystem: AwakeSystem<DBManagerComponent>
        {
            protected override void Awake(DBManagerComponent self)
            {
                DBManagerComponent.Instance = self;
            }
        }

        [ObjectSystem]
        public class DBManagerComponentDestroySystem: DestroySystem<DBManagerComponent>
        {
            protected override void Destroy(DBManagerComponent self)
            {
                DBManagerComponent.Instance = null;
            }
        }

        public static DBComponent GetZoneDB(this DBManagerComponent self, int zone)
        {
            DBComponent dbComponent = self.DBComponents[zone];
            if (dbComponent != null)
            {
                return dbComponent;
            }

            StartZoneConfig startZoneConfig = StartZoneConfigCategory.Instance.Get(zone);
            if (startZoneConfig.DBConnection == "")
            {
                throw new Exception($"zone: {zone} not found mongo connect string");
            }

            dbComponent = self.AddChild<DBComponent, string, string, int>(startZoneConfig.DBConnection, startZoneConfig.DBName, zone);
            self.DBComponents[zone] = dbComponent;
            return dbComponent;
        }

        public static void SetLocalDB(this DBManagerComponent self)
        {
            DBLocalComponent dbLocalComponent = self.GetComponent<DBLocalComponent>();
            if (dbLocalComponent == null)
            {
                dbLocalComponent = self.AddComponent<DBLocalComponent>();
                string savePath = EventSystem.Instance.Invoke<ConfigComponent.GetLocalDBSavePath, string>(new ConfigComponent.GetLocalDBSavePath());
                dbLocalComponent.SetSavePath(savePath);
                DataCacheWriteComponent.DefaultSaveWaitTime = 100;
            }
        }

        public static DBLocalComponent GetLocalDB(this DBManagerComponent self)
        {
            DBLocalComponent dbLocalComponent = self.GetComponent<DBLocalComponent>();
            if (dbLocalComponent == null)
            {
                return null;
            }

            return dbLocalComponent;
        }
    }
}