﻿
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[EnableMethod]
	public partial class Scroll_Item_RoomMember : Entity, IAwake, IDestroy, IUIScrollItem, IUILogic
	{
		public long DataId {get;set;}
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_Item_RoomMember BindTrans(Transform trans)
		{
			this.uiTransform = trans;
			if(this.m_es_avatarshow != null)
			{
				this.m_es_avatarshow?.Dispose();
				this.m_es_avatarshow = null;
			}
			return this;
		}

		public ES_AvatarShow ES_AvatarShow
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_es_avatarshow == null )
				{
					Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject, "ES_AvatarShow");
					this.m_es_avatarshow = this.AddChild<ES_AvatarShow, Transform>(subTrans);
				}
				return this.m_es_avatarshow;
			}
		}

		public UnityEngine.UI.Image EButton_boxImage
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
					if( this.m_EButton_boxImage == null )
					{
						this.m_EButton_boxImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EButton_box");
					}
					return this.m_EButton_boxImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EButton_box");
				}
			}
		}

		public UnityEngine.UI.Image EButton_shadowImage
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
					if( this.m_EButton_shadowImage == null )
					{
						this.m_EButton_shadowImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EButton_shadow");
					}
					return this.m_EButton_shadowImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EButton_shadow");
				}
			}
		}

		public UnityEngine.UI.Image EImage_TeamImage
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
					if( this.m_EImage_TeamImage == null )
					{
						this.m_EImage_TeamImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EImage_Team");
					}
					return this.m_EImage_TeamImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EImage_Team");
				}
			}
		}

		public TMPro.TextMeshProUGUI ELabel_Content_LvTextMeshProUGUI
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
					if( this.m_ELabel_Content_LvTextMeshProUGUI == null )
					{
						this.m_ELabel_Content_LvTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "ELabel_Content_Lv");
					}
					return this.m_ELabel_Content_LvTextMeshProUGUI;
				}
				else
				{
					return UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "ELabel_Content_Lv");
				}
			}
		}

		public UnityEngine.UI.Image ELabel_Content_LeaderImage
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
					if( this.m_ELabel_Content_LeaderImage == null )
					{
						this.m_ELabel_Content_LeaderImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "ELabel_Content_Leader");
					}
					return this.m_ELabel_Content_LeaderImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "ELabel_Content_Leader");
				}
			}
		}

		public UnityEngine.RectTransform EG_ReadyRectTransform
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
					if( this.m_EG_ReadyRectTransform == null )
					{
						this.m_EG_ReadyRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_Ready");
					}
					return this.m_EG_ReadyRectTransform;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_Ready");
				}
			}
		}

		public UnityEngine.UI.Image EG_ReadyImage
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
					if( this.m_EG_ReadyImage == null )
					{
						this.m_EG_ReadyImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_Ready");
					}
					return this.m_EG_ReadyImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_Ready");
				}
			}
		}

		public UnityEngine.UI.Button EButton_OperatorButton
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
					if( this.m_EButton_OperatorButton == null )
					{
						this.m_EButton_OperatorButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EButton_Operator");
					}
					return this.m_EButton_OperatorButton;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EButton_Operator");
				}
			}
		}

		public UnityEngine.UI.Image EButton_OperatorImage
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
					if( this.m_EButton_OperatorImage == null )
					{
						this.m_EButton_OperatorImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EButton_Operator");
					}
					return this.m_EButton_OperatorImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EButton_Operator");
				}
			}
		}

		public UnityEngine.UI.Text ELabel_OperatorText
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
					if( this.m_ELabel_OperatorText == null )
					{
						this.m_ELabel_OperatorText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject, "EButton_Operator/ELabel_Operator");
					}
					return this.m_ELabel_OperatorText;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject, "EButton_Operator/ELabel_Operator");
				}
			}
		}

		public UnityEngine.RectTransform EG_NoneRectTransform
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
					if( this.m_EG_NoneRectTransform == null )
					{
						this.m_EG_NoneRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_None");
					}
					return this.m_EG_NoneRectTransform;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_None");
				}
			}
		}

		public TMPro.TextMeshProUGUI EG_NoneTextMeshProUGUI
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
					if( this.m_EG_NoneTextMeshProUGUI == null )
					{
						this.m_EG_NoneTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_None");
					}
					return this.m_EG_NoneTextMeshProUGUI;
				}
				else
				{
					return UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_None");
				}
			}
		}

		public UITextLocalizeMonoView EG_NoneUITextLocalizeMonoView
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
					if( this.m_EG_NoneUITextLocalizeMonoView == null )
					{
						this.m_EG_NoneUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "EG_None");
					}
					return this.m_EG_NoneUITextLocalizeMonoView;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "EG_None");
				}
			}
		}

		public void DestroyWidget()
		{
			this.m_es_avatarshow?.Dispose();
			this.m_es_avatarshow = null;
			this.m_EButton_boxImage = null;
			this.m_EButton_shadowImage = null;
			this.m_EImage_TeamImage = null;
			this.m_ELabel_Content_LvTextMeshProUGUI = null;
			this.m_ELabel_Content_LeaderImage = null;
			this.m_EG_ReadyRectTransform = null;
			this.m_EG_ReadyImage = null;
			this.m_EButton_OperatorButton = null;
			this.m_EButton_OperatorImage = null;
			this.m_ELabel_OperatorText = null;
			this.m_EG_NoneRectTransform = null;
			this.m_EG_NoneTextMeshProUGUI = null;
			this.m_EG_NoneUITextLocalizeMonoView = null;
			this.uiTransform = null;
			this.DataId = 0;
		}

		private ES_AvatarShow m_es_avatarshow = null;
		private UnityEngine.UI.Image m_EButton_boxImage = null;
		private UnityEngine.UI.Image m_EButton_shadowImage = null;
		private UnityEngine.UI.Image m_EImage_TeamImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_Content_LvTextMeshProUGUI = null;
		private UnityEngine.UI.Image m_ELabel_Content_LeaderImage = null;
		private UnityEngine.RectTransform m_EG_ReadyRectTransform = null;
		private UnityEngine.UI.Image m_EG_ReadyImage = null;
		private UnityEngine.UI.Button m_EButton_OperatorButton = null;
		private UnityEngine.UI.Image m_EButton_OperatorImage = null;
		private UnityEngine.UI.Text m_ELabel_OperatorText = null;
		private UnityEngine.RectTransform m_EG_NoneRectTransform = null;
		private TMPro.TextMeshProUGUI m_EG_NoneTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_EG_NoneUITextLocalizeMonoView = null;
		public Transform uiTransform = null;
	}
}
