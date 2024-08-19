
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[EnableMethod]
	public class Scroll_Item_Room : Entity, IAwake, IDestroy, IUIScrollItem, IUILogic
	{
		public long DataId {get;set;}
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_Item_Room BindTrans(Transform trans)
		{
			this.uiTransform = trans;
			return this;
		}

		public UnityEngine.UI.Text ELabel_ContentText
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
					if( this.m_ELabel_ContentText == null )
					{
						this.m_ELabel_ContentText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject, "ELabel_Content");
					}
					return this.m_ELabel_ContentText;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject, "ELabel_Content");
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

		public UnityEngine.UI.Text ELabel_StatusText
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
					if( this.m_ELabel_StatusText == null )
					{
						this.m_ELabel_StatusText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject, "ELabel_Status");
					}
					return this.m_ELabel_StatusText;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject, "ELabel_Status");
				}
			}
		}

		public UnityEngine.UI.Button EButton_JoinButton
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
					if( this.m_EButton_JoinButton == null )
					{
						this.m_EButton_JoinButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EButton_Join");
					}
					return this.m_EButton_JoinButton;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EButton_Join");
				}
			}
		}

		public UnityEngine.UI.Image EButton_JoinImage
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
					if( this.m_EButton_JoinImage == null )
					{
						this.m_EButton_JoinImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EButton_Join");
					}
					return this.m_EButton_JoinImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EButton_Join");
				}
			}
		}

		public UnityEngine.UI.Text ELabel_JoinText
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
					if( this.m_ELabel_JoinText == null )
					{
						this.m_ELabel_JoinText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject, "EButton_Join/ELabel_Join");
					}
					return this.m_ELabel_JoinText;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject, "EButton_Join/ELabel_Join");
				}
			}
		}

		public UITextLocalizeMonoView ELabel_JoinUITextLocalizeMonoView
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
					if( this.m_ELabel_JoinUITextLocalizeMonoView == null )
					{
						this.m_ELabel_JoinUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "EButton_Join/ELabel_Join");
					}
					return this.m_ELabel_JoinUITextLocalizeMonoView;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "EButton_Join/ELabel_Join");
				}
			}
		}

		public void DestroyWidget()
		{
			this.m_ELabel_ContentText = null;
			this.m_EButton_SelectButton = null;
			this.m_EButton_SelectImage = null;
			this.m_ELabel_StatusText = null;
			this.m_EButton_JoinButton = null;
			this.m_EButton_JoinImage = null;
			this.m_ELabel_JoinText = null;
			this.m_ELabel_JoinUITextLocalizeMonoView = null;
			this.uiTransform = null;
			this.DataId = 0;
		}

		private UnityEngine.UI.Text m_ELabel_ContentText = null;
		private UnityEngine.UI.Button m_EButton_SelectButton = null;
		private UnityEngine.UI.Image m_EButton_SelectImage = null;
		private UnityEngine.UI.Text m_ELabel_StatusText = null;
		private UnityEngine.UI.Button m_EButton_JoinButton = null;
		private UnityEngine.UI.Image m_EButton_JoinImage = null;
		private UnityEngine.UI.Text m_ELabel_JoinText = null;
		private UITextLocalizeMonoView m_ELabel_JoinUITextLocalizeMonoView = null;
		public Transform uiTransform = null;
	}
}
