using ET.Ability;
using System;
using System.Collections.Generic;
using System.Linq;
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
                if (self.isNoRest == false)
                {
                    self.ChkMonsterCallAllClearWithRest();
                    self.ChkMonsterCallTimeOutWithRest();
                }
                else
                {
                    self.ChkMonsterCallClearNoRest();
                    self.ChkMonsterCallTimeOutNoRest();
                }
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

        public static void Init(this MonsterWaveCallComponent self, TowerDefenseBattleMonsterWaveCall MonsterWaveCallRule)
        {
            self.monsterWaveRuleCfgId = MonsterWaveCallRule.MonsterWaveCallRuleCfgId;
            if (MonsterWaveCallRule is TowerDefenseBattleWithRest)
            {
                self.isNoRest = false;
            }
            else if (MonsterWaveCallRule is TowerDefenseBattleNoRest towerDefenseBattleNoRest)
            {
                self.isNoRest = true;
                self.isNextWaveWhenClearWhenNoRest = towerDefenseBattleNoRest.IsNextWaveWhenClear;
            }

            List<TowerDefense_MonsterWaveCallRuleCfg> list = TowerDefense_MonsterWaveCallRuleCfgCategory.Instance.DataList;

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].WaveRule == self.monsterWaveRuleCfgId)
                {
                    self.sortWaveIndex.Add(list[i].WaveIndex);
                }
            }

            self.sortWaveIndex.Sort();

            //self.curIndex = -1;
            self.curIndex = MonsterWaveCallRule.MonsterWaveCallStartWaveIndex - 1;
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

        public static void ResetWaveRewardGold(this MonsterWaveCallComponent self, ref float playerIncreaseCoinWhenWave)
        {
            bool bRet = self.GetRealWaveInfo(out int waveIndex, out int circleWaveIndex, out int circleNum, out int circleIndex, out int stageWaveIndex, out float monsterWaveNumScalePercent,
                out float monsterWaveLevelScalePercent, out float waveRewardGoldScalePercent);
            if (bRet == false)
            {
                return;
            }

            playerIncreaseCoinWhenWave = playerIncreaseCoinWhenWave * (100 + waveRewardGoldScalePercent) * 0.01f;
        }

        public static void DoNextMonsterWaveCall(this MonsterWaveCallComponent self)
        {
            self.Timer = TimerComponent.Instance.NewRepeatedTimer(500, TimerInvokeType.GamePlayChkMonsterWaveCallAllClear, self);
            self.curIndex++;
            self.DoMonsterWaveCall();
        }

        public static void TrigNextMonsterWaveCallGlobalBuffAddList(this MonsterWaveCallComponent self)
        {
            int nextIndex = self.curIndex + 1;
            if (nextIndex > self.totalCount - 1)
            {
                return;
            }
            int waveIndex = self.sortWaveIndex[nextIndex];
            TowerDefense_MonsterWaveCallRuleCfg monsterWaveCallCfg = TowerDefense_MonsterWaveCallRuleCfgCategory.Instance.Get(self.monsterWaveRuleCfgId, waveIndex);
            foreach (var actionCfgGlobalBuffAddImmediately in monsterWaveCallCfg.GlobalBuffAddList_Ref)
            {
                long playerId = (long)ET.PlayerId.PlayerNone;
                ET.Ability.ActionGameHandlerHelper.AddGameAction(self.DomainScene(), playerId, actionCfgGlobalBuffAddImmediately, null);

                if (self.IsDisposed)
                {
                    return;
                }
            }
        }

        public static Dictionary<string, int> GetNextMonsterWaveCallList(this MonsterWaveCallComponent self)
        {
            DictionaryComponent<string, int> monsterCfgId2Num = DictionaryComponent<string, int>.Create();
            int nextIndex = self.curIndex + 1;
            if (nextIndex > self.totalCount - 1)
            {
                return null;
            }
            int waveIndex = self.sortWaveIndex[nextIndex];
            TowerDefense_MonsterWaveCallRuleCfg monsterWaveCallCfg =
                TowerDefense_MonsterWaveCallRuleCfgCategory.Instance.Get(self.monsterWaveRuleCfgId, waveIndex);
            int count = monsterWaveCallCfg.Nodes.Count;
            //执行时间点内的事情
            for (int i = 0; i < count; i++)
            {
                MonsterWaveCallNode monsterWaveCallNode = monsterWaveCallCfg.Nodes[i];
                string monsterCfgId = monsterWaveCallNode.MonsterCfgId;
                int totalNum = monsterWaveCallNode.TotalNum;
                if (monsterCfgId2Num.ContainsKey(monsterCfgId))
                {
                    monsterCfgId2Num[monsterCfgId] += totalNum;
                }
                else
                {
                    monsterCfgId2Num[monsterCfgId] = totalNum;
                }
            }
            return monsterCfgId2Num;
        }

        public static void RecoverWaveIndex(this MonsterWaveCallComponent self)
        {
            foreach (var item in self.waveMonsterCallList.Values)
            {
                foreach (MonsterWaveCallOnceComponent monsterWaveCallOnceComponent in item.Values)
                {
                    monsterWaveCallOnceComponent.Dispose();
                }
            }

            self.waveMonsterCallList.Clear();

            TimerComponent.Instance.Remove(ref self.Timer);

            self.curIndex--;
        }

        public static bool ChkIsGameEnd(this MonsterWaveCallComponent self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();

            if (self.GetGamePlayTowerDefense().ChkIsGameEnd()
                || self.GetGamePlayTowerDefense().ChkIsGameRecoverSuccess()
                || self.GetGamePlayTowerDefense().ChkIsGameRecover()
                || self.GetGamePlayTowerDefense().ChkIsGameRecovering())
            {
                return true;
            }
            return false;
        }

        public static void DoMonsterWaveCall(this MonsterWaveCallComponent self)
        {
            bool bRet = self.GetRealWaveInfo(out int waveIndex, out int circleWaveIndex, out int circleNum, out int circleIndex, out int stageWaveIndex, out float monsterWaveNumScalePercent,
                out float monsterWaveLevelScalePercent, out float waveRewardGoldScalePercent);
            if (bRet == false)
            {
                return;
            }
            self.circleWaveIndex = circleWaveIndex;
            self.circleNum = circleNum;
            self.circleIndex = circleIndex;
            self.stageWaveIndex = stageWaveIndex;

            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
            List<long> playerList = gamePlayTowerDefenseComponent.GetPlayerList();
            foreach (long playerId in playerList)
            {
                bool isNeedCreate = true;
                if (gamePlayTowerDefenseComponent.isTowerDefenseTeamOne)
                {
                    PutHomeComponent putHomeComponent = gamePlayTowerDefenseComponent.GetComponent<PutHomeComponent>();
                    if (putHomeComponent != null)
                    {
                        bool isPutHomePlayer = putHomeComponent.ChkIsPutHomePlayer(playerId);
                        if (isPutHomePlayer)
                        {
                            isNeedCreate = true;
                        }
                        else
                        {
                            isNeedCreate = false;
                        }
                    }
                }
                else
                {
                    isNeedCreate = true;
                }

                if (isNeedCreate == false)
                {
                    continue;
                }
                MonsterWaveCallOnceComponent monsterWaveCallOnceComponent = self.AddChild<MonsterWaveCallOnceComponent>();

                monsterWaveCallOnceComponent.Init(playerId, self.monsterWaveRuleCfgId, waveIndex, monsterWaveNumScalePercent, monsterWaveLevelScalePercent,
                    waveRewardGoldScalePercent);

                self.duration = monsterWaveCallOnceComponent.duration;
                self.nextLimitTime = (long)(self.duration*1000) + TimeHelper.ClientNow();

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

        public static bool GetRealWaveInfo(this MonsterWaveCallComponent self, out int waveIndex, out int circleWaveIndex, out int circleNum, out int circleIndex, out int stageWaveIndex,
        out float monsterWaveNumScalePercent, out float monsterWaveLevelScalePercent, out float waveRewardGoldScalePercent)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
            waveIndex = 0;
            circleWaveIndex = 0;
            circleNum = 0;
            circleIndex = 0;
            stageWaveIndex = 0;
            monsterWaveNumScalePercent = 0;
            monsterWaveLevelScalePercent = 0;
            waveRewardGoldScalePercent = 0;


            GamePlayBattleLevelCfg gamePlayBattleLevelCfg = gamePlayTowerDefenseComponent.GetGamePlay().GetGamePlayBattleConfig();
            if (gamePlayBattleLevelCfg.GamePlayMode is GamePlayTowerDefenseNormal gamePlayTowerDefenseNormal)
            {
                int stageNum = gamePlayTowerDefenseNormal.StageNum;
                if (stageNum <= 0)
                {
                    stageNum = 1;
                }
                float monsterWaveNumScalePercentCoefficientWhenStageNum = gamePlayTowerDefenseNormal.MonsterWaveNumScalePercentCoefficientWhenStageNum;
                float monsterWaveLevelScalePercentCoefficientWhenStageNum = gamePlayTowerDefenseNormal.MonsterWaveLevelScalePercentCoefficientWhenStageNum;
                float waveRewardGoldScalePercentCoefficientWhenStageNum = gamePlayTowerDefenseNormal.WaveRewardGoldScalePercentCoefficientWhenStageNum;
                if (self.totalCount < stageNum)
                {
                    stageNum = self.totalCount;
                }

                stageWaveIndex = self.curIndex / stageNum;

                monsterWaveNumScalePercent += stageWaveIndex * monsterWaveNumScalePercentCoefficientWhenStageNum;
                monsterWaveLevelScalePercent += stageWaveIndex * monsterWaveLevelScalePercentCoefficientWhenStageNum;
                waveRewardGoldScalePercent += stageWaveIndex * waveRewardGoldScalePercentCoefficientWhenStageNum;

                if (self.curIndex == -1)
                {
                    waveIndex = self.sortWaveIndex.FirstOrDefault();
                }
                else
                {
                    waveIndex = self.sortWaveIndex[self.curIndex];
                }
            }
            else if (gamePlayBattleLevelCfg.GamePlayMode is GamePlayTowerDefenseNormalTeamOne gamePlayTowerDefenseNormalTeamOne)
            {
                int stageNum = gamePlayTowerDefenseNormalTeamOne.StageNum;
                if (stageNum <= 0)
                {
                    stageNum = 1;
                }
                float monsterWaveNumScalePercentCoefficientWhenStageNum = gamePlayTowerDefenseNormalTeamOne.MonsterWaveNumScalePercentCoefficientWhenStageNum;
                float monsterWaveLevelScalePercentCoefficientWhenStageNum = gamePlayTowerDefenseNormalTeamOne.MonsterWaveLevelScalePercentCoefficientWhenStageNum;
                float waveRewardGoldScalePercentCoefficientWhenStageNum = gamePlayTowerDefenseNormalTeamOne.WaveRewardGoldScalePercentCoefficientWhenStageNum;
                if (self.totalCount < stageNum)
                {
                    stageNum = self.totalCount;
                }

                stageWaveIndex = self.curIndex / stageNum;

                monsterWaveNumScalePercent += stageWaveIndex * monsterWaveNumScalePercentCoefficientWhenStageNum;
                monsterWaveLevelScalePercent += stageWaveIndex * monsterWaveLevelScalePercentCoefficientWhenStageNum;
                waveRewardGoldScalePercent += stageWaveIndex * waveRewardGoldScalePercentCoefficientWhenStageNum;

                if (self.curIndex == -1)
                {
                    waveIndex = self.sortWaveIndex.FirstOrDefault();
                }
                else
                {
                    waveIndex = self.sortWaveIndex[self.curIndex];
                }
            }
            else if (gamePlayBattleLevelCfg.GamePlayMode is GamePlayTowerDefenseEndlessChallengeMonster gamePlayTowerDefenseEndlessChallengeMonster)
            {
                int stageNum = gamePlayTowerDefenseEndlessChallengeMonster.StageNum;
                if (stageNum <= 0)
                {
                    stageNum = 1;
                }
                float monsterWaveNumScalePercentCoefficientWhenStageNum = gamePlayTowerDefenseEndlessChallengeMonster.MonsterWaveNumScalePercentCoefficientWhenStageNum;
                float monsterWaveLevelScalePercentCoefficientWhenStageNum = gamePlayTowerDefenseEndlessChallengeMonster.MonsterWaveLevelScalePercentCoefficientWhenStageNum;
                float waveRewardGoldScalePercentCoefficientWhenStageNum = gamePlayTowerDefenseEndlessChallengeMonster.WaveRewardGoldScalePercentCoefficientWhenStageNum;

                if (self.totalCount < stageNum)
                {
                    stageNum = self.totalCount;
                }

                stageWaveIndex = self.curIndex / stageNum;

                monsterWaveNumScalePercent += stageWaveIndex * monsterWaveNumScalePercentCoefficientWhenStageNum;
                monsterWaveLevelScalePercent += stageWaveIndex * monsterWaveLevelScalePercentCoefficientWhenStageNum;
                waveRewardGoldScalePercent += stageWaveIndex * waveRewardGoldScalePercentCoefficientWhenStageNum;

                if (self.curIndex >= self.totalCount)
                {
                    int repeatNum = gamePlayTowerDefenseEndlessChallengeMonster.RepeatNum;
                    if (repeatNum <= 0)
                    {
                        repeatNum = 1;
                    }
                    float monsterWaveNumScalePercentCoefficientWhenRepeatNum = gamePlayTowerDefenseEndlessChallengeMonster.MonsterWaveNumScalePercentCoefficientWhenRepeatNum;
                    float monsterWaveLevelScalePercentCoefficientWhenRepeatNum = gamePlayTowerDefenseEndlessChallengeMonster.MonsterWaveLevelScalePercentCoefficientWhenRepeatNum;
                    float waveRewardGoldScalePercentCoefficientWhenRepeatNum = gamePlayTowerDefenseEndlessChallengeMonster.WaveRewardGoldScalePercentCoefficientWhenRepeatNum;
                    if (self.totalCount < repeatNum)
                    {
                        repeatNum = self.totalCount;
                    }

                    int tmp1 = (int)(1f * (self.curIndex - self.totalCount) / repeatNum) + 1;
                    int tmp2 = (self.curIndex - self.totalCount) % repeatNum;

                    waveIndex = self.sortWaveIndex[self.totalCount - (repeatNum - tmp2)];
                    circleWaveIndex = self.curIndex + 1 - self.totalCount;
                    circleNum = tmp1;
                    circleIndex = tmp2;
                    monsterWaveNumScalePercent += tmp1 * monsterWaveNumScalePercentCoefficientWhenRepeatNum;
                    monsterWaveLevelScalePercent += tmp1 * monsterWaveLevelScalePercentCoefficientWhenRepeatNum;
                    waveRewardGoldScalePercent += tmp1 * waveRewardGoldScalePercentCoefficientWhenRepeatNum;
                }
                else
                {
                    if (self.curIndex == -1)
                    {
                        waveIndex = self.sortWaveIndex.FirstOrDefault();
                    }
                    else
                    {
                        waveIndex = self.sortWaveIndex[self.curIndex];
                    }
                }
            }
            else if (gamePlayBattleLevelCfg.GamePlayMode is GamePlayTowerDefenseEndlessChallengeMonsterTeamOne gamePlayTowerDefenseEndlessChallengeMonsterTeamOne)
            {
                int stageNum = gamePlayTowerDefenseEndlessChallengeMonsterTeamOne.StageNum;
                if (stageNum <= 0)
                {
                    stageNum = 1;
                }
                float monsterWaveNumScalePercentCoefficientWhenStageNum = gamePlayTowerDefenseEndlessChallengeMonsterTeamOne.MonsterWaveNumScalePercentCoefficientWhenStageNum;
                float monsterWaveLevelScalePercentCoefficientWhenStageNum = gamePlayTowerDefenseEndlessChallengeMonsterTeamOne.MonsterWaveLevelScalePercentCoefficientWhenStageNum;
                float waveRewardGoldScalePercentCoefficientWhenStageNum = gamePlayTowerDefenseEndlessChallengeMonsterTeamOne.WaveRewardGoldScalePercentCoefficientWhenStageNum;

                if (self.totalCount < stageNum)
                {
                    stageNum = self.totalCount;
                }

                stageWaveIndex = self.curIndex / stageNum;

                monsterWaveNumScalePercent += stageWaveIndex * monsterWaveNumScalePercentCoefficientWhenStageNum;
                monsterWaveLevelScalePercent += stageWaveIndex * monsterWaveLevelScalePercentCoefficientWhenStageNum;
                waveRewardGoldScalePercent += stageWaveIndex * waveRewardGoldScalePercentCoefficientWhenStageNum;

                if (self.curIndex >= self.totalCount)
                {
                    int repeatNum = gamePlayTowerDefenseEndlessChallengeMonsterTeamOne.RepeatNum;
                    if (repeatNum <= 0)
                    {
                        repeatNum = 1;
                    }
                    float monsterWaveNumScalePercentCoefficientWhenRepeatNum = gamePlayTowerDefenseEndlessChallengeMonsterTeamOne.MonsterWaveNumScalePercentCoefficientWhenRepeatNum;
                    float monsterWaveLevelScalePercentCoefficientWhenRepeatNum = gamePlayTowerDefenseEndlessChallengeMonsterTeamOne.MonsterWaveLevelScalePercentCoefficientWhenRepeatNum;
                    float waveRewardGoldScalePercentCoefficientWhenRepeatNum = gamePlayTowerDefenseEndlessChallengeMonsterTeamOne.WaveRewardGoldScalePercentCoefficientWhenRepeatNum;
                    if (self.totalCount < repeatNum)
                    {
                        repeatNum = self.totalCount;
                    }

                    int tmp1 = (int)(1f * (self.curIndex - self.totalCount) / repeatNum) + 1;
                    int tmp2 = (self.curIndex - self.totalCount) % repeatNum;

                    waveIndex = self.sortWaveIndex[self.totalCount - (repeatNum - tmp2)];
                    circleWaveIndex = self.curIndex + 1 - self.totalCount;
                    circleNum = tmp1;
                    circleIndex = tmp2;
                    monsterWaveNumScalePercent += tmp1 * monsterWaveNumScalePercentCoefficientWhenRepeatNum;
                    monsterWaveLevelScalePercent += tmp1 * monsterWaveLevelScalePercentCoefficientWhenRepeatNum;
                    waveRewardGoldScalePercent += tmp1 * waveRewardGoldScalePercentCoefficientWhenRepeatNum;
                }
                else
                {
                    if (self.curIndex == -1)
                    {
                        waveIndex = self.sortWaveIndex.FirstOrDefault();
                    }
                    else
                    {
                        waveIndex = self.sortWaveIndex[self.curIndex];
                    }
                }
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
                randomForward, teamFlagType, rewardGold, self.curIndex + 1, self.circleWaveIndex, self.circleNum, self.circleIndex, self.stageWaveIndex);

            monsterUnit.AddComponent<UnitWaitResetPosComponent, float3>(pos);


            GamePlayBattleLevelCfg gamePlayBattleLevelCfg = gamePlayTowerDefenseComponent.GetGamePlay().GetGamePlayBattleConfig();
            if (gamePlayBattleLevelCfg.GamePlayMode is GamePlayTowerDefenseEndlessChallengeMonster gamePlayTowerDefenseEndlessChallengeMonster)
            {
                ET.GamePlayHelper.DoCreateActions(monsterUnit, gamePlayTowerDefenseEndlessChallengeMonster.CreateActionIds).Coroutine();

                if (self.curIndex >= self.totalCount)
                {
                    ET.GamePlayHelper.DoCreateActions(monsterUnit, gamePlayTowerDefenseEndlessChallengeMonster.CreateActionIdsWhenRepeat).Coroutine();
                }

            }
            else if (gamePlayBattleLevelCfg.GamePlayMode is GamePlayTowerDefenseEndlessChallengeMonsterTeamOne gamePlayTowerDefenseEndlessChallengeMonsterTeamOne)
            {
                ET.GamePlayHelper.DoCreateActions(monsterUnit, gamePlayTowerDefenseEndlessChallengeMonsterTeamOne.CreateActionIds).Coroutine();

                if (self.curIndex >= self.totalCount)
                {
                    ET.GamePlayHelper.DoCreateActions(monsterUnit, gamePlayTowerDefenseEndlessChallengeMonsterTeamOne.CreateActionIdsWhenRepeat).Coroutine();
                }

            }
            else if (gamePlayBattleLevelCfg.GamePlayMode is GamePlayTowerDefenseNormal gamePlayTowerDefenseNormal)
            {
                ET.GamePlayHelper.DoCreateActions(monsterUnit, gamePlayTowerDefenseNormal.CreateActionIds).Coroutine();
            }
            else if (gamePlayBattleLevelCfg.GamePlayMode is GamePlayTowerDefenseNormalTeamOne gamePlayTowerDefenseNormalTeamOne)
            {
                ET.GamePlayHelper.DoCreateActions(monsterUnit, gamePlayTowerDefenseNormalTeamOne.CreateActionIds).Coroutine();
            }

            return monsterUnit;
        }

        public static void ChkMonsterCallAllClearWithRest(this MonsterWaveCallComponent self)
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

        public static void ClearMonsterCallWhenDebug(this MonsterWaveCallComponent self)
        {
            TimerComponent.Instance.Remove(ref self.Timer);
            if (self.IsDisposed)
            {
                return;
            }

            foreach (var playerWaveMonsterCall in self.waveMonsterCallList)
            {
                foreach (var waveMonsterCall in playerWaveMonsterCall.Value)
                {
                    MonsterWaveCallOnceComponent monsterWaveCallOnceComponent = waveMonsterCall.Value;
                    monsterWaveCallOnceComponent.ClearMonsterCallWhenDebug();
                }
            }

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
        }

        public static void RecordMonsterWhenCallActor(this MonsterWaveCallComponent self, Unit unit, Unit beCallActor)
        {
            MonsterComponent monsterComponent = unit.GetComponent<MonsterComponent>();
            if (monsterComponent == null)
            {
                return;
            }

            if (monsterComponent.waveIndex <= 0)
            {
                return;
            }

            foreach (var playerWaveMonsterCall in self.waveMonsterCallList)
            {
                foreach (var waveMonsterCall in playerWaveMonsterCall.Value)
                {
                    MonsterWaveCallOnceComponent monsterWaveCallOnceComponent = waveMonsterCall.Value;
                    bool bRet = monsterWaveCallOnceComponent.RecordMonsterWhenCallActor(unit, beCallActor);
                    if (bRet)
                    {
                        MonsterComponent monsterComponentBeCall = beCallActor.AddComponent<MonsterComponent>();
                        monsterComponentBeCall.waveIndex = monsterComponent.waveIndex;
                        monsterComponentBeCall.circleWaveIndex = monsterComponent.circleWaveIndex;
                        monsterComponentBeCall.circleNum = monsterComponent.circleNum;
                        monsterComponentBeCall.circleIndex = monsterComponent.circleIndex;
                        monsterComponentBeCall.stageWaveIndex = monsterComponent.stageWaveIndex;
                    }
                }
            }
        }

        public static void ChkMonsterCallTimeOutWithRest(this MonsterWaveCallComponent self)
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

        public static void ChkMonsterCallClearNoRest(this MonsterWaveCallComponent self)
        {
            if (self.IsDisposed)
            {
                TimerComponent.Instance.Remove(ref self.Timer);
                return;
            }

            foreach (var playerWaveMonsterCall in self.waveMonsterCallList.Values)
            {
                float maxLastTime = 0;
                while (true)
                {
                    foreach (var item in playerWaveMonsterCall)
                    {
                        if (item.Value.IsDisposed)
                        {
                            playerWaveMonsterCall.Remove(item.Key);
                            break;
                        }
                    }
                    break;
                }
                foreach (var waveMonsterCall in playerWaveMonsterCall)
                {
                    MonsterWaveCallOnceComponent monsterWaveCallOnceComponent = waveMonsterCall.Value;
                    if (maxLastTime < monsterWaveCallOnceComponent.duration)
                    {
                        maxLastTime = monsterWaveCallOnceComponent.duration;
                    }
                    if (monsterWaveCallOnceComponent.ChkMonsterCallAllClear())
                    {
                        monsterWaveCallOnceComponent.waitingDestroy = true;
                    }
                }

                if (self.isNextWaveWhenClearWhenNoRest)
                {
                    self.duration = maxLastTime;
                }
                else
                {
                    if (maxLastTime > 0)
                    {
                        self.duration = maxLastTime;
                    }
                }
            }
        }

        public static void ChkMonsterCallTimeOutNoRest(this MonsterWaveCallComponent self)
        {
            if (self.IsDisposed)
            {
                TimerComponent.Instance.Remove(ref self.Timer);
                return;
            }

            if (self.duration <= 0 || self.nextLimitTime <= TimeHelper.ClientNow())
            {
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
    }
}