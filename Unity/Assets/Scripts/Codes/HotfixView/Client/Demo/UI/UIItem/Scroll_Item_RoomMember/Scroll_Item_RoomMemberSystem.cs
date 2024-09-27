using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using SuperScrollView;
using UnityEngine.Events;
using ET.AbilityConfig;

namespace ET.Client
{
	[FriendOf(typeof(Scroll_Item_RoomMember))]
	public static class Scroll_Item_RoomMemberSystem
	{
		public static void Init(this Scroll_Item_RoomMember self)
		{


		}

        // 默认初始化成员项的UI状态
        public static void InitItemRoom(this Scroll_Item_RoomMember itemRoom)
        {
            itemRoom.ELabel_Content_LvTextMeshProUGUI.gameObject.SetActive(false);
            itemRoom.EImage_TeamImage.SetVisible(false);
            itemRoom.EButton_boxImage.color = new Color(1, 1, 1, 1);
        }

        //如果没有成员
        public static async ETTask SetEmptyState(this Scroll_Item_RoomMember itemRoom, int index)
		{
            itemRoom.ES_AvatarShow.View.E_PlayerNameTextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_RoomMemberStatus_SeatIndex", index);
            itemRoom.ELabel_OperatorText.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_RoomMemberStatus_ChgSeat");
            itemRoom.EButton_OperatorButton.AddListener(() => { itemRoom.ChgRoomSeat(index).Coroutine(); });
            itemRoom.ELabel_Content_LeaderImage.gameObject.SetActive(false);
            itemRoom.ES_AvatarShow.View.E_AvatarIconImage.gameObject.SetActive(false);
            itemRoom.ES_AvatarShow.View.EImage_FrameIconImage.gameObject.SetActive(false);
            itemRoom.EG_ReadyRectTransform.gameObject.SetActive(false);

            await ETTask.CompletedTask;
        }

        //如果有成员
        public static async ETTask SetMemberState(this Scroll_Item_RoomMember itemRoom,RoomComponent roomComponent,long roomMemberId)
        {
           long myPlayerId = PlayerStatusHelper.GetMyPlayerId(itemRoom.DomainScene());
           RoomMember roomMember = roomComponent.GetRoomMember(roomMemberId);

           itemRoom.ES_AvatarShow.View.E_AvatarIconImage.gameObject.SetActive(true);
           itemRoom.ES_AvatarShow.View.EImage_FrameIconImage.gameObject.SetActive(true);


           // 准备的UI
           itemRoom.EG_ReadyRectTransform.gameObject.SetActive(roomMember.isReady);

           // 是否是房主
           if (roomComponent.ownerRoomMemberId == roomMemberId)
           {
               itemRoom.ELabel_Content_LeaderImage.gameObject.SetActive(true);
               itemRoom.ES_AvatarShow.View.E_PlayerNameTextMeshProUGUI.text = $"{roomMemberId}";
           }
           else
           {
               itemRoom.ELabel_Content_LeaderImage.gameObject.SetActive(false);
               itemRoom.ES_AvatarShow.View.E_PlayerNameTextMeshProUGUI.text = $"{roomMemberId}";
           }

           // 判断自身是否是房主，设置踢人按钮
           if (myPlayerId == roomComponent.ownerRoomMemberId)
           {
               itemRoom.ELabel_OperatorText.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_RoomMemberStatus_KickMember");
               itemRoom.EButton_OperatorButton.AddListener(() => { itemRoom.KickOutRoom(roomMemberId).Coroutine(); });
           }
           // 如果自身不是房主，设置查看成员信息按钮
           else
           {

               itemRoom.ELabel_OperatorText.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_RoomMemberStatus_SeeMemberInfo");
               itemRoom.EButton_OperatorButton.AddListener(() => { });
           }

           // 如果自身是该item，隐藏操作按钮并高亮显示
           if (myPlayerId == roomMemberId)
           {
               itemRoom.EButton_OperatorButton.SetVisible(false);
               itemRoom.EButton_boxImage.color = new Color(1, 1, 0, 1);
           }

           // 根据游戏配置决定是否显示不同颜色
           GamePlayBattleLevelCfg gamePlayBattleLevelCfg = GamePlayBattleLevelCfgCategory.Instance.Get(roomComponent.roomTypeInfo.gamePlayBattleLevelCfgId);
           bool isShowTeamChgButton = gamePlayBattleLevelCfg.TeamMode is PlayerTeam;
           if (isShowTeamChgButton)
           {
               itemRoom.EImage_TeamImage.SetVisible(true);

               if (roomMember.roomTeamId == RoomTeamId.Green)
               {
                   itemRoom.EImage_TeamImage.color = Color.green;
               }
               else if (roomMember.roomTeamId == RoomTeamId.Red)
               {
                   itemRoom.EImage_TeamImage.color = Color.red;
               }
               else if (roomMember.roomTeamId == RoomTeamId.Blue)
               {
                   itemRoom.EImage_TeamImage.color = Color.blue;
               }
               else if (roomMember.roomTeamId == RoomTeamId.Yellow)
               {
                   itemRoom.EImage_TeamImage.color = Color.yellow;
               }
           }
            await ETTask.CompletedTask;

        }

        public static async ETTask SetAvatarFrame(this Scroll_Item_RoomMember itemRoom, long roomMemberId)
        {
            // 异步获取其他玩家的基本信息并更新UI
            PlayerBaseInfoComponent otherPlayerBaseInfoComponent = await ET.Client.PlayerCacheHelper.GetOtherPlayerBaseInfo(itemRoom.DomainScene(), roomMemberId);

            //设置其他玩家的名字和头像
            itemRoom.ES_AvatarShow.View.E_PlayerNameTextMeshProUGUI.text = otherPlayerBaseInfoComponent.GetPlayerName();
            await itemRoom.ES_AvatarShow.View.E_AvatarIconImage.SetOtherPlayerIcon(itemRoom.DomainScene(), roomMemberId);

            //设置其他玩家的头像框
            await itemRoom.ES_AvatarShow.View.EImage_FrameIconImage.SetOtherPlayerFrame(itemRoom.DomainScene(), roomMemberId);
        }

        public static async ETTask ChgRoomSeat(this Scroll_Item_RoomMember itemRoom, int newSeat)
        {
            UIAudioManagerHelper.PlayUIAudio(itemRoom.DomainScene(), SoundEffectType.Click);

            await RoomHelper.ChgRoomMemberSeatAsync(itemRoom.ClientScene(), newSeat);
        }

        public static async ETTask KickOutRoom(this Scroll_Item_RoomMember itemRoom, long beKickedPlayerId)
        {
            UIAudioManagerHelper.PlayUIAudio(itemRoom.DomainScene(), SoundEffectType.Click);

            await RoomHelper.KickMemberOutRoomAsync(itemRoom.ClientScene(), beKickedPlayerId);
        }


    }
}
