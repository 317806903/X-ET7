#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine.UI;

namespace DynamicAtlas
{
    [CustomEditor(typeof (DynamicAtlasMono))]
    public class DynamicAtlasMonoEditor: Editor
    {
        private DynamicAtlasMono m_Target;

        private bool showFoldout;

        protected void OnEnable()
        {
            m_Target = (DynamicAtlasMono)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUILayout.Space(5);
            EditorGUILayout.LabelField("--------------------------------------------------------------------------------------");
            var dynamicAtlasMap = DynamicAtlasMgr.Instance.GetDynamicAtlasList();
            foreach ((DynamicAtlasGroup dynamicAtlasGroup, DynamicAtlas dynamicAtlas) in dynamicAtlasMap)
            {
                EditorGUILayout.LabelField($"----- {dynamicAtlasGroup.ToString()} --------");
                for (int i = 0; i < dynamicAtlas.GetPageList().Count; i++)
                {
                    DynamicAtlasPage dynamicAtlasPage = dynamicAtlas.GetPageList()[i];
                    EditorGUILayout.ObjectField($"Atlas{i}", dynamicAtlasPage.texture, typeof (Texture2D));
                }
            }

            EditorGUILayout.BeginHorizontal();
            showFoldout = EditorGUILayout.Foldout(showFoldout, $"散图列表:");
            if (GUILayout.Button("复制散图路径"))
            {
                StringBuilder stringBuilder = new();
                foreach ((DynamicAtlasGroup dynamicAtlasGroup, DynamicAtlas dynamicAtlas) in dynamicAtlasMap)
                {
                    foreach (SaveTextureData saveTextureData in dynamicAtlas.GetUsingTexture().Values)
                    {
                        stringBuilder.AppendLine(saveTextureData.assetPath);
                    }
                }
                GUIUtility.systemCopyBuffer = stringBuilder.ToString();
            }
            EditorGUILayout.EndHorizontal();

            if (showFoldout)
            {
                foreach ((DynamicAtlasGroup dynamicAtlasGroup, DynamicAtlas dynamicAtlas) in dynamicAtlasMap)
                {
                    foreach (SaveTextureData saveTextureData in dynamicAtlas.GetUsingTexture().Values)
                    {
                        EditorGUILayout.LabelField($"----- {saveTextureData.assetPath} --------");
                    }
                }
            }
        }
    }
}
#endif