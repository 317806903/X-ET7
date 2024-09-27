using System.Collections.Generic;

namespace ET
{
    [FriendOf(typeof(AOIEntity))]
    [FriendOf(typeof(Unit))]
    public static class AOIHelper
    {
        public static bool ChkNeedNoticePlayer(Unit unit)
        {
            //if (unit.Type == UnitType.PlayerUnit || unit.Type == UnitType.ObserverUnit)
            if (unit.Type == UnitType.ObserverUnit)
            {
                return true;
            }
            return false;
        }

        public static long CreateCellId(int x, int y)
        {
            return (long) ((ulong) x << 32) | (uint) y;
        }

        public static void CalcEnterAndLeaveCell(AOIEntity aoiEntity, int cellX, int cellY, HashSet<long> enterCell, HashSet<long> leaveCell)
        {
            enterCell.Clear();
            leaveCell.Clear();
            int r = (aoiEntity.ViewDistance - 1) / AOIManagerComponent.CellSize + 1;
            int leaveR = r;
            if (AOIHelper.ChkNeedNoticePlayer(aoiEntity.Unit))
            {
                leaveR += 1;
            }

            for (int i = cellX - leaveR; i <= cellX + leaveR; ++i)
            {
                for (int j = cellY - leaveR; j <= cellY + leaveR; ++j)
                {
                    long cellId = CreateCellId(i, j);
                    leaveCell.Add(cellId);

                    if (i > cellX + r || i < cellX - r || j > cellY + r || j < cellY - r)
                    {
                        continue;
                    }

                    enterCell.Add(cellId);
                }
            }
        }

        public static async ETTask<bool> ChkAOIReady(Entity self, Unit unit)
        {
            bool isReady = false;
            while (isReady == false)
            {
                AOIEntity aoiEntity = unit.GetComponent<AOIEntity>();
                if (aoiEntity != null)
                {
                    isReady = aoiEntity.ChkIsReady();
                }
                await TimerComponent.Instance.WaitFrameAsync();
                if ((self != null && self.IsDisposed) || unit.IsDisposed)
                {
                    return false;
                }
            }
            return true;
        }

    }
}