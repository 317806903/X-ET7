﻿using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Server
{
    [FriendOf(typeof (GamePlayTowerDefenseComponent))]
    [FriendOf(typeof (Unit))]
    public static class GamePlayTowerDefenseComponentSystem
    {
        public static async ETTask GameBeginWhenServer(this GamePlayTowerDefenseComponent self)
        {
            RoomTypeInfo roomTypeInfo = self.GetGamePlay().roomTypeInfo;
            int seasonCfgId = roomTypeInfo.seasonCfgId;
            if (self.IsEndlessChallengeMode())
            {
                if (seasonCfgId > 0)
                {
                    await self.GameBeginWhenServer_IsEndlessChallengeMode_Season();
                }
                else
                {
                    await self.GameBeginWhenServer_IsEndlessChallengeMode_Normal();
                }
            }
            else if (self.IsPVEMode())
            {
                if (seasonCfgId > 0)
                {
                    await self.GameBeginWhenServer_IsPVEMode_Season();
                }
                else
                {
                    await self.GameBeginWhenServer_IsPVEMode_Normal();
                }
            }
            else if (self.IsPVPMode())
            {
                await self.GameBeginWhenServer_IsPVPMode();
            }

            await ETTask.CompletedTask;
        }

        public static async ETTask GameEndWhenServer(this GamePlayTowerDefenseComponent self)
        {
            RoomTypeInfo roomTypeInfo = self.GetGamePlay().roomTypeInfo;
            int seasonCfgId = roomTypeInfo.seasonCfgId;
            if (self.IsEndlessChallengeMode())
            {
                if (seasonCfgId > 0)
                {
                    await self.GameEndWhenServer_IsEndlessChallengeMode_Season();
                }
                else
                {
                    await self.GameEndWhenServer_IsEndlessChallengeMode_Normal();
                }
            }
            else if (self.IsPVEMode())
            {
                if (seasonCfgId > 0)
                {
                    await self.GameEndWhenServer_IsPVEMode_Season();
                }
                else
                {
                    await self.GameEndWhenServer_IsPVEMode_Normal();
                }
            }
            else if (self.IsPVPMode())
            {
                await self.GameEndWhenServer_IsPVPMode();
            }

            await self.DealPlayerFunctionMenu();

            await ETTask.CompletedTask;
        }

        public static async ETTask GameRecoverWhenServer(this GamePlayTowerDefenseComponent self, long playerId)
        {
            int costValue = self.GetComponent<GameRecoverOnceComponent>().recoverCostArcadeCoinNum;
            if (costValue <= 0)
            {
                return;
            }
            await ET.Server.PlayerCacheHelper.ReduceTokenArcadeCoin(self.DomainScene(), playerId, costValue);

            await ETTask.CompletedTask;
        }

        public static async ETTask GameBeginWhenServer_IsEndlessChallengeMode_Normal(this GamePlayTowerDefenseComponent self)
        {
            if (ET.SceneHelper.ChkIsGameModeArcade())
            {
                int costValue = ET.GamePlayHelper.GetArcadeCoinCost(self, false);
                List<long> playerList = self.GetPlayerList();
                for (int i = 0; i < playerList.Count; i++)
                {
                    long playerId = playerList[i];
                    await ET.Server.PlayerCacheHelper.ReduceTokenArcadeCoin(self.DomainScene(), playerId, costValue);
                }
            }
            else
            {
                await ET.Server.PlayerCacheHelper.ReducePhysicalStrenth(self.DomainScene(), self.ownerPlayerId,
                    ET.GamePlayHelper.GetPhysicalCostEndlessChallenge());
            }

            await ETTask.CompletedTask;
        }

        public static async ETTask GameBeginWhenServer_IsEndlessChallengeMode_Season(this GamePlayTowerDefenseComponent self)
        {
            if (ET.SceneHelper.ChkIsGameModeArcade())
            {
                int costValue = ET.GamePlayHelper.GetArcadeCoinCost(self, false);
                List<long> playerList = self.GetPlayerList();
                for (int i = 0; i < playerList.Count; i++)
                {
                    long playerId = playerList[i];
                    await ET.Server.PlayerCacheHelper.ReduceTokenArcadeCoin(self.DomainScene(), playerId, costValue);
                }
            }
            else
            {
                await ET.Server.PlayerCacheHelper.ReducePhysicalStrenth(self.DomainScene(), self.ownerPlayerId,
                    ET.GamePlayHelper.GetPhysicalCostEndlessChallenge());
            }

            await ETTask.CompletedTask;
        }

        public static async ETTask GameBeginWhenServer_IsPVEMode_Normal(this GamePlayTowerDefenseComponent self)
        {
            if (ET.SceneHelper.ChkIsGameModeArcade())
            {
                int costValue = ET.GamePlayHelper.GetArcadeCoinCost(self, false);
                List<long> playerList = self.GetPlayerList();
                for (int i = 0; i < playerList.Count; i++)
                {
                    long playerId = playerList[i];
                    await ET.Server.PlayerCacheHelper.ReduceTokenArcadeCoin(self.DomainScene(), playerId, costValue);
                }
            }
            else
            {
                //扣玩家体力
                await ET.Server.PlayerCacheHelper.ReducePhysicalStrenth(self.DomainScene(), self.ownerPlayerId, ET.GamePlayHelper.GetPhysicalCostPVE());
            }

            await ETTask.CompletedTask;
        }

        public static async ETTask GameBeginWhenServer_IsPVEMode_Season(this GamePlayTowerDefenseComponent self)
        {
            if (ET.SceneHelper.ChkIsGameModeArcade())
            {
                int costValue = ET.GamePlayHelper.GetArcadeCoinCost(self, false);
                List<long> playerList = self.GetPlayerList();
                for (int i = 0; i < playerList.Count; i++)
                {
                    long playerId = playerList[i];
                    await ET.Server.PlayerCacheHelper.ReduceTokenArcadeCoin(self.DomainScene(), playerId, costValue);
                }
            }
            else
            {
                //扣玩家体力
                await ET.Server.PlayerCacheHelper.ReducePhysicalStrenth(self.DomainScene(), self.ownerPlayerId, ET.GamePlayHelper.GetPhysicalCostPVE());
            }

            await ETTask.CompletedTask;
        }

        public static async ETTask GameBeginWhenServer_IsPVPMode(this GamePlayTowerDefenseComponent self)
        {
            if (ET.SceneHelper.ChkIsGameModeArcade())
            {
                int costValue = ET.GamePlayHelper.GetArcadeCoinCost(self, false);
                List<long> playerList = self.GetPlayerList();
                for (int i = 0; i < playerList.Count; i++)
                {
                    long playerId = playerList[i];
                    await ET.Server.PlayerCacheHelper.ReduceTokenArcadeCoin(self.DomainScene(), playerId, costValue);
                }
            }
            else
            {
                //扣玩家体力
                await ET.Server.PlayerCacheHelper.ReducePhysicalStrenth(self.DomainScene(), self.ownerPlayerId,
                    ET.GamePlayHelper.GetPhysicalCostPVP());
            }

            await ETTask.CompletedTask;
        }

        public static async ETTask GameEndWhenServer_IsEndlessChallengeMode_Normal(this GamePlayTowerDefenseComponent self)
        {

            MonsterWaveCallComponent monsterWaveCallComponent = self.GetComponent<MonsterWaveCallComponent>();
            List<long> playerList = self.GetPlayerList();
            for (int i = 0; i < playerList.Count; i++)
            {
                long playerId = playerList[i];

                int killNum = self.GetGamePlay().GetComponent<GamePlayStatisticalDataManagerComponent>().GetPlayerKillNum(playerId);

                PlayerBaseInfoComponent playerBaseInfoComponent = await ET.Server.PlayerCacheHelper.GetPlayerBaseInfoByPlayerId(self.DomainScene(), playerId, true);
                if (playerBaseInfoComponent.EndlessChallengeScore < monsterWaveCallComponent.curIndex || (playerBaseInfoComponent.EndlessChallengeScore == monsterWaveCallComponent.curIndex && playerBaseInfoComponent.EndlessChallengeKillNum < killNum))
                {
                    playerBaseInfoComponent.EndlessChallengeScore = monsterWaveCallComponent.curIndex;
                    playerBaseInfoComponent.EndlessChallengeKillNum = killNum;
                    await ET.Server.PlayerCacheHelper.SavePlayerModel(self.DomainScene(), playerId, PlayerModelType.BaseInfo,
                        new() { "EndlessChallengeScore", "EndlessChallengeKillNum"}, PlayerModelChgType.PlayerBaseInfo_EndlessChallengeScore);
                    await ET.Server.PlayerCacheHelper.SavePlayerRank(self.DomainScene(), playerId, RankType.EndlessChallenge,
                        playerBaseInfoComponent.EndlessChallengeScore, playerBaseInfoComponent.EndlessChallengeKillNum);
                }

                playerBaseInfoComponent.AREndlessChallengeBattleCount++;
                await ET.Server.PlayerCacheHelper.SavePlayerModel(self.DomainScene(), playerId, PlayerModelType.BaseInfo,
                    new() { "AREndlessChallengeBattleCount"}, PlayerModelChgType.PlayerBaseInfo_AREndlessChallengeBattleCount);
            }

            await ETTask.CompletedTask;
        }

        public static async ETTask GameEndWhenServer_IsEndlessChallengeMode_Season(this GamePlayTowerDefenseComponent self)
        {
            int seasonIndex = await SeasonHelper.GetSeasonIndex(self.DomainScene());

            MonsterWaveCallComponent monsterWaveCallComponent = self.GetComponent<MonsterWaveCallComponent>();
            List<long> playerList = self.GetPlayerList();
            for (int i = 0; i < playerList.Count; i++)
            {
                long playerId = playerList[i];

                int killNum = self.GetGamePlay().GetComponent<GamePlayStatisticalDataManagerComponent>().GetPlayerKillNum(playerId);

                RoomTypeInfo roomTypeInfo = self.GetGamePlay().roomTypeInfo;
                if (seasonIndex != roomTypeInfo.seasonIndex)
                {
                    Log.Error($"GameEndWhenServer_IsEndlessChallengeMode_Season seasonIndex[{seasonIndex}] != roomTypeInfo.seasonIndex[{roomTypeInfo.seasonIndex}]");
                }
                else
                {
                    PlayerSeasonInfoComponent playerSeasonInfoComponent = await ET.Server.PlayerCacheHelper.GetPlayerSeasonInfoByPlayerId(self.DomainScene(), playerId, true);
                    if (playerSeasonInfoComponent.EndlessChallengeScore < monsterWaveCallComponent.curIndex ||
                        (playerSeasonInfoComponent.EndlessChallengeScore == monsterWaveCallComponent.curIndex && playerSeasonInfoComponent.EndlessChallengeKillNum < killNum))
                    {
                        playerSeasonInfoComponent.EndlessChallengeScore = monsterWaveCallComponent.curIndex;
                        playerSeasonInfoComponent.EndlessChallengeKillNum = killNum;
                        await ET.Server.PlayerCacheHelper.SavePlayerModel(self.DomainScene(), playerId, PlayerModelType.SeasonInfo,
                            new() { "EndlessChallengeScore", "EndlessChallengeKillNum"}, PlayerModelChgType.PlayerSeasonInfo_EndlessChallengeScore);
                        await ET.Server.PlayerCacheHelper.SavePlayerRank(self.DomainScene(), playerId, RankType.EndlessChallenge,
                            playerSeasonInfoComponent.EndlessChallengeScore, playerSeasonInfoComponent.EndlessChallengeKillNum);
                    }
                }

            }

            await ETTask.CompletedTask;
        }

        public static async ETTask GameEndWhenServer_IsPVPMode(this GamePlayTowerDefenseComponent self)
        {
            List<long> playerList = self.GetPlayerList();
            for (int i = 0; i < playerList.Count; i++)
            {
                long playerId = playerList[i];

                PlayerBaseInfoComponent playerBaseInfoComponent = await ET.Server.PlayerCacheHelper.GetPlayerBaseInfoByPlayerId(self.DomainScene(), playerId, true);
                playerBaseInfoComponent.ARPVPBattleCount++;
                await ET.Server.PlayerCacheHelper.SavePlayerModel(self.DomainScene(), playerId, PlayerModelType.BaseInfo,
                    new() { "ARPVPBattleCount"}, PlayerModelChgType.PlayerBaseInfo_ARPVPBattleCount);
            }

            await ETTask.CompletedTask;
        }

        public static async ETTask GameEndWhenServer_IsPVEMode_Normal(this GamePlayTowerDefenseComponent self)
        {
            RoomTypeInfo roomTypeInfo = self.GetGamePlay().roomTypeInfo;

            int level = roomTypeInfo.pveIndex;

            ChallengeLevelCfg challengeLevelCfg = TowerDefense_ChallengeLevelCfgCategory.Instance.GetChallenge(roomTypeInfo);
            //发放首通奖励
            Dictionary<string, int> firstClearDropItems = ET.DropItemRuleHelper.Drop(challengeLevelCfg.FirstClearDropItem);
            //重复通关奖励
            Dictionary<string, int> repeatClearDropItems = ET.DropItemRuleHelper.Drop(challengeLevelCfg.RepeatClearDropItem);

            List<long> playerList = self.GetPlayerList();
            for (int i = 0; i < playerList.Count; i++)
            {
                long playerId = playerList[i];
                PlayerBaseInfoComponent playerBaseInfoComponent = await ET.Server.PlayerCacheHelper.GetPlayerBaseInfoByPlayerId(self.DomainScene(), playerId, true);
                int curLevel = playerBaseInfoComponent.ChallengeClearLevel;
                bool bHomeWin = self.ChkHomeWin(playerId);

                playerBaseInfoComponent.ARPVEBattleCount++;
                await ET.Server.PlayerCacheHelper.SavePlayerModel(self.DomainScene(), playerId, PlayerModelType.BaseInfo, new() { "ARPVEBattleCount"}, PlayerModelChgType.PlayerBaseInfo_ARPVEBattleCount);

                if (bHomeWin == false)
                {
                    continue;
                }

                if (curLevel + 1 == level)
                {
                    playerBaseInfoComponent.ChallengeClearLevel = level;
                    await ET.Server.PlayerCacheHelper.SavePlayerModel(self.DomainScene(), playerId, PlayerModelType.BaseInfo, new() { "ChallengeClearLevel"}, PlayerModelChgType.PlayerBaseInfo_ChallengeClearLevel);

                    Dictionary<string, int> firstClearDropItemsNew = new();
                    foreach (var item in firstClearDropItems)
                    {
                        string itemCfgId = item.Key;
                        int count = item.Value;
                        if (ET.ItemHelper.ChkIsToken(itemCfgId) == false)
                        {
                            firstClearDropItemsNew[itemCfgId] = count;
                            continue;
                        }

                        ET.GamePlayHelper.ChgGamePlayNumericValueByPlayerId(self.DomainScene(), playerId, GameNumericType.TowerDefense_PlayerRewardWhenGameEndBase, count, true);
                        float newValue = ET.GamePlayHelper.GetGamePlayNumericValueByPlayerId(self.DomainScene(), playerId, GameNumericType.TowerDefense_PlayerRewardWhenGameEnd);
                        firstClearDropItemsNew[itemCfgId] = (int)newValue;
                    }

                    await ET.Server.PlayerCacheHelper.AddItems(self.DomainScene(), playerId, firstClearDropItemsNew);
                    self.GetGamePlay().GetComponent<GamePlayDropItemComponent>().RecordPlayerDropItemsInfo(playerId, firstClearDropItemsNew);
                }
                else if (curLevel >= level)
                {
                    Dictionary<string, int> repeatClearDropItemsNew = new();
                    foreach (var item in repeatClearDropItems)
                    {
                        string itemCfgId = item.Key;
                        int count = item.Value;
                        if (ET.ItemHelper.ChkIsToken(itemCfgId) == false)
                        {
                            repeatClearDropItemsNew[itemCfgId] = count;
                            continue;
                        }

                        ET.GamePlayHelper.ChgGamePlayNumericValueByPlayerId(self.DomainScene(), playerId, GameNumericType.TowerDefense_PlayerRewardWhenGameEndBase, count, true);
                        float newValue = ET.GamePlayHelper.GetGamePlayNumericValueByPlayerId(self.DomainScene(), playerId, GameNumericType.TowerDefense_PlayerRewardWhenGameEnd);
                        repeatClearDropItemsNew[itemCfgId] = (int)newValue;
                    }

                    await ET.Server.PlayerCacheHelper.AddItems(self.DomainScene(), playerId, repeatClearDropItemsNew);
                    self.GetGamePlay().GetComponent<GamePlayDropItemComponent>().RecordPlayerDropItemsInfo(playerId, repeatClearDropItemsNew);
                }
                else
                {
                    //陪玩
                }
            }
            await ETTask.CompletedTask;
        }

        public static async ETTask GameEndWhenServer_IsPVEMode_Season(this GamePlayTowerDefenseComponent self)
        {
            RoomTypeInfo roomTypeInfo = self.GetGamePlay().roomTypeInfo;

            int level = roomTypeInfo.pveIndex;

            ChallengeLevelCfg challengeLevelCfg = SeasonChallengeLevelCfgCategory.Instance.GetChallenge(roomTypeInfo);
            //发放首通奖励
            Dictionary<string, int> firstClearDropItems = ET.DropItemRuleHelper.Drop(challengeLevelCfg.FirstClearDropItem);
            //重复通关奖励
            Dictionary<string, int> repeatClearDropItems = ET.DropItemRuleHelper.Drop(challengeLevelCfg.RepeatClearDropItem);



            List<long> playerList = self.GetPlayerList();
            for (int i = 0; i < playerList.Count; i++)
            {
                long playerId = playerList[i];
                PlayerSeasonInfoComponent playerSeasonInfoComponent = await ET.Server.PlayerCacheHelper.GetPlayerSeasonInfoByPlayerId(self.DomainScene(), playerId, true);
                int curLevel = playerSeasonInfoComponent.pveIndex;
                bool bHomeWin = self.ChkHomeWin(playerId);

                if (bHomeWin == false)
                {
                    continue;
                }

                if (curLevel + 1 == level)
                {
                    int seasonIndex = await SeasonHelper.GetSeasonIndex(self.DomainScene());
                    if (seasonIndex == self.roomTypeInfo.seasonIndex)
                    {
                        playerSeasonInfoComponent.pveIndex = level;
                        await ET.Server.PlayerCacheHelper.SavePlayerModel(self.DomainScene(), playerId, PlayerModelType.SeasonInfo, new() { "pveIndex"}, PlayerModelChgType.PlayerSeasonInfo_ChallengeClearLevel);
                    }

                    Dictionary<string, int> firstClearDropItemsNew = new();
                    foreach (var item in firstClearDropItems)
                    {
                        string itemCfgId = item.Key;
                        int count = item.Value;
                        if (ET.ItemHelper.ChkIsToken(itemCfgId) == false)
                        {
                            firstClearDropItemsNew[itemCfgId] = count;
                            continue;
                        }

                        ET.GamePlayHelper.ChgGamePlayNumericValueByPlayerId(self.DomainScene(), playerId, GameNumericType.TowerDefense_PlayerRewardWhenGameEndBase, count, true);
                        float newValue = ET.GamePlayHelper.GetGamePlayNumericValueByPlayerId(self.DomainScene(), playerId, GameNumericType.TowerDefense_PlayerRewardWhenGameEnd);
                        firstClearDropItemsNew[itemCfgId] = (int)newValue;
                    }

                    await ET.Server.PlayerCacheHelper.AddItems(self.DomainScene(), playerId, firstClearDropItemsNew);
                    self.GetGamePlay().GetComponent<GamePlayDropItemComponent>().RecordPlayerDropItemsInfo(playerId, firstClearDropItemsNew);
                }
                else if (curLevel >= level)
                {

                    Dictionary<string, int> repeatClearDropItemsNew = new();
                    foreach (var item in repeatClearDropItems)
                    {
                        string itemCfgId = item.Key;
                        int count = item.Value;
                        if (ET.ItemHelper.ChkIsToken(itemCfgId) == false)
                        {
                            repeatClearDropItemsNew[itemCfgId] = count;
                            continue;
                        }

                        ET.GamePlayHelper.ChgGamePlayNumericValueByPlayerId(self.DomainScene(), playerId, GameNumericType.TowerDefense_PlayerRewardWhenGameEndBase, count, true);
                        float newValue = ET.GamePlayHelper.GetGamePlayNumericValueByPlayerId(self.DomainScene(), playerId, GameNumericType.TowerDefense_PlayerRewardWhenGameEnd);
                        repeatClearDropItemsNew[itemCfgId] = (int)newValue;
                    }

                    await ET.Server.PlayerCacheHelper.AddItems(self.DomainScene(), playerId, repeatClearDropItemsNew);
                    self.GetGamePlay().GetComponent<GamePlayDropItemComponent>().RecordPlayerDropItemsInfo(playerId, repeatClearDropItemsNew);
                }
                else
                {
                    //陪玩
                }
            }
            await ETTask.CompletedTask;
        }

        public static async ETTask DealPlayerFunctionMenu(this GamePlayTowerDefenseComponent self)
        {
            Dictionary<string, int> paramDic = DictionaryComponent<string, int>.Create();

            if (self.IsEndlessChallengeMode())
            {
                MonsterWaveCallComponent monsterWaveCallComponent = self.GetComponent<MonsterWaveCallComponent>();
                paramDic.Add("EndlessChallengeWaveIndex", monsterWaveCallComponent.GetWaveIndex());
            }

            List<long> playerList = self.GetPlayerList();
            for (int i = 0; i < playerList.Count; i++)
            {
                long playerId = playerList[i];
                await ET.Server.PlayerCacheHelper.DealPlayerFunctionMenu(self.DomainScene(), playerId, paramDic);
            }
            await ETTask.CompletedTask;
        }

        public static async ETTask DealPlayerCancelRecover(this GamePlayTowerDefenseComponent self, long playerId)
        {
            self.GetGamePlay().NoticeGameEndToRoom(false, playerId);

            self.GetGamePlay().PlayerQuitBattle(playerId, true);

            self.NoticeToClient(playerId);

            await ETTask.CompletedTask;
        }

        public static async ETTask<bool> ChkPlayerConfirmRecover(this GamePlayTowerDefenseComponent self, long playerId)
        {
            int costValue = self.GetComponent<GameRecoverOnceComponent>().recoverCostArcadeCoinNum;
            int curArcadeCoin = await ET.Server.PlayerCacheHelper.GetTokenArcadeCoinByPlayerId(self.DomainScene(), playerId, true);
            return curArcadeCoin >= costValue;
        }

        public static async ETTask DealPlayerConfirmRecover(this GamePlayTowerDefenseComponent self, long playerId)
        {
            await self.GameRecoverWhenServer(playerId);

            await ETTask.CompletedTask;
        }

    }
}