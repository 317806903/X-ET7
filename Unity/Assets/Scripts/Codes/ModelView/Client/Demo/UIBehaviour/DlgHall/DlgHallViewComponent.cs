
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgHall))]
	[EnableMethod]
	public  class DlgHallViewComponent : Entity,IAwake,IDestroy 
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

		public UnityEngine.UI.Button E_CreateRoomButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CreateRoomButton == null )
     			{
		    		this.m_E_CreateRoomButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EGBackGround/E_CreateRoom");
     			}
     			return this.m_E_CreateRoomButton;
     		}
     	}

		public UnityEngine.UI.Image E_CreateRoomImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CreateRoomImage == null )
     			{
		    		this.m_E_CreateRoomImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/E_CreateRoom");
     			}
     			return this.m_E_CreateRoomImage;
     		}
     	}

		public UnityEngine.UI.LoopHorizontalScrollRect ELoopScrollList_RoomLoopHorizontalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELoopScrollList_RoomLoopHorizontalScrollRect == null )
     			{
		    		this.m_ELoopScrollList_RoomLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject,"EGBackGround/ELoopScrollList_Room");
     			}
     			return this.m_ELoopScrollList_RoomLoopHorizontalScrollRect;
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

		public void DestroyWidget()
		{
			this.m_EGBackGroundRectTransform = null;
			this.m_E_CreateRoomButton = null;
			this.m_E_CreateRoomImage = null;
			this.m_ELoopScrollList_RoomLoopHorizontalScrollRect = null;
			this.m_E_ReturnLoginButton = null;
			this.m_E_ReturnLoginImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.RectTransform m_EGBackGroundRectTransform = null;
		private UnityEngine.UI.Button m_E_CreateRoomButton = null;
		private UnityEngine.UI.Image m_E_CreateRoomImage = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopScrollList_RoomLoopHorizontalScrollRect = null;
		private UnityEngine.UI.Button m_E_ReturnLoginButton = null;
		private UnityEngine.UI.Image m_E_ReturnLoginImage = null;
		public Transform uiTransform = null;
	}
}
