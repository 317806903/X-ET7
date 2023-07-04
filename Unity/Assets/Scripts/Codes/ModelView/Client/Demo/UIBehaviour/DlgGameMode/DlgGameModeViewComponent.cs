
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgGameMode))]
	[EnableMethod]
	public  class DlgGameModeViewComponent : Entity,IAwake,IDestroy 
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
		    		this.m_EGBackGroundRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject,"EGBackGround");
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
		    		this.m_E_ReturnLoginButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EGBackGround/E_ReturnLogin");
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
		    		this.m_E_ReturnLoginImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/E_ReturnLogin");
     			}
     			return this.m_E_ReturnLoginImage;
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
		    		this.m_E_SingleMapModeButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EGBackGround/E_SingleMapMode");
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
		    		this.m_E_SingleMapModeImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/E_SingleMapMode");
     			}
     			return this.m_E_SingleMapModeImage;
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
		    		this.m_E_RoomModeButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EGBackGround/E_RoomMode");
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
		    		this.m_E_RoomModeImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/E_RoomMode");
     			}
     			return this.m_E_RoomModeImage;
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
		    		this.m_E_ARRoomModeButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EGBackGround/E_ARRoomMode");
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
		    		this.m_E_ARRoomModeImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/E_ARRoomMode");
     			}
     			return this.m_E_ARRoomModeImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_EGBackGroundRectTransform = null;
			this.m_E_ReturnLoginButton = null;
			this.m_E_ReturnLoginImage = null;
			this.m_E_SingleMapModeButton = null;
			this.m_E_SingleMapModeImage = null;
			this.m_E_RoomModeButton = null;
			this.m_E_RoomModeImage = null;
			this.m_E_ARRoomModeButton = null;
			this.m_E_ARRoomModeImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.RectTransform m_EGBackGroundRectTransform = null;
		private UnityEngine.UI.Button m_E_ReturnLoginButton = null;
		private UnityEngine.UI.Image m_E_ReturnLoginImage = null;
		private UnityEngine.UI.Button m_E_SingleMapModeButton = null;
		private UnityEngine.UI.Image m_E_SingleMapModeImage = null;
		private UnityEngine.UI.Button m_E_RoomModeButton = null;
		private UnityEngine.UI.Image m_E_RoomModeImage = null;
		private UnityEngine.UI.Button m_E_ARRoomModeButton = null;
		private UnityEngine.UI.Image m_E_ARRoomModeImage = null;
		public Transform uiTransform = null;
	}
}
