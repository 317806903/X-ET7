using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(DlgHall))]
	public static  class DlgHallSystem
	{
		public static void RegisterUIEvent(this DlgHall self)
		{
			self.View.ELoopScrollList_RoomLoopHorizontalScrollRect.prefabSource.prefabName = "Item_Room";
			self.View.ELoopScrollList_RoomLoopHorizontalScrollRect.prefabSource.poolSize = 4;
			self.View.ELoopScrollList_RoomLoopHorizontalScrollRect.AddItemRefreshListener((transform, i) => self.AddTowerItemRefreshListener
			(transform, i));
			self.View.E_CreateRoomButton.AddListenerAsync(self.CreateRoom);
			self.View.E_RefreshRoomListButton.AddListenerAsync(self.RefreshRoomList);
			self.View.E_ReturnLoginButton.AddListenerAsync(self.ReturnLogin);
		}

		public static void ShowWindow(this DlgHall self, Entity contextData = null)
		{
			self.GetRoomList().Coroutine();
		}

		public static async ETTask GetRoomList(this DlgHall self)
		{
			Scene clientScene = self.ClientScene();
			await RoomHelper.GetRoomListAsync(clientScene);
			
			self.roomList = clientScene.GetComponent<RoomManagerComponent>().GetRoomList();
			
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
			itemRoom.ELabel_ContentText.text = $"RoomId:{index}:{roomId}";
			if (roomStatus == RoomStatus.Idle)
			{
				itemRoom.EButton_JoinButton.AddListener(() =>
				{
					self.JoinRoom(roomId);
				});
			}
			else
			{
				itemRoom.ELabel_JoinText.text = $"状态{roomStatus.ToString()}";
			}
		}

		public static async ETTask CreateRoom(this DlgHall self)
		{
			await RoomHelper.CreateRoomAsync(self.ClientScene(), false);
			self.ClientScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_Hall);
			await self.ClientScene().GetComponent<UIComponent>().ShowWindowAsync(WindowID.WindowID_Room);
		}

		public static async ETTask RefreshRoomList(this DlgHall self)
		{
			await self.GetRoomList();
		}

		public static async ETTask ReturnLogin(this DlgHall self)
		{
			await LoginHelper.LoginOut(self.ClientScene());
			self.ClientScene().GetComponent<UIComponent>().HideAllShownWindow();
			await self.ClientScene().GetComponent<UIComponent>().ShowWindowAsync(WindowID.WindowID_Login);
		}
		
		public static async ETTask JoinRoom(this DlgHall self, long roomId)
		{
			await RoomHelper.JoinRoomAsync(self.ClientScene(), roomId);
			self.ClientScene().GetComponent<UIComponent>().HideAllShownWindow();
			await self.ClientScene().GetComponent<UIComponent>().ShowWindowAsync(WindowID.WindowID_Room);
		}

	}
}
