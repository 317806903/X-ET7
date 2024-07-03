
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[EnableMethod]
	public class Scroll_Item_Mail_Inbox : Entity, IAwake, IDestroy, IUIScrollItem
	{
		public long DataId {get;set;}
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_Item_Mail_Inbox BindTrans(Transform trans)
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

		public TMPro.TextMeshProUGUI ELabel_NameTextMeshProUGUI
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
					if( this.m_ELabel_NameTextMeshProUGUI == null )
					{
						this.m_ELabel_NameTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "ELabel_Name");
					}
					return this.m_ELabel_NameTextMeshProUGUI;
				}
				else
				{
					return UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "ELabel_Name");
				}
			}
		}

		public TMPro.TextMeshProUGUI ELabelExpireTextMeshProUGUI
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
					if( this.m_ELabelExpireTextMeshProUGUI == null )
					{
						this.m_ELabelExpireTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "ELabelExpire");
					}
					return this.m_ELabelExpireTextMeshProUGUI;
				}
				else
				{
					return UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "ELabelExpire");
				}
			}
		}

		public TMPro.TextMeshProUGUI ELabelDescribeTextMeshProUGUI
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
					if( this.m_ELabelDescribeTextMeshProUGUI == null )
					{
						this.m_ELabelDescribeTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "ELabelDescribe");
					}
					return this.m_ELabelDescribeTextMeshProUGUI;
				}
				else
				{
					return UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "ELabelDescribe");
				}
			}
		}

		public UnityEngine.UI.LoopHorizontalScrollRect ELoopScrollListGiftLoopHorizontalScrollRect
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
					if( this.m_ELoopScrollListGiftLoopHorizontalScrollRect == null )
					{
						this.m_ELoopScrollListGiftLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject, "ELoopScrollListGift");
					}
					return this.m_ELoopScrollListGiftLoopHorizontalScrollRect;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject, "ELoopScrollListGift");
				}
			}
		}

		public UnityEngine.UI.Button EBtnCollectButton
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
					if( this.m_EBtnCollectButton == null )
					{
						this.m_EBtnCollectButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EBtnCollect");
					}
					return this.m_EBtnCollectButton;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EBtnCollect");
				}
			}
		}

		public UnityEngine.UI.Image EBtnCollectImage
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
					if( this.m_EBtnCollectImage == null )
					{
						this.m_EBtnCollectImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EBtnCollect");
					}
					return this.m_EBtnCollectImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EBtnCollect");
				}
			}
		}

		public UnityEngine.UI.Image ELabel_HaveReadImage
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
					if( this.m_ELabel_HaveReadImage == null )
					{
						this.m_ELabel_HaveReadImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "ELabel_HaveRead");
					}
					return this.m_ELabel_HaveReadImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "ELabel_HaveRead");
				}
			}
		}

		public UnityEngine.UI.Button ELabel_HaveGiftButton
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
					if( this.m_ELabel_HaveGiftButton == null )
					{
						this.m_ELabel_HaveGiftButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "ELabel_HaveGift");
					}
					return this.m_ELabel_HaveGiftButton;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "ELabel_HaveGift");
				}
			}
		}

		public UnityEngine.UI.Image ELabel_HaveGiftImage
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
					if( this.m_ELabel_HaveGiftImage == null )
					{
						this.m_ELabel_HaveGiftImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "ELabel_HaveGift");
					}
					return this.m_ELabel_HaveGiftImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "ELabel_HaveGift");
				}
			}
		}

		public UnityEngine.UI.Button ELabel_NohaveGiftButton
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
					if( this.m_ELabel_NohaveGiftButton == null )
					{
						this.m_ELabel_NohaveGiftButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "ELabel_NohaveGift");
					}
					return this.m_ELabel_NohaveGiftButton;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "ELabel_NohaveGift");
				}
			}
		}

		public UnityEngine.UI.Image ELabel_NohaveGiftImage
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
					if( this.m_ELabel_NohaveGiftImage == null )
					{
						this.m_ELabel_NohaveGiftImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "ELabel_NohaveGift");
					}
					return this.m_ELabel_NohaveGiftImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "ELabel_NohaveGift");
				}
			}
		}

		public void DestroyWidget()
		{
			this.m_es_avatarshow?.Dispose();
			this.m_es_avatarshow = null;
			this.m_ELabel_NameTextMeshProUGUI = null;
			this.m_ELabelExpireTextMeshProUGUI = null;
			this.m_ELabelDescribeTextMeshProUGUI = null;
			this.m_ELoopScrollListGiftLoopHorizontalScrollRect = null;
			this.m_EBtnCollectButton = null;
			this.m_EBtnCollectImage = null;
			this.m_ELabel_HaveReadImage = null;
			this.m_ELabel_HaveGiftButton = null;
			this.m_ELabel_HaveGiftImage = null;
			this.m_ELabel_NohaveGiftButton = null;
			this.m_ELabel_NohaveGiftImage = null;
			this.uiTransform = null;
			this.DataId = 0;

            this.ScrollGiftDic = null;
            this.mailInfoComponent = null;
            this.mailStatus = 0;
            this.kvpItemCfgNumList = null;


        }

		private ES_AvatarShow m_es_avatarshow = null;
		private TMPro.TextMeshProUGUI m_ELabel_NameTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_ELabelExpireTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_ELabelDescribeTextMeshProUGUI = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopScrollListGiftLoopHorizontalScrollRect = null;
		private UnityEngine.UI.Button m_EBtnCollectButton = null;
		private UnityEngine.UI.Image m_EBtnCollectImage = null;
		private UnityEngine.UI.Image m_ELabel_HaveReadImage = null;
		private UnityEngine.UI.Button m_ELabel_HaveGiftButton = null;
		private UnityEngine.UI.Image m_ELabel_HaveGiftImage = null;
		private UnityEngine.UI.Button m_ELabel_NohaveGiftButton = null;
		private UnityEngine.UI.Image m_ELabel_NohaveGiftImage = null;
		public Transform uiTransform = null;


        public Dictionary<int, Scroll_Item_Gifts> ScrollGiftDic = new Dictionary<int, Scroll_Item_Gifts>();
        public MailInfoComponent mailInfoComponent = new MailInfoComponent();
        public MailStatus mailStatus;
        public List<KeyValuePair<string, int>> kvpItemCfgNumList = new List<KeyValuePair<string, int>>();
    }
}
