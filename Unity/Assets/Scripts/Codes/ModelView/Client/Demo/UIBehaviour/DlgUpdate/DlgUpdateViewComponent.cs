
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgUpdate))]
	[EnableMethod]
	public class DlgUpdateViewComponent : Entity, IAwake, IDestroy
	{
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

		public void DestroyWidget()
		{
			this.m_ELabel_TotalDownloadCountText = null;
			this.m_ELabel_CurrentDownloadCountText = null;
			this.m_ELabel_TotalDownloadSizeBytesText = null;
			this.m_ELabel_CurrentDownloadSizeBytesText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Text m_ELabel_TotalDownloadCountText = null;
		private UnityEngine.UI.Text m_ELabel_CurrentDownloadCountText = null;
		private UnityEngine.UI.Text m_ELabel_TotalDownloadSizeBytesText = null;
		private UnityEngine.UI.Text m_ELabel_CurrentDownloadSizeBytesText = null;
		public Transform uiTransform = null;
	}
}
