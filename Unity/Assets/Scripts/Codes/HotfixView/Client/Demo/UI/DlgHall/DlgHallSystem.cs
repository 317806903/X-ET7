using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[Invoke(TimerInvokeType.HallTimer)]
	public class DlgHallTimer: ATimer<DlgHall>
	{
		protected override void Run(DlgHall self)
		{
			try
			{
				self.GetRoomList().Coroutine();
			}
			catch (Exception e)
			{
				Log.Error($"move timer error: {self.Id}\n{e}");
			}
		}
	}

	[FriendOf(typeof(DlgHall))]
	public static class DlgHallSystem
	{
		public static void RegisterUIEvent(this DlgHall self)
		{
			self.View.ELoopScrollList_RoomLoopHorizontalScrollRect.prefabSource.prefabName = "Item_Room";
			self.View.ELoopScrollList_RoomLoopHorizontalScrollRect.prefabSource.poolSize = 4;
			self.View.ELoopScrollList_RoomLoopHorizontalScrollRect.AddItemRefreshListener((transform, i) => self.AddTowerItemRefreshListener
			(transform, i));
			self.View.E_CreateRoomButton.AddListenerAsync(self.CreateRoom);
			self.View.E_RefreshRoomListButton.AddListenerAsync(self.RefreshRoomList);
			self.View.E_ReturnLoginButton.AddListenerAsync(self.ReturnBack);
		}

		public static async ETTask ShowWindow(this DlgHall self, ShowWindowData contextData = null)
		{
			self.GetRoomList().Coroutine();

			self.Timer = TimerComponent.Instance.NewRepeatedTimer(5000, TimerInvokeType.HallTimer, self);

		}

		public static void HideWindow(this DlgHall self)
		{
			TimerComponent.Instance?.Remove(ref self.Timer);
		}

		public static async ETTask GetRoomList(this DlgHall self)
		{
			bool needARRoom = self.View.E_ARToggleToggle.isOn;
			bool needNotARRoom = self.View.E_NotARToggleToggle.isOn;

			Scene clientScene = self.ClientScene();
			await RoomHelper.GetRoomListAsync(clientScene, needARRoom, needNotARRoom);
			RoomManagerComponent roomManagerComponent = ET.Client.RoomHelper.GetRoomManager(clientScene);

			self.roomList = roomManagerComponent.GetRoomList(needARRoom, needNotARRoom);

			int count = self.roomList.Count;
			self.AddUIScrollItems(ref self.ScrollItemRooms, count);
			self.View.ELoopScrollList_RoomLoopHorizontalScrollRect.SetVisible(true, count);
		}

		public static void AddTowerItemRefreshListener(this DlgHall self, Transform transform, int index)
		{
			Scroll_Item_Room itemRoom = self.ScrollItemRooms[index].BindTrans(transform);

			RoomComponent roomComponent = self.roomList[index];
			long roomId = roomComponent.Id;
			RoomStatus roomStatus = roomComponent.roomStatus;
			bool isARRoom = roomComponent.IsARRoom();
			if (isARRoom)
			{
				itemRoom.ELabel_ContentText.text = $"AR:{index}:{roomId}";
			}
			else
			{
				itemRoom.ELabel_ContentText.text = $"{index}:{roomId}";
			}

			if (roomStatus == RoomStatus.Idle)
			{
				itemRoom.ELabel_StatusText.SetVisible(false);
				itemRoom.EButton_JoinButton.SetVisible(true);
				itemRoom.EButton_JoinButton.AddListener(() =>
				{
					self.JoinRoom(roomId).Coroutine();
				});
			}
			else
			{
				itemRoom.ELabel_StatusText.SetVisible(true);
				itemRoom.ELabel_StatusText.text = $"状态{roomStatus.ToString()}";
				itemRoom.EButton_JoinButton.SetVisible(false);
			}
		}

		public static async ETTask CreateRoom(this DlgHall self)
		{
			UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);

			RoomType roomType = RoomType.Normal;
			SubRoomType subRoomType = SubRoomType.NormalRoom;
			RoomTypeInfo roomTypeInfo = ET.GamePlayHelper.GetRoomTypeInfo(roomType, subRoomType);
			(bool result, long roomId) = await RoomHelper.CreateRoomAsync(self.ClientScene(), roomTypeInfo);
			if (result)
			{
				UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgHall>();
				await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgRoom>();
			}

		}

		public static async ETTask RefreshRoomList(this DlgHall self)
		{
			UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

			//await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgGameJudgeChoose>();
			await self.GetRoomList();
		}

		public static async ETTask ReturnBack(this DlgHall self)
		{
			UIAudioManagerHelper.PlayUIAudio(self.DomainScene(),SoundEffectType.Back);
			await ET.Client.UIManagerHelper.ExitRoomUI(self.DomainScene());
		}

		public static async ETTask JoinRoom(this DlgHall self, long roomId)
		{
			UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);

			bool result = await RoomHelper.JoinRoomAsync(self.ClientScene(), roomId);
			if (result)
			{
				UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.JoinRoom);
				UIManagerHelper.GetUIComponent(self.DomainScene()).HideAllShownWindow();
				await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgRoom>();
			}
			else
			{
				string txt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Hall_JoinRoomError");
				UIManagerHelper.ShowOnlyConfirm(self.DomainScene(), txt, () =>
				{
				});
			}
		}

	}
}
