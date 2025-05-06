#if UNITY_EDITOR && Platform_Mobile

using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(Canvas), true)]
public class CanvasEditor : Editor
{
    void OnEnable()
    {
    }

    // Update is called once per frame
    public override void OnInspectorGUI()
    {
        var targetObject = serializedObject.targetObject as Canvas;
        if(targetObject.name.StartsWith("Canvas (Environment)"))
        {
            var draw = targetObject.GetComponent<DrawRaycastor>();
            if( draw == null)
            {
                targetObject.gameObject.AddComponent<DrawRaycastor>();
            }
        }
        base.OnInspectorGUI();
        serializedObject.ApplyModifiedProperties();
    }
}
#endif