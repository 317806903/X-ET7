using System;
using DotRecast.Detour.Crowd;
using Unity.Mathematics;

namespace ET
{
    /// <summary>
    /// 同一块地图可能有多种寻路数据，玩家可以随时切换，怪物也可能跟玩家的寻路不一样，寻路组件应该挂在Unit上
    /// </summary>
    [ComponentOf(typeof (Unit))]
    public class PathfindingComponent: Entity, IAwake, IDestroy, IFixedUpdate
    {
        public const int FindRandomNavPosMaxRadius = 15000; // 随机找寻路点的最大半径

        [StaticField]
        public static float[] extents = { 15, 10, 15 };

        public string Name;

        public float[] StartPos = new float[3];

        public float[] EndPos = new float[3];

        public float NavObstacleRadius = 0f;
        public float NavObstacleHeight = 0f;

        public long _ObstacleRef = 0;
        public float3? _ObstaclePos = null;

        public EntityRef<NavmeshComponent> _NavMesh;
        public EntityRef<NavmeshManagerComponent> _NavMeshManager;
        public NavmeshComponent NavMesh
        {
            get
            {
                return this._NavMesh;
            }
            set
            {
                this._NavMesh = value;
            }
        }

        public DtCrowdAgent navMeshAgent;
        public int _WaitFrameSyncPos = 0;
        public int _CurFrameSyncPos = 0;
    }
}
