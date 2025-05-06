using ET.Ability;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    [FriendOf(typeof (GamePlayPlayerListComponent))]
    public static class GamePlayPlayerListComponentSystem
    {
        [ObjectSystem]
        public class GamePlayPlayerListComponentAwakeSystem: AwakeSystem<GamePlayPlayerListComponent>
        {
            protected override void Awake(GamePlayPlayerListComponent self)
            {
                self.playerId2UnitIds = new();
                self.playerId2PlayerUnitIdList = new();
                self.playerId2CameraPlayerUnitId = new();
                self.playerId2IsQuit = new();
                self.playerId2BirthPos = new();
                self.playerId2CoinList = new();
                self.team2CoinList = new();
            }
        }

        [ObjectSystem]
        public class GamePlayPlayerListComponentDestroySystem: DestroySystem<GamePlayPlayerListComponent>
        {
            protected override void Destroy(GamePlayPlayerListComponent self)
            {
                self.playerId2UnitIds?.Clear();
                self.playerId2PlayerUnitIdList?.Clear();
                self.playerId2CameraPlayerUnitId?.Clear();
                self.playerId2IsQuit?.Clear();
                self.playerId2BirthPos?.Clear();
                self.playerId2CoinList?.Clear();
                self.team2CoinList?.Clear();
            }
        }

        [ObjectSystem]
        public class GamePlayPlayerListComponentFixedUpdateSystem: FixedUpdateSystem<GamePlayPlayerListComponent>
        {
            protected override void FixedUpdate(GamePlayPlayerListComponent self)
            {
                if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static void FixedUpdate(this GamePlayPlayerListComponent self, float fixedDeltaTime)
        {
            if (++self.curFrameChk >= self.waitFrameChk)
            {
                self.curFrameChk = 0;

            }
        }

        public static void InitWhenRoom(this GamePlayPlayerListComponent self, RoomComponent roomComponent, List<RoomMember> roomMemberList)
        {
            for (int i = 0; i < roomMemberList.Count; i++)
            {
                RoomMember roomMember = roomMemberList[i];
                self.playerId2IsQuit[roomMember.Id] = false;
            }

            self.InitPlayerBirthPosWhenRoom(roomComponent, roomMemberList);
        }

        public static void InitWhenGlobal(this GamePlayPlayerListComponent self)
        {
        }

        public static void AddPlayerWhenGlobal(this GamePlayPlayerListComponent self, long playerId, int playerTeamId)
        {
            self.playerId2IsQuit[playerId] = false;
            self.InitPlayerBirthPosWhenGlobal(playerId, playerTeamId);
        }

        public static void PlayerQuitBattle(this GamePlayPlayerListComponent self, long playerId, bool isNeedRemoveAllPlayerUnits)
        {
            if (self.playerId2IsQuit[playerId])
            {
                return;
            }

            self.playerId2IsQuit[playerId] = true;

            Unit observerUnit = UnitHelper.GetUnit(self.DomainScene(), playerId);
            if (observerUnit != null)
            {
                Unit playerUnit = GamePlayHelper.GetCurPlayerUnit(observerUnit);
                if (playerUnit != null)
                {
                    self._InitPlayerBirthPos(playerId, playerUnit.Position, true);
                }
            }

            if (isNeedRemoveAllPlayerUnits)
            {
                if (self.playerId2UnitIds.TryGetValue(playerId, out var list))
                {
                    foreach (var unitId in list.ToList())
                    {
                        Unit unit = UnitHelper.GetUnit(self.DomainScene(), unitId);
                        if (unit != null)
                        {
                            unit.DestroyNotDeathShow();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 当unit创建时， 存储unitId 和 playerId 的关系
        /// </summary>
        /// <param name="self"></param>
        /// <param name="playerId"></param>
        /// <param name="unitId"></param>
        public static void AddUnitInfo(this GamePlayPlayerListComponent self, long playerId, long unitId, UnitType unitType)
        {
            self.playerId2UnitIds.Add(playerId, unitId);

            if (ET.Ability.UnitHelper.ChkIsCameraPlayer(unitType))
            {
                self.playerId2CameraPlayerUnitId.Add(playerId, unitId);
            }
            else if (ET.Ability.UnitHelper.ChkIsPlayer(unitType))
            {
                self.playerId2PlayerUnitIdList.Add(playerId, unitId);
            }
        }

        /// <summary>
        /// 当unit销毁时， 去掉已存储的unitId 和 playerId 的关系
        /// </summary>
        /// <param name="self"></param>
        /// <param name="unitId"></param>
        public static void RemoveUnitInfo(this GamePlayPlayerListComponent self, long unitId, UnitType unitType)
        {
            Unit unit = UnitHelper.GetUnit(self.DomainScene(), unitId);
            long playerId = TeamFlagHelper.GetPlayerId(unit);
            self.playerId2UnitIds.Remove(playerId, unitId);

            if (ET.Ability.UnitHelper.ChkIsCameraPlayer(unitType))
            {
                self.playerId2CameraPlayerUnitId.Remove(playerId);
            }
            else if (ET.Ability.UnitHelper.ChkIsPlayer(unitType))
            {
                self.playerId2PlayerUnitIdList.Remove(playerId);
            }
        }

        public static long GetCurPlayerUnitId(this GamePlayPlayerListComponent self, long playerId)
        {
            if (self.playerId2PlayerUnitIdList.TryGetValue(playerId, out var ids))
            {
                foreach (var unitId in ids)
                {
                    return unitId;
                }
            }

            return -1;
        }

        public static HashSet<long> GetPlayerUnitIdList(this GamePlayPlayerListComponent self, long playerId)
        {
            if (self.playerId2PlayerUnitIdList.TryGetValue(playerId, out var id))
            {
                return id;
            }

            return null;
        }

        public static long GetCameraPlayerUnitId(this GamePlayPlayerListComponent self, long playerId)
        {
            if (self.playerId2CameraPlayerUnitId.TryGetValue(playerId, out long id))
            {
                return id;
            }

            return -1;
        }

        /// <summary>
        /// 获取未退出战斗的 玩家列表
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static List<long> GetPlayerList(this GamePlayPlayerListComponent self)
        {
            ListComponent<long> playerList = ListComponent<long>.Create();
            foreach (var playerId2IsQuit in self.playerId2IsQuit)
            {
                long playerId = playerId2IsQuit.Key;
                bool isQuit = playerId2IsQuit.Value;
                if (isQuit == false)
                {
                    playerList.Add(playerId);
                }
            }

            return playerList;
        }

        public static bool ChkPlayerIsQuit(this GamePlayPlayerListComponent self, long playerId)
        {
            if (self.playerId2IsQuit.TryGetValue(playerId, out var isQuit))
            {
                return isQuit;
            }

            return true;
        }

        public static GamePlayComponent GetGamePlay(this GamePlayPlayerListComponent self)
        {
            GamePlayComponent gamePlayComponent = self.GetParent<GamePlayComponent>();
            return gamePlayComponent;
        }

        public static GamePlayBattleLevelCfg GetGamePlayBattleConfig(this GamePlayPlayerListComponent self)
        {
            GamePlayComponent gamePlayComponent = self.GetGamePlay();
            return gamePlayComponent.GetGamePlayBattleConfig();
        }

        public static void InitPlayerBirthPosWhenRoom(this GamePlayPlayerListComponent self, RoomComponent roomComponent,
        List<RoomMember> roomMemberList)
        {
            GamePlayBattleLevelCfg gamePlayBattleLevelCfg = self.GetGamePlayBattleConfig();
            List<List<System.Numerics.Vector3>> allPlayerBirthPosList = gamePlayBattleLevelCfg.PlayerBirthPosList;
            if (gamePlayBattleLevelCfg.SceneMap == "ARMap")
            {
                allPlayerBirthPosList = new();
                allPlayerBirthPosList.Add(new List<Vector3>(){Vector3.Zero});
            }
            if (gamePlayBattleLevelCfg.TeamMode is AllPlayersOneGroup allPlayersOneGroup)
            {
                List<System.Numerics.Vector3> playerBirthList = RandomGenerator.RandomArray(allPlayerBirthPosList);
                for (int i = 0; i < roomMemberList.Count; i++)
                {
                    RoomMember roomMember = roomMemberList[i];
                    self._InitPlayerBirthPos(roomMember.Id, RandomGenerator.RandomArray(playerBirthList));
                }
            }
            else if (gamePlayBattleLevelCfg.TeamMode is PlayerAlone playerAlone)
            {
                if (gamePlayBattleLevelCfg.IsGlobalMode)
                {
                    for (int i = 0; i < roomMemberList.Count; i++)
                    {
                        List<System.Numerics.Vector3> playerBirthList = RandomGenerator.RandomArray(allPlayerBirthPosList);
                        RoomMember roomMember = roomMemberList[i];
                        self._InitPlayerBirthPos(roomMember.Id, RandomGenerator.RandomArray(playerBirthList));
                    }
                }
                else
                {
                    if (allPlayerBirthPosList.Count <= roomMemberList.Count - 1)
                    {
                        Log.Error(
                            $"ET.GamePlayPlayerListComponentSystem.InitPlayerBirthPos allPlayerBirthPosList.Count[{allPlayerBirthPosList.Count}] <= roomMemberList.Count[{roomMemberList.Count}] - 1");
                    }

                    for (int i = 0; i < roomMemberList.Count; i++)
                    {
                        RoomMember roomMember = roomMemberList[i];
                        List<System.Numerics.Vector3> playerBirthList = allPlayerBirthPosList[i];
                        self._InitPlayerBirthPos(roomMember.Id, RandomGenerator.RandomArray(playerBirthList));
                    }
                }
            }
            else if (gamePlayBattleLevelCfg.TeamMode is PlayerTeam playerTeam)
            {
                Dictionary<int, int> teamId2BirthIndex = new();
                for (int i = 0; i < roomMemberList.Count; i++)
                {
                    RoomMember roomMember = roomMemberList[i];
                    if (teamId2BirthIndex.ContainsKey((int)roomMember.roomTeamId) == false)
                    {
                        teamId2BirthIndex[(int)roomMember.roomTeamId] = RandomGenerator.RandomNumber(0, allPlayerBirthPosList.Count);
                    }
                }

                for (int i = 0; i < roomMemberList.Count; i++)
                {
                    RoomMember roomMember = roomMemberList[i];
                    List<System.Numerics.Vector3> playerBirthList = allPlayerBirthPosList[teamId2BirthIndex[(int)roomMember.roomTeamId]];
                    self._InitPlayerBirthPos(roomMember.Id, RandomGenerator.RandomArray(playerBirthList));
                }
            }
        }

        public static void _InitPlayerBirthPos(this GamePlayPlayerListComponent self, long playerId, System.Numerics.Vector3 pos, bool forceSet =
            false)
        {
            self._InitPlayerBirthPos(playerId, new float3(pos.X, pos.Y, pos.Z), forceSet);
        }

        public static void _InitPlayerBirthPos(this GamePlayPlayerListComponent self, long playerId, float3 pos, bool forceSet =
            false)
        {
            if (forceSet == false && self.playerId2BirthPos.ContainsKey(playerId))
            {
                return;
            }

            self.playerId2BirthPos[playerId] = pos;
        }

        public static float3 GetPlayerBirthPos(this GamePlayPlayerListComponent self, long playerId)
        {
            return self.playerId2BirthPos[playerId];
        }

        public static void InitPlayerBirthPosWhenGlobal(this GamePlayPlayerListComponent self, long playerId, int playerTeamId)
        {
            GamePlayBattleLevelCfg gamePlayBattleLevelCfg = self.GetGamePlayBattleConfig();
            List<List<System.Numerics.Vector3>> allPlayerBirthPosList = gamePlayBattleLevelCfg.PlayerBirthPosList;
            if (gamePlayBattleLevelCfg.SceneMap == "ARMap")
            {
                allPlayerBirthPosList = new();
                allPlayerBirthPosList.Add(new List<Vector3>(){Vector3.Zero});
            }
            if (gamePlayBattleLevelCfg.TeamMode is AllPlayersOneGroup allPlayersOneGroup)
            {
                List<System.Numerics.Vector3> playerBirthList = RandomGenerator.RandomArray(allPlayerBirthPosList);
                self._InitPlayerBirthPos(playerId, RandomGenerator.RandomArray(playerBirthList));
            }
            else if (gamePlayBattleLevelCfg.TeamMode is PlayerAlone playerAlone)
            {
                List<System.Numerics.Vector3> playerBirthList = RandomGenerator.RandomArray(allPlayerBirthPosList);
                self._InitPlayerBirthPos(playerId, RandomGenerator.RandomArray(playerBirthList));
            }
            else if (gamePlayBattleLevelCfg.TeamMode is PlayerTeam playerTeam)
            {
                List<System.Numerics.Vector3> playerBirthList = allPlayerBirthPosList[playerTeamId];
                self._InitPlayerBirthPos(playerId, RandomGenerator.RandomArray(playerBirthList));
            }
        }

        public static void ResetPlayerBirthPos(this GamePlayPlayerListComponent self, TeamFlagType playerTeamFlagType, float3 pos)
        {
            foreach (long playerId in self.GetPlayerList())
            {
                if (playerTeamFlagType != self.GetGamePlay().GetTeamFlagByPlayerId(playerId))
                {
                    continue;
                }

                self._InitPlayerBirthPos(playerId, pos, true);

                HashSet<long> list = self.GetPlayerUnitIdList(playerId);
                if (list != null)
                {
                    foreach (long unitId in list)
                    {
                        Unit playerUnit = UnitHelper.GetUnit(self.DomainScene(), unitId);
                        if (playerUnit != null)
                        {
                            ET.Ability.UnitHelper.ResetPos(playerUnit, pos, float3.zero);
                        }
                    }
                }
            }
        }

        public static void SetPlayerCoin(this GamePlayPlayerListComponent self, long playerId, CoinTypeInGame coinType, int setValue,
        GetCoinType getCoinType)
        {
            setValue = math.max(0, setValue);
            self.playerId2CoinList.Add(playerId, coinType.ToString(), setValue);

            self.NoticeCoinToClient(playerId, getCoinType);
        }

        public static void ChgPlayerCoin(this GamePlayPlayerListComponent self, long playerId, CoinTypeInGame coinType, float chgValue,
        GetCoinType getCoinType)
        {
            self.playerId2CoinList.TryGetValue(playerId, coinType.ToString(), out float curValue);
            curValue = math.max(0, curValue + chgValue);
            self.playerId2CoinList.Add(playerId, coinType.ToString(), curValue);

            self.NoticeCoinToClient(playerId, getCoinType);
        }

        public static float GetPlayerCoin(this GamePlayPlayerListComponent self, long playerId, CoinTypeInGame coinType)
        {
            self.playerId2CoinList.TryGetValue(playerId, coinType.ToString(), out float curValue);
            return curValue;
        }

        public static void ChgTeamCoin(this GamePlayPlayerListComponent self, TeamFlagType teamFlagType, CoinTypeInGame coinType, int chgValue,
        GetCoinType getCoinType)
        {
            self.team2CoinList.TryGetValue(teamFlagType, coinType.ToString(), out float curValue);
            curValue = math.max(0, curValue + chgValue);
            self.team2CoinList.Add(teamFlagType, coinType.ToString(), curValue);
        }

        public static (bool, MultiDictionary<long, string, int>) GetTeamCoin2Players(this GamePlayPlayerListComponent self)
        {
            if (self.team2CoinList.Count == 0)
            {
                return (false, null);
            }

            MultiDictionary<long, string, int> playerId2Coins = new();
            GamePlayComponent gamePlayComponent = self.GetGamePlay();
            foreach (var teamList in self.team2CoinList)
            {
                int playerNum = 0;
                foreach (long playerId in self.GetPlayerList())
                {
                    TeamFlagType teamFlagType = gamePlayComponent.GetTeamFlagByPlayerId(playerId);
                    if (teamFlagType == teamList.Key)
                    {
                        playerNum++;
                    }
                }

                foreach (var coin in teamList.Value)
                {
                    foreach (long playerId in self.GetPlayerList())
                    {
                        TeamFlagType teamFlagType = gamePlayComponent.GetTeamFlagByPlayerId(playerId);
                        if (teamFlagType == teamList.Key)
                        {
                            playerId2Coins.Add(playerId, coin.Key, (int)(1f * coin.Value / playerNum));
                        }
                    }
                }
            }

            return (true, playerId2Coins);
        }

        public static void RecordPlayerGold(this GamePlayPlayerListComponent self)
        {
            List<long> playerList = self.GetPlayerList();
            GamePlayPlayerListComponent gamePlayPlayerListComponent = self.GetGamePlay().GetComponent<GamePlayPlayerListComponent>();
            for (int i = 0; i < playerList.Count; i++)
            {
                long playerId = playerList[i];
                float curGold = gamePlayPlayerListComponent.GetPlayerCoin(playerId, CoinTypeInGame.Gold);
                if (self.lastPlayerGold.ContainsKey(playerId))
                {
                    self.lastPlayerGold[playerId] = curGold;
                }
                else
                {
                    self.lastPlayerGold.Add(playerId, curGold);
                }
            }
        }

        public static void RecoverPlayerGold(this GamePlayPlayerListComponent self, int recoverAddGold)
        {
            List<long> playerList = self.GetPlayerList();
            for (int i = 0; i < playerList.Count; i++)
            {
                long playerId = playerList[i];
                int recoverGold = recoverAddGold + (int)self.lastPlayerGold[playerId];
                ET.GamePlayHelper.SetPlayerCoin(self.DomainScene(), playerId, CoinTypeInGame.Gold, recoverGold);
            }
        }

        public static void NoticeToClientAll(this GamePlayPlayerListComponent self)
        {
            List<long> playerList = self.GetPlayerList();
            for (int i = 0; i < playerList.Count; i++)
            {
                self.NoticeCoinToClient(playerList[i], GetCoinType.Normal);
            }
        }

        public static void NoticeCoinToClient(this GamePlayPlayerListComponent self, long playerId, GetCoinType getCoinType)
        {
            if (getCoinType == GetCoinType.Normal)
            {
                EventType.WaitNoticeGamePlayPlayerListToClient _WaitNoticeGamePlayPlayerListToClient = new()
                {
                    playerId = playerId, getCoinType = getCoinType, gamePlayPlayerListComponent = self,
                };
                EventSystem.Instance.Publish(self.DomainScene(), _WaitNoticeGamePlayPlayerListToClient);
            }
            else
            {
                EventType.NoticeGamePlayPlayerListToClient _NoticeGamePlayPlayerListToClient = new();
                _NoticeGamePlayPlayerListToClient.playerIds = HashSetComponent<long>.Create();
                _NoticeGamePlayPlayerListToClient.playerIds.Add(playerId);
                _NoticeGamePlayPlayerListToClient.getCoinType = getCoinType;
                _NoticeGamePlayPlayerListToClient.gamePlayPlayerListComponent = self;
                EventSystem.Instance.Publish(self.DomainScene(), _NoticeGamePlayPlayerListToClient);
            }
        }
    }
}