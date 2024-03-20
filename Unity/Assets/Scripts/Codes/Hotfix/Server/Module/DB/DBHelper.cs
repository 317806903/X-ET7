using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Driver;

namespace ET.Server
{
    public static class DBHelper
    {
        public static async ETTask<T> LoadDBWithParent2Child<T>(Entity parent, long playerId, bool createWhenNull) where T :Entity, IAwake
        {
            T entityDB = await ET.Server.DBHelper._LoadDB<T>(parent.DomainScene(), playerId);
            if (entityDB == null)
            {
                if (createWhenNull)
                {
                    entityDB = parent.AddChildWithId<T>(playerId) as T;
                }
            }
            else
            {
                parent.AddChild(entityDB);
            }
            entityDB?.AddComponent<DataCacheWriteComponent>();
            return entityDB;
        }

        public static async ETTask<T> LoadDBWithParent2Component<T>(Entity parent, long playerId, bool createWhenNull) where T :Entity, IAwake, new()
        {
            T entityDB = await ET.Server.DBHelper._LoadDB<T>(parent.DomainScene(), playerId);
            if (entityDB == null)
            {
                if (createWhenNull)
                {
                    entityDB = parent.AddComponentWithId<T>(playerId);
                }
            }
            else
            {
                parent.AddComponent(entityDB);
            }
            entityDB?.AddComponent<DataCacheWriteComponent>();
            return entityDB;
        }

        public static async ETTask<List<T>> LoadDBListWithParent2Child<T>(Entity parent) where T :Entity
        {
            List<T> entityDBList = await ET.Server.DBHelper._LoadDBList<T>(parent.DomainScene());
            if (entityDBList == null || entityDBList.Count == 0)
            {
                return null;
            }

            for (int i = 0; i < entityDBList.Count; i++)
            {
                T entityDB = entityDBList[i];
                parent.AddChild(entityDB);
                entityDB.AddComponent<DataCacheWriteComponent>();
            }

            return entityDBList;
        }

        public static async ETTask<List<T>> LoadDBListWithParent2Component<T>(Entity parent) where T :Entity
        {
            List<T> entityDBList = await ET.Server.DBHelper._LoadDBList<T>(parent.DomainScene());
            if (entityDBList == null || entityDBList.Count == 0)
            {
                return null;
            }

            for (int i = 0; i < entityDBList.Count; i++)
            {
                T entityDB = entityDBList[i];
                parent.AddComponent(entityDB);
                entityDB.AddComponent<DataCacheWriteComponent>();
            }

            return entityDBList;
        }

        public static async ETTask<long> GetDBCount<T>(Scene scene) where T :Entity
        {
            if (DBManagerComponent.Instance.NeedDB == false)
            {
                await ETTask.CompletedTask;
                return 0;
            }
            else
            {
                DBComponent dbComponent = DBManagerComponent.Instance.GetZoneDB(scene.DomainZone());
                return await dbComponent.QueryCount<T>();
            }
        }

        public static async ETTask<T> _LoadDB<T>(Scene scene, long Id) where T :Entity
        {
            if (DBManagerComponent.Instance.NeedDB == false)
            {
                await ETTask.CompletedTask;
                return null;
            }
            else
            {
                DBComponent dbComponent = DBManagerComponent.Instance.GetZoneDB(scene.DomainZone());
                return await dbComponent.Query<T>(Id);
            }
        }

        public static async ETTask<List<T>> _LoadDBList<T>(Scene scene) where T :Entity
        {
            if (DBManagerComponent.Instance.NeedDB == false)
            {
                await ETTask.CompletedTask;
                return null;
            }
            else
            {
                DBComponent dbComponent = DBManagerComponent.Instance.GetZoneDB(scene.DomainZone());
                return await dbComponent.Query<T>((db)=>true);
            }
        }

        public static async ETTask SaveDB<T>(T entity) where T :Entity
        {
            if (DBManagerComponent.Instance.NeedDB == false)
            {
                await ETTask.CompletedTask;
            }
            else
            {
                DBComponent dbComponent = DBManagerComponent.Instance.GetZoneDB(entity.DomainZone());
                await dbComponent.SaveNotWait(entity);
            }
        }

    }
}