using System;
using System.Collections;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.UIElements;
using YooAsset;

namespace ET
{
    [InitializeOnLoad]
    public static class MyEditorToolbar
    {
        private static readonly Type kToolbarType = typeof (Editor).Assembly.GetType("UnityEditor.Toolbar");

        private static ScriptableObject sCurrentToolbar;

        static MyEditorToolbar()
        {
            EditorApplication.update += OnUpdate;
        }

        private static void OnUpdate()
        {
            OnUpdateButton();
        }

        private static void OnUpdateButton()
        {
            if (EditorApplication.isPlaying == false)
            {
                if (DateTime.Now.Second % 15 == 0)
                {
                    System.GC.Collect();
                }
            }
            if (sCurrentToolbar == null)
            {
                UnityEngine.Object[] toolbars = Resources.FindObjectsOfTypeAll(kToolbarType);
                sCurrentToolbar = toolbars.Length > 0? (ScriptableObject) toolbars[0] : null;

                if (sCurrentToolbar != null)
                {
                    FieldInfo root = sCurrentToolbar.GetType().GetField("m_Root", BindingFlags.NonPublic | BindingFlags.Instance);

                    VisualElement concreteRoot = root.GetValue(sCurrentToolbar) as VisualElement;
                    VisualElement toolbarZone = concreteRoot.Q("ToolbarZoneRightAlign");
                    VisualElement parent = new VisualElement() { style = { flexGrow = 1, flexDirection = FlexDirection.Row, } };
                    IMGUIContainer containerHotFix = new IMGUIContainer();
                    containerHotFix.onGUIHandler += OnGuiHotFix;
                    parent.Add(containerHotFix);
                    IMGUIContainer containerReset = new IMGUIContainer();
                    containerReset.onGUIHandler += OnGuiReset;
                    parent.Add(containerReset);
                    toolbarZone.Add(parent);
                }
            }
        }

        static GUIContent ResetLocalEditorGuiContent = new GUIContent("ResetLocalEditor", EditorGUIUtility.FindTexture("PlayButton"));
        static GUIContent HotFixGuiContent = new GUIContent("热重载HotFix", EditorGUIUtility.FindTexture("PlayButton"));
        static GUIContent ReloadAllGuiContent = new GUIContent("热重载All", EditorGUIUtility.FindTexture("PlayButton"));
        static GUIContent BuildAndRunGuiContent = new GUIContent("编译启动", EditorGUIUtility.FindTexture("PlayButton"));
        private static void OnGuiReset()
        {
            if (EditorApplication.isPlaying)
            {
                return;
            }
            //自定义按钮加在此处
            GUILayout.BeginHorizontal();
            if (GUILayout.Button(ResetLocalEditorGuiContent))
            {
                GlobalConfig globalConfig = AssetDatabase.LoadAssetAtPath<GlobalConfig>("Assets/Bundles/Config/GlobalConfig/GlobalConfig.asset");
                // if (globalConfig.CodeMode != CodeMode.ClientServer)
                // {
                //     globalConfig.CodeMode = CodeMode.ClientServer;
                //     EditorUtility.SetDirty(globalConfig);
                //     AssetDatabase.SaveAssets();
                // }
                // if (globalConfig.StartConfig != "Localhost")
                // {
                //     globalConfig.StartConfig = "Localhost";
                //     EditorUtility.SetDirty(globalConfig);
                //     AssetDatabase.SaveAssets();
                // }
                globalConfig.CodeMode = CodeMode.ClientServer;
                globalConfig.StartConfig = "Localhost";
                globalConfig.dbType = DBType.NoDB;
                globalConfig.clientResScaleType = ClientResScaleType.ResScaleAndCameraOrg;
                EditorUtility.SetDirty(globalConfig);
                AssetDatabase.SaveAssets();

                ResConfig resConfig = AssetDatabase.LoadAssetAtPath<ResConfig>("Assets/Resources/ResConfig.asset");
                resConfig.IsNeedSendEventLog = false;
                resConfig.IsGameModeArcade = false;
                resConfig.IsDemoShow = false;
                resConfig.languageType = "EN";
                resConfig.Channel = "10000";
                resConfig.IsShowLanguagePre = false;
                //if (resConfig.ResLoadMode != EPlayMode.EditorSimulateMode)
                {
                    resConfig.ResGameVersion = "v1.0";
                    resConfig.ResLoadMode = EPlayMode.EditorSimulateMode;
                    EditorUtility.SetDirty(resConfig);
                    AssetDatabase.SaveAssets();
                }

                EditorSceneManager.OpenScene("Assets/ResAB/Scene/Init.unity");
                BuildHelper.EnableDefineSymbols("ENABLE_VIEW;ENABLE_CODES", true);

                var tmp = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>("ProjectSettings/ProjectSettings.asset");
                EditorUtility.SetDirty(tmp);
                AssetDatabase.SaveAssets();

                BuildEditor buildEditor = UnityEditor.EditorWindow.GetWindow<BuildEditor>();
                if (buildEditor != null)
                {
                    buildEditor.Refresh();
                }
            }
            GUILayout.EndHorizontal();
        }

        private static void OnGuiHotFix()
        {
#if ENABLE_CODES
            return;
#endif
            //自定义按钮加在此处
            GUILayout.BeginHorizontal();
            if (EditorApplication.isPlaying)
            {
                if (GUILayout.Button(HotFixGuiContent))
                {
                    ReLoadHotFixDll().Coroutine();
                }

                if (GUILayout.Button(ReloadAllGuiContent))
                {
                    ReLoadDll().Coroutine();
                }
            }
            else
            {
                if (GUILayout.Button(BuildAndRunGuiContent))
                {
                    BuildAndStart().Coroutine();
                }
            }

            GUILayout.EndHorizontal();
        }

        private static async ETTask BuildAndStart()
        {
            await BuildHelper.BuildModelAndHotfix();

            EditorApplication.isPlaying = true;
        }

        public static async ETTask ReLoadHotFixDll()
        {
            await BuildHelper.BuildHotfix();

            await MonoResComponent.Instance.ReLoadMap();
            // await MonoResComponent.Instance.Destroy();
            // await MonoResComponent.Instance.InitAsync();
            //ResComponent

            // 热重载代码
            ET.CodeLoader.Instance.LoadHotfix();
            EventSystem.Instance.Load();
            Log.Debug("hot reload success!");
        }

        public static async ETTask ReLoadDll()
        {
            await BuildHelper.BuildModelAndHotfix();

            await GameObject.Find("/Init").GetComponent<Init>().Restart();
        }
    }
}