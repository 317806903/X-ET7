
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[EnableMethod]
	public class Scroll_Item_Tower : Entity, IAwake, IDestroy, IUIScrollItem, IUILogic
	{
		public long DataId {get;set;}
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_Item_Tower BindTrans(Transform trans)
		{
			this.uiTransform = trans;
			return this;
		}

		public UnityEngine.UI.Image E_BoxImage
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
					if( this.m_E_BoxImage == null )
					{
						this.m_E_BoxImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Box");
					}
					return this.m_E_BoxImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Box");
				}
			}
		}

		public UnityEngine.UI.Image EImage_LowImage
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
					if( this.m_EImage_LowImage == null )
					{
						this.m_EImage_LowImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Box/EImage_Low");
					}
					return this.m_EImage_LowImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Box/EImage_Low");
				}
			}
		}

		public UnityEngine.UI.Image EImage_MiddleImage
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
					if( this.m_EImage_MiddleImage == null )
					{
						this.m_EImage_MiddleImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Box/EImage_Middle");
					}
					return this.m_EImage_MiddleImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Box/EImage_Middle");
				}
			}
		}

		public UnityEngine.UI.Image EImage_HighImage
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
					if( this.m_EImage_HighImage == null )
					{
						this.m_EImage_HighImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Box/EImage_High");
					}
					return this.m_EImage_HighImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Box/EImage_High");
				}
			}
		}

		public UnityEngine.UI.Image EButton_TowerIcoImage
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
					if( this.m_EButton_TowerIcoImage == null )
					{
						this.m_EButton_TowerIcoImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EButton_TowerIco");
					}
					return this.m_EButton_TowerIcoImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EButton_TowerIco");
				}
			}
		}

		public UnityEngine.RectTransform EG_IconStarRectTransform
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
					if( this.m_EG_IconStarRectTransform == null )
					{
						this.m_EG_IconStarRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_IconStar");
					}
					return this.m_EG_IconStarRectTransform;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_IconStar");
				}
			}
		}

		public UnityEngine.UI.Image E_IconStar1Image
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
					if( this.m_E_IconStar1Image == null )
					{
						this.m_E_IconStar1Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_IconStar/E_IconStar1");
					}
					return this.m_E_IconStar1Image;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_IconStar/E_IconStar1");
				}
			}
		}

		public UnityEngine.UI.Image E_IconStar2Image
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
					if( this.m_E_IconStar2Image == null )
					{
						this.m_E_IconStar2Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_IconStar/E_IconStar2");
					}
					return this.m_E_IconStar2Image;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_IconStar/E_IconStar2");
				}
			}
		}

		public UnityEngine.UI.Image E_IconStar3Image
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
					if( this.m_E_IconStar3Image == null )
					{
						this.m_E_IconStar3Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_IconStar/E_IconStar3");
					}
					return this.m_E_IconStar3Image;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_IconStar/E_IconStar3");
				}
			}
		}

		public TMPro.TextMeshProUGUI ELabel_NumTextMeshProUGUI
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
					if( this.m_ELabel_NumTextMeshProUGUI == null )
					{
						this.m_ELabel_NumTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "ELabel_Num");
					}
					return this.m_ELabel_NumTextMeshProUGUI;
				}
				else
				{
					return UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "ELabel_Num");
				}
			}
		}

		public TMPro.TextMeshProUGUI ELabel_NameTextMeshProUGUI
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
					if( this.m_ELabel_NameTextMeshProUGUI == null )
					{
						this.m_ELabel_NameTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "ELabel_Name");
					}
					return this.m_ELabel_NameTextMeshProUGUI;
				}
				else
				{
					return UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "ELabel_Name");
				}
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
				if (this.isCacheNode)
				{
					if( this.m_EImage_Label1Image == null )
					{
						this.m_EImage_Label1Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EImage_Label1");
					}
					return this.m_EImage_Label1Image;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EImage_Label1");
				}
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
				if (this.isCacheNode)
				{
					if( this.m_ELabel_Label1TextMeshProUGUI == null )
					{
						this.m_ELabel_Label1TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EImage_Label1/ELabel_Label1");
					}
					return this.m_ELabel_Label1TextMeshProUGUI;
				}
				else
				{
					return UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EImage_Label1/ELabel_Label1");
				}
			}
		}

		public UnityEngine.UI.Image EImage_Label2Image
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
					if( this.m_EImage_Label2Image == null )
					{
						this.m_EImage_Label2Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EImage_Label2");
					}
					return this.m_EImage_Label2Image;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EImage_Label2");
				}
			}
		}

		public TMPro.TextMeshProUGUI ELabel_Label2TextMeshProUGUI
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
					if( this.m_ELabel_Label2TextMeshProUGUI == null )
					{
						this.m_ELabel_Label2TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EImage_Label2/ELabel_Label2");
					}
					return this.m_ELabel_Label2TextMeshProUGUI;
				}
				else
				{
					return UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EImage_Label2/ELabel_Label2");
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

		public void DestroyWidget()
		{
			this.m_E_BoxImage = null;
			this.m_EImage_LowImage = null;
			this.m_EImage_MiddleImage = null;
			this.m_EImage_HighImage = null;
			this.m_EButton_TowerIcoImage = null;
			this.m_EG_IconStarRectTransform = null;
			this.m_E_IconStar1Image = null;
			this.m_E_IconStar2Image = null;
			this.m_E_IconStar3Image = null;
			this.m_ELabel_NumTextMeshProUGUI = null;
			this.m_ELabel_NameTextMeshProUGUI = null;
			this.m_EImage_Label1Image = null;
			this.m_ELabel_Label1TextMeshProUGUI = null;
			this.m_EImage_Label2Image = null;
			this.m_ELabel_Label2TextMeshProUGUI = null;
			this.m_EButton_SelectButton = null;
			this.m_EButton_SelectImage = null;
			this.uiTransform = null;
			this.DataId = 0;
		}

		private UnityEngine.UI.Image m_E_BoxImage = null;
		private UnityEngine.UI.Image m_EImage_LowImage = null;
		private UnityEngine.UI.Image m_EImage_MiddleImage = null;
		private UnityEngine.UI.Image m_EImage_HighImage = null;
		private UnityEngine.UI.Image m_EButton_TowerIcoImage = null;
		private UnityEngine.RectTransform m_EG_IconStarRectTransform = null;
		private UnityEngine.UI.Image m_E_IconStar1Image = null;
		private UnityEngine.UI.Image m_E_IconStar2Image = null;
		private UnityEngine.UI.Image m_E_IconStar3Image = null;
		private TMPro.TextMeshProUGUI m_ELabel_NumTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_ELabel_NameTextMeshProUGUI = null;
		private UnityEngine.UI.Image m_EImage_Label1Image = null;
		private TMPro.TextMeshProUGUI m_ELabel_Label1TextMeshProUGUI = null;
		private UnityEngine.UI.Image m_EImage_Label2Image = null;
		private TMPro.TextMeshProUGUI m_ELabel_Label2TextMeshProUGUI = null;
		private UnityEngine.UI.Button m_EButton_SelectButton = null;
		private UnityEngine.UI.Image m_EButton_SelectImage = null;
		public Transform uiTransform = null;
	}
}
