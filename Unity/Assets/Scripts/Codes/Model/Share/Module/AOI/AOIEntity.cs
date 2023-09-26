using System.Collections.Generic;
using Unity.Mathematics;

namespace ET
{
    [ComponentOf(typeof(Unit))]
    public class AOIEntity: Entity, IAwake<int, float3>, IDestroy
    {
        public Unit Unit => this.GetParent<Unit>();

        public int ViewDistance;

        private EntityRef<Cell> cell;

        public Cell Cell
        {
            get
            {
                return cell;
            }
            set
            {
                this.cell = value;
            }
        }

        // 观察进入视野的Cell
        public HashSet<long> SubscribeEnterCells = new ();

        // 观察离开视野的Cell
        public HashSet<long> SubscribeLeaveCells = new ();

        // 观察进入视野的Cell
        public HashSet<long> enterHashSet = new ();

        // 观察离开视野的Cell
        public HashSet<long> leaveHashSet = new ();

        // 我看的见的Unit
        public Dictionary<long, EntityRef<AOIEntity>> SeeUnits = new ();

        // 看见我的Unit
        public Dictionary<long, EntityRef<AOIEntity>> BeSeeUnits = new ();

        // 我看的见的Player
        public Dictionary<long, EntityRef<AOIEntity>> SeePlayers = new ();

        // 看见我的Player单独放一个Dict，用于广播
        public Dictionary<long, EntityRef<AOIEntity>> BeSeePlayers = new ();
    }
}