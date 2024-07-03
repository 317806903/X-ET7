
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

		public UnityEngine.UI.Button EButton_TokenArcadeCoinButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_TokenArcadeCoinButton == null )
				{
					this.m_EButton_TokenArcadeCoinButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EG_Root/EG_CoinList/EButton_TokenArcadeCoin");
				}
				return this.m_EButton_TokenArcadeCoinButton;
			}
		}

		public UnityEngine.UI.Image EButton_TokenArcadeCoinImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_TokenArcadeCoinImage == null )
				{
					this.m_EButton_TokenArcadeCoinImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_Root/EG_CoinList/EButton_TokenArcadeCoin");
				}
				return this.m_EButton_TokenArcadeCoinImage;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_TokenArcadeCoinNumTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_TokenArcadeCoinNumTextMeshProUGUI == null )
				{
					this.m_ELabel_TokenArcadeCoinNumTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_Root/EG_CoinList/EButton_TokenArcadeCoin/ELabel_TokenArcadeCoinNum");
				}
				return this.m_ELabel_TokenArcadeCoinNumTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Button EButton_TokenDiamondButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_TokenDiamondButton == null )
				{
					this.m_EButton_TokenDiamondButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EG_Root/EG_CoinList/EButton_TokenDiamond");
				}
				return this.m_EButton_TokenDiamondButton;
			}
		}

		public UnityEngine.UI.Image EButton_TokenDiamondImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_TokenDiamondImage == null )
				{
					this.m_EButton_TokenDiamondImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_Root/EG_CoinList/EButton_TokenDiamond");
				}
				return this.m_EButton_TokenDiamondImage;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_TokenDiamondNumTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_TokenDiamondNumTextMeshProUGUI == null )
				{
					this.m_ELabel_TokenDiamondNumTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_Root/EG_CoinList/EButton_TokenDiamond/ELabel_TokenDiamondNum");
				}
				return this.m_ELabel_TokenDiamondNumTextMeshProUGUI;
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
			this.m_EButton_TokenArcadeCoinButton = null;
			this.m_EButton_TokenArcadeCoinImage = null;
			this.m_ELabel_TokenArcadeCoinNumTextMeshProUGUI = null;
			this.m_EButton_TokenDiamondButton = null;
			this.m_EButton_TokenDiamondImage = null;
			this.m_ELabel_TokenDiamondNumTextMeshProUGUI = null;
			this.m_EButton_PhysicalStrengthButton = null;
			this.m_EButton_PhysicalStrengthImage = null;
			this.m_ELabel_PhysicalStrengthNumTextMeshProUGUI = null;
			this.uiTransform = null;
		}

		private UnityEngine.RectTransform m_EG_RootRectTransform = null;
		private UnityEngine.RectTransform m_EG_CoinListRectTransform = null;
		private UnityEngine.UI.Button m_EButton_TokenArcadeCoinButton = null;
		private UnityEngine.UI.Image m_EButton_TokenArcadeCoinImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_TokenArcadeCoinNumTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_EButton_TokenDiamondButton = null;
		private UnityEngine.UI.Image m_EButton_TokenDiamondImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_TokenDiamondNumTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_EButton_PhysicalStrengthButton = null;
		private UnityEngine.UI.Image m_EButton_PhysicalStrengthImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_PhysicalStrengthNumTextMeshProUGUI = null;
		public Transform uiTransform = null;
	}
}
