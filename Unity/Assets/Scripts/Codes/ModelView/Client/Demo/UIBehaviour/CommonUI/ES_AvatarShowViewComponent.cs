
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[EnableMethod]
	public class ES_AvatarShowViewComponent : Entity, ET.IAwake<UnityEngine.Transform>, IDestroy
	{
		public UnityEngine.UI.Button EboxButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EboxButton == null )
				{
					this.m_EboxButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "Ebox");
				}
				return this.m_EboxButton;
			}
		}

		public BlurBackground.TranslucentImage EboxTranslucentImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EboxTranslucentImage == null )
				{
					this.m_EboxTranslucentImage = UIFindHelper.FindDeepChild<BlurBackground.TranslucentImage>(this.uiTransform.gameObject, "Ebox");
				}
				return this.m_EboxTranslucentImage;
			}
		}

		public UnityEngine.UI.Image E_ImgLineImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ImgLineImage == null )
				{
					this.m_E_ImgLineImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "Ebox/E_ImgLine");
				}
				return this.m_E_ImgLineImage;
			}
		}

		public UnityEngine.UI.Image E_AvatarIconImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_AvatarIconImage == null )
				{
					this.m_E_AvatarIconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "Ebox/E_AvatarIcon");
				}
				return this.m_E_AvatarIconImage;
			}
		}

		public UnityEngine.UI.Image EImage_FrameIconImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EImage_FrameIconImage == null )
				{
					this.m_EImage_FrameIconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "Ebox/EImage_FrameIcon");
				}
				return this.m_EImage_FrameIconImage;
			}
		}

		public UnityEngine.UI.Image E_RedDotImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_RedDotImage == null )
				{
					this.m_E_RedDotImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_RedDot");
				}
				return this.m_E_RedDotImage;
			}
		}

		public TMPro.TextMeshProUGUI E_PlayerNameTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_PlayerNameTextMeshProUGUI == null )
				{
					this.m_E_PlayerNameTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_PlayerName");
				}
				return this.m_E_PlayerNameTextMeshProUGUI;
			}
		}

		public void DestroyWidget()
		{
			this.m_EboxButton = null;
			this.m_EboxTranslucentImage = null;
			this.m_E_ImgLineImage = null;
			this.m_E_AvatarIconImage = null;
			this.m_EImage_FrameIconImage = null;
			this.m_E_RedDotImage = null;
			this.m_E_PlayerNameTextMeshProUGUI = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_EboxButton = null;
		private BlurBackground.TranslucentImage m_EboxTranslucentImage = null;
		private UnityEngine.UI.Image m_E_ImgLineImage = null;
		private UnityEngine.UI.Image m_E_AvatarIconImage = null;
		private UnityEngine.UI.Image m_EImage_FrameIconImage = null;
		private UnityEngine.UI.Image m_E_RedDotImage = null;
		private TMPro.TextMeshProUGUI m_E_PlayerNameTextMeshProUGUI = null;
		public Transform uiTransform = null;
	}
}
