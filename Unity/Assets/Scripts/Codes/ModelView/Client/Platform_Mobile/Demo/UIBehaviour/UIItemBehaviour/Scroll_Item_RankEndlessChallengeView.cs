
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[EnableMethod]
	public partial class Scroll_Item_RankEndlessChallenge : Entity, IAwake, IDestroy, IUIScrollItem, IUILogic
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
			if(this.m_es_avatarshow != null)
			{
				this.m_es_avatarshow?.Dispose();
				this.m_es_avatarshow = null;
			}
			return this;
		}

		public UnityEngine.UI.Image EImage_MyBGImage
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
					if( this.m_EImage_MyBGImage == null )
					{
						this.m_EImage_MyBGImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "BG/EImage_MyBG");
					}
					return this.m_EImage_MyBGImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "BG/EImage_MyBG");
				}
			}
		}

		public UnityEngine.UI.Image EImage_NO1Image
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
					if( this.m_EImage_NO1Image == null )
					{
						this.m_EImage_NO1Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "BG/EImage_NO1");
					}
					return this.m_EImage_NO1Image;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "BG/EImage_NO1");
				}
			}
		}

		public UnityEngine.UI.Image EImage_NO2Image
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
					if( this.m_EImage_NO2Image == null )
					{
						this.m_EImage_NO2Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "BG/EImage_NO2");
					}
					return this.m_EImage_NO2Image;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "BG/EImage_NO2");
				}
			}
		}

		public UnityEngine.UI.Image EImage_NO3Image
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
					if( this.m_EImage_NO3Image == null )
					{
						this.m_EImage_NO3Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "BG/EImage_NO3");
					}
					return this.m_EImage_NO3Image;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "BG/EImage_NO3");
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

		public UnityEngine.UI.Image EImage_KillNumsBgImage
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
					if( this.m_EImage_KillNumsBgImage == null )
					{
						this.m_EImage_KillNumsBgImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EImage_KillNumsBg");
					}
					return this.m_EImage_KillNumsBgImage;
				}
				else
				{
					return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EImage_KillNumsBg");
				}
			}
		}

		public TMPro.TextMeshProUGUI ELabel_KillNumsTextMeshProUGUI
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
					if( this.m_ELabel_KillNumsTextMeshProUGUI == null )
					{
						this.m_ELabel_KillNumsTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EImage_KillNumsBg/ELabel_KillNums");
					}
					return this.m_ELabel_KillNumsTextMeshProUGUI;
				}
				else
				{
					return UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EImage_KillNumsBg/ELabel_KillNums");
				}
			}
		}

		public void DestroyWidget()
		{
			this.m_EImage_MyBGImage = null;
			this.m_EImage_NO1Image = null;
			this.m_EImage_NO2Image = null;
			this.m_EImage_NO3Image = null;
			this.m_ELabel_RankNumTextMeshProUGUI = null;
			this.m_es_avatarshow?.Dispose();
			this.m_es_avatarshow = null;
			this.m_ELabel_NameTextMeshProUGUI = null;
			this.m_ELabel_WavesTextMeshProUGUI = null;
			this.m_EImage_KillNumsBgImage = null;
			this.m_ELabel_KillNumsTextMeshProUGUI = null;
			this.uiTransform = null;
			this.DataId = 0;
		}

		private UnityEngine.UI.Image m_EImage_MyBGImage = null;
		private UnityEngine.UI.Image m_EImage_NO1Image = null;
		private UnityEngine.UI.Image m_EImage_NO2Image = null;
		private UnityEngine.UI.Image m_EImage_NO3Image = null;
		private TMPro.TextMeshProUGUI m_ELabel_RankNumTextMeshProUGUI = null;
		private ES_AvatarShow m_es_avatarshow = null;
		private TMPro.TextMeshProUGUI m_ELabel_NameTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_ELabel_WavesTextMeshProUGUI = null;
		private UnityEngine.UI.Image m_EImage_KillNumsBgImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_KillNumsTextMeshProUGUI = null;
		public Transform uiTransform = null;
	}
}
