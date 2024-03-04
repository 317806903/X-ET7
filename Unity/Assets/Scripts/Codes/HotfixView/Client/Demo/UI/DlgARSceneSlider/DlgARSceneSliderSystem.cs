using System.Collections;
using System.Collections.Generic;
using System;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[Invoke(TimerInvokeType.DlgARSceneSliderFrameTimer)]
	public class DlgARSceneSliderTimer: ATimer<DlgARSceneSlider>
	{
		protected override void Run(DlgARSceneSlider self)
		{
			try
			{
				self.Update();
			}
			catch (Exception e)
			{
				Log.Error($"DlgARSceneSliderTimer timer error: {self.Id}\n{e}");
			}
		}
	}

	[FriendOf(typeof(DlgARSceneSlider))]
	public static class DlgARSceneSliderSystem
	{
		public static void RegisterUIEvent(this DlgARSceneSlider self)
		{
			self.View.E_icon_addButton.AddListenerAsync(self.DoAddSceneScale);
			self.View.E_icon_reduceButton.AddListenerAsync(self.DoReduceSceneScale);
			self.View.E_SliderSceneScaleSlider.AddListener(self.OnSceneScaleSider);
		}

		public static void ShowWindow(this DlgARSceneSlider self, ShowWindowData contextData = null)
		{
			self.SetSceneScaleInfo();

			self.Timer = TimerComponent.Instance.NewFrameTimer(TimerInvokeType.DlgARSceneSliderFrameTimer, self);
		}

		public static void HideWindow(this DlgARSceneSlider self)
		{
			TimerComponent.Instance?.Remove(ref self.Timer);
			foreach (var go in self.showPrefabList)
			{
				GameObject.Destroy(go);
			}
			self.showPrefabList.Clear();
		}

		public static void SetSceneScaleInfo(this DlgARSceneSlider self)
		{
			self.minScale = 10;
			self.maxScale = 75;

			self.defaultScale = 35;
			self.curScale = ET.Client.ARSessionHelper.GetScaleAR(self.DomainScene());
			Log.Debug($"SetSceneScaleInfo self.curScale[{self.curScale}]");
			if (self.curScale == 0)
			{
				self.curScale = self.defaultScale;
			}
			self.curScale = math.clamp(self.curScale, self.minScale, self.maxScale);

			self.View.E_SliderSceneScaleSlider.SetValueWithoutNotify((self.maxScale - self.curScale) / (self.maxScale - self.minScale));

			self.showPrefabCfgList = new()
			{
				(new float2(0, 0), "Unit_HeadQuarterPreview")

			};
			self.showPrefabList = new();
		}

		public static void Update(this DlgARSceneSlider self)
		{
			self.ShowPrefab().Coroutine();
		}

		public static float GetSceneScale(this DlgARSceneSlider self)
		{
			return self.curScale;
		}

		public static (bool, Vector3) GetScreenRayPos(this DlgARSceneSlider self)
		{
			Vector3 ScreenMidPos = new Vector3(Screen.width / 2, Screen.height / 2, 0);
			Ray OneShotRay = ET.Client.ARSessionHelper.GetMainCamera(self.DomainScene()).ScreenPointToRay(ScreenMidPos);
			if (Physics.Raycast(OneShotRay, out RaycastHit hitInfo)) // 如果射线碰到了物体
			{
				return (true, hitInfo.point);
			}
			return (false, Vector3.zero);
		}

		public static (bool, Vector3) GetRayPos(this DlgARSceneSlider self, Vector3 pos)
		{
			Vector3 rayBegin = pos + new Vector3(0, 0.5f, 0);
			Vector3 direction = rayBegin + new Vector3(0, -10, 0);
			if (Physics.Raycast(rayBegin, direction, out RaycastHit hitInfo, 100))
			{
				return (true, hitInfo.point);
			}
			return (false, Vector3.zero);
		}

		public static async ETTask ShowPrefab(this DlgARSceneSlider self)
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
				float3 pos = (float3)cameraRayPos + new float3(self.showPrefabCfgList[i].disPos.x, 0, self.showPrefabCfgList[i].disPos.y) / self.curScale;
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
		}

		public static GameObject CreateOnePrefab(this DlgARSceneSlider self, string unitCfgId)
		{
			UnitCfg unitCfg = UnitCfgCategory.Instance.Get(unitCfgId);
			float resScale = unitCfg.ResScale;
			ResUnitCfg resUnitCfg = ResUnitCfgCategory.Instance.Get(unitCfg.ResId);
			string pathName = resUnitCfg.ResName;
			GameObject go = ResComponent.Instance.LoadAsset<GameObject>(pathName);
			GameObject goInstance = GameObject.Instantiate(go);
			goInstance.transform.localScale = Vector3.one * resScale / self.curScale;
			goInstance.SetActive(false);
			return goInstance;
		}

		public static void OnSceneScaleSider(this DlgARSceneSlider self, float scale)
		{
			float disSider = self.minScale - self.maxScale;

			float disChg = disSider * scale;
			float newScale = math.clamp(self.maxScale + disChg, self.minScale, self.maxScale);

			self.ChgScale(newScale);
		}

		public static async ETTask DoAddSceneScale(this DlgARSceneSlider self)
		{
			float disChg = -5f;
			float newScale = math.clamp(self.curScale + disChg, self.minScale, self.maxScale);

			self.ChgScale(newScale);
		}

		public static async ETTask DoReduceSceneScale(this DlgARSceneSlider self)
		{
			float disChg = 5f;
			float newScale = math.clamp(self.curScale + disChg, self.minScale, self.maxScale);

			self.ChgScale(newScale);
		}

		public static void ChgScale(this DlgARSceneSlider self, float newScale)
		{
			float lastScale = self.curScale;
			self.curScale = newScale;

			if (lastScale == newScale)
			{
				return;
			}
			foreach (GameObject go in self.showPrefabList)
			{
				var goScale = go.transform.localScale;
				go.transform.localScale = goScale * lastScale / newScale;
			}

			self.View.E_SliderSceneScaleSlider.SetValueWithoutNotify((self.maxScale - self.curScale) / (self.maxScale - self.minScale));
		}

	}
}
