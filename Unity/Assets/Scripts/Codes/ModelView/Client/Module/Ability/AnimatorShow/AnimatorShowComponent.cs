using System.Collections.Generic;
using ET.AbilityConfig;
using UnityEngine;

namespace ET.Ability.Client
{
    [ComponentOf]
	public class AnimatorShowComponent: Entity, IAwake, IDestroy, IUpdate
    {
        public RecordAnimatorName curRecordAnimatorName;
        public Dictionary<string, AnimationClip> animationClips;
        public Dictionary<string, AnimatorControllerParameterType> Parameter;

        public AnimatorMotionName CurMotionType;
        public AnimatorMotionName NextMotionType;
        public long NextMotionTickTime;

        public long chgToIdleTime = -1;

        public bool isStop;
        public float stopSpeed;
        public Animator mainAnimator;
        public List<Animator> animatorOtherList;

        private EntityRef<AnimatorComponent> _animatorComponent;
        public AnimatorComponent animatorComponent
        {
            get
            {
                return this._animatorComponent;
            }
            set
            {
                this._animatorComponent = value;
            }
        }
    }
}