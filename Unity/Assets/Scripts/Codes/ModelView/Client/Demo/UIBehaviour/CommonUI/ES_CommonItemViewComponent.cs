
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[EnableMethod]
	public class ES_CommonItemViewComponent : Entity, ET.IAwake<UnityEngine.Transform>, IDestroy
	{
		public UnityEngine.RectTransform EG_ShowRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_ShowRectTransform == null )
				{
					this.m_EG_ShowRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_Show");
				}
				return this.m_EG_ShowRectTransform;
			}
		}

		public UnityEngine.UI.Image E_QualityTypeImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_QualityTypeImage == null )
				{
					this.m_E_QualityTypeImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_Show/E_QualityType");
				}
				return this.m_E_QualityTypeImage;
			}
		}

		public UnityEngine.UI.Image EImage_LowImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EImage_LowImage == null )
				{
					this.m_EImage_LowImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_Show/E_QualityType/EImage_Low");
				}
				return this.m_EImage_LowImage;
			}
		}

		public UnityEngine.UI.Image EImage_MiddleImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EImage_MiddleImage == null )
				{
					this.m_EImage_MiddleImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_Show/E_QualityType/EImage_Middle");
				}
				return this.m_EImage_MiddleImage;
			}
		}

		public UnityEngine.UI.Image EImage_HighImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EImage_HighImage == null )
				{
					this.m_EImage_HighImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_Show/E_QualityType/EImage_High");
				}
				return this.m_EImage_HighImage;
			}
		}

		public UnityEngine.UI.Image E_ItemIconImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ItemIconImage == null )
				{
					this.m_E_ItemIconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_Show/E_ItemIcon");
				}
				return this.m_E_ItemIconImage;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_ItemNameTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_ItemNameTextMeshProUGUI == null )
				{
					this.m_ELabel_ItemNameTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_Show/ELabel_ItemName");
				}
				return this.m_ELabel_ItemNameTextMeshProUGUI;
			}
		}

		public UnityEngine.RectTransform EG_ItemQualityRankRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_ItemQualityRankRectTransform == null )
				{
					this.m_EG_ItemQualityRankRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_Show/EG_ItemQualityRank");
				}
				return this.m_EG_ItemQualityRankRectTransform;
			}
		}

		public UnityEngine.UI.Image E_IconStar1Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_IconStar1Image == null )
				{
					this.m_E_IconStar1Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_Show/EG_ItemQualityRank/E_IconStar1");
				}
				return this.m_E_IconStar1Image;
			}
		}

		public UnityEngine.UI.Image E_IconStar2Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_IconStar2Image == null )
				{
					this.m_E_IconStar2Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_Show/EG_ItemQualityRank/E_IconStar2");
				}
				return this.m_E_IconStar2Image;
			}
		}

		public UnityEngine.UI.Image E_IconStar3Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_IconStar3Image == null )
				{
					this.m_E_IconStar3Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_Show/EG_ItemQualityRank/E_IconStar3");
				}
				return this.m_E_IconStar3Image;
			}
		}

		public UnityEngine.RectTransform EG_ItemLabelRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_ItemLabelRectTransform == null )
				{
					this.m_EG_ItemLabelRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_Show/EG_ItemLabel");
				}
				return this.m_EG_ItemLabelRectTransform;
			}
		}

		public UnityEngine.UI.Image EImage_Label1Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EImage_Label1Image == null )
				{
					this.m_EImage_Label1Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_Show/EG_ItemLabel/EImage_Label1");
				}
				return this.m_EImage_Label1Image;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_Label1TextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_Label1TextMeshProUGUI == null )
				{
					this.m_ELabel_Label1TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_Show/EG_ItemLabel/EImage_Label1/ELabel_Label1");
				}
				return this.m_ELabel_Label1TextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Image EImage_Label2Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EImage_Label2Image == null )
				{
					this.m_EImage_Label2Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_Show/EG_ItemLabel/EImage_Label2");
				}
				return this.m_EImage_Label2Image;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_Label2TextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_Label2TextMeshProUGUI == null )
				{
					this.m_ELabel_Label2TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_Show/EG_ItemLabel/EImage_Label2/ELabel_Label2");
				}
				return this.m_ELabel_Label2TextMeshProUGUI;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_ItemNumTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_ItemNumTextMeshProUGUI == null )
				{
					this.m_ELabel_ItemNumTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_Show/ELabel_ItemNum");
				}
				return this.m_ELabel_ItemNumTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Button EButton_SelectWhenCommonButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_SelectWhenCommonButton == null )
				{
					this.m_EButton_SelectWhenCommonButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EButton_SelectWhenCommon");
				}
				return this.m_EButton_SelectWhenCommonButton;
			}
		}

		public UnityEngine.UI.Image EButton_SelectWhenCommonImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_SelectWhenCommonImage == null )
				{
					this.m_EButton_SelectWhenCommonImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EButton_SelectWhenCommon");
				}
				return this.m_EButton_SelectWhenCommonImage;
			}
		}

		public void DestroyWidget()
		{
			this.m_EG_ShowRectTransform = null;
			this.m_E_QualityTypeImage = null;
			this.m_EImage_LowImage = null;
			this.m_EImage_MiddleImage = null;
			this.m_EImage_HighImage = null;
			this.m_E_ItemIconImage = null;
			this.m_ELabel_ItemNameTextMeshProUGUI = null;
			this.m_EG_ItemQualityRankRectTransform = null;
			this.m_E_IconStar1Image = null;
			this.m_E_IconStar2Image = null;
			this.m_E_IconStar3Image = null;
			this.m_EG_ItemLabelRectTransform = null;
			this.m_EImage_Label1Image = null;
			this.m_ELabel_Label1TextMeshProUGUI = null;
			this.m_EImage_Label2Image = null;
			this.m_ELabel_Label2TextMeshProUGUI = null;
			this.m_ELabel_ItemNumTextMeshProUGUI = null;
			this.m_EButton_SelectWhenCommonButton = null;
			this.m_EButton_SelectWhenCommonImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.RectTransform m_EG_ShowRectTransform = null;
		private UnityEngine.UI.Image m_E_QualityTypeImage = null;
		private UnityEngine.UI.Image m_EImage_LowImage = null;
		private UnityEngine.UI.Image m_EImage_MiddleImage = null;
		private UnityEngine.UI.Image m_EImage_HighImage = null;
		private UnityEngine.UI.Image m_E_ItemIconImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_ItemNameTextMeshProUGUI = null;
		private UnityEngine.RectTransform m_EG_ItemQualityRankRectTransform = null;
		private UnityEngine.UI.Image m_E_IconStar1Image = null;
		private UnityEngine.UI.Image m_E_IconStar2Image = null;
		private UnityEngine.UI.Image m_E_IconStar3Image = null;
		private UnityEngine.RectTransform m_EG_ItemLabelRectTransform = null;
		private UnityEngine.UI.Image m_EImage_Label1Image = null;
		private TMPro.TextMeshProUGUI m_ELabel_Label1TextMeshProUGUI = null;
		private UnityEngine.UI.Image m_EImage_Label2Image = null;
		private TMPro.TextMeshProUGUI m_ELabel_Label2TextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_ELabel_ItemNumTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_EButton_SelectWhenCommonButton = null;
		private UnityEngine.UI.Image m_EButton_SelectWhenCommonImage = null;
		public Transform uiTransform = null;
	}
}
