using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
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
	public class MoveOrIdleComponent: Entity, IAwake, IDestroy, IFixedUpdate, ITransferClient
    {
        [BsonIgnore]
        private EntityRef<TimelineObj> _IdleTimeLineObj;
        [BsonIgnore]
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

        [BsonIgnore]
        private EntityRef<TimelineObj> _moveTimeLineObj;
        [BsonIgnore]
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
        [BsonIgnore]
        public bool isIdleCreating;

    }
}