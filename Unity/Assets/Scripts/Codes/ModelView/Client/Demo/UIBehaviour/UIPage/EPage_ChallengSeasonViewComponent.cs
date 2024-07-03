
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[EnableMethod]
	public class EPage_ChallengSeasonViewComponent : Entity, ET.IAwake<UnityEngine.Transform>, IDestroy
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

		public TMPro.TextMeshProUGUI ELabelRestofseasonTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabelRestofseasonTextMeshProUGUI == null )
				{
					this.m_ELabelRestofseasonTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "info/ELabelRestofseason");
				}
				return this.m_ELabelRestofseasonTextMeshProUGUI;
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

		public UnityEngine.UI.Image E_line02Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_line02Image == null )
				{
					this.m_E_line02Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "info/E_line02");
				}
				return this.m_E_line02Image;
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

		public UnityEngine.UI.LoopHorizontalScrollRect ELoopScrollList_propLoopHorizontalScrollRect
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELoopScrollList_propLoopHorizontalScrollRect == null )
				{
					this.m_ELoopScrollList_propLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject, "info/monster/list/ELoopScrollList_prop");
				}
				return this.m_ELoopScrollList_propLoopHorizontalScrollRect;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_LvTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_LvTextMeshProUGUI == null )
				{
					this.m_ELabel_LvTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "info/ELabel_Lv");
				}
				return this.m_ELabel_LvTextMeshProUGUI;
			}
		}

		public void DestroyWidget()
		{
			this.m_ELabelModeDesTextMeshProUGUI = null;
			this.m_ELabelRestofseasonTextMeshProUGUI = null;
			this.m_ELoopScrollList_ChallengeLoopHorizontalScrollRect = null;
			this.m_E_SelectButton = null;
			this.m_E_SelectImage = null;
			this.m_E_UnlockedButton = null;
			this.m_E_UnlockedImage = null;
			this.m_E_line02Image = null;
			this.m_EG_RewardRectTransform = null;
			this.m_ELoopScrollList_RewardLoopHorizontalScrollRect = null;
			this.m_ELoopScrollList_propLoopHorizontalScrollRect = null;
			this.m_ELabel_LvTextMeshProUGUI = null;
			this.uiTransform = null;
		}

		private TMPro.TextMeshProUGUI m_ELabelModeDesTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_ELabelRestofseasonTextMeshProUGUI = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopScrollList_ChallengeLoopHorizontalScrollRect = null;
		private UnityEngine.UI.Button m_E_SelectButton = null;
		private UnityEngine.UI.Image m_E_SelectImage = null;
		private UnityEngine.UI.Button m_E_UnlockedButton = null;
		private UnityEngine.UI.Image m_E_UnlockedImage = null;
		private UnityEngine.UI.Image m_E_line02Image = null;
		private UnityEngine.RectTransform m_EG_RewardRectTransform = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopScrollList_RewardLoopHorizontalScrollRect = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopScrollList_propLoopHorizontalScrollRect = null;
		private TMPro.TextMeshProUGUI m_ELabel_LvTextMeshProUGUI = null;
		public Transform uiTransform = null;
	}
}
