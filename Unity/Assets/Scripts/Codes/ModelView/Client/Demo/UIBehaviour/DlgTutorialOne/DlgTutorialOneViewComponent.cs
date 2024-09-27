
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgTutorialOne))]
	[EnableMethod]
	public class DlgTutorialOneViewComponent : Entity, IAwake, IDestroy
	{
		public UnityEngine.UI.Button E_BG_ClickButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_BG_ClickButton == null )
				{
					this.m_E_BG_ClickButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_BG_Click");
				}
				return this.m_E_BG_ClickButton;
			}
		}

		public UnityEngine.UI.Image E_BG_ClickImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_BG_ClickImage == null )
				{
					this.m_E_BG_ClickImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_BG_Click");
				}
				return this.m_E_BG_ClickImage;
			}
		}

		public UnityEngine.RectTransform EG_bgRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_bgRectTransform == null )
				{
					this.m_EG_bgRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "E_BG_Click/EG_bg");
				}
				return this.m_EG_bgRectTransform;
			}
		}

		public UnityEngine.UI.Image EG_bgImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_bgImage == null )
				{
					this.m_EG_bgImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_BG_Click/EG_bg");
				}
				return this.m_EG_bgImage;
			}
		}

		public UnityEngine.UI.Image E_videoImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_videoImage == null )
				{
					this.m_E_videoImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "Tutorials/E_Tutorials/E_video");
				}
				return this.m_E_videoImage;
			}
		}

		public UnityEngine.Video.VideoPlayer E_videoVideoPlayer
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_videoVideoPlayer == null )
				{
					this.m_E_videoVideoPlayer = UIFindHelper.FindDeepChild<UnityEngine.Video.VideoPlayer>(this.uiTransform.gameObject, "Tutorials/E_Tutorials/E_video");
				}
				return this.m_E_videoVideoPlayer;
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
					this.m_E_VideoShowRawImage = UIFindHelper.FindDeepChild<UnityEngine.UI.RawImage>(this.uiTransform.gameObject, "Tutorials/E_Tutorials/E_video/video/VideoShow/mask/E_VideoShow");
				}
				return this.m_E_VideoShowRawImage;
			}
		}

		public UnityEngine.UI.Button EButton_VideoButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_VideoButton == null )
				{
					this.m_EButton_VideoButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "Tutorials/E_Tutorials/E_video/video/VideoShow/mask/E_VideoShow/EButton_Video");
				}
				return this.m_EButton_VideoButton;
			}
		}

		public UnityEngine.UI.Image EButton_VideoImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_VideoImage == null )
				{
					this.m_EButton_VideoImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "Tutorials/E_Tutorials/E_video/video/VideoShow/mask/E_VideoShow/EButton_Video");
				}
				return this.m_EButton_VideoImage;
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
					this.m_E_SliderSlider = UIFindHelper.FindDeepChild<UnityEngine.UI.Slider>(this.uiTransform.gameObject, "Tutorials/E_Tutorials/E_video/video/VideoShow/mask/E_VideoShow/E_Slider");
				}
				return this.m_E_SliderSlider;
			}
		}

		public UnityEngine.UI.Slider E_Slider01Slider
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_Slider01Slider == null )
				{
					this.m_E_Slider01Slider = UIFindHelper.FindDeepChild<UnityEngine.UI.Slider>(this.uiTransform.gameObject, "Tutorials/E_Tutorials/E_Slider01");
				}
				return this.m_E_Slider01Slider;
			}
		}

		public UnityEngine.UI.Button EButton_Pause01Button
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_Pause01Button == null )
				{
					this.m_EButton_Pause01Button = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "Tutorials/E_Tutorials/EButton_Pause01");
				}
				return this.m_EButton_Pause01Button;
			}
		}

		public UnityEngine.UI.Image EButton_Pause01Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_Pause01Image == null )
				{
					this.m_EButton_Pause01Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "Tutorials/E_Tutorials/EButton_Pause01");
				}
				return this.m_EButton_Pause01Image;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_PauseTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_PauseTextMeshProUGUI == null )
				{
					this.m_ELabel_PauseTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "Tutorials/E_Tutorials/EButton_Pause01/ELabel_Pause");
				}
				return this.m_ELabel_PauseTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Button EButton_PauseButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_PauseButton == null )
				{
					this.m_EButton_PauseButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "Tutorials/E_Tutorials/EButton_Pause");
				}
				return this.m_EButton_PauseButton;
			}
		}

		public UnityEngine.UI.Image EButton_PauseImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_PauseImage == null )
				{
					this.m_EButton_PauseImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "Tutorials/E_Tutorials/EButton_Pause");
				}
				return this.m_EButton_PauseImage;
			}
		}

		public UnityEngine.UI.Image E_Image_PlayImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_Image_PlayImage == null )
				{
					this.m_E_Image_PlayImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "Tutorials/E_Tutorials/EButton_Pause/E_Image_Play");
				}
				return this.m_E_Image_PlayImage;
			}
		}

		public UnityEngine.UI.Image E_Image_PauseImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_Image_PauseImage == null )
				{
					this.m_E_Image_PauseImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "Tutorials/E_Tutorials/EButton_Pause/E_Image_Pause");
				}
				return this.m_E_Image_PauseImage;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_TitleTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_TitleTextMeshProUGUI == null )
				{
					this.m_ELabel_TitleTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "Tutorials/E_Tutorials/ELabel_Title");
				}
				return this.m_ELabel_TitleTextMeshProUGUI;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_InfoTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_InfoTextMeshProUGUI == null )
				{
					this.m_ELabel_InfoTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "Tutorials/E_Tutorials/ELabel_Info");
				}
				return this.m_ELabel_InfoTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Button E_QuitBattleButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_QuitBattleButton == null )
				{
					this.m_E_QuitBattleButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "Tutorials/E_QuitBattle");
				}
				return this.m_E_QuitBattleButton;
			}
		}

		public UnityEngine.UI.Image E_QuitBattleImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_QuitBattleImage == null )
				{
					this.m_E_QuitBattleImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "Tutorials/E_QuitBattle");
				}
				return this.m_E_QuitBattleImage;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_TextBackTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_TextBackTextMeshProUGUI == null )
				{
					this.m_ELabel_TextBackTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "Tutorials/E_QuitBattle/ELabel_TextBack");
				}
				return this.m_ELabel_TextBackTextMeshProUGUI;
			}
		}

		public UITextLocalizeMonoView ELabel_TextBackUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_TextBackUITextLocalizeMonoView == null )
				{
					this.m_ELabel_TextBackUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "Tutorials/E_QuitBattle/ELabel_TextBack");
				}
				return this.m_ELabel_TextBackUITextLocalizeMonoView;
			}
		}

		public void DestroyWidget()
		{
			this.m_E_BG_ClickButton = null;
			this.m_E_BG_ClickImage = null;
			this.m_EG_bgRectTransform = null;
			this.m_EG_bgImage = null;
			this.m_E_videoImage = null;
			this.m_E_videoVideoPlayer = null;
			this.m_E_VideoShowRawImage = null;
			this.m_EButton_VideoButton = null;
			this.m_EButton_VideoImage = null;
			this.m_E_SliderSlider = null;
			this.m_E_Slider01Slider = null;
			this.m_EButton_Pause01Button = null;
			this.m_EButton_Pause01Image = null;
			this.m_ELabel_PauseTextMeshProUGUI = null;
			this.m_EButton_PauseButton = null;
			this.m_EButton_PauseImage = null;
			this.m_E_Image_PlayImage = null;
			this.m_E_Image_PauseImage = null;
			this.m_ELabel_TitleTextMeshProUGUI = null;
			this.m_ELabel_InfoTextMeshProUGUI = null;
			this.m_E_QuitBattleButton = null;
			this.m_E_QuitBattleImage = null;
			this.m_ELabel_TextBackTextMeshProUGUI = null;
			this.m_ELabel_TextBackUITextLocalizeMonoView = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_BG_ClickButton = null;
		private UnityEngine.UI.Image m_E_BG_ClickImage = null;
		private UnityEngine.RectTransform m_EG_bgRectTransform = null;
		private UnityEngine.UI.Image m_EG_bgImage = null;
		private UnityEngine.UI.Image m_E_videoImage = null;
		private UnityEngine.Video.VideoPlayer m_E_videoVideoPlayer = null;
		private UnityEngine.UI.RawImage m_E_VideoShowRawImage = null;
		private UnityEngine.UI.Button m_EButton_VideoButton = null;
		private UnityEngine.UI.Image m_EButton_VideoImage = null;
		private UnityEngine.UI.Slider m_E_SliderSlider = null;
		private UnityEngine.UI.Slider m_E_Slider01Slider = null;
		private UnityEngine.UI.Button m_EButton_Pause01Button = null;
		private UnityEngine.UI.Image m_EButton_Pause01Image = null;
		private TMPro.TextMeshProUGUI m_ELabel_PauseTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_EButton_PauseButton = null;
		private UnityEngine.UI.Image m_EButton_PauseImage = null;
		private UnityEngine.UI.Image m_E_Image_PlayImage = null;
		private UnityEngine.UI.Image m_E_Image_PauseImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_TitleTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_ELabel_InfoTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_E_QuitBattleButton = null;
		private UnityEngine.UI.Image m_E_QuitBattleImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_TextBackTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_ELabel_TextBackUITextLocalizeMonoView = null;
		public Transform uiTransform = null;
	}
}
