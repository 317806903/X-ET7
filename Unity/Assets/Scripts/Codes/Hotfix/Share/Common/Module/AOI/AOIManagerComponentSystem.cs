﻿using System.Collections.Generic;

namespace ET
{
    [FriendOf(typeof(AOIManagerComponent))]
    [FriendOf(typeof(AOIEntity))]
    [FriendOf(typeof(Cell))]
    public static class AOIManagerComponentSystem
    {
        public static void Add(this AOIManagerComponent self, AOIEntity aoiEntity, float x, float y)
        {
            //await TimerComponent.Instance.WaitFrameAsync();

            if (aoiEntity.Parent == null)
            {
                return;
            }

            int cellX = (int)(x * 1000) / AOIManagerComponent.CellSize;
            int cellY = (int)(y * 1000) / AOIManagerComponent.CellSize;

            if (aoiEntity.ViewDistance == 0)
            {
                aoiEntity.ViewDistance = 1;
            }

            AOIHelper.CalcEnterAndLeaveCell(aoiEntity, cellX, cellY, aoiEntity.SubscribeEnterCells, aoiEntity.SubscribeLeaveCells);

            // 遍历EnterCell
            foreach (long cellId in aoiEntity.SubscribeEnterCells)
            {
                Cell cell = self.GetCell(cellId);
                aoiEntity.SubEnter(cell);
            }

            // 遍历LeaveCell
            foreach (long cellId in aoiEntity.SubscribeLeaveCells)
            {
                Cell cell = self.GetCell(cellId);
                aoiEntity.SubLeave(cell);
            }

            // 自己加入的Cell
            Cell selfCell = self.GetCell(AOIHelper.CreateCellId(cellX, cellY));
            aoiEntity.Cell = selfCell;
            selfCell.Add(aoiEntity);
            // 通知订阅该Cell Enter的Unit
            foreach (var kv in selfCell.SubscribeEnterEntities)
            {
                AOIEntity aoiEntityTmp = kv.Value;
                aoiEntityTmp.EnterSight(aoiEntity);
            }
        }

        public static void Remove(this AOIManagerComponent self, AOIEntity aoiEntity)
        {
            if (aoiEntity.Cell == null)
            {
                return;
            }

            // 通知订阅该Cell Leave的Unit
            aoiEntity.Cell.Remove(aoiEntity);
            foreach (var kv in aoiEntity.Cell.SubscribeLeaveEntities)
            {
                AOIEntity aoiEntityTmp = kv.Value;
                aoiEntityTmp.LeaveSight(aoiEntity);
            }

            // 通知自己订阅的Enter Cell，清理自己
            foreach (long cellId in aoiEntity.SubscribeEnterCells)
            {
                Cell cell = self.GetCell(cellId);
                aoiEntity.UnSubEnter(cell);
            }

            foreach (long cellId in aoiEntity.SubscribeLeaveCells)
            {
                Cell cell = self.GetCell(cellId);
                aoiEntity.UnSubLeave(cell);
            }

            // 检查
            if (aoiEntity.SeeUnits.Count > 1)
            {
                Log.Error($"aoiEntity has see units: {aoiEntity.SeeUnits.Count}");
            }

            if (aoiEntity.BeSeeUnits.Count > 1)
            {
                Log.Error($"aoiEntity has beSee units: {aoiEntity.BeSeeUnits.Count}");
            }
        }

        public static void ReNotice(this AOIManagerComponent self, AOIEntity aoiEntity)
        {
            if (aoiEntity.Parent == null)
            {
                return;
            }

            foreach (var aoiEntitys in aoiEntity.GetSeeUnits())
            {
                AOIEntity aoiEntityTmp = aoiEntitys.Value;
                EventSystem.Instance.Publish(self.DomainScene(), new EventType.UnitEnterSightRange() { A = aoiEntity, B = aoiEntityTmp });
            }
        }

        private static Cell GetCell(this AOIManagerComponent self, long cellId)
        {
            Cell cell = self.GetChild<Cell>(cellId);
            if (cell == null)
            {
                cell = self.AddChildWithId<Cell>(cellId);
            }

            return cell;
        }

        public static void Move(AOIEntity aoiEntity, Cell newCell, Cell preCell)
        {
            aoiEntity.Cell = newCell;
            preCell.Remove(aoiEntity);
            newCell.Add(aoiEntity);
            // 通知订阅该newCell Enter的Unit
            foreach (var kv in newCell.SubscribeEnterEntities)
            {
                AOIEntity aoiEntityTmp = kv.Value;
                if (aoiEntityTmp.SubscribeEnterCells.Contains(preCell.Id))
                {
                    continue;
                }

                aoiEntityTmp.EnterSight(aoiEntity);
            }

            // 通知订阅preCell leave的Unit
            foreach (var kv in preCell.SubscribeLeaveEntities)
            {
                AOIEntity aoiEntityTmp = kv.Value;
                // 如果新的cell仍然在对方订阅的subleave中
                if (aoiEntityTmp.SubscribeLeaveCells.Contains(newCell.Id))
                {
                    continue;
                }

                aoiEntityTmp.LeaveSight(aoiEntity);
            }
        }

        public static void Move(this AOIManagerComponent self, AOIEntity aoiEntity, int cellX, int cellY)
        {
            long newCellId = AOIHelper.CreateCellId(cellX, cellY);
            if (aoiEntity.Cell.Id == newCellId) // cell没有变化
            {
                return;
            }

            // 自己加入新的Cell
            Cell newCell = self.GetCell(newCellId);
            Move(aoiEntity, newCell, aoiEntity.Cell);

            AOIHelper.CalcEnterAndLeaveCell(aoiEntity, cellX, cellY, aoiEntity.enterHashSet, aoiEntity.leaveHashSet);

            // 算出自己leave新Cell
            foreach (long cellId in aoiEntity.leaveHashSet)
            {
                if (aoiEntity.SubscribeLeaveCells.Contains(cellId))
                {
                    continue;
                }

                Cell cell = self.GetCell(cellId);
                aoiEntity.SubLeave(cell);
            }

            // 算出需要通知离开的Cell
            aoiEntity.SubscribeLeaveCells.ExceptWith(aoiEntity.leaveHashSet);
            foreach (long cellId in aoiEntity.SubscribeLeaveCells)
            {
                Cell cell = self.GetCell(cellId);
                aoiEntity.UnSubLeave(cell);
            }

            // 这里交换两个HashSet,提高性能
            ObjectHelper.Swap(ref aoiEntity.SubscribeLeaveCells, ref aoiEntity.leaveHashSet);

            // 算出自己看到的新Cell
            foreach (long cellId in aoiEntity.enterHashSet)
            {
                if (aoiEntity.SubscribeEnterCells.Contains(cellId))
                {
                    continue;
                }

                Cell cell = self.GetCell(cellId);
                aoiEntity.SubEnter(cell);
            }

            // 离开的Enter
            aoiEntity.SubscribeEnterCells.ExceptWith(aoiEntity.enterHashSet);
            foreach (long cellId in aoiEntity.SubscribeEnterCells)
            {
                Cell cell = self.GetCell(cellId);
                aoiEntity.UnSubEnter(cell);
            }

            // 这里交换两个HashSet,提高性能
            ObjectHelper.Swap(ref aoiEntity.SubscribeEnterCells, ref aoiEntity.enterHashSet);
        }
    }
}