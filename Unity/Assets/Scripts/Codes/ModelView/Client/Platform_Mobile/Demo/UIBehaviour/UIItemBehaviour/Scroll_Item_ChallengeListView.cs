﻿
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[EnableMethod]
	public partial class Scroll_Item_ChallengeList : Entity, IAwake, IDestroy, IUIScrollItem, IUILogic
	{
		public long DataId {get;set;}
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_Item_ChallengeList BindTrans(Transform trans)
		{
			this.uiTransform = trans;
			return this;
		}

		public UnityEngine.UI.Button EButton_dotButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if (this.isCacheNode)
				{
					if( this.m_EButton_dotButton == null )
					{
						this.m_EButton_dotButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EButton_dot");
					}
					return this.m_EButton_dotButton;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EButton_dot");
				}
			}
		}

		public UnityEngine.UI.Image E_NormalImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if (this.isCacheNode)
				{
					if( this.m_E_NormalImage == null )
					{
						this.m_E_NormalImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EButton_dot/E_Normal");
					}
					return this.m_E_NormalImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EButton_dot/E_Normal");
				}
			}
		}

		public TMPro.TextMeshProUGUI ELabel_NormalTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if (this.isCacheNode)
				{
					if( this.m_ELabel_NormalTextMeshProUGUI == null )
					{
						this.m_ELabel_NormalTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EButton_dot/E_Normal/ELabel_Normal");
					}
					return this.m_ELabel_NormalTextMeshProUGUI;
				}
				else
				{
					return UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EButton_dot/E_Normal/ELabel_Normal");
				}
			}
		}

		public UnityEngine.UI.Image E_SelectedImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if (this.isCacheNode)
				{
					if( this.m_E_SelectedImage == null )
					{
						this.m_E_SelectedImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EButton_dot/E_Selected");
					}
					return this.m_E_SelectedImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EButton_dot/E_Selected");
				}
			}
		}

		public UnityEngine.UI.Image E_UnlockedImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if (this.isCacheNode)
				{
					if( this.m_E_UnlockedImage == null )
					{
						this.m_E_UnlockedImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EButton_dot/E_Unlocked");
					}
					return this.m_E_UnlockedImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EButton_dot/E_Unlocked");
				}
			}
		}

		public TMPro.TextMeshProUGUI ELabel_UnlockedTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if (this.isCacheNode)
				{
					if( this.m_ELabel_UnlockedTextMeshProUGUI == null )
					{
						this.m_ELabel_UnlockedTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EButton_dot/E_Unlocked/ELabel_Unlocked");
					}
					return this.m_ELabel_UnlockedTextMeshProUGUI;
				}
				else
				{
					return UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EButton_dot/E_Unlocked/ELabel_Unlocked");
				}
			}
		}

		public UnityEngine.UI.Image E_Normal_lineImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if (this.isCacheNode)
				{
					if( this.m_E_Normal_lineImage == null )
					{
						this.m_E_Normal_lineImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Normal_line");
					}
					return this.m_E_Normal_lineImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Normal_line");
				}
			}
		}

		public UnityEngine.RectTransform EG_Unlocked_lineRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if (this.isCacheNode)
				{
					if( this.m_EG_Unlocked_lineRectTransform == null )
					{
						this.m_EG_Unlocked_lineRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_Unlocked_line");
					}
					return this.m_EG_Unlocked_lineRectTransform;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_Unlocked_line");
				}
			}
		}

		public UnityEngine.UI.Image E_Rewards_iconImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if (this.isCacheNode)
				{
					if( this.m_E_Rewards_iconImage == null )
					{
						this.m_E_Rewards_iconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Rewards_icon");
					}
					return this.m_E_Rewards_iconImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Rewards_icon");
				}
			}
		}

		public UnityEngine.RectTransform EG_DifficultyMode_iconRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if (this.isCacheNode)
				{
					if( this.m_EG_DifficultyMode_iconRectTransform == null )
					{
						this.m_EG_DifficultyMode_iconRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_DifficultyMode_icon");
					}
					return this.m_EG_DifficultyMode_iconRectTransform;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_DifficultyMode_icon");
				}
			}
		}

		public UnityEngine.UI.Image E_icon_easyImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if (this.isCacheNode)
				{
					if( this.m_E_icon_easyImage == null )
					{
						this.m_E_icon_easyImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_DifficultyMode_icon/E_icon_easy");
					}
					return this.m_E_icon_easyImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_DifficultyMode_icon/E_icon_easy");
				}
			}
		}

		public UnityEngine.UI.Image E_icon_normalImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if (this.isCacheNode)
				{
					if( this.m_E_icon_normalImage == null )
					{
						this.m_E_icon_normalImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_DifficultyMode_icon/E_icon_normal");
					}
					return this.m_E_icon_normalImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_DifficultyMode_icon/E_icon_normal");
				}
			}
		}

		public UnityEngine.UI.Image E_icon_hardImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if (this.isCacheNode)
				{
					if( this.m_E_icon_hardImage == null )
					{
						this.m_E_icon_hardImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_DifficultyMode_icon/E_icon_hard");
					}
					return this.m_E_icon_hardImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_DifficultyMode_icon/E_icon_hard");
				}
			}
		}

		public UnityEngine.UI.Image E_icon_extremeImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if (this.isCacheNode)
				{
					if( this.m_E_icon_extremeImage == null )
					{
						this.m_E_icon_extremeImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_DifficultyMode_icon/E_icon_extreme");
					}
					return this.m_E_icon_extremeImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_DifficultyMode_icon/E_icon_extreme");
				}
			}
		}

		public void DestroyWidget()
		{
			this.m_EButton_dotButton = null;
			this.m_E_NormalImage = null;
			this.m_ELabel_NormalTextMeshProUGUI = null;
			this.m_E_SelectedImage = null;
			this.m_E_UnlockedImage = null;
			this.m_ELabel_UnlockedTextMeshProUGUI = null;
			this.m_E_Normal_lineImage = null;
			this.m_EG_Unlocked_lineRectTransform = null;
			this.m_E_Rewards_iconImage = null;
			this.m_EG_DifficultyMode_iconRectTransform = null;
			this.m_E_icon_easyImage = null;
			this.m_E_icon_normalImage = null;
			this.m_E_icon_hardImage = null;
			this.m_E_icon_extremeImage = null;
			this.uiTransform = null;
			this.DataId = 0;
		}

		private UnityEngine.UI.Button m_EButton_dotButton = null;
		private UnityEngine.UI.Image m_E_NormalImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_NormalTextMeshProUGUI = null;
		private UnityEngine.UI.Image m_E_SelectedImage = null;
		private UnityEngine.UI.Image m_E_UnlockedImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_UnlockedTextMeshProUGUI = null;
		private UnityEngine.UI.Image m_E_Normal_lineImage = null;
		private UnityEngine.RectTransform m_EG_Unlocked_lineRectTransform = null;
		private UnityEngine.UI.Image m_E_Rewards_iconImage = null;
		private UnityEngine.RectTransform m_EG_DifficultyMode_iconRectTransform = null;
		private UnityEngine.UI.Image m_E_icon_easyImage = null;
		private UnityEngine.UI.Image m_E_icon_normalImage = null;
		private UnityEngine.UI.Image m_E_icon_hardImage = null;
		private UnityEngine.UI.Image m_E_icon_extremeImage = null;
		public Transform uiTransform = null;
	}
}
