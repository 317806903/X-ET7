using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    public enum GameJudgeChooseType
    {
        None,
        ClickLoveIt,
        ClickComplain,
        ClickClose,
    }

    [ChildOf(typeof(GameJudgeChooseManagerComponent))]
    public class GameJudgeChooseComponent : Entity, IAwake, IDestroy
    {
        public long recordTime;
        public GameJudgeChooseType gameJudgeChooseType;
        public string complainMsg;
    }
}