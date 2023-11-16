using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [FriendOf(typeof (DlgRankEndlessChallenge))]
    public static class DlgRankEndlessChallengeSystem
    {
        public static void RegisterUIEvent(this DlgRankEndlessChallenge self)
        {
            self.View.E_QuitRankButton.AddListener(self.QuitRank);
            self.View.E_BG_ClickButton.AddListener(self.OnBgClick);

            self.View.ELoopScrollList_RankLoopVerticalScrollRect.prefabSource.prefabName = "Item_RankEndlessChallenge";
            self.View.ELoopScrollList_RankLoopVerticalScrollRect.prefabSource.poolSize = 8;
            self.View.ELoopScrollList_RankLoopVerticalScrollRect.AddItemRefreshListener(((transform, i) =>
                    self.AddRankItemRefreshListener(transform, i).Coroutine()));
        }

        public static void ShowWindow(this DlgRankEndlessChallenge self, ShowWindowData contextData = null)
        {
            self.ShowBg().Coroutine();
            self._ShowWindow().Coroutine();
        }

        public static async ETTask _ShowWindow(this DlgRankEndlessChallenge self)
        {
            await self.ShowPersonalInfo();
            await self.ShowRankScrollItem();
        }

        public static async ETTask ShowBg(this DlgRankEndlessChallenge self)
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


        public static void QuitRank(this DlgRankEndlessChallenge self)
        {
            UIAudioManagerHelper.PlayUIAudioBack(self.DomainScene());
            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgRankEndlessChallenge>();
        }


        public static async ETTask ShowRankScrollItem(this DlgRankEndlessChallenge self)
        {
            RankShowComponent rankShowComponent = await ET.Client.RankHelper.GetRankShow(self.DomainScene(), RankType.EndlessChallenge, false);
            var list = rankShowComponent.GetRankList();
            int count = list.Count;
            self.AddUIScrollItems(ref self.ScrollItemRankEndlessChallenges, count);
            self.View.ELoopScrollList_RankLoopVerticalScrollRect.SetVisible(true, count);
        }

        public static async ETTask AddRankItemRefreshListener(this DlgRankEndlessChallenge self, Transform transform, int index)
        {
            Scroll_Item_RankEndlessChallenge itemRank = self.ScrollItemRankEndlessChallenges[index].BindTrans(transform);

            RankShowComponent rankShowComponent = await ET.Client.RankHelper.GetRankShow(self.DomainScene(), RankType.EndlessChallenge, false);
            var list = rankShowComponent.GetRankList();
            int rank = index + 1;
            RankShowItemComponent rankShowItemComponent = list[rank];
            //Log.Debug($"rank:{rank} playerId[{rankShowItemComponent.playerId}] score[{rankShowItemComponent.score}]");

            long playerId = rankShowItemComponent.playerId;
            PlayerBaseInfoComponent playerBaseInfoComponent =
                    await PlayerCacheHelper.GetOtherPlayerBaseInfo(self.DomainScene(), playerId);
            List<string> avatarIconList = ET.Client.PlayerHelper.GetAvatarIconList();
            await itemRank.EImage_AvatorImage.SetImageByPath(avatarIconList[playerBaseInfoComponent.IconIndex]);
            itemRank.ELabel_NameTextMeshProUGUI.text = playerBaseInfoComponent.PlayerName;
            if (rank <= 3)
            {
                itemRank.EImage_RankBGImage.gameObject.SetActive(true);
            }
            else
            {
                itemRank.EImage_RankBGImage.gameObject.SetActive(false);
            }

            itemRank.ELabel_RankNumTextMeshProUGUI.text = rank.ToString();
            itemRank.ELabel_WavesTextMeshProUGUI.text = rankShowItemComponent.score.ToString();
        }

        public static async ETTask ShowPersonalInfo(this DlgRankEndlessChallenge self)
        {
            await self.View.EImage_AvatarImage.SetMyIcon(self.DomainScene());
            PlayerBaseInfoComponent playerBaseInfoComponent =
                    await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(self.DomainScene());
            self.View.ELabel_NameTextMeshProUGUI.text = playerBaseInfoComponent.PlayerName;

            RankShowComponent rankShowComponent = await ET.Client.RankHelper.GetRankShow(self.DomainScene(), RankType.EndlessChallenge, false);
            var list = rankShowComponent.GetRankList();
            foreach (var item in list)
            {
                int rank = item.Key;
                RankShowItemComponent rankShowItemComponent = item.Value;
                if (rankShowItemComponent.playerId == playerBaseInfoComponent.Id)
                {
                    self.View.ELabel_RankNumTextMeshProUGUI.text = rank.ToString();
                    long waveIndex = rankShowItemComponent.score;
                    string text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_BattleEnd_ChallengeEnds1", waveIndex);
                    self.View.ELabel_ChanllengeTextMeshProUGUI.text = text;
                }
            }
        }

        public static void OnBgClick(this DlgRankEndlessChallenge self)
        {
            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgRankEndlessChallenge>();
        }
    }
}