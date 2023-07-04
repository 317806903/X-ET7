
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgBattleTower))]
	[EnableMethod]
	public  class DlgBattleTowerViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Image E_PutHomeAndMonsterPointImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_PutHomeAndMonsterPointImage == null )
     			{
		    		this.m_E_PutHomeAndMonsterPointImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_PutHomeAndMonsterPoint");
     			}
     			return this.m_E_PutHomeAndMonsterPointImage;
     		}
     	}

		public UnityEngine.UI.Button EButton_PutHomeButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_PutHomeButton == null )
     			{
		    		this.m_EButton_PutHomeButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_PutHomeAndMonsterPoint/EButton_PutHome");
     			}
     			return this.m_EButton_PutHomeButton;
     		}
     	}

		public UnityEngine.UI.Image EButton_PutHomeImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_PutHomeImage == null )
     			{
		    		this.m_EButton_PutHomeImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_PutHomeAndMonsterPoint/EButton_PutHome");
     			}
     			return this.m_EButton_PutHomeImage;
     		}
     	}

		public UnityEngine.UI.Text ELabel_PutHomeText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELabel_PutHomeText == null )
     			{
		    		this.m_ELabel_PutHomeText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_PutHomeAndMonsterPoint/EButton_PutHome/ELabel_PutHome");
     			}
     			return this.m_ELabel_PutHomeText;
     		}
     	}

		public UnityEngine.UI.Button EButton_PutMonsterPointButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_PutMonsterPointButton == null )
     			{
		    		this.m_EButton_PutMonsterPointButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_PutHomeAndMonsterPoint/EButton_PutMonsterPoint");
     			}
     			return this.m_EButton_PutMonsterPointButton;
     		}
     	}

		public UnityEngine.UI.Image EButton_PutMonsterPointImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_PutMonsterPointImage == null )
     			{
		    		this.m_EButton_PutMonsterPointImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_PutHomeAndMonsterPoint/EButton_PutMonsterPoint");
     			}
     			return this.m_EButton_PutMonsterPointImage;
     		}
     	}

		public UnityEngine.UI.Text ELabel_PutMonsterPointText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELabel_PutMonsterPointText == null )
     			{
		    		this.m_ELabel_PutMonsterPointText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_PutHomeAndMonsterPoint/EButton_PutMonsterPoint/ELabel_PutMonsterPoint");
     			}
     			return this.m_ELabel_PutMonsterPointText;
     		}
     	}

		public UnityEngine.UI.Image E_BattleImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_BattleImage == null )
     			{
		    		this.m_E_BattleImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Battle");
     			}
     			return this.m_E_BattleImage;
     		}
     	}

		public UnityEngine.UI.LoopHorizontalScrollRect ELoopScrollList_TowerLoopHorizontalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELoopScrollList_TowerLoopHorizontalScrollRect == null )
     			{
		    		this.m_ELoopScrollList_TowerLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject,"E_Battle/ELoopScrollList_Tower");
     			}
     			return this.m_ELoopScrollList_TowerLoopHorizontalScrollRect;
     		}
     	}

		public UnityEngine.UI.Text ELabel_TotalGoldText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELabel_TotalGoldText == null )
     			{
		    		this.m_ELabel_TotalGoldText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Battle/GameObject/ELabel_TotalGold");
     			}
     			return this.m_ELabel_TotalGoldText;
     		}
     	}

		public UnityEngine.UI.Text ELabel_LeftMonsterWaveText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELabel_LeftMonsterWaveText == null )
     			{
		    		this.m_ELabel_LeftMonsterWaveText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Battle/ELabel_LeftMonsterWave");
     			}
     			return this.m_ELabel_LeftMonsterWaveText;
     		}
     	}

		public UnityEngine.UI.Text ELabel_LeftTimeText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELabel_LeftTimeText == null )
     			{
		    		this.m_ELabel_LeftTimeText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Battle/ELabel_LeftTime");
     			}
     			return this.m_ELabel_LeftTimeText;
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
		    		this.m_EButton_BuyButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Battle/EButton_Buy");
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
		    		this.m_EButton_BuyImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Battle/EButton_Buy");
     			}
     			return this.m_EButton_BuyImage;
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
		    		this.m_ELabel_BuyText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Battle/EButton_Buy/ELabel_Buy");
     			}
     			return this.m_ELabel_BuyText;
     		}
     	}

		public UnityEngine.UI.Image E_TowerBuyShowImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_TowerBuyShowImage == null )
     			{
		    		this.m_E_TowerBuyShowImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Battle/E_TowerBuyShow");
     			}
     			return this.m_E_TowerBuyShowImage;
     		}
     	}

		public UnityEngine.UI.LoopHorizontalScrollRect ELoopScrollList_BuyLoopHorizontalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELoopScrollList_BuyLoopHorizontalScrollRect == null )
     			{
		    		this.m_ELoopScrollList_BuyLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject,"E_Battle/E_TowerBuyShow/ELoopScrollList_Buy");
     			}
     			return this.m_ELoopScrollList_BuyLoopHorizontalScrollRect;
     		}
     	}

		public UnityEngine.UI.Button EButton_RefreshButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_RefreshButton == null )
     			{
		    		this.m_EButton_RefreshButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Battle/E_TowerBuyShow/EButton_Refresh");
     			}
     			return this.m_EButton_RefreshButton;
     		}
     	}

		public UnityEngine.UI.Image EButton_RefreshImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_RefreshImage == null )
     			{
		    		this.m_EButton_RefreshImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Battle/E_TowerBuyShow/EButton_Refresh");
     			}
     			return this.m_EButton_RefreshImage;
     		}
     	}

		public UnityEngine.UI.Text ELabel_RefreshText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELabel_RefreshText == null )
     			{
		    		this.m_ELabel_RefreshText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Battle/E_TowerBuyShow/EButton_Refresh/ELabel_Refresh");
     			}
     			return this.m_ELabel_RefreshText;
     		}
     	}

		public UnityEngine.UI.Button EButton_BuyCloseButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_BuyCloseButton == null )
     			{
		    		this.m_EButton_BuyCloseButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Battle/E_TowerBuyShow/EButton_BuyClose");
     			}
     			return this.m_EButton_BuyCloseButton;
     		}
     	}

		public UnityEngine.UI.Image EButton_BuyCloseImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_BuyCloseImage == null )
     			{
		    		this.m_EButton_BuyCloseImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Battle/E_TowerBuyShow/EButton_BuyClose");
     			}
     			return this.m_EButton_BuyCloseImage;
     		}
     	}

		public UnityEngine.UI.Text ELabel_BuyCloseText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELabel_BuyCloseText == null )
     			{
		    		this.m_ELabel_BuyCloseText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Battle/E_TowerBuyShow/EButton_BuyClose/ELabel_BuyClose");
     			}
     			return this.m_ELabel_BuyCloseText;
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
		    		this.m_E_QuitBattleButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_QuitBattle");
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
		    		this.m_E_QuitBattleImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_QuitBattle");
     			}
     			return this.m_E_QuitBattleImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_PutHomeAndMonsterPointImage = null;
			this.m_EButton_PutHomeButton = null;
			this.m_EButton_PutHomeImage = null;
			this.m_ELabel_PutHomeText = null;
			this.m_EButton_PutMonsterPointButton = null;
			this.m_EButton_PutMonsterPointImage = null;
			this.m_ELabel_PutMonsterPointText = null;
			this.m_E_BattleImage = null;
			this.m_ELoopScrollList_TowerLoopHorizontalScrollRect = null;
			this.m_ELabel_TotalGoldText = null;
			this.m_ELabel_LeftMonsterWaveText = null;
			this.m_ELabel_LeftTimeText = null;
			this.m_EButton_BuyButton = null;
			this.m_EButton_BuyImage = null;
			this.m_ELabel_BuyText = null;
			this.m_E_TowerBuyShowImage = null;
			this.m_ELoopScrollList_BuyLoopHorizontalScrollRect = null;
			this.m_EButton_RefreshButton = null;
			this.m_EButton_RefreshImage = null;
			this.m_ELabel_RefreshText = null;
			this.m_EButton_BuyCloseButton = null;
			this.m_EButton_BuyCloseImage = null;
			this.m_ELabel_BuyCloseText = null;
			this.m_E_QuitBattleButton = null;
			this.m_E_QuitBattleImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Image m_E_PutHomeAndMonsterPointImage = null;
		private UnityEngine.UI.Button m_EButton_PutHomeButton = null;
		private UnityEngine.UI.Image m_EButton_PutHomeImage = null;
		private UnityEngine.UI.Text m_ELabel_PutHomeText = null;
		private UnityEngine.UI.Button m_EButton_PutMonsterPointButton = null;
		private UnityEngine.UI.Image m_EButton_PutMonsterPointImage = null;
		private UnityEngine.UI.Text m_ELabel_PutMonsterPointText = null;
		private UnityEngine.UI.Image m_E_BattleImage = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopScrollList_TowerLoopHorizontalScrollRect = null;
		private UnityEngine.UI.Text m_ELabel_TotalGoldText = null;
		private UnityEngine.UI.Text m_ELabel_LeftMonsterWaveText = null;
		private UnityEngine.UI.Text m_ELabel_LeftTimeText = null;
		private UnityEngine.UI.Button m_EButton_BuyButton = null;
		private UnityEngine.UI.Image m_EButton_BuyImage = null;
		private UnityEngine.UI.Text m_ELabel_BuyText = null;
		private UnityEngine.UI.Image m_E_TowerBuyShowImage = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopScrollList_BuyLoopHorizontalScrollRect = null;
		private UnityEngine.UI.Button m_EButton_RefreshButton = null;
		private UnityEngine.UI.Image m_EButton_RefreshImage = null;
		private UnityEngine.UI.Text m_ELabel_RefreshText = null;
		private UnityEngine.UI.Button m_EButton_BuyCloseButton = null;
		private UnityEngine.UI.Image m_EButton_BuyCloseImage = null;
		private UnityEngine.UI.Text m_ELabel_BuyCloseText = null;
		private UnityEngine.UI.Button m_E_QuitBattleButton = null;
		private UnityEngine.UI.Image m_E_QuitBattleImage = null;
		public Transform uiTransform = null;
	}
}
