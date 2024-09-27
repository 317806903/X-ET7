
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgSeasonNotice))]
	[EnableMethod]
	public class DlgSeasonNoticeViewComponent : Entity, IAwake, IDestroy
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
					this.m_EG_bgARRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "E_BG_Click/EG_bgAR");
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
					this.m_EG_bgARTranslucentImage = UIFindHelper.FindDeepChild<BlurBackground.TranslucentImage>(this.uiTransform.gameObject, "E_BG_Click/EG_bgAR");
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

		public TMPro.TextMeshProUGUI ETxtTitleTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtTitleTextMeshProUGUI == null )
				{
					this.m_ETxtTitleTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "Panel/PanelBK/ETxtTitle");
				}
				return this.m_ETxtTitleTextMeshProUGUI;
			}
		}

		public TMPro.TextMeshProUGUI ETxtMonstersTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtMonstersTextMeshProUGUI == null )
				{
					this.m_ETxtMonstersTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "Panel/SeasonInfoPanel/ETxtMonsters");
				}
				return this.m_ETxtMonstersTextMeshProUGUI;
			}
		}

		public UITextLocalizeMonoView ETxtMonstersUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtMonstersUITextLocalizeMonoView == null )
				{
					this.m_ETxtMonstersUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "Panel/SeasonInfoPanel/ETxtMonsters");
				}
				return this.m_ETxtMonstersUITextLocalizeMonoView;
			}
		}

		public UnityEngine.UI.LoopHorizontalScrollRect ELoopListView_MonsersLoopHorizontalScrollRect
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELoopListView_MonsersLoopHorizontalScrollRect == null )
				{
					this.m_ELoopListView_MonsersLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject, "Panel/SeasonInfoPanel/ELoopListView_Monsers");
				}
				return this.m_ELoopListView_MonsersLoopHorizontalScrollRect;
			}
		}

		public TMPro.TextMeshProUGUI ETxtMonsterDesTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtMonsterDesTextMeshProUGUI == null )
				{
					this.m_ETxtMonsterDesTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "Panel/SeasonInfoPanel/ETxtMonsterDes");
				}
				return this.m_ETxtMonsterDesTextMeshProUGUI;
			}
		}

		public UITextLocalizeMonoView ETxtMonsterDesUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtMonsterDesUITextLocalizeMonoView == null )
				{
					this.m_ETxtMonsterDesUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "Panel/SeasonInfoPanel/ETxtMonsterDes");
				}
				return this.m_ETxtMonsterDesUITextLocalizeMonoView;
			}
		}

		public TMPro.TextMeshProUGUI ETxtCardsTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtCardsTextMeshProUGUI == null )
				{
					this.m_ETxtCardsTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "Panel/SeasonInfoPanel/ETxtCards");
				}
				return this.m_ETxtCardsTextMeshProUGUI;
			}
		}

		public UITextLocalizeMonoView ETxtCardsUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtCardsUITextLocalizeMonoView == null )
				{
					this.m_ETxtCardsUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "Panel/SeasonInfoPanel/ETxtCards");
				}
				return this.m_ETxtCardsUITextLocalizeMonoView;
			}
		}

		public UnityEngine.UI.LoopHorizontalScrollRect ELoopListView_CardsLoopHorizontalScrollRect
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELoopListView_CardsLoopHorizontalScrollRect == null )
				{
					this.m_ELoopListView_CardsLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject, "Panel/SeasonInfoPanel/ELoopListView_Cards");
				}
				return this.m_ELoopListView_CardsLoopHorizontalScrollRect;
			}
		}

		public TMPro.TextMeshProUGUI ETxtCardDesTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtCardDesTextMeshProUGUI == null )
				{
					this.m_ETxtCardDesTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "Panel/SeasonInfoPanel/ETxtCardDes");
				}
				return this.m_ETxtCardDesTextMeshProUGUI;
			}
		}

		public UITextLocalizeMonoView ETxtCardDesUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtCardDesUITextLocalizeMonoView == null )
				{
					this.m_ETxtCardDesUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "Panel/SeasonInfoPanel/ETxtCardDes");
				}
				return this.m_ETxtCardDesUITextLocalizeMonoView;
			}
		}

		public TMPro.TextMeshProUGUI ETxtFrameTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtFrameTextMeshProUGUI == null )
				{
					this.m_ETxtFrameTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "Panel/SeasonInfoPanel/ETxtFrame");
				}
				return this.m_ETxtFrameTextMeshProUGUI;
			}
		}

		public UITextLocalizeMonoView ETxtFrameUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtFrameUITextLocalizeMonoView == null )
				{
					this.m_ETxtFrameUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "Panel/SeasonInfoPanel/ETxtFrame");
				}
				return this.m_ETxtFrameUITextLocalizeMonoView;
			}
		}

		public TMPro.TextMeshProUGUI ETxtFrameDesTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtFrameDesTextMeshProUGUI == null )
				{
					this.m_ETxtFrameDesTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "Panel/SeasonInfoPanel/ETxtFrameDes");
				}
				return this.m_ETxtFrameDesTextMeshProUGUI;
			}
		}

		public UITextLocalizeMonoView ETxtFrameDesUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtFrameDesUITextLocalizeMonoView == null )
				{
					this.m_ETxtFrameDesUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "Panel/SeasonInfoPanel/ETxtFrameDes");
				}
				return this.m_ETxtFrameDesUITextLocalizeMonoView;
			}
		}

		public UnityEngine.UI.LoopHorizontalScrollRect ELoopListView_FrameLoopHorizontalScrollRect
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELoopListView_FrameLoopHorizontalScrollRect == null )
				{
					this.m_ELoopListView_FrameLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject, "Panel/SeasonInfoPanel/ELoopListView_Frame");
				}
				return this.m_ELoopListView_FrameLoopHorizontalScrollRect;
			}
		}

		public void DestroyWidget()
		{
			this.m_E_BG_ClickButton = null;
			this.m_E_BG_ClickImage = null;
			this.m_EG_bgARRectTransform = null;
			this.m_EG_bgARTranslucentImage = null;
			this.m_EG_bgRectTransform = null;
			this.m_EG_bgImage = null;
			this.m_ETxtTitleTextMeshProUGUI = null;
			this.m_ETxtMonstersTextMeshProUGUI = null;
			this.m_ETxtMonstersUITextLocalizeMonoView = null;
			this.m_ELoopListView_MonsersLoopHorizontalScrollRect = null;
			this.m_ETxtMonsterDesTextMeshProUGUI = null;
			this.m_ETxtMonsterDesUITextLocalizeMonoView = null;
			this.m_ETxtCardsTextMeshProUGUI = null;
			this.m_ETxtCardsUITextLocalizeMonoView = null;
			this.m_ELoopListView_CardsLoopHorizontalScrollRect = null;
			this.m_ETxtCardDesTextMeshProUGUI = null;
			this.m_ETxtCardDesUITextLocalizeMonoView = null;
			this.m_ETxtFrameTextMeshProUGUI = null;
			this.m_ETxtFrameUITextLocalizeMonoView = null;
			this.m_ETxtFrameDesTextMeshProUGUI = null;
			this.m_ETxtFrameDesUITextLocalizeMonoView = null;
			this.m_ELoopListView_FrameLoopHorizontalScrollRect = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_BG_ClickButton = null;
		private UnityEngine.UI.Image m_E_BG_ClickImage = null;
		private UnityEngine.RectTransform m_EG_bgARRectTransform = null;
		private BlurBackground.TranslucentImage m_EG_bgARTranslucentImage = null;
		private UnityEngine.RectTransform m_EG_bgRectTransform = null;
		private UnityEngine.UI.Image m_EG_bgImage = null;
		private TMPro.TextMeshProUGUI m_ETxtTitleTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_ETxtMonstersTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_ETxtMonstersUITextLocalizeMonoView = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopListView_MonsersLoopHorizontalScrollRect = null;
		private TMPro.TextMeshProUGUI m_ETxtMonsterDesTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_ETxtMonsterDesUITextLocalizeMonoView = null;
		private TMPro.TextMeshProUGUI m_ETxtCardsTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_ETxtCardsUITextLocalizeMonoView = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopListView_CardsLoopHorizontalScrollRect = null;
		private TMPro.TextMeshProUGUI m_ETxtCardDesTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_ETxtCardDesUITextLocalizeMonoView = null;
		private TMPro.TextMeshProUGUI m_ETxtFrameTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_ETxtFrameUITextLocalizeMonoView = null;
		private TMPro.TextMeshProUGUI m_ETxtFrameDesTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_ETxtFrameDesUITextLocalizeMonoView = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopListView_FrameLoopHorizontalScrollRect = null;
		public Transform uiTransform = null;
	}
}
