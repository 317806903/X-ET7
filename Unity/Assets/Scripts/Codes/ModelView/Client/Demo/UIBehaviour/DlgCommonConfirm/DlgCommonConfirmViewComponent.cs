
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgCommonConfirm))]
	[EnableMethod]
	public class DlgCommonConfirmViewComponent : Entity, IAwake, IDestroy
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

		public UnityEngine.UI.Button EGBackGroundButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EGBackGroundButton == null )
				{
					this.m_EGBackGroundButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround");
				}
				return this.m_EGBackGroundButton;
			}
		}

		public UnityEngine.UI.Image EGBackGroundImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EGBackGroundImage == null )
				{
					this.m_EGBackGroundImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround");
				}
				return this.m_EGBackGroundImage;
			}
		}

		public TMPro.TextMeshProUGUI E_TextTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_TextTextMeshProUGUI == null )
				{
					this.m_E_TextTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/Root/Scroll View/Viewport/Content/E_Text");
				}
				return this.m_E_TextTextMeshProUGUI;
			}
		}

		public UnityEngine.RectTransform EG_ConfirmRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_ConfirmRectTransform == null )
				{
					this.m_EG_ConfirmRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround/Root/EG_Confirm");
				}
				return this.m_EG_ConfirmRectTransform;
			}
		}

		public UnityEngine.UI.Button E_ConfirmCancelButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ConfirmCancelButton == null )
				{
					this.m_E_ConfirmCancelButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/Root/EG_Confirm/E_ConfirmCancel");
				}
				return this.m_E_ConfirmCancelButton;
			}
		}

		public UnityEngine.UI.Image E_ConfirmCancelImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ConfirmCancelImage == null )
				{
					this.m_E_ConfirmCancelImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/EG_Confirm/E_ConfirmCancel");
				}
				return this.m_E_ConfirmCancelImage;
			}
		}

		public UnityEngine.UI.Button E_ConfirmSureButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ConfirmSureButton == null )
				{
					this.m_E_ConfirmSureButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/Root/EG_Confirm/E_ConfirmSure");
				}
				return this.m_E_ConfirmSureButton;
			}
		}

		public UnityEngine.UI.Image E_ConfirmSureImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ConfirmSureImage == null )
				{
					this.m_E_ConfirmSureImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/EG_Confirm/E_ConfirmSure");
				}
				return this.m_E_ConfirmSureImage;
			}
		}

		public UnityEngine.RectTransform EG_SureRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_SureRectTransform == null )
				{
					this.m_EG_SureRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround/Root/EG_Sure");
				}
				return this.m_EG_SureRectTransform;
			}
		}

		public UnityEngine.UI.Button E_SureButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_SureButton == null )
				{
					this.m_E_SureButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/Root/EG_Sure/E_Sure");
				}
				return this.m_E_SureButton;
			}
		}

		public UnityEngine.UI.Image E_SureImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_SureImage == null )
				{
					this.m_E_SureImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/EG_Sure/E_Sure");
				}
				return this.m_E_SureImage;
			}
		}

		public void DestroyWidget()
		{
			this.m_EGBackGroundRectTransform = null;
			this.m_EGBackGroundButton = null;
			this.m_EGBackGroundImage = null;
			this.m_E_TextTextMeshProUGUI = null;
			this.m_EG_ConfirmRectTransform = null;
			this.m_E_ConfirmCancelButton = null;
			this.m_E_ConfirmCancelImage = null;
			this.m_E_ConfirmSureButton = null;
			this.m_E_ConfirmSureImage = null;
			this.m_EG_SureRectTransform = null;
			this.m_E_SureButton = null;
			this.m_E_SureImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.RectTransform m_EGBackGroundRectTransform = null;
		private UnityEngine.UI.Button m_EGBackGroundButton = null;
		private UnityEngine.UI.Image m_EGBackGroundImage = null;
		private TMPro.TextMeshProUGUI m_E_TextTextMeshProUGUI = null;
		private UnityEngine.RectTransform m_EG_ConfirmRectTransform = null;
		private UnityEngine.UI.Button m_E_ConfirmCancelButton = null;
		private UnityEngine.UI.Image m_E_ConfirmCancelImage = null;
		private UnityEngine.UI.Button m_E_ConfirmSureButton = null;
		private UnityEngine.UI.Image m_E_ConfirmSureImage = null;
		private UnityEngine.RectTransform m_EG_SureRectTransform = null;
		private UnityEngine.UI.Button m_E_SureButton = null;
		private UnityEngine.UI.Image m_E_SureImage = null;
		public Transform uiTransform = null;
	}
}
