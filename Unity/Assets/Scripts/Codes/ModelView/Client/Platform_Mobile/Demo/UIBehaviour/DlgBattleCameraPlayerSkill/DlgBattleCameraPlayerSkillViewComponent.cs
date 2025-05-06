
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgBattleCameraPlayerSkill))]
	[EnableMethod]
	public class DlgBattleCameraPlayerSkillViewComponent : Entity, IAwake, IDestroy
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

		public UnityEngine.UI.Image EImageAimImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EImageAimImage == null )
				{
					this.m_EImageAimImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRoot/EImageAim");
				}
				return this.m_EImageAimImage;
			}
		}

		public UnityEngine.RectTransform EGSkillRootTmpRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EGSkillRootTmpRectTransform == null )
				{
					this.m_EGSkillRootTmpRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGRoot/EGSkillRootTmp");
				}
				return this.m_EGSkillRootTmpRectTransform;
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
					this.m_EGSkillRootRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGRoot/EGSkillRootTmp/EGSkillRoot");
				}
				return this.m_EGSkillRootRectTransform;
			}
		}

		public UnityEngine.UI.LoopHorizontalScrollRect ELoopScrollList_SkillLoopHorizontalScrollRect
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELoopScrollList_SkillLoopHorizontalScrollRect == null )
				{
					this.m_ELoopScrollList_SkillLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject, "EGRoot/EGSkillRootTmp/EGSkillRoot/ELoopScrollList_Skill");
				}
				return this.m_ELoopScrollList_SkillLoopHorizontalScrollRect;
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
					this.m_EButton_RestoreEnergyButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGRoot/EGSkillRootTmp/EGSkillRoot/EButton_RestoreEnergy");
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
					this.m_EButton_RestoreEnergyImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRoot/EGSkillRootTmp/EGSkillRoot/EButton_RestoreEnergy");
				}
				return this.m_EButton_RestoreEnergyImage;
			}
		}

		public UnityEngine.RectTransform EGAimRootRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EGAimRootRectTransform == null )
				{
					this.m_EGAimRootRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGRoot/EGAimRoot");
				}
				return this.m_EGAimRootRectTransform;
			}
		}

		public UnityEngine.UI.Image ESprite_AimImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ESprite_AimImage == null )
				{
					this.m_ESprite_AimImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRoot/EGAimRoot/ESprite_Aim");
				}
				return this.m_ESprite_AimImage;
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
			this.m_EImageAimImage = null;
			this.m_EGSkillRootTmpRectTransform = null;
			this.m_EGSkillRootRectTransform = null;
			this.m_ELoopScrollList_SkillLoopHorizontalScrollRect = null;
			this.m_EButton_RestoreEnergyButton = null;
			this.m_EButton_RestoreEnergyImage = null;
			this.m_EGAimRootRectTransform = null;
			this.m_ESprite_AimImage = null;
			this.m_EG_OpenAnimationRectTransform = null;
			this.m_EG_CloseAnimationRectTransform = null;
			this.uiTransform = null;
		}

		private UnityEngine.RectTransform m_EGRootRectTransform = null;
		private UnityEngine.UI.Image m_EImageAimImage = null;
		private UnityEngine.RectTransform m_EGSkillRootTmpRectTransform = null;
		private UnityEngine.RectTransform m_EGSkillRootRectTransform = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopScrollList_SkillLoopHorizontalScrollRect = null;
		private UnityEngine.UI.Button m_EButton_RestoreEnergyButton = null;
		private UnityEngine.UI.Image m_EButton_RestoreEnergyImage = null;
		private UnityEngine.RectTransform m_EGAimRootRectTransform = null;
		private UnityEngine.UI.Image m_ESprite_AimImage = null;
		private UnityEngine.RectTransform m_EG_OpenAnimationRectTransform = null;
		private UnityEngine.RectTransform m_EG_CloseAnimationRectTransform = null;
		public Transform uiTransform = null;
	}
}
