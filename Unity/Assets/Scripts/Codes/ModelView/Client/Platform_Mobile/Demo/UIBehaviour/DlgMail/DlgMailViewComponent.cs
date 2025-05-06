
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgMail))]
	[EnableMethod]
	public class DlgMailViewComponent : Entity, IAwake, IDestroy
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
					this.m_E_BG_ClickButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "Inbox/E_BG_Click");
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
					this.m_E_BG_ClickImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "Inbox/E_BG_Click");
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
					this.m_EG_bgARRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "Inbox/E_BG_Click/EG_bgAR");
				}
				return this.m_EG_bgARRectTransform;
			}
		}

		public UnityEngine.UI.Image EG_bgARImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_bgARImage == null )
				{
					this.m_EG_bgARImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "Inbox/E_BG_Click/EG_bgAR");
				}
				return this.m_EG_bgARImage;
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
					this.m_EG_bgRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "Inbox/E_BG_Click/EG_bg");
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
					this.m_EG_bgImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "Inbox/E_BG_Click/EG_bg");
				}
				return this.m_EG_bgImage;
			}
		}

		public UnityEngine.UI.LoopVerticalScrollRect ELoopScrollList_MailLoopVerticalScrollRect
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELoopScrollList_MailLoopVerticalScrollRect == null )
				{
					this.m_ELoopScrollList_MailLoopVerticalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopVerticalScrollRect>(this.uiTransform.gameObject, "Inbox/MainContent/MidContent/ELoopScrollList_Mail");
				}
				return this.m_ELoopScrollList_MailLoopVerticalScrollRect;
			}
		}

		public UnityEngine.UI.Image ELabel_InboxEmptyImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_InboxEmptyImage == null )
				{
					this.m_ELabel_InboxEmptyImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "Inbox/MainContent/MidContent/ELabel_InboxEmpty");
				}
				return this.m_ELabel_InboxEmptyImage;
			}
		}

		public UnityEngine.UI.Button EBtnCollectAllButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EBtnCollectAllButton == null )
				{
					this.m_EBtnCollectAllButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "Inbox/Filter/EBtnCollectAll");
				}
				return this.m_EBtnCollectAllButton;
			}
		}

		public UnityEngine.UI.Image EBtnCollectAllImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EBtnCollectAllImage == null )
				{
					this.m_EBtnCollectAllImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "Inbox/Filter/EBtnCollectAll");
				}
				return this.m_EBtnCollectAllImage;
			}
		}

		public UnityEngine.UI.Button EBtnCollectAll_NoneButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EBtnCollectAll_NoneButton == null )
				{
					this.m_EBtnCollectAll_NoneButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "Inbox/Filter/EBtnCollectAll_None");
				}
				return this.m_EBtnCollectAll_NoneButton;
			}
		}

		public UnityEngine.UI.Image EBtnCollectAll_NoneImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EBtnCollectAll_NoneImage == null )
				{
					this.m_EBtnCollectAll_NoneImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "Inbox/Filter/EBtnCollectAll_None");
				}
				return this.m_EBtnCollectAll_NoneImage;
			}
		}

		public TMPro.TMP_Dropdown EMailDropdownTMP_Dropdown
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EMailDropdownTMP_Dropdown == null )
				{
					this.m_EMailDropdownTMP_Dropdown = UIFindHelper.FindDeepChild<TMPro.TMP_Dropdown>(this.uiTransform.gameObject, "Inbox/Filter/EMailDropdown");
				}
				return this.m_EMailDropdownTMP_Dropdown;
			}
		}

		public UnityEngine.UI.Image EMailDropdownImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EMailDropdownImage == null )
				{
					this.m_EMailDropdownImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "Inbox/Filter/EMailDropdown");
				}
				return this.m_EMailDropdownImage;
			}
		}

		public UnityEngine.UI.Image EMailDropdown_NoneImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EMailDropdown_NoneImage == null )
				{
					this.m_EMailDropdown_NoneImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "Inbox/Filter/EMailDropdown_None");
				}
				return this.m_EMailDropdown_NoneImage;
			}
		}

		public UnityEngine.UI.Button EQuitBattleButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EQuitBattleButton == null )
				{
					this.m_EQuitBattleButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "Inbox/EQuitBattle");
				}
				return this.m_EQuitBattleButton;
			}
		}

		public UnityEngine.UI.Image EQuitBattleImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EQuitBattleImage == null )
				{
					this.m_EQuitBattleImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "Inbox/EQuitBattle");
				}
				return this.m_EQuitBattleImage;
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
			this.m_E_BG_ClickButton = null;
			this.m_E_BG_ClickImage = null;
			this.m_EG_bgARRectTransform = null;
			this.m_EG_bgARImage = null;
			this.m_EG_bgRectTransform = null;
			this.m_EG_bgImage = null;
			this.m_ELoopScrollList_MailLoopVerticalScrollRect = null;
			this.m_ELabel_InboxEmptyImage = null;
			this.m_EBtnCollectAllButton = null;
			this.m_EBtnCollectAllImage = null;
			this.m_EBtnCollectAll_NoneButton = null;
			this.m_EBtnCollectAll_NoneImage = null;
			this.m_EMailDropdownTMP_Dropdown = null;
			this.m_EMailDropdownImage = null;
			this.m_EMailDropdown_NoneImage = null;
			this.m_EQuitBattleButton = null;
			this.m_EQuitBattleImage = null;
			this.m_EG_OpenAnimationRectTransform = null;
			this.m_EG_CloseAnimationRectTransform = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_BG_ClickButton = null;
		private UnityEngine.UI.Image m_E_BG_ClickImage = null;
		private UnityEngine.RectTransform m_EG_bgARRectTransform = null;
		private UnityEngine.UI.Image m_EG_bgARImage = null;
		private UnityEngine.RectTransform m_EG_bgRectTransform = null;
		private UnityEngine.UI.Image m_EG_bgImage = null;
		private UnityEngine.UI.LoopVerticalScrollRect m_ELoopScrollList_MailLoopVerticalScrollRect = null;
		private UnityEngine.UI.Image m_ELabel_InboxEmptyImage = null;
		private UnityEngine.UI.Button m_EBtnCollectAllButton = null;
		private UnityEngine.UI.Image m_EBtnCollectAllImage = null;
		private UnityEngine.UI.Button m_EBtnCollectAll_NoneButton = null;
		private UnityEngine.UI.Image m_EBtnCollectAll_NoneImage = null;
		private TMPro.TMP_Dropdown m_EMailDropdownTMP_Dropdown = null;
		private UnityEngine.UI.Image m_EMailDropdownImage = null;
		private UnityEngine.UI.Image m_EMailDropdown_NoneImage = null;
		private UnityEngine.UI.Button m_EQuitBattleButton = null;
		private UnityEngine.UI.Image m_EQuitBattleImage = null;
		private UnityEngine.RectTransform m_EG_OpenAnimationRectTransform = null;
		private UnityEngine.RectTransform m_EG_CloseAnimationRectTransform = null;
		public Transform uiTransform = null;
	}
}
