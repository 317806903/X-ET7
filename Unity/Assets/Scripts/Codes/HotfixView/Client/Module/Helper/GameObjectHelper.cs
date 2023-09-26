using System;
using UnityEngine;

namespace ET.Client
{
	public static class GameObjectHelper
	{
		public static T Get<T>(this GameObject gameObject, string key) where T : class
		{
			try
			{
				return gameObject.GetComponent<ReferenceCollector>().Get<T>(key);
			}
			catch (Exception e)
			{
				throw new Exception($"获取{gameObject.name}的ReferenceCollector key失败, key: {key}", e);
			}
		}

		public static void SetLayer(this GameObject go, int layer, bool isRecursivelyChilds)
		{
			go.layer = layer;
			if (isRecursivelyChilds)
			{
				Transform t = go.transform;
				for (int i = 0; i < t.childCount; i++)
					SetLayer(t.GetChild(i).gameObject, layer, isRecursivelyChilds);
			}
		}
	}
}