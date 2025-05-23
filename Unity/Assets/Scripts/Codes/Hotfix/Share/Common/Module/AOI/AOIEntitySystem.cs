﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Mathematics;

namespace ET
{
    [FriendOf(typeof(AOIEntity))]
    [FriendOf(typeof(Unit))]
    [FriendOf(typeof(Cell))]
    public static class AOIEntitySystem
    {
        [ObjectSystem]
        public class AwakeSystem: AwakeSystem<AOIEntity, int, float3>
        {
            protected override void Awake(AOIEntity self, int distance, float3 pos)
            {
                self.ViewDistance = distance;
                self.bInit = false;
                //self.DomainScene().GetComponent<AOIManagerComponent>().Add(self, pos.x, pos.z);
                self.WaitNextFrame(pos.x, pos.z).Coroutine();
            }
        }

        [ObjectSystem]
        public class DestroySystem: DestroySystem<AOIEntity>
        {
            protected override void Destroy(AOIEntity self)
            {
                self.DomainScene().GetComponent<AOIManagerComponent>()?.Remove(self);
                self.ViewDistance = 0;
                self.Cell = null;
                self.SeeUnits.Clear();
                self.SeePlayers.Clear();
                self.BeSeePlayers.Clear();
                self.BeSeeUnits.Clear();
                self.SubscribeEnterCells.Clear();
                self.SubscribeLeaveCells.Clear();
                self.enterHashSet.Clear();
                self.leaveHashSet.Clear();
            }
        }

        public static bool ChkIsReady(this AOIEntity self)
        {
            return self.bInit;
        }

        public static async ETTask WaitNextFrame(this AOIEntity self, float posX, float posZ)
        {
            await TimerComponent.Instance.WaitFrameAsync();
            if (self == null)
            {
                Log.Error(" self == null");
                return;
            }
            if (self.IsDisposed)
            {
                //Log.Debug(" self.IsDisposed");
                return;
            }
            if (self.DomainScene() == null)
            {
                Log.Error(" self.DomainScene() == null");
                return;
            }
            if (self.DomainScene().GetComponent<AOIManagerComponent>() == null)
            {
                Log.Error(" self.DomainScene().GetComponent<AOIManagerComponent>() == null");
                return;
            }
            self.DomainScene().GetComponent<AOIManagerComponent>().Add(self, posX, posZ);
            self.bInit = true;
            await ETTask.CompletedTask;
        }

        // 获取在自己视野中的对象
        public static Dictionary<long, EntityRef<AOIEntity>> GetSeeUnits(this AOIEntity self)
        {
            return self.SeeUnits;
        }

        public static Dictionary<long, EntityRef<AOIEntity>> GetBeSeePlayers(this AOIEntity self)
        {
            return self.BeSeePlayers;
        }

        public static Dictionary<long, EntityRef<AOIEntity>> GetSeePlayers(this AOIEntity self)
        {
            return self.SeePlayers;
        }

        // cell中的unit进入self的视野
        public static void SubEnter(this AOIEntity self, Cell cell)
        {
            cell.SubscribeEnterEntities.Add(self.Id, self);
            foreach (var kv in cell.AOIUnits)
            {
                if (kv.Key == self.Id)
                {
                    continue;
                }

                self.EnterSight(kv.Value);
            }
        }

        public static void UnSubEnter(this AOIEntity self, Cell cell)
        {
            cell.SubscribeEnterEntities.Remove(self.Id);
        }

        public static void SubLeave(this AOIEntity self, Cell cell)
        {
            cell.SubscribeLeaveEntities.Add(self.Id, self);
        }

        // cell中的unit离开self的视野
        public static void UnSubLeave(this AOIEntity self, Cell cell)
        {
            foreach (var kv in cell.AOIUnits)
            {
                if (kv.Key == self.Id)
                {
                    continue;
                }

                self.LeaveSight(kv.Value);
            }

            cell.SubscribeLeaveEntities.Remove(self.Id);
        }

        // enter进入self视野
        public static void EnterSight(this AOIEntity self, AOIEntity enter)
        {
            // 有可能之前在Enter，后来出了Enter还在LeaveCell，这样仍然没有删除，继续进来Enter，这种情况不需要处理
            if (self.SeeUnits.ContainsKey(enter.Id))
            {
                return;
            }

            if (!AOISeeCheckHelper.IsCanSee(self, enter))
            {
                return;
            }

            if (AOIHelper.ChkNeedNoticePlayer(self.Unit))
            {
                if (AOIHelper.ChkNeedNoticePlayer(enter.Unit))
                {
                    self.SeePlayers.Add(enter.Id, enter);
                }
                self.SeeUnits.Add(enter.Id, enter);
                enter.BeSeeUnits.Add(self.Id, self);
                enter.BeSeePlayers.Add(self.Id, self);
            }
            else
            {
                if (AOIHelper.ChkNeedNoticePlayer(enter.Unit))
                {
                    self.SeePlayers.Add(enter.Id, enter);
                }
                self.SeeUnits.Add(enter.Id, enter);
                enter.BeSeeUnits.Add(self.Id, self);
            }
            EventSystem.Instance.Publish(self.DomainScene(), new EventType.UnitEnterSightRange() { A = self, B = enter });
        }

        // leave离开self视野
        public static void LeaveSight(this AOIEntity self, AOIEntity leave)
        {
            if (self == null || leave == null)
            {
                return;
            }
            if (self.Id == leave.Id)
            {
                return;
            }

            if (!self.SeeUnits.ContainsKey(leave.Id))
            {
                return;
            }

            self.SeeUnits.Remove(leave.Id);
            if (AOIHelper.ChkNeedNoticePlayer(leave.Unit))
            {
                self.SeePlayers.Remove(leave.Id);
            }

            leave.BeSeeUnits.Remove(self.Id);
            if (AOIHelper.ChkNeedNoticePlayer(self.Unit))
            {
                leave.BeSeePlayers.Remove(self.Id);
            }

            EventSystem.Instance.Publish(self.DomainScene(), new EventType.UnitLeaveSightRange { A = self, B = leave });
        }

        /// <summary>
        /// 是否在Unit视野范围内
        /// </summary>
        /// <param name="self"></param>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public static bool IsBeSee(this AOIEntity self, long unitId)
        {
            return self.BeSeePlayers.ContainsKey(unitId);
        }

        public static void ReNotice(this AOIEntity self)
        {
            self.DomainScene().GetComponent<AOIManagerComponent>()?.ReNotice(self);
        }
    }
}