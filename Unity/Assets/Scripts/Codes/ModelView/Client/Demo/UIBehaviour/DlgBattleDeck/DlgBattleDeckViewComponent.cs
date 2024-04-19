
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgBattleDeck))]
	[EnableMethod]
	public class DlgBattleDeckViewComponent : Entity, IAwake, IDestroy
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

		public UnityEngine.UI.Button E_QuitBattleButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_QuitBattleButton == null )
				{
					this.m_E_QuitBattleButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_QuitBattle");
				}
				return this.m_E_QuitBattleButton;
			}
		}

		public UnityEngine.UI.Image E_QuitBattleImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_QuitBattleImage == null )
				{
					this.m_E_QuitBattleImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_QuitBattle");
				}
				return this.m_E_QuitBattleImage;
			}
		}

		public UnityEngine.UI.LoopHorizontalScrollRect ELoopScrollList_BattleDeckItemLoopHorizontalScrollRect
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELoopScrollList_BattleDeckItemLoopHorizontalScrollRect == null )
				{
					this.m_ELoopScrollList_BattleDeckItemLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject, "E_Bag/E_BattleDeck/info/list/ELoopScrollList_BattleDeckItem");
				}
				return this.m_ELoopScrollList_BattleDeckItemLoopHorizontalScrollRect;
			}
		}

		public UnityEngine.UI.LoopHorizontalScrollRect ELoopScrollList_TowerCardItemLoopHorizontalScrollRect
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELoopScrollList_TowerCardItemLoopHorizontalScrollRect == null )
				{
					this.m_ELoopScrollList_TowerCardItemLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject, "E_Bag/E_CardCollection/info/list/ELoopScrollList_TowerCardItem");
				}
				return this.m_ELoopScrollList_TowerCardItemLoopHorizontalScrollRect;
			}
		}

		public UnityEngine.RectTransform EG_MoveItemRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_MoveItemRectTransform == null )
				{
					this.m_EG_MoveItemRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "E_Bag/EG_MoveItem");
				}
				return this.m_EG_MoveItemRectTransform;
			}
		}

		public UnityEngine.UI.Image E_NoneImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_NoneImage == null )
				{
					this.m_E_NoneImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Bag/EG_MoveItem/Item_TowerBuy/E_None");
				}
				return this.m_E_NoneImage;
			}
		}

		public UnityEngine.UI.Image EImage_TowerBuyShowImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EImage_TowerBuyShowImage == null )
				{
					this.m_EImage_TowerBuyShowImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Bag/EG_MoveItem/Item_TowerBuy/EImage_TowerBuyShow");
				}
				return this.m_EImage_TowerBuyShowImage;
			}
		}

		public UnityEngine.UI.Image E_BoxImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_BoxImage == null )
				{
					this.m_E_BoxImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Bag/EG_MoveItem/Item_TowerBuy/EImage_TowerBuyShow/E_Box");
				}
				return this.m_E_BoxImage;
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
					this.m_EImage_LowImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Bag/EG_MoveItem/Item_TowerBuy/EImage_TowerBuyShow/E_Box/EImage_Low");
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
					this.m_EImage_MiddleImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Bag/EG_MoveItem/Item_TowerBuy/EImage_TowerBuyShow/E_Box/EImage_Middle");
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
					this.m_EImage_HighImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Bag/EG_MoveItem/Item_TowerBuy/EImage_TowerBuyShow/E_Box/EImage_High");
				}
				return this.m_EImage_HighImage;
			}
		}

		public UnityEngine.UI.Button EButton_IconButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_IconButton == null )
				{
					this.m_EButton_IconButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_Bag/EG_MoveItem/Item_TowerBuy/EImage_TowerBuyShow/EButton_Icon");
				}
				return this.m_EButton_IconButton;
			}
		}

		public UnityEngine.UI.Image EButton_IconImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_IconImage == null )
				{
					this.m_EButton_IconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Bag/EG_MoveItem/Item_TowerBuy/EImage_TowerBuyShow/EButton_Icon");
				}
				return this.m_EButton_IconImage;
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
					this.m_EG_IconStarRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "E_Bag/EG_MoveItem/Item_TowerBuy/EImage_TowerBuyShow/EG_IconStar");
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
					this.m_E_IconStar1Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Bag/EG_MoveItem/Item_TowerBuy/EImage_TowerBuyShow/EG_IconStar/E_IconStar1");
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
					this.m_E_IconStar2Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Bag/EG_MoveItem/Item_TowerBuy/EImage_TowerBuyShow/EG_IconStar/E_IconStar2");
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
					this.m_E_IconStar3Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Bag/EG_MoveItem/Item_TowerBuy/EImage_TowerBuyShow/EG_IconStar/E_IconStar3");
				}
				return this.m_E_IconStar3Image;
			}
		}

		public TMPro.TextMeshProUGUI EButton_nameTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_nameTextMeshProUGUI == null )
				{
					this.m_EButton_nameTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_Bag/EG_MoveItem/Item_TowerBuy/EImage_TowerBuyShow/EButton_name");
				}
				return this.m_EButton_nameTextMeshProUGUI;
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
					this.m_EImage_Label1Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Bag/EG_MoveItem/Item_TowerBuy/EImage_TowerBuyShow/EImage_Label1");
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
					this.m_ELabel_Label1TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_Bag/EG_MoveItem/Item_TowerBuy/EImage_TowerBuyShow/EImage_Label1/ELabel_Label1");
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
					this.m_EImage_Label2Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Bag/EG_MoveItem/Item_TowerBuy/EImage_TowerBuyShow/EImage_Label2");
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
					this.m_ELabel_Label2TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_Bag/EG_MoveItem/Item_TowerBuy/EImage_TowerBuyShow/EImage_Label2/ELabel_Label2");
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
					this.m_EImage_Label3Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Bag/EG_MoveItem/Item_TowerBuy/EImage_TowerBuyShow/EImage_Label3");
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
					this.m_ELabel_Label3TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_Bag/EG_MoveItem/Item_TowerBuy/EImage_TowerBuyShow/EImage_Label3/ELabel_Label3");
				}
				return this.m_ELabel_Label3TextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Image EImage_BuyBGImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EImage_BuyBGImage == null )
				{
					this.m_EImage_BuyBGImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Bag/EG_MoveItem/Item_TowerBuy/EImage_TowerBuyShow/EImage_BuyBG");
				}
				return this.m_EImage_BuyBGImage;
			}
		}

		public UnityEngine.UI.Text ELabel_BuyText
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_BuyText == null )
				{
					this.m_ELabel_BuyText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject, "E_Bag/EG_MoveItem/Item_TowerBuy/EImage_TowerBuyShow/EImage_BuyBG/ELabel_Buy");
				}
				return this.m_ELabel_BuyText;
			}
		}

		public UITextLocalizeMonoView ELabel_BuyUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_BuyUITextLocalizeMonoView == null )
				{
					this.m_ELabel_BuyUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "E_Bag/EG_MoveItem/Item_TowerBuy/EImage_TowerBuyShow/EImage_BuyBG/ELabel_Buy");
				}
				return this.m_ELabel_BuyUITextLocalizeMonoView;
			}
		}

		public UnityEngine.UI.Button EButton_BuyButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_BuyButton == null )
				{
					this.m_EButton_BuyButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_Bag/EG_MoveItem/Item_TowerBuy/EImage_TowerBuyShow/EImage_BuyBG/EButton_Buy");
				}
				return this.m_EButton_BuyButton;
			}
		}

		public UnityEngine.UI.Image EButton_BuyImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_BuyImage == null )
				{
					this.m_EButton_BuyImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Bag/EG_MoveItem/Item_TowerBuy/EImage_TowerBuyShow/EImage_BuyBG/EButton_Buy");
				}
				return this.m_EButton_BuyImage;
			}
		}

		public UnityEngine.UI.Image ELabel_UnableBuy_iconImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_UnableBuy_iconImage == null )
				{
					this.m_ELabel_UnableBuy_iconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Bag/EG_MoveItem/Item_TowerBuy/EImage_TowerBuyShow/EImage_BuyBG/ELabel_UnableBuy_icon");
				}
				return this.m_ELabel_UnableBuy_iconImage;
			}
		}

		public UnityEngine.UI.Text ELabel_ContentText
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_ContentText == null )
				{
					this.m_ELabel_ContentText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject, "E_Bag/EG_MoveItem/Item_TowerBuy/EImage_TowerBuyShow/ELabel_Content");
				}
				return this.m_ELabel_ContentText;
			}
		}

		public UITextLocalizeMonoView ELabel_ContentUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_ContentUITextLocalizeMonoView == null )
				{
					this.m_ELabel_ContentUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "E_Bag/EG_MoveItem/Item_TowerBuy/EImage_TowerBuyShow/ELabel_Content");
				}
				return this.m_ELabel_ContentUITextLocalizeMonoView;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_Content12TextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_Content12TextMeshProUGUI == null )
				{
					this.m_ELabel_Content12TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_Bag/EG_MoveItem/Item_TowerBuy/EImage_TowerBuyShow/ELabel_Content12");
				}
				return this.m_ELabel_Content12TextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Image EImage_PurchasedImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EImage_PurchasedImage == null )
				{
					this.m_EImage_PurchasedImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Bag/EG_MoveItem/Item_TowerBuy/EImage_TowerBuyShow/EImage_Purchased");
				}
				return this.m_EImage_PurchasedImage;
			}
		}

		public UnityEngine.UI.Image EImage_ReplaceImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EImage_ReplaceImage == null )
				{
					this.m_EImage_ReplaceImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Bag/EG_MoveItem/Item_TowerBuy/EImage_TowerBuyShow/EImage_Replace");
				}
				return this.m_EImage_ReplaceImage;
			}
		}

		public UnityEngine.UI.Image E_iconImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_iconImage == null )
				{
					this.m_E_iconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Bag/EG_MoveItem/Item_TowerBuy/E_icon");
				}
				return this.m_E_iconImage;
			}
		}

		public UnityEngine.UI.Button EButton_SelectButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_SelectButton == null )
				{
					this.m_EButton_SelectButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_Bag/EG_MoveItem/Item_TowerBuy/EButton_Select");
				}
				return this.m_EButton_SelectButton;
			}
		}

		public UnityEngine.UI.Image EButton_SelectImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_SelectImage == null )
				{
					this.m_EButton_SelectImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Bag/EG_MoveItem/Item_TowerBuy/EButton_Select");
				}
				return this.m_EButton_SelectImage;
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
			this.m_E_QuitBattleButton = null;
			this.m_E_QuitBattleImage = null;
			this.m_ELoopScrollList_BattleDeckItemLoopHorizontalScrollRect = null;
			this.m_ELoopScrollList_TowerCardItemLoopHorizontalScrollRect = null;
			this.m_EG_MoveItemRectTransform = null;
			this.m_E_NoneImage = null;
			this.m_EImage_TowerBuyShowImage = null;
			this.m_E_BoxImage = null;
			this.m_EImage_LowImage = null;
			this.m_EImage_MiddleImage = null;
			this.m_EImage_HighImage = null;
			this.m_EButton_IconButton = null;
			this.m_EButton_IconImage = null;
			this.m_EG_IconStarRectTransform = null;
			this.m_E_IconStar1Image = null;
			this.m_E_IconStar2Image = null;
			this.m_E_IconStar3Image = null;
			this.m_EButton_nameTextMeshProUGUI = null;
			this.m_EImage_Label1Image = null;
			this.m_ELabel_Label1TextMeshProUGUI = null;
			this.m_EImage_Label2Image = null;
			this.m_ELabel_Label2TextMeshProUGUI = null;
			this.m_EImage_Label3Image = null;
			this.m_ELabel_Label3TextMeshProUGUI = null;
			this.m_EImage_BuyBGImage = null;
			this.m_ELabel_BuyText = null;
			this.m_ELabel_BuyUITextLocalizeMonoView = null;
			this.m_EButton_BuyButton = null;
			this.m_EButton_BuyImage = null;
			this.m_ELabel_UnableBuy_iconImage = null;
			this.m_ELabel_ContentText = null;
			this.m_ELabel_ContentUITextLocalizeMonoView = null;
			this.m_ELabel_Content12TextMeshProUGUI = null;
			this.m_EImage_PurchasedImage = null;
			this.m_EImage_ReplaceImage = null;
			this.m_E_iconImage = null;
			this.m_EButton_SelectButton = null;
			this.m_EButton_SelectImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_BG_ClickButton = null;
		private UnityEngine.UI.Image m_E_BG_ClickImage = null;
		private UnityEngine.RectTransform m_EG_bgARRectTransform = null;
		private BlurBackground.TranslucentImage m_EG_bgARTranslucentImage = null;
		private UnityEngine.RectTransform m_EG_bgRectTransform = null;
		private UnityEngine.UI.Image m_EG_bgImage = null;
		private UnityEngine.UI.Button m_E_QuitBattleButton = null;
		private UnityEngine.UI.Image m_E_QuitBattleImage = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopScrollList_BattleDeckItemLoopHorizontalScrollRect = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopScrollList_TowerCardItemLoopHorizontalScrollRect = null;
		private UnityEngine.RectTransform m_EG_MoveItemRectTransform = null;
		private UnityEngine.UI.Image m_E_NoneImage = null;
		private UnityEngine.UI.Image m_EImage_TowerBuyShowImage = null;
		private UnityEngine.UI.Image m_E_BoxImage = null;
		private UnityEngine.UI.Image m_EImage_LowImage = null;
		private UnityEngine.UI.Image m_EImage_MiddleImage = null;
		private UnityEngine.UI.Image m_EImage_HighImage = null;
		private UnityEngine.UI.Button m_EButton_IconButton = null;
		private UnityEngine.UI.Image m_EButton_IconImage = null;
		private UnityEngine.RectTransform m_EG_IconStarRectTransform = null;
		private UnityEngine.UI.Image m_E_IconStar1Image = null;
		private UnityEngine.UI.Image m_E_IconStar2Image = null;
		private UnityEngine.UI.Image m_E_IconStar3Image = null;
		private TMPro.TextMeshProUGUI m_EButton_nameTextMeshProUGUI = null;
		private UnityEngine.UI.Image m_EImage_Label1Image = null;
		private TMPro.TextMeshProUGUI m_ELabel_Label1TextMeshProUGUI = null;
		private UnityEngine.UI.Image m_EImage_Label2Image = null;
		private TMPro.TextMeshProUGUI m_ELabel_Label2TextMeshProUGUI = null;
		private UnityEngine.UI.Image m_EImage_Label3Image = null;
		private TMPro.TextMeshProUGUI m_ELabel_Label3TextMeshProUGUI = null;
		private UnityEngine.UI.Image m_EImage_BuyBGImage = null;
		private UnityEngine.UI.Text m_ELabel_BuyText = null;
		private UITextLocalizeMonoView m_ELabel_BuyUITextLocalizeMonoView = null;
		private UnityEngine.UI.Button m_EButton_BuyButton = null;
		private UnityEngine.UI.Image m_EButton_BuyImage = null;
		private UnityEngine.UI.Image m_ELabel_UnableBuy_iconImage = null;
		private UnityEngine.UI.Text m_ELabel_ContentText = null;
		private UITextLocalizeMonoView m_ELabel_ContentUITextLocalizeMonoView = null;
		private TMPro.TextMeshProUGUI m_ELabel_Content12TextMeshProUGUI = null;
		private UnityEngine.UI.Image m_EImage_PurchasedImage = null;
		private UnityEngine.UI.Image m_EImage_ReplaceImage = null;
		private UnityEngine.UI.Image m_E_iconImage = null;
		private UnityEngine.UI.Button m_EButton_SelectButton = null;
		private UnityEngine.UI.Image m_EButton_SelectImage = null;
		public Transform uiTransform = null;
	}
}
