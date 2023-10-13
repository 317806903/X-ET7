
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
					this.m_ELoopScrollList_TowerLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject, "GameObject/ELoopScrollList_Tower");
				}
				return this.m_ELoopScrollList_TowerLoopHorizontalScrollRect;
			}
		}

		public UnityEngine.UI.LoopHorizontalScrollRect ELoopScrollList_TankLoopHorizontalScrollRect
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELoopScrollList_TankLoopHorizontalScrollRect == null )
				{
					this.m_ELoopScrollList_TankLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject, "GameObject/MonsterRoot/ELoopScrollList_Tank");
				}
				return this.m_ELoopScrollList_TankLoopHorizontalScrollRect;
			}
		}

		public TMPro.TextMeshProUGUI E_TankOnceCountTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_TankOnceCountTextMeshProUGUI == null )
				{
					this.m_E_TankOnceCountTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "GameObject/MonsterRoot/E_TankOnceCount");
				}
				return this.m_E_TankOnceCountTextMeshProUGUI;
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

		public UnityEngine.RectTransform EGSkillRootRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EGSkillRootRectTransform == null )
				{
					this.m_EGSkillRootRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "GameObject/EGSkillRoot");
				}
				return this.m_EGSkillRootRectTransform;
			}
		}

		public UnityEngine.UI.Image EGSkillRootImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EGSkillRootImage == null )
				{
					this.m_EGSkillRootImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "GameObject/EGSkillRoot");
				}
				return this.m_EGSkillRootImage;
			}
		}

		public UnityEngine.UI.Button EButton_Skill1Button
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_Skill1Button == null )
				{
					this.m_EButton_Skill1Button = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "GameObject/EGSkillRoot/EButton_Skill1");
				}
				return this.m_EButton_Skill1Button;
			}
		}

		public UnityEngine.UI.Image EButton_Skill1Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_Skill1Image == null )
				{
					this.m_EButton_Skill1Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "GameObject/EGSkillRoot/EButton_Skill1");
				}
				return this.m_EButton_Skill1Image;
			}
		}

		public UnityEngine.UI.Text ELabel_Skill1Text
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_Skill1Text == null )
				{
					this.m_ELabel_Skill1Text = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject, "GameObject/EGSkillRoot/EButton_Skill1/ELabel_Skill1");
				}
				return this.m_ELabel_Skill1Text;
			}
		}

		public UnityEngine.UI.Button EButton_Skill2Button
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_Skill2Button == null )
				{
					this.m_EButton_Skill2Button = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "GameObject/EGSkillRoot/EButton_Skill2");
				}
				return this.m_EButton_Skill2Button;
			}
		}

		public UnityEngine.UI.Image EButton_Skill2Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_Skill2Image == null )
				{
					this.m_EButton_Skill2Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "GameObject/EGSkillRoot/EButton_Skill2");
				}
				return this.m_EButton_Skill2Image;
			}
		}

		public UnityEngine.UI.Text ELabel_Skill2Text
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_Skill2Text == null )
				{
					this.m_ELabel_Skill2Text = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject, "GameObject/EGSkillRoot/EButton_Skill2/ELabel_Skill2");
				}
				return this.m_ELabel_Skill2Text;
			}
		}

		public UnityEngine.UI.Button EButton_Skill3Button
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_Skill3Button == null )
				{
					this.m_EButton_Skill3Button = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "GameObject/EGSkillRoot/EButton_Skill3");
				}
				return this.m_EButton_Skill3Button;
			}
		}

		public UnityEngine.UI.Image EButton_Skill3Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_Skill3Image == null )
				{
					this.m_EButton_Skill3Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "GameObject/EGSkillRoot/EButton_Skill3");
				}
				return this.m_EButton_Skill3Image;
			}
		}

		public UnityEngine.UI.Text ELabel_Skill3Text
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_Skill3Text == null )
				{
					this.m_ELabel_Skill3Text = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject, "GameObject/EGSkillRoot/EButton_Skill3/ELabel_Skill3");
				}
				return this.m_ELabel_Skill3Text;
			}
		}

		public UnityEngine.UI.Button EButton_Skill4Button
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_Skill4Button == null )
				{
					this.m_EButton_Skill4Button = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "GameObject/EGSkillRoot/EButton_Skill4");
				}
				return this.m_EButton_Skill4Button;
			}
		}

		public UnityEngine.UI.Image EButton_Skill4Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_Skill4Image == null )
				{
					this.m_EButton_Skill4Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "GameObject/EGSkillRoot/EButton_Skill4");
				}
				return this.m_EButton_Skill4Image;
			}
		}

		public UnityEngine.UI.Text ELabel_Skill4Text
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_Skill4Text == null )
				{
					this.m_ELabel_Skill4Text = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject, "GameObject/EGSkillRoot/EButton_Skill4/ELabel_Skill4");
				}
				return this.m_ELabel_Skill4Text;
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

		public UnityEngine.UI.Image EGClearRootImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EGClearRootImage == null )
				{
					this.m_EGClearRootImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "GameObject/EGClearRoot");
				}
				return this.m_EGClearRootImage;
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

		public void DestroyWidget()
		{
			this.m_ELoopScrollList_TowerLoopHorizontalScrollRect = null;
			this.m_ELoopScrollList_TankLoopHorizontalScrollRect = null;
			this.m_E_TankOnceCountTextMeshProUGUI = null;
			this.m_E_InputFieldTMP_InputField = null;
			this.m_E_InputFieldImage = null;
			this.m_E_QuitBattleButton = null;
			this.m_E_QuitBattleImage = null;
			this.m_EGSkillRootRectTransform = null;
			this.m_EGSkillRootImage = null;
			this.m_EButton_Skill1Button = null;
			this.m_EButton_Skill1Image = null;
			this.m_ELabel_Skill1Text = null;
			this.m_EButton_Skill2Button = null;
			this.m_EButton_Skill2Image = null;
			this.m_ELabel_Skill2Text = null;
			this.m_EButton_Skill3Button = null;
			this.m_EButton_Skill3Image = null;
			this.m_ELabel_Skill3Text = null;
			this.m_EButton_Skill4Button = null;
			this.m_EButton_Skill4Image = null;
			this.m_ELabel_Skill4Text = null;
			this.m_EGClearRootRectTransform = null;
			this.m_EGClearRootImage = null;
			this.m_EButton_ClearMyTowerButton = null;
			this.m_EButton_ClearMyTowerImage = null;
			this.m_EButton_ClearAllMonsterButton = null;
			this.m_EButton_ClearAllMonsterImage = null;
			this.m_E_TipNodeImage = null;
			this.m_E_TipTextTextMeshProUGUI = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopScrollList_TowerLoopHorizontalScrollRect = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopScrollList_TankLoopHorizontalScrollRect = null;
		private TMPro.TextMeshProUGUI m_E_TankOnceCountTextMeshProUGUI = null;
		private TMPro.TMP_InputField m_E_InputFieldTMP_InputField = null;
		private UnityEngine.UI.Image m_E_InputFieldImage = null;
		private UnityEngine.UI.Button m_E_QuitBattleButton = null;
		private UnityEngine.UI.Image m_E_QuitBattleImage = null;
		private UnityEngine.RectTransform m_EGSkillRootRectTransform = null;
		private UnityEngine.UI.Image m_EGSkillRootImage = null;
		private UnityEngine.UI.Button m_EButton_Skill1Button = null;
		private UnityEngine.UI.Image m_EButton_Skill1Image = null;
		private UnityEngine.UI.Text m_ELabel_Skill1Text = null;
		private UnityEngine.UI.Button m_EButton_Skill2Button = null;
		private UnityEngine.UI.Image m_EButton_Skill2Image = null;
		private UnityEngine.UI.Text m_ELabel_Skill2Text = null;
		private UnityEngine.UI.Button m_EButton_Skill3Button = null;
		private UnityEngine.UI.Image m_EButton_Skill3Image = null;
		private UnityEngine.UI.Text m_ELabel_Skill3Text = null;
		private UnityEngine.UI.Button m_EButton_Skill4Button = null;
		private UnityEngine.UI.Image m_EButton_Skill4Image = null;
		private UnityEngine.UI.Text m_ELabel_Skill4Text = null;
		private UnityEngine.RectTransform m_EGClearRootRectTransform = null;
		private UnityEngine.UI.Image m_EGClearRootImage = null;
		private UnityEngine.UI.Button m_EButton_ClearMyTowerButton = null;
		private UnityEngine.UI.Image m_EButton_ClearMyTowerImage = null;
		private UnityEngine.UI.Button m_EButton_ClearAllMonsterButton = null;
		private UnityEngine.UI.Image m_EButton_ClearAllMonsterImage = null;
		private UnityEngine.UI.Image m_E_TipNodeImage = null;
		private TMPro.TextMeshProUGUI m_E_TipTextTextMeshProUGUI = null;
		public Transform uiTransform = null;
	}
}
