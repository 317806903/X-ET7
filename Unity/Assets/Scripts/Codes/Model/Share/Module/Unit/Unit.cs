using System.Diagnostics;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;
using Unity.Mathematics;

namespace ET
{
    [ChildOf(typeof(UnitComponent))]
    [DebuggerDisplay("ViewName,nq")]
    public class Unit: Entity, IAwake<string>
    {
        public string CfgId { get; set; } //配置表id

        [BsonIgnore]
        public UnitCfg model
        {
            get
            {
                if (Type == UnitType.Bullet || Type == UnitType.Aoe)
                {
                    return null;
                }
                return UnitCfgCategory.Instance.Get(this.CfgId);
            }
        }

        public int level;
        public UnitType Type;

        [BsonElement]
        private float3 position; //坐标

        [BsonIgnore]
        public float3 Position
        {
            get => this.position;
            set
            {
                if (this.position.Equals(value))
                {
                    return;
                }
                float3 oldPos = this.position;
                this.position = value;
                EventSystem.Instance.Publish(this.DomainScene(), new EventType.ChangePosition() { Unit = this, OldPos = oldPos });
            }
        }

        public void SetPositionWhenClient(float3 position)
        {
            this.position = position;
        }

        [BsonIgnore]
        public float3 EulerAnglesDegrees
        {
            get
            {
                return math.degrees(this.EulerAngles);
            }
        }

        [BsonIgnore]
        public float3 EulerAngles
        {
            get
            {
                float3 eulerAngles = QuaternionToEulerAngles(this.Rotation);
                return eulerAngles;
            }
            set
            {
                this.Rotation = quaternion.Euler(value);
            }
        }

        private float3 QuaternionToEulerAngles(quaternion q)
        {
            // 从四元数计算欧拉角
            float3 euler = new float3(
                (math.asin(2.0f * (q.value.w * q.value.x - q.value.y * q.value.z))),
                (math.atan2(2.0f * (q.value.w * q.value.y + q.value.x * q.value.z),
                    1.0f - 2.0f * (q.value.y * q.value.y + q.value.z * q.value.z))),
                (math.atan2(2.0f * (q.value.w * q.value.z + q.value.x * q.value.y),
                    1.0f - 2.0f * (q.value.z * q.value.z + q.value.x * q.value.x)))
            );

            return euler;
        }

        [BsonIgnore]
        public float3 Forward
        {
            get => math.mul(this.Rotation, math.forward());
            set
            {
                if (value.x == 0 && value.z == 0)
                {
                    value.x = 0.0001f;
                }
                this.Rotation = quaternion.LookRotation(value, math.up());
            }
        }

        [BsonElement]
        private quaternion rotation;

        [BsonIgnore]
        public quaternion Rotation
        {
            get => this.rotation;
            set
            {
                if (this.rotation.Equals(value))
                {
                    return;
                }
                this.rotation = value;
                EventSystem.Instance.Publish(this.DomainScene(), new EventType.ChangeRotation() { Unit = this });
            }
        }

        public void SetRotationWhenClient(quaternion rotation)
        {
            this.rotation = rotation;
        }

        protected override string ViewName
        {
            get
            {
                return $"{this.GetType().FullName} ({this.Id})";
            }
        }
    }
}