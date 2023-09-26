using System.Collections.Generic;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;

namespace ET.Ability
{
    [ComponentOf(typeof(Unit))]
	public class AnimatorComponent: Entity, IAwake, IDestroy, IFixedUpdate, ITransferClient
    {
        [BsonIgnore]
        public bool isNeedNoticeClient;
        public AnimatorMotionName name;
        public bool isStoppingAnimator;
        public AnimatorMotionName controlStateName;
    }
}