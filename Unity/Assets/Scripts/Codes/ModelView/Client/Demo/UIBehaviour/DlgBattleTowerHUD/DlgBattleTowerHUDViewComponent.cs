﻿
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgBattleTowerHUD))]
	[EnableMethod]
	public class DlgBattleTowerHUDViewComponent : Entity, IAwake, IDestroy
	{
		public UnityEngine.UI.Button E_Sprite_BGButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_Sprite_BGButton == null )
				{
					this.m_E_Sprite_BGButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_Sprite_BG");
				}
				return this.m_E_Sprite_BGButton;
			}
		}

		public UnityEngine.UI.Image E_Sprite_BGImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_Sprite_BGImage == null )
				{
					this.m_E_Sprite_BGImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Sprite_BG");
				}
				return this.m_E_Sprite_BGImage;
			}
		}

		public UnityEngine.RectTransform EG_OperatorMenuRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_OperatorMenuRectTransform == null )
				{
					this.m_EG_OperatorMenuRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_OperatorMenu");
				}
				return this.m_EG_OperatorMenuRectTransform;
			}
		}

		public UnityEngine.UI.Image EG_OperatorMenuImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_OperatorMenuImage == null )
				{
					this.m_EG_OperatorMenuImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_OperatorMenu");
				}
				return this.m_EG_OperatorMenuImage;
			}
		}

		public TMPro.TextMeshProUGUI E_TowerNameTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_TowerNameTextMeshProUGUI == null )
				{
					this.m_E_TowerNameTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_OperatorMenu/Image/E_TowerName");
				}
				return this.m_E_TowerNameTextMeshProUGUI;
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
					this.m_E_IconStar1Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_OperatorMenu/Image/E_IconStar/E_IconStar1");
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
					this.m_E_IconStar2Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_OperatorMenu/Image/E_IconStar/E_IconStar2");
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
					this.m_E_IconStar3Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_OperatorMenu/Image/E_IconStar/E_IconStar3");
				}
				return this.m_E_IconStar3Image;
			}
		}

		public UnityEngine.RectTransform EG_OperatorRootRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_OperatorRootRectTransform == null )
				{
					this.m_EG_OperatorRootRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_OperatorMenu/EG_OperatorRoot");
				}
				return this.m_EG_OperatorRootRectTransform;
			}
		}

		public UIGuide.EventTriggerListener EG_OperatorRootEventTriggerListener
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_OperatorRootEventTriggerListener == null )
				{
					this.m_EG_OperatorRootEventTriggerListener = UIFindHelper.FindDeepChild<UIGuide.EventTriggerListener>(this.uiTransform.gameObject, "EG_OperatorMenu/EG_OperatorRoot");
				}
				return this.m_EG_OperatorRootEventTriggerListener;
			}
		}

		public UnityEngine.UI.Button EButton_SaleButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_SaleButton == null )
				{
					this.m_EButton_SaleButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EG_OperatorMenu/EG_OperatorRoot/Sell/EButton_Sale");
				}
				return this.m_EButton_SaleButton;
			}
		}

		public UnityEngine.UI.Image EButton_SaleImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_SaleImage == null )
				{
					this.m_EButton_SaleImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_OperatorMenu/EG_OperatorRoot/Sell/EButton_Sale");
				}
				return this.m_EButton_SaleImage;
			}
		}

		public UnityEngine.UI.Button EButton_ConfirmButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_ConfirmButton == null )
				{
					this.m_EButton_ConfirmButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EG_OperatorMenu/EG_OperatorRoot/Sell/EButton_Confirm");
				}
				return this.m_EButton_ConfirmButton;
			}
		}

		public UnityEngine.UI.Image EButton_ConfirmImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_ConfirmImage == null )
				{
					this.m_EButton_ConfirmImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_OperatorMenu/EG_OperatorRoot/Sell/EButton_Confirm");
				}
				return this.m_EButton_ConfirmImage;
			}
		}

		public TMPro.TextMeshProUGUI E_SaleMoney_textTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_SaleMoney_textTextMeshProUGUI == null )
				{
					this.m_E_SaleMoney_textTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_OperatorMenu/EG_OperatorRoot/Sell/money/E_SaleMoney_text");
				}
				return this.m_E_SaleMoney_textTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Button E_ReclaimButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ReclaimButton == null )
				{
					this.m_E_ReclaimButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EG_OperatorMenu/EG_OperatorRoot/E_Reclaim");
				}
				return this.m_E_ReclaimButton;
			}
		}

		public UnityEngine.UI.Image E_ReclaimImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ReclaimImage == null )
				{
					this.m_E_ReclaimImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_OperatorMenu/EG_OperatorRoot/E_Reclaim");
				}
				return this.m_E_ReclaimImage;
			}
		}

		public UnityEngine.UI.Button E_UpgradeButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_UpgradeButton == null )
				{
					this.m_E_UpgradeButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EG_OperatorMenu/EG_OperatorRoot/E_Upgrade");
				}
				return this.m_E_UpgradeButton;
			}
		}

		public UnityEngine.UI.Image E_UpgradeImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_UpgradeImage == null )
				{
					this.m_E_UpgradeImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_OperatorMenu/EG_OperatorRoot/E_Upgrade");
				}
				return this.m_E_UpgradeImage;
			}
		}

		public UnityEngine.UI.Image E_Upgrade_number_iconImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_Upgrade_number_iconImage == null )
				{
					this.m_E_Upgrade_number_iconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_OperatorMenu/EG_OperatorRoot/E_Upgrade/number/E_Upgrade_number_icon");
				}
				return this.m_E_Upgrade_number_iconImage;
			}
		}

		public TMPro.TextMeshProUGUI E_Upgrade_number_TextTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_Upgrade_number_TextTextMeshProUGUI == null )
				{
					this.m_E_Upgrade_number_TextTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_OperatorMenu/EG_OperatorRoot/E_Upgrade/number/E_Upgrade_number_Text");
				}
				return this.m_E_Upgrade_number_TextTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Button E_MaxUpgradeButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_MaxUpgradeButton == null )
				{
					this.m_E_MaxUpgradeButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EG_OperatorMenu/EG_OperatorRoot/E_MaxUpgrade");
				}
				return this.m_E_MaxUpgradeButton;
			}
		}

		public UnityEngine.UI.Image E_MaxUpgradeImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_MaxUpgradeImage == null )
				{
					this.m_E_MaxUpgradeImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_OperatorMenu/EG_OperatorRoot/E_MaxUpgrade");
				}
				return this.m_E_MaxUpgradeImage;
			}
		}

		public UnityEngine.UI.Button E_ShowDetailButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ShowDetailButton == null )
				{
					this.m_E_ShowDetailButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EG_OperatorMenu/E_ShowDetail");
				}
				return this.m_E_ShowDetailButton;
			}
		}

		public UnityEngine.RectTransform EG_MyTowerDescRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_MyTowerDescRectTransform == null )
				{
					this.m_EG_MyTowerDescRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_MyTowerDesc");
				}
				return this.m_EG_MyTowerDescRectTransform;
			}
		}

		public UnityEngine.UI.Image EG_MyTowerDescImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_MyTowerDescImage == null )
				{
					this.m_EG_MyTowerDescImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_MyTowerDesc");
				}
				return this.m_EG_MyTowerDescImage;
			}
		}

		public UnityEngine.UI.Image E_IconImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_IconImage == null )
				{
					this.m_E_IconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_MyTowerDesc/E_Icon");
				}
				return this.m_E_IconImage;
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
					this.m_EImage_Label1Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_MyTowerDesc/EImage_Label1");
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
					this.m_ELabel_Label1TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_MyTowerDesc/EImage_Label1/ELabel_Label1");
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
					this.m_EImage_Label2Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_MyTowerDesc/EImage_Label2");
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
					this.m_ELabel_Label2TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_MyTowerDesc/EImage_Label2/ELabel_Label2");
				}
				return this.m_ELabel_Label2TextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Image EImage_Label3Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EImage_Label3Image == null )
				{
					this.m_EImage_Label3Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_MyTowerDesc/EImage_Label3");
				}
				return this.m_EImage_Label3Image;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_Label3TextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_Label3TextMeshProUGUI == null )
				{
					this.m_ELabel_Label3TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_MyTowerDesc/EImage_Label3/ELabel_Label3");
				}
				return this.m_ELabel_Label3TextMeshProUGUI;
			}
		}

		public UnityEngine.RectTransform EG_ViewInfoRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_ViewInfoRectTransform == null )
				{
					this.m_EG_ViewInfoRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_ViewInfo");
				}
				return this.m_EG_ViewInfoRectTransform;
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
					this.m_ELabel_DescriptionTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_ViewInfo/Scroll View/Viewport/Content/ELabel_Description");
				}
				return this.m_ELabel_DescriptionTextMeshProUGUI;
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
					this.m_ENode_Attribute1Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_ViewInfo/Attribute/Value/ENode_Attribute1");
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
					this.m_Elabel_Attribute1TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_ViewInfo/Attribute/Value/ENode_Attribute1/Elabel_Attribute1");
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
					this.m_Elabel_AttributeValue1TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_ViewInfo/Attribute/Value/ENode_Attribute1/Elabel_Attribute1/Elabel_AttributeValue1");
				}
				return this.m_Elabel_AttributeValue1TextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Image ENode_AttributeLine2Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ENode_AttributeLine2Image == null )
				{
					this.m_ENode_AttributeLine2Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_ViewInfo/Attribute/Value/ENode_AttributeLine2");
				}
				return this.m_ENode_AttributeLine2Image;
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
					this.m_ENode_Attribute2Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_ViewInfo/Attribute/Value/ENode_Attribute2");
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
					this.m_Elabel_Attribute2TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_ViewInfo/Attribute/Value/ENode_Attribute2/Elabel_Attribute2");
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
					this.m_Elabel_AttributeValue2TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_ViewInfo/Attribute/Value/ENode_Attribute2/Elabel_Attribute2/Elabel_AttributeValue2");
				}
				return this.m_Elabel_AttributeValue2TextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Image ENode_AttributeLine3Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ENode_AttributeLine3Image == null )
				{
					this.m_ENode_AttributeLine3Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_ViewInfo/Attribute/Value/ENode_AttributeLine3");
				}
				return this.m_ENode_AttributeLine3Image;
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
					this.m_ENode_Attribute3Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_ViewInfo/Attribute/Value/ENode_Attribute3");
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
					this.m_Elabel_Attribute3TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_ViewInfo/Attribute/Value/ENode_Attribute3/Elabel_Attribute3");
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
					this.m_Elabel_AttributeValue3TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_ViewInfo/Attribute/Value/ENode_Attribute3/Elabel_Attribute3/Elabel_AttributeValue3");
				}
				return this.m_Elabel_AttributeValue3TextMeshProUGUI;
			}
		}

		public void DestroyWidget()
		{
			this.m_E_Sprite_BGButton = null;
			this.m_E_Sprite_BGImage = null;
			this.m_EG_OperatorMenuRectTransform = null;
			this.m_EG_OperatorMenuImage = null;
			this.m_E_TowerNameTextMeshProUGUI = null;
			this.m_E_IconStar1Image = null;
			this.m_E_IconStar2Image = null;
			this.m_E_IconStar3Image = null;
			this.m_EG_OperatorRootRectTransform = null;
			this.m_EG_OperatorRootEventTriggerListener = null;
			this.m_EButton_SaleButton = null;
			this.m_EButton_SaleImage = null;
			this.m_EButton_ConfirmButton = null;
			this.m_EButton_ConfirmImage = null;
			this.m_E_SaleMoney_textTextMeshProUGUI = null;
			this.m_E_ReclaimButton = null;
			this.m_E_ReclaimImage = null;
			this.m_E_UpgradeButton = null;
			this.m_E_UpgradeImage = null;
			this.m_E_Upgrade_number_iconImage = null;
			this.m_E_Upgrade_number_TextTextMeshProUGUI = null;
			this.m_E_MaxUpgradeButton = null;
			this.m_E_MaxUpgradeImage = null;
			this.m_E_ShowDetailButton = null;
			this.m_EG_MyTowerDescRectTransform = null;
			this.m_EG_MyTowerDescImage = null;
			this.m_E_IconImage = null;
			this.m_EImage_Label1Image = null;
			this.m_ELabel_Label1TextMeshProUGUI = null;
			this.m_EImage_Label2Image = null;
			this.m_ELabel_Label2TextMeshProUGUI = null;
			this.m_EImage_Label3Image = null;
			this.m_ELabel_Label3TextMeshProUGUI = null;
			this.m_EG_ViewInfoRectTransform = null;
			this.m_ELabel_DescriptionTextMeshProUGUI = null;
			this.m_ENode_Attribute1Image = null;
			this.m_Elabel_Attribute1TextMeshProUGUI = null;
			this.m_Elabel_AttributeValue1TextMeshProUGUI = null;
			this.m_ENode_AttributeLine2Image = null;
			this.m_ENode_Attribute2Image = null;
			this.m_Elabel_Attribute2TextMeshProUGUI = null;
			this.m_Elabel_AttributeValue2TextMeshProUGUI = null;
			this.m_ENode_AttributeLine3Image = null;
			this.m_ENode_Attribute3Image = null;
			this.m_Elabel_Attribute3TextMeshProUGUI = null;
			this.m_Elabel_AttributeValue3TextMeshProUGUI = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_Sprite_BGButton = null;
		private UnityEngine.UI.Image m_E_Sprite_BGImage = null;
		private UnityEngine.RectTransform m_EG_OperatorMenuRectTransform = null;
		private UnityEngine.UI.Image m_EG_OperatorMenuImage = null;
		private TMPro.TextMeshProUGUI m_E_TowerNameTextMeshProUGUI = null;
		private UnityEngine.UI.Image m_E_IconStar1Image = null;
		private UnityEngine.UI.Image m_E_IconStar2Image = null;
		private UnityEngine.UI.Image m_E_IconStar3Image = null;
		private UnityEngine.RectTransform m_EG_OperatorRootRectTransform = null;
		private UIGuide.EventTriggerListener m_EG_OperatorRootEventTriggerListener = null;
		private UnityEngine.UI.Button m_EButton_SaleButton = null;
		private UnityEngine.UI.Image m_EButton_SaleImage = null;
		private UnityEngine.UI.Button m_EButton_ConfirmButton = null;
		private UnityEngine.UI.Image m_EButton_ConfirmImage = null;
		private TMPro.TextMeshProUGUI m_E_SaleMoney_textTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_E_ReclaimButton = null;
		private UnityEngine.UI.Image m_E_ReclaimImage = null;
		private UnityEngine.UI.Button m_E_UpgradeButton = null;
		private UnityEngine.UI.Image m_E_UpgradeImage = null;
		private UnityEngine.UI.Image m_E_Upgrade_number_iconImage = null;
		private TMPro.TextMeshProUGUI m_E_Upgrade_number_TextTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_E_MaxUpgradeButton = null;
		private UnityEngine.UI.Image m_E_MaxUpgradeImage = null;
		private UnityEngine.UI.Button m_E_ShowDetailButton = null;
		private UnityEngine.RectTransform m_EG_MyTowerDescRectTransform = null;
		private UnityEngine.UI.Image m_EG_MyTowerDescImage = null;
		private UnityEngine.UI.Image m_E_IconImage = null;
		private UnityEngine.UI.Image m_EImage_Label1Image = null;
		private TMPro.TextMeshProUGUI m_ELabel_Label1TextMeshProUGUI = null;
		private UnityEngine.UI.Image m_EImage_Label2Image = null;
		private TMPro.TextMeshProUGUI m_ELabel_Label2TextMeshProUGUI = null;
		private UnityEngine.UI.Image m_EImage_Label3Image = null;
		private TMPro.TextMeshProUGUI m_ELabel_Label3TextMeshProUGUI = null;
		private UnityEngine.RectTransform m_EG_ViewInfoRectTransform = null;
		private TMPro.TextMeshProUGUI m_ELabel_DescriptionTextMeshProUGUI = null;
		private UnityEngine.UI.Image m_ENode_Attribute1Image = null;
		private TMPro.TextMeshProUGUI m_Elabel_Attribute1TextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_Elabel_AttributeValue1TextMeshProUGUI = null;
		private UnityEngine.UI.Image m_ENode_AttributeLine2Image = null;
		private UnityEngine.UI.Image m_ENode_Attribute2Image = null;
		private TMPro.TextMeshProUGUI m_Elabel_Attribute2TextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_Elabel_AttributeValue2TextMeshProUGUI = null;
		private UnityEngine.UI.Image m_ENode_AttributeLine3Image = null;
		private UnityEngine.UI.Image m_ENode_Attribute3Image = null;
		private TMPro.TextMeshProUGUI m_Elabel_Attribute3TextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_Elabel_AttributeValue3TextMeshProUGUI = null;
		public Transform uiTransform = null;
	}
}