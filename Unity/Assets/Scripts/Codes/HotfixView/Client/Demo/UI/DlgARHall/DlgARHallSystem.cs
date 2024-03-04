using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using ET.AbilityConfig;
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
				Log.Error("contextData == null");
				return;
			}
			else
			{
				DlgARHall_ShowWindowData _DlgARHall_ShowWindowData = contextData as DlgARHall_ShowWindowData;
				self.RoomTypeIn = _DlgARHall_ShowWindowData.RoomType;
				self.SubRoomTypeIn = _DlgARHall_ShowWindowData.SubRoomType;
				self.battleCfgId = _DlgARHall_ShowWindowData.battleCfgId;
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

			bool bForceIntoCreate = false;
			bool bForceIntoScan = false;
			if (self.RoomTypeIn == RoomType.Normal)
			{
				if (self.SubRoomTypeIn == SubRoomType.NormalARCreate)
				{
					bForceIntoCreate = true;
				}
				else if (self.SubRoomTypeIn == SubRoomType.NormalARScanCode)
				{
					bForceIntoScan = true;
				}
			}
			else if (self.RoomTypeIn == RoomType.AR)
			{
				if (self.SubRoomTypeIn == SubRoomType.ARScanCode)
				{
					bForceIntoScan = true;
				}
				else
				{
					bForceIntoCreate = true;
				}
			}

			ARSessionComponent arSessionComponent = ET.Client.ARSessionHelper.GetARSession(self.DomainScene());
			await arSessionComponent.Init(
				() => { self.OnClose().Coroutine(); },
				() => { self.OnFinishedCallBack().Coroutine(); },
				() => { self.OnCreateRoomCallBack().Coroutine(); },
				() => { self.OnQuitRoomCallBack().Coroutine(); },
				() => { self.OnCreateRoomCallBack().Coroutine(); },
				(sQRCodeInfo) => { self.OnJoinByQRCodeCallBack(sQRCodeInfo).Coroutine(); },
				() => { return self.GetQRCodeInfo(); },
				self.arSceneId, bForceIntoCreate, bForceIntoScan
				);

			// 此时进入AR场景，开始扫图或扫码，或加载上次扫图
			UIAudioManagerHelper.PlayMusic(self.DomainScene(), MusicType.ARStart);
			Log.Debug($"AR Prepare Start. game mode is {self.SubRoomTypeIn}.");
			EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
			{
				// 准备阶段开始
				eventName = "PrepareStarted",
				properties = new()
						{
							{"game_mode", self.SubRoomTypeIn.ToString()},
						}
			});
			
			EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLoggingStart()
			{
				// 准备阶段结束计时开始
				eventName = "PrepareEnded",
			});
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
			if (self.roomId == 0)
			{
				if (string.IsNullOrEmpty(self.battleCfgId))
				{
					self.battleCfgId = ET.GamePlayHelper.GetBattleCfgId(self.RoomTypeIn, self.SubRoomTypeIn, 0);
				}
				Log.Debug($"self.roomId==0 {self.battleCfgId}");
				return;
			}

			bool roomExist = await RoomHelper.GetRoomInfoAsync(self.DomainScene(), self.roomId);
			if (roomExist == false)
			{
				if (string.IsNullOrEmpty(self.battleCfgId))
				{
					self.battleCfgId = ET.GamePlayHelper.GetBattleCfgId(self.RoomTypeIn, self.SubRoomTypeIn, 0);
				}
				Log.Debug($"roomExist==false {self.battleCfgId}");
				return;
			}

			RoomManagerComponent roomManagerComponent = ET.Client.RoomHelper.GetRoomManager(self.DomainScene());
			RoomComponent roomComponent = roomManagerComponent.GetRoom(self.roomId);
			self.arSceneId = roomComponent.arSceneId;
			self.battleCfgId = roomComponent.gamePlayBattleLevelCfgId;
			await Task.CompletedTask;
		}

		public static async ETTask OnClose(this DlgARHall self)
		{
			Log.Debug($"ET.Client.DlgARHallSystem.OnClose ");
			UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);

			// 此时AR准备阶段被用户取消，AR界面关闭，回到主界面。
			string entranceType = ARSessionHelper.GetAREntranceType(self.DomainScene());
			Log.Debug($"AR Prepare End with cancellation. EntranceType={entranceType}");
			EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
			{
				// 准备阶段结束计时结束
				eventName = "PrepareEnded",
				properties = new()
						{
							{"success", false },
							{"session_id", ""},
							{"game_mode", self.SubRoomTypeIn.ToString()},
							{"mesh_source", entranceType}
						}
			});
			// 重置entrance type给下一个session
			ARSessionHelper.ResetAREntranceType(self.DomainScene());

			await UIManagerHelper.ExitRoom(self.DomainScene());
		}

		public static async ETTask OnCreateRoomCallBack(this DlgARHall self)
		{
			Log.Debug("ET.Client.DlgARHallSystem.OnCreateRoomCallBack begin");
			UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);
			//UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Scan, true);

			self.roomId = 0;
			await self.CreateRoom();
			PlayerStatusComponent playerStatusComponent = ET.Client.PlayerHelper.GetMyPlayerStatusComponent(self.DomainScene());
			long roomId = playerStatusComponent.RoomId;
			self.isHost = true;
			self.roomId = roomId;
			Log.Debug($"ET.Client.DlgARHallSystem.OnCreateRoomCallBack end {self.roomId}");
		}

		public static async ETTask OnQuitRoomCallBack(this DlgARHall self)
		{
			try
			{
				Log.Debug($"ET.Client.DlgARHallSystem.OnQuitRoomCallBack begin self=[{self}]");
				UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

				self.QuitRoom().Coroutine();
				self.isHost = false;
				self.roomId = 0;
				Log.Debug($"ET.Client.DlgARHallSystem.OnQuitRoomCallBack end {self.roomId}");
			}
			catch (Exception e)
			{
				Log.Error(e);
			}
		}

		public static (bool, string) GetQRCodeInfo(this DlgARHall self)
		{
			UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

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
			UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

			self.isHost = false;
			self.roomId = long.Parse(sQRCodeInfo);

			bool roomExist = await RoomHelper.GetRoomInfoAsync(self.DomainScene(), self.roomId);
			if (roomExist == false)
			{
				self.HideMenu().Coroutine();
				self.roomId = 0;

				string txt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Hall_RoomNotExist");
				UIManagerHelper.ShowOnlyConfirm(self.DomainScene(), txt, () =>
				{
					//self.ReStart().Coroutine();
					self.OnClose().Coroutine();
				});
				return;
			}
			else
			{
				bool result = await RoomHelper.JoinRoomAsync(self.ClientScene(), self.roomId);
			}
		}

		public static async ETTask OnFinishedCallBack(this DlgARHall self)
		{
			Log.Debug($"ET.Client.DlgARHallSystem.OnFinishedCallBack ");
			UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);

			PlayerStatusComponent playerStatusComponent = ET.Client.PlayerHelper.GetMyPlayerStatusComponent(self.DomainScene());
			Log.Debug($"-OnFinishedCallBack self.playerStatusIn[{self.playerStatusIn.ToString()}] playerComponent.PlayerStatus[{playerStatusComponent.PlayerStatus.ToString()}]");

			string arSceneId = ARSessionHelper.GetARSceneId(self.DomainScene());

			if (self.playerStatusIn != PlayerStatus.Hall)
			{
				if (playerStatusComponent.PlayerStatus == PlayerStatus.Battle)
				{
					//这里应该是已经进入战斗后杀掉进程重启后
					await RoomHelper.ReturnBackBattle(self.ClientScene());

					return;
				}
				else if (playerStatusComponent.PlayerStatus == PlayerStatus.Room)
				{
					bool roomExist = await RoomHelper.GetRoomInfoAsync(self.DomainScene(), self.roomId);
					if (roomExist == false)
					{
						self.HideMenu().Coroutine();
						string txt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Hall_RoomNotExist");
						UIManagerHelper.ShowOnlyConfirm(self.DomainScene(), txt, () =>
						{
							//self.ReStart().Coroutine();
							self.OnClose().Coroutine();
						});
						return;
					}

					if (self.isHost)
					{
						await self.SetARRoomInfoAsync();

						await self.EnterARRoomUI();
					}
					else
					{
						//从机 会走这里
						await self.JoinRoom(self.roomId);
					}

					return;
				}
				else if (playerStatusComponent.PlayerStatus == PlayerStatus.Hall)
				{
					//这里应该是下载完地图后发现已经被踢出房间

					await self.CreateRoom();

					await self.SetARRoomInfoAsync();

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
				await self.SetARRoomInfoAsync();
				//主机 会走这里
				await self.EnterARRoomUI();
			}
			else
			{
				//从机 会走这里
				await self.JoinRoom(self.roomId);
			}

			// 此时AR准备阶段完全结束，玩家已经加入房间等待
			string entranceType = ARSessionHelper.GetAREntranceType(self.DomainScene());
			Log.Debug($"AR Prepare End. EntranceType={entranceType} sessionID={arSceneId}");
			EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
			{
				// 准备阶段结束计时结束
				eventName = "PrepareEnded",
				properties = new()
						{
							{"success", true },
							{"session_id", arSceneId},
							{"game_mode", self.SubRoomTypeIn.ToString()},
							{"mesh_source", entranceType}
						}
			});
			// 重置entrance type给下一个session
			ARSessionHelper.ResetAREntranceType(self.DomainScene());
		}

		public static async ETTask CreateRoom(this DlgARHall self)
		{
			UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

			if (GamePlayBattleLevelCfgCategory.Instance.Contain(self.battleCfgId) == false)
			{
				Log.Error($"GamePlayBattleLevelCfgCategory.Instance.Contain({self.battleCfgId}) == false");
				return;
			}

			bool result = await RoomHelper.CreateRoomAsync(self.ClientScene(), self.battleCfgId, self.RoomTypeIn, self.SubRoomTypeIn);
			if (result)
			{
			}
			else
			{
				self.HideMenu().Coroutine();
				string txt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Hall_CreateRoomError");
				UIManagerHelper.ShowOnlyConfirm(self.DomainScene(), txt, () =>
				{
					//self.ReStart().Coroutine();
					self.OnClose().Coroutine();
				});
			}
		}

		public static async ETTask QuitRoom(this DlgARHall self)
		{
			UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

			await RoomHelper.QuitRoomAsync(self.ClientScene());
		}

		public static async ETTask EnterARRoomUI(this DlgARHall self)
		{
			await ET.Client.UIManagerHelper.EnterRoom(self.DomainScene());
		}

		public static async ETTask JoinRoom(this DlgARHall self, long roomId)
		{
			await self.EnterARRoomUI();
			// bool result = await RoomHelper.JoinRoomAsync(self.ClientScene(), roomId);
			// if (result)
			// {
			// 	await self.EnterARRoomUI();
			// }
			// else
			// {
			// 	self.HideMenu().Coroutine();
			// 	string txt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Hall_RoomNotExist");
			// 	UIManagerHelper.ShowOnlyConfirm(self.DomainScene(), txt, () =>
			// 	{
			// 		self.ReStart().Coroutine();
			// 	});
			// }
		}

		public static async ETTask SetARRoomInfoAsync(this DlgARHall self)
		{
			string arSceneId = ARSessionHelper.GetARSceneId(self.DomainScene());
			string arMeshDownLoadUrl = ARSessionHelper.GetARMeshDownLoadUrl(self.DomainScene());
			float arMapScale = ARSessionHelper.GetScaleAR(self.DomainScene());
			if (arMapScale == 0)
			{
				arMapScale = 30;
			}
			bool result = await RoomHelper.SetARRoomInfoAsync(self.ClientScene(), arSceneId, arMeshDownLoadUrl, arMapScale);
			if (result)
			{
			}
		}

	}
}
