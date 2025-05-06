using System.Collections.Generic;
using System.Linq;

namespace ET
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

    }
}