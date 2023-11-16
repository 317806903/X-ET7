using UnityEditor;
using UnityEngine;

namespace Unity.Services.PlayerAccounts
{
    [CustomEditor(typeof(PlayerAccountSettings))]
    internal class PlayerAccountSettingsEditor : UnityEditor.Editor
    {
        SerializedProperty m_UseCustomDeepLinkUriProp;
        SerializedProperty m_ScopeMaskProp;
        SerializedProperty m_CustomSchemeProp;
        SerializedProperty m_CustomHostProp;

        void OnEnable()
        {
            m_UseCustomDeepLinkUriProp = serializedObject.FindProperty("useCustomDeepLinkUri");
            m_ScopeMaskProp = serializedObject.FindProperty("scopeMask");
            m_CustomSchemeProp = serializedObject.FindProperty("customScheme");
            m_CustomHostProp = serializedObject.FindProperty("customHost");
        }

        /// <summary>
        ///
        /// </summary>
        public override void OnInspectorGUI()
        {
            // Draw default inspector
            DrawDefaultInspector();

            // Display the dropdown list for scope selection
            m_ScopeMaskProp.intValue = (int)(PlayerAccountSettings.SupportedScopesEnum)EditorGUILayout.EnumFlagsField(
                new GUIContent("Scope", "'All' and 'Empty' options grant all available scopes"),
                PlayerAccountSettings.Instance.ScopeFlags
            );

            // Draw useCustomDeepLinkUri field
            EditorGUILayout.PropertyField(m_UseCustomDeepLinkUriProp);

            // Only draw customScheme and customHost fields if useCustomDeepLinkUri is true
            if (m_UseCustomDeepLinkUriProp.boolValue)
            {
                EditorGUILayout.PropertyField(m_CustomSchemeProp);
                EditorGUILayout.PropertyField(m_CustomHostProp);
            }

            // Apply changes to serialized object
            serializedObject.ApplyModifiedProperties();
        }
    }
}
