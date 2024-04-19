using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Animator))]
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
        private void Awake()
        {
            if (Application.isPlaying == false)
            {
                ResetRecordDic();
            }
        }

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
                if (childAnimatorState.state.motion == null)
                {
                    Debug.LogError($"childAnimatorState.state.motion == null [{this.gameObject.name}][{childAnimatorState.state.name}]");
                    continue;
                }
                this.recordDic[childAnimatorState.state.name] = childAnimatorState.state.motion.name;
            }
            UnityEditor.EditorUtility.SetDirty(this);
            UnityEditor.AssetDatabase.SaveAssets();
        }
#endif
    }
}
