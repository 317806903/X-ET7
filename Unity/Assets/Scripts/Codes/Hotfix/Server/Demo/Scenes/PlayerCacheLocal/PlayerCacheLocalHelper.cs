using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Server
{
    public static class PlayerCacheLocalHelper
    {
	    public static PlayerCacheManagerComponent GetPlayerCacheManager(Scene scene)
	    {
		    PlayerCacheManagerComponent playerCacheManagerComponent = scene.GetComponent<PlayerCacheManagerComponent>();
		    return playerCacheManagerComponent;
	    }

        public static async ETTask<Entity> GetPlayerModel(Scene scene, long playerId, PlayerModelType playerModelType)
        {
	        PlayerCacheManagerComponent playerCacheManagerComponent = GetPlayerCacheManager(scene);

	        PlayerDataComponent playerDataComponent = playerCacheManagerComponent.GetPlayerData(playerId);
	        if (playerDataComponent == null)
	        {
		        playerDataComponent = await playerCacheManagerComponent.AddPlayerData(playerId);
	        }

	        Entity entityModel = playerDataComponent.GetPlayerModel(playerModelType);

	        return entityModel;
        }

        public static async ETTask SetPlayerModel(Scene scene, long playerId, PlayerModelType playerModelType, byte[] bytes, List<string> setPlayerKeys)
        {
	        PlayerCacheManagerComponent playerCacheManagerComponent = GetPlayerCacheManager(scene);

	        PlayerDataComponent playerDataComponent = playerCacheManagerComponent.GetPlayerData(playerId);
	        if (playerDataComponent == null)
	        {
		        return;
	        }

	        Entity entityModel = playerDataComponent.SetPlayerModel(playerModelType, bytes, setPlayerKeys);
	        DataCacheWriteComponent dataCacheWriteComponent = entityModel.GetComponent<DataCacheWriteComponent>();
	        if (dataCacheWriteComponent == null)
	        {
		        dataCacheWriteComponent = entityModel.AddComponent<DataCacheWriteComponent>();
	        }
	        dataCacheWriteComponent.SetNeedSave();
	        await ETTask.CompletedTask;
        }

    }
}