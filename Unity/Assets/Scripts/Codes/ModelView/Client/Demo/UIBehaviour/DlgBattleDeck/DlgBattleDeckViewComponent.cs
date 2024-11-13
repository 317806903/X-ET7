
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgBattleDeck))]
	[EnableMethod]
	public class DlgBattleDeckViewComponent : Entity, IAwake, IDestroy
	{
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
					this.m_EG_bgARRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "E_HomePage/BG/EG_bgAR");
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
					this.m_EG_bgARTranslucentImage = UIFindHelper.FindDeepChild<BlurBackground.TranslucentImage>(this.uiTransform.gameObject, "E_HomePage/BG/EG_bgAR");
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
					this.m_EG_bgRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "E_HomePage/BG/EG_bg");
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
					this.m_EG_bgImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_HomePage/BG/EG_bg");
				}
				return this.m_EG_bgImage;
			}
		}

		public EPage_BattleDeckSkill EPage_BattleDeckSkill
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_epage_battledeckskill == null )
				{
					Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject, "E_HomePage/EPage_BattleDeckSkill");
					this.m_epage_battledeckskill = this.AddChild<EPage_BattleDeckSkill, Transform>(subTrans);
				}
				return this.m_epage_battledeckskill;
			}
		}

		public EPage_BattleDeckTower EPage_BattleDeckTower
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_epage_battledecktower == null )
				{
					Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject, "E_HomePage/EPage_BattleDeckTower");
					this.m_epage_battledecktower = this.AddChild<EPage_BattleDeckTower, Transform>(subTrans);
				}
				return this.m_epage_battledecktower;
			}
		}

		public UnityEngine.UI.Button ETabSelectBtnTowerButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETabSelectBtnTowerButton == null )
				{
					this.m_ETabSelectBtnTowerButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_HomePage/E_play/E_GameMode/titleBK/TabTower/ETabSelectBtnTower");
				}
				return this.m_ETabSelectBtnTowerButton;
			}
		}

		public UnityEngine.UI.Image ETabSelectBtnTowerImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETabSelectBtnTowerImage == null )
				{
					this.m_ETabSelectBtnTowerImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_HomePage/E_play/E_GameMode/titleBK/TabTower/ETabSelectBtnTower");
				}
				return this.m_ETabSelectBtnTowerImage;
			}
		}

		public UnityEngine.UI.Button ETabUnSelectBtnTowerButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETabUnSelectBtnTowerButton == null )
				{
					this.m_ETabUnSelectBtnTowerButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_HomePage/E_play/E_GameMode/titleBK/TabTower/ETabUnSelectBtnTower");
				}
				return this.m_ETabUnSelectBtnTowerButton;
			}
		}

		public UnityEngine.UI.Image ETabUnSelectBtnTowerImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETabUnSelectBtnTowerImage == null )
				{
					this.m_ETabUnSelectBtnTowerImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_HomePage/E_play/E_GameMode/titleBK/TabTower/ETabUnSelectBtnTower");
				}
				return this.m_ETabUnSelectBtnTowerImage;
			}
		}

		public UnityEngine.UI.Button ETabSelectBtnSkillButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETabSelectBtnSkillButton == null )
				{
					this.m_ETabSelectBtnSkillButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_HomePage/E_play/E_GameMode/titleBK/TabSkill/ETabSelectBtnSkill");
				}
				return this.m_ETabSelectBtnSkillButton;
			}
		}

		public UnityEngine.UI.Image ETabSelectBtnSkillImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETabSelectBtnSkillImage == null )
				{
					this.m_ETabSelectBtnSkillImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_HomePage/E_play/E_GameMode/titleBK/TabSkill/ETabSelectBtnSkill");
				}
				return this.m_ETabSelectBtnSkillImage;
			}
		}

		public UnityEngine.UI.Button ETabUnSelectBtnSkillButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETabUnSelectBtnSkillButton == null )
				{
					this.m_ETabUnSelectBtnSkillButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_HomePage/E_play/E_GameMode/titleBK/TabSkill/ETabUnSelectBtnSkill");
				}
				return this.m_ETabUnSelectBtnSkillButton;
			}
		}

		public UnityEngine.UI.Image ETabUnSelectBtnSkillImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETabUnSelectBtnSkillImage == null )
				{
					this.m_ETabUnSelectBtnSkillImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_HomePage/E_play/E_GameMode/titleBK/TabSkill/ETabUnSelectBtnSkill");
				}
				return this.m_ETabUnSelectBtnSkillImage;
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
					this.m_E_QuitBattleButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_HomePage/E_QuitBattle");
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
					this.m_E_QuitBattleImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_HomePage/E_QuitBattle");
				}
				return this.m_E_QuitBattleImage;
			}
		}

		public void DestroyWidget()
		{
			this.m_EG_bgARRectTransform = null;
			this.m_EG_bgARTranslucentImage = null;
			this.m_EG_bgRectTransform = null;
			this.m_EG_bgImage = null;
			this.m_epage_battledeckskill?.Dispose();
			this.m_epage_battledeckskill = null;
			this.m_epage_battledecktower?.Dispose();
			this.m_epage_battledecktower = null;
			this.m_ETabSelectBtnTowerButton = null;
			this.m_ETabSelectBtnTowerImage = null;
			this.m_ETabUnSelectBtnTowerButton = null;
			this.m_ETabUnSelectBtnTowerImage = null;
			this.m_ETabSelectBtnSkillButton = null;
			this.m_ETabSelectBtnSkillImage = null;
			this.m_ETabUnSelectBtnSkillButton = null;
			this.m_ETabUnSelectBtnSkillImage = null;
			this.m_E_QuitBattleButton = null;
			this.m_E_QuitBattleImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.RectTransform m_EG_bgARRectTransform = null;
		private BlurBackground.TranslucentImage m_EG_bgARTranslucentImage = null;
		private UnityEngine.RectTransform m_EG_bgRectTransform = null;
		private UnityEngine.UI.Image m_EG_bgImage = null;
		private EPage_BattleDeckSkill m_epage_battledeckskill = null;
		private EPage_BattleDeckTower m_epage_battledecktower = null;
		private UnityEngine.UI.Button m_ETabSelectBtnTowerButton = null;
		private UnityEngine.UI.Image m_ETabSelectBtnTowerImage = null;
		private UnityEngine.UI.Button m_ETabUnSelectBtnTowerButton = null;
		private UnityEngine.UI.Image m_ETabUnSelectBtnTowerImage = null;
		private UnityEngine.UI.Button m_ETabSelectBtnSkillButton = null;
		private UnityEngine.UI.Image m_ETabSelectBtnSkillImage = null;
		private UnityEngine.UI.Button m_ETabUnSelectBtnSkillButton = null;
		private UnityEngine.UI.Image m_ETabUnSelectBtnSkillImage = null;
		private UnityEngine.UI.Button m_E_QuitBattleButton = null;
		private UnityEngine.UI.Image m_E_QuitBattleImage = null;
		public Transform uiTransform = null;
	}
}
