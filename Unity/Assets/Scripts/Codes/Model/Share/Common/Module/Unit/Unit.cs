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
        public float3 EulerAngles
        {
            get
            {
                float3 eulerAngles = ET.MathHelper.QuaternionToEulerAngles(this.Rotation);
                return eulerAngles;
            }
            set
            {
                this.Rotation = quaternion.Euler(math.radians(value));
            }
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