using System.Collections.Generic;
using System.Linq;

namespace ET.Server
{
    [FriendOf(typeof(GameJudgeChooseManagerComponent))]
    public static class GameJudgeChooseManagerComponentSystem
    {
        [ObjectSystem]
        public class GameJudgeChooseManagerComponentAwakeSystem : AwakeSystem<GameJudgeChooseManagerComponent>
        {
            protected override void Awake(GameJudgeChooseManagerComponent self)
            {
            }
        }

        public static async ETTask RecordGameJudgeChoose(this GameJudgeChooseManagerComponent self, long playerId, GameJudgeChooseType gameJudgeChooseType, string complainMsg)
        {
            if (DBManagerComponent.Instance.NeedDB == false)
            {
                await self.RecordGameJudgeChooseNoDB(playerId, gameJudgeChooseType, complainMsg);
            }
            else
            {
                await self.RecordGameJudgeChooseWithDB(playerId, gameJudgeChooseType, complainMsg);
            }
        }

        public static async ETTask RecordGameJudgeChooseWithDB(this GameJudgeChooseManagerComponent self, long playerId, GameJudgeChooseType gameJudgeChooseType, string complainMsg)
        {
            GameJudgeChooseComponent gameJudgeChooseComponent = await self.GetGameJudgeChooseComponent(playerId);
            gameJudgeChooseComponent.Record(gameJudgeChooseType, complainMsg);

            gameJudgeChooseComponent.SetDataCacheAutoWrite();
        }

        public static async ETTask RecordGameJudgeChooseNoDB(this GameJudgeChooseManagerComponent self, long playerId, GameJudgeChooseType gameJudgeChooseType, string complainMsg)
        {
            GameJudgeChooseComponent gameJudgeChooseComponent = await self.GetGameJudgeChooseComponent(playerId);
            gameJudgeChooseComponent.Record(gameJudgeChooseType, complainMsg);
        }

        public static async ETTask<GameJudgeChooseComponent> GetGameJudgeChooseComponent(this GameJudgeChooseManagerComponent self, long playerId)
        {
            GameJudgeChooseComponent gameJudgeChooseComponent = self.GetChild<GameJudgeChooseComponent>(playerId);
            if (gameJudgeChooseComponent == null)
            {
                gameJudgeChooseComponent = await self.InitByDBOne(playerId);
            }
            return gameJudgeChooseComponent;
        }

        public static async ETTask<GameJudgeChooseComponent> InitByDBOne(this GameJudgeChooseManagerComponent self, long playerId)
        {
            return await ET.Server.DBHelper.LoadDBWithParent2Child<GameJudgeChooseComponent>(self, playerId, true);
        }

    }
}