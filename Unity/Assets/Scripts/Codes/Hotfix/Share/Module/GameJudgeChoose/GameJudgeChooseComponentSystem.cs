using ET.AbilityConfig;

namespace ET
{
    [FriendOf(typeof(GameJudgeChooseComponent))]
    public static class GameJudgeChooseComponentSystem
    {
        [ObjectSystem]
        public class GameJudgeChooseComponentAwakeSystem : AwakeSystem<GameJudgeChooseComponent>
        {
            protected override void Awake(GameJudgeChooseComponent self)
            {
                self.Init();
            }
        }

        public static void Init(this GameJudgeChooseComponent self)
        {
            self.recordTime = TimeHelper.ServerNow();
            self.gameJudgeChooseType = GameJudgeChooseType.None;
            self.complainMsg = "";
        }

        public static void Record(this GameJudgeChooseComponent self, GameJudgeChooseType gameJudgeChooseType, string complainMsg)
        {
            self.recordTime = TimeHelper.ServerNow();
            self.gameJudgeChooseType = gameJudgeChooseType;
            self.complainMsg = complainMsg;
        }

        public static bool ChkNeedShow(this GameJudgeChooseComponent self)
        {
            if (self.gameJudgeChooseType == GameJudgeChooseType.None)
            {
                return true;
            }
            else if (self.gameJudgeChooseType == GameJudgeChooseType.ClickLoveIt)
            {
                return false;
            }
            else if (self.gameJudgeChooseType == GameJudgeChooseType.ClickComplain)
            {
                if (GlobalSettingCfgCategory.Instance.GameReJudgeTime >= TimeHelper.ServerNow())
                {
                    return true;
                }
                return false;
            }
            else if (self.gameJudgeChooseType == GameJudgeChooseType.ClickClose)
            {
                if (GlobalSettingCfgCategory.Instance.GameReJudgeTime >= TimeHelper.ServerNow())
                {
                    return true;
                }
                if (self.recordTime < TimeHelper.ServerNow() - 3 * 24 * 60 * 60 * 1000)
                {
                    return true;
                }
                return false;
            }
            return true;
        }
    }
}