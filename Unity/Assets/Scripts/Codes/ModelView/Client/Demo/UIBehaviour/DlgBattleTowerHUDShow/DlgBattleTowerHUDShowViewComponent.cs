
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgBattleTowerHUDShow))]
	[EnableMethod]
	public class DlgBattleTowerHUDShowViewComponent : Entity, IAwake, IDestroy
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

		public UnityEngine.RectTransform EGHPRootRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EGHPRootRectTransform == null )
				{
					this.m_EGHPRootRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGRoot/EGHPRoot");
				}
				return this.m_EGHPRootRectTransform;
			}
		}

		public UnityEngine.RectTransform EGDamageRootRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EGDamageRootRectTransform == null )
				{
					this.m_EGDamageRootRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGRoot/EGDamageRoot");
				}
				return this.m_EGDamageRootRectTransform;
			}
		}

		public UnityEngine.RectTransform EGShowCoinGetRootRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EGShowCoinGetRootRectTransform == null )
				{
					this.m_EGShowCoinGetRootRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGRoot/EGShowCoinGetRoot");
				}
				return this.m_EGShowCoinGetRootRectTransform;
			}
		}

		public void DestroyWidget()
		{
			this.m_EGRootRectTransform = null;
			this.m_EGHPRootRectTransform = null;
			this.m_EGDamageRootRectTransform = null;
			this.m_EGShowCoinGetRootRectTransform = null;
			this.uiTransform = null;
		}

		private UnityEngine.RectTransform m_EGRootRectTransform = null;
		private UnityEngine.RectTransform m_EGHPRootRectTransform = null;
		private UnityEngine.RectTransform m_EGDamageRootRectTransform = null;
		private UnityEngine.RectTransform m_EGShowCoinGetRootRectTransform = null;
		public Transform uiTransform = null;
	}
}
