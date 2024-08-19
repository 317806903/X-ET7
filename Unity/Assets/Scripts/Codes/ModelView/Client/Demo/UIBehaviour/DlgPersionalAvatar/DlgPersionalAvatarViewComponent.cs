
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgPersionalAvatar))]
	[EnableMethod]
	public class DlgPersionalAvatarViewComponent : Entity, IAwake, IDestroy
	{
		public UnityEngine.RectTransform EGBackGroundRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EGBackGroundRectTransform == null )
				{
					this.m_EGBackGroundRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround");
				}
				return this.m_EGBackGroundRectTransform;
			}
		}

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
					this.m_E_BG_ClickButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/E_BG_Click");
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
					this.m_E_BG_ClickImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/E_BG_Click");
				}
				return this.m_E_BG_ClickImage;
			}
		}

		public UnityEngine.RectTransform EG_bgARRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_bgARRectTransform == null )
				{
					this.m_EG_bgARRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround/E_BG_Click/EG_bgAR");
				}
				return this.m_EG_bgARRectTransform;
			}
		}

		public BlurBackground.TranslucentImage EG_bgARTranslucentImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_bgARTranslucentImage == null )
				{
					this.m_EG_bgARTranslucentImage = UIFindHelper.FindDeepChild<BlurBackground.TranslucentImage>(this.uiTransform.gameObject, "EGBackGround/E_BG_Click/EG_bgAR");
				}
				return this.m_EG_bgARTranslucentImage;
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
					this.m_EG_bgRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround/E_BG_Click/EG_bg");
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
					this.m_EG_bgImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/E_BG_Click/EG_bg");
				}
				return this.m_EG_bgImage;
			}
		}

		public UnityEngine.UI.LoopHorizontalScrollRect ELoopScrollList_AvatarLoopHorizontalScrollRect
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELoopScrollList_AvatarLoopHorizontalScrollRect == null )
				{
					this.m_ELoopScrollList_AvatarLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject, "EGBackGround/Root/E_info/Avatar/ELoopScrollList_Avatar");
				}
				return this.m_ELoopScrollList_AvatarLoopHorizontalScrollRect;
			}
		}

		public UnityEngine.UI.LoopHorizontalScrollRect ELoopScrollList_FrameLoopHorizontalScrollRect
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELoopScrollList_FrameLoopHorizontalScrollRect == null )
				{
					this.m_ELoopScrollList_FrameLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject, "EGBackGround/Root/E_info/Frame/ELoopScrollList_Frame");
				}
				return this.m_ELoopScrollList_FrameLoopHorizontalScrollRect;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_NoFrameTipTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_NoFrameTipTextMeshProUGUI == null )
				{
					this.m_ELabel_NoFrameTipTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/Root/E_info/Frame/ELabel_NoFrameTip");
				}
				return this.m_ELabel_NoFrameTipTextMeshProUGUI;
			}
		}

		public UnityEngine.RectTransform EG_titleRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_titleRectTransform == null )
				{
					this.m_EG_titleRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround/Root/EG_title");
				}
				return this.m_EG_titleRectTransform;
			}
		}

		public UnityEngine.UI.Image EG_titleImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_titleImage == null )
				{
					this.m_EG_titleImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/EG_title");
				}
				return this.m_EG_titleImage;
			}
		}

		public UnityEngine.UI.Button E_SaveButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_SaveButton == null )
				{
					this.m_E_SaveButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/Root/E_Save  ");
				}
				return this.m_E_SaveButton;
			}
		}

		public UnityEngine.UI.Image E_SaveImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_SaveImage == null )
				{
					this.m_E_SaveImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/E_Save  ");
				}
				return this.m_E_SaveImage;
			}
		}

		public UnityEngine.UI.Image EImageCurrentBkImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EImageCurrentBkImage == null )
				{
					this.m_EImageCurrentBkImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/EImageCurrentBk");
				}
				return this.m_EImageCurrentBkImage;
			}
		}

		public ES_AvatarShow ES_AvatarShow
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_es_avatarshow == null )
				{
					Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject, "EGBackGround/Root/EImageCurrentBk/ES_AvatarShow");
					this.m_es_avatarshow = this.AddChild<ES_AvatarShow, Transform>(subTrans);
				}
				return this.m_es_avatarshow;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_CurrentTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_CurrentTextMeshProUGUI == null )
				{
					this.m_ELabel_CurrentTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/Root/EImageCurrentBk/ELabel_Current");
				}
				return this.m_ELabel_CurrentTextMeshProUGUI;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_PreviewTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_PreviewTextMeshProUGUI == null )
				{
					this.m_ELabel_PreviewTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/Root/EImageCurrentBk/ELabel_Preview");
				}
				return this.m_ELabel_PreviewTextMeshProUGUI;
			}
		}

		public TMPro.TextMeshProUGUI ELabelDesFrameTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabelDesFrameTextMeshProUGUI == null )
				{
					this.m_ELabelDesFrameTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/Root/EImageCurrentBk/ELabelDesFrame");
				}
				return this.m_ELabelDesFrameTextMeshProUGUI;
			}
		}

		public TMPro.TextMeshProUGUI ELabelDesNOFrameTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabelDesNOFrameTextMeshProUGUI == null )
				{
					this.m_ELabelDesNOFrameTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/Root/EImageCurrentBk/ELabelDesNOFrame");
				}
				return this.m_ELabelDesNOFrameTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Button E_BtnCloseButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_BtnCloseButton == null )
				{
					this.m_E_BtnCloseButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/E_BtnClose");
				}
				return this.m_E_BtnCloseButton;
			}
		}

		public UnityEngine.UI.Image E_BtnCloseImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_BtnCloseImage == null )
				{
					this.m_E_BtnCloseImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/E_BtnClose");
				}
				return this.m_E_BtnCloseImage;
			}
		}

		public void DestroyWidget()
		{
			this.m_EGBackGroundRectTransform = null;
			this.m_E_BG_ClickButton = null;
			this.m_E_BG_ClickImage = null;
			this.m_EG_bgARRectTransform = null;
			this.m_EG_bgARTranslucentImage = null;
			this.m_EG_bgRectTransform = null;
			this.m_EG_bgImage = null;
			this.m_ELoopScrollList_AvatarLoopHorizontalScrollRect = null;
			this.m_ELoopScrollList_FrameLoopHorizontalScrollRect = null;
			this.m_ELabel_NoFrameTipTextMeshProUGUI = null;
			this.m_EG_titleRectTransform = null;
			this.m_EG_titleImage = null;
			this.m_E_SaveButton = null;
			this.m_E_SaveImage = null;
			this.m_EImageCurrentBkImage = null;
			this.m_es_avatarshow?.Dispose();
			this.m_es_avatarshow = null;
			this.m_ELabel_CurrentTextMeshProUGUI = null;
			this.m_ELabel_PreviewTextMeshProUGUI = null;
			this.m_ELabelDesFrameTextMeshProUGUI = null;
			this.m_ELabelDesNOFrameTextMeshProUGUI = null;
			this.m_E_BtnCloseButton = null;
			this.m_E_BtnCloseImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.RectTransform m_EGBackGroundRectTransform = null;
		private UnityEngine.UI.Button m_E_BG_ClickButton = null;
		private UnityEngine.UI.Image m_E_BG_ClickImage = null;
		private UnityEngine.RectTransform m_EG_bgARRectTransform = null;
		private BlurBackground.TranslucentImage m_EG_bgARTranslucentImage = null;
		private UnityEngine.RectTransform m_EG_bgRectTransform = null;
		private UnityEngine.UI.Image m_EG_bgImage = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopScrollList_AvatarLoopHorizontalScrollRect = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopScrollList_FrameLoopHorizontalScrollRect = null;
		private TMPro.TextMeshProUGUI m_ELabel_NoFrameTipTextMeshProUGUI = null;
		private UnityEngine.RectTransform m_EG_titleRectTransform = null;
		private UnityEngine.UI.Image m_EG_titleImage = null;
		private UnityEngine.UI.Button m_E_SaveButton = null;
		private UnityEngine.UI.Image m_E_SaveImage = null;
		private UnityEngine.UI.Image m_EImageCurrentBkImage = null;
		private ES_AvatarShow m_es_avatarshow = null;
		private TMPro.TextMeshProUGUI m_ELabel_CurrentTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_ELabel_PreviewTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_ELabelDesFrameTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_ELabelDesNOFrameTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_E_BtnCloseButton = null;
		private UnityEngine.UI.Image m_E_BtnCloseImage = null;
		public Transform uiTransform = null;
	}
}
