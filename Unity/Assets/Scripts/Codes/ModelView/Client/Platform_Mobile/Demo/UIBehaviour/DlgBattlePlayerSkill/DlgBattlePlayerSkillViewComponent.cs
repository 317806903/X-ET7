
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgBattlePlayerSkill))]
	[EnableMethod]
	public class DlgBattlePlayerSkillViewComponent : Entity, IAwake, IDestroy
	{
		public UnityEngine.RectTransform EGRootRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EGRootRectTransform == null )
				{
					this.m_EGRootRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGRoot");
				}
				return this.m_EGRootRectTransform;
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
					this.m_EGSkillRootRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGRoot/EGSkillRoot");
				}
				return this.m_EGSkillRootRectTransform;
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
					this.m_EButton_Skill1Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRoot/EGSkillRoot/EButton_Skill1");
				}
				return this.m_EButton_Skill1Image;
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
					this.m_EButton_Skill2Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRoot/EGSkillRoot/EButton_Skill2");
				}
				return this.m_EButton_Skill2Image;
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
					this.m_EButton_Skill3Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRoot/EGSkillRoot/EButton_Skill3");
				}
				return this.m_EButton_Skill3Image;
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
					this.m_EButton_Skill4Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRoot/EGSkillRoot/EButton_Skill4");
				}
				return this.m_EButton_Skill4Image;
			}
		}

		public UnityEngine.UI.Button EButton_RestoreEnergyButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_RestoreEnergyButton == null )
				{
					this.m_EButton_RestoreEnergyButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGRoot/EGSkillRoot/EButton_RestoreEnergy");
				}
				return this.m_EButton_RestoreEnergyButton;
			}
		}

		public UnityEngine.UI.Image EButton_RestoreEnergyImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_RestoreEnergyImage == null )
				{
					this.m_EButton_RestoreEnergyImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRoot/EGSkillRoot/EButton_RestoreEnergy");
				}
				return this.m_EButton_RestoreEnergyImage;
			}
		}

		public UnityEngine.RectTransform EGDragRootRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EGDragRootRectTransform == null )
				{
					this.m_EGDragRootRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGRoot/EGDragRoot");
				}
				return this.m_EGDragRootRectTransform;
			}
		}

		public UnityEngine.UI.Image ESprite_DragBgImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ESprite_DragBgImage == null )
				{
					this.m_ESprite_DragBgImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRoot/EGDragRoot/ESprite_DragBg");
				}
				return this.m_ESprite_DragBgImage;
			}
		}

		public UnityEngine.UI.Image ESprite_DragOutRangeImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ESprite_DragOutRangeImage == null )
				{
					this.m_ESprite_DragOutRangeImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRoot/EGDragRoot/ESprite_DragOutRange");
				}
				return this.m_ESprite_DragOutRangeImage;
			}
		}

		public UnityEngine.UI.Image ESprite_DragInRangeImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ESprite_DragInRangeImage == null )
				{
					this.m_ESprite_DragInRangeImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRoot/EGDragRoot/ESprite_DragInRange");
				}
				return this.m_ESprite_DragInRangeImage;
			}
		}

		public UnityEngine.RectTransform EG_OpenAnimationRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_OpenAnimationRectTransform == null )
				{
					this.m_EG_OpenAnimationRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_OpenAnimation");
				}
				return this.m_EG_OpenAnimationRectTransform;
			}
		}

		public UnityEngine.RectTransform EG_CloseAnimationRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_CloseAnimationRectTransform == null )
				{
					this.m_EG_CloseAnimationRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_CloseAnimation");
				}
				return this.m_EG_CloseAnimationRectTransform;
			}
		}

		public void DestroyWidget()
		{
			this.m_EGRootRectTransform = null;
			this.m_EGSkillRootRectTransform = null;
			this.m_EButton_Skill1Image = null;
			this.m_EButton_Skill2Image = null;
			this.m_EButton_Skill3Image = null;
			this.m_EButton_Skill4Image = null;
			this.m_EButton_RestoreEnergyButton = null;
			this.m_EButton_RestoreEnergyImage = null;
			this.m_EGDragRootRectTransform = null;
			this.m_ESprite_DragBgImage = null;
			this.m_ESprite_DragOutRangeImage = null;
			this.m_ESprite_DragInRangeImage = null;
			this.m_EG_OpenAnimationRectTransform = null;
			this.m_EG_CloseAnimationRectTransform = null;
			this.uiTransform = null;
		}

		private UnityEngine.RectTransform m_EGRootRectTransform = null;
		private UnityEngine.RectTransform m_EGSkillRootRectTransform = null;
		private UnityEngine.UI.Image m_EButton_Skill1Image = null;
		private UnityEngine.UI.Image m_EButton_Skill2Image = null;
		private UnityEngine.UI.Image m_EButton_Skill3Image = null;
		private UnityEngine.UI.Image m_EButton_Skill4Image = null;
		private UnityEngine.UI.Button m_EButton_RestoreEnergyButton = null;
		private UnityEngine.UI.Image m_EButton_RestoreEnergyImage = null;
		private UnityEngine.RectTransform m_EGDragRootRectTransform = null;
		private UnityEngine.UI.Image m_ESprite_DragBgImage = null;
		private UnityEngine.UI.Image m_ESprite_DragOutRangeImage = null;
		private UnityEngine.UI.Image m_ESprite_DragInRangeImage = null;
		private UnityEngine.RectTransform m_EG_OpenAnimationRectTransform = null;
		private UnityEngine.RectTransform m_EG_CloseAnimationRectTransform = null;
		public Transform uiTransform = null;
	}
}
