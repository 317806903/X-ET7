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
        }

        public static bool InitHomeByPlayer(this PutHomeComponent self, long playerId, string unitCfgId, float3 homePos)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
            TeamFlagType teamFlagType = gamePlayTowerDefenseComponent.GetHomeTeamFlagTypeByPlayer(playerId);
            if (self.HomeUnitIdList.ContainsKey(teamFlagType))
            {
                return false;
            }

            int hp = gamePlayTowerDefenseComponent.model.HomeLife;
            Unit homeUnit = self.CreateHome(unitCfgId, homePos, hp, hp, teamFlagType);
            self.HomeUnitIdList[teamFlagType] = homeUnit.Id;

            self.ChkNextStep().Coroutine();
            return true;
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

        public static Unit CreateHome(this PutHomeComponent self, string unitCfgId, float3 pos, int maxHp, int hp, TeamFlagType teamFlagType)
        {
            return GamePlayTowerDefenseHelper.CreateHome(self.DomainScene(), unitCfgId, pos, maxHp, hp, teamFlagType);
        }

        public static Unit GetHomeUnit(this PutHomeComponent self, Unit unit)
        {
            TeamFlagType teamFlagType = self.GetGamePlay().GetTeamFlagByUnit(unit);
            return self.GetHomeUnitByTeamFlagType(teamFlagType);
        }

        public static Unit GetHomeUnit(this PutHomeComponent self, long playerId)
        {
            TeamFlagType teamFlagType = self.GetGamePlayTowerDefense().GetHomeTeamFlagTypeByPlayer(playerId);
            return self.GetHomeUnitByTeamFlagType(teamFlagType);
        }

        public static Dictionary<TeamFlagType, long> GetHomeUnitList(this PutHomeComponent self)
        {
            return self.HomeUnitIdList;
        }

        public static Unit GetHomeUnitByTeamFlagType(this PutHomeComponent self, TeamFlagType teamFlagType)
        {
            TeamFlagType homeTeamFlagType = ET.GamePlayTowerDefenseHelper.GetHomeTeamFlagType(teamFlagType);
            if (self.HomeUnitIdList.TryGetValue(homeTeamFlagType, out long homeUnitId))
            {
                return UnitHelper.GetUnit(self.DomainScene(), self.HomeUnitIdList[homeTeamFlagType]);
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

        public static float3 GetMidPos(this PutHomeComponent self)
        {
            Dictionary<TeamFlagType, long> homeUnitList = self.GetHomeUnitList();
            if (homeUnitList.Count == 2)
            {
                return self.GetMidPosWhen2Homes(homeUnitList);
            }
            else if (homeUnitList.Count == 3)
            {
                return self.GetMidPosWhen3Homes(homeUnitList);
            }

            return float3.zero;
        }

        public static float3 GetMidPosWhen2Homes(this PutHomeComponent self, Dictionary<TeamFlagType, long> homeUnitList)
        {
            Unit homeUnit1 = null;
            Unit homeUnit2 = null;
            List<long> list = homeUnitList.Values.ToList();
            for (int i = 0; i < list.Count; i++)
            {
                Unit homeUnit = ET.Ability.UnitHelper.GetUnit(self.DomainScene(), list[i]);
                if (i == 0)
                {
                    homeUnit1 = homeUnit;
                }
                else if (i == 1)
                {
                    homeUnit2 = homeUnit;
                }
            }

            Unit observerUnit = self.GetOneObserverUnit();

            List<float3> points = ET.RecastHelper.GetArrivePath(observerUnit, homeUnit1.Position, homeUnit2.Position);
            if (points == null)
            {
                return float3.zero;
            }

            if (points.Count <= 1)
            {
                return float3.zero;
            }

            float totalLength = 0;
            float3 midPos = float3.zero;
            for (int i = 1; i < points.Count; i++)
            {
                totalLength += math.length(points[i] - points[i - 1]);
            }

            float curLength = 0;
            for (int i = 1; i < points.Count; i++)
            {
                float lastLength = curLength;
                curLength += math.length(points[i] - points[i - 1]);
                if (lastLength <= 0.5f * totalLength && curLength > 0.5f * totalLength)
                {
                    float needLength = 0.5f * totalLength - lastLength;
                    midPos = points[i - 1] + math.normalize(points[i] - points[i - 1]) * needLength;
                    break;
                }
            }

            return midPos;
        }

        public static float3 GetMidPosWhen3Homes(this PutHomeComponent self, Dictionary<TeamFlagType, long> homeUnitList)
        {
            Unit homeUnit1 = null;
            Unit homeUnit2 = null;
            Unit homeUnit3 = null;
            List<long> list = homeUnitList.Values.ToList();
            for (int i = 0; i < list.Count; i++)
            {
                Unit homeUnit = ET.Ability.UnitHelper.GetUnit(self.DomainScene(), list[i]);
                if (i == 0)
                {
                    homeUnit1 = homeUnit;
                }
                else if (i == 1)
                {
                    homeUnit2 = homeUnit;
                }
                else if (i == 2)
                {
                    homeUnit3 = homeUnit;
                }
            }

            float2 circleCenter2D = GetCircleCenter(new float2(homeUnit1.Position.x, homeUnit1.Position.z),
                new float2(homeUnit2.Position.x, homeUnit2.Position.z),
                new float2(homeUnit3.Position.x, homeUnit3.Position.z));
            float3 circleCenterOrg = new float3(circleCenter2D.x, homeUnit1.Position.y, circleCenter2D.y);

            Func<Unit, float3, (bool, float3)> chkCanReach = (observerUnit, pos) =>
            {
                float3 centerPos = ET.RecastHelper.GetNearNavmeshPos(observerUnit, pos);

                if (self.ChkHomeUnitPosition(homeUnit1, centerPos) == false)
                {
                    return (false, float3.zero);
                }

                if (self.ChkHomeUnitPosition(homeUnit2, centerPos) == false)
                {
                    return (false, float3.zero);
                }

                if (self.ChkHomeUnitPosition(homeUnit3, centerPos) == false)
                {
                    return (false, float3.zero);
                }

                return (true, centerPos);
            };
            Unit observerUnit = self.GetOneObserverUnit();
            float dis = 0.1f;
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    float3 circleCenter = circleCenterOrg + new float3(dis * i, 0, dis * j);
                    (bool canReach, float3 point) = chkCanReach(observerUnit, circleCenter);
                    if (canReach)
                    {
                        return point;
                    }

                    circleCenter = circleCenterOrg + new float3(-dis * i, 0, dis * j);
                    (canReach, point) = chkCanReach(observerUnit, circleCenter);
                    if (canReach)
                    {
                        return point;
                    }

                    circleCenter = circleCenterOrg + new float3(-dis * i, 0, -dis * j);
                    (canReach, point) = chkCanReach(observerUnit, circleCenter);
                    if (canReach)
                    {
                        return point;
                    }

                    circleCenter = circleCenterOrg + new float3(dis * i, 0, -dis * j);
                    (canReach, point) = chkCanReach(observerUnit, circleCenter);
                    if (canReach)
                    {
                        return point;
                    }
                }
            }

            return float3.zero;
        }

        public static Unit GetOneObserverUnit(this PutHomeComponent self)
        {
            Unit observerUnit = null;
            UnitComponent unitComponent = UnitHelper.GetUnitComponent(self.DomainScene());
            foreach (var _observerUnit in unitComponent.observerList)
            {
                observerUnit = _observerUnit;
                break;
            }

            return observerUnit;
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

        public static float2 GetCircleCenter(float2 p1, float2 p2, float2 p3)
        {
            float a = p1.x - p2.x;
            float b = p1.y - p2.y;
            float c = p1.x - p3.x;
            float d = p1.y - p3.y;
            float e = (math.pow(p1.x, 2) - math.pow(p2.x, 2) + math.pow(p1.y, 2) - math.pow(p2.y, 2)) / 2.0f;
            float f = (math.pow(p1.x, 2) - math.pow(p3.x, 2) + math.pow(p1.y, 2) - math.pow(p3.y, 2)) / 2.0f;
            float det = b * c - a * d;
            if (math.abs(det) > 0)
            {
                //x0,y0为计算得到的原点
                float x0 = -(d * e - b * f) / det;
                float y0 = -(a * f - c * e) / det;
                return new float2(x0, y0);
            }
            else
            {
                return new float2(0, 0);
            }
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

        public static void RecoverHomeHp(this PutHomeComponent self)
        {
            Dictionary<TeamFlagType, long> tmpHomeUnitIdList = self.HomeUnitIdList;
            self.HomeUnitIdList = new();
            foreach (var homeUnitId in tmpHomeUnitIdList)
            {
                (string homeCfgId, float3 homePos, int maxHp, int curHp) = self.RecordHomeInfo[homeUnitId.Value];
                TeamFlagType teamFlagType = homeUnitId.Key;
                curHp = math.min(GlobalSettingCfgCategory.Instance.AREndlessChallengeRecoverHp + curHp, maxHp);
                Unit newHomeUnit = self.CreateHome(homeCfgId, homePos, maxHp, curHp, teamFlagType);
                self.HomeUnitIdList[homeUnitId.Key] = newHomeUnit.Id;
            }
        }
    }
}