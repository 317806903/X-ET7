
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgPhysicalStrengthTip))]
	[EnableMethod]
	public class DlgPhysicalStrengthTipViewComponent : Entity, IAwake, IDestroy
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

		public UnityEngine.UI.Button E_BGButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_BGButton == null )
				{
					this.m_E_BGButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/E_BG");
				}
				return this.m_E_BGButton;
			}
		}

		public UnityEngine.UI.Image E_BGImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_BGImage == null )
				{
					this.m_E_BGImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/E_BG");
				}
				return this.m_E_BGImage;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_TakephysicalStrengthTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_TakephysicalStrengthTextMeshProUGUI == null )
				{
					this.m_ELabel_TakephysicalStrengthTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/tips/E_info/ELabel_TakephysicalStrength");
				}
				return this.m_ELabel_TakephysicalStrengthTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Button EButton_Cancel2Button
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_Cancel2Button == null )
				{
					this.m_EButton_Cancel2Button = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/tips/Buton/EButton_Cancel2");
				}
				return this.m_EButton_Cancel2Button;
			}
		}

		public UnityEngine.UI.Image EButton_Cancel2Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_Cancel2Image == null )
				{
					this.m_EButton_Cancel2Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/tips/Buton/EButton_Cancel2");
				}
				return this.m_EButton_Cancel2Image;
			}
		}

		public UnityEngine.UI.Button EButton_SureButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_SureButton == null )
				{
					this.m_EButton_SureButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/tips/Buton/EButton_Sure");
				}
				return this.m_EButton_SureButton;
			}
		}

		public UnityEngine.UI.Image EButton_SureImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_SureImage == null )
				{
					this.m_EButton_SureImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/tips/Buton/EButton_Sure");
				}
				return this.m_EButton_SureImage;
			}
		}

		public UnityEngine.UI.Button EButton_CancelButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_CancelButton == null )
				{
					this.m_EButton_CancelButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/tips/EButton_Cancel");
				}
				return this.m_EButton_CancelButton;
			}
		}

		public UnityEngine.UI.Image EButton_CancelImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_CancelImage == null )
				{
					this.m_EButton_CancelImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/tips/EButton_Cancel");
				}
				return this.m_EButton_CancelImage;
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
					this.m_EG_titleRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround/tips/EG_title");
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
					this.m_EG_titleImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/tips/EG_title");
				}
				return this.m_EG_titleImage;
			}
		}

		public UnityEngine.UI.Button EButton_CloseButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_CloseButton == null )
				{
					this.m_EButton_CloseButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/tips/EG_title/EButton_Close");
				}
				return this.m_EButton_CloseButton;
			}
		}

		public UnityEngine.UI.Image EButton_CloseImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_CloseImage == null )
				{
					this.m_EButton_CloseImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/tips/EG_title/EButton_Close");
				}
				return this.m_EButton_CloseImage;
			}
		}

		public TMPro.TextMeshProUGUI E_TitleTextTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_TitleTextTextMeshProUGUI == null )
				{
					this.m_E_TitleTextTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/tips/EG_title/E_TitleText");
				}
				return this.m_E_TitleTextTextMeshProUGUI;
			}
		}

		public UITextLocalizeMonoView E_TitleTextUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_TitleTextUITextLocalizeMonoView == null )
				{
					this.m_E_TitleTextUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "EGBackGround/tips/EG_title/E_TitleText");
				}
				return this.m_E_TitleTextUITextLocalizeMonoView;
			}
		}

		public void DestroyWidget()
		{
			this.m_EGBackGroundRectTransform = null;
			this.m_E_BGButton = null;
			this.m_E_BGImage = null;
			this.m_ELabel_TakephysicalStrengthTextMeshProUGUI = null;
			this.m_EButton_Cancel2Button = null;
			this.m_EButton_Cancel2Image = null;
			this.m_EButton_SureButton = null;
			this.m_EButton_SureImage = null;
			this.m_EButton_CancelButton = null;
			this.m_EButton_CancelImage = null;
			this.m_EG_titleRectTransform = null;
			this.m_EG_titleImage = null;
			this.m_EButton_CloseButton = null;
			this.m_EButton_CloseImage = null;
			this.m_E_TitleTextTextMeshProUGUI = null;
			this.m_E_TitleTextUITextLocalizeMonoView = null;
			this.uiTransform = null;
		}

		private UnityEngine.RectTransform m_EGBackGroundRectTransform = null;
		private UnityEngine.UI.Button m_E_BGButton = null;
		private UnityEngine.UI.Image m_E_BGImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_TakephysicalStrengthTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_EButton_Cancel2Button = null;
		private UnityEngine.UI.Image m_EButton_Cancel2Image = null;
		private UnityEngine.UI.Button m_EButton_SureButton = null;
		private UnityEngine.UI.Image m_EButton_SureImage = null;
		private UnityEngine.UI.Button m_EButton_CancelButton = null;
		private UnityEngine.UI.Image m_EButton_CancelImage = null;
		private UnityEngine.RectTransform m_EG_titleRectTransform = null;
		private UnityEngine.UI.Image m_EG_titleImage = null;
		private UnityEngine.UI.Button m_EButton_CloseButton = null;
		private UnityEngine.UI.Image m_EButton_CloseImage = null;
		private TMPro.TextMeshProUGUI m_E_TitleTextTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_E_TitleTextUITextLocalizeMonoView = null;
		public Transform uiTransform = null;
	}
}
