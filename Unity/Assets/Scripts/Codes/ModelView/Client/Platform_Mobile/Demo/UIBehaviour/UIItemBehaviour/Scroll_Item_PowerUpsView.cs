
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[EnableMethod]
	public partial class Scroll_Item_PowerUps : Entity, IAwake, IDestroy, IUIScrollItem, IUILogic
	{
		public long DataId {get;set;}
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_Item_PowerUps BindTrans(Transform trans)
		{
			this.uiTransform = trans;
			return this;
		}

		public UnityEngine.UI.Image EMainIconImage
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
					if( this.m_EMainIconImage == null )
					{
						this.m_EMainIconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "TopIcon/EMainIcon");
					}
					return this.m_EMainIconImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "TopIcon/EMainIcon");
				}
			}
		}

		public UnityEngine.UI.Image EOutlineColorImage
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
					if( this.m_EOutlineColorImage == null )
					{
						this.m_EOutlineColorImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "TopIcon/OutLine/EOutlineColor");
					}
					return this.m_EOutlineColorImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "TopIcon/OutLine/EOutlineColor");
				}
			}
		}

		public UnityEngine.UI.Image ELevelline2Image
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
					if( this.m_ELevelline2Image == null )
					{
						this.m_ELevelline2Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "TopIcon/OutLine/ELevelline2");
					}
					return this.m_ELevelline2Image;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "TopIcon/OutLine/ELevelline2");
				}
			}
		}

		public UnityEngine.UI.Image ELevelline3Image
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
					if( this.m_ELevelline3Image == null )
					{
						this.m_ELevelline3Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "TopIcon/OutLine/ELevelline3");
					}
					return this.m_ELevelline3Image;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "TopIcon/OutLine/ELevelline3");
				}
			}
		}

		public UnityEngine.UI.Image ELevelline4Image
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
					if( this.m_ELevelline4Image == null )
					{
						this.m_ELevelline4Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "TopIcon/OutLine/ELevelline4");
					}
					return this.m_ELevelline4Image;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "TopIcon/OutLine/ELevelline4");
				}
			}
		}

		public UnityEngine.UI.Image EImgSelectIconImage
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
					if( this.m_EImgSelectIconImage == null )
					{
						this.m_EImgSelectIconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "TopIcon/EImgSelectIcon");
					}
					return this.m_EImgSelectIconImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "TopIcon/EImgSelectIcon");
				}
			}
		}

		public UnityEngine.UI.Image EIconUpImage
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
					if( this.m_EIconUpImage == null )
					{
						this.m_EIconUpImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EIconUp");
					}
					return this.m_EIconUpImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EIconUp");
				}
			}
		}

		public TMPro.TextMeshProUGUI ETxtItemDesTextMeshProUGUI
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
					if( this.m_ETxtItemDesTextMeshProUGUI == null )
					{
						this.m_ETxtItemDesTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "ETxtItemDes");
					}
					return this.m_ETxtItemDesTextMeshProUGUI;
				}
				else
				{
					return UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "ETxtItemDes");
				}
			}
		}

		public UnityEngine.UI.Button EItemBtnButton
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
					if( this.m_EItemBtnButton == null )
					{
						this.m_EItemBtnButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EItemBtn");
					}
					return this.m_EItemBtnButton;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EItemBtn");
				}
			}
		}

		public UnityEngine.UI.Image EItemBtnImage
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
					if( this.m_EItemBtnImage == null )
					{
						this.m_EItemBtnImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EItemBtn");
					}
					return this.m_EItemBtnImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EItemBtn");
				}
			}
		}

		public void DestroyWidget()
		{
			this.m_EMainIconImage = null;
			this.m_EOutlineColorImage = null;
			this.m_ELevelline2Image = null;
			this.m_ELevelline3Image = null;
			this.m_ELevelline4Image = null;
			this.m_EImgSelectIconImage = null;
			this.m_EIconUpImage = null;
			this.m_ETxtItemDesTextMeshProUGUI = null;
			this.m_EItemBtnButton = null;
			this.m_EItemBtnImage = null;
			this.uiTransform = null;
			this.DataId = 0;
		}

		private UnityEngine.UI.Image m_EMainIconImage = null;
		private UnityEngine.UI.Image m_EOutlineColorImage = null;
		private UnityEngine.UI.Image m_ELevelline2Image = null;
		private UnityEngine.UI.Image m_ELevelline3Image = null;
		private UnityEngine.UI.Image m_ELevelline4Image = null;
		private UnityEngine.UI.Image m_EImgSelectIconImage = null;
		private UnityEngine.UI.Image m_EIconUpImage = null;
		private TMPro.TextMeshProUGUI m_ETxtItemDesTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_EItemBtnButton = null;
		private UnityEngine.UI.Image m_EItemBtnImage = null;
		public Transform uiTransform = null;
	}
}
