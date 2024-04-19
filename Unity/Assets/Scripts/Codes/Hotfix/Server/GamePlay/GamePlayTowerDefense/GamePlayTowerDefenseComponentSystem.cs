using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Server
{
    [FriendOf(typeof (GamePlayTowerDefenseComponent))]
    [FriendOf(typeof (Unit))]
    public static class GamePlayTowerDefenseComponentSystem
    {
        public static async ETTask GameBeginWhenServer(this GamePlayTowerDefenseComponent self)
        {
            if (self.IsEndlessChallengeMode())
            {
                await self.GameBeginWhenServer_IsEndlessChallengeMode();
            }
            else if (self.IsPVEMode())
            {
                await self.GameBeginWhenServer_IsPVEMode();
            }
            else if (self.IsPVPMode())
            {
                await self.GameBeginWhenServer_IsPVPMode();
            }

            await ETTask.CompletedTask;
        }

        public static async ETTask GameEndWhenServer(this GamePlayTowerDefenseComponent self)
        {
            if (self.IsEndlessChallengeMode())
            {
                await self.GameEndWhenServer_IsEndlessChallengeMode();
            }
            else if (self.IsPVEMode())
            {
                await self.GameEndWhenServer_IsPVEMode();
            }
            else if (self.IsPVPMode())
            {
                await self.GameEndWhenServer_IsPVPMode();
            }

            await self.DealPlayerFunctionMenu();

            await ETTask.CompletedTask;
        }

        public static async ETTask GameBeginWhenServer_IsEndlessChallengeMode(this GamePlayTowerDefenseComponent self)
        {
            await ET.Server.PlayerCacheHelper.ReducePhysicalStrenth(self.DomainScene(), self.ownerPlayerId,
                GlobalSettingCfgCategory.Instance.AREndlessChallengeTakePhsicalStrength);

            await ETTask.CompletedTask;
        }

        public static async ETTask GameBeginWhenServer_IsPVEMode(this GamePlayTowerDefenseComponent self)
        {
            string cfgId = self.GetGamePlay().GetGamePlayBattleConfig().Id;
            bool isChallengeLevelCfg = TowerDefense_ChallengeLevelCfgCategory.Instance.Contain(cfgId);
            if (isChallengeLevelCfg == false)
            {
                Log.Error($"TowerDefense_ChallengeLevelCfgCategory.Instance.Contain({cfgId}) == false");
                return;
            }

            //扣玩家体力
            await ET.Server.PlayerCacheHelper.ReducePhysicalStrenth(self.DomainScene(), self.ownerPlayerId,
                GlobalSettingCfgCategory.Instance.ARPVECfgTakePhsicalStrength);

            await ETTask.CompletedTask;
        }

        public static async ETTask GameBeginWhenServer_IsPVPMode(this GamePlayTowerDefenseComponent self)
        {
            //扣玩家体力
            await ET.Server.PlayerCacheHelper.ReducePhysicalStrenth(self.DomainScene(), self.ownerPlayerId,
                GlobalSettingCfgCategory.Instance.ARPVPCfgTakePhsicalStrength);

            await ETTask.CompletedTask;
        }

        public static async ETTask GameEndWhenServer_IsEndlessChallengeMode(this GamePlayTowerDefenseComponent self)
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

        public static async ETTask GameEndWhenServer_IsPVEMode(this GamePlayTowerDefenseComponent self)
        {
            string cfgId = self.GetGamePlay().GetGamePlayBattleConfig().Id;
            bool isChallengeLevelCfg = TowerDefense_ChallengeLevelCfgCategory.Instance.Contain(cfgId);
            if (isChallengeLevelCfg == false)
            {
                Log.Error($"TowerDefense_ChallengeLevelCfgCategory.Instance.Contain({cfgId}) == false");
                return;
            }
            TowerDefense_ChallengeLevelCfg challengeLevelCfg = TowerDefense_ChallengeLevelCfgCategory.Instance.Get(cfgId);
            int level = challengeLevelCfg.Index;

            List<long> playerList = self.GetPlayerList();
            for (int i = 0; i < playerList.Count; i++)
            {
                long playerId = playerList[i];
                PlayerBaseInfoComponent playerBaseInfoComponent = await ET.Server.PlayerCacheHelper.GetPlayerBaseInfoByPlayerId(self.DomainScene(), playerId, true);
                bool bHomeWin = self.ChkHomeWin(playerId);

                playerBaseInfoComponent.ARPVEBattleCount++;
                await ET.Server.PlayerCacheHelper.SavePlayerModel(self.DomainScene(), playerId, PlayerModelType.BaseInfo, new() { "ARPVEBattleCount"}, PlayerModelChgType.PlayerBaseInfo_ARPVEBattleCount);

                if (bHomeWin == false)
                {
                    continue;
                }

                if (playerBaseInfoComponent.ChallengeClearLevel + 1 == level)
                {
                    playerBaseInfoComponent.ChallengeClearLevel = level;
                    await ET.Server.PlayerCacheHelper.SavePlayerModel(self.DomainScene(), playerId, PlayerModelType.BaseInfo, new() { "ChallengeClearLevel"}, PlayerModelChgType.PlayerBaseInfo_ChallengeClearLevel);
                    //发放首通奖励
                    Dictionary<string, int> dropItems = ET.DropItemRuleHelper.Drop(challengeLevelCfg.FirstClearDropItem);
                    await ET.Server.PlayerCacheHelper.AddItems(self.DomainScene(), playerId, dropItems);
                    self.GetGamePlay().GetComponent<GamePlayDropItemComponent>().RecordPlayerDropItemsInfo(playerId, dropItems);
                }
                else if (playerBaseInfoComponent.ChallengeClearLevel >= level)
                {
                    //重复通关奖励
                    Dictionary<string, int> dropItems = ET.DropItemRuleHelper.Drop(challengeLevelCfg.RepeatClearDropItem);
                    await ET.Server.PlayerCacheHelper.AddItems(self.DomainScene(), playerId, dropItems);
                    self.GetGamePlay().GetComponent<GamePlayDropItemComponent>().RecordPlayerDropItemsInfo(playerId, dropItems);
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
    }
}