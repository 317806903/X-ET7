using System.Collections;
using System.Collections.Generic;
using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [FriendOf(typeof (DlgRankEndlessChallenge))]
    public static class DlgRankEndlessChallengeSystem
    {
        public static void RegisterUIEvent(this DlgRankEndlessChallenge self)
        {
            self.View.E_QuitRankButton.AddListenerAsync(self.QuitRank);
            self.View.E_BG_ClickButton.AddListenerAsync(self.OnBgClick);

            self.View.ELoopScrollList_RankLoopVerticalScrollRect.prefabSource.prefabName = "Item_RankEndlessChallenge";
            self.View.ELoopScrollList_RankLoopVerticalScrollRect.prefabSource.poolSize = 15;
            self.View.ELoopScrollList_RankLoopVerticalScrollRect.AddItemRefreshListener(((transform, i) =>
                    self.AddRankItemRefreshListener(transform, i).Coroutine()));
        }

        public static async ETTask ShowWindow(this DlgRankEndlessChallenge self, ShowWindowData contextData = null)
        {
            self.ShowBg();
            self._ShowWindow().Coroutine();
        }

        public static async ETTask _ShowWindow(this DlgRankEndlessChallenge self)
        {
            await self.ShowPersonalInfo();
            await self.ShowRankScrollItem();
            self.View.ES_AvatarShow.ShowMyAvatarIcon().Coroutine();
        }

        public static void ShowBg(this DlgRankEndlessChallenge self)
        {
            bool isARCameraEnable = ET.Client.ARSessionHelper.ChkARCameraEnable(self.DomainScene());
            isARCameraEnable = false;
            if (isARCameraEnable)
            {
                self.View.EG_bgARRectTransform.SetVisible(true);
                self.View.EG_bgRectTransform.SetVisible(false);
            }
            else
            {
                self.View.EG_bgARRectTransform.SetVisible(false);
                self.View.EG_bgRectTransform.SetVisible(true);
            }
        }

        public static async ETTask QuitRank(this DlgRankEndlessChallenge self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(),SoundEffectType.Back);
            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgRankEndlessChallenge>();
            await UIManagerHelper.EnterGameModeUI(self.DomainScene());
        }

        public static async ETTask ShowRankScrollItem(this DlgRankEndlessChallenge self)
        {
            RankShowComponent rankShowComponent = await ET.Client.RankHelper.GetRankShow(self.DomainScene(), RankType.EndlessChallenge, false);
            var list = rankShowComponent.GetRankList();
            int count = list.Count;
            self.View.ELabel_EmptyLeaderbordTextMeshProUGUI.SetVisible(count == 0);
            self.AddUIScrollItems(ref self.ScrollItemRankEndlessChallenges, count);
            self.View.ELoopScrollList_RankLoopVerticalScrollRect.SetVisible(true, count);
        }

        public static async ETTask AddRankItemRefreshListener(this DlgRankEndlessChallenge self, Transform transform, int index)
        {
            Scroll_Item_RankEndlessChallenge itemRank = self.ScrollItemRankEndlessChallenges[index].BindTrans(transform);

            RankShowComponent rankShowComponent = await ET.Client.RankHelper.GetRankShow(self.DomainScene(), RankType.EndlessChallenge, false);
            var list = rankShowComponent.GetRankList();
            RankEndlessChallengeShowItemComponent rankShowItemComponent = (RankEndlessChallengeShowItemComponent)list[index];
            int rank = rankShowItemComponent.rank;
            long playerId = rankShowItemComponent.playerId;
            long wave = rankShowItemComponent.score;
            PlayerBaseInfoComponent playerBaseInfoComponent = await PlayerCacheHelper.GetOtherPlayerBaseInfo(self.DomainScene(), playerId);
            await itemRank.ES_AvatarShow.ShowAvatarIconByPlayerId(playerId);
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
            itemRank.Eimage_MyBGImage.gameObject.SetActive(myBaseInfoComponent.Id == playerBaseInfoComponent.Id);
        }

        public static async ETTask ShowPersonalInfo(this DlgRankEndlessChallenge self)
        {
            await self.View.E_PlayerIcoImage.SetMyIcon(self.DomainScene());
            PlayerBaseInfoComponent playerBaseInfoComponent =
                    await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(self.DomainScene());
            self.View.E_PlayerNameTextMeshProUGUI.text = playerBaseInfoComponent.PlayerName;

            RankShowComponent rankShowComponent = await ET.Client.RankHelper.GetRankShow(self.DomainScene(), RankType.EndlessChallenge, false);
            (int myRank, long score) = rankShowComponent.GetMyRank();
            if (score != -1)
            {
                string text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_BattleEnd_ChallengeEnds1", score);
                self.View.ELabel_ChanllengeTextMeshProUGUI.text = text;
            }

            if (myRank == -1)
            {
                self.View.EImage_LongRankedBGImage.gameObject.SetActive(true);
                if (score == -1)
                {
                    self.View.ELabel_LongRankNumTextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Rank_NoData");
                }
                else
                {
                    self.View.ELabel_LongRankNumTextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Rank_NoRank");
                }
                self.View.EImage_ShortRankedBGImage.gameObject.SetActive(false);
                return;
            }
            if (myRank < 10000)
            {
                self.View.EImage_ShortRankedBGImage.gameObject.SetActive(true);
                self.View.ELabel_ShortRankNumTextMeshProUGUI.text = myRank.ToString();
                self.View.EImage_LongRankedBGImage.gameObject.SetActive(false);
            }
            else
            {
                self.View.EImage_LongRankedBGImage.gameObject.SetActive(true);
                self.View.ELabel_LongRankNumTextMeshProUGUI.text = myRank.ToString();
                self.View.EImage_ShortRankedBGImage.gameObject.SetActive(false);
            }

        }

        public static async ETTask OnBgClick(this DlgRankEndlessChallenge self)
        {
            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgRankEndlessChallenge>();
            await UIManagerHelper.EnterGameModeUI(self.DomainScene());
        }
    }
}