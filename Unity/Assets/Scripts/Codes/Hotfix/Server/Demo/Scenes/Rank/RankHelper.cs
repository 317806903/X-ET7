using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Server
{
    public static class RankHelper
    {
	    public static RankManagerComponent GetRankManager(Scene scene)
	    {
		    RankManagerComponent rankManagerComponent = scene.GetComponent<RankManagerComponent>();
		    return rankManagerComponent;
	    }

	    public static RankShowManagerComponent GetRankShowManager(Scene scene)
	    {
		    RankShowManagerComponent rankShowManagerComponent = scene.GetComponent<RankShowManagerComponent>();
		    return rankShowManagerComponent;
	    }

        public static async ETTask<RankShowComponent> GetRankShow(Scene scene, long playerId, RankType rankType)
        {
	        RankManagerComponent rankManagerComponent = GetRankManager(scene);
	        RankShowManagerComponent rankShowManagerComponent = GetRankShowManager(scene);

	        RankShowComponent rankShowComponent = rankShowManagerComponent.GetRankShow(playerId, rankType);
	        if (rankShowComponent == null)
	        {
		        bool bRet = rankManagerComponent.ChkRankExist(rankType);
		        if (bRet == false)
		        {
			        await rankManagerComponent.LoadRank(rankType);
		        }
		        SortedDictionary<int, RankItemComponent> rankIndex2PlayerId = rankManagerComponent.GetRankShow(playerId, rankType);

		        rankShowComponent = rankShowManagerComponent.SetRankShow(playerId, rankType, rankIndex2PlayerId);
	        }

	        return rankShowComponent;
        }

        public static async ETTask ResetPlayerRank(Scene scene, long playerId, RankType rankType, long score)
        {
	        RankManagerComponent rankManagerComponent = GetRankManager(scene);

	        bool bRet = rankManagerComponent.ChkRankExist(rankType);
	        if (bRet == false)
	        {
		        await rankManagerComponent.LoadRank(rankType);
	        }

	        await rankManagerComponent.ResetRankItem(rankType, playerId, score);
        }

    }
}