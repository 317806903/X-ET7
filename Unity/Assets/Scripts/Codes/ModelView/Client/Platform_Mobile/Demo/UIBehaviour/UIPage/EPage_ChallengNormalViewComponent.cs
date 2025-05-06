
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[EnableMethod]
	public class EPage_ChallengNormalViewComponent : Entity, ET.IAwake<UnityEngine.Transform>, IDestroy
	{
		public TMPro.TextMeshProUGUI ELabelModeDesTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabelModeDesTextMeshProUGUI == null )
				{
					this.m_ELabelModeDesTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "info/ELabelModeDes");
				}
				return this.m_ELabelModeDesTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.LoopHorizontalScrollRect ELoopScrollList_ChallengeLoopHorizontalScrollRect
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELoopScrollList_ChallengeLoopHorizontalScrollRect == null )
				{
					this.m_ELoopScrollList_ChallengeLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject, "info/list/ELoopScrollList_Challenge");
				}
				return this.m_ELoopScrollList_ChallengeLoopHorizontalScrollRect;
			}
		}

		public UnityEngine.RectTransform EG_BackstoryRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_BackstoryRectTransform == null )
				{
					this.m_EG_BackstoryRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "info/EG_Backstory");
				}
				return this.m_EG_BackstoryRectTransform;
			}
		}

		public TMPro.TextMeshProUGUI ELable_Text_Backstory_describeTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELable_Text_Backstory_describeTextMeshProUGUI == null )
				{
					this.m_ELable_Text_Backstory_describeTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "info/EG_Backstory/ELable_Text_Backstory_describe");
				}
				return this.m_ELable_Text_Backstory_describeTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Image E_line03Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_line03Image == null )
				{
					this.m_E_line03Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "info/E_line03");
				}
				return this.m_E_line03Image;
			}
		}

		public UnityEngine.RectTransform EG_RewardRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_RewardRectTransform == null )
				{
					this.m_EG_RewardRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "info/EG_Reward");
				}
				return this.m_EG_RewardRectTransform;
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
					this.m_ELoopScrollList_RewardLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject, "info/EG_Reward/list/ELoopScrollList_Reward");
				}
				return this.m_ELoopScrollList_RewardLoopHorizontalScrollRect;
			}
		}

		public UnityEngine.UI.Button E_default_EasyButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_default_EasyButton == null )
				{
					this.m_E_default_EasyButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "info/E_difficultyselect_btnlist/E_default_Easy");
				}
				return this.m_E_default_EasyButton;
			}
		}

		public UnityEngine.UI.Image E_default_EasyImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_default_EasyImage == null )
				{
					this.m_E_default_EasyImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "info/E_difficultyselect_btnlist/E_default_Easy");
				}
				return this.m_E_default_EasyImage;
			}
		}

		public UnityEngine.UI.Image E_select_EasyImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_select_EasyImage == null )
				{
					this.m_E_select_EasyImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "info/E_difficultyselect_btnlist/E_select_Easy");
				}
				return this.m_E_select_EasyImage;
			}
		}

		public UnityEngine.UI.Button E_default_NormalButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_default_NormalButton == null )
				{
					this.m_E_default_NormalButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "info/E_difficultyselect_btnlist/E_default_Normal");
				}
				return this.m_E_default_NormalButton;
			}
		}

		public UnityEngine.UI.Image E_default_NormalImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_default_NormalImage == null )
				{
					this.m_E_default_NormalImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "info/E_difficultyselect_btnlist/E_default_Normal");
				}
				return this.m_E_default_NormalImage;
			}
		}

		public UnityEngine.UI.Image E_select_NormalImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_select_NormalImage == null )
				{
					this.m_E_select_NormalImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "info/E_difficultyselect_btnlist/E_select_Normal");
				}
				return this.m_E_select_NormalImage;
			}
		}

		public UnityEngine.UI.Button E_default_HardButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_default_HardButton == null )
				{
					this.m_E_default_HardButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "info/E_difficultyselect_btnlist/E_default_Hard");
				}
				return this.m_E_default_HardButton;
			}
		}

		public UnityEngine.UI.Image E_default_HardImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_default_HardImage == null )
				{
					this.m_E_default_HardImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "info/E_difficultyselect_btnlist/E_default_Hard");
				}
				return this.m_E_default_HardImage;
			}
		}

		public UnityEngine.UI.Image E_select_HardImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_select_HardImage == null )
				{
					this.m_E_select_HardImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "info/E_difficultyselect_btnlist/E_select_Hard");
				}
				return this.m_E_select_HardImage;
			}
		}

		public UnityEngine.UI.Button E_default_ExtremeButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_default_ExtremeButton == null )
				{
					this.m_E_default_ExtremeButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "info/E_difficultyselect_btnlist/E_default_Extreme");
				}
				return this.m_E_default_ExtremeButton;
			}
		}

		public UnityEngine.UI.Image E_default_ExtremeImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_default_ExtremeImage == null )
				{
					this.m_E_default_ExtremeImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "info/E_difficultyselect_btnlist/E_default_Extreme");
				}
				return this.m_E_default_ExtremeImage;
			}
		}

		public UnityEngine.UI.Image E_select_ExtremeImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_select_ExtremeImage == null )
				{
					this.m_E_select_ExtremeImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "info/E_difficultyselect_btnlist/E_select_Extreme");
				}
				return this.m_E_select_ExtremeImage;
			}
		}

		public UnityEngine.UI.Button E_SelectButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_SelectButton == null )
				{
					this.m_E_SelectButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "info/E_Select");
				}
				return this.m_E_SelectButton;
			}
		}

		public UnityEngine.UI.Image E_SelectImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_SelectImage == null )
				{
					this.m_E_SelectImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "info/E_Select");
				}
				return this.m_E_SelectImage;
			}
		}

		public UnityEngine.UI.Button E_UnlockedButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_UnlockedButton == null )
				{
					this.m_E_UnlockedButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "info/E_Unlocked");
				}
				return this.m_E_UnlockedButton;
			}
		}

		public UnityEngine.UI.Image E_UnlockedImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_UnlockedImage == null )
				{
					this.m_E_UnlockedImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "info/E_Unlocked");
				}
				return this.m_E_UnlockedImage;
			}
		}

		public UnityEngine.UI.Button E_DebugButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_DebugButton == null )
				{
					this.m_E_DebugButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_Debug");
				}
				return this.m_E_DebugButton;
			}
		}

		public UnityEngine.UI.Image E_DebugImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_DebugImage == null )
				{
					this.m_E_DebugImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Debug");
				}
				return this.m_E_DebugImage;
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
			this.m_ELabelModeDesTextMeshProUGUI = null;
			this.m_ELoopScrollList_ChallengeLoopHorizontalScrollRect = null;
			this.m_EG_BackstoryRectTransform = null;
			this.m_ELable_Text_Backstory_describeTextMeshProUGUI = null;
			this.m_E_line03Image = null;
			this.m_EG_RewardRectTransform = null;
			this.m_ELoopScrollList_RewardLoopHorizontalScrollRect = null;
			this.m_E_default_EasyButton = null;
			this.m_E_default_EasyImage = null;
			this.m_E_select_EasyImage = null;
			this.m_E_default_NormalButton = null;
			this.m_E_default_NormalImage = null;
			this.m_E_select_NormalImage = null;
			this.m_E_default_HardButton = null;
			this.m_E_default_HardImage = null;
			this.m_E_select_HardImage = null;
			this.m_E_default_ExtremeButton = null;
			this.m_E_default_ExtremeImage = null;
			this.m_E_select_ExtremeImage = null;
			this.m_E_SelectButton = null;
			this.m_E_SelectImage = null;
			this.m_E_UnlockedButton = null;
			this.m_E_UnlockedImage = null;
			this.m_E_DebugButton = null;
			this.m_E_DebugImage = null;
			this.m_EG_OpenAnimationRectTransform = null;
			this.m_EG_CloseAnimationRectTransform = null;
			this.uiTransform = null;
		}

		private TMPro.TextMeshProUGUI m_ELabelModeDesTextMeshProUGUI = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopScrollList_ChallengeLoopHorizontalScrollRect = null;
		private UnityEngine.RectTransform m_EG_BackstoryRectTransform = null;
		private TMPro.TextMeshProUGUI m_ELable_Text_Backstory_describeTextMeshProUGUI = null;
		private UnityEngine.UI.Image m_E_line03Image = null;
		private UnityEngine.RectTransform m_EG_RewardRectTransform = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopScrollList_RewardLoopHorizontalScrollRect = null;
		private UnityEngine.UI.Button m_E_default_EasyButton = null;
		private UnityEngine.UI.Image m_E_default_EasyImage = null;
		private UnityEngine.UI.Image m_E_select_EasyImage = null;
		private UnityEngine.UI.Button m_E_default_NormalButton = null;
		private UnityEngine.UI.Image m_E_default_NormalImage = null;
		private UnityEngine.UI.Image m_E_select_NormalImage = null;
		private UnityEngine.UI.Button m_E_default_HardButton = null;
		private UnityEngine.UI.Image m_E_default_HardImage = null;
		private UnityEngine.UI.Image m_E_select_HardImage = null;
		private UnityEngine.UI.Button m_E_default_ExtremeButton = null;
		private UnityEngine.UI.Image m_E_default_ExtremeImage = null;
		private UnityEngine.UI.Image m_E_select_ExtremeImage = null;
		private UnityEngine.UI.Button m_E_SelectButton = null;
		private UnityEngine.UI.Image m_E_SelectImage = null;
		private UnityEngine.UI.Button m_E_UnlockedButton = null;
		private UnityEngine.UI.Image m_E_UnlockedImage = null;
		private UnityEngine.UI.Button m_E_DebugButton = null;
		private UnityEngine.UI.Image m_E_DebugImage = null;
		private UnityEngine.RectTransform m_EG_OpenAnimationRectTransform = null;
		private UnityEngine.RectTransform m_EG_CloseAnimationRectTransform = null;
		public Transform uiTransform = null;
	}
}
