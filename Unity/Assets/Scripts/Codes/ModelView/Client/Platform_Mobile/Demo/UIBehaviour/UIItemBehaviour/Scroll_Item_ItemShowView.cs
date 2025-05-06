
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[EnableMethod]
	public partial class Scroll_Item_ItemShow : Entity, IAwake, IDestroy, IUIScrollItem, IUILogic
	{
		public long DataId {get;set;}
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_Item_ItemShow BindTrans(Transform trans)
		{
			this.uiTransform = trans;
			if(this.m_es_commonitem != null)
			{
				this.m_es_commonitem?.Dispose();
				this.m_es_commonitem = null;
			}
			return this;
		}

		public ES_CommonItem ES_CommonItem
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_es_commonitem == null )
				{
					Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject, "ES_CommonItem");
					this.m_es_commonitem = this.AddChild<ES_CommonItem, Transform>(subTrans);
				}
				return this.m_es_commonitem;
			}
		}

		public UnityEngine.RectTransform EG_ExtendShowRectTransform
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
					if( this.m_EG_ExtendShowRectTransform == null )
					{
						this.m_EG_ExtendShowRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_ExtendShow");
					}
					return this.m_EG_ExtendShowRectTransform;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_ExtendShow");
				}
			}
		}

		public UnityEngine.RectTransform EG_CheckRectTransform
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
					if( this.m_EG_CheckRectTransform == null )
					{
						this.m_EG_CheckRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_ExtendShow/EG_Check");
					}
					return this.m_EG_CheckRectTransform;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_ExtendShow/EG_Check");
				}
			}
		}

		public void DestroyWidget()
		{
			this.m_es_commonitem?.Dispose();
			this.m_es_commonitem = null;
			this.m_EG_ExtendShowRectTransform = null;
			this.m_EG_CheckRectTransform = null;
			this.uiTransform = null;
			this.DataId = 0;
		}

		private ES_CommonItem m_es_commonitem = null;
		private UnityEngine.RectTransform m_EG_ExtendShowRectTransform = null;
		private UnityEngine.RectTransform m_EG_CheckRectTransform = null;
		public Transform uiTransform = null;
	}
}
