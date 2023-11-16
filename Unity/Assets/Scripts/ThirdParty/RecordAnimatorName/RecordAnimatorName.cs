using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class RecordAnimatorName : MonoBehaviour
    {
        public SerializableDictionary<string, string> recordDic = new();
        public string GetRecordValue(string key)
        {
            if (this.recordDic.TryGetValue(key, out string value))
            {
                return value;
            }
            return "";
        }

#if UNITY_EDITOR
        [ContextMenu("---ResetRecordDic")]
        public void ResetRecordDic()
        {
            Animator animator = this.gameObject.GetComponent<Animator>();
            if (animator == null)
            {
                return;
            }
            UnityEditor.Animations.AnimatorController animatorController = animator.runtimeAnimatorController as UnityEditor.Animations.AnimatorController;
            UnityEditor.Animations.AnimatorStateMachine animatorStateMachine = animatorController.layers[0].stateMachine;
            this.recordDic.Clear();
            foreach (UnityEditor.Animations.ChildAnimatorState childAnimatorState in animatorStateMachine.states)
            {
                this.recordDic[childAnimatorState.state.name] = childAnimatorState.state.motion.name;
            }
        }
#endif
    }
}
