
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[EnableMethod]
	public class Scroll_Item_RoomMember : Entity, IAwake, IDestroy, IUIScrollItem
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
			return this;
		}

		public UnityEngine.UI.Button EButton_IconButton
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
					if( this.m_EButton_IconButton == null )
					{
						this.m_EButton_IconButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EButton_Icon");
					}
					return this.m_EButton_IconButton;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EButton_Icon");
				}
			}
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

		public TMPro.TextMeshProUGUI ELabel_Content_NameTextMeshProUGUI
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
					if( this.m_ELabel_Content_NameTextMeshProUGUI == null )
					{
						this.m_ELabel_Content_NameTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "ELabel_Content_Name");
					}
					return this.m_ELabel_Content_NameTextMeshProUGUI;
				}
				else
				{
					return UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "ELabel_Content_Name");
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

		public void DestroyWidget()
		{
			this.m_EButton_IconButton = null;
			this.m_EButton_IconImage = null;
			this.m_ELabel_Content_NameTextMeshProUGUI = null;
			this.m_ELabel_Content_LvTextMeshProUGUI = null;
			this.m_ELabel_Content_LeaderImage = null;
			this.m_EG_ReadyRectTransform = null;
			this.m_EButton_OperatorButton = null;
			this.m_EButton_OperatorImage = null;
			this.m_ELabel_OperatorText = null;
			this.m_EImage_TeamImage = null;
			this.uiTransform = null;
			this.DataId = 0;
		}

		private UnityEngine.UI.Button m_EButton_IconButton = null;
		private UnityEngine.UI.Image m_EButton_IconImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_Content_NameTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_ELabel_Content_LvTextMeshProUGUI = null;
		private UnityEngine.UI.Image m_ELabel_Content_LeaderImage = null;
		private UnityEngine.RectTransform m_EG_ReadyRectTransform = null;
		private UnityEngine.UI.Button m_EButton_OperatorButton = null;
		private UnityEngine.UI.Image m_EButton_OperatorImage = null;
		private UnityEngine.UI.Text m_ELabel_OperatorText = null;
		private UnityEngine.UI.Image m_EImage_TeamImage = null;
		public Transform uiTransform = null;
	}
}
