using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Server
{
    [FriendOf(typeof (GamePlayTowerDefenseComponent))]
    [FriendOf(typeof (Unit))]
    public static class GamePlayTowerDefenseComponentSystem
    {
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

                PlayerBaseInfoComponent playerBaseInfoComponent =
                    await ET.Server.PlayerCacheHelper.GetPlayerModel(self.DomainScene(), playerId, PlayerModelType.BaseInfo, true) as
                        PlayerBaseInfoComponent;
                if (playerBaseInfoComponent.EndlessChallengeScore < monsterWaveCallComponent.curIndex || (playerBaseInfoComponent.EndlessChallengeScore == monsterWaveCallComponent.curIndex && playerBaseInfoComponent.EndlessChallengeKillNum < killNum))
                {
                    playerBaseInfoComponent.EndlessChallengeScore = monsterWaveCallComponent.curIndex;
                    playerBaseInfoComponent.EndlessChallengeKillNum = killNum;
                    await ET.Server.PlayerCacheHelper.SavePlayerModel(self.DomainScene(), playerId, PlayerModelType.BaseInfo,
                        new() { "EndlessChallengeScore", "EndlessChallengeKillNum"});
                    await ET.Server.PlayerCacheHelper.SavePlayerRank(self.DomainScene(), playerId, RankType.EndlessChallenge,
                        playerBaseInfoComponent.EndlessChallengeScore, playerBaseInfoComponent.EndlessChallengeKillNum);
                }

                playerBaseInfoComponent.AREndlessChallengeBattleCount++;
                await ET.Server.PlayerCacheHelper.SavePlayerModel(self.DomainScene(), playerId, PlayerModelType.BaseInfo,
                    new() { "AREndlessChallengeBattleCount"});

                await ET.Server.PlayerCacheHelper.ReducePhysicalStrenth(self.DomainScene(), playerId,
                    GlobalSettingCfgCategory.Instance.AREndlessChallengeTakePhsicalStrength, PlayerModelChgType.PlayerBaseInfo_111);
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
                PlayerBaseInfoComponent playerBaseInfoComponent =
                        await ET.Server.PlayerCacheHelper.GetPlayerModel(self.DomainScene(), playerId, PlayerModelType.BaseInfo, true) as
                                PlayerBaseInfoComponent;
                bool bHomeWin = self.ChkHomeWin(playerId);

                playerBaseInfoComponent.ARPVEBattleCount++;
                await ET.Server.PlayerCacheHelper.SavePlayerModel(self.DomainScene(), playerId, PlayerModelType.BaseInfo,
                    new() { "ARPVEBattleCount"});

                if (bHomeWin == false)
                {
                    //非陪玩失败才扣体力
                    if(playerBaseInfoComponent.ChallengeClearLevel >= level - 1){
                        await ET.Server.PlayerCacheHelper.ReducePhysicalStrenth(self.DomainScene(), playerId,
                            GlobalSettingCfgCategory.Instance.ARPVECfgTakePhsicalStrength, PlayerModelChgType.PlayerBaseInfo_111);
                    }
                    continue;
                }

                if (playerBaseInfoComponent.ChallengeClearLevel + 1 == level)
                {
                    if(playerBaseInfoComponent.ChkPhysicalStrength(GlobalSettingCfgCategory.Instance.ARPVECfgTakePhsicalStrength))
                    {
                        playerBaseInfoComponent.ChallengeClearLevel = level;
                        await ET.Server.PlayerCacheHelper.SavePlayerModel(self.DomainScene(), playerId, PlayerModelType.BaseInfo,
                            new() { "ChallengeClearLevel"});
                        //发放首通奖励
                        Dictionary<string, int> dropItems = ET.DropItemRuleHelper.Drop(challengeLevelCfg.FirstClearDropItem);
                        await ET.Server.PlayerCacheHelper.AddItems(self.DomainScene(), playerId, dropItems);
                        self.GetGamePlay().GetComponent<GamePlayStatisticalDataManagerComponent>().AddPlayerDropItemsInfo(playerId, dropItems);
                        //扣玩家体力
                        await ET.Server.PlayerCacheHelper.ReducePhysicalStrenth(self.DomainScene(), playerId,
                            GlobalSettingCfgCategory.Instance.ARPVECfgTakePhsicalStrength, PlayerModelChgType.PlayerBaseInfo_111);
                    }
                }
                else if (playerBaseInfoComponent.ChallengeClearLevel >= level)
                {
                    if(playerBaseInfoComponent.ChkPhysicalStrength(GlobalSettingCfgCategory.Instance.ARPVECfgTakePhsicalStrength))
                    {
                        //重复通关奖励
                        Dictionary<string, int> dropItems = ET.DropItemRuleHelper.Drop(challengeLevelCfg.RepeatClearDropItem);
                        await ET.Server.PlayerCacheHelper.AddItems(self.DomainScene(), playerId, dropItems);
                        self.GetGamePlay().GetComponent<GamePlayStatisticalDataManagerComponent>().AddPlayerDropItemsInfo(playerId, dropItems);
                        //扣玩家体力
                        await ET.Server.PlayerCacheHelper.ReducePhysicalStrenth(self.DomainScene(), playerId,
                            GlobalSettingCfgCategory.Instance.ARPVECfgTakePhsicalStrength, PlayerModelChgType.PlayerBaseInfo_111);
                    }
                }
                else
                {
                    //陪玩
                }
            }
            await ETTask.CompletedTask;
        }
    }
}