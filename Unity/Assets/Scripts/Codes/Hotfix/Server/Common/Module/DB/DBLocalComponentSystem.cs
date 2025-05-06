using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace ET.Server
{
	[FriendOf(typeof(DBLocalComponent))]
    public static class DBLocalComponentSystem
    {
	    public class DBLocalComponentAwakeSystem : AwakeSystem<DBLocalComponent>
	    {
			protected override void Awake(DBLocalComponent self)
			{
		    }
	    }

	    public static void SetSavePath(this DBLocalComponent self, string savePath)
	    {
		    self.savePath = savePath;
	    }

	    public static string GetCollectionPath(this DBLocalComponent self, string collectionName)
	    {
		    string path = Path.Combine(self.savePath, collectionName);
		    return path;
	    }

	    public static string GetFilePath(this DBLocalComponent self, string collectionName, long id)
	    {
		    string file = Path.Combine(self.savePath, collectionName, id.ToString());
		    return file;
	    }

	    public static bool CollectionExists(this DBLocalComponent self, string collectionName)
	    {
		    string path = self.GetCollectionPath(collectionName);
		    if (Directory.Exists(path))
		    {
			    return true;
		    }

		    return false;
	    }

	    public static bool FileExists(this DBLocalComponent self, string collectionName, long id)
	    {
		    if (self.CollectionExists(collectionName) == false)
		    {
			    return false;
		    }
		    string file = self.GetFilePath(collectionName, id);
		    if (File.Exists(file))
		    {
			    return true;
		    }

		    return false;
	    }

	    public static byte[] GetCollectionBytesById(this DBLocalComponent self, string collectionName, long id)
	    {
		    if (self.FileExists(collectionName, id) == false)
		    {
			    return null;
		    }
		    string file = self.GetFilePath(collectionName, id);
		    return File.ReadAllBytes(file);
	    }

	    public static void SaveFileByBytes(this DBLocalComponent self, string collectionName, long id, byte[] bytes)
	    {
		    string file = self.GetFilePath(collectionName, id);
		    FileHelper.CreateDirectory(file);
		    File.WriteAllBytes(file, bytes);
	    }

	    #region RenameCollection
	    public static void RenameCollection(this DBLocalComponent self, string oldName, string newName)
	    {
		    if (self.CollectionExists(oldName) == false)
		    {
			    Log.Error($"RenameCollection self.CollectionExists({oldName}) == false");
			    return;
		    }
		    if (self.CollectionExists(newName))
		    {
			    Log.Error($"RenameCollection self.CollectionExists({newName})");
			    return;
		    }
		    string sourceFileName = Path.Combine(self.savePath, oldName);
		    string destFileName = Path.Combine(self.savePath, newName);
		    File.Move(sourceFileName, destFileName);
	    }
	    #endregion

	    #region Query

	    public static T Query<T>(this DBLocalComponent self, long id, string collection = null) where T : Entity
	    {
		    string name = collection ?? typeof (T).FullName;
		    byte[] bytes = self.GetCollectionBytesById(name, id);
		    if (bytes == null)
		    {
			    return null;
		    }
		    Entity entity = MongoHelper.Deserialize<Entity>(bytes);
		    return entity as T;
	    }

	    public static T QueryFirst<T>(this DBLocalComponent self, string collection = null) where T : Entity
	    {
		    string name = collection ?? typeof (T).FullName;
		    if (self.CollectionExists(name) == false)
		    {
			    return null;
		    }
		    string path = self.GetCollectionPath(name);
		    List<string> list = FileHelper.GetAllFiles(path);
		    foreach (string file in list)
		    {
			    if (file.EndsWith(".meta"))
			    {
				    continue;
			    }
			    string fileOne = Path.GetFileName(file);
			    long id = long.Parse(fileOne);
			    byte[] bytes = self.GetCollectionBytesById(name, id);
			    Entity entity = MongoHelper.Deserialize<Entity>(bytes);
			    return entity as T;
		    }

		    return null;
	    }

	    public static List<T> QueryAll<T>(this DBLocalComponent self, string collection = null) where T : Entity
	    {
		    string name = collection ?? typeof (T).FullName;
		    if (self.CollectionExists(name) == false)
		    {
			    return null;
		    }
		    string path = self.GetCollectionPath(name);
		    List<string> list = FileHelper.GetAllFiles(path);
		    List<T> listEntity = new();
		    foreach (string file in list)
		    {
			    if (file.EndsWith(".meta"))
			    {
				    continue;
			    }
			    string fileOne = Path.GetFileName(file);
			    long id = long.Parse(fileOne);
			    byte[] bytes = self.GetCollectionBytesById(name, id);
			    Entity entity = MongoHelper.Deserialize<Entity>(bytes);
			    listEntity.Add(entity as T);
		    }

		    return listEntity;
	    }

	    #endregion

	    #region QueryCount

	    public static long QueryCount<T>(this DBLocalComponent self, string collection = null) where T : Entity
	    {
		    string name = collection ?? typeof (T).FullName;
		    if (self.CollectionExists(name) == false)
		    {
			    return 0;
		    }
		    string path = self.GetCollectionPath(name);
		    int count = 0;
		    List<string> list = FileHelper.GetAllFiles(path);
		    foreach (string file in list)
		    {
			    if (file.EndsWith(".meta"))
			    {
				    continue;
			    }
			    count++;
		    }
		    return count;
	    }

	    #endregion

	    #region Save

	    public static void Save<T>(this DBLocalComponent self, T entity, string collection = null) where T : Entity
	    {
		    if (entity == null)
		    {
			    Log.Error($"save entity is null: {typeof (T).FullName}");

			    return;
		    }

		    if (collection == null)
		    {
			    collection = entity.GetType().FullName;
		    }

		    entity.BeginInit();

		    self.SaveFileByBytes(collection, entity.Id, entity.ToBson());
	    }

	    #endregion

	    #region Remove

	    public static void Remove<T>(this DBLocalComponent self, long id, string collection = null) where T : Entity
	    {
		    if (collection == null)
		    {
			    collection = typeof(T).FullName;
		    }
		    if (self.FileExists(collection, id) == false)
		    {
			    return;
		    }
		    string file = self.GetFilePath(collection, id);
		    File.Delete(file);
	    }

	    #endregion

	    #region DropCollection
	    public static void DropCollection(this DBLocalComponent self, string collection)
	    {
		    if (self.CollectionExists(collection) == false)
		    {
			    Log.Error($"DropCollection self.CollectionExists({collection}) == false");
			    return;
		    }

		    string path = self.GetCollectionPath(collection);
		    FileHelper.CleanDirectory(path);
	    }

	    public static void DropCollection<T>(this DBLocalComponent self) where T : Entity
	    {
		    string collection = typeof(T).FullName;
		    self.DropCollection(collection);
	    }
	    #endregion

    }
}