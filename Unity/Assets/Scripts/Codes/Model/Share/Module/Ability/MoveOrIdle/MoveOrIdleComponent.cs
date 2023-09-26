using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Ability
{
    /// <summary>
    /// 移动方式
    /// </summary>
    public enum MoveInputType
    {
        Stop,
        /// <summary>
        /// 罗盘输入
        /// </summary>
        Direction,
        /// <summary>
        /// 目的地位置输入
        /// </summary>
        TargetPosition,
    }

    [ComponentOf(typeof(Unit))]
	public class MoveOrIdleComponent: Entity, IAwake, IDestroy, IFixedUpdate
    {
        private EntityRef<TimelineObj> _IdleTimeLineObj;
        public TimelineObj CurIdleTimelineObj
        {
            get
            {
                return this._IdleTimeLineObj;
            }
            set
            {
                this._IdleTimeLineObj = value;
            }
        }

        private EntityRef<TimelineObj> _moveTimeLineObj;
        public TimelineObj CurMoveTimelineObj
        {
            get
            {
                return this._moveTimeLineObj;
            }
            set
            {
                this._moveTimeLineObj = value;
            }
        }

        public MoveInputType moveInputType;
        public float3 directionInput;
        public float3 targetPositionInput;
    }
}