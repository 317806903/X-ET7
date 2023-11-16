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
				Log.Error($"当前没有指定，使用的是原生的链接");
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
		public static void RetSetResConfig()
		{
			if (ChkDebugConnectNull())
			{
				return;
			}
			string key = "DebugConnect_Key";
			string debugConnectKey = PlayerPrefs.GetString(key);
			if (string.IsNullOrEmpty(debugConnectKey) || debugConnectKey == "Null")
			{
				return;
			}
			if (DebugConnetCfgCategory.Instance.Contain(debugConnectKey) == false)
			{
				Log.Error($"DebugConnetCfgCategory.Instance.Contain({debugConnectKey}) == false");
				return;
			}

			DebugConnetCfg debugConnetCfg = DebugConnetCfgCategory.Instance.Get(debugConnectKey);
			if (debugConnetCfg.IsChgResUpdate)
			{
				ResConfig.Instance.ResHostServerIP = debugConnetCfg.ResHostServerIP;
				ResConfig.Instance.ResGameVersion = debugConnetCfg.ResGameVersion;
			}
			if (debugConnetCfg.IsChgServer)
			{
				ResConfig.Instance.RouterHttpHost = debugConnetCfg.RouterHttpHost;
				ResConfig.Instance.RouterHttpPort = debugConnetCfg.RouterHttpPort;
			}

			if (Enum.TryParse(debugConnetCfg.AreaType, out AreaType areaType))
			{
				ResConfig.Instance.areaType = areaType;
			}
			ResConfig.Instance.IsShowDebugMode = debugConnetCfg.IsShowDebugMode;
			ResConfig.Instance.IsShowEditorLoginMode = debugConnetCfg.IsShowEditorLoginMode;
		}
	}
}
