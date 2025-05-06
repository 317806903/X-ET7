
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[EnableMethod]
	public partial class Scroll_Item_Tutorials : Entity, IAwake, IDestroy, IUIScrollItem, IUILogic
	{
		public long DataId {get;set;}
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_Item_Tutorials BindTrans(Transform trans)
		{
			this.uiTransform = trans;
			return this;
		}

		public TMPro.TextMeshProUGUI ELabel_VideoSelectTextMeshProUGUI
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
					if( this.m_ELabel_VideoSelectTextMeshProUGUI == null )
					{
						this.m_ELabel_VideoSelectTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "ELabel_VideoSelect");
					}
					return this.m_ELabel_VideoSelectTextMeshProUGUI;
				}
				else
				{
					return UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "ELabel_VideoSelect");
				}
			}
		}

		public UnityEngine.UI.Image E_SelectedImage
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
					if( this.m_E_SelectedImage == null )
					{
						this.m_E_SelectedImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Selected");
					}
					return this.m_E_SelectedImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Selected");
				}
			}
		}

		public UnityEngine.UI.Button EButton_VideoSelectButton
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
					if( this.m_EButton_VideoSelectButton == null )
					{
						this.m_EButton_VideoSelectButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EButton_VideoSelect");
					}
					return this.m_EButton_VideoSelectButton;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EButton_VideoSelect");
				}
			}
		}

		public UnityEngine.UI.Image EButton_VideoSelectImage
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
					if( this.m_EButton_VideoSelectImage == null )
					{
						this.m_EButton_VideoSelectImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EButton_VideoSelect");
					}
					return this.m_EButton_VideoSelectImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EButton_VideoSelect");
				}
			}
		}

		public void DestroyWidget()
		{
			this.m_ELabel_VideoSelectTextMeshProUGUI = null;
			this.m_E_SelectedImage = null;
			this.m_EButton_VideoSelectButton = null;
			this.m_EButton_VideoSelectImage = null;
			this.uiTransform = null;
			this.DataId = 0;
		}

		private TMPro.TextMeshProUGUI m_ELabel_VideoSelectTextMeshProUGUI = null;
		private UnityEngine.UI.Image m_E_SelectedImage = null;
		private UnityEngine.UI.Button m_EButton_VideoSelectButton = null;
		private UnityEngine.UI.Image m_EButton_VideoSelectImage = null;
		public Transform uiTransform = null;
	}
}
