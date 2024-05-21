using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    public enum PlayerModelType: byte
    {
        BaseInfo,
        BackPack,
        BattleCard,
        OtherInfo,
        FunctionMenu,
        Mails,
        ArcadeCoinAdd,
        ArcadeCoinReduce,
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

        PlayerBaseInfo_AddPhysical = 100,
        PlayerBaseInfo_ReducePhysical = 101,
        PlayerBaseInfo_EndlessChallengeScore = 102,
        PlayerBaseInfo_AREndlessChallengeBattleCount = 103,
        PlayerBaseInfo_ARPVEBattleCount = 104,
        PlayerBaseInfo_ARPVPBattleCount = 105,
        PlayerBaseInfo_ChallengeClearLevel = 106,
        PlayerBaseInfo_AddArcadeCoinNum = 107,
        PlayerBaseInfo_ReduceArcadeCoinNum = 108,

        PlayerBackPack_AddItem = 200,
        PlayerBackPack_DeleteItem = 201,

        PlayerBattleCard_AutoSetByBackPack = 300,

        PlayerFunctionMenu_BattleEnd = 400,
        PlayerFunctionMenu_ResetStatusWhenDebug = 401,
        PlayerFunctionMenu_DealWaitChg = 402,
    }

    [ChildOf(typeof(PlayerCacheManagerComponent))]
    public class PlayerDataComponent : Entity, IAwake, IDestroy
    {
        public long playerId;
    }
}