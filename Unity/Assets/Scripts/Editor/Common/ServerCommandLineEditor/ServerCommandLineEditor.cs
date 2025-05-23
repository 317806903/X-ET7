﻿using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ET
{
    public class ServerCommandLineEditor: EditorWindow
    {
        [MenuItem("ET/ServerTools")]
        public static void ShowWindow()
        {
            GetWindow<ServerCommandLineEditor>(DockDefine.Types);
        }
        
        private int selectStartConfigIndex = 1;
        private string[] startConfigs;
        private string startConfig;
        
        public void OnEnable()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo("../Config/Excel/s/StartConfig");
            this.startConfigs = directoryInfo.GetDirectories().Select(x => x.Name).ToArray();
        }

        public void OnGUI()
        {
            selectStartConfigIndex = EditorGUILayout.Popup("起服模式：", selectStartConfigIndex, this.startConfigs);
            this.startConfig = this.startConfigs[this.selectStartConfigIndex];

            string dotnet = "dotnet.exe";
            
#if UNITY_EDITOR_OSX
            dotnet = "dotnet";
#endif
            
            if (GUILayout.Button("Build Server"))
            {
                ProcessHelper.Run(dotnet, "build DotNet.sln", "../DotNet");
            }
            if (GUILayout.Button("Start Server - require admin (Single Process)"))
            {
                string arguments = $"App.dll --Process=1 --StartConfig=StartConfig/{this.startConfig} --Console=1";
                ProcessHelper.Run(dotnet, arguments, "../Bin/", false, true);
            }
            
            if (GUILayout.Button("Start Watcher"))
            {
                string arguments = $"App.dll --AppType=Watcher --StartConfig=StartConfig/{this.startConfig} --Console=1";
                ProcessHelper.Run(dotnet, arguments, "../Bin/");
            }

            if (GUILayout.Button("Start Mongo"))
            {
                ProcessHelper.Run("mongod", @"--dbpath=db", "../Database/bin/");
            }
        }
    }
}
