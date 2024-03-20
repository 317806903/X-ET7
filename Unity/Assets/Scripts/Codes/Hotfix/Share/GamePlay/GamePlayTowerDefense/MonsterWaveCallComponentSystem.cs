using ET.Ability;
using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    [Invoke(TimerInvokeType.GamePlayChkMonsterWaveCallAllClear)]
    public class MonsterWaveCallTimer: ATimer<MonsterWaveCallComponent>
    {
        protected override void Run(MonsterWaveCallComponent self)
        {
            try
            {
                self.ChkMonsterCallAllClear();
                self.ChkMonsterCallTimeOut();
            }
            catch (Exception e)
            {
                Log.Error($"MonsterWaveCallTimer timer error: {self.Id}\n{e}");
            }
        }
    }

    [FriendOf(typeof (MonsterWaveCallComponent))]
    public static class MonsterWaveCallComponentSystem
    {
        [ObjectSystem]
        public class MonsterWaveCallComponentAwakeSystem: AwakeSystem<MonsterWaveCallComponent>
        {
            protected override void Awake(MonsterWaveCallComponent self)
            {
                self.sortWaveIndex = new();
                self.waveMonsterCallList = new();
            }
        }

        [ObjectSystem]
        public class MonsterWaveCallComponentDestroySystem: DestroySystem<MonsterWaveCallComponent>
        {
            protected override void Destroy(MonsterWaveCallComponent self)
            {
                self.waveMonsterCallList?.Clear();
                TimerComponent.Instance.Remove(ref self.Timer);
            }
        }

        public static void Init(this MonsterWaveCallComponent self, string monsterWaveRule, int monsterWaveIndex)
        {
            self.monsterWaveRule = monsterWaveRule;

            self.totalCount = 0;
            List<TowerDefense_MonsterWaveCallRuleCfg> list = TowerDefense_MonsterWaveCallRuleCfgCategory.Instance.DataList;

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].WaveRule == self.monsterWaveRule)
                {
                    self.totalCount++;
                    self.sortWaveIndex.Add(list[i].WaveIndex);
                }
            }

            self.sortWaveIndex.Sort();

            //self.curIndex = -1;
            self.curIndex = monsterWaveIndex - 1;
            if (self.curIndex >= self.totalCount - 1)
            {
                GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
                if (gamePlayTowerDefenseComponent.IsEndlessChallengeMonster() == false)
                {
                    self.curIndex = self.totalCount - 2;
                }
            }
        }

        public static float3 GetCallMonsterPosition(this MonsterWaveCallComponent self, long playerId)
        {
            return self.GetGamePlayTowerDefense().GetCallMonsterPosition(playerId);
        }

        public static int GetWaveIndex(this MonsterWaveCallComponent self)
        {
            return self.curIndex + 1;
        }

        public static int GetWaveRewardGold(this MonsterWaveCallComponent self)
        {
            bool bRet = self.GetRealWaveInfo(out int waveIndex, out float monsterWaveNumScalePercent,
                out float monsterWaveLevelScalePercent, out float waveRewardGoldScalePercent);
            if (bRet == false)
            {
                return 0;
            }

            TowerDefense_MonsterWaveCallRuleCfg monsterWaveCallCfg =
                TowerDefense_MonsterWaveCallRuleCfgCategory.Instance.Get(self.monsterWaveRule, waveIndex);

            int waveRewardGold = (int)math.floor(monsterWaveCallCfg.WaveRewardGold * (100 + waveRewardGoldScalePercent) * 0.01f);
            return waveRewardGold;
        }

        public static void DoNextMonsterWaveCall(this MonsterWaveCallComponent self)
        {
            self.Timer = TimerComponent.Instance.NewRepeatedTimer(500, TimerInvokeType.GamePlayChkMonsterWaveCallAllClear, self);
            self.curIndex++;
            self.DoMonsterWaveCall();
        }

        public static void RecoverWaveIndex(this MonsterWaveCallComponent self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
            List<long> playerList = gamePlayTowerDefenseComponent.GetPlayerList();
            foreach (long playerId in playerList)
            {
                if (self.waveMonsterCallList.ContainsKey(playerId) == false)
                {
                    continue;
                }
                if (self.waveMonsterCallList[playerId].ContainsKey(self.curIndex) == false)
                {
                    continue;
                }
                MonsterWaveCallOnceComponent monsterWaveCallOnceComponent = self.waveMonsterCallList[playerId][self.curIndex];
                monsterWaveCallOnceComponent.Dispose();
                self.waveMonsterCallList[playerId].Remove(self.curIndex);
            }

            TimerComponent.Instance.Remove(ref self.Timer);

            self.curIndex--;
        }

        public static bool ChkIsGameEnd(this MonsterWaveCallComponent self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();

            if (self.GetGamePlayTowerDefense().ChkIsGameEnd()
                || self.GetGamePlayTowerDefense().ChkIsGameRecover()
                || self.GetGamePlayTowerDefense().ChkIsGameRecovering())
            {
                return true;
            }
            return false;
        }

        public static void DoMonsterWaveCall(this MonsterWaveCallComponent self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
            List<long> playerList = gamePlayTowerDefenseComponent.GetPlayerList();
            foreach (long playerId in playerList)
            {
                MonsterWaveCallOnceComponent monsterWaveCallOnceComponent = self.AddChild<MonsterWaveCallOnceComponent>();
                bool bRet = self.GetRealWaveInfo(out int waveIndex, out float monsterWaveNumScalePercent,
                    out float monsterWaveLevelScalePercent, out float waveRewardGoldScalePercent);
                if (bRet == false)
                {
                    return;
                }

                monsterWaveCallOnceComponent.Init(playerId, self.monsterWaveRule, waveIndex, monsterWaveNumScalePercent, monsterWaveLevelScalePercent,
                    waveRewardGoldScalePercent);

                self.duration = monsterWaveCallOnceComponent.duration;
                if (self.waveMonsterCallList.ContainsKey(playerId) == false)
                {
                    self.waveMonsterCallList[playerId] = new();
                }

                if (self.waveMonsterCallList[playerId].ContainsKey(self.curIndex))
                {
                    self.waveMonsterCallList[playerId][self.curIndex] = monsterWaveCallOnceComponent;
                }
                else
                {
                    self.waveMonsterCallList[playerId].Add(self.curIndex, monsterWaveCallOnceComponent);
                }
            }
        }

        public static bool GetRealWaveInfo(this MonsterWaveCallComponent self, out int waveIndex,
        out float monsterWaveNumScalePercent, out float monsterWaveLevelScalePercent, out float waveRewardGoldScalePercent)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
            waveIndex = 0;
            monsterWaveNumScalePercent = 0;
            monsterWaveLevelScalePercent = 0;
            waveRewardGoldScalePercent = 0;
            if (self.curIndex >= self.sortWaveIndex.Count)
            {
                if (gamePlayTowerDefenseComponent.IsEndlessChallengeMonster() == false)
                {
                    return false;
                }

                GamePlayBattleLevelCfg gamePlayBattleLevelCfg = gamePlayTowerDefenseComponent.GetGamePlay().GetGamePlayBattleConfig();
                GamePlayTowerDefenseEndlessChallengeMonster gamePlayTowerDefenseEndlessChallengeMonster =
                    gamePlayBattleLevelCfg.GamePlayMode as GamePlayTowerDefenseEndlessChallengeMonster;
                int repeatNum = gamePlayTowerDefenseEndlessChallengeMonster.RepeatNum;
                float monsterWaveNumScalePercentCoefficient = gamePlayTowerDefenseEndlessChallengeMonster.MonsterWaveNumScalePercentCoefficient;
                float monsterWaveLevelScalePercentCoefficient = gamePlayTowerDefenseEndlessChallengeMonster.MonsterWaveLevelScalePercentCoefficient;
                float waveRewardGoldScalePercentCoefficient = gamePlayTowerDefenseEndlessChallengeMonster.WaveRewardGoldScalePercentCoefficient;
                if (self.sortWaveIndex.Count < repeatNum)
                {
                    repeatNum = self.sortWaveIndex.Count;
                }

                int tmp1 = (int)(1f * (self.curIndex - self.sortWaveIndex.Count) / repeatNum) + 1;
                int tmp2 = (self.curIndex - self.sortWaveIndex.Count) % repeatNum;

                waveIndex = self.sortWaveIndex[self.sortWaveIndex.Count - (repeatNum - tmp2)];
                monsterWaveNumScalePercent = tmp1 * monsterWaveNumScalePercentCoefficient;
                monsterWaveLevelScalePercent = tmp1 * monsterWaveLevelScalePercentCoefficient;
                waveRewardGoldScalePercent = tmp1 * waveRewardGoldScalePercentCoefficient;
            }
            else
            {
                waveIndex = self.sortWaveIndex[self.curIndex];
            }

            return true;
        }

        public static GamePlayTowerDefenseComponent GetGamePlayTowerDefense(this MonsterWaveCallComponent self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetParent<GamePlayTowerDefenseComponent>();
            return gamePlayTowerDefenseComponent;
        }

        public static Unit CallMonsterOnce(this MonsterWaveCallComponent self, long playerId, string monsterCfgId, int level, int rewardGold)
        {
            float3 pos = self.GetCallMonsterPosition(playerId);
            float3 randomPos = pos + new float3(RandomGenerator.RandFloat01(), 0, RandomGenerator.RandFloat01());
            float3 randomForward = new float3(RandomGenerator.RandFloat01(), 0, RandomGenerator.RandFloat01());

            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
            TeamFlagType teamFlagType = gamePlayTowerDefenseComponent.GetMonsterTeamFlagTypeByPlayer(playerId);

            Unit monsterUnit = ET.GamePlayTowerDefenseHelper.CreateMonster(self.DomainScene(), playerId, monsterCfgId, level, randomPos,
                randomForward, teamFlagType, rewardGold, self.curIndex + 1, self.curIndex + 1 - self.sortWaveIndex.Count);

            ET.GamePlayHelper.DoCreateActions(monsterUnit, gamePlayTowerDefenseComponent.model.MonsterWaveCallCreateActionIds);

            if (self.curIndex >= self.sortWaveIndex.Count)
            {
                if (gamePlayTowerDefenseComponent.IsEndlessChallengeMonster())
                {
                    GamePlayBattleLevelCfg gamePlayBattleLevelCfg = gamePlayTowerDefenseComponent.GetGamePlay().GetGamePlayBattleConfig();
                    GamePlayTowerDefenseEndlessChallengeMonster gamePlayTowerDefenseEndlessChallengeMonster =
                        gamePlayBattleLevelCfg.GamePlayMode as GamePlayTowerDefenseEndlessChallengeMonster;
                    ET.GamePlayHelper.DoCreateActions(monsterUnit, gamePlayTowerDefenseEndlessChallengeMonster.CreateActionIds);
                }
            }

            return monsterUnit;
        }

        public static void ChkMonsterCallAllClear(this MonsterWaveCallComponent self)
        {
            if (self.IsDisposed)
            {
                TimerComponent.Instance.Remove(ref self.Timer);
                return;
            }

            bool allPlayerWaveMonsterClear = true;
            foreach (var playerWaveMonsterCall in self.waveMonsterCallList)
            {
                bool playerWaveMonsterClear = true;
                foreach (var waveMonsterCall in playerWaveMonsterCall.Value)
                {
                    MonsterWaveCallOnceComponent monsterWaveCallOnceComponent = waveMonsterCall.Value;
                    self.duration = monsterWaveCallOnceComponent.duration;
                    if (monsterWaveCallOnceComponent.ChkMonsterCallAllClear() == false)
                    {
                        playerWaveMonsterClear = false;
                        break;
                    }
                    else
                    {
                    }
                }

                if (playerWaveMonsterClear == false)
                {
                    allPlayerWaveMonsterClear = false;
                    break;
                }
            }

            if (allPlayerWaveMonsterClear)
            {
                while (self.Children.Count > 0)
                {
                    foreach (var child in self.Children.Values)
                    {
                        child.Dispose();
                        break;
                    }
                }

                foreach (var list in self.waveMonsterCallList)
                {
                    foreach (var monsterWaveCallOnceComponent in list.Value.Values)
                    {
                        monsterWaveCallOnceComponent.Dispose();
                    }
                }
                self.waveMonsterCallList.Clear();

                if (self.GetGamePlayTowerDefense().IsEndlessChallengeMonster())
                {
                    self.GetGamePlayTowerDefense().TransToInTheBattleEnd().Coroutine();
                }
                else
                {
                    if (self.curIndex == self.totalCount - 1)
                    {
                        self.GetGamePlayTowerDefense().TransToGameResult(true, false).Coroutine();
                    }
                    else
                    {
                        self.GetGamePlayTowerDefense().TransToInTheBattleEnd().Coroutine();
                    }
                }

                TimerComponent.Instance.Remove(ref self.Timer);
            }
        }

        public static void ChkMonsterCallTimeOut(this MonsterWaveCallComponent self)
        {
            if (self.IsDisposed)
            {
                TimerComponent.Instance.Remove(ref self.Timer);
                return;
            }

            if (self.duration <= 0)
            {
                self.GetGamePlayTowerDefense().TransToGameResult(false, true).Coroutine();
                TimerComponent.Instance.Remove(ref self.Timer);
            }
        }
    }
}