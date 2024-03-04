
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgVideoShow))]
	[EnableMethod]
	public class DlgVideoShowViewComponent : Entity, IAwake, IDestroy
	{
		public UnityEngine.UI.Button E_VideoShowButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_VideoShowButton == null )
				{
					this.m_E_VideoShowButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "Sprite_BackGround/E_VideoShow");
				}
				return this.m_E_VideoShowButton;
			}
		}

		public UnityEngine.UI.RawImage E_VideoShowRawImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_VideoShowRawImage == null )
				{
					this.m_E_VideoShowRawImage = UIFindHelper.FindDeepChild<UnityEngine.UI.RawImage>(this.uiTransform.gameObject, "Sprite_BackGround/E_VideoShow");
				}
				return this.m_E_VideoShowRawImage;
			}
		}

		public UnityEngine.UI.Image E_bgImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_bgImage == null )
				{
					this.m_E_bgImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "Sprite_BackGround/E_bg");
				}
				return this.m_E_bgImage;
			}
		}

		public TMPro.TextMeshProUGUI E_TextContextTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_TextContextTextMeshProUGUI == null )
				{
					this.m_E_TextContextTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "Sprite_BackGround/E_TextContext");
				}
				return this.m_E_TextContextTextMeshProUGUI;
			}
		}

		public UITextLocalizeMonoView E_TextContextUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_TextContextUITextLocalizeMonoView == null )
				{
					this.m_E_TextContextUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "Sprite_BackGround/E_TextContext");
				}
				return this.m_E_TextContextUITextLocalizeMonoView;
			}
		}

		public UnityEngine.UI.Button E_NextButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_NextButton == null )
				{
					this.m_E_NextButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "Sprite_BackGround/next/E_Next");
				}
				return this.m_E_NextButton;
			}
		}

		public UnityEngine.UI.Image E_NextImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_NextImage == null )
				{
					this.m_E_NextImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "Sprite_BackGround/next/E_Next");
				}
				return this.m_E_NextImage;
			}
		}

		public UnityEngine.UI.Slider E_SliderSlider
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_SliderSlider == null )
				{
					this.m_E_SliderSlider = UIFindHelper.FindDeepChild<UnityEngine.UI.Slider>(this.uiTransform.gameObject, "E_Slider");
				}
				return this.m_E_SliderSlider;
			}
		}

		public UnityEngine.UI.Button E_ReturnLoginButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ReturnLoginButton == null )
				{
					this.m_E_ReturnLoginButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_ReturnLogin");
				}
				return this.m_E_ReturnLoginButton;
			}
		}

		public UnityEngine.UI.Image E_ReturnLoginImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ReturnLoginImage == null )
				{
					this.m_E_ReturnLoginImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_ReturnLogin");
				}
				return this.m_E_ReturnLoginImage;
			}
		}

		public void DestroyWidget()
		{
			this.m_E_VideoShowButton = null;
			this.m_E_VideoShowRawImage = null;
			this.m_E_bgImage = null;
			this.m_E_TextContextTextMeshProUGUI = null;
			this.m_E_TextContextUITextLocalizeMonoView = null;
			this.m_E_NextButton = null;
			this.m_E_NextImage = null;
			this.m_E_SliderSlider = null;
			this.m_E_ReturnLoginButton = null;
			this.m_E_ReturnLoginImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_VideoShowButton = null;
		private UnityEngine.UI.RawImage m_E_VideoShowRawImage = null;
		private UnityEngine.UI.Image m_E_bgImage = null;
		private TMPro.TextMeshProUGUI m_E_TextContextTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_E_TextContextUITextLocalizeMonoView = null;
		private UnityEngine.UI.Button m_E_NextButton = null;
		private UnityEngine.UI.Image m_E_NextImage = null;
		private UnityEngine.UI.Slider m_E_SliderSlider = null;
		private UnityEngine.UI.Button m_E_ReturnLoginButton = null;
		private UnityEngine.UI.Image m_E_ReturnLoginImage = null;
		public Transform uiTransform = null;
	}
}
