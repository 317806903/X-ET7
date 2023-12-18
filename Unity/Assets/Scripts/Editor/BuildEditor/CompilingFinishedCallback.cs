using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MongoDB.Bson;
using UnityEditor;
using UnityEngine;

namespace ET
{
    public class CompilingFinishedCallback
    {
        private static string TmpMethodNamesKey = "CompilingFinishedCallback_TmpMethodNames";

        public static void Set<T>(string methodName, string arg = null)
        {
            var type = typeof (T);
            string methoedName = type.FullName + "." + methodName + "(" + arg + ")";
            AddTmpMethodName(methoedName);
        }

        public static void SetMethodName(Type type, string methodName, Dictionary<string, object> methodParamDic = null)
        {
            string methodParam = "";
            if (methodParamDic != null)
            {
                methodParam = ET.JsonHelper.ToJson(methodParamDic);
            }
            string methoedName = type.FullName + "." + methodName + "(" + methodParam + ")";
            AddTmpMethodName(methoedName);
        }

        static string[] GetTmpMethodNameArr()
        {
            string tmpMethodNames = GetTmpMethodNames();
            if (string.IsNullOrEmpty(tmpMethodNames))
            {
                return null;
            }
            return GetTmpMethodNames().Split(';').Where(value => value != null && value.Trim() != "").ToArray();
        }

        static void AddTmpMethodName(string value)
        {
            SetTmpMethodNames(GetTmpMethodNames() + ";" + value);
        }

        static string GetTmpMethodNames()
        {
            if (EditorPrefs.HasKey(TmpMethodNamesKey))
                return EditorPrefs.GetString(TmpMethodNamesKey);
            return "";
        }

        static void SetTmpMethodNames(string value)
        {
            EditorPrefs.SetString(TmpMethodNamesKey, value);
        }

        [UnityEditor.Callbacks.DidReloadScripts]
        private static void OnScriptsReloaded()
        {
            DoScriptsReloaded().Coroutine();
        }

        private static async ETTask DoScriptsReloaded()
        {
            var methods = GetTmpMethodNameArr();
            SetTmpMethodNames("");
            if (methods == null || methods.Length == 0)
            {
                return;
            }
            await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(0.5f));
            // while(assemblyBuilder.status != AssemblyBuilderStatus.Finished)
            //     System.Threading.Thread.Sleep(10);
            foreach (var method in methods)
            {
                var pointIndex = method.LastIndexOf(".");
                var left = method.LastIndexOf("(");
                var right = method.LastIndexOf(")");
                string className = method.Substring(0, pointIndex);
                string methodName = method.Substring(pointIndex + 1, left - pointIndex - 1);
                string argName = method.Substring(left + 1, right - left - 1);
                Type type = Type.GetType(className);
                if (type == null)
                {
                    Debug.LogError("type == null");
                    continue;
                }

                var method1 = type.GetMethod(methodName, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public, null, new Type[] { },
                    null);
                var method2 = type.GetMethod(methodName, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public, null,
                    new Type[] { typeof (string) }, null);
                if (method1 == null && method2 == null)
                {
                    Debug.LogError("method1 == null && method2 == null");
                    continue;
                }

                if (method1 != null && argName.Trim() == "")
                    method1.Invoke(null, null);
                if (method2 != null && argName.Trim() != "")
                {
                    CallMethod(method2, argName);
                }
            }

        }

        public static void CallMethod(MethodInfo invokeMethodWithNamedArgument, string methodParam)
        {
            Dictionary<string, object> methodParamDic = ET.JsonHelper.FromJson<Dictionary<string, object>>(methodParam);
            ParameterInfo[] parameterInfos = invokeMethodWithNamedArgument.GetParameters();
            object[] parameters = new object[parameterInfos.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                if (methodParamDic.TryGetValue(parameterInfos[i].Name, out var value))
                {
                    parameters[i] = value;
                }
            }
            invokeMethodWithNamedArgument.Invoke(null, parameters);
        }
    }
}