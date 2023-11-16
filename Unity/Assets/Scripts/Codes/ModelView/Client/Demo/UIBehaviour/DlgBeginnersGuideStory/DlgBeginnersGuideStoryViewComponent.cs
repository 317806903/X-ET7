
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgBeginnersGuideStory))]
	[EnableMethod]
	public class DlgBeginnersGuideStoryViewComponent : Entity, IAwake, IDestroy
	{
		public UnityEngine.UI.Image E_StoryImgImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_StoryImgImage == null )
				{
					this.m_E_StoryImgImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "story/E_StoryImg");
				}
				return this.m_E_StoryImgImage;
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
					this.m_E_TextContextTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_TextContext");
				}
				return this.m_E_TextContextTextMeshProUGUI;
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
					this.m_E_NextButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_Next");
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
					this.m_E_NextImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Next");
				}
				return this.m_E_NextImage;
			}
		}

		public void DestroyWidget()
		{
			this.m_E_StoryImgImage = null;
			this.m_E_TextContextTextMeshProUGUI = null;
			this.m_E_NextButton = null;
			this.m_E_NextImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Image m_E_StoryImgImage = null;
		private TMPro.TextMeshProUGUI m_E_TextContextTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_E_NextButton = null;
		private UnityEngine.UI.Image m_E_NextImage = null;
		public Transform uiTransform = null;
	}
}
