
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[EnableMethod]
	public class Scroll_Item_Gifts : Entity, IAwake, IDestroy, IUIScrollItem
	{
		public long DataId {get;set;}
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_Item_Gifts BindTrans(Transform trans)
		{
			this.uiTransform = trans;
			return this;
		}

		public UnityEngine.UI.Button EbtnIconButton
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
					if( this.m_EbtnIconButton == null )
					{
						this.m_EbtnIconButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EbtnIcon");
					}
					return this.m_EbtnIconButton;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EbtnIcon");
				}
			}
		}

		public UnityEngine.UI.Image EbtnIconImage
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
					if( this.m_EbtnIconImage == null )
					{
						this.m_EbtnIconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EbtnIcon");
					}
					return this.m_EbtnIconImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EbtnIcon");
				}
			}
		}

		public TMPro.TextMeshProUGUI ETxtNumTextMeshProUGUI
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
					if( this.m_ETxtNumTextMeshProUGUI == null )
					{
						this.m_ETxtNumTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "ETxtNum");
					}
					return this.m_ETxtNumTextMeshProUGUI;
				}
				else
				{
					return UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "ETxtNum");
				}
			}
		}

		public void DestroyWidget()
		{
			this.m_EbtnIconButton = null;
			this.m_EbtnIconImage = null;
			this.m_ETxtNumTextMeshProUGUI = null;
			this.uiTransform = null;
			this.DataId = 0;
		}

		private UnityEngine.UI.Button m_EbtnIconButton = null;
		private UnityEngine.UI.Image m_EbtnIconImage = null;
		private TMPro.TextMeshProUGUI m_ETxtNumTextMeshProUGUI = null;
		public Transform uiTransform = null;
	}
}
