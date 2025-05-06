
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[EnableMethod]
	public partial class Scroll_Item_GameCfgItem : Entity, IAwake, IDestroy, IUIScrollItem, IUILogic
	{
		public long DataId {get;set;}
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_Item_GameCfgItem BindTrans(Transform trans)
		{
			this.uiTransform = trans;
			return this;
		}

		public UnityEngine.RectTransform EG_UnopenedRectTransform
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
					if( this.m_EG_UnopenedRectTransform == null )
					{
						this.m_EG_UnopenedRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_Unopened");
					}
					return this.m_EG_UnopenedRectTransform;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_Unopened");
				}
			}
		}

		public UnityEngine.UI.Image EG_UnopenedImage
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
					if( this.m_EG_UnopenedImage == null )
					{
						this.m_EG_UnopenedImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_Unopened");
					}
					return this.m_EG_UnopenedImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_Unopened");
				}
			}
		}

		public UnityEngine.RectTransform EG_TurnOnRectTransform
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
					if( this.m_EG_TurnOnRectTransform == null )
					{
						this.m_EG_TurnOnRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_TurnOn");
					}
					return this.m_EG_TurnOnRectTransform;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_TurnOn");
				}
			}
		}

		public UnityEngine.RectTransform EG_InfoRectTransform
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
					if( this.m_EG_InfoRectTransform == null )
					{
						this.m_EG_InfoRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_Info");
					}
					return this.m_EG_InfoRectTransform;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_Info");
				}
			}
		}

		public TMPro.TextMeshProUGUI E_Text_LevelTextMeshProUGUI
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
					if( this.m_E_Text_LevelTextMeshProUGUI == null )
					{
						this.m_E_Text_LevelTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_Info/E_Text_Level");
					}
					return this.m_E_Text_LevelTextMeshProUGUI;
				}
				else
				{
					return UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_Info/E_Text_Level");
				}
			}
		}

		public TMPro.TextMeshProUGUI E_Text_NameTextMeshProUGUI
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
					if( this.m_E_Text_NameTextMeshProUGUI == null )
					{
						this.m_E_Text_NameTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_Info/E_Text_Name");
					}
					return this.m_E_Text_NameTextMeshProUGUI;
				}
				else
				{
					return UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_Info/E_Text_Name");
				}
			}
		}

		public UnityEngine.UI.Image E_CheckBoxImage
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
					if( this.m_E_CheckBoxImage == null )
					{
						this.m_E_CheckBoxImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_CheckBox");
					}
					return this.m_E_CheckBoxImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_CheckBox");
				}
			}
		}

		public UnityEngine.UI.Button E_ClickButton
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
					if( this.m_E_ClickButton == null )
					{
						this.m_E_ClickButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_Click");
					}
					return this.m_E_ClickButton;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_Click");
				}
			}
		}

		public UnityEngine.UI.Image E_ClickImage
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
					if( this.m_E_ClickImage == null )
					{
						this.m_E_ClickImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Click");
					}
					return this.m_E_ClickImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Click");
				}
			}
		}

		public void DestroyWidget()
		{
			this.m_EG_UnopenedRectTransform = null;
			this.m_EG_UnopenedImage = null;
			this.m_EG_TurnOnRectTransform = null;
			this.m_EG_InfoRectTransform = null;
			this.m_E_Text_LevelTextMeshProUGUI = null;
			this.m_E_Text_NameTextMeshProUGUI = null;
			this.m_E_CheckBoxImage = null;
			this.m_E_ClickButton = null;
			this.m_E_ClickImage = null;
			this.uiTransform = null;
			this.DataId = 0;
		}

		private UnityEngine.RectTransform m_EG_UnopenedRectTransform = null;
		private UnityEngine.UI.Image m_EG_UnopenedImage = null;
		private UnityEngine.RectTransform m_EG_TurnOnRectTransform = null;
		private UnityEngine.RectTransform m_EG_InfoRectTransform = null;
		private TMPro.TextMeshProUGUI m_E_Text_LevelTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_E_Text_NameTextMeshProUGUI = null;
		private UnityEngine.UI.Image m_E_CheckBoxImage = null;
		private UnityEngine.UI.Button m_E_ClickButton = null;
		private UnityEngine.UI.Image m_E_ClickImage = null;
		public Transform uiTransform = null;
	}
}
