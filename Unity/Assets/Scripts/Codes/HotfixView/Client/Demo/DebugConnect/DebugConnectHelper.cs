using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using UnityEngine;
using UIGuide;

namespace ET.Client
{
	public static class DebugConnectHelper
	{
		public static void SeeDebugConnectList()
		{
			var list = DebugConnetCfgCategory.Instance.DataList;
			string showTxt = "";
			for (int i = 0; i < list.Count; i++)
			{
				showTxt += $"id[{list[i].Id}] name[{list[i].Name}]\n";
			}

			Log.Error($"列表: {showTxt}");
		}

		public static void SeeCurDebugConnect()
		{
			if (ChkDebugConnectNull())
			{
				string resConfigJson1 = ET.JsonHelper.ToJson(ResConfig.Instance);
				Log.Error($"当前没有指定，使用的是原生的链接 [{resConfigJson1}]");
				return;
			}
			string key = "DebugConnect_Key";
			string debugConnectKey = PlayerPrefs.GetString(key);
			if (DebugConnetCfgCategory.Instance.Contain(debugConnectKey) == false)
			{
				Log.Error($"DebugConnetCfgCategory.Instance.Contain({debugConnectKey}) == false");
				return;
			}

			string resConfigJson = ET.JsonHelper.ToJson(ResConfig.Instance);
			Log.Error($"key[{debugConnectKey}] [{resConfigJson}]");
		}

		public static void SetDebugConnect(string debugConnectKey)
		{
			if (DebugConnetCfgCategory.Instance.Contain(debugConnectKey) == false)
			{
				Log.Error($"DebugConnetCfgCategory.Instance.Contain({debugConnectKey}) == false");
				return;
			}
			string key = "DebugConnect_Key";
			PlayerPrefs.SetString(key, debugConnectKey);
		}

		public static void SetDebugConnectNull()
		{
			string key = "DebugConnect_Key";
			PlayerPrefs.SetString(key, "Null");
		}

		public static bool ChkDebugConnectNull()
		{
			string key = "DebugConnect_Key";
			if (PlayerPrefs.HasKey(key) == false)
			{
				return true;
			}
			string debugConnectKey = PlayerPrefs.GetString(key);
			if (string.IsNullOrEmpty(debugConnectKey) || debugConnectKey == "Null")
			{
				return true;
			}

			return false;
		}

		public static bool RetSetResConfig()
		{
			if (ChkDebugConnectNull())
			{
				return false;
			}
			string key = "DebugConnect_Key";
			string debugConnectKey = PlayerPrefs.GetString(key);
			if (string.IsNullOrEmpty(debugConnectKey) || debugConnectKey == "Null")
			{
				return false;
			}
			if (DebugConnetCfgCategory.Instance.Contain(debugConnectKey) == false)
			{
				Log.Error($"DebugConnetCfgCategory.Instance.Contain({debugConnectKey}) == false");
				return false;
			}

			bool isChging = false;
			DebugConnetCfg debugConnetCfg = DebugConnetCfgCategory.Instance.Get(debugConnectKey);
			if (debugConnetCfg.IsChgResUpdate)
			{
				if (ResConfig.Instance.ResHostServerIP != debugConnetCfg.ResHostServerIP)
				{
					isChging = true;
					ResConfig.Instance.ResHostServerIP = debugConnetCfg.ResHostServerIP;
				}

				if (ResConfig.Instance.ResGameVersion != debugConnetCfg.ResGameVersion)
				{
					isChging = true;
					ResConfig.Instance.ResGameVersion = debugConnetCfg.ResGameVersion;
				}
			}
			if (debugConnetCfg.IsChgServer)
			{
				if (ResConfig.Instance.RouterHttpHost != debugConnetCfg.RouterHttpHost)
				{
					isChging = true;
					ResConfig.Instance.RouterHttpHost = debugConnetCfg.RouterHttpHost;
				}

				if (ResConfig.Instance.RouterHttpPort != debugConnetCfg.RouterHttpPort)
				{
					isChging = true;
					ResConfig.Instance.RouterHttpPort = debugConnetCfg.RouterHttpPort;
				}
			}

			if (Enum.TryParse(debugConnetCfg.AreaType, out AreaType areaType))
			{
				if (ResConfig.Instance.areaType != areaType)
				{
					isChging = true;
					ResConfig.Instance.areaType = areaType;
				}
			}

			if (ResConfig.Instance.IsShowDebugMode != debugConnetCfg.IsShowDebugMode)
			{
				isChging = true;
				ResConfig.Instance.IsShowDebugMode = debugConnetCfg.IsShowDebugMode;
			}

			if (ResConfig.Instance.IsShowEditorLoginMode != debugConnetCfg.IsShowEditorLoginMode)
			{
				isChging = true;
				ResConfig.Instance.IsShowEditorLoginMode = debugConnetCfg.IsShowEditorLoginMode;
			}

			if (ResConfig.Instance.IsNeedSendEventLog != debugConnetCfg.IsNeedSendEventLog)
			{
				isChging = true;
				ResConfig.Instance.IsNeedSendEventLog = debugConnetCfg.IsNeedSendEventLog;
			}
			return isChging;
		}
	}
}
