using System.Collections.Generic;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;
using Unity.Mathematics;

namespace ET.Ability
{
    [ComponentOf(typeof (Unit))]
    public class AoeObj: Entity, IAwake, IDestroy, IFixedUpdate
    {
        public string CfgId { get; set; }

        [BsonIgnore]
        public AoeCfg model
        {
            get
            {
                return AoeCfgCategory.Instance.Get(this.CfgId);
            }
        }

        ///<summary>
        ///aoe的半径，单位：米
        ///目前这游戏的设计中，aoe只有圆形，所以只有一个半径，也不存在角度一说，如果需要可以扩展
        ///</summary>
        public float radius;

        ///<summary>
        ///aoe的施法者
        ///</summary>
        public long casterUnitId;

        ///<summary>
        ///aoe存在的时间，单位：秒
        ///</summary>
        public float duration;

        ///<summary>
        ///是否是一个永久的aoe，永久的duration不会减少，但是timeElapsed还会增加
        ///</summary>
        public bool permanent;

        ///<summary>
        ///aoe已经存在过的时间，单位：秒
        ///</summary>
        public float timeElapsed = 0;

        ///<summary>
        ///aoe执行了多少次onTick了，如果不会执行onTick，那将永远是0
        ///</summary>
        public int ticked = 0;

        ///<summary>
        ///现在aoe范围内的所有unitId
        ///</summary>
        public HashSet<long> unitIds = new ();
        public HashSet<long> chgUnitList = new ();

        [BsonIgnore]
        public AoeTargetCondition aoeTargetCondition;

        [BsonIgnore]
        public ActionContext actionContext;
    }
}