
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgUpdate))]
	[EnableMethod]
	public class DlgUpdateViewComponent : Entity, IAwake, IDestroy
	{
		public UnityEngine.UI.Image E_BGImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_BGImage == null )
				{
					this.m_E_BGImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "Sprite_BackGround/E_BG");
				}
				return this.m_E_BGImage;
			}
		}

		public UnityEngine.UI.Text ELabel_TotalDownloadCountText
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_TotalDownloadCountText == null )
				{
					this.m_ELabel_TotalDownloadCountText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject, "Sprite_BackGround/GameObject/ELabel_TotalDownloadCount");
				}
				return this.m_ELabel_TotalDownloadCountText;
			}
		}

		public UnityEngine.UI.Text ELabel_CurrentDownloadCountText
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_CurrentDownloadCountText == null )
				{
					this.m_ELabel_CurrentDownloadCountText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject, "Sprite_BackGround/GameObject/ELabel_CurrentDownloadCount");
				}
				return this.m_ELabel_CurrentDownloadCountText;
			}
		}

		public UnityEngine.UI.Text ELabel_TotalDownloadSizeBytesText
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_TotalDownloadSizeBytesText == null )
				{
					this.m_ELabel_TotalDownloadSizeBytesText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject, "Sprite_BackGround/GameObject/ELabel_TotalDownloadSizeBytes");
				}
				return this.m_ELabel_TotalDownloadSizeBytesText;
			}
		}

		public UnityEngine.UI.Text ELabel_CurrentDownloadSizeBytesText
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_CurrentDownloadSizeBytesText == null )
				{
					this.m_ELabel_CurrentDownloadSizeBytesText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject, "Sprite_BackGround/GameObject/ELabel_CurrentDownloadSizeBytes");
				}
				return this.m_ELabel_CurrentDownloadSizeBytesText;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_VersionTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_VersionTextMeshProUGUI == null )
				{
					this.m_ELabel_VersionTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "ELabel_Version");
				}
				return this.m_ELabel_VersionTextMeshProUGUI;
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
			this.m_E_BGImage = null;
			this.m_ELabel_TotalDownloadCountText = null;
			this.m_ELabel_CurrentDownloadCountText = null;
			this.m_ELabel_TotalDownloadSizeBytesText = null;
			this.m_ELabel_CurrentDownloadSizeBytesText = null;
			this.m_ELabel_VersionTextMeshProUGUI = null;
			this.m_EG_OpenAnimationRectTransform = null;
			this.m_EG_CloseAnimationRectTransform = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Image m_E_BGImage = null;
		private UnityEngine.UI.Text m_ELabel_TotalDownloadCountText = null;
		private UnityEngine.UI.Text m_ELabel_CurrentDownloadCountText = null;
		private UnityEngine.UI.Text m_ELabel_TotalDownloadSizeBytesText = null;
		private UnityEngine.UI.Text m_ELabel_CurrentDownloadSizeBytesText = null;
		private TMPro.TextMeshProUGUI m_ELabel_VersionTextMeshProUGUI = null;
		private UnityEngine.RectTransform m_EG_OpenAnimationRectTransform = null;
		private UnityEngine.RectTransform m_EG_CloseAnimationRectTransform = null;
		public Transform uiTransform = null;
	}
}
