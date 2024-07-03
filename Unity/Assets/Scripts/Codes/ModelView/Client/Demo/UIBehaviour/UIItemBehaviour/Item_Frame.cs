
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[EnableMethod]
	public class Scroll_Item_Frame : Entity, IAwake, IDestroy, IUIScrollItem
	{
		public long DataId {get;set;}
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_Item_Frame BindTrans(Transform trans)
		{
			this.uiTransform = trans;
			return this;
		}

		public UnityEngine.UI.Button EImage_FrameButton
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
					if( this.m_EImage_FrameButton == null )
					{
						this.m_EImage_FrameButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EImage_Frame");
					}
					return this.m_EImage_FrameButton;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EImage_Frame");
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
						this.m_EImage_FrameImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EImage_Frame");
					}
					return this.m_EImage_FrameImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EImage_Frame");
				}
			}
		}

		public UnityEngine.UI.Image EIcon_SelectedImage
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
					if( this.m_EIcon_SelectedImage == null )
					{
						this.m_EIcon_SelectedImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EIcon_Selected");
					}
					return this.m_EIcon_SelectedImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EIcon_Selected");
				}
			}
		}

		public void DestroyWidget()
		{
			this.m_EImage_FrameButton = null;
			this.m_EImage_FrameImage = null;
			this.m_EIcon_SelectedImage = null;
			this.uiTransform = null;
			this.DataId = 0;
		}

		private UnityEngine.UI.Button m_EImage_FrameButton = null;
		private UnityEngine.UI.Image m_EImage_FrameImage = null;
		private UnityEngine.UI.Image m_EIcon_SelectedImage = null;
		public Transform uiTransform = null;
	}
}
