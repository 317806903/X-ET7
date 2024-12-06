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
        BattleSkill,
        OtherInfo,
        SeasonInfo,
        FunctionMenu,
        Mails,

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
        PlayerBattleSkill_Client = 4,
        PlayerFunctionMenu_Client = 5,
        PlayerOtherInfo_Client = 6,
        PlayerSeasonInfo_Client = 7,
        PlayerMail_Client = 8,

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
        PlayerBackPack_SetItemNum = 202,
        PlayerBackPack_NewItemList = 203,

        PlayerBattleCard_AutoSetByBackPack = 300,
        PlayerBattleSkill_AutoSetByBackPack = 400,

        PlayerFunctionMenu_BattleEnd = 500,
        PlayerFunctionMenu_ResetStatusWhenDebug = 501,
        PlayerFunctionMenu_DealWaitChg = 502,

        PlayerSeasonInfo_ChallengeClearLevel = 601,
        PlayerSeasonInfo_EndlessChallengeScore = 602,
        PlayerSeasonInfo_PowerUP = 603,

        PlayerMails_GetItemGifts = 701,

        PlayerOtherInfo_SetUIRedDotType = 801,
        PlayerOtherInfo_RewardQuestionnaire = 802,
        PlayerOtherInfo_BattleNotice = 803,
        PlayerOtherInfo_DealUIRedDotType = 804,

    }

    [ChildOf(typeof(PlayerCacheManagerComponent))]
    public class PlayerDataComponent : Entity, IAwake, IDestroy
    {
        public long playerId;
    }
}