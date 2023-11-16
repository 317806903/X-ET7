
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[EnableMethod]
	public class Scroll_Item_RankEndlessChallenge : Entity, IAwake, IDestroy, IUIScrollItem
	{
		public long DataId {get;set;}
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_Item_RankEndlessChallenge BindTrans(Transform trans)
		{
			this.uiTransform = trans;
			return this;
		}

		public UnityEngine.UI.Image EImage_RankBGImage
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
					if( this.m_EImage_RankBGImage == null )
					{
						this.m_EImage_RankBGImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EImage_RankBG");
					}
					return this.m_EImage_RankBGImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EImage_RankBG");
				}
			}
		}

		public TMPro.TextMeshProUGUI ELabel_RankNumTextMeshProUGUI
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
					if( this.m_ELabel_RankNumTextMeshProUGUI == null )
					{
						this.m_ELabel_RankNumTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "ELabel_RankNum");
					}
					return this.m_ELabel_RankNumTextMeshProUGUI;
				}
				else
				{
					return UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "ELabel_RankNum");
				}
			}
		}

		public UnityEngine.UI.Image EImage_AvatorImage
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
					if( this.m_EImage_AvatorImage == null )
					{
						this.m_EImage_AvatorImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EImage_Avator");
					}
					return this.m_EImage_AvatorImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EImage_Avator");
				}
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

		public TMPro.TextMeshProUGUI ELabel_WavesTextMeshProUGUI
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
					if( this.m_ELabel_WavesTextMeshProUGUI == null )
					{
						this.m_ELabel_WavesTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "ELabel_Waves");
					}
					return this.m_ELabel_WavesTextMeshProUGUI;
				}
				else
				{
					return UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "ELabel_Waves");
				}
			}
		}

		public void DestroyWidget()
		{
			this.m_EImage_RankBGImage = null;
			this.m_ELabel_RankNumTextMeshProUGUI = null;
			this.m_EImage_AvatorImage = null;
			this.m_ELabel_NameTextMeshProUGUI = null;
			this.m_ELabel_WavesTextMeshProUGUI = null;
			this.uiTransform = null;
			this.DataId = 0;
		}

		private UnityEngine.UI.Image m_EImage_RankBGImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_RankNumTextMeshProUGUI = null;
		private UnityEngine.UI.Image m_EImage_AvatorImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_NameTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_ELabel_WavesTextMeshProUGUI = null;
		public Transform uiTransform = null;
	}
}
