﻿using System.Collections.Generic;
using System.Text;

namespace ET
{
    [FriendOf(typeof(Cell))]
    public static class CellSystem
    {
        [ObjectSystem]
        public class CellDestroySystem: DestroySystem<Cell>
        {
            protected override void Destroy(Cell self)
            {
                self.AOIUnits.Clear();

                self.SubscribeEnterEntities.Clear();

                self.SubscribeLeaveEntities.Clear();
            }
        }

        public static void Add(this Cell self, AOIEntity aoiEntity)
        {
            self.AOIUnits.Add(aoiEntity.Id, aoiEntity);
        }

        public static void Remove(this Cell self, AOIEntity aoiEntity)
        {
            self.AOIUnits.Remove(aoiEntity.Id);
        }

        public static string CellIdToString(this long cellId)
        {
            int y = (int) (cellId & 0xffffffff);
            int x = (int) ((ulong) cellId >> 32);
            return $"{x}:{y}";
        }

        public static string CellIdToString(this HashSet<long> cellIds)
        {
            StringBuilder sb = new StringBuilder();
            foreach (long cellId in cellIds)
            {
                sb.Append(cellId.CellIdToString());
                sb.Append(",");
            }

            return sb.ToString();
        }
    }
}