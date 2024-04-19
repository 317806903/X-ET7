namespace ET
{
    public static class DataCacheHelper
    {
        public static void SetDataCacheAutoWrite(this Entity entity)
        {
            DataCacheWriteComponent dataCacheWriteComponent = entity.GetComponent<DataCacheWriteComponent>();
            if (dataCacheWriteComponent == null)
            {
                dataCacheWriteComponent = entity.AddComponent<DataCacheWriteComponent>();
            }
            dataCacheWriteComponent.SetNeedSave();
        }

        public static void SetDataCacheAutoClear(this Entity entity, float chkTimeInterval = 30)
        {
            if (chkTimeInterval < 0)
            {
                Log.Error($"ET.DataCacheHelper.SetDataCacheAutoClear chkTimeInterval[{chkTimeInterval}] < 0");
                return;
            }
            DataCacheClearComponent dataCacheClearComponent = entity.GetComponent<DataCacheClearComponent>();
            if (dataCacheClearComponent == null)
            {
                dataCacheClearComponent = entity.AddComponent<DataCacheClearComponent>();
            }

            if (chkTimeInterval == 0)
            {
                dataCacheClearComponent.RefreshTime();
            }
            else
            {
                dataCacheClearComponent.ResetChkTimeInterval(chkTimeInterval);
            }
        }

        public static void ClearDataCache(this Entity entity)
        {
            DataCacheClearComponent dataCacheClearComponent = entity.GetComponent<DataCacheClearComponent>();
            if (dataCacheClearComponent == null)
            {
                return;
            }

            dataCacheClearComponent.ClearDataCache();
        }

    }
}