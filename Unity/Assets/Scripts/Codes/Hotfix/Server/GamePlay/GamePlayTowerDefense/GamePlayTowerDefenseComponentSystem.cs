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
                PlayerBaseInfoComponent playerBaseInfoComponent =
                    await ET.Server.PlayerCacheHelper.GetPlayerModel(self.DomainScene(), playerId, PlayerModelType.BaseInfo, true) as
                        PlayerBaseInfoComponent;
                if (playerBaseInfoComponent.EndlessChallengeScore < monsterWaveCallComponent.curIndex)
                {
                    playerBaseInfoComponent.EndlessChallengeScore = monsterWaveCallComponent.curIndex;
                    await ET.Server.PlayerCacheHelper.SavePlayerModel(self.DomainScene(), playerId, PlayerModelType.BaseInfo,
                        new() { "EndlessChallengeScore" });
                    await ET.Server.PlayerCacheHelper.SavePlayerRank(self.DomainScene(), playerId, RankType.EndlessChallenge,
                        playerBaseInfoComponent.EndlessChallengeScore);
                }
            }

            await ETTask.CompletedTask;
        }

        public static async ETTask GameEndWhenServer_IsPVEMode(this GamePlayTowerDefenseComponent self)
        {
            List<long> playerList = self.GetPlayerList();
            for (int i = 0; i < playerList.Count; i++)
            {
                long playerId = playerList[i];
                bool bHomeWin = self.ChkHomeWin(playerId);
                if (bHomeWin == false)
                {
                    continue;
                }

                PlayerBaseInfoComponent playerBaseInfoComponent =
                    await ET.Server.PlayerCacheHelper.GetPlayerModel(self.DomainScene(), playerId, PlayerModelType.BaseInfo, true) as
                        PlayerBaseInfoComponent;
                string cfgId = self.GetGamePlay().GetGamePlayBattleConfig().Id;
                string[] sArray = cfgId.Split('_');
                string challengeLevel = sArray[2];
                string result = System.Text.RegularExpressions.Regex.Replace(challengeLevel, @"[^0-9]+", "");
                if(result == ""){
                    await ETTask.CompletedTask;
                }
                TowerDefense_ChallengeLevelCfg challengeLevelCfg =
                    TowerDefense_ChallengeLevelCfgCategory.Instance.Get(challengeLevel);
                int level = int.Parse(result);
                if (playerBaseInfoComponent.ChallengeClearLevel + 1 == level)
                {
                    //发放首通奖励 challengeLevelCfg.FirstClearDropItem
                    //扣玩家体力 challengeLevelCfg.EnergyCost
                    playerBaseInfoComponent.ChallengeClearLevel = level;
                    await ET.Server.PlayerCacheHelper.SavePlayerModel(self.DomainScene(), playerId, PlayerModelType.BaseInfo,
                        new() { "ChallengeClearLevel" });
                }
                else if (playerBaseInfoComponent.ChallengeClearLevel >= level)
                {
                    //重复通关奖励 challengeLevelCfg.RepeatClearDropItem
                    //扣玩家体力 challengeLevelCfg.EnergyCost
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