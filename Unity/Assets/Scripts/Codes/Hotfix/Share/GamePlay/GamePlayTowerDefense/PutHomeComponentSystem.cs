using ET.Ability;
using System;
using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    [FriendOf(typeof (PutHomeComponent))]
    public static class PutHomeComponentSystem
    {
        [ObjectSystem]
        public class PutHomeComponentAwakeSystem: AwakeSystem<PutHomeComponent>
        {
            protected override void Awake(PutHomeComponent self)
            {
                self.HomeUnitIdList = new();
                self.TeamFlagType2PlayerIdCanPutHome = new();
                self.TeamFlagType2Result = new();

                self.Init();
            }
        }

        [ObjectSystem]
        public class PutHomeComponentDestroySystem: DestroySystem<PutHomeComponent>
        {
            protected override void Destroy(PutHomeComponent self)
            {
                self.HomeUnitIdList?.Clear();
                self.TeamFlagType2PlayerIdCanPutHome?.Clear();
                self.TeamFlagType2Result?.Clear();
            }
        }

        [ObjectSystem]
        public class RestTimeComponentFixedUpdateSystem: FixedUpdateSystem<PutHomeComponent>
        {
            protected override void FixedUpdate(PutHomeComponent self)
            {
                if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static void FixedUpdate(this PutHomeComponent self, float fixedDeltaTime)
        {
            if (self.GetGamePlayTowerDefense().ChkIsGameEnd()
                || self.GetGamePlayTowerDefense().ChkIsGameRecover()
                || self.GetGamePlayTowerDefense().ChkIsGameRecovering())
            {
                return;
            }

            self.ChkGameEnd();
        }

        public static GamePlayComponent GetGamePlay(this PutHomeComponent self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetParent<GamePlayTowerDefenseComponent>();
            GamePlayComponent gamePlayComponent = gamePlayTowerDefenseComponent.GetParent<GamePlayComponent>();
            return gamePlayComponent;
        }

        public static GamePlayTowerDefenseComponent GetGamePlayTowerDefense(this PutHomeComponent self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetParent<GamePlayTowerDefenseComponent>();
            return gamePlayTowerDefenseComponent;
        }

        public static GamePlayBattleLevelCfg GetGamePlayBattleConfig(this PutHomeComponent self)
        {
            GamePlayComponent gamePlayComponent = self.GetGamePlay();
            return gamePlayComponent.GetGamePlayBattleConfig();
        }

        public static void Init(this PutHomeComponent self)
        {
            Dictionary<long, TeamFlagType> allPlayerTeamFlag = self.GetGamePlay().GetAllPlayerTeamFlag();

            long ownerPlayerId = self.GetGamePlay().ownerPlayerId;
            TeamFlagType ownerTeamFlagType = allPlayerTeamFlag[ownerPlayerId];
            self.TeamFlagType2PlayerIdCanPutHome[ownerTeamFlagType] = ownerPlayerId;

            foreach (var playerTeamFlag in allPlayerTeamFlag)
            {
                TeamFlagType teamFlagType = playerTeamFlag.Value;
                if (self.TeamFlagType2PlayerIdCanPutHome.ContainsKey(teamFlagType))
                {
                    continue;
                }

                long playerId = playerTeamFlag.Key;
                self.TeamFlagType2PlayerIdCanPutHome.Add(teamFlagType, playerId);
            }

            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
            foreach (var playerTeamFlag in allPlayerTeamFlag)
            {
                long playerId = playerTeamFlag.Key;
                TeamFlagType teamFlagType = gamePlayTowerDefenseComponent.GetHomeTeamFlagTypeByPlayer(playerId);

                GamePlayHelper.ChgGamePlayNumericValueByHomeTeamFlagType(self.DomainScene(), teamFlagType, GameNumericType.TowerDefense_HomeMaxHpBase, gamePlayTowerDefenseComponent.model.HomeLife, true);

            }
        }

        public static bool InitHomeByPlayer(this PutHomeComponent self, long playerId, string unitCfgId, float3 homePos)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
            TeamFlagType homeTeamFlagType = gamePlayTowerDefenseComponent.GetHomeTeamFlagTypeByPlayer(playerId);
            if (self.HomeUnitIdList.ContainsKey(homeTeamFlagType))
            {
                return false;
            }

            float hp = GamePlayHelper.GetGamePlayNumericValueByHomeTeamFlagType(self.DomainScene(), homeTeamFlagType, GameNumericType.TowerDefense_HomeMaxHp);
            Unit homeUnit = self.CreateHome(unitCfgId, homePos, (int)hp, (int)hp, homeTeamFlagType);
            self.HomeUnitIdList[homeTeamFlagType] = homeUnit.Id;

            self.ChkNextStep().Coroutine();
            return true;
        }

        public static void ResetByPlayer(this PutHomeComponent self, long playerId)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
            TeamFlagType homeTeamFlagType = gamePlayTowerDefenseComponent.GetHomeTeamFlagTypeByPlayer(playerId);
            if (self.HomeUnitIdList.TryGetValue(homeTeamFlagType, out long homeUnitId))
            {
                Unit homeUnit = UnitHelper.GetUnit(self.DomainScene(), homeUnitId);
                homeUnit.DestroyNotDeathShow();
                self.HomeUnitIdList.Remove(homeTeamFlagType);
            }
        }

        public static void RecoveryHomeHP(this PutHomeComponent self)
        {
            ActionContext actionContext = new();
            foreach (var child in self.HomeUnitIdList)
            {
                TeamFlagType homeTeamFlagType = child.Key;
                long homeUnitId = child.Value;
                Unit homeUnit = UnitHelper.GetUnit(self.DomainScene(), homeUnitId);
                NumericComponent numericComponent = homeUnit.GetComponent<NumericComponent>();
                float curHomeHp = numericComponent.GetAsFloat((int)NumericType.Hp);

                GamePlayHelper.ChgGamePlayNumericValueByHomeTeamFlagType(self.DomainScene(), homeTeamFlagType, GameNumericType.TowerDefense_HomeRecoveryCurHpBase, curHomeHp, true);
                float newHomeHp = GamePlayHelper.GetGamePlayNumericValueByHomeTeamFlagType(self.DomainScene(), homeTeamFlagType, GameNumericType.TowerDefense_HomeRecoveryCurHp);
                float maxHp = numericComponent.GetAsFloat(NumericType.MaxHp);
                float newHp = math.min(math.max(0, newHomeHp), maxHp);

                int attackValue = (int)(curHomeHp - newHp);
                if (attackValue < 0)
                {
                    Damage damage = new(NumericType.PhysicalAttack, attackValue);
                    ET.Ability.DamageHelper.CreateDamageInfo(homeUnit, homeUnit, damage, false, ref actionContext);
                }
            }
        }

        public static async ETTask ChkNextStep(this PutHomeComponent self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();

            foreach (var teamFlag in self.TeamFlagType2PlayerIdCanPutHome.Keys)
            {
                TeamFlagType homeTeamFlagType = ET.GamePlayTowerDefenseHelper.GetHomeTeamFlagType(teamFlag);
                if (self.HomeUnitIdList.ContainsKey(homeTeamFlagType) == false)
                {
                    gamePlayTowerDefenseComponent.NoticeToClientAll();
                    return;
                }
            }

            gamePlayTowerDefenseComponent.DealFriendTeamFlagType();

            EventSystem.Instance.Publish(self.DomainScene(), new ET.Ability.AbilityTriggerEventType.GamePlayTowerDefense_Status_PutHomeEnd());

            await gamePlayTowerDefenseComponent.DoNextStep();
        }

        public static (bool, bool) ChkCanPutHome(this PutHomeComponent self, long playerId)
        {
            TeamFlagType homeTeamFlagType = self.GetGamePlayTowerDefense().GetHomeTeamFlagTypeByPlayer(playerId);
            if (self.HomeUnitIdList.ContainsKey(homeTeamFlagType))
            {
                return (false, true);
            }

            foreach (var _playerId in self.TeamFlagType2PlayerIdCanPutHome.Values)
            {
                if (playerId == _playerId)
                {
                    return (true, false);
                }
            }

            return (false, false);
        }

        public static bool ChkIsPutHomePlayer(this PutHomeComponent self, long playerId)
        {
            foreach (var _playerId in self.TeamFlagType2PlayerIdCanPutHome.Values)
            {
                if (playerId == _playerId)
                {
                    return true;
                }
            }

            return false;
        }

        public static Unit CreateHome(this PutHomeComponent self, string unitCfgId, float3 pos, int maxHp, int hp, TeamFlagType teamFlagType)
        {
            return GamePlayTowerDefenseHelper.CreateHome(self.DomainScene(), unitCfgId, pos, maxHp, hp, teamFlagType);
        }

        public static Unit GetHomeUnit(this PutHomeComponent self, TeamFlagType teamFlagType)
        {
            return self._GetHomeUnitByTeamFlagType(teamFlagType);
        }

        public static Unit GetHomeUnit(this PutHomeComponent self, Unit unit)
        {
            TeamFlagType teamFlagType = self.GetGamePlay().GetTeamFlagByUnitId(unit.Id);
            return self._GetHomeUnitByTeamFlagType(teamFlagType);
        }

        public static Unit GetHomeUnit(this PutHomeComponent self, long playerId)
        {
            TeamFlagType teamFlagType = self.GetGamePlayTowerDefense().GetHomeTeamFlagTypeByPlayer(playerId);
            return self._GetHomeUnitByTeamFlagType(teamFlagType);
        }

        public static float3 GetPosition(this PutHomeComponent self, long playerId)
        {
            Unit homeUnit = self.GetHomeUnit(playerId);
            return homeUnit.Position;
        }

        public static Dictionary<TeamFlagType, long> GetHomeUnitList(this PutHomeComponent self)
        {
            return self.HomeUnitIdList;
        }

        public static Unit _GetHomeUnitByTeamFlagType(this PutHomeComponent self, TeamFlagType teamFlagType)
        {
            TeamFlagType homeTeamFlagType = ET.GamePlayTowerDefenseHelper.GetHomeTeamFlagType(teamFlagType);
            if (self.HomeUnitIdList.TryGetValue(homeTeamFlagType, out long homeUnitId))
            {
                return UnitHelper.GetUnit(self.DomainScene(), homeUnitId);
            }
            return null;
        }

        public static bool ChkGameEnd(this PutHomeComponent self)
        {
            foreach (var homeUnitId in self.HomeUnitIdList)
            {
                Unit homeUnit = UnitHelper.GetUnit(self.DomainScene(), homeUnitId.Value);
                if (ET.Ability.UnitHelper.ChkUnitAlive(homeUnit) == false)
                {
                    if (ET.Ability.DeathShowHelper.ChkIsInDeath(homeUnit))
                    {
                        self.GetGamePlay().PauseAllAI();
                        return true;
                    }
                    self.GetGamePlayTowerDefense().TransToGameResult(false, false).Coroutine();
                    return false;
                }
            }

            return true;
        }

        public static bool ChkHomeWin(this PutHomeComponent self, long playerId)
        {
            TeamFlagType myHomeTeamFlagType = self.GetGamePlayTowerDefense().GetHomeTeamFlagTypeByPlayer(playerId);

            if (self.TeamFlagType2Result.TryGetValue(myHomeTeamFlagType, out bool win))
            {
                return win;
            }

            return false;
        }

        public static void BattleResult(this PutHomeComponent self, bool isTimeOut = false)
        {
            float maxHomeHp = -1;
            TeamFlagType maxHomeTeamFlagType = TeamFlagType.TeamGlobal1;
            foreach (var homeUnitId in self.HomeUnitIdList)
            {
                TeamFlagType homeTeamFlagType = homeUnitId.Key;
                Unit homeUnit = UnitHelper.GetUnit(self.DomainScene(), homeUnitId.Value);
                float hp = 0;
                if (ET.Ability.UnitHelper.ChkUnitAlive(homeUnit) == false)
                {
                    hp = 0;
                    if (maxHomeHp < 0)
                    {
                        maxHomeHp = hp;
                        maxHomeTeamFlagType = homeTeamFlagType;
                    }
                }
                else
                {
                    NumericComponent numericComponent = homeUnit.GetComponent<NumericComponent>();
                    hp = numericComponent.GetAsInt(NumericType.Hp);
                    if (maxHomeHp < hp)
                    {
                        maxHomeHp = hp;
                        maxHomeTeamFlagType = homeTeamFlagType;
                    }
                }
            }

            if (self.TeamFlagType2Result.Count > 1)
            {
                if (maxHomeHp > 0)
                {
                    self.TeamFlagType2Result[maxHomeTeamFlagType] = true;
                }
                else
                {
                    self.TeamFlagType2Result[maxHomeTeamFlagType] = false;
                }
            }
            else
            {
                if (isTimeOut)
                {
                }
                else
                {
                    if (maxHomeHp > 0)
                    {
                        self.TeamFlagType2Result[maxHomeTeamFlagType] = true;
                    }
                    else
                    {
                        self.TeamFlagType2Result[maxHomeTeamFlagType] = false;
                    }
                }
            }
        }

        public static (float3 midPos, float3 forward) GetMidPos(this PutHomeComponent self)
        {
            float3 midPos = float3.zero;
            float3 forward = float3.zero;
            Dictionary<TeamFlagType, long> homeUnitList = self.GetHomeUnitList();

            List<float3> posList = ListComponent<float3>.Create();
            foreach (long homeUnitId in homeUnitList.Values)
            {
                Unit homeUnit = ET.Ability.UnitHelper.GetUnit(self.DomainScene(), homeUnitId);
                posList.Add(homeUnit.Position);
            }

            if (homeUnitList.Count == 2)
            {
                midPos = self.GetMidPosWhen2Homes(posList);
            }
            else if (homeUnitList.Count == 3)
            {
                midPos = self.GetMidPosWhen3Homes(posList);
            }
            else if (homeUnitList.Count >= 4)
            {
                midPos = self.GetMidPosWhenNHomes(posList);
            }

            foreach (var pos in posList)
            {
                forward += midPos - pos;
            }

            if (forward.Equals(float3.zero))
            {
                forward = new float3(0, 0, 1);
            }
            forward = math.normalize(forward);
            return (midPos, forward);
        }

        public static float3 GetMidPosWhen2Homes(this PutHomeComponent self, List<float3> posList)
        {
            if (posList.Count != 2)
            {
                return float3.zero;
            }

            return ET.RecastHelper.GetMidPosWhen2Pos(self.DomainScene(), posList[0], posList[1]);
        }

        public static float3 GetMidPosWhen3Homes(this PutHomeComponent self, List<float3> posList)
        {
            if (posList.Count != 3)
            {
                return float3.zero;
            }

            return ET.RecastHelper.GetMidPosWhen3Pos(self.DomainScene(), posList);
        }

        public static float3 GetMidPosWhenNHomes(this PutHomeComponent self, List<float3> posList)
        {
            if (posList.Count < 4)
            {
                return float3.zero;
            }

            return ET.RecastHelper.GetMidPosWhenNPos(self.DomainScene(), posList);
        }

        public static Unit GetOneObserverUnit(this PutHomeComponent self)
        {
            return ET.Ability.UnitHelper.GetOneObserverUnit(self.DomainScene());
        }

        public static bool ChkPosition(this PutHomeComponent self, float3 putHomePos)
        {
            Dictionary<TeamFlagType, long> homeUnitList = self.GetHomeUnitList();
            if (homeUnitList.Count == 0)
            {
                return true;
            }

            foreach (var homeUnits in homeUnitList)
            {
                long homeUnitId = homeUnits.Value;
                Unit homeUnit = ET.Ability.UnitHelper.GetUnit(self.DomainScene(), homeUnitId);
                if (homeUnit != null)
                {
                    bool bRet = self.ChkHomeUnitPosition(homeUnit, putHomePos);
                    if (bRet == false)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool ChkHomeUnitPosition(this PutHomeComponent self, Unit homeUnit, float3 chkPos)
        {
            Unit observerUnit = self.GetOneObserverUnit();
            float3 homePos = homeUnit.Position;
            float3 startPos = chkPos;

            (bool canArrive, List<float3> pointList) = ET.RecastHelper.ChkArrive(observerUnit, startPos, homePos);
            return canArrive;
        }

        public static (bool, float3) ChkHomeUnitPositionAndForward(this PutHomeComponent self, Unit homeUnit, float3 chkPos)
        {
            Unit observerUnit = self.GetOneObserverUnit();
            float3 homePos = homeUnit.Position;
            float3 startPos = chkPos;

            (bool canArrive, List<float3> pointList) = ET.RecastHelper.ChkArrive(observerUnit, startPos, homePos);
            float3 forward = float3.zero;
            if (canArrive)
            {
                forward = pointList[0] - chkPos;
                if (forward.Equals(float3.zero))
                {
                    forward = pointList[1] - chkPos;
                }
            }
            return (canArrive, forward);
        }

        public static (TeamFlagType, Unit) GetNearHostileHomeByPlayerId(this PutHomeComponent self, TeamFlagType playerTeamFlagType, float3 pos)
        {
            TeamFlagType homeTeamFlagType = ET.GamePlayTowerDefenseHelper.GetHomeTeamFlagType(playerTeamFlagType);
            Dictionary<TeamFlagType, long> homeUnitList = self.GetHomeUnitList();

            float minDisSq = -1;
            float myHomeDisSq = -1;
            TeamFlagType minTeamFlagType = TeamFlagType.TeamGlobal1;
            Unit minHomeUnit = null;
            foreach (var homeUnits in homeUnitList)
            {
                TeamFlagType curHomeTeamFlagType = homeUnits.Key;

                // bool isFriend = self.GetGamePlay().ChkIsFriend(homeTeamFlagType, curHomeTeamFlagType);
                // if (isFriend)
                // {
                // 	continue;
                // }
                long homeUnitId = homeUnits.Value;
                Unit homeUnit = ET.Ability.UnitHelper.GetUnit(self.DomainScene(), homeUnitId);
                if (homeUnit != null)
                {
                    float curDisSq = math.lengthsq(homeUnit.Position - pos);
                    if (curHomeTeamFlagType == homeTeamFlagType)
                    {
                        myHomeDisSq = curDisSq;
                    }
                    else
                    {
                        if (minDisSq == -1 || minDisSq > curDisSq)
                        {
                            minDisSq = curDisSq;
                            minTeamFlagType = curHomeTeamFlagType;
                            minHomeUnit = homeUnit;
                        }
                    }
                }
            }

            if (minHomeUnit != null)
            {
                if (myHomeDisSq > minDisSq)
                {
                    return (minTeamFlagType, null);
                }
            }

            return (minTeamFlagType, minHomeUnit);
        }

        public static void RecordHomeHp(this PutHomeComponent self)
        {
            Dictionary<TeamFlagType, long> homeUnitList = self.GetHomeUnitList();
            foreach (var homeUnitId in homeUnitList)
            {
                Unit homeUnit = UnitHelper.GetUnit(self.DomainScene(), homeUnitId.Value);
                NumericComponent numericComponent = homeUnit.GetComponent<NumericComponent>();
                string homeCfgId = homeUnit.CfgId;
                float3 homePos = homeUnit.Position;
                int maxHp = numericComponent.GetAsInt(NumericType.MaxHp);
                int hp = numericComponent.GetAsInt(NumericType.Hp);
                self.RecordHomeInfo[homeUnitId.Value] = (homeCfgId, homePos, maxHp, hp);
            }
        }

        public static void RecoverHomeHp(this PutHomeComponent self, int recoverAddHp)
        {
            Dictionary<TeamFlagType, long> tmpHomeUnitIdList = self.HomeUnitIdList;
            self.HomeUnitIdList = new();
            foreach (var homeUnitId in tmpHomeUnitIdList)
            {
                (string homeCfgId, float3 homePos, int maxHp, int curHp) = self.RecordHomeInfo[homeUnitId.Value];
                TeamFlagType teamFlagType = homeUnitId.Key;
                curHp = math.min(recoverAddHp + curHp, maxHp);
                Unit newHomeUnit = self.CreateHome(homeCfgId, homePos, maxHp, curHp, teamFlagType);
                self.HomeUnitIdList[homeUnitId.Key] = newHomeUnit.Id;
            }
        }
    }
}