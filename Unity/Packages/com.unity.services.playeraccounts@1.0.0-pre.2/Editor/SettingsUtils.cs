using UnityEditor;
using UnityEngine;

namespace Unity.Services.PlayerAccounts.Editor
{
    /// <summary>
    /// A utility class that provides functionality for creating and opening the PlayerAccountSettings asset in the Unity editor.
    /// </summary>
    [InitializeOnLoad]
    static class SettingsUtils
    {
        const string k_AssetPath = "Assets/Resources";
        const int k_ConfigureMenuPriority = 100;
        const string k_ServiceMenuRoot = "Services/Player Accounts/";

        /// <summary>
        /// Initializes the SettingsUtils class and ensures the PlayerAccountSettings asset is created.
        /// </summary>
        static SettingsUtils()
        {
            EditorApplication.delayCall += () => { CreateSettings(); };
        }

        /// <summary>
        /// Opens the PlayerAccountSettings asset in the Unity editor.
        /// </summary>
        [MenuItem(k_ServiceMenuRoot + "Settings", priority = k_ConfigureMenuPriority)]
        public static void OpenSettings()
        {
            var settings = CreateSettings();
            AssetDatabase.OpenAsset(settings);
        }

        /// <summary>
        /// Creates or retrieves the existing PlayerAccountSettings asset.
        /// </summary>
        /// <returns>The PlayerAccountSettings asset.</returns>
        static PlayerAccountSettings CreateSettings()
        {
            var settings = PlayerAccountSettings.Instance;

            if (settings == null)
            {
                if (!AssetDatabase.IsValidFolder(k_AssetPath))
                {
                    AssetDatabase.CreateFolder("Assets", "Resources");
                }

                settings = ScriptableObject.CreateInstance<PlayerAccountSettings>();
                AssetDatabase.CreateAsset(settings, $"{k_AssetPath}/{nameof(PlayerAccountSettings)}.asset");
            }

            return settings;
        }
    }
}
