
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgFixedMenuHighest))]
	[EnableMethod]
	public class DlgFixedMenuHighestViewComponent : Entity, IAwake, IDestroy
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

		public UnityEngine.RectTransform EG_ReportRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_ReportRectTransform == null )
				{
					this.m_EG_ReportRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_Root/EG_Report");
				}
				return this.m_EG_ReportRectTransform;
			}
		}

		public void DestroyWidget()
		{
			this.m_EG_RootRectTransform = null;
			this.m_EG_ReportRectTransform = null;
			this.uiTransform = null;
		}

		private UnityEngine.RectTransform m_EG_RootRectTransform = null;
		private UnityEngine.RectTransform m_EG_ReportRectTransform = null;
		public Transform uiTransform = null;
	}
}
