﻿using System.Collections;
using System.Collections.Generic;
using System;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[Invoke(TimerInvokeType.DlgARSceneSliderSimpleFrameTimer)]
	public class DlgARSceneSliderSimpleTimer: ATimer<DlgARSceneSliderSimple>
	{
		protected override void Run(DlgARSceneSliderSimple self)
		{
			try
			{
				self.Update();
			}
			catch (Exception e)
			{
				Log.Error($"DlgARSceneSliderSimpleTimer timer error: {self.Id}\n{e}");
			}
		}
	}

	[FriendOf(typeof(DlgARSceneSliderSimple))]
	public static class DlgARSceneSliderSimpleSystem
	{
		public static void RegisterUIEvent(this DlgARSceneSliderSimple self)
		{
			self.View.EButton_1Button.AddListenerAsync(self.SetSceneScale1);
			self.View.EButton_2Button.AddListenerAsync(self.SetSceneScale2);
			self.View.EButton_3Button.AddListenerAsync(self.SetSceneScale3);
		}

		public static async ETTask ShowWindow(this DlgARSceneSliderSimple self, ShowWindowData contextData = null)
		{
			self.SetSceneScaleInfo();

			self.Timer = TimerComponent.Instance.NewFrameTimer(TimerInvokeType.DlgARSceneSliderSimpleFrameTimer, self);
		}

		public static void HideWindow(this DlgARSceneSliderSimple self)
		{
			TimerComponent.Instance?.Remove(ref self.Timer);
			foreach (var go in self.showPrefabList)
			{
				GameObject.Destroy(go);
			}
			self.showPrefabList.Clear();
		}

		public static void SetSceneScaleInfo(this DlgARSceneSliderSimple self)
		{
			self.showPrefabCfgList = new()
			{
				(new float2(0, 0), "Unit_HeadQuarterPreview")

			};
			self.orgPrefabLocalScaleDic = new();
			self.showPrefabList = new();

			self.scaleSettingList = new List<float>() { 75, 42, 10, };
			self.defaultScaleIndex = 0;
			self.curScaleIndex = self.defaultScaleIndex;

			float curScale = ET.Client.ARSessionHelper.GetScaleAR(self.DomainScene());
			for (int i = 0; i < self.scaleSettingList.Count; i++)
			{
				if (self.scaleSettingList[i] == curScale)
				{
					self.curScaleIndex = i;
					break;
				}
			}

			self.ChgScale(self.curScaleIndex);

		}

		public static void Update(this DlgARSceneSliderSimple self)
		{
			self.ShowPrefab().Coroutine();
		}

		public static float GetSceneScale(this DlgARSceneSliderSimple self)
		{
			return self.scaleSettingList[self.curScaleIndex];
		}

		public static void SetCurChooseIndex(this DlgARSceneSliderSimple self, int curScaleIndex)
		{
			self.View.E_select1Image.SetVisible(0 == curScaleIndex);
			self.View.E_select2Image.SetVisible(1 == curScaleIndex);
			self.View.E_select3Image.SetVisible(2 == curScaleIndex);
		}

		public static (bool, Vector3) GetScreenRayPos(this DlgARSceneSliderSimple self)
		{
			Vector3 ScreenMidPos = new Vector3(Screen.width / 2, Screen.height / 2, 0);
			Ray OneShotRay = ET.Client.ARSessionHelper.GetMainCamera(self.DomainScene()).ScreenPointToRay(ScreenMidPos);
			if (Physics.Raycast(OneShotRay, out RaycastHit hitInfo)) // 如果射线碰到了物体
			{
				return (true, hitInfo.point);
			}
			return (false, Vector3.zero);
		}

		public static (bool, Vector3) GetRayPos(this DlgARSceneSliderSimple self, Vector3 pos)
		{
			Vector3 rayBegin = pos + new Vector3(0, 0.5f, 0);
			Vector3 direction = rayBegin + new Vector3(0, -10, 0);
			if (Physics.Raycast(rayBegin, direction, out RaycastHit hitInfo, 100))
			{
				return (true, hitInfo.point);
			}
			return (false, Vector3.zero);
		}

		public static async ETTask ShowPrefab(this DlgARSceneSliderSimple self)
		{
			//获取镜头投射点
			(bool bRet, Vector3 cameraRayPos) = self.GetScreenRayPos();
			if (bRet == false)
			{
				Log.Debug("GetScreenRayPos bRet == false");
				return;
			}

			for (int i = 0; i < self.showPrefabCfgList.Count; i++)
			{
				float3 pos = (float3)cameraRayPos + new float3(self.showPrefabCfgList[i].disPos.x, 0, self.showPrefabCfgList[i].disPos.y) / self.GetSceneScale();
				(bool bRet2, Vector3 hitPos) = self.GetRayPos(pos);
				string unitCfgId = self.showPrefabCfgList[i].unitCfgId;

				GameObject go = null;
				if (self.showPrefabList.Count > i)
				{
					go = self.showPrefabList[i];
				}
				else
				{
					go = self.CreateOnePrefab(unitCfgId);
					self.showPrefabList.Add(go);
				}

				if (bRet2)
				{
					go.transform.position = hitPos;
					go.SetActive(true);
				}
				else
				{
					go.SetActive(false);
				}
			}
			self.ChgScale(self.curScaleIndex);
		}

		public static GameObject CreateOnePrefab(this DlgARSceneSliderSimple self, string unitCfgId)
		{
			float resScale = ET.Ability.UnitHelper.GetUnitResScale(unitCfgId);
			string pathName = ET.Ability.UnitHelper.GetResName(unitCfgId);
			GameObject go = ResComponent.Instance.LoadAsset<GameObject>(pathName);
			GameObject goInstance = GameObject.Instantiate(go);
			self.orgPrefabLocalScaleDic.Add(goInstance, resScale);
			goInstance.SetActive(false);
			return goInstance;
		}

		public static async ETTask SetSceneScale1(this DlgARSceneSliderSimple self)
		{
			self.ChgScale(0);
		}

		public static async ETTask SetSceneScale2(this DlgARSceneSliderSimple self)
		{
			self.ChgScale(1);
		}

		public static async ETTask SetSceneScale3(this DlgARSceneSliderSimple self)
		{
			self.ChgScale(2);
		}

		public static void ChgScale(this DlgARSceneSliderSimple self, int curScaleIndex)
		{
			float lastScale = self.curScaleIndex;
			self.curScaleIndex = curScaleIndex;
			float newScale = self.scaleSettingList[self.curScaleIndex];

			foreach (GameObject go in self.showPrefabList)
			{
				go.transform.localScale = Vector3.one / (self.orgPrefabLocalScaleDic[go] * newScale);
			}

			self.SetCurChooseIndex(curScaleIndex);

			if (lastScale != self.curScaleIndex)
			{
				EventSystem.Instance.Publish(self.DomainScene(), new ClientEventType.NoticeEventLogging()
				{
					eventName = "ScalSelected",
					properties = new()
					{
						{"battlefield_scal_num", self.GetSceneScale()},
					}
				});
			}
		}

	}
}
