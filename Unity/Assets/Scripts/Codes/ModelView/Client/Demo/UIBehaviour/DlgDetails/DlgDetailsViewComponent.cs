
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgDetails))]
	[EnableMethod]
	public class DlgDetailsViewComponent : Entity, IAwake, IDestroy
	{
		public UnityEngine.UI.Image E_DetailsImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_DetailsImage == null )
				{
					this.m_E_DetailsImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Details");
				}
				return this.m_E_DetailsImage;
			}
		}

		public UnityEngine.UI.Button EButton_DetailBgButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_DetailBgButton == null )
				{
					this.m_EButton_DetailBgButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_Details/EButton_DetailBg");
				}
				return this.m_EButton_DetailBgButton;
			}
		}

		public UnityEngine.UI.Image EButton_DetailBgImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_DetailBgImage == null )
				{
					this.m_EButton_DetailBgImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Details/EButton_DetailBg");
				}
				return this.m_EButton_DetailBgImage;
			}
		}

		public UnityEngine.UI.Image EImage_BoxLowImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EImage_BoxLowImage == null )
				{
					this.m_EImage_BoxLowImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Details/EImage_BoxLow");
				}
				return this.m_EImage_BoxLowImage;
			}
		}

		public UnityEngine.UI.Image EImage_BoxMiddleImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EImage_BoxMiddleImage == null )
				{
					this.m_EImage_BoxMiddleImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Details/EImage_BoxMiddle");
				}
				return this.m_EImage_BoxMiddleImage;
			}
		}

		public UnityEngine.UI.Image EImage_BoxHighImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EImage_BoxHighImage == null )
				{
					this.m_EImage_BoxHighImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Details/EImage_BoxHigh");
				}
				return this.m_EImage_BoxHighImage;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_NameTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_NameTextMeshProUGUI == null )
				{
					this.m_ELabel_NameTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_Details/info/Name/ELabel_Name");
				}
				return this.m_ELabel_NameTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Image E_LabelsImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_LabelsImage == null )
				{
					this.m_E_LabelsImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Details/info/E_Labels");
				}
				return this.m_E_LabelsImage;
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
					this.m_EImage_Label1Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Details/info/E_Labels/EImage_Label1");
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
					this.m_ELabel_Label1TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_Details/info/E_Labels/EImage_Label1/ELabel_Label1");
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
					this.m_EImage_Label2Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Details/info/E_Labels/EImage_Label2");
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
					this.m_ELabel_Label2TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_Details/info/E_Labels/EImage_Label2/ELabel_Label2");
				}
				return this.m_ELabel_Label2TextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Image ENode_Attribute1Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ENode_Attribute1Image == null )
				{
					this.m_ENode_Attribute1Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Details/info/Value/ENode_Attribute1");
				}
				return this.m_ENode_Attribute1Image;
			}
		}

		public TMPro.TextMeshProUGUI Elabel_Attribute1TextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_Elabel_Attribute1TextMeshProUGUI == null )
				{
					this.m_Elabel_Attribute1TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_Details/info/Value/ENode_Attribute1/Elabel_Attribute1");
				}
				return this.m_Elabel_Attribute1TextMeshProUGUI;
			}
		}

		public TMPro.TextMeshProUGUI Elabel_AttributeValue1TextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_Elabel_AttributeValue1TextMeshProUGUI == null )
				{
					this.m_Elabel_AttributeValue1TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_Details/info/Value/ENode_Attribute1/Elabel_Attribute1/Elabel_AttributeValue1");
				}
				return this.m_Elabel_AttributeValue1TextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Image ENode_Attribute2Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ENode_Attribute2Image == null )
				{
					this.m_ENode_Attribute2Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Details/info/Value/ENode_Attribute2");
				}
				return this.m_ENode_Attribute2Image;
			}
		}

		public TMPro.TextMeshProUGUI Elabel_Attribute2TextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_Elabel_Attribute2TextMeshProUGUI == null )
				{
					this.m_Elabel_Attribute2TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_Details/info/Value/ENode_Attribute2/Elabel_Attribute2");
				}
				return this.m_Elabel_Attribute2TextMeshProUGUI;
			}
		}

		public TMPro.TextMeshProUGUI Elabel_AttributeValue2TextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_Elabel_AttributeValue2TextMeshProUGUI == null )
				{
					this.m_Elabel_AttributeValue2TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_Details/info/Value/ENode_Attribute2/Elabel_Attribute2/Elabel_AttributeValue2");
				}
				return this.m_Elabel_AttributeValue2TextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Image ENode_Attribute3Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ENode_Attribute3Image == null )
				{
					this.m_ENode_Attribute3Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Details/info/Value/ENode_Attribute3");
				}
				return this.m_ENode_Attribute3Image;
			}
		}

		public TMPro.TextMeshProUGUI Elabel_Attribute3TextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_Elabel_Attribute3TextMeshProUGUI == null )
				{
					this.m_Elabel_Attribute3TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_Details/info/Value/ENode_Attribute3/Elabel_Attribute3");
				}
				return this.m_Elabel_Attribute3TextMeshProUGUI;
			}
		}

		public TMPro.TextMeshProUGUI Elabel_AttributeValue3TextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_Elabel_AttributeValue3TextMeshProUGUI == null )
				{
					this.m_Elabel_AttributeValue3TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_Details/info/Value/ENode_Attribute3/Elabel_Attribute3/Elabel_AttributeValue3");
				}
				return this.m_Elabel_AttributeValue3TextMeshProUGUI;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_DescriptionTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_DescriptionTextMeshProUGUI == null )
				{
					this.m_ELabel_DescriptionTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_Details/info/ELabel_Description");
				}
				return this.m_ELabel_DescriptionTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.ScrollRect E_ScrollView_IconScrollRect
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ScrollView_IconScrollRect == null )
				{
					this.m_E_ScrollView_IconScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.ScrollRect>(this.uiTransform.gameObject, "E_Details/info/E_ScrollView_Icon");
				}
				return this.m_E_ScrollView_IconScrollRect;
			}
		}

		public UnityEngine.UI.Image E_ScrollView_IconImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ScrollView_IconImage == null )
				{
					this.m_E_ScrollView_IconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Details/info/E_ScrollView_Icon");
				}
				return this.m_E_ScrollView_IconImage;
			}
		}

		public SuperScrollView.LoopListView2 E_ScrollView_IconLoopListView2
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ScrollView_IconLoopListView2 == null )
				{
					this.m_E_ScrollView_IconLoopListView2 = UIFindHelper.FindDeepChild<SuperScrollView.LoopListView2>(this.uiTransform.gameObject, "E_Details/info/E_ScrollView_Icon");
				}
				return this.m_E_ScrollView_IconLoopListView2;
			}
		}

		public UnityEngine.UI.Button EButton_LeftButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_LeftButton == null )
				{
					this.m_EButton_LeftButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_Details/info/E_ScrollView_Icon/EButton_Left");
				}
				return this.m_EButton_LeftButton;
			}
		}

		public UnityEngine.UI.Image EButton_LeftImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_LeftImage == null )
				{
					this.m_EButton_LeftImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Details/info/E_ScrollView_Icon/EButton_Left");
				}
				return this.m_EButton_LeftImage;
			}
		}

		public UnityEngine.UI.Button EButton_RightButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_RightButton == null )
				{
					this.m_EButton_RightButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_Details/info/E_ScrollView_Icon/EButton_Right");
				}
				return this.m_EButton_RightButton;
			}
		}

		public UnityEngine.UI.Image EButton_RightImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_RightImage == null )
				{
					this.m_EButton_RightImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Details/info/E_ScrollView_Icon/EButton_Right");
				}
				return this.m_EButton_RightImage;
			}
		}

		public UnityEngine.UI.Image EImage_IconImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EImage_IconImage == null )
				{
					this.m_EImage_IconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Details/info/EImage_Icon");
				}
				return this.m_EImage_IconImage;
			}
		}

		public UnityEngine.UI.Image EButton_TowerIcoImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_TowerIcoImage == null )
				{
					this.m_EButton_TowerIcoImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Details/info/EImage_Icon/Item_Tower/EButton_TowerIco");
				}
				return this.m_EButton_TowerIcoImage;
			}
		}

		public UnityEngine.RectTransform EG_IconStarRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_IconStarRectTransform == null )
				{
					this.m_EG_IconStarRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "E_Details/info/EImage_Icon/Item_Tower/EButton_TowerIco/EG_IconStar");
				}
				return this.m_EG_IconStarRectTransform;
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
					this.m_E_IconStar1Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Details/info/EImage_Icon/Item_Tower/EButton_TowerIco/EG_IconStar/E_IconStar1");
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
					this.m_E_IconStar2Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Details/info/EImage_Icon/Item_Tower/EButton_TowerIco/EG_IconStar/E_IconStar2");
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
					this.m_E_IconStar3Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Details/info/EImage_Icon/Item_Tower/EButton_TowerIco/EG_IconStar/E_IconStar3");
				}
				return this.m_E_IconStar3Image;
			}
		}

		public void DestroyWidget()
		{
			this.m_E_DetailsImage = null;
			this.m_EButton_DetailBgButton = null;
			this.m_EButton_DetailBgImage = null;
			this.m_EImage_BoxLowImage = null;
			this.m_EImage_BoxMiddleImage = null;
			this.m_EImage_BoxHighImage = null;
			this.m_ELabel_NameTextMeshProUGUI = null;
			this.m_E_LabelsImage = null;
			this.m_EImage_Label1Image = null;
			this.m_ELabel_Label1TextMeshProUGUI = null;
			this.m_EImage_Label2Image = null;
			this.m_ELabel_Label2TextMeshProUGUI = null;
			this.m_ENode_Attribute1Image = null;
			this.m_Elabel_Attribute1TextMeshProUGUI = null;
			this.m_Elabel_AttributeValue1TextMeshProUGUI = null;
			this.m_ENode_Attribute2Image = null;
			this.m_Elabel_Attribute2TextMeshProUGUI = null;
			this.m_Elabel_AttributeValue2TextMeshProUGUI = null;
			this.m_ENode_Attribute3Image = null;
			this.m_Elabel_Attribute3TextMeshProUGUI = null;
			this.m_Elabel_AttributeValue3TextMeshProUGUI = null;
			this.m_ELabel_DescriptionTextMeshProUGUI = null;
			this.m_E_ScrollView_IconScrollRect = null;
			this.m_E_ScrollView_IconImage = null;
			this.m_E_ScrollView_IconLoopListView2 = null;
			this.m_EButton_LeftButton = null;
			this.m_EButton_LeftImage = null;
			this.m_EButton_RightButton = null;
			this.m_EButton_RightImage = null;
			this.m_EImage_IconImage = null;
			this.m_EButton_TowerIcoImage = null;
			this.m_EG_IconStarRectTransform = null;
			this.m_E_IconStar1Image = null;
			this.m_E_IconStar2Image = null;
			this.m_E_IconStar3Image = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Image m_E_DetailsImage = null;
		private UnityEngine.UI.Button m_EButton_DetailBgButton = null;
		private UnityEngine.UI.Image m_EButton_DetailBgImage = null;
		private UnityEngine.UI.Image m_EImage_BoxLowImage = null;
		private UnityEngine.UI.Image m_EImage_BoxMiddleImage = null;
		private UnityEngine.UI.Image m_EImage_BoxHighImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_NameTextMeshProUGUI = null;
		private UnityEngine.UI.Image m_E_LabelsImage = null;
		private UnityEngine.UI.Image m_EImage_Label1Image = null;
		private TMPro.TextMeshProUGUI m_ELabel_Label1TextMeshProUGUI = null;
		private UnityEngine.UI.Image m_EImage_Label2Image = null;
		private TMPro.TextMeshProUGUI m_ELabel_Label2TextMeshProUGUI = null;
		private UnityEngine.UI.Image m_ENode_Attribute1Image = null;
		private TMPro.TextMeshProUGUI m_Elabel_Attribute1TextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_Elabel_AttributeValue1TextMeshProUGUI = null;
		private UnityEngine.UI.Image m_ENode_Attribute2Image = null;
		private TMPro.TextMeshProUGUI m_Elabel_Attribute2TextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_Elabel_AttributeValue2TextMeshProUGUI = null;
		private UnityEngine.UI.Image m_ENode_Attribute3Image = null;
		private TMPro.TextMeshProUGUI m_Elabel_Attribute3TextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_Elabel_AttributeValue3TextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_ELabel_DescriptionTextMeshProUGUI = null;
		private UnityEngine.UI.ScrollRect m_E_ScrollView_IconScrollRect = null;
		private UnityEngine.UI.Image m_E_ScrollView_IconImage = null;
		private SuperScrollView.LoopListView2 m_E_ScrollView_IconLoopListView2 = null;
		private UnityEngine.UI.Button m_EButton_LeftButton = null;
		private UnityEngine.UI.Image m_EButton_LeftImage = null;
		private UnityEngine.UI.Button m_EButton_RightButton = null;
		private UnityEngine.UI.Image m_EButton_RightImage = null;
		private UnityEngine.UI.Image m_EImage_IconImage = null;
		private UnityEngine.UI.Image m_EButton_TowerIcoImage = null;
		private UnityEngine.RectTransform m_EG_IconStarRectTransform = null;
		private UnityEngine.UI.Image m_E_IconStar1Image = null;
		private UnityEngine.UI.Image m_E_IconStar2Image = null;
		private UnityEngine.UI.Image m_E_IconStar3Image = null;
		public Transform uiTransform = null;
	}
}
