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
        List<RoomMember> roomMemberList, ARMeshType _ARMeshType, string _ARSceneId, string _ARMeshDownLoadUrl, byte[] _ARMeshBytes)
        {
            self.dynamicMapInstanceId = dynamicMapInstanceId;
            self.roomId = roomComponent.Id;
            self.roomTypeInfo = roomComponent.roomTypeInfo;
            self.ownerPlayerId = roomComponent.ownerRoomMemberId;

            self.isAR = roomComponent.IsARRoom();
            self.arMapScale = roomComponent.arMapScale;
            self._ARMeshType = _ARMeshType;
            self._ARSceneId = _ARSceneId;
            self._ARMeshDownLoadUrl = _ARMeshDownLoadUrl;
            self._ARMeshBytes = _ARMeshBytes;

            self.gamePlayStatus = GamePlayStatus.WaitForStart;

            GamePlayPlayerListComponent gamePlayPlayerListComponent = self.AddComponent<GamePlayPlayerListComponent>();
            gamePlayPlayerListComponent.InitWhenRoom(roomComponent, roomMemberList);

            GamePlayFriendTeamFlagCompent gamePlayFriendTeamFlagCompent = self.AddComponent<GamePlayFriendTeamFlagCompent>();
            gamePlayFriendTeamFlagCompent.InitWhenRoom(roomComponent, roomMemberList);

            GamePlayStatisticalDataManagerComponent gamePlayStatisticalDataManagerComponent = self.AddComponent<GamePlayStatisticalDataManagerComponent>();
            gamePlayStatisticalDataManagerComponent.Init();

            GamePlayDropItemComponent gamePlayDropItemComponent = self.AddComponent<GamePlayDropItemComponent>();
            gamePlayDropItemComponent.Init();

            GamePlayNumericComponent gamePlayNumericComponent = self.AddComponent<GamePlayNumericComponent>();

            self.AddComponent<RestoreEnergyComponent>();

            await self.DownloadMapRecast();

            await self.DoGlobalBuffForBattle();

            await self.DoWaitForStart();

            await self.CreateGamePlayMode(self.roomTypeInfo);

            await self.DoReadyForBattle();

            self.AddComponent<PlayerLocationChkComponent>();

            self.NoticeToClientAll();
            await ETTask.CompletedTask;
        }

        public static async ETTask InitWhenGlobal(this GamePlayComponent self, long dynamicMapInstanceId, string gamePlayBattleLevelCfgId)
        {
            self.dynamicMapInstanceId = dynamicMapInstanceId;
            self.roomId = 0;
            self.roomTypeInfo = ET.GamePlayHelper.GetRoomTypeInfo(RoomType.Normal, SubRoomType.NormalSingleMap, -1, -1, gamePlayBattleLevelCfgId);
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

            GamePlayNumericComponent gamePlayNumericComponent = self.AddComponent<GamePlayNumericComponent>();

            self.AddComponent<RestoreEnergyComponent>();

            await self.DownloadMapRecast();

            await self.DoGlobalBuffForBattle();

            await self.DoWaitForStart();

            await self.CreateGamePlayMode(self.roomTypeInfo);

            await self.DoReadyForBattle();

            self.AddComponent<PlayerLocationChkComponent>();

            //self.NoticeToClientAll();
            await ETTask.CompletedTask;
        }

        public static async Task<byte[]> ReadMeshFile(this GamePlayComponent self, string fileName)
        {
            await ETTask.CompletedTask;
            string savePath = EventSystem.Instance.Invoke<ConfigComponent.GetLocalMeshSavePath, string>(new ConfigComponent.GetLocalMeshSavePath());

            string file = System.IO.Path.Combine(savePath, fileName);
            if (System.IO.File.Exists(file) == false)
            {
                return null;
            }
            return System.IO.File.ReadAllBytes(file);
        }

        public static async Task WriteMeshFile(this GamePlayComponent self, string fileName, byte[] data)
        {
            await ETTask.CompletedTask;
            string savePath = EventSystem.Instance.Invoke<ConfigComponent.GetLocalMeshSavePath, string>(new ConfigComponent.GetLocalMeshSavePath());

            string file = System.IO.Path.Combine(savePath, fileName);
            if (System.IO.File.Exists(file))
            {
                return;
            }

            FileHelper.CreateDirectory(file);
            System.IO.File.WriteAllBytes(file, data);
        }

        public static async Task<byte[]> DownloadFileBytesAsync(this GamePlayComponent self, string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                Log.Error("Empty URL, return an empty task.");
                return null;
            }

            byte[] data = null;
            int retryNum = 5;
            for (int i = 0; i < retryNum; i++)
            {
                try
                {
                    using HttpClient httpClient = new();
                    data = await httpClient.GetByteArrayAsync(url);
                    break;
                }
                catch (Exception e)
                {
                    Log.Error(e);
                    await TimerComponent.Instance.WaitAsync(1000);
                    if (self.IsDisposed)
                    {
                        return null;
                    }
                }
            }
            return data;
        }

        public static async Task<string> DownloadFileTextAsync(this GamePlayComponent self, string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                Log.Info("Empty URL, return an empty task.");
                return null;
            }

            string data = null;
            int retryNum = 30;
            for (int i = 0; i < retryNum; i++)
            {
                try
                {
                    using HttpClient httpClient = new();
                    data = await httpClient.GetStringAsync(url);
                    break;
                }
                catch (Exception e)
                {
                    Log.Error(e);
                    await TimerComponent.Instance.WaitAsync(1000);
                    if (self.IsDisposed)
                    {
                        return null;
                    }
                }
            }
            return data;

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
            NavmeshManagerComponent navmeshManagerComponent = self.GetComponent<NavmeshManagerComponent>();
            if (navmeshManagerComponent == null)
            {
                return;
            }

            byte[] bytes = null;
            try
            {
                // Note: ARMeshBytes is zipped.
                //byte[] bytes = self._ARMeshBytes;
                bytes = ZipHelper.Decompress(self._ARMeshBytes);

                Log.Info($"Received obj content from client. Zipped size = {self._ARMeshBytes.Length}, Obj size = {bytes.Length}");
            }
            catch (Exception e)
            {
                Log.Error($"ET.GamePlayComponentSystem.LoadByClientObj Error {e}");
            }
            if (self.IsDisposed)
            {
                return;
            }

            if (bytes == null || bytes.Length == 0)
            {
                navmeshManagerComponent.isLoadMeshFinished = true;
                navmeshManagerComponent.isLoadMeshError = true;
                return;
            }

            string pathfindingMapName = self.GetPathfindingMapName();

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
            NavmeshManagerComponent navmeshManagerComponent = self.GetComponent<NavmeshManagerComponent>();
            if (navmeshManagerComponent == null)
            {
                return;
            }

            string pathfindingMapName = self.GetPathfindingMapName();

            byte[] bytes = EventSystem.Instance.Invoke<NavmeshManagerComponent.RecastFileLoader, byte[]>(new NavmeshManagerComponent.RecastFileLoader() {Name = pathfindingMapName});

            if (self.IsDisposed)
            {
                return;
            }

            if (bytes == null || bytes.Length == 0)
            {
                navmeshManagerComponent.isLoadMeshFinished = true;
                navmeshManagerComponent.isLoadMeshError = true;
                return;
            }

            Task task = new Task(
                    () => {
                        navmeshManagerComponent.InitByFileBytes(bytes, 1);
                    }
                );
            task.Start();

            await ETTask.CompletedTask;
        }

        public static async ETTask LoadByObjURL(this GamePlayComponent self)
        {
            NavmeshManagerComponent navmeshManagerComponent = self.GetComponent<NavmeshManagerComponent>();
            if (navmeshManagerComponent == null)
            {
                return;
            }

            byte[] bytes = await self.DownloadFileBytesAsync(self._ARMeshDownLoadUrl);

            if (self.IsDisposed)
            {
                return;
            }

            if (bytes == null || bytes.Length == 0)
            {
                navmeshManagerComponent.isLoadMeshFinished = true;
                navmeshManagerComponent.isLoadMeshError = true;
                return;
            }

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

            NavmeshManagerComponent navmeshManagerComponent = self.GetComponent<NavmeshManagerComponent>();
            if (navmeshManagerComponent == null)
            {
                return;
            }

            byte[] bytes = await self.ReadMeshFile(self._ARSceneId);
            if (bytes == null)
            {
                // Mesh URL
                string mapUrl = self._ARMeshDownLoadUrl;

                // Draco bytes
                bytes = await self.DownloadFileBytesAsync(mapUrl);
                if (self.IsDisposed)
                {
                    return;
                }

                if (bytes == null || bytes.Length == 0)
                {
                    navmeshManagerComponent.isLoadMeshFinished = true;
                    navmeshManagerComponent.isLoadMeshError = true;
                    return;
                }
            }

            Log.Info($"====================Draco bytes length {bytes.Length}");

            try
            {
                // Decode draco obj to data
                MeshHelper.MeshData meshData = MeshHelper.GetMeshDataFromBytes(bytes);

                await self.WriteMeshFile(self._ARSceneId, bytes);

                Log.Info($"====================MeshData {meshData.vertices.Length}, {meshData.normals.Length}, {meshData.triangles.Length}");

                Task task = new Task(
                    () => {
                        navmeshManagerComponent.InitByMeshData(meshData, self.GetARScale());
                    }
                );
                task.Start();

            }
            catch (Exception e)
            {
                Log.Error($"ET.GamePlayComponentSystem.LoadByMeshData Error {e}");
                navmeshManagerComponent.isLoadMeshFinished = true;
                navmeshManagerComponent.isLoadMeshError = true;
            }

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
                GamePlayPkComponentBase gamePlayPkComponentBase = self.GetComponent<GamePlayPkComponentBase>();
                gamePlayPkComponentBase.NoticeToClient(playerId);
            }

            self.NoticeToClient(playerId);
        }

        public static void Start(this GamePlayComponent self)
        {
            self.gamePlayStatus = GamePlayStatus.Gaming;

            EventSystem.Instance.Publish(self.DomainScene(), new ET.Ability.AbilityTriggerEventType.GamePlay_Status_GameStart());

            self.NoticeToClientAll();
        }

        public static void StartShow(this GamePlayComponent self)
        {
            GamePlayPlayerListComponent gamePlayPlayerListComponent = self.GetComponent<GamePlayPlayerListComponent>();
            if (gamePlayPlayerListComponent == null)
            {
                return;
            }
            gamePlayPlayerListComponent.NoticeToClientAll();
        }

        public static async ETTask GameEnd(this GamePlayComponent self)
        {
            self.gamePlayStatus = GamePlayStatus.GameEnd;

            EventSystem.Instance.Publish(self.DomainScene(), new ET.Ability.AbilityTriggerEventType.GamePlay_Status_GameEnd());

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

        public static async ETTask CreateGamePlayMode(this GamePlayComponent self, RoomTypeInfo roomTypeInfo)
        {
            bool isTowerDefense = false;
            bool isPK = false;
            string gamePlayModeCfgId = "";
            GamePlayBattleLevelCfg gamePlayBattleLevelCfg = self.GetGamePlayBattleConfig();
            if (gamePlayBattleLevelCfg.GamePlayMode is GamePlayTowerDefenseBase gamePlayTowerDefenseBase)
            {
                isTowerDefense = true;
                isPK = false;
                gamePlayModeCfgId = gamePlayTowerDefenseBase.GamePlayModeCfgId;
            }
            else if (gamePlayBattleLevelCfg.GamePlayMode is GamePlayPKBase gamePlayPKBase)
            {
                isTowerDefense = false;
                isPK = true;
                gamePlayModeCfgId = gamePlayPKBase.GamePlayModeCfgId;
            }

            if (isTowerDefense)
            {
                self.gamePlayMode = GamePlayMode.TowerDefense;
                GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.AddComponent<GamePlayTowerDefenseComponent>();
                await gamePlayTowerDefenseComponent.Init(self.ownerPlayerId, gamePlayModeCfgId, roomTypeInfo);
            }

            if (isPK)
            {
                self.gamePlayMode = GamePlayMode.PK;
                GamePlayPkComponentBase gamePlayPkComponentBase = self.AddComponent<GamePlayPkComponentBase>();
                await gamePlayPkComponentBase.Init(self.ownerPlayerId, gamePlayModeCfgId, roomTypeInfo);
            }
        }

        public static async ETTask DoGlobalBuffForBattle(this GamePlayComponent self)
        {
            self.DomainScene().AddComponent<ET.Ability.GlobalBuffManagerComponent>();

            await self.NoticeGameWaitForStart2Server();
        }

        public static async ETTask DoWaitForStart(this GamePlayComponent self)
        {
            EventSystem.Instance.Publish(self.DomainScene(), new ET.Ability.AbilityTriggerEventType.GamePlay_Status_GameWaitForStart());
            await ETTask.CompletedTask;
        }

        public static async ETTask DoReadyForBattle(this GamePlayComponent self)
        {
            if (self.gamePlayMode == GamePlayMode.TowerDefense)
            {
                GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetComponent<GamePlayTowerDefenseComponent>();
                await gamePlayTowerDefenseComponent.DoReadyForBattle();
            }
            else if (self.gamePlayMode == GamePlayMode.PK)
            {
                GamePlayPkComponentBase gamePlayPkComponentBase = self.GetComponent<GamePlayPkComponentBase>();
                await gamePlayPkComponentBase.DoReadyForBattle();
            }
        }

        public static GamePlayModeComponentBase GetGamePlayMode(this GamePlayComponent self)
        {
            if (self.gamePlayMode == GamePlayMode.TowerDefense)
            {
                GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetComponent<GamePlayTowerDefenseComponent>();
                return gamePlayTowerDefenseComponent;
            }
            else if (self.gamePlayMode == GamePlayMode.PK)
            {
                GamePlayPkComponentBase gamePlayPkComponentBase = self.GetComponent<GamePlayPkComponentBase>();
                return gamePlayPkComponentBase;
            }

            return null;
        }

        public static GamePlayBattleLevelCfg GetGamePlayBattleConfig(this GamePlayComponent self)
        {
            GamePlayBattleLevelCfg gamePlayBattleLevelCfg = GamePlayBattleLevelCfgCategory.Instance.Get(self.roomTypeInfo.gamePlayBattleLevelCfgId);
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

        public static Unit GetCurPlayerUnit(this GamePlayComponent self, long playerId)
        {
            GamePlayPlayerListComponent gamePlayPlayerListComponent = self.GetComponent<GamePlayPlayerListComponent>();

            long playerUnitId = gamePlayPlayerListComponent.GetCurPlayerUnitId(playerId);
            if (playerUnitId == -1)
            {
                return null;
            }
            Unit playerUnit = UnitHelper.GetUnit(self.DomainScene(), playerUnitId);
            return playerUnit;
        }

        public static List<Unit> GetPlayerUnitList(this GamePlayComponent self, long playerId)
        {
            GamePlayPlayerListComponent gamePlayPlayerListComponent = self.GetComponent<GamePlayPlayerListComponent>();

            HashSet<long> playerUnitIdList = gamePlayPlayerListComponent.GetPlayerUnitIdList(playerId);
            if (playerUnitIdList == null)
            {
                return null;
            }
            List<Unit> list = ListComponent<Unit>.Create();
            foreach (long unitId in playerUnitIdList)
            {
                Unit playerUnit = UnitHelper.GetUnit(self.DomainScene(), unitId);
                if (UnitHelper.ChkUnitAlive(playerUnit) == false)
                {
                    continue;
                }
                list.Add(playerUnit);
            }
            return list;
        }

        public static Unit GetCameraPlayerUnit(this GamePlayComponent self, long playerId)
        {
            GamePlayPlayerListComponent gamePlayPlayerListComponent = self.GetComponent<GamePlayPlayerListComponent>();

            long cameraPlayerUnitId = gamePlayPlayerListComponent.GetCameraPlayerUnitId(playerId);
            if (cameraPlayerUnitId == -1)
            {
                return null;
            }
            Unit cameraPlayerUnit = UnitHelper.GetUnit(self.DomainScene(), cameraPlayerUnitId);
            return cameraPlayerUnit;
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
                GamePlayPkComponentBase gamePlayPkComponentBase = self.GetComponent<GamePlayPkComponentBase>();
                gamePlayPkComponentBase.NoticeToClient(playerId);
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
                // GamePlayPkComponentBase gamePlayPKComponent = self.GetComponent<GamePlayPkComponentBase>();
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

        public static async ETTask NoticeGameWaitForStart2Server(this GamePlayComponent self)
        {
            EventType.NoticeGameWaitForStart2Server _NoticeGameWaitForStart2Server = new() { gamePlayComponent = self, };
            await EventSystem.Instance.PublishAsync(self.DomainScene(), _NoticeGameWaitForStart2Server);
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

        public static TeamFlagType GetTeamFlagByUnitId(this GamePlayComponent self, long unitId)
        {
            GamePlayFriendTeamFlagCompent gamePlayFriendTeamFlagCompent = self.GetComponent<GamePlayFriendTeamFlagCompent>();
            return gamePlayFriendTeamFlagCompent.GetTeamFlagByUnitId(unitId);
        }

        public static TeamFlagType GetTeamFlagByPlayerId(this GamePlayComponent self, long playerId)
        {
            GamePlayFriendTeamFlagCompent gamePlayFriendTeamFlagCompent = self.GetComponent<GamePlayFriendTeamFlagCompent>();
            return gamePlayFriendTeamFlagCompent?.GetTeamFlagByPlayerId(playerId) ?? TeamFlagType.None;
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
            bool isPlayer = false;
            if (UnitHelper.ChkIsObserver(unit)
                || UnitHelper.ChkIsPlayer(unit)
                || UnitHelper.ChkIsCameraPlayer(unit))
            {
                isPlayer = true;
            }
            PathfindingComponent pathfindingComponent = unit.AddComponent<PathfindingComponent>();
            pathfindingComponent.Init(self.GetComponent<NavmeshManagerComponent>(), isPlayer, unit.model.NavObstacleRadius).Coroutine();
        }

        public static (bool, bool) ChkNavMeshReady(this GamePlayComponent self)
        {
            NavmeshManagerComponent navmeshManagerComponent = self.GetComponent<NavmeshManagerComponent>();

            return (navmeshManagerComponent.isLoadMeshFinished, navmeshManagerComponent.isLoadMeshError);
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
                return self.isTestARObjScale;
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
                GamePlayPkComponentBase gamePlayPkComponentBase = self.GetComponent<GamePlayPkComponentBase>();
                gamePlayPkComponentBase.DealUnitBeKill(attackerUnit, beKillUnit);
            }
        }

        public static float GetPlayerCoin(this GamePlayComponent self, long playerId, CoinTypeInGame coinType)
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