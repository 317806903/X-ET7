
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
					this.m_ELoopScrollList_SkillLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject, "EGRoot/EGSkillRoot/ELoopScrollList_Skill");
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

		public void DestroyWidget()
		{
			this.m_EGRootRectTransform = null;
			this.m_EGSkillRootRectTransform = null;
			this.m_ELoopScrollList_SkillLoopHorizontalScrollRect = null;
			this.m_EButton_RestoreEnergyButton = null;
			this.m_EButton_RestoreEnergyImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.RectTransform m_EGRootRectTransform = null;
		private UnityEngine.RectTransform m_EGSkillRootRectTransform = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopScrollList_SkillLoopHorizontalScrollRect = null;
		private UnityEngine.UI.Button m_EButton_RestoreEnergyButton = null;
		private UnityEngine.UI.Image m_EButton_RestoreEnergyImage = null;
		public Transform uiTransform = null;
	}
}
