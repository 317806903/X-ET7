using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    public enum PlayerModelType: byte
    {
        None,
        BaseInfo,
        BackPack,
        BattleCard,
        OtherInfo,
        SeasonInfo,
        FunctionMenu,
        Mails,
        Skills,

        TokenArcadeCoinAdd,
        TokenArcadeCoinReduce,
        TokenDiamondAdd,
        TokenDiamondReduce,
        RankPVE,
        RankEndlessChallenge,
    }

    public enum PlayerModelChgType
    {
        None = 0,
        PlayerBaseInfo_Client = 1,
        PlayerBackPack_Client = 2,
        PlayerBattleCard_Client = 3,
        PlayerFunctionMenu_Client = 4,
        PlayerOtherInfo_Client = 5,
        PlayerSeasonInfo_Client = 6,
        PlayerMail_Client = 7,

        PlayerBaseInfo_AddPhysical = 100,
        PlayerBaseInfo_ReducePhysical = 101,
        PlayerBaseInfo_EndlessChallengeScore = 102,
        PlayerBaseInfo_AREndlessChallengeBattleCount = 103,
        PlayerBaseInfo_ARPVEBattleCount = 104,
        PlayerBaseInfo_ARPVPBattleCount = 105,
        PlayerBaseInfo_ChallengeClearLevel = 106,
        PlayerBaseInfo_AddArcadeCoinNum = 107,
        PlayerBaseInfo_ReduceArcadeCoinNum = 108,
        PlayerBaseInfo_ChgSeasonIndex = 109,

        PlayerBackPack_AddItem = 200,
        PlayerBackPack_DeleteItem = 201,
        PlayerBackPack_NewItemList = 202,

        PlayerBattleCard_AutoSetByBackPack = 300,

        PlayerFunctionMenu_BattleEnd = 400,
        PlayerFunctionMenu_ResetStatusWhenDebug = 401,
        PlayerFunctionMenu_DealWaitChg = 402,

        PlayerSeasonInfo_ChallengeClearLevel = 501,
        PlayerSeasonInfo_EndlessChallengeScore = 502,
        PlayerSeasonInfo_PowerUP = 503,

        PlayerMails_GetItemGifts = 601,

        PlayerOtherInfo_SetUIRedDotType = 701,
        PlayerOtherInfo_RewardQuestionnaire = 702,
        PlayerOtherInfo_BattleNotice = 703,
        PlayerOtherInfo_DealUIRedDotType = 704,

        PlayerSkill_LearnOrUpdate = 801,
        PlayerSkill_Replace = 802,
        PlayerSkill_NewSkillList = 803,
    }

    [ChildOf(typeof(PlayerCacheManagerComponent))]
    public class PlayerDataComponent : Entity, IAwake, IDestroy
    {
        public long playerId;
    }
}