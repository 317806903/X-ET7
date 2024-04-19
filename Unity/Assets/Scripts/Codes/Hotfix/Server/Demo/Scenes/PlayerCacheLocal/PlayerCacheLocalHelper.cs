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

	    public static async ETTask<PlayerDataComponent> GetPlayerCache(Scene scene, long playerId)
	    {
		    PlayerCacheManagerComponent playerCacheManagerComponent = GetPlayerCacheManager(scene);
		    PlayerDataComponent playerDataComponent = playerCacheManagerComponent.GetPlayerData(playerId);
		    if (playerDataComponent == null)
		    {
			    playerDataComponent = await playerCacheManagerComponent.AddPlayerData(playerId);
		    }
		    return playerDataComponent;
	    }

        public static async ETTask<Entity> GetPlayerModel(Scene scene, long playerId, PlayerModelType playerModelType)
        {
	        PlayerDataComponent playerDataComponent = await GetPlayerCache(scene, playerId);

	        Entity entityModel = playerDataComponent.GetPlayerModel(playerModelType);

	        return entityModel;
        }

        public static async ETTask SetPlayerModel(Scene scene, long playerId, PlayerModelType playerModelType, byte[] bytes, List<string> setPlayerKeys, PlayerModelChgType playerModelChgType)
        {
	        PlayerDataComponent playerDataComponent = await GetPlayerCache(scene, playerId);

	        Entity entityModel = playerDataComponent.SetPlayerModel(playerModelType, bytes, setPlayerKeys);
	        entityModel.SetDataCacheAutoWrite();
	        await ETTask.CompletedTask;
        }

    }
}