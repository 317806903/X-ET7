using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

namespace ET.Client
{
	[FriendOf(typeof(EPage_Rank))]
	public static class EPage_RankSystem
	{
		/// <summary>
        /// 注册
        /// </summary>
        /// <param name="self"></param>
		public static void RegisterUIEvent(this EPage_Rank self)
		{
            self.View.ELoopScrollList_RankLoopVerticalScrollRect.prefabSource.prefabName = "Item_RankEndlessChallenge";
            self.View.ELoopScrollList_RankLoopVerticalScrollRect.prefabSource.poolSize = 15;
            self.View.ELoopScrollList_RankLoopVerticalScrollRect.AddItemRefreshListener(((transform, i) =>
                    self.AddRankItemRefreshListener(transform, i).Coroutine()));

        }

        #region 展示
        public static async ETTask ShowPage(this EPage_Rank self, ShowWindowData contextData = null)
		{
            await self.ShowPersonalInfo();
            self.View.uiTransform.SetVisible(true);
            await self.ShowRankScrollItem();
            self.View.ES_AvatarShow.ShowMyAvatarIcon().Coroutine();
        }

        public static async ETTask ShowPersonalInfo(this EPage_Rank self)
        {
            RankShowComponent rankShowComponent = await ET.Client.RankHelper.GetRankShow(self.DomainScene(), RankType.EndlessChallenge, false);
            (int myRank, long score) = rankShowComponent.GetMyRank();
            if (score != -1)
            {
                //string text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_BattleEnd_ChallengeEnds1", score);
                self.View.ELabel_ChanllengeTextMeshProUGUI.text = $"{score}";
            }
            else
            {
                self.View.ELabel_ChanllengeTextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Rank_NoData");
            }

            //没有排名
            if (myRank == -1)
            {
                //if (score == -1)
                //{
                //    self.View.ETxtRankTextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Rank_NoData");
                //}
                //else
                //{
                    self.View.ETxtRankTextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Rank_NoRank");
                //}
                return;
            }
               self.View.ETxtRankTextMeshProUGUI.text = myRank.ToString();
        }

        public static async ETTask ShowRankScrollItem(this EPage_Rank self)
        {
            RankShowComponent rankShowComponent = await ET.Client.RankHelper.GetRankShow(self.DomainScene(), RankType.EndlessChallenge, false);
            var list = rankShowComponent.GetRankList();
            int count = list.Count;
            self.View.ELabel_EmptyLeaderbordTextMeshProUGUI.SetVisible(count == 0);
            self.AddUIScrollItems(ref self.ScrollItemRankEndlessChallenges, count);
            self.View.ELoopScrollList_RankLoopVerticalScrollRect.SetVisible(true, count);
        }
        #endregion

        /// <summary>
        /// 隐藏
        /// </summary>
        /// <param name="self"></param>
        public static void HidePage(this EPage_Rank self)
		{
			self.View.uiTransform.SetVisible(false);

		}

        #region 事件监听函数
        /// <summary>
        /// 排行榜列表
        /// </summary>
        /// <param name="self"></param>
        /// <param name="transform"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static async ETTask AddRankItemRefreshListener(this EPage_Rank self, Transform transform, int index)
        {
            Scroll_Item_RankEndlessChallenge itemRank = self.ScrollItemRankEndlessChallenges[index].BindTrans(transform);

            RankShowComponent rankShowComponent = await ET.Client.RankHelper.GetRankShow(self.DomainScene(), RankType.EndlessChallenge, false);
            var list = rankShowComponent.GetRankList();
            RankEndlessChallengeShowItemComponent rankShowItemComponent = (RankEndlessChallengeShowItemComponent)list[index];
            int rank = rankShowItemComponent.rank;
            long playerId = rankShowItemComponent.playerId;
            long wave = rankShowItemComponent.score;
            PlayerBaseInfoComponent playerBaseInfoComponent = await PlayerCacheHelper.GetOtherPlayerBaseInfo(self.DomainScene(), playerId);
            await itemRank.ES_AvatarShow.ShowAvatarIconByPlayerId(playerId,false);
            itemRank.ELabel_NameTextMeshProUGUI.text = playerBaseInfoComponent.PlayerName;
            itemRank.ELabel_WavesTextMeshProUGUI.text = wave.ToString();
            itemRank.ELabel_KillNumsTextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_BattleEnd_KillNum", rankShowItemComponent.killNum);
            itemRank.EImage_NO1Image.gameObject.SetActive(rank == 1);
            itemRank.EImage_NO2Image.gameObject.SetActive(rank == 2);
            itemRank.EImage_NO3Image.gameObject.SetActive(rank == 3);

            if (rank > 3)
            {
                itemRank.ELabel_RankNumTextMeshProUGUI.text = rank.ToString();
            }
            else
            {
                itemRank.ELabel_RankNumTextMeshProUGUI.text = "";
            }

            long lastPlayerWave = -1, nextPlayerWave = -1;
            if (index - 1 >= 0)
            {
                RankEndlessChallengeShowItemComponent lastRankShowItemComponent = (RankEndlessChallengeShowItemComponent)list[index - 1];
                lastPlayerWave = lastRankShowItemComponent.score;
            }
            if (index + 1 < list.Count)
            {
                RankEndlessChallengeShowItemComponent nextRankShowItemComponent = (RankEndlessChallengeShowItemComponent)list[index + 1];
                nextPlayerWave = nextRankShowItemComponent.score;
            }
            itemRank.EImage_KillNumsBgImage.SetVisible(wave == lastPlayerWave || wave == nextPlayerWave);

            PlayerBaseInfoComponent myBaseInfoComponent =
                    await PlayerCacheHelper.GetMyPlayerBaseInfo(self.DomainScene());
            itemRank.EImage_MyBGImage.gameObject.SetActive(myBaseInfoComponent.Id == playerBaseInfoComponent.Id);
        }
        #endregion

    }
}
