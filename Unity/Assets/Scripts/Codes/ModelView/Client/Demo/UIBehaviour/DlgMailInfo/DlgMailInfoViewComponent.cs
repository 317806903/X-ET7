
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgMailInfo))]
	[EnableMethod]
	public class DlgMailInfoViewComponent : Entity, IAwake, IDestroy
	{
		public UnityEngine.RectTransform EG_MailReportRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_MailReportRectTransform == null )
				{
					this.m_EG_MailReportRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_MailReport");
				}
				return this.m_EG_MailReportRectTransform;
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
					this.m_E_BG_ClickButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EG_MailReport/E_BG_Click");
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
					this.m_E_BG_ClickImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_MailReport/E_BG_Click");
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
					this.m_EG_bgARRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_MailReport/E_BG_Click/EG_bgAR");
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
					this.m_EG_bgARTranslucentImage = UIFindHelper.FindDeepChild<BlurBackground.TranslucentImage>(this.uiTransform.gameObject, "EG_MailReport/E_BG_Click/EG_bgAR");
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
					this.m_EG_bgRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_MailReport/E_BG_Click/EG_bg");
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
					this.m_EG_bgImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_MailReport/E_BG_Click/EG_bg");
				}
				return this.m_EG_bgImage;
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
					this.m_EG_titleRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_MailReport/Root/EG_title");
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
					this.m_EG_titleImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_MailReport/Root/EG_title");
				}
				return this.m_EG_titleImage;
			}
		}

		public TMPro.TextMeshProUGUI ETxttitleTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxttitleTextMeshProUGUI == null )
				{
					this.m_ETxttitleTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_MailReport/Root/EG_title/ETxttitle");
				}
				return this.m_ETxttitleTextMeshProUGUI;
			}
		}

		public UnityEngine.RectTransform EG_infoRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_infoRectTransform == null )
				{
					this.m_EG_infoRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_MailReport/Root/EG_info");
				}
				return this.m_EG_infoRectTransform;
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
					Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject, "EG_MailReport/Root/EG_info/ES_AvatarShow");
					this.m_es_avatarshow = this.AddChild<ES_AvatarShow, Transform>(subTrans);
				}
				return this.m_es_avatarshow;
			}
		}

		public TMPro.TextMeshProUGUI ETxtSendDateTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtSendDateTextMeshProUGUI == null )
				{
					this.m_ETxtSendDateTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_MailReport/Root/EG_info/ETxtSendDate");
				}
				return this.m_ETxtSendDateTextMeshProUGUI;
			}
		}

		public TMPro.TextMeshProUGUI ETxtLimintDataTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtLimintDataTextMeshProUGUI == null )
				{
					this.m_ETxtLimintDataTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_MailReport/Root/EG_info/ETxtLimintData");
				}
				return this.m_ETxtLimintDataTextMeshProUGUI;
			}
		}

		public TMPro.TextMeshProUGUI ETxtDetailsTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtDetailsTextMeshProUGUI == null )
				{
					this.m_ETxtDetailsTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_MailReport/Root/EG_info/ETxtDetails");
				}
				return this.m_ETxtDetailsTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Image EMainContentGiftsImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EMainContentGiftsImage == null )
				{
					this.m_EMainContentGiftsImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_MailReport/Root/EMainContentGifts");
				}
				return this.m_EMainContentGiftsImage;
			}
		}

		public UnityEngine.UI.Button E_CollectButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_CollectButton == null )
				{
					this.m_E_CollectButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EG_MailReport/Root/EMainContentGifts/E_Collect");
				}
				return this.m_E_CollectButton;
			}
		}

		public UnityEngine.UI.Image E_CollectImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_CollectImage == null )
				{
					this.m_E_CollectImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_MailReport/Root/EMainContentGifts/E_Collect");
				}
				return this.m_E_CollectImage;
			}
		}

		public UnityEngine.UI.Image E_CollectUnSelectImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_CollectUnSelectImage == null )
				{
					this.m_E_CollectUnSelectImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_MailReport/Root/EMainContentGifts/E_CollectUnSelect");
				}
				return this.m_E_CollectUnSelectImage;
			}
		}

		public UnityEngine.UI.LoopHorizontalScrollRect ELoopScrollList_LoopHorizontalScrollRect
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELoopScrollList_LoopHorizontalScrollRect == null )
				{
					this.m_ELoopScrollList_LoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject, "EG_MailReport/Root/EMainContentGifts/ELoopScrollList_");
				}
				return this.m_ELoopScrollList_LoopHorizontalScrollRect;
			}
		}

		public void DestroyWidget()
		{
			this.m_EG_MailReportRectTransform = null;
			this.m_E_BG_ClickButton = null;
			this.m_E_BG_ClickImage = null;
			this.m_EG_bgARRectTransform = null;
			this.m_EG_bgARTranslucentImage = null;
			this.m_EG_bgRectTransform = null;
			this.m_EG_bgImage = null;
			this.m_EG_titleRectTransform = null;
			this.m_EG_titleImage = null;
			this.m_ETxttitleTextMeshProUGUI = null;
			this.m_EG_infoRectTransform = null;
			this.m_es_avatarshow?.Dispose();
			this.m_es_avatarshow = null;
			this.m_ETxtSendDateTextMeshProUGUI = null;
			this.m_ETxtLimintDataTextMeshProUGUI = null;
			this.m_ETxtDetailsTextMeshProUGUI = null;
			this.m_EMainContentGiftsImage = null;
			this.m_E_CollectButton = null;
			this.m_E_CollectImage = null;
			this.m_E_CollectUnSelectImage = null;
			this.m_ELoopScrollList_LoopHorizontalScrollRect = null;
			this.uiTransform = null;
		}

		private UnityEngine.RectTransform m_EG_MailReportRectTransform = null;
		private UnityEngine.UI.Button m_E_BG_ClickButton = null;
		private UnityEngine.UI.Image m_E_BG_ClickImage = null;
		private UnityEngine.RectTransform m_EG_bgARRectTransform = null;
		private BlurBackground.TranslucentImage m_EG_bgARTranslucentImage = null;
		private UnityEngine.RectTransform m_EG_bgRectTransform = null;
		private UnityEngine.UI.Image m_EG_bgImage = null;
		private UnityEngine.RectTransform m_EG_titleRectTransform = null;
		private UnityEngine.UI.Image m_EG_titleImage = null;
		private TMPro.TextMeshProUGUI m_ETxttitleTextMeshProUGUI = null;
		private UnityEngine.RectTransform m_EG_infoRectTransform = null;
		private ES_AvatarShow m_es_avatarshow = null;
		private TMPro.TextMeshProUGUI m_ETxtSendDateTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_ETxtLimintDataTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_ETxtDetailsTextMeshProUGUI = null;
		private UnityEngine.UI.Image m_EMainContentGiftsImage = null;
		private UnityEngine.UI.Button m_E_CollectButton = null;
		private UnityEngine.UI.Image m_E_CollectImage = null;
		private UnityEngine.UI.Image m_E_CollectUnSelectImage = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopScrollList_LoopHorizontalScrollRect = null;
		public Transform uiTransform = null;
	}
}
