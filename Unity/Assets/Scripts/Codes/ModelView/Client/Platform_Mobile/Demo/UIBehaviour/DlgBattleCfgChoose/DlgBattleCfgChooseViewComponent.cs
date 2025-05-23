﻿
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

		public UnityEngine.UI.Button E_SureButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_SureButton == null )
				{
					this.m_E_SureButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/E_Sure");
				}
				return this.m_E_SureButton;
			}
		}

		public UnityEngine.UI.Image E_SureImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_SureImage == null )
				{
					this.m_E_SureImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/E_Sure");
				}
				return this.m_E_SureImage;
			}
		}

		public UnityEngine.UI.Text ELable_SureText
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELable_SureText == null )
				{
					this.m_ELable_SureText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject, "EGBackGround/E_Sure/ELable_Sure");
				}
				return this.m_ELable_SureText;
			}
		}

		public UITextLocalizeMonoView ELable_SureUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELable_SureUITextLocalizeMonoView == null )
				{
					this.m_ELable_SureUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "EGBackGround/E_Sure/ELable_Sure");
				}
				return this.m_ELable_SureUITextLocalizeMonoView;
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
					this.m_ELoopScrollList_ItemLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject, "EGBackGround/E_BattleChooseList/ELoopScrollList_Item");
				}
				return this.m_ELoopScrollList_ItemLoopHorizontalScrollRect;
			}
		}

		public TMPro.TMP_Dropdown E_DropdownGameModeTMP_Dropdown
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_DropdownGameModeTMP_Dropdown == null )
				{
					this.m_E_DropdownGameModeTMP_Dropdown = UIFindHelper.FindDeepChild<TMPro.TMP_Dropdown>(this.uiTransform.gameObject, "EGBackGround/E_Menu/E_GameMode/E_DropdownGameMode");
				}
				return this.m_E_DropdownGameModeTMP_Dropdown;
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

		public TMPro.TMP_Dropdown E_DropdownTeamModeTMP_Dropdown
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_DropdownTeamModeTMP_Dropdown == null )
				{
					this.m_E_DropdownTeamModeTMP_Dropdown = UIFindHelper.FindDeepChild<TMPro.TMP_Dropdown>(this.uiTransform.gameObject, "EGBackGround/E_Menu/E_TeamMode/E_DropdownTeamMode");
				}
				return this.m_E_DropdownTeamModeTMP_Dropdown;
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

		public TMPro.TMP_InputField E_InputFieldTMP_InputField
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_InputFieldTMP_InputField == null )
				{
					this.m_E_InputFieldTMP_InputField = UIFindHelper.FindDeepChild<TMPro.TMP_InputField>(this.uiTransform.gameObject, "EGBackGround/E_Menu/E_InputField");
				}
				return this.m_E_InputFieldTMP_InputField;
			}
		}

		public UnityEngine.UI.Image E_InputFieldImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_InputFieldImage == null )
				{
					this.m_E_InputFieldImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/E_Menu/E_InputField");
				}
				return this.m_E_InputFieldImage;
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
			this.m_EGBackGroundRectTransform = null;
			this.m_EGBackGroundImage = null;
			this.m_E_SureButton = null;
			this.m_E_SureImage = null;
			this.m_ELable_SureText = null;
			this.m_ELable_SureUITextLocalizeMonoView = null;
			this.m_E_BackButton = null;
			this.m_E_BackImage = null;
			this.m_ELoopScrollList_ItemLoopHorizontalScrollRect = null;
			this.m_E_DropdownGameModeTMP_Dropdown = null;
			this.m_E_DropdownGameModeImage = null;
			this.m_E_DropdownTeamModeTMP_Dropdown = null;
			this.m_E_DropdownTeamModeImage = null;
			this.m_E_InputFieldTMP_InputField = null;
			this.m_E_InputFieldImage = null;
			this.m_EG_OpenAnimationRectTransform = null;
			this.m_EG_CloseAnimationRectTransform = null;
			this.uiTransform = null;
		}

		private UnityEngine.RectTransform m_EGBackGroundRectTransform = null;
		private UnityEngine.UI.Image m_EGBackGroundImage = null;
		private UnityEngine.UI.Button m_E_SureButton = null;
		private UnityEngine.UI.Image m_E_SureImage = null;
		private UnityEngine.UI.Text m_ELable_SureText = null;
		private UITextLocalizeMonoView m_ELable_SureUITextLocalizeMonoView = null;
		private UnityEngine.UI.Button m_E_BackButton = null;
		private UnityEngine.UI.Image m_E_BackImage = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopScrollList_ItemLoopHorizontalScrollRect = null;
		private TMPro.TMP_Dropdown m_E_DropdownGameModeTMP_Dropdown = null;
		private UnityEngine.UI.Image m_E_DropdownGameModeImage = null;
		private TMPro.TMP_Dropdown m_E_DropdownTeamModeTMP_Dropdown = null;
		private UnityEngine.UI.Image m_E_DropdownTeamModeImage = null;
		private TMPro.TMP_InputField m_E_InputFieldTMP_InputField = null;
		private UnityEngine.UI.Image m_E_InputFieldImage = null;
		private UnityEngine.RectTransform m_EG_OpenAnimationRectTransform = null;
		private UnityEngine.RectTransform m_EG_CloseAnimationRectTransform = null;
		public Transform uiTransform = null;
	}
}
