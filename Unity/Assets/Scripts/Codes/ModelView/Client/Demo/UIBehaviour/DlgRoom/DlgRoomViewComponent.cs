
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgRoom))]
	[EnableMethod]
	public class DlgRoomViewComponent : Entity, IAwake, IDestroy
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

		public UnityEngine.UI.Text ELabel_RoomIdText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELabel_RoomIdText == null )
     			{
		    		this.m_ELabel_RoomIdText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"EGBackGround/ELabel_RoomId");
     			}
     			return this.m_ELabel_RoomIdText;
     		}
     	}

		public UnityEngine.UI.Button E_RoomMemberStatusButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_RoomMemberStatusButton == null )
     			{
		    		this.m_E_RoomMemberStatusButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EGBackGround/E_RoomMemberStatus");
     			}
     			return this.m_E_RoomMemberStatusButton;
     		}
     	}

		public UnityEngine.UI.Image E_RoomMemberStatusImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_RoomMemberStatusImage == null )
     			{
		    		this.m_E_RoomMemberStatusImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/E_RoomMemberStatus");
     			}
     			return this.m_E_RoomMemberStatusImage;
     		}
     	}

		public UnityEngine.UI.Text ELable_RoomMemberStatusText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELable_RoomMemberStatusText == null )
     			{
		    		this.m_ELable_RoomMemberStatusText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"EGBackGround/E_RoomMemberStatus/ELable_RoomMemberStatus");
     			}
     			return this.m_ELable_RoomMemberStatusText;
     		}
     	}

		public UnityEngine.UI.Button E_QuitRoomButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_QuitRoomButton == null )
     			{
		    		this.m_E_QuitRoomButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EGBackGround/E_QuitRoom");
     			}
     			return this.m_E_QuitRoomButton;
     		}
     	}

		public UnityEngine.UI.Image E_QuitRoomImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_QuitRoomImage == null )
     			{
		    		this.m_E_QuitRoomImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/E_QuitRoom");
     			}
     			return this.m_E_QuitRoomImage;
     		}
     	}

		public UnityEngine.UI.LoopHorizontalScrollRect ELoopScrollList_MemberLoopHorizontalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELoopScrollList_MemberLoopHorizontalScrollRect == null )
     			{
		    		this.m_ELoopScrollList_MemberLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject,"EGBackGround/RoomMemberList/ELoopScrollList_Member");
     			}
     			return this.m_ELoopScrollList_MemberLoopHorizontalScrollRect;
     		}
     	}

		public UnityEngine.UI.InputField E_InputFieldBattleLevelCfgInputField
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_InputFieldBattleLevelCfgInputField == null )
     			{
		    		this.m_E_InputFieldBattleLevelCfgInputField = UIFindHelper.FindDeepChild<UnityEngine.UI.InputField>(this.uiTransform.gameObject,"EGBackGround/E_InputFieldBattleLevelCfg");
     			}
     			return this.m_E_InputFieldBattleLevelCfgInputField;
     		}
     	}

		public UnityEngine.UI.Image E_InputFieldBattleLevelCfgImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_InputFieldBattleLevelCfgImage == null )
     			{
		    		this.m_E_InputFieldBattleLevelCfgImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/E_InputFieldBattleLevelCfg");
     			}
     			return this.m_E_InputFieldBattleLevelCfgImage;
     		}
     	}

		public UnityEngine.UI.Text E_TextText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_TextText == null )
     			{
		    		this.m_E_TextText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"EGBackGround/E_InputFieldBattleLevelCfg/E_Text");
     			}
     			return this.m_E_TextText;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_EGBackGroundRectTransform = null;
			this.m_ELabel_RoomIdText = null;
			this.m_E_RoomMemberStatusButton = null;
			this.m_E_RoomMemberStatusImage = null;
			this.m_ELable_RoomMemberStatusText = null;
			this.m_E_QuitRoomButton = null;
			this.m_E_QuitRoomImage = null;
			this.m_ELoopScrollList_MemberLoopHorizontalScrollRect = null;
			this.m_E_InputFieldBattleLevelCfgInputField = null;
			this.m_E_InputFieldBattleLevelCfgImage = null;
			this.m_E_TextText = null;
			this.uiTransform = null;
		}

		private UnityEngine.RectTransform m_EGBackGroundRectTransform = null;
		private UnityEngine.UI.Text m_ELabel_RoomIdText = null;
		private UnityEngine.UI.Button m_E_RoomMemberStatusButton = null;
		private UnityEngine.UI.Image m_E_RoomMemberStatusImage = null;
		private UnityEngine.UI.Text m_ELable_RoomMemberStatusText = null;
		private UnityEngine.UI.Button m_E_QuitRoomButton = null;
		private UnityEngine.UI.Image m_E_QuitRoomImage = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopScrollList_MemberLoopHorizontalScrollRect = null;
		private UnityEngine.UI.InputField m_E_InputFieldBattleLevelCfgInputField = null;
		private UnityEngine.UI.Image m_E_InputFieldBattleLevelCfgImage = null;
		private UnityEngine.UI.Text m_E_TextText = null;
		public Transform uiTransform = null;
	}
}
