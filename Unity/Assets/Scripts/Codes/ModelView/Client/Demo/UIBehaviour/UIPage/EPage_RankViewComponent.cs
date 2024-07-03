
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[EnableMethod]
	public class EPage_RankViewComponent : Entity, ET.IAwake<UnityEngine.Transform>, IDestroy
	{
		public TMPro.TextMeshProUGUI ELabel_TitleTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_TitleTextMeshProUGUI == null )
				{
					this.m_ELabel_TitleTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "TopInfo/ELabel_Title");
				}
				return this.m_ELabel_TitleTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.LoopVerticalScrollRect ELoopScrollList_RankLoopVerticalScrollRect
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELoopScrollList_RankLoopVerticalScrollRect == null )
				{
					this.m_ELoopScrollList_RankLoopVerticalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopVerticalScrollRect>(this.uiTransform.gameObject, "MidLeaderboard/ELoopScrollList_Rank");
				}
				return this.m_ELoopScrollList_RankLoopVerticalScrollRect;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_EmptyLeaderbordTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_EmptyLeaderbordTextMeshProUGUI == null )
				{
					this.m_ELabel_EmptyLeaderbordTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "MidLeaderboard/ELabel_EmptyLeaderbord");
				}
				return this.m_ELabel_EmptyLeaderbordTextMeshProUGUI;
			}
		}

		public UITextLocalizeMonoView ELabel_EmptyLeaderbordUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_EmptyLeaderbordUITextLocalizeMonoView == null )
				{
					this.m_ELabel_EmptyLeaderbordUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "MidLeaderboard/ELabel_EmptyLeaderbord");
				}
				return this.m_ELabel_EmptyLeaderbordUITextLocalizeMonoView;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_ChanllengeTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_ChanllengeTextMeshProUGUI == null )
				{
					this.m_ELabel_ChanllengeTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "BottomInfo/ELabel_Chanllenge");
				}
				return this.m_ELabel_ChanllengeTextMeshProUGUI;
			}
		}

		public TMPro.TextMeshProUGUI ETxtRankTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtRankTextMeshProUGUI == null )
				{
					this.m_ETxtRankTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "BottomInfo/AvatarAndRank/ETxtRank");
				}
				return this.m_ETxtRankTextMeshProUGUI;
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
					Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject, "BottomInfo/AvatarAndRank/ES_AvatarShow");
					this.m_es_avatarshow = this.AddChild<ES_AvatarShow, Transform>(subTrans);
				}
				return this.m_es_avatarshow;
			}
		}

		public void DestroyWidget()
		{
			this.m_ELabel_TitleTextMeshProUGUI = null;
			this.m_ELoopScrollList_RankLoopVerticalScrollRect = null;
			this.m_ELabel_EmptyLeaderbordTextMeshProUGUI = null;
			this.m_ELabel_EmptyLeaderbordUITextLocalizeMonoView = null;
			this.m_ELabel_ChanllengeTextMeshProUGUI = null;
			this.m_ETxtRankTextMeshProUGUI = null;
			this.m_es_avatarshow?.Dispose();
			this.m_es_avatarshow = null;
			this.uiTransform = null;
		}

		private TMPro.TextMeshProUGUI m_ELabel_TitleTextMeshProUGUI = null;
		private UnityEngine.UI.LoopVerticalScrollRect m_ELoopScrollList_RankLoopVerticalScrollRect = null;
		private TMPro.TextMeshProUGUI m_ELabel_EmptyLeaderbordTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_ELabel_EmptyLeaderbordUITextLocalizeMonoView = null;
		private TMPro.TextMeshProUGUI m_ELabel_ChanllengeTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_ETxtRankTextMeshProUGUI = null;
		private ES_AvatarShow m_es_avatarshow = null;
		public Transform uiTransform = null;
	}
}
