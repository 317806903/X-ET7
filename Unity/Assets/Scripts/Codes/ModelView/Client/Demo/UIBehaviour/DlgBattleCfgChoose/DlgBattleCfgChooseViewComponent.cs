
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgBattleCfgChoose))]
	[EnableMethod]
	public class DlgBattleCfgChooseViewComponent : Entity, IAwake, IDestroy
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

		public UnityEngine.UI.Button E_RoomLevelChooseButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_RoomLevelChooseButton == null )
				{
					this.m_E_RoomLevelChooseButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/E_RoomLevelChoose");
				}
				return this.m_E_RoomLevelChooseButton;
			}
		}

		public UnityEngine.UI.Image E_RoomLevelChooseImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_RoomLevelChooseImage == null )
				{
					this.m_E_RoomLevelChooseImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/E_RoomLevelChoose");
				}
				return this.m_E_RoomLevelChooseImage;
			}
		}

		public UnityEngine.UI.Text ELable_RoomLevelChooseText
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELable_RoomLevelChooseText == null )
				{
					this.m_ELable_RoomLevelChooseText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject, "EGBackGround/E_RoomLevelChoose/ELable_RoomLevelChoose");
				}
				return this.m_ELable_RoomLevelChooseText;
			}
		}

		public UnityEngine.UI.Button E_BackButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_BackButton == null )
				{
					this.m_E_BackButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/E_Back");
				}
				return this.m_E_BackButton;
			}
		}

		public UnityEngine.UI.Image E_BackImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_BackImage == null )
				{
					this.m_E_BackImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/E_Back");
				}
				return this.m_E_BackImage;
			}
		}

		public UnityEngine.UI.LoopHorizontalScrollRect ELoopScrollList_ItemLoopHorizontalScrollRect
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELoopScrollList_ItemLoopHorizontalScrollRect == null )
				{
					this.m_ELoopScrollList_ItemLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject, "EGBackGround/RoomLevelChooseList/ELoopScrollList_Item");
				}
				return this.m_ELoopScrollList_ItemLoopHorizontalScrollRect;
			}
		}

		public UnityEngine.UI.Image E_DropdownGameModeImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_DropdownGameModeImage == null )
				{
					this.m_E_DropdownGameModeImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/E_Menu/E_GameMode/E_DropdownGameMode");
				}
				return this.m_E_DropdownGameModeImage;
			}
		}

		public UnityEngine.UI.Image E_DropdownTeamModeImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_DropdownTeamModeImage == null )
				{
					this.m_E_DropdownTeamModeImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/E_Menu/E_TeamMode/E_DropdownTeamMode");
				}
				return this.m_E_DropdownTeamModeImage;
			}
		}

		public void DestroyWidget()
		{
			this.m_EGBackGroundRectTransform = null;
			this.m_EGBackGroundImage = null;
			this.m_E_RoomLevelChooseButton = null;
			this.m_E_RoomLevelChooseImage = null;
			this.m_ELable_RoomLevelChooseText = null;
			this.m_E_BackButton = null;
			this.m_E_BackImage = null;
			this.m_ELoopScrollList_ItemLoopHorizontalScrollRect = null;
			this.m_E_DropdownGameModeImage = null;
			this.m_E_DropdownTeamModeImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.RectTransform m_EGBackGroundRectTransform = null;
		private UnityEngine.UI.Image m_EGBackGroundImage = null;
		private UnityEngine.UI.Button m_E_RoomLevelChooseButton = null;
		private UnityEngine.UI.Image m_E_RoomLevelChooseImage = null;
		private UnityEngine.UI.Text m_ELable_RoomLevelChooseText = null;
		private UnityEngine.UI.Button m_E_BackButton = null;
		private UnityEngine.UI.Image m_E_BackImage = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopScrollList_ItemLoopHorizontalScrollRect = null;
		private UnityEngine.UI.Image m_E_DropdownGameModeImage = null;
		private UnityEngine.UI.Image m_E_DropdownTeamModeImage = null;
		public Transform uiTransform = null;
	}
}
