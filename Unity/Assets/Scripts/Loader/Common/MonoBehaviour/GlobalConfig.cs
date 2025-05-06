using System;
using UnityEngine;

namespace ET
{
    public enum CodeMode
    {
        Client = 1,
        Server = 2,
        ClientServer = 3,
    }

    public enum CodeOptimization
    {
        None = 1,
        Debug = 2,
        Release = 3,
    }

    public enum DBType
    {
        NoDB,
        MongoDB,
        LocalDB,
    }

  	public enum ClientResScaleType
  	{
        ResOrgAndCameraScale,
        ResScaleAndCameraOrg,
    }

    [CreateAssetMenu(menuName = "ET/CreateGlobalConfig", fileName = "GlobalConfig", order = 0)]
    public class GlobalConfig: ScriptableObject
    {
        public static GlobalConfig Instance;

        public CodeMode CodeMode;
        public CodeOptimization codeOptimization = CodeOptimization.Debug;

        public string StartConfig;

        public int ModelVersion = 1;

        public int HotFixVersion = 1;

        public DBType dbType;
        public ClientResScaleType clientResScaleType;
    }
}