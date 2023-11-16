
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgLogin))]
	[EnableMethod]
	public class DlgLoginViewComponent : Entity, IAwake, IDestroy
	{
		public UnityEngine.RectTransform EG_LoginAccountRootRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_LoginAccountRootRectTransform == null )
				{
					this.m_EG_LoginAccountRootRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "Sprite_BackGround/EG_LoginAccountRoot");
				}
				return this.m_EG_LoginAccountRootRectTransform;
			}
		}

		public UnityEngine.RectTransform EG_LoginWhenEditorRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_LoginWhenEditorRectTransform == null )
				{
					this.m_EG_LoginWhenEditorRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "Sprite_BackGround/EG_LoginAccountRoot/EG_LoginWhenEditor");
				}
				return this.m_EG_LoginWhenEditorRectTransform;
			}
		}

		public UnityEngine.UI.InputField E_AccountInputField
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_AccountInputField == null )
				{
					this.m_E_AccountInputField = UIFindHelper.FindDeepChild<UnityEngine.UI.InputField>(this.uiTransform.gameObject, "Sprite_BackGround/EG_LoginAccountRoot/EG_LoginWhenEditor/E_Account");
				}
				return this.m_E_AccountInputField;
			}
		}

		public UnityEngine.UI.Image E_AccountImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_AccountImage == null )
				{
					this.m_E_AccountImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "Sprite_BackGround/EG_LoginAccountRoot/EG_LoginWhenEditor/E_Account");
				}
				return this.m_E_AccountImage;
			}
		}

		public UnityEngine.UI.InputField E_PasswordInputField
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_PasswordInputField == null )
				{
					this.m_E_PasswordInputField = UIFindHelper.FindDeepChild<UnityEngine.UI.InputField>(this.uiTransform.gameObject, "Sprite_BackGround/EG_LoginAccountRoot/EG_LoginWhenEditor/E_Password");
				}
				return this.m_E_PasswordInputField;
			}
		}

		public UnityEngine.UI.Image E_PasswordImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_PasswordImage == null )
				{
					this.m_E_PasswordImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "Sprite_BackGround/EG_LoginAccountRoot/EG_LoginWhenEditor/E_Password");
				}
				return this.m_E_PasswordImage;
			}
		}

		public UnityEngine.UI.Button E_LoginButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_LoginButton == null )
				{
					this.m_E_LoginButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "Sprite_BackGround/EG_LoginAccountRoot/EG_LoginWhenEditor/E_Login");
				}
				return this.m_E_LoginButton;
			}
		}

		public UnityEngine.UI.Image E_LoginImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_LoginImage == null )
				{
					this.m_E_LoginImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "Sprite_BackGround/EG_LoginAccountRoot/EG_LoginWhenEditor/E_Login");
				}
				return this.m_E_LoginImage;
			}
		}

		public UnityEngine.RectTransform EG_LoginWhenSDKRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_LoginWhenSDKRectTransform == null )
				{
					this.m_EG_LoginWhenSDKRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "Sprite_BackGround/EG_LoginAccountRoot/EG_LoginWhenSDK");
				}
				return this.m_EG_LoginWhenSDKRectTransform;
			}
		}

		public UnityEngine.UI.Button E_Login_GuestButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_Login_GuestButton == null )
				{
					this.m_E_Login_GuestButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "Sprite_BackGround/EG_LoginAccountRoot/EG_LoginWhenSDK/E_Login_Guest");
				}
				return this.m_E_Login_GuestButton;
			}
		}

		public UnityEngine.UI.Image E_Login_GuestImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_Login_GuestImage == null )
				{
					this.m_E_Login_GuestImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "Sprite_BackGround/EG_LoginAccountRoot/EG_LoginWhenSDK/E_Login_Guest");
				}
				return this.m_E_Login_GuestImage;
			}
		}

		public UnityEngine.UI.Button E_Login_SDKButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_Login_SDKButton == null )
				{
					this.m_E_Login_SDKButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "Sprite_BackGround/EG_LoginAccountRoot/EG_LoginWhenSDK/E_Login_SDK");
				}
				return this.m_E_Login_SDKButton;
			}
		}

		public UnityEngine.UI.Image E_Login_SDKImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_Login_SDKImage == null )
				{
					this.m_E_Login_SDKImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "Sprite_BackGround/EG_LoginAccountRoot/EG_LoginWhenSDK/E_Login_SDK");
				}
				return this.m_E_Login_SDKImage;
			}
		}

		public UnityEngine.UI.Toggle E_ToggleDebugModeToggle
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ToggleDebugModeToggle == null )
				{
					this.m_E_ToggleDebugModeToggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject, "Sprite_BackGround/DebugRoot/E_ToggleDebugMode");
				}
				return this.m_E_ToggleDebugModeToggle;
			}
		}

		public UnityEngine.UI.Toggle E_ToggleLoginEditorModeToggle
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ToggleLoginEditorModeToggle == null )
				{
					this.m_E_ToggleLoginEditorModeToggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject, "Sprite_BackGround/DebugRoot/E_ToggleLoginEditorMode");
				}
				return this.m_E_ToggleLoginEditorModeToggle;
			}
		}

		public void DestroyWidget()
		{
			this.m_EG_LoginAccountRootRectTransform = null;
			this.m_EG_LoginWhenEditorRectTransform = null;
			this.m_E_AccountInputField = null;
			this.m_E_AccountImage = null;
			this.m_E_PasswordInputField = null;
			this.m_E_PasswordImage = null;
			this.m_E_LoginButton = null;
			this.m_E_LoginImage = null;
			this.m_EG_LoginWhenSDKRectTransform = null;
			this.m_E_Login_GuestButton = null;
			this.m_E_Login_GuestImage = null;
			this.m_E_Login_SDKButton = null;
			this.m_E_Login_SDKImage = null;
			this.m_E_ToggleDebugModeToggle = null;
			this.m_E_ToggleLoginEditorModeToggle = null;
			this.uiTransform = null;
		}

		private UnityEngine.RectTransform m_EG_LoginAccountRootRectTransform = null;
		private UnityEngine.RectTransform m_EG_LoginWhenEditorRectTransform = null;
		private UnityEngine.UI.InputField m_E_AccountInputField = null;
		private UnityEngine.UI.Image m_E_AccountImage = null;
		private UnityEngine.UI.InputField m_E_PasswordInputField = null;
		private UnityEngine.UI.Image m_E_PasswordImage = null;
		private UnityEngine.UI.Button m_E_LoginButton = null;
		private UnityEngine.UI.Image m_E_LoginImage = null;
		private UnityEngine.RectTransform m_EG_LoginWhenSDKRectTransform = null;
		private UnityEngine.UI.Button m_E_Login_GuestButton = null;
		private UnityEngine.UI.Image m_E_Login_GuestImage = null;
		private UnityEngine.UI.Button m_E_Login_SDKButton = null;
		private UnityEngine.UI.Image m_E_Login_SDKImage = null;
		private UnityEngine.UI.Toggle m_E_ToggleDebugModeToggle = null;
		private UnityEngine.UI.Toggle m_E_ToggleLoginEditorModeToggle = null;
		public Transform uiTransform = null;
	}
}
