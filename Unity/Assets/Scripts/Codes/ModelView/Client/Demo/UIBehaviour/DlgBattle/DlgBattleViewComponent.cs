
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgBattle))]
	[EnableMethod]
	public class DlgBattleViewComponent : Entity, IAwake, IDestroy
	{
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
					this.m_ELoopScrollList_TowerLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject, "GameObject/TowerRoot/ELoopScrollList_Tower");
				}
				return this.m_ELoopScrollList_TowerLoopHorizontalScrollRect;
			}
		}

		public TMPro.TMP_InputField E_InputFieldCreateActionTowerTMP_InputField
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_InputFieldCreateActionTowerTMP_InputField == null )
				{
					this.m_E_InputFieldCreateActionTowerTMP_InputField = UIFindHelper.FindDeepChild<TMPro.TMP_InputField>(this.uiTransform.gameObject, "GameObject/TowerRoot/E_InputFieldCreateActionTower");
				}
				return this.m_E_InputFieldCreateActionTowerTMP_InputField;
			}
		}

		public UnityEngine.UI.Image E_InputFieldCreateActionTowerImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_InputFieldCreateActionTowerImage == null )
				{
					this.m_E_InputFieldCreateActionTowerImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "GameObject/TowerRoot/E_InputFieldCreateActionTower");
				}
				return this.m_E_InputFieldCreateActionTowerImage;
			}
		}

		public TMPro.TMP_InputField E_InputFieldMatchTowerTMP_InputField
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_InputFieldMatchTowerTMP_InputField == null )
				{
					this.m_E_InputFieldMatchTowerTMP_InputField = UIFindHelper.FindDeepChild<TMPro.TMP_InputField>(this.uiTransform.gameObject, "GameObject/TowerRoot/E_InputFieldMatchTower");
				}
				return this.m_E_InputFieldMatchTowerTMP_InputField;
			}
		}

		public UnityEngine.UI.Image E_InputFieldMatchTowerImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_InputFieldMatchTowerImage == null )
				{
					this.m_E_InputFieldMatchTowerImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "GameObject/TowerRoot/E_InputFieldMatchTower");
				}
				return this.m_E_InputFieldMatchTowerImage;
			}
		}

		public UnityEngine.UI.LoopHorizontalScrollRect ELoopScrollList_MonsterLoopHorizontalScrollRect
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELoopScrollList_MonsterLoopHorizontalScrollRect == null )
				{
					this.m_ELoopScrollList_MonsterLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject, "GameObject/MonsterRoot/ELoopScrollList_Monster");
				}
				return this.m_ELoopScrollList_MonsterLoopHorizontalScrollRect;
			}
		}

		public TMPro.TextMeshProUGUI E_MonsterOnceCountTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_MonsterOnceCountTextMeshProUGUI == null )
				{
					this.m_E_MonsterOnceCountTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "GameObject/MonsterRoot/E_MonsterOnceCount");
				}
				return this.m_E_MonsterOnceCountTextMeshProUGUI;
			}
		}

		public TMPro.TMP_InputField E_InputFieldTMP_InputField
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_InputFieldTMP_InputField == null )
				{
					this.m_E_InputFieldTMP_InputField = UIFindHelper.FindDeepChild<TMPro.TMP_InputField>(this.uiTransform.gameObject, "GameObject/MonsterRoot/E_InputField");
				}
				return this.m_E_InputFieldTMP_InputField;
			}
		}

		public UnityEngine.UI.Image E_InputFieldImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_InputFieldImage == null )
				{
					this.m_E_InputFieldImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "GameObject/MonsterRoot/E_InputField");
				}
				return this.m_E_InputFieldImage;
			}
		}

		public TMPro.TMP_InputField E_InputFieldCreateActionTMP_InputField
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_InputFieldCreateActionTMP_InputField == null )
				{
					this.m_E_InputFieldCreateActionTMP_InputField = UIFindHelper.FindDeepChild<TMPro.TMP_InputField>(this.uiTransform.gameObject, "GameObject/MonsterRoot/E_InputFieldCreateAction");
				}
				return this.m_E_InputFieldCreateActionTMP_InputField;
			}
		}

		public UnityEngine.UI.Image E_InputFieldCreateActionImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_InputFieldCreateActionImage == null )
				{
					this.m_E_InputFieldCreateActionImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "GameObject/MonsterRoot/E_InputFieldCreateAction");
				}
				return this.m_E_InputFieldCreateActionImage;
			}
		}

		public TMPro.TMP_InputField E_InputFieldMatchMonsterTMP_InputField
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_InputFieldMatchMonsterTMP_InputField == null )
				{
					this.m_E_InputFieldMatchMonsterTMP_InputField = UIFindHelper.FindDeepChild<TMPro.TMP_InputField>(this.uiTransform.gameObject, "GameObject/MonsterRoot/E_InputFieldMatchMonster");
				}
				return this.m_E_InputFieldMatchMonsterTMP_InputField;
			}
		}

		public UnityEngine.UI.Image E_InputFieldMatchMonsterImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_InputFieldMatchMonsterImage == null )
				{
					this.m_E_InputFieldMatchMonsterImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "GameObject/MonsterRoot/E_InputFieldMatchMonster");
				}
				return this.m_E_InputFieldMatchMonsterImage;
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
					this.m_E_QuitBattleButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "GameObject/E_QuitBattle");
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
					this.m_E_QuitBattleImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "GameObject/E_QuitBattle");
				}
				return this.m_E_QuitBattleImage;
			}
		}

		public UnityEngine.RectTransform EGClearRootRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EGClearRootRectTransform == null )
				{
					this.m_EGClearRootRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "GameObject/EGClearRoot");
				}
				return this.m_EGClearRootRectTransform;
			}
		}

		public UnityEngine.UI.Button EButton_ClearAllMonsterButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_ClearAllMonsterButton == null )
				{
					this.m_EButton_ClearAllMonsterButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "GameObject/EGClearRoot/EButton_ClearAllMonster");
				}
				return this.m_EButton_ClearAllMonsterButton;
			}
		}

		public UnityEngine.UI.Image EButton_ClearAllMonsterImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_ClearAllMonsterImage == null )
				{
					this.m_EButton_ClearAllMonsterImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "GameObject/EGClearRoot/EButton_ClearAllMonster");
				}
				return this.m_EButton_ClearAllMonsterImage;
			}
		}

		public UnityEngine.UI.Button EButton_ClearMyTowerButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_ClearMyTowerButton == null )
				{
					this.m_EButton_ClearMyTowerButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "GameObject/EGClearRoot/EButton_ClearMyTower");
				}
				return this.m_EButton_ClearMyTowerButton;
			}
		}

		public UnityEngine.UI.Image EButton_ClearMyTowerImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_ClearMyTowerImage == null )
				{
					this.m_EButton_ClearMyTowerImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "GameObject/EGClearRoot/EButton_ClearMyTower");
				}
				return this.m_EButton_ClearMyTowerImage;
			}
		}

		public UnityEngine.UI.Image E_TipNodeImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_TipNodeImage == null )
				{
					this.m_E_TipNodeImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "GameObject/TipNode/E_TipNode");
				}
				return this.m_E_TipNodeImage;
			}
		}

		public TMPro.TextMeshProUGUI E_TipTextTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_TipTextTextMeshProUGUI == null )
				{
					this.m_E_TipTextTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "GameObject/TipNode/E_TipNode/E_TipText");
				}
				return this.m_E_TipTextTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Button E_GameSettingButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_GameSettingButton == null )
				{
					this.m_E_GameSettingButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "GameObject/E_GameSetting");
				}
				return this.m_E_GameSettingButton;
			}
		}

		public UnityEngine.UI.Image E_GameSettingImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_GameSettingImage == null )
				{
					this.m_E_GameSettingImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "GameObject/E_GameSetting");
				}
				return this.m_E_GameSettingImage;
			}
		}

		public void DestroyWidget()
		{
			this.m_ELoopScrollList_TowerLoopHorizontalScrollRect = null;
			this.m_E_InputFieldCreateActionTowerTMP_InputField = null;
			this.m_E_InputFieldCreateActionTowerImage = null;
			this.m_E_InputFieldMatchTowerTMP_InputField = null;
			this.m_E_InputFieldMatchTowerImage = null;
			this.m_ELoopScrollList_MonsterLoopHorizontalScrollRect = null;
			this.m_E_MonsterOnceCountTextMeshProUGUI = null;
			this.m_E_InputFieldTMP_InputField = null;
			this.m_E_InputFieldImage = null;
			this.m_E_InputFieldCreateActionTMP_InputField = null;
			this.m_E_InputFieldCreateActionImage = null;
			this.m_E_InputFieldMatchMonsterTMP_InputField = null;
			this.m_E_InputFieldMatchMonsterImage = null;
			this.m_E_QuitBattleButton = null;
			this.m_E_QuitBattleImage = null;
			this.m_EGClearRootRectTransform = null;
			this.m_EButton_ClearAllMonsterButton = null;
			this.m_EButton_ClearAllMonsterImage = null;
			this.m_EButton_ClearMyTowerButton = null;
			this.m_EButton_ClearMyTowerImage = null;
			this.m_E_TipNodeImage = null;
			this.m_E_TipTextTextMeshProUGUI = null;
			this.m_E_GameSettingButton = null;
			this.m_E_GameSettingImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopScrollList_TowerLoopHorizontalScrollRect = null;
		private TMPro.TMP_InputField m_E_InputFieldCreateActionTowerTMP_InputField = null;
		private UnityEngine.UI.Image m_E_InputFieldCreateActionTowerImage = null;
		private TMPro.TMP_InputField m_E_InputFieldMatchTowerTMP_InputField = null;
		private UnityEngine.UI.Image m_E_InputFieldMatchTowerImage = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopScrollList_MonsterLoopHorizontalScrollRect = null;
		private TMPro.TextMeshProUGUI m_E_MonsterOnceCountTextMeshProUGUI = null;
		private TMPro.TMP_InputField m_E_InputFieldTMP_InputField = null;
		private UnityEngine.UI.Image m_E_InputFieldImage = null;
		private TMPro.TMP_InputField m_E_InputFieldCreateActionTMP_InputField = null;
		private UnityEngine.UI.Image m_E_InputFieldCreateActionImage = null;
		private TMPro.TMP_InputField m_E_InputFieldMatchMonsterTMP_InputField = null;
		private UnityEngine.UI.Image m_E_InputFieldMatchMonsterImage = null;
		private UnityEngine.UI.Button m_E_QuitBattleButton = null;
		private UnityEngine.UI.Image m_E_QuitBattleImage = null;
		private UnityEngine.RectTransform m_EGClearRootRectTransform = null;
		private UnityEngine.UI.Button m_EButton_ClearAllMonsterButton = null;
		private UnityEngine.UI.Image m_EButton_ClearAllMonsterImage = null;
		private UnityEngine.UI.Button m_EButton_ClearMyTowerButton = null;
		private UnityEngine.UI.Image m_EButton_ClearMyTowerImage = null;
		private UnityEngine.UI.Image m_E_TipNodeImage = null;
		private TMPro.TextMeshProUGUI m_E_TipTextTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_E_GameSettingButton = null;
		private UnityEngine.UI.Image m_E_GameSettingImage = null;
		public Transform uiTransform = null;
	}
}
