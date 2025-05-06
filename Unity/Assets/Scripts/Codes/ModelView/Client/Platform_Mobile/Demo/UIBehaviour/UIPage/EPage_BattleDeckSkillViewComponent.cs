
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[EnableMethod]
	public class EPage_BattleDeckSkillViewComponent : Entity, ET.IAwake<UnityEngine.Transform>, IDestroy
	{
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
					this.m_ELoopScrollList_BattleDeckItemLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject, "E_Root/E_BattleDeck/info/list/ELoopScrollList_BattleDeckItem");
				}
				return this.m_ELoopScrollList_BattleDeckItemLoopHorizontalScrollRect;
			}
		}

		public UnityEngine.UI.LoopHorizontalScrollRect ELoopScrollList_BagCardItemLoopHorizontalScrollRect
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELoopScrollList_BagCardItemLoopHorizontalScrollRect == null )
				{
					this.m_ELoopScrollList_BagCardItemLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject, "E_Root/E_CardCollection/info/list/ELoopScrollList_BagCardItem");
				}
				return this.m_ELoopScrollList_BagCardItemLoopHorizontalScrollRect;
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
					this.m_EG_MoveItemRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "E_Root/EG_MoveItem");
				}
				return this.m_EG_MoveItemRectTransform;
			}
		}

		public UnityEngine.UI.Button E_DebugButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_DebugButton == null )
				{
					this.m_E_DebugButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_Root/E_Debug");
				}
				return this.m_E_DebugButton;
			}
		}

		public UnityEngine.UI.Image E_DebugImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_DebugImage == null )
				{
					this.m_E_DebugImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Root/E_Debug");
				}
				return this.m_E_DebugImage;
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
			this.m_ELoopScrollList_BattleDeckItemLoopHorizontalScrollRect = null;
			this.m_ELoopScrollList_BagCardItemLoopHorizontalScrollRect = null;
			this.m_EG_MoveItemRectTransform = null;
			this.m_E_DebugButton = null;
			this.m_E_DebugImage = null;
			this.m_EG_OpenAnimationRectTransform = null;
			this.m_EG_CloseAnimationRectTransform = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopScrollList_BattleDeckItemLoopHorizontalScrollRect = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopScrollList_BagCardItemLoopHorizontalScrollRect = null;
		private UnityEngine.RectTransform m_EG_MoveItemRectTransform = null;
		private UnityEngine.UI.Button m_E_DebugButton = null;
		private UnityEngine.UI.Image m_E_DebugImage = null;
		private UnityEngine.RectTransform m_EG_OpenAnimationRectTransform = null;
		private UnityEngine.RectTransform m_EG_CloseAnimationRectTransform = null;
		public Transform uiTransform = null;
	}
}
