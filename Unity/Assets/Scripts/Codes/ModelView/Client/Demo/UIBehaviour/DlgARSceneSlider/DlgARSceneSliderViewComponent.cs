
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgARSceneSlider))]
	[EnableMethod]
	public class DlgARSceneSliderViewComponent : Entity, IAwake, IDestroy
	{
		public UnityEngine.UI.Image E_SliderImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_SliderImage == null )
				{
					this.m_E_SliderImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Slider");
				}
				return this.m_E_SliderImage;
			}
		}

		public UnityEngine.UI.Slider E_SliderSceneScaleSlider
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_SliderSceneScaleSlider == null )
				{
					this.m_E_SliderSceneScaleSlider = UIFindHelper.FindDeepChild<UnityEngine.UI.Slider>(this.uiTransform.gameObject, "E_Slider/E_SliderSceneScale");
				}
				return this.m_E_SliderSceneScaleSlider;
			}
		}

		public UnityEngine.UI.Button E_icon_addButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_icon_addButton == null )
				{
					this.m_E_icon_addButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_Slider/E_icon_add");
				}
				return this.m_E_icon_addButton;
			}
		}

		public UnityEngine.UI.Image E_icon_addImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_icon_addImage == null )
				{
					this.m_E_icon_addImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Slider/E_icon_add");
				}
				return this.m_E_icon_addImage;
			}
		}

		public UnityEngine.UI.Button E_icon_reduceButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_icon_reduceButton == null )
				{
					this.m_E_icon_reduceButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_Slider/E_icon_reduce");
				}
				return this.m_E_icon_reduceButton;
			}
		}

		public UnityEngine.UI.Image E_icon_reduceImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_icon_reduceImage == null )
				{
					this.m_E_icon_reduceImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Slider/E_icon_reduce");
				}
				return this.m_E_icon_reduceImage;
			}
		}

		public TMPro.TextMeshProUGUI E_titleTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_titleTextMeshProUGUI == null )
				{
					this.m_E_titleTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_Slider/E_title");
				}
				return this.m_E_titleTextMeshProUGUI;
			}
		}

		public UITextLocalizeMonoView E_titleUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_titleUITextLocalizeMonoView == null )
				{
					this.m_E_titleUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "E_Slider/E_title");
				}
				return this.m_E_titleUITextLocalizeMonoView;
			}
		}

		public void DestroyWidget()
		{
			this.m_E_SliderImage = null;
			this.m_E_SliderSceneScaleSlider = null;
			this.m_E_icon_addButton = null;
			this.m_E_icon_addImage = null;
			this.m_E_icon_reduceButton = null;
			this.m_E_icon_reduceImage = null;
			this.m_E_titleTextMeshProUGUI = null;
			this.m_E_titleUITextLocalizeMonoView = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Image m_E_SliderImage = null;
		private UnityEngine.UI.Slider m_E_SliderSceneScaleSlider = null;
		private UnityEngine.UI.Button m_E_icon_addButton = null;
		private UnityEngine.UI.Image m_E_icon_addImage = null;
		private UnityEngine.UI.Button m_E_icon_reduceButton = null;
		private UnityEngine.UI.Image m_E_icon_reduceImage = null;
		private TMPro.TextMeshProUGUI m_E_titleTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_E_titleUITextLocalizeMonoView = null;
		public Transform uiTransform = null;
	}
}
