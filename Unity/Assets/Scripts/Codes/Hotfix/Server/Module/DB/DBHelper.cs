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
                    entityDB?.SetDataCacheAutoWrite();
                }
            }
            else
            {
                parent.AddChild(entityDB);
            }
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
                    entityDB?.SetDataCacheAutoWrite();
                }
            }
            else
            {
                parent.AddComponent(entityDB);
            }
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
            }

            return entityDBList;
        }

        public static async ETTask<long> GetDBCount<T>(Scene scene) where T :Entity
        {
            if (DBManagerComponent.Instance.dbType == DBType.NoDB)
            {
                await ETTask.CompletedTask;
                return 0;
            }
            else if (DBManagerComponent.Instance.dbType == DBType.MongoDB)
            {
                DBComponent dbComponent = DBManagerComponent.Instance.GetZoneDB(scene.DomainZone());
                return await dbComponent.QueryCount<T>();
            }
            else if (DBManagerComponent.Instance.dbType == DBType.LocalDB)
            {
                DBLocalComponent dbLocalComponent = DBManagerComponent.Instance.GetLocalDB();
                return dbLocalComponent.QueryCount<T>();
            }
            return 0;
        }

        public static async ETTask<T> _LoadDB<T>(Scene scene, long Id) where T :Entity
        {
            if (DBManagerComponent.Instance.dbType == DBType.NoDB)
            {
                await ETTask.CompletedTask;
                return null;
            }
            else if (DBManagerComponent.Instance.dbType == DBType.MongoDB)
            {
                DBComponent dbComponent = DBManagerComponent.Instance.GetZoneDB(scene.DomainZone());
                return await dbComponent.Query<T>(Id);
            }
            else if (DBManagerComponent.Instance.dbType == DBType.LocalDB)
            {
                DBLocalComponent dbLocalComponent = DBManagerComponent.Instance.GetLocalDB();
                return dbLocalComponent.Query<T>(Id);
            }

            return null;
        }

        public static async ETTask<List<T>> _LoadDBList<T>(Scene scene, Expression<Func<T, bool>> filter = null) where T :Entity
        {
            if (DBManagerComponent.Instance.dbType == DBType.NoDB)
            {
                await ETTask.CompletedTask;
                return null;
            }
            else if (DBManagerComponent.Instance.dbType == DBType.MongoDB)
            {
                DBComponent dbComponent = DBManagerComponent.Instance.GetZoneDB(scene.DomainZone());
                if (filter == null)
                {
                    return await dbComponent.Query<T>((db)=>true);
                }
                else
                {
                    return await dbComponent.Query<T>(filter);
                }
            }
            else if (DBManagerComponent.Instance.dbType == DBType.LocalDB)
            {
                DBLocalComponent dbLocalComponent = DBManagerComponent.Instance.GetLocalDB();
                return dbLocalComponent.QueryAll<T>();
            }
            return null;
        }

        public static async ETTask SaveDB<T>(T entity) where T :Entity
        {
            if (DBManagerComponent.Instance.dbType == DBType.NoDB)
            {
                await ETTask.CompletedTask;
            }
            else if (DBManagerComponent.Instance.dbType == DBType.MongoDB)
            {
                DBComponent dbComponent = DBManagerComponent.Instance.GetZoneDB(entity.DomainZone());
                await dbComponent.SaveNotWait(entity);
            }
            else if (DBManagerComponent.Instance.dbType == DBType.LocalDB)
            {
                DBLocalComponent dbLocalComponent = DBManagerComponent.Instance.GetLocalDB();
                dbLocalComponent.Save(entity);
            }
        }

        public static async ETTask RemoveDB<T>(T entity) where T :Entity
        {
            if (DBManagerComponent.Instance.dbType == DBType.NoDB)
            {
                await ETTask.CompletedTask;
            }
            else if (DBManagerComponent.Instance.dbType == DBType.MongoDB)
            {
                DBComponent dbComponent = DBManagerComponent.Instance.GetZoneDB(entity.DomainZone());
                await dbComponent.Remove<T>(entity.Id);
            }
            else if (DBManagerComponent.Instance.dbType == DBType.LocalDB)
            {
                DBLocalComponent dbLocalComponent = DBManagerComponent.Instance.GetLocalDB();
                dbLocalComponent.Remove<T>(entity.Id);
            }
        }

        public static async ETTask RenameCollection(Scene scene, string oldName, string newName)
        {
            if (DBManagerComponent.Instance.dbType == DBType.NoDB)
            {
                await ETTask.CompletedTask;
            }
            else if (DBManagerComponent.Instance.dbType == DBType.MongoDB)
            {
                DBComponent dbComponent = DBManagerComponent.Instance.GetZoneDB(scene.DomainZone());
                await dbComponent.RenameCollection(oldName, newName);
            }
            else if (DBManagerComponent.Instance.dbType == DBType.LocalDB)
            {
                DBLocalComponent dbLocalComponent = DBManagerComponent.Instance.GetLocalDB();
                dbLocalComponent.RenameCollection(oldName, newName);
            }
        }

        public static async ETTask<T> _LoadDBFirst<T>(Scene scene) where T :Entity
        {
            if (DBManagerComponent.Instance.dbType == DBType.NoDB)
            {
                await ETTask.CompletedTask;
                return null;
            }
            else if (DBManagerComponent.Instance.dbType == DBType.MongoDB)
            {
                DBComponent dbComponent = DBManagerComponent.Instance.GetZoneDB(scene.DomainZone());
                return await dbComponent.QueryFirst<T>();
            }
            else if (DBManagerComponent.Instance.dbType == DBType.LocalDB)
            {
                DBLocalComponent dbLocalComponent = DBManagerComponent.Instance.GetLocalDB();
                return dbLocalComponent.QueryFirst<T>();
            }
            return null;
        }

        public static async ETTask RenameCollection(Scene scene, Type entityType, int seasonIndex)
        {
            if (DBManagerComponent.Instance.dbType == DBType.NoDB)
            {
                await ETTask.CompletedTask;
            }
            else if (DBManagerComponent.Instance.dbType == DBType.MongoDB)
            {
                DBComponent dbComponent = DBManagerComponent.Instance.GetZoneDB(scene.DomainZone());
                string oldName = entityType.FullName;
                string newName = $"{oldName}_Season{seasonIndex}";
                await dbComponent.RenameCollection(oldName, newName);
            }
            else if (DBManagerComponent.Instance.dbType == DBType.LocalDB)
            {
                DBLocalComponent dbLocalComponent = DBManagerComponent.Instance.GetLocalDB();
                string oldName = entityType.FullName;
                string newName = $"{oldName}_Season{seasonIndex}";
                dbLocalComponent.RenameCollection(oldName, newName);
            }
        }

        public static async ETTask DropCollection<T>(Scene scene) where T :Entity
        {
            if (DBManagerComponent.Instance.dbType == DBType.NoDB)
            {
                await ETTask.CompletedTask;
            }
            else if (DBManagerComponent.Instance.dbType == DBType.MongoDB)
            {
                DBComponent dbComponent = DBManagerComponent.Instance.GetZoneDB(scene.DomainZone());
                await dbComponent.DropCollection<T>();
            }
            else if (DBManagerComponent.Instance.dbType == DBType.LocalDB)
            {
                DBLocalComponent dbLocalComponent = DBManagerComponent.Instance.GetLocalDB();
                dbLocalComponent.DropCollection<T>();
            }
        }

        public static async ETTask DropCollection(Scene scene, string collection)
        {
            if (DBManagerComponent.Instance.dbType == DBType.NoDB)
            {
                await ETTask.CompletedTask;
            }
            else if (DBManagerComponent.Instance.dbType == DBType.MongoDB)
            {
                DBComponent dbComponent = DBManagerComponent.Instance.GetZoneDB(scene.DomainZone());
                await dbComponent.DropCollection(collection);
            }
            else if (DBManagerComponent.Instance.dbType == DBType.LocalDB)
            {
                DBLocalComponent dbLocalComponent = DBManagerComponent.Instance.GetLocalDB();
                dbLocalComponent.DropCollection(collection);
            }
        }

    }
}