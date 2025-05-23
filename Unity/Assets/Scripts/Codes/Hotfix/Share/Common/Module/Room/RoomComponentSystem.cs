﻿using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET
{
    [FriendOf(typeof(RoomComponent))]
    public static class RoomComponentSystem
    {
        [ObjectSystem]
        public class RoomComponentAwakeSystem : AwakeSystem<RoomComponent>
        {
            protected override void Awake(RoomComponent self)
            {
                self.roomMemberSeat = new();
            }
        }

        public static void Init(this RoomComponent self, RoomTypeInfo roomTypeInfo, long playerId, RoomTeamMode roomTeamMode)
        {
            self.roomTypeInfo = roomTypeInfo;
            self.arSceneId = "";
            self.arSceneMeshId = "";
            self.roomStatus = RoomStatus.Idle;
            self.ownerRoomMemberId = playerId;
            self.roomTeamMode = roomTeamMode;

            GamePlayBattleLevelCfg gamePlayBattleLevelCfg = GamePlayBattleLevelCfgCategory.Instance.Get(self.roomTypeInfo.gamePlayBattleLevelCfgId);
            self.MaxMemberCount = gamePlayBattleLevelCfg.MaxPlayerCount;
            for (int i = 0; i < self.MaxMemberCount; i++)
            {
                self.roomMemberSeat.Add(-1);
            }

            self.AddRoomMember(playerId, true, RoomTeamId.Green, 0);
        }

        public static void ChgRoomStatus(this RoomComponent self, RoomStatus roomStatus)
        {
            self.roomStatus = roomStatus;
        }

        public static void ChgRoomTeamMode(this RoomComponent self, RoomTeamMode roomTeamMode)
        {
            self.roomTeamMode = roomTeamMode;
        }

        public static bool ChkIsOwner(this RoomComponent self, long playerId)
        {
            return self.ownerRoomMemberId == playerId;
        }

        public static bool ChkIsAllReady(this RoomComponent self)
        {
            foreach (RoomMember roomMember in self.GetRoomMemberList())
            {
                if (self.ownerRoomMemberId != roomMember.Id)
                {
                    if (roomMember.isReady == false)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static List<RoomMember> GetRoomMemberList(this RoomComponent self)
        {
            ListComponent<RoomMember> list = ListComponent<RoomMember>.Create();
            foreach (var child in self.Children)
            {
                list.Add(child.Value as RoomMember);
            }
            return list;
        }

        public static void ChgOwnerRoomMember(this RoomComponent self, long ownerPlayerId)
        {
            long oldOwnerPlayerId = self.ownerRoomMemberId;
            RoomMember oldOwnerRoomMember = self.GetRoomMember(oldOwnerPlayerId);
            oldOwnerRoomMember.isOwner = false;
            RoomMember ownerRoomMember = self.GetRoomMember(ownerPlayerId);
            ownerRoomMember.isOwner = true;
            ownerRoomMember.isReady = false;
        }

        public static RoomMember GetRoomMember(this RoomComponent self, long playerId)
        {
            return self.GetChild<RoomMember>(playerId);
        }

        public static bool ChkRoomMemberIsFull(this RoomComponent self)
        {
            int count = self.roomMemberSeat.Count;
            for (int i = 0; i < count; i++)
            {
                if (self.roomMemberSeat[i] == -1)
                {
                    return false;
                }
            }
            return true;
        }

        public static RoomMember AddRoomMember(this RoomComponent self, long playerId, bool isOwner, RoomTeamId roomTeamId, int seatIndex)
        {
            if (seatIndex == -1)
            {
                int count = self.roomMemberSeat.Count;
                for (int i = 0; i < count; i++)
                {
                    if (self.roomMemberSeat[i] == -1)
                    {
                        seatIndex = i;
                        break;
                    }
                }

                if (seatIndex == -1)
                {
                    return null;
                }
            }

            RoomMember roomMember = self.AddChildWithId<RoomMember>(playerId);
            roomMember.isOwner = isOwner;
            roomMember.isReady = false;
            roomMember.roomTeamId = roomTeamId;
            roomMember.seatIndex = seatIndex;

            self.roomMemberSeat[seatIndex] = roomMember.Id;

            return roomMember;
        }

        public static bool RemoveRoomMember(this RoomComponent self, long playerId)
        {
            RoomMember curRoomMember = self.GetChild<RoomMember>(playerId);
            if (curRoomMember == null)
            {
                return false;
            }
            bool isEmptyMember = false;
            bool isOwner = playerId == self.ownerRoomMemberId;
            if (isOwner)
            {
                RoomMember newOwnRoomMember = null;
                foreach (var child in self.Children)
                {
                    RoomMember roomMember = child.Value as RoomMember;
                    if (roomMember.Id != playerId)
                    {
                        newOwnRoomMember = roomMember;
                        break;
                    }
                }

                if (newOwnRoomMember != null)
                {
                    self.ChgOwnerRoomMember(newOwnRoomMember.Id);
                    self.ownerRoomMemberId = newOwnRoomMember.Id;
                }
                else
                {
                    isEmptyMember = true;
                }
            }

            if (self.roomMemberSeat.Count > curRoomMember.seatIndex)
            {
                self.roomMemberSeat[curRoomMember.seatIndex] = -1;
            }

            curRoomMember.Dispose();

            if (isEmptyMember)
            {
                self.ownerRoomMemberId = -1;
            }

            return isEmptyMember;
        }

        public static void ChgRoomMemberStatus(this RoomComponent self, long playerId, bool isReady)
        {
            RoomMember ownerRoomMember = self.GetRoomMember(playerId);
            ownerRoomMember.isReady = isReady;
        }

        public static void ChgRoomMemberSeat(this RoomComponent self, long playerId, int seatIndex)
        {
            RoomMember ownerRoomMember = self.GetRoomMember(playerId);
            self.roomMemberSeat[ownerRoomMember.seatIndex] = -1;
            ownerRoomMember.seatIndex = seatIndex;
            self.roomMemberSeat[seatIndex] = playerId;
        }

        public static void ChgRoomMemberTeam(this RoomComponent self, long playerId, RoomTeamId roomTeamId)
        {
            RoomMember ownerRoomMember = self.GetRoomMember(playerId);
            ownerRoomMember.roomTeamId = roomTeamId;
            ownerRoomMember.isReady = false;
        }

        public static void ChgRoomBattleLevelCfg(this RoomComponent self, RoomTypeInfo roomTypeInfo)
        {
            self.roomTypeInfo = roomTypeInfo;

            int lastMaxMemberCount = self.MaxMemberCount;
            GamePlayBattleLevelCfg gamePlayBattleLevelCfg = GamePlayBattleLevelCfgCategory.Instance.Get(self.roomTypeInfo.gamePlayBattleLevelCfgId);
            self.MaxMemberCount = gamePlayBattleLevelCfg.MaxPlayerCount;
            if (lastMaxMemberCount < self.MaxMemberCount)
            {
                for (int i = lastMaxMemberCount; i < self.MaxMemberCount; i++)
                {
                    self.roomMemberSeat.Add(-1);
                }
            }
            else if (lastMaxMemberCount > self.MaxMemberCount)
            {
                for (int i = lastMaxMemberCount - 1; i >= self.MaxMemberCount; i--)
                {
                    long playerId = self.roomMemberSeat[i];
                    if (playerId != (long)ET.PlayerId.PlayerNone)
                    {
                        for (int j = 0; j < self.MaxMemberCount; j++)
                        {
                            if (self.roomMemberSeat[j] == -1)
                            {
                                self.roomMemberSeat[j] = playerId;
                                RoomMember roomMember = self.GetRoomMember(playerId);
                                roomMember.seatIndex = j;
                                break;
                            }
                        }
                    }
                    self.roomMemberSeat.RemoveAt(i);
                }
            }

        }


        public static bool IsARRoom(this RoomComponent self)
        {
            bool isAR = false;
            if (self.roomTypeInfo.roomType == RoomType.AR)
            {
                isAR = true;
            }
            else if(self.roomTypeInfo.roomType == RoomType.Normal)
            {
                if (self.roomTypeInfo.subRoomType == SubRoomType.NormalARCreate || self.roomTypeInfo.subRoomType == SubRoomType.NormalARScanCode)
                {
                    isAR = true;
                }
            }
            return isAR;
        }

        public static (bool, string) ChkOwnerStartGame(this RoomComponent self)
        {
            if (self.ChkIsAllReady() == false)
            {
                string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Room_WaitAllReady");
                return (false, msg);
            }

            GamePlayBattleLevelCfg gamePlayBattleLevelCfg = GamePlayBattleLevelCfgCategory.Instance.Get(self.roomTypeInfo.gamePlayBattleLevelCfgId);
            PlayerTeam playerTeam = gamePlayBattleLevelCfg.TeamMode as PlayerTeam;
            if (playerTeam != null)
            {
                int countRoomTeamId_Green = 0;
                int countRoomTeamId_Red = 0;
                int countRoomTeamId_Blue = 0;

                List<RoomMember> memberList = self.GetRoomMemberList();
                foreach (var roomMember in memberList)
                {
                    if (roomMember.roomTeamId == RoomTeamId.Green)
                    {
                        countRoomTeamId_Green++;
                    }
                    else if (roomMember.roomTeamId == RoomTeamId.Red)
                    {
                        countRoomTeamId_Red++;
                    }
                    else if (roomMember.roomTeamId == RoomTeamId.Blue)
                    {
                        countRoomTeamId_Blue++;
                    }
                }
                if (playerTeam.MaxTeamCount == 2)
                {
                    if (countRoomTeamId_Green == 0 || countRoomTeamId_Red == 0)
                    {
                        string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Room_EachCampHaveSomeone");
                        return (false, msg);
                    }
                    if (playerTeam.IsAllowDiffTeamMember == false)
                    {
                        if (countRoomTeamId_Green != countRoomTeamId_Red)
                        {
                            string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Room_CampNumberInconsistent");
                            return (false, msg);
                        }
                    }
                }
                else if (playerTeam.MaxTeamCount == 3)
                {
                    if (countRoomTeamId_Green == 0 || countRoomTeamId_Red == 0 || countRoomTeamId_Blue == 0)
                    {
                        string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Room_EachCampHaveSomeone");
                        return (false, msg);
                    }
                    if (playerTeam.IsAllowDiffTeamMember == false)
                    {
                        if (countRoomTeamId_Green != countRoomTeamId_Red
                            || countRoomTeamId_Green != countRoomTeamId_Blue
                            || countRoomTeamId_Red != countRoomTeamId_Blue)
                        {
                            string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Room_CampNumberInconsistent");
                            return (false, msg);
                        }
                    }
                }
                else if (playerTeam.MaxTeamCount < 2)
                {
                    string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Room_NotSupport",2);
                    return (false, msg);
                }
                else if (playerTeam.MaxTeamCount > 3)
                {
                    string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Room_NotSupport",3);
                    return (false, msg);
                }
            }

            return (true, "");
        }

    }
}