using System.Collections.Generic;
using System.Linq;

namespace ET.Server
{
    [FriendOf(typeof(RankManagerComponent))]
    public static class RankManagerComponentSystem
    {
        [ObjectSystem]
        public class RankManagerComponentAwakeSystem : AwakeSystem<RankManagerComponent>
        {
            protected override void Awake(RankManagerComponent self)
            {
            }
        }

        public static async ETTask LoadRank(this RankManagerComponent self, RankType rankType)
        {
            switch (rankType)
            {
                case RankType.PVE:
                {
                    RankPVEComponent rankEndlessChallengeComponent = await self.InitByDBOne<RankPVEComponent, RankPVEItemComponent>();
                    break;
                }
                case RankType.EndlessChallenge:
                {
                    RankEndlessChallengeComponent rankEndlessChallengeComponent = await self.InitByDBOne<RankEndlessChallengeComponent, RankEndlessChallengeItemComponent>();
                    break;
                }
                default:
                    break;
            }

            await ETTask.CompletedTask;
        }

        public static async ETTask ResetRankItem(this RankManagerComponent self, RankType rankType, long playerId, long scoreNew, int killNum)
        {
            switch (rankType)
            {
                case RankType.PVE:
                    await self.ResetRankItem<RankPVEComponent, RankPVEItemComponent>(playerId, scoreNew, killNum);
                    break;
                case RankType.EndlessChallenge:
                    await self.ResetRankItem<RankEndlessChallengeComponent, RankEndlessChallengeItemComponent>(playerId, scoreNew, killNum);
                    break;
                default:
                    break;
            }

            await ETTask.CompletedTask;
        }

        public static async ETTask<T> InitByDBOne<T, TItem>(this RankManagerComponent self) where T : RankComponent, new() where TItem : RankItemComponent, new()
        {
            T rankComponent;
            List<T> list = await ET.Server.DBHelper.LoadDBListWithParent2Component<T>(self);
            if (list == null || list.Count == 0)
            {
                rankComponent = self.AddComponent<T>();
                rankComponent.AddComponent<DataCacheWriteComponent>();
            }
            else
            {
                rankComponent = list[0];
            }
            await rankComponent.Init<TItem>();
            return rankComponent;
        }

        public static async ETTask ResetRankItem<T, TItem>(this RankManagerComponent self, long playerId, long scoreNew, int killNum) where T : RankComponent where TItem : RankItemComponent, new()
        {
            T rankComponent = self.GetComponent<T>();
            await rankComponent.ResetRankItem<TItem>(playerId, scoreNew, killNum);
        }

    }
}