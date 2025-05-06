
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[EnableMethod]
	public class EPage_PowerupViewComponent : Entity, ET.IAwake<UnityEngine.Transform>, IDestroy
	{
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
					this.m_ELabel_TitleTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "TopInfo/ELabel_Title");
				}
				return this.m_ELabel_TitleTextMeshProUGUI;
			}
		}

		public UITextLocalizeMonoView ELabel_TitleUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_TitleUITextLocalizeMonoView == null )
				{
					this.m_ELabel_TitleUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "TopInfo/ELabel_Title");
				}
				return this.m_ELabel_TitleUITextLocalizeMonoView;
			}
		}

		public UnityEngine.UI.Button EBtnResetButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EBtnResetButton == null )
				{
					this.m_EBtnResetButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "TopInfo/EBtnReset");
				}
				return this.m_EBtnResetButton;
			}
		}

		public UnityEngine.UI.Image EBtnResetImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EBtnResetImage == null )
				{
					this.m_EBtnResetImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "TopInfo/EBtnReset");
				}
				return this.m_EBtnResetImage;
			}
		}

		public TMPro.TextMeshProUGUI ETxtResetTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtResetTextMeshProUGUI == null )
				{
					this.m_ETxtResetTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "TopInfo/EBtnReset/ETxtReset");
				}
				return this.m_ETxtResetTextMeshProUGUI;
			}
		}

		public UITextLocalizeMonoView ETxtResetUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtResetUITextLocalizeMonoView == null )
				{
					this.m_ETxtResetUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "TopInfo/EBtnReset/ETxtReset");
				}
				return this.m_ETxtResetUITextLocalizeMonoView;
			}
		}

		public TMPro.TextMeshProUGUI ETxtResetNumTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtResetNumTextMeshProUGUI == null )
				{
					this.m_ETxtResetNumTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "TopInfo/EBtnReset/ETxtResetNum");
				}
				return this.m_ETxtResetNumTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.LoopVerticalScrollRect ELoopScrollList_LoopVerticalScrollRect
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELoopScrollList_LoopVerticalScrollRect == null )
				{
					this.m_ELoopScrollList_LoopVerticalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopVerticalScrollRect>(this.uiTransform.gameObject, "Midnfo/ELoopScrollList_");
				}
				return this.m_ELoopScrollList_LoopVerticalScrollRect;
			}
		}

		public UnityEngine.UI.Image EMainIconImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EMainIconImage == null )
				{
					this.m_EMainIconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "BottomInfo/PowerUps/TopIcon/EMainIcon");
				}
				return this.m_EMainIconImage;
			}
		}

		public UnityEngine.UI.Image EOutlineColorImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EOutlineColorImage == null )
				{
					this.m_EOutlineColorImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "BottomInfo/PowerUps/TopIcon/OutLine/EOutlineColor");
				}
				return this.m_EOutlineColorImage;
			}
		}

		public UnityEngine.UI.Image ELevelline2Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELevelline2Image == null )
				{
					this.m_ELevelline2Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "BottomInfo/PowerUps/TopIcon/OutLine/ELevelline2");
				}
				return this.m_ELevelline2Image;
			}
		}

		public UnityEngine.UI.Image ELevelline3Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELevelline3Image == null )
				{
					this.m_ELevelline3Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "BottomInfo/PowerUps/TopIcon/OutLine/ELevelline3");
				}
				return this.m_ELevelline3Image;
			}
		}

		public UnityEngine.UI.Image ELevelline4Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELevelline4Image == null )
				{
					this.m_ELevelline4Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "BottomInfo/PowerUps/TopIcon/OutLine/ELevelline4");
				}
				return this.m_ELevelline4Image;
			}
		}

		public UnityEngine.UI.Image EIconUpImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EIconUpImage == null )
				{
					this.m_EIconUpImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "BottomInfo/PowerUps/TopIcon/EIconUp");
				}
				return this.m_EIconUpImage;
			}
		}

		public TMPro.TextMeshProUGUI ETxtBottomItemNameTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtBottomItemNameTextMeshProUGUI == null )
				{
					this.m_ETxtBottomItemNameTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "BottomInfo/Txt/ETxtBottomItemName");
				}
				return this.m_ETxtBottomItemNameTextMeshProUGUI;
			}
		}

		public TMPro.TextMeshProUGUI ETxtBottomItemDesTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtBottomItemDesTextMeshProUGUI == null )
				{
					this.m_ETxtBottomItemDesTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "BottomInfo/Txt/ETxtBottomItemDes");
				}
				return this.m_ETxtBottomItemDesTextMeshProUGUI;
			}
		}

		public TMPro.TextMeshProUGUI ETxtNextLevelTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtNextLevelTextMeshProUGUI == null )
				{
					this.m_ETxtNextLevelTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "BottomInfo/Txt/Nextlevel/ETxtNextLevel");
				}
				return this.m_ETxtNextLevelTextMeshProUGUI;
			}
		}

		public TMPro.TextMeshProUGUI ETxtBottomExtendNameTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtBottomExtendNameTextMeshProUGUI == null )
				{
					this.m_ETxtBottomExtendNameTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "BottomInfo/Txt/Nextlevel/ETxtBottomExtendName");
				}
				return this.m_ETxtBottomExtendNameTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Button EBtnUpdateButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EBtnUpdateButton == null )
				{
					this.m_EBtnUpdateButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "BottomInfo/EBtnUpdate");
				}
				return this.m_EBtnUpdateButton;
			}
		}

		public UnityEngine.UI.Image EBtnUpdateImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EBtnUpdateImage == null )
				{
					this.m_EBtnUpdateImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "BottomInfo/EBtnUpdate");
				}
				return this.m_EBtnUpdateImage;
			}
		}

		public TMPro.TextMeshProUGUI ETxtUpgradeTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtUpgradeTextMeshProUGUI == null )
				{
					this.m_ETxtUpgradeTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "BottomInfo/EBtnUpdate/ETxtUpgrade");
				}
				return this.m_ETxtUpgradeTextMeshProUGUI;
			}
		}

		public UITextLocalizeMonoView ETxtUpgradeUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtUpgradeUITextLocalizeMonoView == null )
				{
					this.m_ETxtUpgradeUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "BottomInfo/EBtnUpdate/ETxtUpgrade");
				}
				return this.m_ETxtUpgradeUITextLocalizeMonoView;
			}
		}

		public TMPro.TextMeshProUGUI ETxtUpgradeNumTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtUpgradeNumTextMeshProUGUI == null )
				{
					this.m_ETxtUpgradeNumTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "BottomInfo/EBtnUpdate/ETxtUpgradeNum");
				}
				return this.m_ETxtUpgradeNumTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Button EBtnUpdate_nullButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EBtnUpdate_nullButton == null )
				{
					this.m_EBtnUpdate_nullButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "BottomInfo/EBtnUpdate_null");
				}
				return this.m_EBtnUpdate_nullButton;
			}
		}

		public UnityEngine.UI.Image EBtnUpdate_nullImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EBtnUpdate_nullImage == null )
				{
					this.m_EBtnUpdate_nullImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "BottomInfo/EBtnUpdate_null");
				}
				return this.m_EBtnUpdate_nullImage;
			}
		}

		public TMPro.TextMeshProUGUI ETxtUpgradeNullTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtUpgradeNullTextMeshProUGUI == null )
				{
					this.m_ETxtUpgradeNullTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "BottomInfo/EBtnUpdate_null/ETxtUpgradeNull");
				}
				return this.m_ETxtUpgradeNullTextMeshProUGUI;
			}
		}

		public UITextLocalizeMonoView ETxtUpgradeNullUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtUpgradeNullUITextLocalizeMonoView == null )
				{
					this.m_ETxtUpgradeNullUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "BottomInfo/EBtnUpdate_null/ETxtUpgradeNull");
				}
				return this.m_ETxtUpgradeNullUITextLocalizeMonoView;
			}
		}

		public TMPro.TextMeshProUGUI ETxtUpgradeNumNullTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtUpgradeNumNullTextMeshProUGUI == null )
				{
					this.m_ETxtUpgradeNumNullTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "BottomInfo/EBtnUpdate_null/ETxtUpgradeNumNull");
				}
				return this.m_ETxtUpgradeNumNullTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Button EBtnUpdate_maxButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EBtnUpdate_maxButton == null )
				{
					this.m_EBtnUpdate_maxButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "BottomInfo/EBtnUpdate_max");
				}
				return this.m_EBtnUpdate_maxButton;
			}
		}

		public UnityEngine.UI.Image EBtnUpdate_maxImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EBtnUpdate_maxImage == null )
				{
					this.m_EBtnUpdate_maxImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "BottomInfo/EBtnUpdate_max");
				}
				return this.m_EBtnUpdate_maxImage;
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
			this.m_ELabel_TitleTextMeshProUGUI = null;
			this.m_ELabel_TitleUITextLocalizeMonoView = null;
			this.m_EBtnResetButton = null;
			this.m_EBtnResetImage = null;
			this.m_ETxtResetTextMeshProUGUI = null;
			this.m_ETxtResetUITextLocalizeMonoView = null;
			this.m_ETxtResetNumTextMeshProUGUI = null;
			this.m_ELoopScrollList_LoopVerticalScrollRect = null;
			this.m_EMainIconImage = null;
			this.m_EOutlineColorImage = null;
			this.m_ELevelline2Image = null;
			this.m_ELevelline3Image = null;
			this.m_ELevelline4Image = null;
			this.m_EIconUpImage = null;
			this.m_ETxtBottomItemNameTextMeshProUGUI = null;
			this.m_ETxtBottomItemDesTextMeshProUGUI = null;
			this.m_ETxtNextLevelTextMeshProUGUI = null;
			this.m_ETxtBottomExtendNameTextMeshProUGUI = null;
			this.m_EBtnUpdateButton = null;
			this.m_EBtnUpdateImage = null;
			this.m_ETxtUpgradeTextMeshProUGUI = null;
			this.m_ETxtUpgradeUITextLocalizeMonoView = null;
			this.m_ETxtUpgradeNumTextMeshProUGUI = null;
			this.m_EBtnUpdate_nullButton = null;
			this.m_EBtnUpdate_nullImage = null;
			this.m_ETxtUpgradeNullTextMeshProUGUI = null;
			this.m_ETxtUpgradeNullUITextLocalizeMonoView = null;
			this.m_ETxtUpgradeNumNullTextMeshProUGUI = null;
			this.m_EBtnUpdate_maxButton = null;
			this.m_EBtnUpdate_maxImage = null;
			this.m_EG_OpenAnimationRectTransform = null;
			this.m_EG_CloseAnimationRectTransform = null;
			this.uiTransform = null;
		}

		private TMPro.TextMeshProUGUI m_ELabel_TitleTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_ELabel_TitleUITextLocalizeMonoView = null;
		private UnityEngine.UI.Button m_EBtnResetButton = null;
		private UnityEngine.UI.Image m_EBtnResetImage = null;
		private TMPro.TextMeshProUGUI m_ETxtResetTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_ETxtResetUITextLocalizeMonoView = null;
		private TMPro.TextMeshProUGUI m_ETxtResetNumTextMeshProUGUI = null;
		private UnityEngine.UI.LoopVerticalScrollRect m_ELoopScrollList_LoopVerticalScrollRect = null;
		private UnityEngine.UI.Image m_EMainIconImage = null;
		private UnityEngine.UI.Image m_EOutlineColorImage = null;
		private UnityEngine.UI.Image m_ELevelline2Image = null;
		private UnityEngine.UI.Image m_ELevelline3Image = null;
		private UnityEngine.UI.Image m_ELevelline4Image = null;
		private UnityEngine.UI.Image m_EIconUpImage = null;
		private TMPro.TextMeshProUGUI m_ETxtBottomItemNameTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_ETxtBottomItemDesTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_ETxtNextLevelTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_ETxtBottomExtendNameTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_EBtnUpdateButton = null;
		private UnityEngine.UI.Image m_EBtnUpdateImage = null;
		private TMPro.TextMeshProUGUI m_ETxtUpgradeTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_ETxtUpgradeUITextLocalizeMonoView = null;
		private TMPro.TextMeshProUGUI m_ETxtUpgradeNumTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_EBtnUpdate_nullButton = null;
		private UnityEngine.UI.Image m_EBtnUpdate_nullImage = null;
		private TMPro.TextMeshProUGUI m_ETxtUpgradeNullTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_ETxtUpgradeNullUITextLocalizeMonoView = null;
		private TMPro.TextMeshProUGUI m_ETxtUpgradeNumNullTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_EBtnUpdate_maxButton = null;
		private UnityEngine.UI.Image m_EBtnUpdate_maxImage = null;
		private UnityEngine.RectTransform m_EG_OpenAnimationRectTransform = null;
		private UnityEngine.RectTransform m_EG_CloseAnimationRectTransform = null;
		public Transform uiTransform = null;
	}
}
