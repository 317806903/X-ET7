
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[EnableMethod]
	public partial class Scroll_Item_SkillBattleInfo : Entity, IAwake, IDestroy, IUIScrollItem, IUILogic
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

		public UnityEngine.RectTransform EG_IconRectTransform
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
					if( this.m_EG_IconRectTransform == null )
					{
						this.m_EG_IconRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_Root/EG_Icon");
					}
					return this.m_EG_IconRectTransform;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_Root/EG_Icon");
				}
			}
		}

		public UnityEngine.UI.Image EG_IconImage
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
					if( this.m_EG_IconImage == null )
					{
						this.m_EG_IconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_Root/EG_Icon");
					}
					return this.m_EG_IconImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_Root/EG_Icon");
				}
			}
		}

		public UnityEngine.UI.Image EImage_IconImage
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
					if( this.m_EImage_IconImage == null )
					{
						this.m_EImage_IconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_Root/EG_Icon/EImage_Icon");
					}
					return this.m_EImage_IconImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_Root/EG_Icon/EImage_Icon");
				}
			}
		}

		public UnityEngine.UI.Image EImage_EnergyCDImage
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
					if( this.m_EImage_EnergyCDImage == null )
					{
						this.m_EImage_EnergyCDImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_Root/EG_Icon/EImage_EnergyCD");
					}
					return this.m_EImage_EnergyCDImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_Root/EG_Icon/EImage_EnergyCD");
				}
			}
		}

		public UnityEngine.UI.Image EImage_FrameImage
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
					if( this.m_EImage_FrameImage == null )
					{
						this.m_EImage_FrameImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_Root/EImage_Frame");
					}
					return this.m_EImage_FrameImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_Root/EImage_Frame");
				}
			}
		}

		public UnityEngine.UI.Image EImage_CDImage
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
					if( this.m_EImage_CDImage == null )
					{
						this.m_EImage_CDImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_Root/EImage_CD");
					}
					return this.m_EImage_CDImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_Root/EImage_CD");
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

		public UnityEngine.RectTransform EG_BuyEnergyRectTransform
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
					if( this.m_EG_BuyEnergyRectTransform == null )
					{
						this.m_EG_BuyEnergyRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_BuyEnergy");
					}
					return this.m_EG_BuyEnergyRectTransform;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_BuyEnergy");
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
						this.m_EButton_BuyEnergyButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EG_BuyEnergy/EButton_BuyEnergy");
					}
					return this.m_EButton_BuyEnergyButton;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EG_BuyEnergy/EButton_BuyEnergy");
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
						this.m_EButton_BuyEnergyImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_BuyEnergy/EButton_BuyEnergy");
					}
					return this.m_EButton_BuyEnergyImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_BuyEnergy/EButton_BuyEnergy");
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
						this.m_ELabel_BuyEnergyTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_BuyEnergy/EButton_BuyEnergy/ELabel_BuyEnergy");
					}
					return this.m_ELabel_BuyEnergyTextMeshProUGUI;
				}
				else
				{
					return UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_BuyEnergy/EButton_BuyEnergy/ELabel_BuyEnergy");
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
						this.m_ELabel_BuyEnergyUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "EG_BuyEnergy/EButton_BuyEnergy/ELabel_BuyEnergy");
					}
					return this.m_ELabel_BuyEnergyUITextLocalizeMonoView;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "EG_BuyEnergy/EButton_BuyEnergy/ELabel_BuyEnergy");
				}
			}
		}

		public UnityEngine.RectTransform EGBuyEnergyPressRectTransform
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
					if( this.m_EGBuyEnergyPressRectTransform == null )
					{
						this.m_EGBuyEnergyPressRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_BuyEnergy/EGBuyEnergyPress");
					}
					return this.m_EGBuyEnergyPressRectTransform;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_BuyEnergy/EGBuyEnergyPress");
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

		public UnityEngine.UI.Image EImage_ShowDetailBg1Image
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
					if( this.m_EImage_ShowDetailBg1Image == null )
					{
						this.m_EImage_ShowDetailBg1Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EButton_ShowDetail/EImage_ShowDetailBg1");
					}
					return this.m_EImage_ShowDetailBg1Image;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EButton_ShowDetail/EImage_ShowDetailBg1");
				}
			}
		}

		public UnityEngine.UI.Image EImage_ShowDetailBg2Image
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
					if( this.m_EImage_ShowDetailBg2Image == null )
					{
						this.m_EImage_ShowDetailBg2Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EButton_ShowDetail/EImage_ShowDetailBg2");
					}
					return this.m_EImage_ShowDetailBg2Image;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EButton_ShowDetail/EImage_ShowDetailBg2");
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

		public UnityEngine.UI.Image EImage_ArrowImage
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
					if( this.m_EImage_ArrowImage == null )
					{
						this.m_EImage_ArrowImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EButton_ShowDetail/EImage_Arrow");
					}
					return this.m_EImage_ArrowImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EButton_ShowDetail/EImage_Arrow");
				}
			}
		}

		public UnityEngine.UI.Image EImage_Arrow1Image
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
					if( this.m_EImage_Arrow1Image == null )
					{
						this.m_EImage_Arrow1Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EButton_ShowDetail/EImage_Arrow1");
					}
					return this.m_EImage_Arrow1Image;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EButton_ShowDetail/EImage_Arrow1");
				}
			}
		}

		public UnityEngine.UI.Image EImage_Arrow2Image
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
					if( this.m_EImage_Arrow2Image == null )
					{
						this.m_EImage_Arrow2Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EButton_ShowDetail/EImage_Arrow2");
					}
					return this.m_EImage_Arrow2Image;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EButton_ShowDetail/EImage_Arrow2");
				}
			}
		}

		public UnityEngine.RectTransform EG_TimesRectTransform
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
					if( this.m_EG_TimesRectTransform == null )
					{
						this.m_EG_TimesRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "GameObject/EG_Times");
					}
					return this.m_EG_TimesRectTransform;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "GameObject/EG_Times");
				}
			}
		}

		public UnityEngine.UI.Image EImage_TimeBg1Image
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
					if( this.m_EImage_TimeBg1Image == null )
					{
						this.m_EImage_TimeBg1Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "GameObject/EG_Times/EImage_TimeBg1");
					}
					return this.m_EImage_TimeBg1Image;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "GameObject/EG_Times/EImage_TimeBg1");
				}
			}
		}

		public UnityEngine.UI.Image EImage_TimeBg2Image
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
					if( this.m_EImage_TimeBg2Image == null )
					{
						this.m_EImage_TimeBg2Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "GameObject/EG_Times/EImage_TimeBg2");
					}
					return this.m_EImage_TimeBg2Image;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "GameObject/EG_Times/EImage_TimeBg2");
				}
			}
		}

		public UnityEngine.UI.Image EImage_TimeBg3Image
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
					if( this.m_EImage_TimeBg3Image == null )
					{
						this.m_EImage_TimeBg3Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "GameObject/EG_Times/EImage_TimeBg3");
					}
					return this.m_EImage_TimeBg3Image;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "GameObject/EG_Times/EImage_TimeBg3");
				}
			}
		}

		public TMPro.TextMeshProUGUI ELabel_TimesTextMeshProUGUI
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
					if( this.m_ELabel_TimesTextMeshProUGUI == null )
					{
						this.m_ELabel_TimesTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "GameObject/EG_Times/ELabel_Times");
					}
					return this.m_ELabel_TimesTextMeshProUGUI;
				}
				else
				{
					return UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "GameObject/EG_Times/ELabel_Times");
				}
			}
		}

		public TMPro.TextMeshProUGUI ELabel_AddTimesTipsTextMeshProUGUI
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
					if( this.m_ELabel_AddTimesTipsTextMeshProUGUI == null )
					{
						this.m_ELabel_AddTimesTipsTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "GameObject/ELabel_AddTimesTips");
					}
					return this.m_ELabel_AddTimesTipsTextMeshProUGUI;
				}
				else
				{
					return UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "GameObject/ELabel_AddTimesTips");
				}
			}
		}

		public void DestroyWidget()
		{
			this.m_EG_RootRectTransform = null;
			this.m_EG_IconRectTransform = null;
			this.m_EG_IconImage = null;
			this.m_EImage_IconImage = null;
			this.m_EImage_EnergyCDImage = null;
			this.m_EImage_FrameImage = null;
			this.m_EImage_CDImage = null;
			this.m_EButton_SelectButton = null;
			this.m_EButton_SelectImage = null;
			this.m_EG_BuyEnergyRectTransform = null;
			this.m_EButton_BuyEnergyButton = null;
			this.m_EButton_BuyEnergyImage = null;
			this.m_ELabel_BuyEnergyTextMeshProUGUI = null;
			this.m_ELabel_BuyEnergyUITextLocalizeMonoView = null;
			this.m_EGBuyEnergyPressRectTransform = null;
			this.m_EButton_ShowDetailButton = null;
			this.m_EImage_ShowDetailBg1Image = null;
			this.m_EImage_ShowDetailBg2Image = null;
			this.m_ELabel_ShowDetailTextMeshProUGUI = null;
			this.m_ELabel_ShowDetailUITextLocalizeMonoView = null;
			this.m_EImage_ArrowImage = null;
			this.m_EImage_Arrow1Image = null;
			this.m_EImage_Arrow2Image = null;
			this.m_EG_TimesRectTransform = null;
			this.m_EImage_TimeBg1Image = null;
			this.m_EImage_TimeBg2Image = null;
			this.m_EImage_TimeBg3Image = null;
			this.m_ELabel_TimesTextMeshProUGUI = null;
			this.m_ELabel_AddTimesTipsTextMeshProUGUI = null;
			this.uiTransform = null;
			this.DataId = 0;
		}

		private UnityEngine.RectTransform m_EG_RootRectTransform = null;
		private UnityEngine.RectTransform m_EG_IconRectTransform = null;
		private UnityEngine.UI.Image m_EG_IconImage = null;
		private UnityEngine.UI.Image m_EImage_IconImage = null;
		private UnityEngine.UI.Image m_EImage_EnergyCDImage = null;
		private UnityEngine.UI.Image m_EImage_FrameImage = null;
		private UnityEngine.UI.Image m_EImage_CDImage = null;
		private UnityEngine.UI.Button m_EButton_SelectButton = null;
		private UnityEngine.UI.Image m_EButton_SelectImage = null;
		private UnityEngine.RectTransform m_EG_BuyEnergyRectTransform = null;
		private UnityEngine.UI.Button m_EButton_BuyEnergyButton = null;
		private UnityEngine.UI.Image m_EButton_BuyEnergyImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_BuyEnergyTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_ELabel_BuyEnergyUITextLocalizeMonoView = null;
		private UnityEngine.RectTransform m_EGBuyEnergyPressRectTransform = null;
		private UnityEngine.UI.Button m_EButton_ShowDetailButton = null;
		private UnityEngine.UI.Image m_EImage_ShowDetailBg1Image = null;
		private UnityEngine.UI.Image m_EImage_ShowDetailBg2Image = null;
		private TMPro.TextMeshProUGUI m_ELabel_ShowDetailTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_ELabel_ShowDetailUITextLocalizeMonoView = null;
		private UnityEngine.UI.Image m_EImage_ArrowImage = null;
		private UnityEngine.UI.Image m_EImage_Arrow1Image = null;
		private UnityEngine.UI.Image m_EImage_Arrow2Image = null;
		private UnityEngine.RectTransform m_EG_TimesRectTransform = null;
		private UnityEngine.UI.Image m_EImage_TimeBg1Image = null;
		private UnityEngine.UI.Image m_EImage_TimeBg2Image = null;
		private UnityEngine.UI.Image m_EImage_TimeBg3Image = null;
		private TMPro.TextMeshProUGUI m_ELabel_TimesTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_ELabel_AddTimesTipsTextMeshProUGUI = null;
		public Transform uiTransform = null;
	}
}
