#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine.UI;

namespace DynamicAtlas
{
    [CustomEditor(typeof (DynamicImage))]
    public class DynamicImageEditor: Editor
    {
        private DynamicImage m_Target;

        protected void OnEnable()
        {
            m_Target = (DynamicImage)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUILayout.Space(5);
            EditorGUILayout.LabelField("--------------------------------------------------------------------------------------------------------------------");
            EditorGUILayout.ObjectField($"DefaultSprite", m_Target.DefaultSprite, typeof (Sprite));
            EditorGUILayout.LabelField($"SpriteName", m_Target.SpriteName);
            if (m_Target.Atlas != null)
            {
                for (int i = 0; i < m_Target.Atlas.GetPageList().Count; i++)
                {
                    EditorGUILayout.ObjectField($"Atlas{i}", m_Target.Atlas.GetPageList()[i].texture, typeof (Texture2D));
                }
            }

            //DrawDefaultInspector();
        }
    }
}
#endif