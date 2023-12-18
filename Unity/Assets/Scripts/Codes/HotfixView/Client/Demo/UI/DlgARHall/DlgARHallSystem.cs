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
				self.PveLevel = _DlgARHall_ShowWindowData.PveLevel;
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
			UIAudioManagerHelper.PlayUIAudioBack(self.DomainScene());

			PlayerStatusComponent playerStatusComponent = ET.Client.PlayerHelper.GetMyPlayerStatusComponent(self.DomainScene());
			string arSceneIdTmp = ARSessionHelper.GetARSceneId(self.DomainScene());
			EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
			{
				eventName = "ScanEnded",
				properties = new()
				{
					{"success", false},
					{"session_id", arSceneIdTmp},
					{"game_mode", playerStatusComponent.SubRoomType.ToString()},
				}
			});

			await UIManagerHelper.ExitRoom(self.DomainScene());
		}

		public static async ETTask OnCreateRoomCallBack(this DlgARHall self)
		{
			Log.Debug("ET.Client.DlgARHallSystem.OnCreateRoomCallBack begin");
			UIAudioManagerHelper.PlayUIAudioClick(self.DomainScene());

			self.roomId = 0;
			await self.CreateRoom();
			PlayerStatusComponent playerStatusComponent = ET.Client.PlayerHelper.GetMyPlayerStatusComponent(self.DomainScene());
			long roomId = playerStatusComponent.RoomId;
			self.isHost = true;
			self.roomId = roomId;
			Log.Debug($"ET.Client.DlgARHallSystem.OnCreateRoomCallBack end {self.roomId}");

			EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
			{
				eventName = "ScanStarted",
				properties = new()
				{
					{"game_mode", playerStatusComponent.SubRoomType.ToString()},
				}
			});

			EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLoggingStart()
			{
				eventName = "ScanEnded",
			});

		}

		public static async ETTask OnQuitRoomCallBack(this DlgARHall self)
		{
			try
			{
				Log.Debug($"ET.Client.DlgARHallSystem.OnQuitRoomCallBack begin self=[{self}]");
				UIAudioManagerHelper.PlayUIAudioClick(self.DomainScene());

				self.QuitRoom().Coroutine();
				self.isHost = false;
				self.roomId = 0;
				Log.Debug($"ET.Client.DlgARHallSystem.OnQuitRoomCallBack end {self.roomId}");

				PlayerStatusComponent playerStatusComponent = ET.Client.PlayerHelper.GetMyPlayerStatusComponent(self.DomainScene());
				string arSceneIdTmp = ARSessionHelper.GetARSceneId(self.DomainScene());
				EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
				{
					eventName = "ScanEnded",
					properties = new()
					{
						{"success", false},
						{"session_id", arSceneIdTmp},
						{"game_mode", playerStatusComponent.SubRoomType.ToString()},
					}
				});

			}
			catch (Exception e)
			{
				Log.Error(e);
			}
		}

		public static (bool, string) GetQRCodeInfo(this DlgARHall self)
		{
			UIAudioManagerHelper.PlayUIAudioClick(self.DomainScene());

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
			UIAudioManagerHelper.PlayUIAudioClick(self.DomainScene());

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
					self.ReStart().Coroutine();
				});
				return;
			}
		}

		public static async ETTask OnFinishedCallBack(this DlgARHall self)
		{
			Log.Debug($"ET.Client.DlgARHallSystem.OnFinishedCallBack ");
			UIAudioManagerHelper.PlayUIAudioConfirm(self.DomainScene());

			PlayerStatusComponent playerStatusComponent = ET.Client.PlayerHelper.GetMyPlayerStatusComponent(self.DomainScene());
			Log.Debug($"-OnFinishedCallBack self.playerStatusIn[{self.playerStatusIn.ToString()}] playerComponent.PlayerStatus[{playerStatusComponent.PlayerStatus.ToString()}]");

			string arSceneIdTmp = ARSessionHelper.GetARSceneId(self.DomainScene());
			EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
			{
				eventName = "ScanEnded",
				properties = new()
				{
					{"success", true},
					{"session_id", arSceneIdTmp},
					{"game_mode", playerStatusComponent.SubRoomType.ToString()},
				}
			});

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
				else if (playerStatusComponent.PlayerStatus == PlayerStatus.Hall)
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
			UIAudioManagerHelper.PlayUIAudioClick(self.DomainScene());

			string battleCfgId = "";
			if (self.RoomTypeIn == RoomType.Normal)
			{
				battleCfgId = "GamePlayBattleLevel_ARRoom";
			}
			else if (self.RoomTypeIn == RoomType.AR)
			{
				if (self.SubRoomTypeIn == SubRoomType.ARPVE)
				{
					TowerDefense_ChallengeLevelCfg challengeLevelCfg =
                    	TowerDefense_ChallengeLevelCfgCategory.Instance.Get("Level" + self.PveLevel);
					battleCfgId = challengeLevelCfg.BattleLevel;
				}
				else if (self.SubRoomTypeIn == SubRoomType.ARPVP)
				{
					battleCfgId = GlobalSettingCfgCategory.Instance.ARPVPCfgId;
				}
				else if (self.SubRoomTypeIn == SubRoomType.AREndlessChallenge)
				{
					battleCfgId = GlobalSettingCfgCategory.Instance.AREndlessChallengeCfgId;
				}
				else if (self.SubRoomTypeIn == SubRoomType.ARTutorialFirst)
				{
					battleCfgId = GlobalSettingCfgCategory.Instance.ARTutorialFirstCfgId;
				}
			}

			bool result = await RoomHelper.CreateRoomAsync(self.ClientScene(), battleCfgId, self.RoomTypeIn, self.SubRoomTypeIn);
			if (result)
			{
			}
			else
			{
				self.HideMenu().Coroutine();
				string txt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Hall_CreateRoomError");
				UIManagerHelper.ShowOnlyConfirm(self.DomainScene(), txt, () =>
				{
					self.ReStart().Coroutine();
				});
			}
		}

		public static async ETTask QuitRoom(this DlgARHall self)
		{
			UIAudioManagerHelper.PlayUIAudioClick(self.DomainScene());

			await RoomHelper.QuitRoomAsync(self.ClientScene());
		}

		public static async ETTask EnterARRoomUI(this DlgARHall self)
		{
			await ET.Client.UIManagerHelper.EnterRoom(self.DomainScene());
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
				string txt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Hall_RoomNotExist");
				UIManagerHelper.ShowOnlyConfirm(self.DomainScene(), txt, () =>
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
