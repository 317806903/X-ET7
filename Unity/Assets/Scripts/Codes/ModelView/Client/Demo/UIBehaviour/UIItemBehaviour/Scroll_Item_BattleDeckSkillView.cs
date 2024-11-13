
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[EnableMethod]
	public partial class Scroll_Item_BattleDeckSkill : Entity, IAwake, IDestroy, IUIScrollItem, IUILogic
	{
		public long DataId {get;set;}
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_Item_BattleDeckSkill BindTrans(Transform trans)
		{
			this.uiTransform = trans;
			if(this.m_es_commonitem != null)
			{
				this.m_es_commonitem?.Dispose();
				this.m_es_commonitem = null;
			}
			return this;
		}

		public UnityEngine.UI.Image E_NoneImage
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
					if( this.m_E_NoneImage == null )
					{
						this.m_E_NoneImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_None");
					}
					return this.m_E_NoneImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_None");
				}
			}
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

		public UnityEngine.UI.Image E_RedDotImage
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
					if( this.m_E_RedDotImage == null )
					{
						this.m_E_RedDotImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_ExtendShow/E_RedDot");
					}
					return this.m_E_RedDotImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_ExtendShow/E_RedDot");
				}
			}
		}

		public UnityEngine.RectTransform EG_LockRectTransform
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
					if( this.m_EG_LockRectTransform == null )
					{
						this.m_EG_LockRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_ExtendShow/EG_Lock");
					}
					return this.m_EG_LockRectTransform;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_ExtendShow/EG_Lock");
				}
			}
		}

		public void DestroyWidget()
		{
			this.m_E_NoneImage = null;
			this.m_es_commonitem?.Dispose();
			this.m_es_commonitem = null;
			this.m_EG_ExtendShowRectTransform = null;
			this.m_E_RedDotImage = null;
			this.m_EG_LockRectTransform = null;
			this.uiTransform = null;
			this.DataId = 0;
		}

		private UnityEngine.UI.Image m_E_NoneImage = null;
		private ES_CommonItem m_es_commonitem = null;
		private UnityEngine.RectTransform m_EG_ExtendShowRectTransform = null;
		private UnityEngine.UI.Image m_E_RedDotImage = null;
		private UnityEngine.RectTransform m_EG_LockRectTransform = null;
		public Transform uiTransform = null;
	}
}
