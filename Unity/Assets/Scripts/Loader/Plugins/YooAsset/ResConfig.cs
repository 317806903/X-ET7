using UnityEngine;
using YooAsset;

namespace ET
{
	public enum AreaType
	{
		CN,
		TW,
		EN,
		GDC,
		OnDevice,
	}

    [CreateAssetMenu(menuName = "ET/CreateResConfig", fileName = "ResConfig", order = 0)]
    public class ResConfig: ScriptableObject
    {
        public static ResConfig Instance;
        public EPlayMode ResLoadMode;
		public string ResHostServerIP = "http://127.0.0.1";
		public string ResGameVersion = "v1.0";
		public string Version = "1.0";
		public string Channel = "10000";
		public string RouterHttpHost = "127.0.0.1";
		public int RouterHttpPort = 30300;
		public AreaType areaType;
		public string languageType;
		public bool IsShowDebugMode = false;
		public bool IsShowEditorLoginMode = false;
		public bool IsNeedSendEventLog;
		public string MirrorARSessionAuthAppKey = "";
		public string MirrorARSessionAuthAppSecret = "";
		public bool IsGameModeArcade;
    }
}