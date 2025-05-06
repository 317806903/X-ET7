
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgBeginnersGuideStory))]
	[EnableMethod]
	public class DlgBeginnersGuideStoryViewComponent : Entity, IAwake, IDestroy
	{
		public UnityEngine.UI.Image E_ImgBGImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ImgBGImage == null )
				{
					this.m_E_ImgBGImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_ImgBG");
				}
				return this.m_E_ImgBGImage;
			}
		}

		public UnityEngine.UI.Button E_BG1Button
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_BG1Button == null )
				{
					this.m_E_BG1Button = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "story1/next/E_BG1");
				}
				return this.m_E_BG1Button;
			}
		}

		public UnityEngine.UI.Image E_BG1Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_BG1Image == null )
				{
					this.m_E_BG1Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "story1/next/E_BG1");
				}
				return this.m_E_BG1Image;
			}
		}

		public UnityEngine.UI.Button E_BG2Button
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_BG2Button == null )
				{
					this.m_E_BG2Button = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "story2/next/E_BG2");
				}
				return this.m_E_BG2Button;
			}
		}

		public UnityEngine.UI.Image E_BG2Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_BG2Image == null )
				{
					this.m_E_BG2Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "story2/next/E_BG2");
				}
				return this.m_E_BG2Image;
			}
		}

		public UnityEngine.UI.Button E_BG3Button
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_BG3Button == null )
				{
					this.m_E_BG3Button = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "story3/next/E_BG3");
				}
				return this.m_E_BG3Button;
			}
		}

		public UnityEngine.UI.Image E_BG3Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_BG3Image == null )
				{
					this.m_E_BG3Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "story3/next/E_BG3");
				}
				return this.m_E_BG3Image;
			}
		}

		public UnityEngine.UI.Button E_BG4Button
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_BG4Button == null )
				{
					this.m_E_BG4Button = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "story4/next/E_BG4");
				}
				return this.m_E_BG4Button;
			}
		}

		public UnityEngine.UI.Image E_BG4Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_BG4Image == null )
				{
					this.m_E_BG4Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "story4/next/E_BG4");
				}
				return this.m_E_BG4Image;
			}
		}

		public UnityEngine.UI.Button E_VideoImgButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_VideoImgButton == null )
				{
					this.m_E_VideoImgButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "story5/E_VideoImg");
				}
				return this.m_E_VideoImgButton;
			}
		}

		public UnityEngine.UI.RawImage E_VideoImgRawImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_VideoImgRawImage == null )
				{
					this.m_E_VideoImgRawImage = UIFindHelper.FindDeepChild<UnityEngine.UI.RawImage>(this.uiTransform.gameObject, "story5/E_VideoImg");
				}
				return this.m_E_VideoImgRawImage;
			}
		}

		public UnityEngine.UI.Button E_BG5Button
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_BG5Button == null )
				{
					this.m_E_BG5Button = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "story5/next/E_BG5");
				}
				return this.m_E_BG5Button;
			}
		}

		public UnityEngine.UI.Image E_BG5Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_BG5Image == null )
				{
					this.m_E_BG5Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "story5/next/E_BG5");
				}
				return this.m_E_BG5Image;
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

		public UnityEngine.UI.Button E_SKIPTUTORIALButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_SKIPTUTORIALButton == null )
				{
					this.m_E_SKIPTUTORIALButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_SKIPTUTORIAL");
				}
				return this.m_E_SKIPTUTORIALButton;
			}
		}

		public UnityEngine.UI.Image E_SKIPTUTORIALImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_SKIPTUTORIALImage == null )
				{
					this.m_E_SKIPTUTORIALImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_SKIPTUTORIAL");
				}
				return this.m_E_SKIPTUTORIALImage;
			}
		}

		public UnityEngine.RectTransform EG_OpenAnimationRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_OpenAnimationRectTransform == null )
				{
					this.m_EG_OpenAnimationRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_OpenAnimation");
				}
				return this.m_EG_OpenAnimationRectTransform;
			}
		}

		public UnityEngine.RectTransform EG_CloseAnimationRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_CloseAnimationRectTransform == null )
				{
					this.m_EG_CloseAnimationRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_CloseAnimation");
				}
				return this.m_EG_CloseAnimationRectTransform;
			}
		}

		public void DestroyWidget()
		{
			this.m_E_ImgBGImage = null;
			this.m_E_BG1Button = null;
			this.m_E_BG1Image = null;
			this.m_E_BG2Button = null;
			this.m_E_BG2Image = null;
			this.m_E_BG3Button = null;
			this.m_E_BG3Image = null;
			this.m_E_BG4Button = null;
			this.m_E_BG4Image = null;
			this.m_E_VideoImgButton = null;
			this.m_E_VideoImgRawImage = null;
			this.m_E_BG5Button = null;
			this.m_E_BG5Image = null;
			this.m_E_ReturnLoginButton = null;
			this.m_E_ReturnLoginImage = null;
			this.m_E_SKIPTUTORIALButton = null;
			this.m_E_SKIPTUTORIALImage = null;
			this.m_EG_OpenAnimationRectTransform = null;
			this.m_EG_CloseAnimationRectTransform = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Image m_E_ImgBGImage = null;
		private UnityEngine.UI.Button m_E_BG1Button = null;
		private UnityEngine.UI.Image m_E_BG1Image = null;
		private UnityEngine.UI.Button m_E_BG2Button = null;
		private UnityEngine.UI.Image m_E_BG2Image = null;
		private UnityEngine.UI.Button m_E_BG3Button = null;
		private UnityEngine.UI.Image m_E_BG3Image = null;
		private UnityEngine.UI.Button m_E_BG4Button = null;
		private UnityEngine.UI.Image m_E_BG4Image = null;
		private UnityEngine.UI.Button m_E_VideoImgButton = null;
		private UnityEngine.UI.RawImage m_E_VideoImgRawImage = null;
		private UnityEngine.UI.Button m_E_BG5Button = null;
		private UnityEngine.UI.Image m_E_BG5Image = null;
		private UnityEngine.UI.Button m_E_ReturnLoginButton = null;
		private UnityEngine.UI.Image m_E_ReturnLoginImage = null;
		private UnityEngine.UI.Button m_E_SKIPTUTORIALButton = null;
		private UnityEngine.UI.Image m_E_SKIPTUTORIALImage = null;
		private UnityEngine.RectTransform m_EG_OpenAnimationRectTransform = null;
		private UnityEngine.RectTransform m_EG_CloseAnimationRectTransform = null;
		public Transform uiTransform = null;
	}
}
