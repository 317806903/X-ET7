
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgVideoShow))]
	[EnableMethod]
	public class DlgVideoShowViewComponent : Entity, IAwake, IDestroy
	{
		public UnityEngine.UI.RawImage E_VideoShowRawImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_VideoShowRawImage == null )
				{
					this.m_E_VideoShowRawImage = UIFindHelper.FindDeepChild<UnityEngine.UI.RawImage>(this.uiTransform.gameObject, "Sprite_BackGround/E_VideoShow");
				}
				return this.m_E_VideoShowRawImage;
			}
		}

		public TMPro.TextMeshProUGUI E_TextContextTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_TextContextTextMeshProUGUI == null )
				{
					this.m_E_TextContextTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "Sprite_BackGround/E_TextContext");
				}
				return this.m_E_TextContextTextMeshProUGUI;
			}
		}

		public UITextLocalizeMonoView E_TextContextUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_TextContextUITextLocalizeMonoView == null )
				{
					this.m_E_TextContextUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "Sprite_BackGround/E_TextContext");
				}
				return this.m_E_TextContextUITextLocalizeMonoView;
			}
		}

		public UnityEngine.UI.Button E_NextButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_NextButton == null )
				{
					this.m_E_NextButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "Sprite_BackGround/E_Next");
				}
				return this.m_E_NextButton;
			}
		}

		public UnityEngine.UI.Image E_NextImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_NextImage == null )
				{
					this.m_E_NextImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "Sprite_BackGround/E_Next");
				}
				return this.m_E_NextImage;
			}
		}

		public void DestroyWidget()
		{
			this.m_E_VideoShowRawImage = null;
			this.m_E_TextContextTextMeshProUGUI = null;
			this.m_E_TextContextUITextLocalizeMonoView = null;
			this.m_E_NextButton = null;
			this.m_E_NextImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.RawImage m_E_VideoShowRawImage = null;
		private TMPro.TextMeshProUGUI m_E_TextContextTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_E_TextContextUITextLocalizeMonoView = null;
		private UnityEngine.UI.Button m_E_NextButton = null;
		private UnityEngine.UI.Image m_E_NextImage = null;
		public Transform uiTransform = null;
	}
}
