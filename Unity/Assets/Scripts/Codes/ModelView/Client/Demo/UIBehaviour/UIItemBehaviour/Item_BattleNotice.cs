
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[EnableMethod]
	public class Scroll_Item_BattleNotice : Entity, IAwake, IDestroy, IUIScrollItem, IUILogic
	{
		public long DataId {get;set;}
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_Item_BattleNotice BindTrans(Transform trans)
		{
			this.uiTransform = trans;
			return this;
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
						this.m_EButton_IconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EButton_Icon");
					}
					return this.m_EButton_IconImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EButton_Icon");
				}
			}
		}

		public UnityEngine.UI.Button EClickButton
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
					if( this.m_EClickButton == null )
					{
						this.m_EClickButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EClick");
					}
					return this.m_EClickButton;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EClick");
				}
			}
		}

		public UnityEngine.UI.Image EClickImage
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
					if( this.m_EClickImage == null )
					{
						this.m_EClickImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EClick");
					}
					return this.m_EClickImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EClick");
				}
			}
		}

		public void DestroyWidget()
		{
			this.m_EButton_IconImage = null;
			this.m_EClickButton = null;
			this.m_EClickImage = null;
			this.uiTransform = null;
			this.DataId = 0;
		}

		private UnityEngine.UI.Image m_EButton_IconImage = null;
		private UnityEngine.UI.Button m_EClickButton = null;
		private UnityEngine.UI.Image m_EClickImage = null;
		public Transform uiTransform = null;
	}
}
