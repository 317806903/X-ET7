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
		        (int myRank, RankItemComponent myRankItemComponent) = rankManagerComponent.GetMyRankShow(playerId, rankType);

		        rankShowComponent = rankShowManagerComponent.SetRankShow(playerId, rankType, myRank, myRankItemComponent, rankIndex2PlayerId);
	        }

	        return rankShowComponent;
        }

        public static async ETTask<(ulong, int)> GetRankedMoreThan(Scene scene, RankType rankType, long score, long playerId)
        {
	        RankManagerComponent rankManagerComponent = GetRankManager(scene);

	        bool bRet = rankManagerComponent.ChkRankExist(rankType);
	        if (bRet == false)
	        {
		        await rankManagerComponent.LoadRank(rankType);
	        }
	        (ulong rank, int rankedMoreThan) = rankManagerComponent.GetRankedMoreThan(rankType, score, playerId);
	        return (rank, rankedMoreThan);
        }

        public static async ETTask ResetPlayerRank(Scene scene, long playerId, RankType rankType, long score, int killNum)
        {
	        RankManagerComponent rankManagerComponent = GetRankManager(scene);

	        bool bRet = rankManagerComponent.ChkRankExist(rankType);
	        if (bRet == false)
	        {
		        await rankManagerComponent.LoadRank(rankType);
	        }

	        await rankManagerComponent.ResetRankItem(rankType, playerId, score, killNum);

	        RankShowManagerComponent rankShowManagerComponent = GetRankShowManager(scene);
	        rankShowManagerComponent.RemoveRankShow(playerId, rankType);
        }

        public static async ETTask RecordWhenSeasonFinished(Scene scene, RankType rankType, int seasonIndex, int seasonCfgId)
        {
	        RankManagerComponent rankManagerComponent = GetRankManager(scene);

	        bool bRet = rankManagerComponent.ChkRankExist(rankType);
	        if (bRet == false)
	        {
		        await rankManagerComponent.LoadRank(rankType);
	        }

	        await rankManagerComponent.RecordWhenSeasonFinished(rankType, seasonIndex, seasonCfgId);

	        RankShowManagerComponent rankShowManagerComponent = GetRankShowManager(scene);
	        rankShowManagerComponent?.Dispose();
	        scene.AddComponent<RankShowManagerComponent>();
        }

        public static async ETTask<HashSet<long>> ClearRankWhenDebug(Scene scene, RankType rankType)
        {
	        RankManagerComponent rankManagerComponent = GetRankManager(scene);

	        bool bRet = rankManagerComponent.ChkRankExist(rankType);
	        if (bRet == false)
	        {
		        await rankManagerComponent.LoadRank(rankType);
	        }

	        RankComponent rankComponent = rankManagerComponent.GetRank(rankType);

	        HashSetComponent<long> allPlayer = HashSetComponent<long>.Create();
	        foreach (long playerId in rankComponent.topRankPlayerList)
	        {
		        allPlayer.Add(playerId);
	        }
	        foreach (long playerId in rankComponent.playerId2Score.Keys)
	        {
		        allPlayer.Add(playerId);
	        }

	        var list = rankComponent.SkipList.GetListShow();
	        foreach (var skipListNode in list[list.Count-1])
	        {
		        long playerId = ((RankItemComponent)skipListNode.obj).playerId;
		        allPlayer.Add(playerId);
	        }

	        rankComponent.topRankPlayerList.Clear();
	        rankComponent.playerId2Score.Clear();
	        rankComponent.SkipList = SkipList.CreateList(true);

	        ListComponent<long> removeChild = ListComponent<long>.Create();
	        foreach (var item in rankComponent.Children)
	        {
		        removeChild.Add(item.Key);
	        }
	        foreach (long id in removeChild)
	        {
		        rankComponent.RemoveChild(id);
	        }
	        rankComponent.SetDataCacheAutoWrite();

	        RankShowManagerComponent rankShowManagerComponent = GetRankShowManager(scene);
	        foreach (var item in rankShowManagerComponent.Children)
	        {
		        removeChild.Add(item.Key);
	        }
	        foreach (long id in removeChild)
	        {
		        rankShowManagerComponent.RemoveChild(id);
	        }

	        return allPlayer;
        }

    }
}