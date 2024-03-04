using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Client
{
    public static class RankHelper
    {
	    public static RankShowPlayerComponent GetRankShowPlayerManager(Scene scene)
	    {
		    Scene currentScene = null;
		    Scene clientScene = null;
		    if (scene == scene.ClientScene())
		    {
			    currentScene = scene.GetComponent<CurrentScenesComponent>().Scene;
			    clientScene = scene;
		    }
		    else
		    {
			    currentScene = scene;
			    clientScene = currentScene.Parent.GetParent<Scene>();
		    }

		    RankShowPlayerComponent rankShowPlayerComponent = clientScene.GetComponent<RankShowPlayerComponent>();
		    if (rankShowPlayerComponent == null)
		    {
			    rankShowPlayerComponent = clientScene.AddComponent<RankShowPlayerComponent>();
		    }
		    return rankShowPlayerComponent;
	    }

        public static async ETTask<RankShowComponent> GetRankShow(Scene scene, RankType rankType, bool forceReGet)
        {
	        using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.Rank, (int)rankType))
	        {
		        RankShowPlayerComponent rankShowPlayerComponent = GetRankShowPlayerManager(scene);
		        RankShowComponent rankShowComponent = rankShowPlayerComponent.GetRankShow(rankType);
		        if (rankShowComponent == null || forceReGet)
		        {
			        (bool bRet, Entity entityModel) = await SendGetRankShowAsync(scene, rankType);
			        if (bRet)
			        {
				        if (rankShowComponent != null)
				        {
					        rankShowComponent.Dispose();
				        }
				        long myPlayerId = ET.Client.PlayerHelper.GetMyPlayerId(scene);
				        rankShowComponent = rankShowPlayerComponent.SetRankShow(myPlayerId, rankType, (RankShowComponent)entityModel);
			        }
		        }

		        return rankShowComponent;
	        }
        }

        public static async ETTask<(ulong, int)> GetRankedMoreThan(Scene scene, RankType rankType, long score)
        {
	        (bool bRet, ulong rank, int rankedMoreThan) = await SendGetRankedMoreThanAsync(scene, rankType, score);
	        if (bRet)
	        {
		        return (rank, rankedMoreThan);
	        }

	        return (0, 0);
        }

        public static async ETTask<(bool, Entity)> SendGetRankShowAsync(Scene clientScene, RankType rankType)
        {
	        G2C_GetRank _G2C_GetRank = await ET.Client.SessionHelper.GetSession(clientScene).Call(new C2G_GetRank()
		        {
			        RankType = (int)rankType,
		        }) as
		        G2C_GetRank;
	        if (_G2C_GetRank.Error != ET.ErrorCode.ERR_Success)
	        {
		        Log.Error($"SendGetRankShowAsync Error==1 msg={_G2C_GetRank.Message}");
		        return (false, null);
	        }
	        else
	        {
		        byte[] rankShowComponentBytes = _G2C_GetRank.RankShowComponentBytes;
		        Entity entity = MongoHelper.Deserialize<Entity>(rankShowComponentBytes);
		        return (true, entity);
	        }
        }

        public static async ETTask<(bool, ulong, int)> SendGetRankedMoreThanAsync(Scene clientScene, RankType rankType, long score)
        {
	        G2C_GetRankedMoreThan _G2C_GetRankedMoreThan = await ET.Client.SessionHelper.GetSession(clientScene).Call(new C2G_GetRankedMoreThan()
		        {
			        RankType = (int)rankType,
			        Score = score,
		        }) as
		        G2C_GetRankedMoreThan;
	        if (_G2C_GetRankedMoreThan.Error != ET.ErrorCode.ERR_Success)
	        {
		        Log.Error($"SendGetRankShowAsync Error==1 msg={_G2C_GetRankedMoreThan.Message}");
		        return (false, 0, 0);
	        }
	        else
	        {
		        ulong rank = (ulong)_G2C_GetRankedMoreThan.Rank;
		        int rankedMoreThan = _G2C_GetRankedMoreThan.RankedMoreThan;
		        return (true, rank, rankedMoreThan);
	        }
        }

    }
}