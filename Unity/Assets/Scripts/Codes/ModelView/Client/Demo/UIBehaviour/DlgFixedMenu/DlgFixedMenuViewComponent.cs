
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgFixedMenu))]
	[EnableMethod]
	public class DlgFixedMenuViewComponent : Entity, IAwake, IDestroy
	{
		public UnityEngine.RectTransform EG_RootRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_RootRectTransform == null )
				{
					this.m_EG_RootRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_Root");
				}
				return this.m_EG_RootRectTransform;
			}
		}

		public UnityEngine.RectTransform EG_CoinListRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_CoinListRectTransform == null )
				{
					this.m_EG_CoinListRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_Root/EG_CoinList");
				}
				return this.m_EG_CoinListRectTransform;
			}
		}

		public UnityEngine.UI.Button EButton_GoldCoinButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_GoldCoinButton == null )
				{
					this.m_EButton_GoldCoinButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EG_Root/EG_CoinList/EButton_GoldCoin");
				}
				return this.m_EButton_GoldCoinButton;
			}
		}

		public UnityEngine.UI.Image EButton_GoldCoinImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_GoldCoinImage == null )
				{
					this.m_EButton_GoldCoinImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_Root/EG_CoinList/EButton_GoldCoin");
				}
				return this.m_EButton_GoldCoinImage;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_GoldCoinNumTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_GoldCoinNumTextMeshProUGUI == null )
				{
					this.m_ELabel_GoldCoinNumTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_Root/EG_CoinList/EButton_GoldCoin/ELabel_GoldCoinNum");
				}
				return this.m_ELabel_GoldCoinNumTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Button EButton_PhysicalStrengthButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_PhysicalStrengthButton == null )
				{
					this.m_EButton_PhysicalStrengthButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EG_Root/EG_CoinList/EButton_PhysicalStrength");
				}
				return this.m_EButton_PhysicalStrengthButton;
			}
		}

		public UnityEngine.UI.Image EButton_PhysicalStrengthImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_PhysicalStrengthImage == null )
				{
					this.m_EButton_PhysicalStrengthImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_Root/EG_CoinList/EButton_PhysicalStrength");
				}
				return this.m_EButton_PhysicalStrengthImage;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_PhysicalStrengthNumTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_PhysicalStrengthNumTextMeshProUGUI == null )
				{
					this.m_ELabel_PhysicalStrengthNumTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_Root/EG_CoinList/EButton_PhysicalStrength/ELabel_PhysicalStrengthNum");
				}
				return this.m_ELabel_PhysicalStrengthNumTextMeshProUGUI;
			}
		}

		public void DestroyWidget()
		{
			this.m_EG_RootRectTransform = null;
			this.m_EG_CoinListRectTransform = null;
			this.m_EButton_GoldCoinButton = null;
			this.m_EButton_GoldCoinImage = null;
			this.m_ELabel_GoldCoinNumTextMeshProUGUI = null;
			this.m_EButton_PhysicalStrengthButton = null;
			this.m_EButton_PhysicalStrengthImage = null;
			this.m_ELabel_PhysicalStrengthNumTextMeshProUGUI = null;
			this.uiTransform = null;
		}

		private UnityEngine.RectTransform m_EG_RootRectTransform = null;
		private UnityEngine.RectTransform m_EG_CoinListRectTransform = null;
		private UnityEngine.UI.Button m_EButton_GoldCoinButton = null;
		private UnityEngine.UI.Image m_EButton_GoldCoinImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_GoldCoinNumTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_EButton_PhysicalStrengthButton = null;
		private UnityEngine.UI.Image m_EButton_PhysicalStrengthImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_PhysicalStrengthNumTextMeshProUGUI = null;
		public Transform uiTransform = null;
	}
}
