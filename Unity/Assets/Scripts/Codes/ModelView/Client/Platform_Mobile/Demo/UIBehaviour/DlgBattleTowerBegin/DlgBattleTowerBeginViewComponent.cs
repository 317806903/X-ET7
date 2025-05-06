
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgBattleTowerBegin))]
	[EnableMethod]
	public class DlgBattleTowerBeginViewComponent : Entity, IAwake, IDestroy
	{
		public UnityEngine.UI.Image E_EffectImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_EffectImage == null )
				{
					this.m_E_EffectImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Effect");
				}
				return this.m_E_EffectImage;
			}
		}

		public UnityEngine.UI.Button Effect_GameBeginButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_Effect_GameBeginButton == null )
				{
					this.m_Effect_GameBeginButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_Effect/Effect_GameBegin");
				}
				return this.m_Effect_GameBeginButton;
			}
		}

		public UnityEngine.UI.Image Effect_GameBeginImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_Effect_GameBeginImage == null )
				{
					this.m_Effect_GameBeginImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Effect/Effect_GameBegin");
				}
				return this.m_Effect_GameBeginImage;
			}
		}

		public UnityEngine.UI.Image EButton_GameBegin_lightImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_GameBegin_lightImage == null )
				{
					this.m_EButton_GameBegin_lightImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Effect/Effect_GameBegin/EButton_GameBegin/EButton_GameBegin_light");
				}
				return this.m_EButton_GameBegin_lightImage;
			}
		}

		public UnityEngine.UI.Image EButton_GameBegin_boxImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_GameBegin_boxImage == null )
				{
					this.m_EButton_GameBegin_boxImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Effect/Effect_GameBegin/EButton_GameBegin/EButton_GameBegin_box");
				}
				return this.m_EButton_GameBegin_boxImage;
			}
		}

		public UnityEngine.UI.Image EButton_GameBegin_txtImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_GameBegin_txtImage == null )
				{
					this.m_EButton_GameBegin_txtImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Effect/Effect_GameBegin/EButton_GameBegin/EButton_GameBegin_txt");
				}
				return this.m_EButton_GameBegin_txtImage;
			}
		}

		public TMPro.TextMeshProUGUI EButton_GameBegin_txtTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_GameBegin_txtTextMeshProUGUI == null )
				{
					this.m_EButton_GameBegin_txtTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_Effect/Effect_GameBegin/EButton_GameBegin/EButton_GameBegin_txt");
				}
				return this.m_EButton_GameBegin_txtTextMeshProUGUI;
			}
		}

		public UITextLocalizeMonoView EButton_GameBegin_txtUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_GameBegin_txtUITextLocalizeMonoView == null )
				{
					this.m_EButton_GameBegin_txtUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "E_Effect/Effect_GameBegin/EButton_GameBegin/EButton_GameBegin_txt");
				}
				return this.m_EButton_GameBegin_txtUITextLocalizeMonoView;
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
			this.m_E_EffectImage = null;
			this.m_Effect_GameBeginButton = null;
			this.m_Effect_GameBeginImage = null;
			this.m_EButton_GameBegin_lightImage = null;
			this.m_EButton_GameBegin_boxImage = null;
			this.m_EButton_GameBegin_txtImage = null;
			this.m_EButton_GameBegin_txtTextMeshProUGUI = null;
			this.m_EButton_GameBegin_txtUITextLocalizeMonoView = null;
			this.m_EG_OpenAnimationRectTransform = null;
			this.m_EG_CloseAnimationRectTransform = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Image m_E_EffectImage = null;
		private UnityEngine.UI.Button m_Effect_GameBeginButton = null;
		private UnityEngine.UI.Image m_Effect_GameBeginImage = null;
		private UnityEngine.UI.Image m_EButton_GameBegin_lightImage = null;
		private UnityEngine.UI.Image m_EButton_GameBegin_boxImage = null;
		private UnityEngine.UI.Image m_EButton_GameBegin_txtImage = null;
		private TMPro.TextMeshProUGUI m_EButton_GameBegin_txtTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_EButton_GameBegin_txtUITextLocalizeMonoView = null;
		private UnityEngine.RectTransform m_EG_OpenAnimationRectTransform = null;
		private UnityEngine.RectTransform m_EG_CloseAnimationRectTransform = null;
		public Transform uiTransform = null;
	}
}
