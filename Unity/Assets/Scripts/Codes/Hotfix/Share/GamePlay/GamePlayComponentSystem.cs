using ET.Ability;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    [FriendOf(typeof (GamePlayComponent))]
    public static class GamePlayComponentSystem
    {
        [ObjectSystem]
        public class GamePlayComponentAwakeSystem: AwakeSystem<GamePlayComponent>
        {
            protected override void Awake(GamePlayComponent self)
            {
                self.AddComponent<NavmeshManagerComponent>();
            }
        }

        [ObjectSystem]
        public class GamePlayComponentDestroySystem: DestroySystem<GamePlayComponent>
        {
            protected override void Destroy(GamePlayComponent self)
            {
            }
        }

        public static async ETTask InitWhenRoom(this GamePlayComponent self, long dynamicMapInstanceId, string gamePlayBattleLevelCfgId,
        RoomComponent roomComponent,
        List<RoomMember> roomMemberList, ARMeshType _ARMeshType, string _ARMeshDownLoadUrl, byte[] _ARMeshBytes)
        {
            self.dynamicMapInstanceId = dynamicMapInstanceId;
            self.gamePlayBattleLevelCfgId = gamePlayBattleLevelCfgId;
            self.roomId = roomComponent.Id;
            self.ownerPlayerId = roomComponent.ownerRoomMemberId;

            self.isAR = roomComponent.IsARRoom();
            self.arMapScale = roomComponent.arMapScale;
            self._ARMeshType = _ARMeshType;
            self._ARMeshDownLoadUrl = _ARMeshDownLoadUrl;
            self._ARMeshBytes = _ARMeshBytes;

            GamePlayPlayerListComponent gamePlayPlayerListComponent = self.AddComponent<GamePlayPlayerListComponent>();
            gamePlayPlayerListComponent.InitWhenRoom(roomComponent, roomMemberList);

            GamePlayFriendTeamFlagCompent gamePlayFriendTeamFlagCompent = self.AddComponent<GamePlayFriendTeamFlagCompent>();
            gamePlayFriendTeamFlagCompent.InitWhenRoom(roomComponent, roomMemberList);

            GamePlayStatisticalDataManagerComponent gamePlayStatisticalDataManagerComponent = self.AddComponent<GamePlayStatisticalDataManagerComponent>();
            gamePlayStatisticalDataManagerComponent.Init();

            GamePlayDropItemComponent gamePlayDropItemComponent = self.AddComponent<GamePlayDropItemComponent>();
            gamePlayDropItemComponent.Init();

            await self.DownloadMapRecast();

            await self.CreateGamePlayMode();

            //self.gamePlayStatus = GamePlayStatus.WaitForStart;

            self.NoticeToClientAll();
            await ETTask.CompletedTask;
        }

        public static async ETTask InitWhenGlobal(this GamePlayComponent self, long dynamicMapInstanceId, string gamePlayBattleLevelCfgId)
        {
            self.dynamicMapInstanceId = dynamicMapInstanceId;
            self.gamePlayBattleLevelCfgId = gamePlayBattleLevelCfgId;
            self.roomId = 0;
            self.ownerPlayerId = -1;
            self.isAR = false;

            self.gamePlayStatus = GamePlayStatus.WaitForStart;

            GamePlayPlayerListComponent gamePlayPlayerListComponent = self.AddComponent<GamePlayPlayerListComponent>();
            gamePlayPlayerListComponent.InitWhenGlobal();

            GamePlayFriendTeamFlagCompent gamePlayFriendTeamFlagCompent = self.AddComponent<GamePlayFriendTeamFlagCompent>();
            gamePlayFriendTeamFlagCompent.InitWhenGlobal();

            GamePlayStatisticalDataManagerComponent gamePlayStatisticalDataManagerComponent = self.AddComponent<GamePlayStatisticalDataManagerComponent>();
            gamePlayStatisticalDataManagerComponent.Init();

            GamePlayDropItemComponent gamePlayDropItemComponent = self.AddComponent<GamePlayDropItemComponent>();
            gamePlayDropItemComponent.Init();

            await self.DownloadMapRecast();

            await self.CreateGamePlayMode();

            //self.NoticeToClientAll();
            await ETTask.CompletedTask;
        }

        public static Task<byte[]> DownloadFileBytesAsync(this GamePlayComponent self, string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                Log.Info("Empty URL, return an empty task.");
                return Task.FromResult<byte[]>(null);
            }

            HttpClient httpClient = new();
            return httpClient.GetByteArrayAsync(url);
        }

        public static Task<string> DownloadFileTextAsync(this GamePlayComponent self, string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                Log.Info("Empty URL, return an empty task.");
                return Task.FromResult<string>(null);
            }

            HttpClient httpClient = new();
            return httpClient.GetStringAsync(url);
        }

        public static async ETTask DownloadMapRecast(this GamePlayComponent self)
        {
            string pathfindingMapName = self.GetPathfindingMapName();

            NavmeshManagerComponent navmeshManagerComponent = self.GetComponent<NavmeshManagerComponent>();

            if (self._ARMeshType == ARMeshType.FromClientObj)
            {
                self.LoadByClientObj().Coroutine();
            }
            else
            {

                if (self.isTestARMesh)
                {
                    self._ARMeshDownLoadUrl = self.isTestARMeshUrl;
                    self.LoadByMeshData().Coroutine();
                }
                else if (self.isTestARObj)
                {
                    self._ARMeshDownLoadUrl = self.isTestARObjUrl;
                    self.LoadByObjURL().Coroutine();
                    return;
                }
                else if (self.ChkIsAR() == false)
                {
                    self.LoadByFile().Coroutine();
                    return;
                }
                else
                {
                    self.LoadByMeshData().Coroutine();
                }

            }

            await ETTask.CompletedTask;
        }

        public static async ETTask LoadByClientObj(this GamePlayComponent self)
        {

            // Note: ARMeshBytes is zipped.
            //byte[] bytes = self._ARMeshBytes;
            byte[] bytes = ZipHelper.Decompress(self._ARMeshBytes);

            Log.Info($"Received obj content from client. Zipped size = {self._ARMeshBytes.Length}, Obj size = {bytes.Length}");

            string pathfindingMapName = self.GetPathfindingMapName();
            NavmeshManagerComponent navmeshManagerComponent = self.GetComponent<NavmeshManagerComponent>();
            Task task = new Task(
                () => {
                    navmeshManagerComponent.InitByFileBytes(bytes, self.GetARScale());
                }
            );
            task.Start();

            await ETTask.CompletedTask;
        }

        public static async ETTask LoadByFile(this GamePlayComponent self)
        {
            string pathfindingMapName = self.GetPathfindingMapName();
            NavmeshManagerComponent navmeshManagerComponent = self.GetComponent<NavmeshManagerComponent>();
            Task task = new Task(
                    () => {
                        navmeshManagerComponent.InitByFile(pathfindingMapName, 1);
                    }
                );
            task.Start();

            // var tmp = Task.Run(() =>
            // {
            //     navmeshManagerComponent.InitByFile(pathfindingMapName);
            // });
            await ETTask.CompletedTask;
        }

        public static async ETTask LoadByObjURL(this GamePlayComponent self)
        {
            byte[] bytes = await self.DownloadFileBytesAsync(self._ARMeshDownLoadUrl);
            string pathfindingMapName = self.GetPathfindingMapName();
            NavmeshManagerComponent navmeshManagerComponent = self.GetComponent<NavmeshManagerComponent>();
            Task task = new Task(
                    () => {
                        navmeshManagerComponent.InitByFileBytes(bytes, self.GetARScale());
                    }
                );
            task.Start();

            await ETTask.CompletedTask;
        }

        public static async ETTask LoadByMeshData(this GamePlayComponent self)
        {
            if (string.IsNullOrEmpty(self._ARMeshDownLoadUrl))
            {
                return;
            }

            string pathfindingMapName = self.GetPathfindingMapName();

            NavmeshManagerComponent navmeshManagerComponent = self.GetComponent<NavmeshManagerComponent>();

            // Mesh URL
            string mapUrl = self._ARMeshDownLoadUrl;

            // Draco bytes
            byte[] bytes = await self.DownloadFileBytesAsync(mapUrl);

            Log.Info($"====================Draco bytes length {bytes.Length}");

            // Decode draco obj to data
            MeshHelper.MeshData meshData = MeshHelper.GetMeshDataFromBytes(bytes);

            Log.Info($"====================MeshData {meshData.vertices.Length}, {meshData.normals.Length}, {meshData.triangles.Length}");

            Task task = new Task(
                () => {
                    navmeshManagerComponent.InitByMeshData(meshData, self.GetARScale());
                }
            );
            task.Start();

            // var tmp2 = Task.Run(() =>
            // {
            //     navmeshManagerComponent.InitByMeshData(meshData, self.GetARScale());
            // });

            await ETTask.CompletedTask;
        }

        public static void AddPlayerWhenGlobal(this GamePlayComponent self, long playerId, int playerTeamId)
        {
            GamePlayPlayerListComponent gamePlayPlayerListComponent = self.GetComponent<GamePlayPlayerListComponent>();
            gamePlayPlayerListComponent.AddPlayerWhenGlobal(playerId, playerTeamId);

            GamePlayFriendTeamFlagCompent gamePlayFriendTeamFlagCompent = self.GetComponent<GamePlayFriendTeamFlagCompent>();
            gamePlayFriendTeamFlagCompent.AddPlayerWhenGlobal(playerId, playerTeamId);

            if (self.gamePlayMode == GamePlayMode.TowerDefense)
            {
                GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetComponent<GamePlayTowerDefenseComponent>();
                gamePlayTowerDefenseComponent.NoticeToClient(playerId);
            }
            else if (self.gamePlayMode == GamePlayMode.PK)
            {
                GamePlayPKComponent gamePlayPKComponent = self.GetComponent<GamePlayPKComponent>();
                gamePlayPKComponent.NoticeToClient(playerId);
            }

            self.NoticeToClient(playerId);
        }

        public static void Start(this GamePlayComponent self)
        {
            self.gamePlayStatus = GamePlayStatus.Gaming;
            self.NoticeToClientAll();
        }

        public static async ETTask GameEnd(this GamePlayComponent self)
        {
            self.gamePlayStatus = GamePlayStatus.GameEnd;
            self.StopAllAI();

            await self.NoticeGameEnd2Server();

            self.NoticeToClientAll();
            self.NoticeGamePlayStatisticalData2Client();
            self.NoticeGameEndToRoom(false);
            self.AllPlayerQuit().Coroutine();
        }

        public static void StopAllAI(this GamePlayComponent self)
        {
            GamePlayFriendTeamFlagCompent gamePlayFriendTeamFlagCompent = self.GetComponent<GamePlayFriendTeamFlagCompent>();
            gamePlayFriendTeamFlagCompent.StopAllAI();
        }

        public static void PauseAllAI(this GamePlayComponent self)
        {
            GamePlayFriendTeamFlagCompent gamePlayFriendTeamFlagCompent = self.GetComponent<GamePlayFriendTeamFlagCompent>();
            gamePlayFriendTeamFlagCompent.PauseAllAI();
        }

        public static void RecoveryAllAI(this GamePlayComponent self)
        {
            GamePlayFriendTeamFlagCompent gamePlayFriendTeamFlagCompent = self.GetComponent<GamePlayFriendTeamFlagCompent>();
            gamePlayFriendTeamFlagCompent.RecoveryAllAI();
        }

        public static async ETTask CreateGamePlayMode(this GamePlayComponent self)
        {
            GamePlayBattleLevelCfg gamePlayBattleLevelCfg = self.GetGamePlayBattleConfig();
            if (gamePlayBattleLevelCfg.GamePlayMode is GamePlayTowerDefenseNormal gamePlayTowerDefenseNormal)
            {
                self.gamePlayMode = GamePlayMode.TowerDefense;
                GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.AddComponent<GamePlayTowerDefenseComponent>();
                await gamePlayTowerDefenseComponent.Init(self.ownerPlayerId, gamePlayTowerDefenseNormal.GamePlayModeCfgId, GamePlayTowerDefenseMode.TowerDefense_Normal);
            }
            else if (gamePlayBattleLevelCfg.GamePlayMode is GamePlayTowerDefenseTutorialFirst gamePlayTowerDefenseTutorialFirst)
            {
                self.gamePlayMode = GamePlayMode.TowerDefense;
                GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.AddComponent<GamePlayTowerDefenseComponent>();
                await gamePlayTowerDefenseComponent.Init(self.ownerPlayerId, gamePlayTowerDefenseTutorialFirst.GamePlayModeCfgId, GamePlayTowerDefenseMode.TowerDefense_TutorialFirst);
            }
            else if (gamePlayBattleLevelCfg.GamePlayMode is GamePlayTowerDefensePVE gamePlayTowerDefensePVE)
            {
                self.gamePlayMode = GamePlayMode.TowerDefense;
                GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.AddComponent<GamePlayTowerDefenseComponent>();
                await gamePlayTowerDefenseComponent.Init(self.ownerPlayerId, gamePlayTowerDefensePVE.GamePlayModeCfgId, GamePlayTowerDefenseMode.TowerDefense_PVE);
            }
            else if (gamePlayBattleLevelCfg.GamePlayMode is GamePlayTowerDefensePVP gamePlayTowerDefensePVP)
            {
                self.gamePlayMode = GamePlayMode.TowerDefense;
                GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.AddComponent<GamePlayTowerDefenseComponent>();
                await gamePlayTowerDefenseComponent.Init(self.ownerPlayerId, gamePlayTowerDefensePVP.GamePlayModeCfgId, GamePlayTowerDefenseMode.TowerDefense_PVP);
            }
            else if (gamePlayBattleLevelCfg.GamePlayMode is GamePlayTowerDefenseEndlessChallenge gamePlayTowerDefenseEndlessChallenge)
            {
                self.gamePlayMode = GamePlayMode.TowerDefense;
                GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.AddComponent<GamePlayTowerDefenseComponent>();
                await gamePlayTowerDefenseComponent.Init(self.ownerPlayerId, gamePlayTowerDefenseEndlessChallenge.GamePlayModeCfgId, GamePlayTowerDefenseMode.TowerDefense_EndlessChallenge);
            }
            else if (gamePlayBattleLevelCfg.GamePlayMode is GamePlayPKNormal gamePlayPkNormal)
            {
                self.gamePlayMode = GamePlayMode.PK;
                GamePlayPKComponent gamePlayPKComponent = self.AddComponent<GamePlayPKComponent>();
                await gamePlayPKComponent.Init(self.ownerPlayerId, gamePlayPkNormal.GamePlayModeCfgId);
            }
        }

        public static GamePlayModeComponent GetGamePlayMode(this GamePlayComponent self)
        {
            if (self.gamePlayMode == GamePlayMode.TowerDefense)
            {
                GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetComponent<GamePlayTowerDefenseComponent>();
                return gamePlayTowerDefenseComponent;
            }
            else if (self.gamePlayMode == GamePlayMode.PK)
            {
                GamePlayPKComponent gamePlayPKComponent = self.GetComponent<GamePlayPKComponent>();
                return gamePlayPKComponent;
            }

            return null;
        }

        public static GamePlayBattleLevelCfg GetGamePlayBattleConfig(this GamePlayComponent self)
        {
            GamePlayBattleLevelCfg gamePlayBattleLevelCfg = GamePlayBattleLevelCfgCategory.Instance.Get(self.gamePlayBattleLevelCfgId);
            return gamePlayBattleLevelCfg;
        }

        public static List<long> GetPlayerList(this GamePlayComponent self)
        {
            GamePlayPlayerListComponent gamePlayPlayerListComponent = self.GetComponent<GamePlayPlayerListComponent>();
            if (gamePlayPlayerListComponent == null)
            {
                return null;
            }
            return gamePlayPlayerListComponent.GetPlayerList();
        }

        public static bool ChkPlayerIsQuit(this GamePlayComponent self, long playerId)
        {
            GamePlayPlayerListComponent gamePlayPlayerListComponent = self.GetComponent<GamePlayPlayerListComponent>();
            if (gamePlayPlayerListComponent == null)
            {
                return true;
            }
            return gamePlayPlayerListComponent.ChkPlayerIsQuit(playerId);
        }

        /// <summary>
        /// 通过这个判断 unitId 是否归属 player, 返回-1则不是玩家的
        /// </summary>
        /// <param name="self"></param>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public static long GetPlayerIdByUnitId(this GamePlayComponent self, long unitId)
        {
            GamePlayPlayerListComponent gamePlayPlayerListComponent = self.GetComponent<GamePlayPlayerListComponent>();
            return gamePlayPlayerListComponent.GetPlayerIdByUnitId(unitId);
        }

        public static Unit GetPlayerUnit(this GamePlayComponent self, long playerId)
        {
            GamePlayPlayerListComponent gamePlayPlayerListComponent = self.GetComponent<GamePlayPlayerListComponent>();

            long playerUnitId = gamePlayPlayerListComponent.GetPlayerUnitId(playerId);
            if (playerUnitId == -1)
            {
                return null;
            }
            Unit playerUnit = UnitHelper.GetUnit(self.DomainScene(), playerUnitId);
            return playerUnit;
        }

        public static void PlayerQuitBattle(this GamePlayComponent self, long playerId, bool isNeedRemoveAllPlayerUnits)
        {
            GamePlayPlayerListComponent gamePlayPlayerListComponent = self.GetComponent<GamePlayPlayerListComponent>();
            gamePlayPlayerListComponent?.PlayerQuitBattle(playerId, isNeedRemoveAllPlayerUnits);

            EventType.NoticeGameBattleRemovePlayer _NoticeGameBattleRemovePlayer = new()
            {
                playerId = playerId,
            };
            EventSystem.Instance.Publish(self.DomainScene(), _NoticeGameBattleRemovePlayer);
        }

        /// <summary>
        /// 处理阵营关系
        /// </summary>
        /// <param name="self"></param>
        /// <param name="teamFlagTypes"></param>
        /// <param name="isWithPlayers"></param>
        /// <param name="reset"></param>
        public static void DealFriendTeamFlag(this GamePlayComponent self, List<TeamFlagType> teamFlagTypes, bool isWithPlayers, bool reset)
        {
            GamePlayFriendTeamFlagCompent gamePlayFriendTeamFlagCompent = self.GetComponent<GamePlayFriendTeamFlagCompent>();
            gamePlayFriendTeamFlagCompent.DealFriendTeamFlag(teamFlagTypes, isWithPlayers, reset);
        }

        public static float3 GetPlayerColor(this GamePlayComponent self, long playerId)
        {
            GamePlayFriendTeamFlagCompent gamePlayFriendTeamFlagCompent = self.GetComponent<GamePlayFriendTeamFlagCompent>();
            return gamePlayFriendTeamFlagCompent.GetPlayerColor(playerId);
        }

        public static void NoticeToClientAll(this GamePlayComponent self)
        {
            List<long> playerList = self.GetPlayerList();
            for (int i = 0; i < playerList.Count; i++)
            {
                self.NoticeToClient(playerList[i]);
            }
        }

        public static void ReNoticeToClient(this GamePlayComponent self, long playerId)
        {
            self.NoticeToClient(playerId);

            if (self.gamePlayMode == GamePlayMode.TowerDefense)
            {
                GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetComponent<GamePlayTowerDefenseComponent>();
                gamePlayTowerDefenseComponent.NoticeToClient(playerId);
            }
            else if (self.gamePlayMode == GamePlayMode.PK)
            {
                GamePlayPKComponent gamePlayPKComponent = self.GetComponent<GamePlayPKComponent>();
                gamePlayPKComponent.NoticeToClient(playerId);
            }
        }

        public static void NoticeToClient(this GamePlayComponent self, long playerId)
        {
            EventType.WaitNoticeGamePlayToClient _WaitNoticeGamePlayChgToClient = new() { playerId = playerId, gamePlayComponent = self, };
            EventSystem.Instance.Publish(self.DomainScene(), _WaitNoticeGamePlayChgToClient);
        }

        public static void NoticeGameEndToRoom(this GamePlayComponent self, bool isReady, long playerId = -1)
        {
            EventType.NoticeGameEndToRoom _NoticeGameEndToRoom = new()
            {
                roomId = self.roomId,
                playerId = playerId,
                isReady = isReady,
            };
            if (self.gamePlayMode == GamePlayMode.TowerDefense)
            {
                GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetComponent<GamePlayTowerDefenseComponent>();
                _NoticeGameEndToRoom.playerWinResult = gamePlayTowerDefenseComponent.GetPlayerWinResult();
            }
            else if (self.gamePlayMode == GamePlayMode.PK)
            {
                // GamePlayPKComponent gamePlayPKComponent = self.GetComponent<GamePlayPKComponent>();
                // gamePlayPKComponent.NoticeToClient(playerId);
            }
            EventSystem.Instance.Publish(self.DomainScene(), _NoticeGameEndToRoom);

        }

        public static async ETTask AllPlayerQuit(this GamePlayComponent self)
        {
            await TimerComponent.Instance.WaitAsync(1000);
            if (self.IsDisposed)
            {
                return;
            }
            foreach (long playerId in self.GetPlayerList())
            {
                self.PlayerQuitBattle(playerId, true);
            }

            await ETTask.CompletedTask;
        }

        public static void NoticeGamePlayStatisticalData2Client(this GamePlayComponent self)
        {
            self.GetComponent<GamePlayStatisticalDataManagerComponent>().NoticeToClientAll();
        }

        public static void NoticeGameBegin2Server(this GamePlayComponent self)
        {
            EventType.NoticeGameBegin2Server _NoticeGameBegin2Server = new() { gamePlayComponent = self, };
            EventSystem.Instance.Publish(self.DomainScene(), _NoticeGameBegin2Server);
        }

        public static async ETTask NoticeGameEnd2Server(this GamePlayComponent self)
        {
            EventType.NoticeGameEnd2Server _NoticeGameEnd2Server = new() { gamePlayComponent = self, };
            await EventSystem.Instance.PublishAsync(self.DomainScene(), _NoticeGameEnd2Server);
        }

        /// <summary>
        /// 指定玩家的阵营,例如塔
        /// </summary>
        /// <param name="self"></param>
        /// <param name="playerId"></param>
        /// <param name="unit"></param>
        public static void AddPlayerUnitTeamFlag(this GamePlayComponent self, long playerId, Unit unit)
        {
            GamePlayFriendTeamFlagCompent gamePlayFriendTeamFlagCompent = self.GetComponent<GamePlayFriendTeamFlagCompent>();
            gamePlayFriendTeamFlagCompent.AddPlayerUnitTeamFlag(playerId, unit);
        }

        /// <summary>
        /// 直接指定阵营信息,例如 大本营，怪物
        /// </summary>
        /// <param name="self"></param>
        /// <param name="unit"></param>
        /// <param name="teamFlag"></param>
        public static void AddUnitTeamFlag(this GamePlayComponent self, Unit unit, TeamFlagType teamFlag)
        {
            GamePlayFriendTeamFlagCompent gamePlayFriendTeamFlagCompent = self.GetComponent<GamePlayFriendTeamFlagCompent>();
            gamePlayFriendTeamFlagCompent.AddUnitTeamFlag(unit, teamFlag);
        }

        /// <summary>
        /// 有召唤者的设置阵营信息，例如子弹
        /// </summary>
        /// <param name="self"></param>
        /// <param name="unitParent"></param>
        /// <param name="unit"></param>
        public static void AddUnitTeamFlagByParent(this GamePlayComponent self, Unit unitParent, Unit unit)
        {
            GamePlayFriendTeamFlagCompent gamePlayFriendTeamFlagCompent = self.GetComponent<GamePlayFriendTeamFlagCompent>();
            gamePlayFriendTeamFlagCompent.AddUnitTeamFlagByParent(unitParent, unit);
        }

        public static Dictionary<long, TeamFlagType> GetAllPlayerTeamFlag(this GamePlayComponent self)
        {
            GamePlayFriendTeamFlagCompent gamePlayFriendTeamFlagCompent = self.GetComponent<GamePlayFriendTeamFlagCompent>();
            return gamePlayFriendTeamFlagCompent.GetAllPlayerTeamFlag();
        }

        public static TeamFlagType GetTeamFlagByUnit(this GamePlayComponent self, Unit unit)
        {
            GamePlayFriendTeamFlagCompent gamePlayFriendTeamFlagCompent = self.GetComponent<GamePlayFriendTeamFlagCompent>();
            return gamePlayFriendTeamFlagCompent.GetTeamFlagByUnit(unit);
        }

        public static TeamFlagType GetTeamFlagByPlayerId(this GamePlayComponent self, long playerId)
        {
            GamePlayFriendTeamFlagCompent gamePlayFriendTeamFlagCompent = self.GetComponent<GamePlayFriendTeamFlagCompent>();
            return gamePlayFriendTeamFlagCompent.GetTeamFlagByPlayerId(playerId);
        }

        public static bool ChkIsFriend(this GamePlayComponent self, TeamFlagType curTeamFlagType, TeamFlagType targetTeamFlagType)
        {
            GamePlayFriendTeamFlagCompent gamePlayFriendTeamFlagCompent = self.GetComponent<GamePlayFriendTeamFlagCompent>();
            return gamePlayFriendTeamFlagCompent._ChkIsFriend(curTeamFlagType, targetTeamFlagType);
        }

        public static string GetPathfindingMapName(this GamePlayComponent self)
        {
            if (self.ChkIsAR())
            {
                return $"ARMap{self.roomId}";
            }

            string sceneMap = self.GetGamePlayBattleConfig().SceneMap;
            return sceneMap;
        }

        /// <summary>
        /// 创建寻路agent
        /// </summary>
        /// <param name="self"></param>
        /// <param name="unit"></param>
        public static void AddUnitPathfinding(this GamePlayComponent self, Unit unit)
        {
            PathfindingComponent pathfindingComponent = unit.AddComponent<PathfindingComponent>();
            pathfindingComponent.Init(self.GetComponent<NavmeshManagerComponent>()).Coroutine();
        }

        public static bool ChkNavMeshReady(this GamePlayComponent self)
        {
            NavmeshManagerComponent navmeshManagerComponent = self.GetComponent<NavmeshManagerComponent>();
            if (navmeshManagerComponent.GetNavMesh() == null)
            {
                return false;
            }
            return true;
        }

        public static bool ChkIsAR(this GamePlayComponent self)
        {
            string sceneMap = self.GetGamePlayBattleConfig().SceneMap;
            if (sceneMap == "ARMap")
            {
                return true;
            }

            return false;
        }

        public static float GetARScale(this GamePlayComponent self)
        {
            if (self.isTestARMesh || self.isTestARObj)
            {
                return 30f;
            }

            if (self.ChkIsAR())
            {
                return self.arMapScale;
            }

            return 1;
        }

        public static float3 GetPlayerBirthPos(this GamePlayComponent self, long playerId)
        {
            if (self.IsAR())
            {
                return float3.zero;
            }
            else
            {
                GamePlayPlayerListComponent gamePlayPlayerListComponent = self.GetComponent<GamePlayPlayerListComponent>();
                return gamePlayPlayerListComponent.GetPlayerBirthPos(playerId);
            }
        }

        public static bool IsAR(this GamePlayComponent self)
        {
            return self.isAR;
        }

        public static bool ChkIsGameEnd(this GamePlayComponent self)
        {
            return self.gamePlayStatus == GamePlayStatus.GameEnd;
        }

        public static void DealUnitBeKill(this GamePlayComponent self, Unit attackerUnit, Unit beKillUnit)
        {
            GamePlayStatisticalDataManagerComponent gamePlayStatisticalDataManagerComponent = self.GetComponent<GamePlayStatisticalDataManagerComponent>();
            gamePlayStatisticalDataManagerComponent.AddKillInfo(attackerUnit, beKillUnit);

            if (self.gamePlayMode == GamePlayMode.TowerDefense)
            {
                GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetComponent<GamePlayTowerDefenseComponent>();
                gamePlayTowerDefenseComponent.DealUnitBeKill(attackerUnit, beKillUnit);
            }
            else if (self.gamePlayMode == GamePlayMode.PK)
            {
                GamePlayPKComponent gamePlayPKComponent = self.GetComponent<GamePlayPKComponent>();
                gamePlayPKComponent.DealUnitBeKill(attackerUnit, beKillUnit);
            }
        }

        public static float GetPlayerCoin(this GamePlayComponent self, long playerId, CoinType coinType)
        {
            GamePlayPlayerListComponent gamePlayPlayerListComponent = self.GetComponent<GamePlayPlayerListComponent>();
            return gamePlayPlayerListComponent.GetPlayerCoin(playerId, coinType);
        }

        public static void ResetPlayerBirthPos(this GamePlayComponent self, TeamFlagType playerTeamFlagType, float3 pos)
        {
            GamePlayPlayerListComponent gamePlayPlayerListComponent = self.GetComponent<GamePlayPlayerListComponent>();
            gamePlayPlayerListComponent.ResetPlayerBirthPos(playerTeamFlagType, pos);
        }

    }
}