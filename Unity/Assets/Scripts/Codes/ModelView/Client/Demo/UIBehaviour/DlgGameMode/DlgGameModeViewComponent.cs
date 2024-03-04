
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

		public UnityEngine.UI.Button E_KnapsackModeButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_KnapsackModeButton == null )
				{
					this.m_E_KnapsackModeButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/E_KnapsackMode");
				}
				return this.m_E_KnapsackModeButton;
			}
		}

		public UnityEngine.UI.Image E_KnapsackModeImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_KnapsackModeImage == null )
				{
					this.m_E_KnapsackModeImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/E_KnapsackMode");
				}
				return this.m_E_KnapsackModeImage;
			}
		}

		public UnityEngine.UI.Text E_KnapsackTextText
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_KnapsackTextText == null )
				{
					this.m_E_KnapsackTextText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject, "EGBackGround/E_KnapsackMode/E_KnapsackText");
				}
				return this.m_E_KnapsackTextText;
			}
		}

		public UITextLocalizeMonoView E_KnapsackTextUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_KnapsackTextUITextLocalizeMonoView == null )
				{
					this.m_E_KnapsackTextUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "EGBackGround/E_KnapsackMode/E_KnapsackText");
				}
				return this.m_E_KnapsackTextUITextLocalizeMonoView;
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

		public UnityEngine.UI.Button E_ARRoomModeCreateButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ARRoomModeCreateButton == null )
				{
					this.m_E_ARRoomModeCreateButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/E_ARRoomModeCreate");
				}
				return this.m_E_ARRoomModeCreateButton;
			}
		}

		public UnityEngine.UI.Image E_ARRoomModeCreateImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ARRoomModeCreateImage == null )
				{
					this.m_E_ARRoomModeCreateImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/E_ARRoomModeCreate");
				}
				return this.m_E_ARRoomModeCreateImage;
			}
		}

		public UnityEngine.UI.Text E_ARRoomModeCreateTextText
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ARRoomModeCreateTextText == null )
				{
					this.m_E_ARRoomModeCreateTextText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject, "EGBackGround/E_ARRoomModeCreate/E_ARRoomModeCreateText");
				}
				return this.m_E_ARRoomModeCreateTextText;
			}
		}

		public UITextLocalizeMonoView E_ARRoomModeCreateTextUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ARRoomModeCreateTextUITextLocalizeMonoView == null )
				{
					this.m_E_ARRoomModeCreateTextUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "EGBackGround/E_ARRoomModeCreate/E_ARRoomModeCreateText");
				}
				return this.m_E_ARRoomModeCreateTextUITextLocalizeMonoView;
			}
		}

		public UnityEngine.UI.Button E_ARRoomModeJoinButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ARRoomModeJoinButton == null )
				{
					this.m_E_ARRoomModeJoinButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/E_ARRoomModeJoin");
				}
				return this.m_E_ARRoomModeJoinButton;
			}
		}

		public UnityEngine.UI.Image E_ARRoomModeJoinImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ARRoomModeJoinImage == null )
				{
					this.m_E_ARRoomModeJoinImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/E_ARRoomModeJoin");
				}
				return this.m_E_ARRoomModeJoinImage;
			}
		}

		public UnityEngine.UI.Text E_ARRoomModeJoinTextText
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ARRoomModeJoinTextText == null )
				{
					this.m_E_ARRoomModeJoinTextText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject, "EGBackGround/E_ARRoomModeJoin/E_ARRoomModeJoinText");
				}
				return this.m_E_ARRoomModeJoinTextText;
			}
		}

		public UITextLocalizeMonoView E_ARRoomModeJoinTextUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ARRoomModeJoinTextUITextLocalizeMonoView == null )
				{
					this.m_E_ARRoomModeJoinTextUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "EGBackGround/E_ARRoomModeJoin/E_ARRoomModeJoinText");
				}
				return this.m_E_ARRoomModeJoinTextUITextLocalizeMonoView;
			}
		}

		public void DestroyWidget()
		{
			this.m_EGBackGroundRectTransform = null;
			this.m_E_ReturnLoginButton = null;
			this.m_E_ReturnLoginImage = null;
			this.m_E_ReturnTextText = null;
			this.m_E_ReturnTextUITextLocalizeMonoView = null;
			this.m_E_SingleMapModeButton = null;
			this.m_E_SingleMapModeImage = null;
			this.m_E_SingleMapTextText = null;
			this.m_E_SingleMapTextUITextLocalizeMonoView = null;
			this.m_E_KnapsackModeButton = null;
			this.m_E_KnapsackModeImage = null;
			this.m_E_KnapsackTextText = null;
			this.m_E_KnapsackTextUITextLocalizeMonoView = null;
			this.m_E_RoomModeButton = null;
			this.m_E_RoomModeImage = null;
			this.m_E_RoomModeTextText = null;
			this.m_E_RoomModeTextUITextLocalizeMonoView = null;
			this.m_E_ARRoomModeCreateButton = null;
			this.m_E_ARRoomModeCreateImage = null;
			this.m_E_ARRoomModeCreateTextText = null;
			this.m_E_ARRoomModeCreateTextUITextLocalizeMonoView = null;
			this.m_E_ARRoomModeJoinButton = null;
			this.m_E_ARRoomModeJoinImage = null;
			this.m_E_ARRoomModeJoinTextText = null;
			this.m_E_ARRoomModeJoinTextUITextLocalizeMonoView = null;
			this.uiTransform = null;
		}

		private UnityEngine.RectTransform m_EGBackGroundRectTransform = null;
		private UnityEngine.UI.Button m_E_ReturnLoginButton = null;
		private UnityEngine.UI.Image m_E_ReturnLoginImage = null;
		private UnityEngine.UI.Text m_E_ReturnTextText = null;
		private UITextLocalizeMonoView m_E_ReturnTextUITextLocalizeMonoView = null;
		private UnityEngine.UI.Button m_E_SingleMapModeButton = null;
		private UnityEngine.UI.Image m_E_SingleMapModeImage = null;
		private UnityEngine.UI.Text m_E_SingleMapTextText = null;
		private UITextLocalizeMonoView m_E_SingleMapTextUITextLocalizeMonoView = null;
		private UnityEngine.UI.Button m_E_KnapsackModeButton = null;
		private UnityEngine.UI.Image m_E_KnapsackModeImage = null;
		private UnityEngine.UI.Text m_E_KnapsackTextText = null;
		private UITextLocalizeMonoView m_E_KnapsackTextUITextLocalizeMonoView = null;
		private UnityEngine.UI.Button m_E_RoomModeButton = null;
		private UnityEngine.UI.Image m_E_RoomModeImage = null;
		private UnityEngine.UI.Text m_E_RoomModeTextText = null;
		private UITextLocalizeMonoView m_E_RoomModeTextUITextLocalizeMonoView = null;
		private UnityEngine.UI.Button m_E_ARRoomModeCreateButton = null;
		private UnityEngine.UI.Image m_E_ARRoomModeCreateImage = null;
		private UnityEngine.UI.Text m_E_ARRoomModeCreateTextText = null;
		private UITextLocalizeMonoView m_E_ARRoomModeCreateTextUITextLocalizeMonoView = null;
		private UnityEngine.UI.Button m_E_ARRoomModeJoinButton = null;
		private UnityEngine.UI.Image m_E_ARRoomModeJoinImage = null;
		private UnityEngine.UI.Text m_E_ARRoomModeJoinTextText = null;
		private UITextLocalizeMonoView m_E_ARRoomModeJoinTextUITextLocalizeMonoView = null;
		public Transform uiTransform = null;
	}
}
