
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[EnableMethod]
	public class Scroll_Item_SkillBattleInfo : Entity, IAwake, IDestroy, IUIScrollItem, IUILogic
	{
		public long DataId {get;set;}
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_Item_SkillBattleInfo BindTrans(Transform trans)
		{
			this.uiTransform = trans;
			return this;
		}

		public UnityEngine.RectTransform EG_RootRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if (this.isCacheNode)
				{
					if( this.m_EG_RootRectTransform == null )
					{
						this.m_EG_RootRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_Root");
					}
					return this.m_EG_RootRectTransform;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_Root");
				}
			}
		}

		public UnityEngine.UI.Button EButton_IconButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if (this.isCacheNode)
				{
					if( this.m_EButton_IconButton == null )
					{
						this.m_EButton_IconButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EG_Root/EButton_Icon");
					}
					return this.m_EButton_IconButton;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EG_Root/EButton_Icon");
				}
			}
		}

		public UnityEngine.UI.Image EButton_IconImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if (this.isCacheNode)
				{
					if( this.m_EButton_IconImage == null )
					{
						this.m_EButton_IconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_Root/EButton_Icon");
					}
					return this.m_EButton_IconImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_Root/EButton_Icon");
				}
			}
		}

		public TMPro.TextMeshProUGUI EButton_nameTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if (this.isCacheNode)
				{
					if( this.m_EButton_nameTextMeshProUGUI == null )
					{
						this.m_EButton_nameTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_Root/EButton_name");
					}
					return this.m_EButton_nameTextMeshProUGUI;
				}
				else
				{
					return UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_Root/EButton_name");
				}
			}
		}

		public TMPro.TextMeshProUGUI ELabel_CDTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if (this.isCacheNode)
				{
					if( this.m_ELabel_CDTextMeshProUGUI == null )
					{
						this.m_ELabel_CDTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_Root/GameObject/ELabel_CD");
					}
					return this.m_ELabel_CDTextMeshProUGUI;
				}
				else
				{
					return UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_Root/GameObject/ELabel_CD");
				}
			}
		}

		public TMPro.TextMeshProUGUI ELabel_EnergyTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if (this.isCacheNode)
				{
					if( this.m_ELabel_EnergyTextMeshProUGUI == null )
					{
						this.m_ELabel_EnergyTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_Root/GameObject/ELabel_Energy");
					}
					return this.m_ELabel_EnergyTextMeshProUGUI;
				}
				else
				{
					return UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_Root/GameObject/ELabel_Energy");
				}
			}
		}

		public TMPro.TextMeshProUGUI ELabel_CommonEnergyTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if (this.isCacheNode)
				{
					if( this.m_ELabel_CommonEnergyTextMeshProUGUI == null )
					{
						this.m_ELabel_CommonEnergyTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_Root/GameObject/ELabel_CommonEnergy");
					}
					return this.m_ELabel_CommonEnergyTextMeshProUGUI;
				}
				else
				{
					return UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_Root/GameObject/ELabel_CommonEnergy");
				}
			}
		}

		public UnityEngine.UI.Button EButton_SelectButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if (this.isCacheNode)
				{
					if( this.m_EButton_SelectButton == null )
					{
						this.m_EButton_SelectButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EButton_Select");
					}
					return this.m_EButton_SelectButton;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EButton_Select");
				}
			}
		}

		public UnityEngine.UI.Image EButton_SelectImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if (this.isCacheNode)
				{
					if( this.m_EButton_SelectImage == null )
					{
						this.m_EButton_SelectImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EButton_Select");
					}
					return this.m_EButton_SelectImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EButton_Select");
				}
			}
		}

		public UnityEngine.UI.Button EButton_BuyEnergyButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if (this.isCacheNode)
				{
					if( this.m_EButton_BuyEnergyButton == null )
					{
						this.m_EButton_BuyEnergyButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EButton_BuyEnergy");
					}
					return this.m_EButton_BuyEnergyButton;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EButton_BuyEnergy");
				}
			}
		}

		public UnityEngine.UI.Image EButton_BuyEnergyImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if (this.isCacheNode)
				{
					if( this.m_EButton_BuyEnergyImage == null )
					{
						this.m_EButton_BuyEnergyImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EButton_BuyEnergy");
					}
					return this.m_EButton_BuyEnergyImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EButton_BuyEnergy");
				}
			}
		}

		public TMPro.TextMeshProUGUI ELabel_BuyEnergyTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if (this.isCacheNode)
				{
					if( this.m_ELabel_BuyEnergyTextMeshProUGUI == null )
					{
						this.m_ELabel_BuyEnergyTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EButton_BuyEnergy/ELabel_BuyEnergy");
					}
					return this.m_ELabel_BuyEnergyTextMeshProUGUI;
				}
				else
				{
					return UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EButton_BuyEnergy/ELabel_BuyEnergy");
				}
			}
		}

		public UITextLocalizeMonoView ELabel_BuyEnergyUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if (this.isCacheNode)
				{
					if( this.m_ELabel_BuyEnergyUITextLocalizeMonoView == null )
					{
						this.m_ELabel_BuyEnergyUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "EButton_BuyEnergy/ELabel_BuyEnergy");
					}
					return this.m_ELabel_BuyEnergyUITextLocalizeMonoView;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "EButton_BuyEnergy/ELabel_BuyEnergy");
				}
			}
		}

		public UnityEngine.UI.Button EButton_ShowDetailButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if (this.isCacheNode)
				{
					if( this.m_EButton_ShowDetailButton == null )
					{
						this.m_EButton_ShowDetailButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EButton_ShowDetail");
					}
					return this.m_EButton_ShowDetailButton;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EButton_ShowDetail");
				}
			}
		}

		public UnityEngine.UI.Image EButton_ShowDetailImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if (this.isCacheNode)
				{
					if( this.m_EButton_ShowDetailImage == null )
					{
						this.m_EButton_ShowDetailImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EButton_ShowDetail");
					}
					return this.m_EButton_ShowDetailImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EButton_ShowDetail");
				}
			}
		}

		public TMPro.TextMeshProUGUI ELabel_ShowDetailTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if (this.isCacheNode)
				{
					if( this.m_ELabel_ShowDetailTextMeshProUGUI == null )
					{
						this.m_ELabel_ShowDetailTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EButton_ShowDetail/ELabel_ShowDetail");
					}
					return this.m_ELabel_ShowDetailTextMeshProUGUI;
				}
				else
				{
					return UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EButton_ShowDetail/ELabel_ShowDetail");
				}
			}
		}

		public UITextLocalizeMonoView ELabel_ShowDetailUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if (this.isCacheNode)
				{
					if( this.m_ELabel_ShowDetailUITextLocalizeMonoView == null )
					{
						this.m_ELabel_ShowDetailUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "EButton_ShowDetail/ELabel_ShowDetail");
					}
					return this.m_ELabel_ShowDetailUITextLocalizeMonoView;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "EButton_ShowDetail/ELabel_ShowDetail");
				}
			}
		}

		public void DestroyWidget()
		{
			this.m_EG_RootRectTransform = null;
			this.m_EButton_IconButton = null;
			this.m_EButton_IconImage = null;
			this.m_EButton_nameTextMeshProUGUI = null;
			this.m_ELabel_CDTextMeshProUGUI = null;
			this.m_ELabel_EnergyTextMeshProUGUI = null;
			this.m_ELabel_CommonEnergyTextMeshProUGUI = null;
			this.m_EButton_SelectButton = null;
			this.m_EButton_SelectImage = null;
			this.m_EButton_BuyEnergyButton = null;
			this.m_EButton_BuyEnergyImage = null;
			this.m_ELabel_BuyEnergyTextMeshProUGUI = null;
			this.m_ELabel_BuyEnergyUITextLocalizeMonoView = null;
			this.m_EButton_ShowDetailButton = null;
			this.m_EButton_ShowDetailImage = null;
			this.m_ELabel_ShowDetailTextMeshProUGUI = null;
			this.m_ELabel_ShowDetailUITextLocalizeMonoView = null;
			this.uiTransform = null;
			this.DataId = 0;
		}

		private UnityEngine.RectTransform m_EG_RootRectTransform = null;
		private UnityEngine.UI.Button m_EButton_IconButton = null;
		private UnityEngine.UI.Image m_EButton_IconImage = null;
		private TMPro.TextMeshProUGUI m_EButton_nameTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_ELabel_CDTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_ELabel_EnergyTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_ELabel_CommonEnergyTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_EButton_SelectButton = null;
		private UnityEngine.UI.Image m_EButton_SelectImage = null;
		private UnityEngine.UI.Button m_EButton_BuyEnergyButton = null;
		private UnityEngine.UI.Image m_EButton_BuyEnergyImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_BuyEnergyTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_ELabel_BuyEnergyUITextLocalizeMonoView = null;
		private UnityEngine.UI.Button m_EButton_ShowDetailButton = null;
		private UnityEngine.UI.Image m_EButton_ShowDetailImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_ShowDetailTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_ELabel_ShowDetailUITextLocalizeMonoView = null;
		public Transform uiTransform = null;
	}
}
