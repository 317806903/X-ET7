
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgLogin))]
	[EnableMethod]
	public class DlgLoginViewComponent : Entity, IAwake, IDestroy
	{
		public UnityEngine.UI.Button E_Login_googleButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_Login_googleButton == null )
				{
					this.m_E_Login_googleButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "Sprite_BackGround/E_Login_google");
				}
				return this.m_E_Login_googleButton;
			}
		}

		public UnityEngine.UI.Image E_Login_googleImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_Login_googleImage == null )
				{
					this.m_E_Login_googleImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "Sprite_BackGround/E_Login_google");
				}
				return this.m_E_Login_googleImage;
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
					this.m_E_LoginButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "Sprite_BackGround/E_Login");
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
					this.m_E_LoginImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "Sprite_BackGround/E_Login");
				}
				return this.m_E_LoginImage;
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
					this.m_E_AccountInputField = UIFindHelper.FindDeepChild<UnityEngine.UI.InputField>(this.uiTransform.gameObject, "Sprite_BackGround/E_Account");
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
					this.m_E_AccountImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "Sprite_BackGround/E_Account");
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
					this.m_E_PasswordInputField = UIFindHelper.FindDeepChild<UnityEngine.UI.InputField>(this.uiTransform.gameObject, "Sprite_BackGround/E_Password");
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
					this.m_E_PasswordImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "Sprite_BackGround/E_Password");
				}
				return this.m_E_PasswordImage;
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
					this.m_E_ToggleDebugModeToggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject, "Sprite_BackGround/E_ToggleDebugMode");
				}
				return this.m_E_ToggleDebugModeToggle;
			}
		}

		public void DestroyWidget()
		{
			this.m_E_Login_googleButton = null;
			this.m_E_Login_googleImage = null;
			this.m_E_LoginButton = null;
			this.m_E_LoginImage = null;
			this.m_E_AccountInputField = null;
			this.m_E_AccountImage = null;
			this.m_E_PasswordInputField = null;
			this.m_E_PasswordImage = null;
			this.m_E_ToggleDebugModeToggle = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_Login_googleButton = null;
		private UnityEngine.UI.Image m_E_Login_googleImage = null;
		private UnityEngine.UI.Button m_E_LoginButton = null;
		private UnityEngine.UI.Image m_E_LoginImage = null;
		private UnityEngine.UI.InputField m_E_AccountInputField = null;
		private UnityEngine.UI.Image m_E_AccountImage = null;
		private UnityEngine.UI.InputField m_E_PasswordInputField = null;
		private UnityEngine.UI.Image m_E_PasswordImage = null;
		private UnityEngine.UI.Toggle m_E_ToggleDebugModeToggle = null;
		public Transform uiTransform = null;
	}
}
