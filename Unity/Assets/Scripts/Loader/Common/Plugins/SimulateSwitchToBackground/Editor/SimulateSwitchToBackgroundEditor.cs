#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof (SimulateSwitchToBackground))]
public class SimulateSwitchToBackgroundEditor: Editor
{
    void OnEnable()
    {
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        serializedObject.Update();

        serializedObject.ApplyModifiedProperties(); //now varibles in script have been updated

        if (GUILayout.Button("SendEnterBackgroundMessage"))
        {
            if (Application.isPlaying)
            {
                ((SimulateSwitchToBackground)target).SendEnterBackgroundMessage();
            }
        }

        if (GUILayout.Button("SendEnterFoegroundMessage"))
        {
            if (Application.isPlaying)
            {
                ((SimulateSwitchToBackground)target).SendEnterFoegroundMessage();
            }
        }
    }
}
#endif