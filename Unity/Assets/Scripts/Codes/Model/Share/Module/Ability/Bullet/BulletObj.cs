using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;
using Unity.Mathematics;

namespace ET.Ability
{
    [ComponentOf(typeof (Unit))]
    public class BulletObj: Entity, IAwake, IDestroy, ITransferClient, IFixedUpdate
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
        ///要发射子弹的这个人
        ///当然可以是null发射的，但是写效果逻辑的时候得小心caster是null的情况
        ///</summary>
        public long casterUnitId;

        ///<summary>
        ///子弹的生命周期，单位：秒
        ///</summary>
        public float duration;

        ///<summary>
        ///子弹已经存在了多久了，单位：秒
        ///毕竟duration是可以被重设的，比如经过一个aoe，生命周期减半了
        ///</summary>
        public float timeElapsed = 0;

        public HashSet<long> preHitUnitIds;

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

        [BsonIgnore]
        public ActionContext actionContext;
    }

    ///<summary>
    ///子弹命中纪录
    ///</summary>
    public class BulletHitRecord: DisposablClass
    {
        ///<summary>
        ///角色的GameObject
        ///</summary>
        public long targetUnitId;

        ///<summary>
        ///多久之后还能再次命中，单位秒
        ///</summary>
        public float timeToCanHit;

        public static BulletHitRecord Create()
        {
            BulletHitRecord bulletHitRecord = ObjectPool.Instance.Fetch(typeof (SelectHandle)) as BulletHitRecord;
            bulletHitRecord.Reuse();
            return bulletHitRecord;
        }

        public override void Reuse()
        {
            this.isDisposed = false;
            base.Reuse();
        }

        public bool isDisposed; //表示是否已经被回收
        protected override void Dispose(bool disposing)
        {
            if(this.isDisposed) return; //如果已经被回收，就中断执行
            try
            {
                this.isDisposed = true;
                if(disposing) //如果需要回收一些托管资源
                {
                    ObjectPool.Instance?.Recycle(this);
                }
            }
            catch (Exception e)
            {
                Log.Error($"ET.Ability.BulletHitRecord.Dispose {e}");
            }

            base.Dispose(disposing);//再调用父类的垃圾回收逻辑
        }

    }
}