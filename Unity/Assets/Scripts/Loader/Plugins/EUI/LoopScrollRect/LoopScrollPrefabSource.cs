using System;
using UnityEngine;
using System.Collections;
using ET;
using ET.Client;

namespace UnityEngine.UI
{
    public interface LoopScrollPrefabSource
    {
        GameObject GetObject(int index);

        void ReturnObject(Transform trans,bool isDestroy = false);
    }



    [System.Serializable]
    public class LoopScrollPrefabSourceInstance : LoopScrollPrefabSource
    {
        public string prefabName;
        public int poolSize = 5;

        public static Action<GameObject> AddCellItemAction;
        public static Action<GameObject> RemoveCellItemAction;

        private bool inited = false;
        public virtual GameObject GetObject(int index)
        {
            try
            {
                if(!inited)
                {
                    GameObjectPoolHelper.InitPool(prefabName, poolSize);
                    inited = true;
                }
                GameObject go =  GameObjectPoolHelper.GetObjectFromPool(prefabName);
                if (go != null)
                {
                    AddCellItemAction?.Invoke(go);
                }
                return go;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                return null;
            }
        }

        public virtual void ReturnObject(Transform go , bool isDestroy = false)
        {
            try
            {
                RemoveCellItemAction?.Invoke(go.gameObject);
                if (isDestroy)
                {
                    UnityEngine.GameObject.Destroy(go.gameObject);
                }
                else
                {
                    GameObjectPoolHelper.ReturnObjectToPool(go.gameObject);
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }

}
