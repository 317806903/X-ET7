
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgGameMode))]
	[EnableMethod]
	public class DlgGameModeViewComponent : Entity, IAwake, IDestroy
	{
		public UnityEngine.RectTransform EGBackGroundRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EGBackGroundRectTransform == null )
				{
					this.m_EGBackGroundRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround");
				}
				return this.m_EGBackGroundRectTransform;
			}
		}

		public UnityEngine.UI.Image EGBackGroundImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EGBackGroundImage == null )
				{
					this.m_EGBackGroundImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround");
				}
				return this.m_EGBackGroundImage;
			}
		}

		public UnityEngine.UI.Button E_ReturnLoginButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ReturnLoginButton == null )
				{
					this.m_E_ReturnLoginButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/E_ReturnLogin");
				}
				return this.m_E_ReturnLoginButton;
			}
		}

		public UnityEngine.UI.Image E_ReturnLoginImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ReturnLoginImage == null )
				{
					this.m_E_ReturnLoginImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/E_ReturnLogin");
				}
				return this.m_E_ReturnLoginImage;
			}
		}

		public UnityEngine.UI.Text E_ReturnTextText
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ReturnTextText == null )
				{
					this.m_E_ReturnTextText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject, "EGBackGround/E_ReturnLogin/E_ReturnText");
				}
				return this.m_E_ReturnTextText;
			}
		}

		public UITextLocalizeMonoView E_ReturnTextUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ReturnTextUITextLocalizeMonoView == null )
				{
					this.m_E_ReturnTextUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "EGBackGround/E_ReturnLogin/E_ReturnText");
				}
				return this.m_E_ReturnTextUITextLocalizeMonoView;
			}
		}

		public UnityEngine.UI.Button E_SingleMapModeButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_SingleMapModeButton == null )
				{
					this.m_E_SingleMapModeButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/E_SingleMapMode");
				}
				return this.m_E_SingleMapModeButton;
			}
		}

		public UnityEngine.UI.Image E_SingleMapModeImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_SingleMapModeImage == null )
				{
					this.m_E_SingleMapModeImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/E_SingleMapMode");
				}
				return this.m_E_SingleMapModeImage;
			}
		}

		public UnityEngine.UI.Text E_SingleMapTextText
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_SingleMapTextText == null )
				{
					this.m_E_SingleMapTextText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject, "EGBackGround/E_SingleMapMode/E_SingleMapText");
				}
				return this.m_E_SingleMapTextText;
			}
		}

		public UITextLocalizeMonoView E_SingleMapTextUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_SingleMapTextUITextLocalizeMonoView == null )
				{
					this.m_E_SingleMapTextUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "EGBackGround/E_SingleMapMode/E_SingleMapText");
				}
				return this.m_E_SingleMapTextUITextLocalizeMonoView;
			}
		}

		public UnityEngine.UI.Button E_RoomModeButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_RoomModeButton == null )
				{
					this.m_E_RoomModeButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/E_RoomMode");
				}
				return this.m_E_RoomModeButton;
			}
		}

		public UnityEngine.UI.Image E_RoomModeImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_RoomModeImage == null )
				{
					this.m_E_RoomModeImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/E_RoomMode");
				}
				return this.m_E_RoomModeImage;
			}
		}

		public UnityEngine.UI.Text E_RoomModeTextText
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_RoomModeTextText == null )
				{
					this.m_E_RoomModeTextText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject, "EGBackGround/E_RoomMode/E_RoomModeText");
				}
				return this.m_E_RoomModeTextText;
			}
		}

		public UITextLocalizeMonoView E_RoomModeTextUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_RoomModeTextUITextLocalizeMonoView == null )
				{
					this.m_E_RoomModeTextUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "EGBackGround/E_RoomMode/E_RoomModeText");
				}
				return this.m_E_RoomModeTextUITextLocalizeMonoView;
			}
		}

		public UnityEngine.UI.Button E_ARRoomModeButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ARRoomModeButton == null )
				{
					this.m_E_ARRoomModeButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/E_ARRoomMode");
				}
				return this.m_E_ARRoomModeButton;
			}
		}

		public UnityEngine.UI.Image E_ARRoomModeImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ARRoomModeImage == null )
				{
					this.m_E_ARRoomModeImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/E_ARRoomMode");
				}
				return this.m_E_ARRoomModeImage;
			}
		}

		public UnityEngine.UI.Text E_ARRoomModeTextText
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ARRoomModeTextText == null )
				{
					this.m_E_ARRoomModeTextText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject, "EGBackGround/E_ARRoomMode/E_ARRoomModeText");
				}
				return this.m_E_ARRoomModeTextText;
			}
		}

		public UITextLocalizeMonoView E_ARRoomModeTextUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ARRoomModeTextUITextLocalizeMonoView == null )
				{
					this.m_E_ARRoomModeTextUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "EGBackGround/E_ARRoomMode/E_ARRoomModeText");
				}
				return this.m_E_ARRoomModeTextUITextLocalizeMonoView;
			}
		}

		public void DestroyWidget()
		{
			this.m_EGBackGroundRectTransform = null;
			this.m_EGBackGroundImage = null;
			this.m_E_ReturnLoginButton = null;
			this.m_E_ReturnLoginImage = null;
			this.m_E_ReturnTextText = null;
			this.m_E_ReturnTextUITextLocalizeMonoView = null;
			this.m_E_SingleMapModeButton = null;
			this.m_E_SingleMapModeImage = null;
			this.m_E_SingleMapTextText = null;
			this.m_E_SingleMapTextUITextLocalizeMonoView = null;
			this.m_E_RoomModeButton = null;
			this.m_E_RoomModeImage = null;
			this.m_E_RoomModeTextText = null;
			this.m_E_RoomModeTextUITextLocalizeMonoView = null;
			this.m_E_ARRoomModeButton = null;
			this.m_E_ARRoomModeImage = null;
			this.m_E_ARRoomModeTextText = null;
			this.m_E_ARRoomModeTextUITextLocalizeMonoView = null;
			this.uiTransform = null;
		}

		private UnityEngine.RectTransform m_EGBackGroundRectTransform = null;
		private UnityEngine.UI.Image m_EGBackGroundImage = null;
		private UnityEngine.UI.Button m_E_ReturnLoginButton = null;
		private UnityEngine.UI.Image m_E_ReturnLoginImage = null;
		private UnityEngine.UI.Text m_E_ReturnTextText = null;
		private UITextLocalizeMonoView m_E_ReturnTextUITextLocalizeMonoView = null;
		private UnityEngine.UI.Button m_E_SingleMapModeButton = null;
		private UnityEngine.UI.Image m_E_SingleMapModeImage = null;
		private UnityEngine.UI.Text m_E_SingleMapTextText = null;
		private UITextLocalizeMonoView m_E_SingleMapTextUITextLocalizeMonoView = null;
		private UnityEngine.UI.Button m_E_RoomModeButton = null;
		private UnityEngine.UI.Image m_E_RoomModeImage = null;
		private UnityEngine.UI.Text m_E_RoomModeTextText = null;
		private UITextLocalizeMonoView m_E_RoomModeTextUITextLocalizeMonoView = null;
		private UnityEngine.UI.Button m_E_ARRoomModeButton = null;
		private UnityEngine.UI.Image m_E_ARRoomModeImage = null;
		private UnityEngine.UI.Text m_E_ARRoomModeTextText = null;
		private UITextLocalizeMonoView m_E_ARRoomModeTextUITextLocalizeMonoView = null;
		public Transform uiTransform = null;
	}
}
