
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgBattleTowerNotice))]
	[EnableMethod]
	public class DlgBattleTowerNoticeViewComponent : Entity, IAwake, IDestroy
	{
		public UnityEngine.UI.Button E_Sprite_BGButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_Sprite_BGButton == null )
				{
					this.m_E_Sprite_BGButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_Sprite_BG");
				}
				return this.m_E_Sprite_BGButton;
			}
		}

		public UnityEngine.UI.Image E_Sprite_BGImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_Sprite_BGImage == null )
				{
					this.m_E_Sprite_BGImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Sprite_BG");
				}
				return this.m_E_Sprite_BGImage;
			}
		}

		public UnityEngine.RectTransform EG_NoticeRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_NoticeRectTransform == null )
				{
					this.m_EG_NoticeRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_Notice");
				}
				return this.m_EG_NoticeRectTransform;
			}
		}

		public UnityEngine.UI.LoopVerticalScrollRect ELoopScrollList_NoticeLoopVerticalScrollRect
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELoopScrollList_NoticeLoopVerticalScrollRect == null )
				{
					this.m_ELoopScrollList_NoticeLoopVerticalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopVerticalScrollRect>(this.uiTransform.gameObject, "EG_Notice/ELoopScrollList_Notice");
				}
				return this.m_ELoopScrollList_NoticeLoopVerticalScrollRect;
			}
		}

		public void DestroyWidget()
		{
			this.m_E_Sprite_BGButton = null;
			this.m_E_Sprite_BGImage = null;
			this.m_EG_NoticeRectTransform = null;
			this.m_ELoopScrollList_NoticeLoopVerticalScrollRect = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_Sprite_BGButton = null;
		private UnityEngine.UI.Image m_E_Sprite_BGImage = null;
		private UnityEngine.RectTransform m_EG_NoticeRectTransform = null;
		private UnityEngine.UI.LoopVerticalScrollRect m_ELoopScrollList_NoticeLoopVerticalScrollRect = null;
		public Transform uiTransform = null;
	}
}
