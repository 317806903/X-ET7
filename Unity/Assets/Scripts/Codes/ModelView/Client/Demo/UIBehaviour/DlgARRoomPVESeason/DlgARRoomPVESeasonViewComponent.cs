﻿
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgARRoomPVESeason))]
	[EnableMethod]
	public class DlgARRoomPVESeasonViewComponent : Entity, IAwake, IDestroy
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

		public UnityEngine.UI.Button E_BG_ClickButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_BG_ClickButton == null )
				{
					this.m_E_BG_ClickButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/E_BG_Click");
				}
				return this.m_E_BG_ClickButton;
			}
		}

		public UnityEngine.UI.Image E_BG_ClickImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_BG_ClickImage == null )
				{
					this.m_E_BG_ClickImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/E_BG_Click");
				}
				return this.m_E_BG_ClickImage;
			}
		}

		public BlurBackground.TranslucentImage E_BGTranslucentImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_BGTranslucentImage == null )
				{
					this.m_E_BGTranslucentImage = UIFindHelper.FindDeepChild<BlurBackground.TranslucentImage>(this.uiTransform.gameObject, "EGBackGround/E_BG_Click/E_BG");
				}
				return this.m_E_BGTranslucentImage;
			}
		}

		public UnityEngine.UI.Image E_BGARImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_BGARImage == null )
				{
					this.m_E_BGARImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/E_BG_Click/E_BGAR");
				}
				return this.m_E_BGARImage;
			}
		}

		public UnityEngine.RectTransform EGRootRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EGRootRectTransform == null )
				{
					this.m_EGRootRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround/EGRoot");
				}
				return this.m_EGRootRectTransform;
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
					this.m_E_QuitRoomButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/EGRoot/E_QuitRoom");
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
					this.m_E_QuitRoomImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/EGRoot/E_QuitRoom");
				}
				return this.m_E_QuitRoomImage;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_LevelTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_LevelTextMeshProUGUI == null )
				{
					this.m_ELabel_LevelTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/EGRoot/E_OperationPanel/Level/title/ELabel_Level");
				}
				return this.m_ELabel_LevelTextMeshProUGUI;
			}
		}

		public UITextLocalizeMonoView ELabel_LevelUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_LevelUITextLocalizeMonoView == null )
				{
					this.m_ELabel_LevelUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "EGBackGround/EGRoot/E_OperationPanel/Level/title/ELabel_Level");
				}
				return this.m_ELabel_LevelUITextLocalizeMonoView;
			}
		}

		public UnityEngine.UI.LoopHorizontalScrollRect ELoopScrollList_RewardLoopHorizontalScrollRect
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELoopScrollList_RewardLoopHorizontalScrollRect == null )
				{
					this.m_ELoopScrollList_RewardLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject, "EGBackGround/EGRoot/E_OperationPanel/Level/Reward/list/ELoopScrollList_Reward");
				}
				return this.m_ELoopScrollList_RewardLoopHorizontalScrollRect;
			}
		}

		public UnityEngine.UI.LoopHorizontalScrollRect ELoopScrollList_MonsterLoopHorizontalScrollRect
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELoopScrollList_MonsterLoopHorizontalScrollRect == null )
				{
					this.m_ELoopScrollList_MonsterLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject, "EGBackGround/EGRoot/E_OperationPanel/Level/monster/list/ELoopScrollList_Monster");
				}
				return this.m_ELoopScrollList_MonsterLoopHorizontalScrollRect;
			}
		}

		public UnityEngine.UI.Image EImage_Label1Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EImage_Label1Image == null )
				{
					this.m_EImage_Label1Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/EGRoot/E_OperationPanel/Level/monster/tips/EImage_Label1");
				}
				return this.m_EImage_Label1Image;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_Label1TextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_Label1TextMeshProUGUI == null )
				{
					this.m_ELabel_Label1TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/EGRoot/E_OperationPanel/Level/monster/tips/EImage_Label1/ELabel_Label1");
				}
				return this.m_ELabel_Label1TextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Button E_ReScanButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ReScanButton == null )
				{
					this.m_E_ReScanButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/EGRoot/E_OperationPanel/Info/E_ReScan");
				}
				return this.m_E_ReScanButton;
			}
		}

		public UnityEngine.UI.Button E_ShowQrCodeButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ShowQrCodeButton == null )
				{
					this.m_E_ShowQrCodeButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/EGRoot/E_OperationPanel/Info/E_ShowQrCode");
				}
				return this.m_E_ShowQrCodeButton;
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
					this.m_E_RoomMemberStatusButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/EGRoot/E_OperationPanel/Info/E_RoomMemberStatus");
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
					this.m_E_RoomMemberStatusImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/EGRoot/E_OperationPanel/Info/E_RoomMemberStatus");
				}
				return this.m_E_RoomMemberStatusImage;
			}
		}

		public TMPro.TextMeshProUGUI ELable_RoomMemberStatusTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELable_RoomMemberStatusTextMeshProUGUI == null )
				{
					this.m_ELable_RoomMemberStatusTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/EGRoot/E_OperationPanel/Info/E_RoomMemberStatus/ELable_RoomMemberStatus");
				}
				return this.m_ELable_RoomMemberStatusTextMeshProUGUI;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_TakePhysicalStrengthNumTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_TakePhysicalStrengthNumTextMeshProUGUI == null )
				{
					this.m_ELabel_TakePhysicalStrengthNumTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/EGRoot/E_OperationPanel/Info/E_RoomMemberStatus/ELabel_TakePhysicalStrengthNum");
				}
				return this.m_ELabel_TakePhysicalStrengthNumTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Image E_CointIconImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_CointIconImage == null )
				{
					this.m_E_CointIconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/EGRoot/E_OperationPanel/Info/E_RoomMemberStatus/E_CointIcon");
				}
				return this.m_E_CointIconImage;
			}
		}

		public UnityEngine.UI.LoopHorizontalScrollRect ELoopScrollList_RoomMemberLoopHorizontalScrollRect
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELoopScrollList_RoomMemberLoopHorizontalScrollRect == null )
				{
					this.m_ELoopScrollList_RoomMemberLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject, "EGBackGround/EGRoot/E_OperationPanel/Info/info/list/ELoopScrollList_RoomMember");
				}
				return this.m_ELoopScrollList_RoomMemberLoopHorizontalScrollRect;
			}
		}

		public void DestroyWidget()
		{
			this.m_EGBackGroundRectTransform = null;
			this.m_E_BG_ClickButton = null;
			this.m_E_BG_ClickImage = null;
			this.m_E_BGTranslucentImage = null;
			this.m_E_BGARImage = null;
			this.m_EGRootRectTransform = null;
			this.m_E_QuitRoomButton = null;
			this.m_E_QuitRoomImage = null;
			this.m_ELabel_LevelTextMeshProUGUI = null;
			this.m_ELabel_LevelUITextLocalizeMonoView = null;
			this.m_ELoopScrollList_RewardLoopHorizontalScrollRect = null;
			this.m_ELoopScrollList_MonsterLoopHorizontalScrollRect = null;
			this.m_EImage_Label1Image = null;
			this.m_ELabel_Label1TextMeshProUGUI = null;
			this.m_E_ReScanButton = null;
			this.m_E_ShowQrCodeButton = null;
			this.m_E_RoomMemberStatusButton = null;
			this.m_E_RoomMemberStatusImage = null;
			this.m_ELable_RoomMemberStatusTextMeshProUGUI = null;
			this.m_ELabel_TakePhysicalStrengthNumTextMeshProUGUI = null;
			this.m_E_CointIconImage = null;
			this.m_ELoopScrollList_RoomMemberLoopHorizontalScrollRect = null;
			this.uiTransform = null;
		}

		private UnityEngine.RectTransform m_EGBackGroundRectTransform = null;
		private UnityEngine.UI.Button m_E_BG_ClickButton = null;
		private UnityEngine.UI.Image m_E_BG_ClickImage = null;
		private BlurBackground.TranslucentImage m_E_BGTranslucentImage = null;
		private UnityEngine.UI.Image m_E_BGARImage = null;
		private UnityEngine.RectTransform m_EGRootRectTransform = null;
		private UnityEngine.UI.Button m_E_QuitRoomButton = null;
		private UnityEngine.UI.Image m_E_QuitRoomImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_LevelTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_ELabel_LevelUITextLocalizeMonoView = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopScrollList_RewardLoopHorizontalScrollRect = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopScrollList_MonsterLoopHorizontalScrollRect = null;
		private UnityEngine.UI.Image m_EImage_Label1Image = null;
		private TMPro.TextMeshProUGUI m_ELabel_Label1TextMeshProUGUI = null;
		private UnityEngine.UI.Button m_E_ReScanButton = null;
		private UnityEngine.UI.Button m_E_ShowQrCodeButton = null;
		private UnityEngine.UI.Button m_E_RoomMemberStatusButton = null;
		private UnityEngine.UI.Image m_E_RoomMemberStatusImage = null;
		private TMPro.TextMeshProUGUI m_ELable_RoomMemberStatusTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_ELabel_TakePhysicalStrengthNumTextMeshProUGUI = null;
		private UnityEngine.UI.Image m_E_CointIconImage = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopScrollList_RoomMemberLoopHorizontalScrollRect = null;
		public Transform uiTransform = null;
	}
}