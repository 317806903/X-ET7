using System;
using System.Collections.Generic;
using UnityEngine;

namespace ET.Client
{
    public static class GameObjectPoolHelper
    {
        private static Dictionary<string, GameObjectPool> poolDict = new Dictionary<string, GameObjectPool>();
        private static GameObject _poolRoot;
        private static GameObject poolRoot
        {
            get
            {
                if (_poolRoot == null)
                {
                    _poolRoot = GameObject.Find("/Init/GlobalRoot/PoolRoot");
                }
                return _poolRoot;
            }
        }

        public static void InitPool(string poolName, int size, PoolInflationType type = PoolInflationType.DOUBLE)
        {
            if (poolDict.ContainsKey(poolName))
            {
                return;
            }
            else
            {
                try
                {
                    GameObject pb = GetGameObjectByResType(poolName);
                    if (pb == null)
                    {
                        Debug.LogError("[ResourceManager] Invalide prefab name for pooling :" + poolName);
                        return;
                    }

                    poolDict[poolName] = new GameObjectPool(poolName, pb, poolRoot, size, type);
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }
        }

        public static async ETTask InitPoolFormGamObjectAsync(GameObject pb, int size, PoolInflationType type = PoolInflationType.DOUBLE)
        {
            string poolName = pb.name;
            if (poolDict.ContainsKey(poolName))
            {
                return;
            }
            else
            {
                try
                {
                    if (pb == null)
                    {
                        Debug.LogError("[ResourceManager] Invalide prefab name for pooling :" + poolName);
                        return;
                    }

                    poolDict[poolName] = new GameObjectPool(poolName, pb, poolRoot, size, type);
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }

            await ETTask.CompletedTask;
        }

        /// <summary>
        /// Returns an available object from the pool
        /// OR null in case the pool does not have any object available & can grow size is false.
        /// </summary>
        /// <OtherParam name="poolName"></OtherParam>
        /// <returns></returns>
        public static GameObject GetObjectFromPool(string poolName, bool autoActive = true, int autoCreate = 0)
        {
            GameObject result = null;

            bool bRet = poolDict.ContainsKey(poolName);
            if (bRet)
            {
                GameObjectPool pool = poolDict[poolName];
                bRet = pool.ChkObjAvailability();
                if (bRet == false)
                {
                    poolDict.Remove(poolName);
                    InitPool(poolName, autoCreate, PoolInflationType.INCREMENT);
                }
            }
            else if (autoCreate > 0)
            {
                InitPool(poolName, autoCreate, PoolInflationType.INCREMENT);
            }

            if (poolDict.ContainsKey(poolName))
            {
                GameObjectPool pool = poolDict[poolName];
                result = pool.NextAvailableObject(autoActive);

                TrigFromPool(result);

                //scenario when no available object is found in pool
#if UNITY_EDITOR
                if (result == null)
                {
                    Debug.LogWarning("[ResourceManager]:No object available in " + poolName);
                }
#endif
            }
#if UNITY_EDITOR
            else
            {
                Debug.LogError("[ResourceManager]:Invalid pool name specified: " + poolName);
            }
#endif
            return result;
        }

        public static void TrigFromPool(GameObject go)
        {
            if (go == null)
            {
                return;
            }
            PoolObject po = go.GetComponent<PoolObject>();
            if (po == null)
            {
                return;
            }

            if (po.particleSystemList != null)
            {
                foreach (ParticleSystem particleSystem in po.particleSystemList)
                {
                    if (particleSystem != null)
                    {
                        particleSystem.Play();
                    }
                }
            }

            if (po.trailRendererList != null)
            {
                foreach (TrailRenderer trailRenderer in po.trailRendererList)
                {
                    if (trailRenderer != null)
                    {
                        trailRenderer.Clear();
                        trailRenderer.enabled = true;
                    }
                }
            }
        }

        public static void TrigToPool(PoolObject po)
        {
            if (po.particleSystemList != null)
            {
                foreach (ParticleSystem particleSystem in po.particleSystemList)
                {
                    if (particleSystem != null)
                    {
                        particleSystem.Stop();
                    }
                }
            }

            if (po.trailRendererList != null)
            {
                foreach (TrailRenderer trailRenderer in po.trailRendererList)
                {
                    if (trailRenderer != null)
                    {
                        trailRenderer.Clear();
                        trailRenderer.enabled = false;
                    }
                }
            }
        }

        /// <summary>
        /// Return obj to the pool
        /// </summary>
        /// <OtherParam name="go"></OtherParam>
        public static void ReturnObjectToPool(GameObject go)
        {
            PoolObject po = go.GetComponent<PoolObject>();
            if (po == null)
            {
#if UNITY_EDITOR
                Debug.LogWarning("Specified object is not a pooled instance: " + go.name);
#endif
            }
            else
            {
                TrigToPool(po);

                GameObjectPool pool = null;
                if (poolDict.TryGetValue(po.poolName, out pool))
                {
                    pool.ReturnObjectToPool(po);
                }
#if UNITY_EDITOR
                else
                {
                    Debug.LogWarning("No pool available with name: " + po.poolName);
                }
#endif
            }
        }

        /// <summary>
        /// Return obj to the pool
        /// </summary>
        /// <OtherParam name="t"></OtherParam>
        public static void ReturnTransformToPool(Transform t)
        {
            if (t == null)
            {
#if UNITY_EDITOR
                Debug.LogError("[ResourceManager] try to return a null transform to pool!");
#endif
                return;
            }

            ReturnObjectToPool(t.gameObject);
        }

        public static GameObject GetGameObjectByResType(string poolName)
        {
            // GameObject pb = null;
            // Dictionary<string, UnityEngine.Object>  assetDict = AssetsBundleHelper.LoadBundle(poolName + ".unity3d");
            // pb = assetDict[poolName] as GameObject;
            //
            //
            GameObject go = EventSystem.Instance.Invoke<ConfigComponent.GetRes, GameObject>(new ConfigComponent.GetRes() { ResName = poolName });
            return go;
        }

        public static void ShowPoolDictCount(int showCount)
        {
            foreach (var item in poolDict)
            {
                GameObjectPool gameObjectPool = item.Value;
                if (gameObjectPool.GetAvailableObjStackCount() >= showCount)
                {
                    Log.Debug($"ShowPoolDictCount [{item.Key}] count[{gameObjectPool.GetAvailableObjStackCount()}]");
                }
            }
        }

        public static void ResetPoolDictCount(int maxCount)
        {
            foreach (var item in poolDict)
            {
                GameObjectPool gameObjectPool = item.Value;
                if (gameObjectPool.GetAvailableObjStackCount() > maxCount)
                {
                    gameObjectPool.ResetPoolDictCount(maxCount);
                }
            }
        }

        public static void ResetPoolDictCount(string poolName, int maxCount)
        {
            if (poolDict.TryGetValue(poolName, out GameObjectPool gameObjectPool))
            {
                if (gameObjectPool.GetAvailableObjStackCount() > maxCount)
                {
                    gameObjectPool.ResetPoolDictCount(maxCount);
                }
            }
        }
    }
}