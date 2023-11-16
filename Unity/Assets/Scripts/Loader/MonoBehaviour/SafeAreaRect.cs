using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	[DisallowMultipleComponent]//用于MonoBehaviour或其子类，不能重复添加这个类的组件，重复添加会弹出对话框
	[RequireComponent(typeof(RectTransform))]
	public class SafeAreaRect: MonoBehaviour
	{
		[NonSerialized]
		private RectTransform _rect;
		protected RectTransform rectTransform
		{
			get
			{
				if (_rect == null)
					_rect = GetComponent<RectTransform>();
				return _rect;
			}
		}

		public static Rect cacheSafeArea = Rect.zero;
		private void Awake()
		{
			if (cacheSafeArea == Rect.zero)
				cacheSafeArea = GetSafeArea();

			ApplySafeArea(cacheSafeArea);
		}

		private Rect __screenSafeArea = Rect.zero;
		private void Update()
		{
			if (__screenSafeArea != Screen.safeArea)
			{
				cacheSafeArea = GetSafeArea();
				ApplySafeArea(cacheSafeArea);
			}
		}

		/// <summary>
		/// 获取当前屏幕的参数
		/// </summary>
		private float[] GetCustomerSceneParam()
		{
			float[] sceneParam = new float[3];
			sceneParam[0] = Screen.width;
			sceneParam[1] = Screen.height;
			sceneParam[2] = Screen.safeArea.x * 0.5f;
			return sceneParam;
		}

		private Rect GetSafeArea()
		{
			float x = 0, y = 0, w = 1, h = 1;
			float[] param = GetCustomerSceneParam();//width、height、异性高度
			var orientation = Screen.orientation;//获取屏幕方向
			if (orientation == ScreenOrientation.Portrait || orientation == ScreenOrientation.PortraitUpsideDown)
			{  //竖屏
				y = param[2] / param[0];
				h = 1 - y;
			}
			else
			{   //横屏
				x = param[2] / param[0];
				w = 1 - x;
			}
			return new Rect(x, y, w, h);
		}

		private void ApplySafeArea(Rect rect)
		{
	#if UNITY_EDITOR
			__screenSafeArea = Screen.safeArea;//安全区域
	#endif
			rectTransform.anchorMin = rect.position;//rect.position的x、y轴最小值(对用的是rect(x,y))
			rectTransform.anchorMax = rect.size;//rect.size也就是x、y轴最大值(对用的是rect(w,h))
		}
	}
}