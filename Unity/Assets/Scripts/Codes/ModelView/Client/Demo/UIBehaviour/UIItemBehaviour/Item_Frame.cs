
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[EnableMethod]
	public class Scroll_Item_Frame : Entity, IAwake, IDestroy, IUIScrollItem, IUILogic
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
						this.m_EImage_FrameImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "MainIcon/EImage_Frame");
					}
					return this.m_EImage_FrameImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "MainIcon/EImage_Frame");
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
						this.m_E_RedDotImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "MainIcon/E_RedDot");
					}
					return this.m_E_RedDotImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "MainIcon/E_RedDot");
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
						this.m_EIcon_SelectedImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "MainIcon/EIcon_Selected");
					}
					return this.m_EIcon_SelectedImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "MainIcon/EIcon_Selected");
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
						this.m_EButton_SelectButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "MainIcon/EButton_Select");
					}
					return this.m_EButton_SelectButton;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "MainIcon/EButton_Select");
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
						this.m_EButton_SelectImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "MainIcon/EButton_Select");
					}
					return this.m_EButton_SelectImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "MainIcon/EButton_Select");
				}
			}
		}

		public void DestroyWidget()
		{
			this.m_EImage_FrameImage = null;
			this.m_E_RedDotImage = null;
			this.m_EIcon_SelectedImage = null;
			this.m_EButton_SelectButton = null;
			this.m_EButton_SelectImage = null;
			this.uiTransform = null;
			this.DataId = 0;
		}

		private UnityEngine.UI.Image m_EImage_FrameImage = null;
		private UnityEngine.UI.Image m_E_RedDotImage = null;
		private UnityEngine.UI.Image m_EIcon_SelectedImage = null;
		private UnityEngine.UI.Button m_EButton_SelectButton = null;
		private UnityEngine.UI.Image m_EButton_SelectImage = null;
		public Transform uiTransform = null;
	}
}
