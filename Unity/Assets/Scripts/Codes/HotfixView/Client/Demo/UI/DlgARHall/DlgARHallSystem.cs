using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(DlgARHall))]
	public static class DlgARHallSystem
	{

		public static void RegisterUIEvent(this DlgARHall self)
		{

		}

		public static void ShowWindow(this DlgARHall self, ShowWindowData contextData = null)
		{
			if (contextData == null)
			{
				self.roomId = 0;
				self.arSceneId = "";
				self.playerStatusIn = PlayerStatus.Hall;
			}
			else
			{
				DlgARHall_ShowWindowData _DlgARHall_ShowWindowData = contextData as DlgARHall_ShowWindowData;
				if (_DlgARHall_ShowWindowData.playerStatus == PlayerStatus.Hall)
				{
					self.roomId = 0;
					self.arSceneId = "";
					self.playerStatusIn = PlayerStatus.Hall;
				}
				else
				{
					self.roomId = _DlgARHall_ShowWindowData.arRoomId;
					self.arSceneId = "";
					self.playerStatusIn = _DlgARHall_ShowWindowData.playerStatus;
				}
			}
			self.InitArSession().Coroutine();
		}

		public static async ETTask InitArSession(this DlgARHall self)
		{
			await self.TriggerJoinScene();
			ARSessionComponent arSessionComponent = ET.Client.ARSessionHelper.GetARSession(self.DomainScene());
			await arSessionComponent.Init(
				() => { self.OnClose().Coroutine(); },
				() => { self.OnFinishedCallBack().Coroutine(); },
				() => { self.OnCreateRoomCallBack().Coroutine(); },
				() => { self.OnQuitRoomCallBack().Coroutine(); },
				() => { self.OnCreateRoomCallBack().Coroutine(); },
				(sQRCodeInfo) => { self.OnJoinByQRCodeCallBack(sQRCodeInfo).Coroutine(); },
				() => { return self.GetQRCodeInfo(); },
				self.arSceneId
				);

		}

		public static async ETTask ReStart(this DlgARHall self)
		{
			ARSessionComponent arSessionComponent = ET.Client.ARSessionHelper.GetARSession(self.DomainScene());
			await arSessionComponent.ReStart();
		}

		public static async ETTask HideMenu(this DlgARHall self)
		{
			ARSessionComponent arSessionComponent = ET.Client.ARSessionHelper.GetARSession(self.DomainScene());
			await arSessionComponent.HideMenu();
		}

		public static async ETTask TriggerJoinScene(this DlgARHall self)
		{
			if (self.playerStatusIn == PlayerStatus.Hall)
			{
				return;
			}

			bool roomExist = await RoomHelper.GetRoomInfoAsync(self.DomainScene(), self.roomId);
			if (roomExist == false)
			{
				return;
			}

			RoomManagerComponent roomManagerComponent = ET.Client.RoomHelper.GetRoomManager(self.DomainScene());
			RoomComponent roomComponent = roomManagerComponent.GetRoom(self.roomId);
			self.arSceneId = roomComponent.arSceneId;
			await Task.CompletedTask;
		}

		public static async ETTask OnClose(this DlgARHall self)
		{
			Log.Debug($"ET.Client.DlgARHallSystem.OnClose ");
			ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioBack(self.DomainScene());

			UIManagerHelper.GetUIComponent(self.DomainScene()).HideAllShownWindow();
			await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgGameMode>();
		}

		public static async ETTask OnCreateRoomCallBack(this DlgARHall self)
		{
			Log.Debug("ET.Client.DlgARHallSystem.OnCreateRoomCallBack begin");
			ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioClick(self.DomainScene());

			self.roomId = 0;
			await self.CreateRoom();
			PlayerComponent playerComponent = ET.Client.PlayerHelper.GetMyPlayerComponent(self.DomainScene());
			long roomId = playerComponent.RoomId;
			self.isHost = true;
			self.roomId = roomId;
			Log.Debug($"ET.Client.DlgARHallSystem.OnCreateRoomCallBack end {self.roomId}");
		}

		public static async ETTask OnQuitRoomCallBack(this DlgARHall self)
		{
			Log.Debug("ET.Client.DlgARHallSystem.OnQuitRoomCallBack begin");
			ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioClick(self.DomainScene());

			await self.QuitRoom();
			self.isHost = false;
			self.roomId = 0;
			Log.Debug($"ET.Client.DlgARHallSystem.OnQuitRoomCallBack end {self.roomId}");
		}

		public static (bool, string) GetQRCodeInfo(this DlgARHall self)
		{
			ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioClick(self.DomainScene());

			if (self.roomId == 0)
			{
				Log.Debug($"ET.Client.DlgARHallSystem.GetQRCodeInfo self.roomId == 0");
				return (false, "");
			}
			Log.Debug($"ET.Client.DlgARHallSystem.GetQRCodeInfo end {self.roomId}");
			return (true, self.roomId.ToString());
		}

		public static async ETTask OnJoinByQRCodeCallBack(this DlgARHall self, string sQRCodeInfo)
		{
			Log.Debug($"ET.Client.DlgARHallSystem.OnJoinByQRCodeCallBack end {sQRCodeInfo}");
			ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioClick(self.DomainScene());

			self.isHost = false;
			self.roomId = long.Parse(sQRCodeInfo);

			bool roomExist = await RoomHelper.GetRoomInfoAsync(self.DomainScene(), self.roomId);
			if (roomExist == false)
			{
				self.HideMenu().Coroutine();
				self.roomId = 0;

				string txt = "房间不存在";
				UIManagerHelper.ShowConfirm(self.DomainScene(), txt, () =>
				{
					self.ReStart().Coroutine();
				});
				return;
			}
		}

		public static async ETTask OnFinishedCallBack(this DlgARHall self)
		{
			Log.Debug($"ET.Client.DlgARHallSystem.OnFinishedCallBack ");
			ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioConfirm(self.DomainScene());

			PlayerComponent playerComponent = ET.Client.PlayerHelper.GetMyPlayerComponent(self.DomainScene());
			if (self.playerStatusIn != PlayerStatus.Hall)
			{
				if (playerComponent.PlayerStatus == PlayerStatus.Battle)
				{
					//这里应该是已经进入战斗后杀掉进程重启后
					await RoomHelper.ReturnBackBattle(self.ClientScene());

					return;
				}
				else if (playerComponent.PlayerStatus == PlayerStatus.Room)
				{
					bool roomExist = await RoomHelper.GetRoomInfoAsync(self.DomainScene(), self.roomId);
					if (roomExist == false)
					{
						self.HideMenu().Coroutine();
						string txt = "房间不存在";
						UIManagerHelper.ShowConfirm(self.DomainScene(), txt, () =>
						{
							self.ReStart().Coroutine();
						});
						return;
					}

					string arSceneId = ARSessionHelper.GetARSceneId(self.DomainScene());
					string _ARMeshDownLoadUrl = ARSessionHelper.GetARMeshDownLoadUrl(self.DomainScene());
					await self.SetARRoomInfoAsync(arSceneId, _ARMeshDownLoadUrl);

					await self.EnterARRoomUI();

					return;
				}
				else if (playerComponent.PlayerStatus == PlayerStatus.Hall)
				{
					//这里应该是下载完地图后发现已经被踢出房间

					await self.CreateRoom();

					string arSceneId = ARSessionHelper.GetARSceneId(self.DomainScene());
					string _ARMeshDownLoadUrl = ARSessionHelper.GetARMeshDownLoadUrl(self.DomainScene());
					await self.SetARRoomInfoAsync(arSceneId, _ARMeshDownLoadUrl);

					await self.EnterARRoomUI();

					return;
				}
			}

			if (self.roomId <= 0)
			{
				Log.Error($"self.roomId <= 0");
				return;
			}

			if (self.isHost)
			{
				string arSceneId = ARSessionHelper.GetARSceneId(self.DomainScene());
				string _ARMeshDownLoadUrl = ARSessionHelper.GetARMeshDownLoadUrl(self.DomainScene());
				await self.SetARRoomInfoAsync(arSceneId, _ARMeshDownLoadUrl);
				//主机 会走这里
				await self.EnterARRoomUI();
			}
			else
			{
				//从机 会走这里
				await self.JoinRoom(self.roomId);
			}
		}

		public static async ETTask CreateRoom(this DlgARHall self)
		{
			ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioClick(self.DomainScene());

			string battleCfgId = "GamePlayBattleLevel_ARRoom";
			bool result = await RoomHelper.CreateRoomAsync(self.ClientScene(), battleCfgId, true);
			if (result)
			{
			}
			else
			{
				self.HideMenu().Coroutine();
				string txt = "房间创建失败";
				UIManagerHelper.ShowConfirm(self.DomainScene(), txt, () =>
				{
					self.ReStart().Coroutine();
				});
			}
		}

		public static async ETTask QuitRoom(this DlgARHall self)
		{
			ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioClick(self.DomainScene());

			await RoomHelper.QuitRoomAsync(self.ClientScene());
		}

		public static async ETTask EnterARRoomUI(this DlgARHall self)
		{
			UIManagerHelper.GetUIComponent(self.DomainScene()).HideAllShownWindow();
			await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgARRoom>();
		}

		public static async ETTask JoinRoom(this DlgARHall self, long roomId)
		{
			bool result = await RoomHelper.JoinRoomAsync(self.ClientScene(), roomId);
			if (result)
			{
				await self.EnterARRoomUI();
			}
			else
			{
				self.HideMenu().Coroutine();
				string txt = "房间不存在";
				UIManagerHelper.ShowConfirm(self.DomainScene(), txt, () =>
				{
					self.ReStart().Coroutine();
				});
			}
		}

		public static async ETTask SetARRoomInfoAsync(this DlgARHall self, string arSceneId, string ARMeshDownLoadUrl)
		{
			bool result = await RoomHelper.SetARRoomInfoAsync(self.ClientScene(), arSceneId, ARMeshDownLoadUrl);
			if (result)
			{
			}
		}

	}
}
