using System.Collections.Generic;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;
using Unity.Mathematics;

namespace ET.Ability
{
    [ComponentOf(typeof (Unit))]
    public class BulletObj: Entity, IAwake, IDestroy
    {
        public string CfgId { get; set; }
        
        ///<summary>
        ///这是一颗怎样的子弹
        ///</summary>
        [BsonIgnore]
        public BulletCfg model
        {
            get
            {
                return BulletCfgCategory.Instance.Get(this.CfgId);
            }
        }

        ///<summary>
        ///要发射子弹的这个人的gameObject，这里就认角色（拥有ChaState的）
        ///当然可以是null发射的，但是写效果逻辑的时候得小心caster是null的情况
        ///</summary>
        public long casterUnitId;

        // ///<summary>
        // ///子弹发射时候，caster的属性，如果caster不存在，就会是一个ChaProperty.zero
        // ///在一些设计中，比如wow的技能中，技能效果是跟发出时候的角色状态有关的，之后即使获得或者取消了buff，更换了装备，数值一样不会受到影响，所以得记录这个释放当时的值
        // ///</summary>
        // public ChaProperty propWhileCast = ChaProperty.zero;

        ///<summary>
        ///子弹的生命周期，单位：秒
        ///</summary>
        public float duration;

        ///<summary>
        ///子弹已经存在了多久了，单位：秒
        ///毕竟duration是可以被重设的，比如经过一个aoe，生命周期减半了
        ///</summary>
        public float timeElapsed = 0;

        ///<summary>
        ///子弹命中纪录
        ///</summary>
        public ListComponent<BulletHitRecord> hitRecords;

        ///<summary>
        ///子弹创建后多久是没有碰撞的，这样比如子母弹之类的，不会在创建后立即命中目标，但绝大多子弹还应该是0的
        ///单位：秒
        ///</summary>
        public float canHitAfterCreated = 0;

        ///<summary>
        ///还能命中几次
        ///</summary>
        public int canHitTimes = 1;
    }

    ///<summary>
    ///子弹命中纪录
    ///</summary>
    public class BulletHitRecord
    {
        ///<summary>
        ///角色的GameObject
        ///</summary>
        public long targetUnitId;

        ///<summary>
        ///多久之后还能再次命中，单位秒
        ///</summary>
        public float timeToCanHit;
    }
}