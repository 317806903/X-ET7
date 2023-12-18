using System;
using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof (GlobalBuffComponent))]
    [FriendOf(typeof (GlobalBuffUnitObj))]
    public static class GlobalBuffComponentSystem
    {
        [ObjectSystem]
        public class GlobalBuffComponentAwakeSystem: AwakeSystem<GlobalBuffComponent>
        {
            protected override void Awake(GlobalBuffComponent self)
            {
                self.removeList = new();

                self.childId2GlobalBuffType = new();
                self.monitorTriggerList = new();
            }
        }

        [ObjectSystem]
        public class GlobalBuffComponentDestroySystem: DestroySystem<GlobalBuffComponent>
        {
            protected override void Destroy(GlobalBuffComponent self)
            {
                self.removeList.Clear();
                self.childId2GlobalBuffType.Clear();
                self.monitorTriggerList.Clear();
            }
        }

        [ObjectSystem]
        public class GlobalBuffComponentFixedUpdateSystem: FixedUpdateSystem<GlobalBuffComponent>
        {
            protected override void FixedUpdate(GlobalBuffComponent self)
            {
                if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static void FixedUpdate(this GlobalBuffComponent self, float fixedDeltaTime)
        {
            if (self.Children.Count <= 0)
            {
                return;
            }

            self.removeList.Clear();

            foreach (var obj in self.Children.Values)
            {
                long globalBuffObjId = obj.Id;
                GlobalBuffType globalBuffType = self.childId2GlobalBuffType[globalBuffObjId];
                if (globalBuffType == GlobalBuffType.Unit)
                {
                    GlobalBuffUnitObj globalBuffUnitObj = obj as GlobalBuffUnitObj;
                    globalBuffUnitObj.FixedUpdate(fixedDeltaTime);

                    if (globalBuffUnitObj.ChkNeedRemove())
                    {
                        self.removeList.Add(globalBuffUnitObj.Id);
                    }
                }
                else if (globalBuffType == GlobalBuffType.Player)
                {
                    GlobalBuffPlayerObj globalBuffPlayerObj = obj as GlobalBuffPlayerObj;
                    globalBuffPlayerObj.FixedUpdate(fixedDeltaTime);

                    if (globalBuffPlayerObj.ChkNeedRemove())
                    {
                        self.removeList.Add(globalBuffPlayerObj.Id);
                    }
                }
                else if (globalBuffType == GlobalBuffType.Game)
                {
                    GlobalBuffGameObj globalBuffGameObj = obj as GlobalBuffGameObj;
                    globalBuffGameObj.FixedUpdate(fixedDeltaTime);

                    if (globalBuffGameObj.ChkNeedRemove())
                    {
                        self.removeList.Add(globalBuffGameObj.Id);
                    }
                }
            }

            int count = self.removeList.Count;
            for (int i = 0; i < count; i++)
            {
                self.Remove(self.removeList[i]);
            }

            self.removeList.Clear();
        }

        public static void Remove(this GlobalBuffComponent self, long globalBuffObjId)
        {
            GlobalBuffType globalBuffType = self.childId2GlobalBuffType[globalBuffObjId];
            if (globalBuffType == GlobalBuffType.Unit)
            {
                GlobalBuffUnitObj globalBuffUnitObj = self.GetChild<GlobalBuffUnitObj>(globalBuffObjId);

                self.DealWhenRemoveBuff(globalBuffUnitObj);
            }
            else if (globalBuffType == GlobalBuffType.Player)
            {
                GlobalBuffPlayerObj globalBuffPlayerObj = self.GetChild<GlobalBuffPlayerObj>(globalBuffObjId);

                self.DealWhenRemoveBuff(globalBuffPlayerObj);
            }
            else if (globalBuffType == GlobalBuffType.Game)
            {
                GlobalBuffGameObj globalBuffGameObj = self.GetChild<GlobalBuffGameObj>(globalBuffObjId);

                self.DealWhenRemoveBuff(globalBuffGameObj);
            }

            self.RemoveChild(globalBuffObjId);
            self.childId2GlobalBuffType.Remove(globalBuffObjId);
        }

        //=================================================
        public static async ETTask<GlobalBuffUnitObj> AddGlobalBuff_Unit(this GlobalBuffComponent self, Unit unit, UnitGlobalBuffCfg unitGlobalBuffCfg, ActionContext actionContext)
        {
            await ETTask.CompletedTask;
            GlobalBuffUnitObj globalBuffUnitObj = self.AddChild<GlobalBuffUnitObj>();
            globalBuffUnitObj.Init(unit, unitGlobalBuffCfg);
            globalBuffUnitObj.InitActionContext(ref actionContext);

            self.AddMonitorTriggerList(globalBuffUnitObj);

            self.childId2GlobalBuffType.Add(globalBuffUnitObj.Id, GlobalBuffType.Unit);

            return globalBuffUnitObj;
        }

        public static void DealWhenRemoveBuff(this GlobalBuffComponent self, GlobalBuffUnitObj globalBuffUnitObj)
        {
            self.RemoveMonitorTriggerList(globalBuffUnitObj);
        }

        public static void AddMonitorTriggerList(this GlobalBuffComponent self, GlobalBuffUnitObj globalBuffUnitObj)
        {
            foreach (var monitorTrigger in globalBuffUnitObj.monitorTriggerList)
            {
                self.monitorTriggerList.Add(monitorTrigger.Key, globalBuffUnitObj.Id);
            }
        }

        public static void RemoveMonitorTriggerList(this GlobalBuffComponent self, GlobalBuffUnitObj globalBuffUnitObj)
        {
            foreach (var monitorTrigger in globalBuffUnitObj.monitorTriggerList)
            {
                self.monitorTriggerList.Remove(monitorTrigger.Key, globalBuffUnitObj.Id);
            }
        }

        //=================================================

        public static async ETTask<GlobalBuffPlayerObj> AddGlobalBuff_Player(this GlobalBuffComponent self, long playerId, PlayerGlobalBuffCfg playerGlobalBuffCfg, ActionPlayerContext actionPlayerContext)
        {
            await ETTask.CompletedTask;
            GlobalBuffPlayerObj globalBuffPlayerObj = self.AddChild<GlobalBuffPlayerObj>();
            globalBuffPlayerObj.Init(playerId, playerGlobalBuffCfg);
            globalBuffPlayerObj.InitActionContext(ref actionPlayerContext);

            self.AddMonitorTriggerList(globalBuffPlayerObj);

            self.childId2GlobalBuffType.Add(globalBuffPlayerObj.Id, GlobalBuffType.Player);

            return globalBuffPlayerObj;
        }

        public static void DealWhenRemoveBuff(this GlobalBuffComponent self, GlobalBuffPlayerObj globalBuffPlayerObj)
        {
            self.RemoveMonitorTriggerList(globalBuffPlayerObj);
        }

        public static void AddMonitorTriggerList(this GlobalBuffComponent self, GlobalBuffPlayerObj globalBuffPlayerObj)
        {
            foreach (var monitorTrigger in globalBuffPlayerObj.monitorTriggerList)
            {
                self.monitorTriggerList.Add(monitorTrigger.Key, globalBuffPlayerObj.Id);
            }
        }

        public static void RemoveMonitorTriggerList(this GlobalBuffComponent self, GlobalBuffPlayerObj globalBuffPlayerObj)
        {
            foreach (var monitorTrigger in globalBuffPlayerObj.monitorTriggerList)
            {
                self.monitorTriggerList.Remove(monitorTrigger.Key, globalBuffPlayerObj.Id);
            }
        }

        //=================================================

        public static async ETTask<GlobalBuffGameObj> AddGlobalBuff_Game(this GlobalBuffComponent self, GameGlobalBuffCfg gameGlobalBuffCfg, ActionGameContext actionGameContext)
        {
            await ETTask.CompletedTask;
            GlobalBuffGameObj globalBuffGameObj = self.AddChild<GlobalBuffGameObj>();
            globalBuffGameObj.Init(gameGlobalBuffCfg);
            globalBuffGameObj.InitActionContext(ref actionGameContext);

            self.AddMonitorTriggerList(globalBuffGameObj);

            self.childId2GlobalBuffType.Add(globalBuffGameObj.Id, GlobalBuffType.Game);

            return globalBuffGameObj;
        }

        public static void DealWhenRemoveBuff(this GlobalBuffComponent self, GlobalBuffGameObj globalBuffGameObj)
        {
            self.RemoveMonitorTriggerList(globalBuffGameObj);
        }

        public static void AddMonitorTriggerList(this GlobalBuffComponent self, GlobalBuffGameObj globalBuffGameObj)
        {
            foreach (var monitorTrigger in globalBuffGameObj.monitorTriggerList)
            {
                self.monitorTriggerList.Add(monitorTrigger.Key, globalBuffGameObj.Id);
            }
        }

        public static void RemoveMonitorTriggerList(this GlobalBuffComponent self, GlobalBuffGameObj globalBuffGameObj)
        {
            foreach (var monitorTrigger in globalBuffGameObj.monitorTriggerList)
            {
                self.monitorTriggerList.Remove(monitorTrigger.Key, globalBuffGameObj.Id);
            }
        }

        //=================================================

        public static bool ChkIsTrigGlobalBuff(this GlobalBuffComponent self, AbilityGameMonitorTriggerEvent abilityGameMonitorTriggerEvent)
        {
            if (self.monitorTriggerList.ContainsKey(abilityGameMonitorTriggerEvent) == false)
            {
                return false;
            }

            return true;
        }

        public static void EventHandler(this GlobalBuffComponent self, AbilityGameMonitorTriggerEvent abilityGameMonitorTriggerEvent, Unit onAttackUnit, Unit beHurtUnit)
        {
            if (self.monitorTriggerList.ContainsKey(abilityGameMonitorTriggerEvent) == false)
            {
                return;
            }
            List<long> globalBuffObjs = self.monitorTriggerList[abilityGameMonitorTriggerEvent];
            foreach (var globalBuffObjId in globalBuffObjs)
            {
                GlobalBuffType globalBuffType = self.childId2GlobalBuffType[globalBuffObjId];
                if (globalBuffType == GlobalBuffType.Unit)
                {
                    GlobalBuffUnitObj globalBuffUnitObj = self.GetChild<GlobalBuffUnitObj>(globalBuffObjId);
                    globalBuffUnitObj.TrigEvent(abilityGameMonitorTriggerEvent, onAttackUnit, beHurtUnit);
                }
                else if (globalBuffType == GlobalBuffType.Player)
                {
                    GlobalBuffPlayerObj globalBuffPlayerObj = self.GetChild<GlobalBuffPlayerObj>(globalBuffObjId);
                    globalBuffPlayerObj.TrigEvent(abilityGameMonitorTriggerEvent, onAttackUnit, beHurtUnit);
                }
                else if (globalBuffType == GlobalBuffType.Game)
                {
                    GlobalBuffGameObj globalBuffGameObj = self.GetChild<GlobalBuffGameObj>(globalBuffObjId);
                    globalBuffGameObj.TrigEvent(abilityGameMonitorTriggerEvent, onAttackUnit, beHurtUnit);
                }
            }
        }

    }
}