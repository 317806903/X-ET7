using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ET
{
    public static class MenuEditor
    {
		[MenuItem("GameObject/==Copy节点相对prefab的路径", false, 36)]
		public static void CopyPathByPrefab()
		{
			GameObject[] objs = Selection.gameObjects;
			List<string> resultList = new();
			for (int i = 0; i < objs.Length; i++)
			{
				GameObject obj = objs[i];
				if (obj == null)
				{
					Log.Error("You must select Obj first!");
					continue;
				}
				string result = AssetDatabase.GetAssetPath(obj);
				if (string.IsNullOrEmpty(result))//如果不是资源则在场景中查找
				{
					Transform selectChild = obj.transform;
					if (selectChild != null)
					{
						result = selectChild.name;
						while (selectChild.parent != null)
						{
							selectChild = selectChild.parent;
							result = string.Format("{0}/{1}", selectChild.name, result);
						}
					}
				}
				resultList.Add(result);
			}

			resultList.Sort();
			string resultAll = "";
			for (int i = 0; i < resultList.Count; i++)
			{
				resultAll = string.Format("{0}\n{1}", resultAll, resultList[i]);
			}
			//ClipBoard.Copy(result);
			GUIUtility.systemCopyBuffer = resultAll;
			Debug.Log(string.Format("The gameobject:{0}'s path has been copied to the clipboard!", resultAll));
		}
    }
}