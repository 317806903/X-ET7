
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgARSceneSliderSimple))]
	[EnableMethod]
	public class DlgARSceneSliderSimpleViewComponent : Entity, IAwake, IDestroy
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

		public UnityEngine.UI.Button EButton_1Button
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_1Button == null )
				{
					this.m_EButton_1Button = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_Slider/EButton_1");
				}
				return this.m_EButton_1Button;
			}
		}

		public UnityEngine.UI.Image EButton_1Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_1Image == null )
				{
					this.m_EButton_1Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Slider/EButton_1");
				}
				return this.m_EButton_1Image;
			}
		}

		public UnityEngine.UI.Image E_select1Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_select1Image == null )
				{
					this.m_E_select1Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Slider/EButton_1/button/E_select1");
				}
				return this.m_E_select1Image;
			}
		}

		public UnityEngine.UI.Button EButton_2Button
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_2Button == null )
				{
					this.m_EButton_2Button = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_Slider/EButton_2");
				}
				return this.m_EButton_2Button;
			}
		}

		public UnityEngine.UI.Image EButton_2Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_2Image == null )
				{
					this.m_EButton_2Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Slider/EButton_2");
				}
				return this.m_EButton_2Image;
			}
		}

		public UnityEngine.UI.Image E_select2Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_select2Image == null )
				{
					this.m_E_select2Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Slider/EButton_2/button/E_select2");
				}
				return this.m_E_select2Image;
			}
		}

		public UnityEngine.UI.Button EButton_3Button
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_3Button == null )
				{
					this.m_EButton_3Button = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_Slider/EButton_3");
				}
				return this.m_EButton_3Button;
			}
		}

		public UnityEngine.UI.Image EButton_3Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_3Image == null )
				{
					this.m_EButton_3Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Slider/EButton_3");
				}
				return this.m_EButton_3Image;
			}
		}

		public UnityEngine.UI.Image E_select3Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_select3Image == null )
				{
					this.m_E_select3Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Slider/EButton_3/button/E_select3");
				}
				return this.m_E_select3Image;
			}
		}

		public void DestroyWidget()
		{
			this.m_E_SliderImage = null;
			this.m_EButton_1Button = null;
			this.m_EButton_1Image = null;
			this.m_E_select1Image = null;
			this.m_EButton_2Button = null;
			this.m_EButton_2Image = null;
			this.m_E_select2Image = null;
			this.m_EButton_3Button = null;
			this.m_EButton_3Image = null;
			this.m_E_select3Image = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Image m_E_SliderImage = null;
		private UnityEngine.UI.Button m_EButton_1Button = null;
		private UnityEngine.UI.Image m_EButton_1Image = null;
		private UnityEngine.UI.Image m_E_select1Image = null;
		private UnityEngine.UI.Button m_EButton_2Button = null;
		private UnityEngine.UI.Image m_EButton_2Image = null;
		private UnityEngine.UI.Image m_E_select2Image = null;
		private UnityEngine.UI.Button m_EButton_3Button = null;
		private UnityEngine.UI.Image m_EButton_3Image = null;
		private UnityEngine.UI.Image m_E_select3Image = null;
		public Transform uiTransform = null;
	}
}
