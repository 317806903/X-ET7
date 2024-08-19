using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using MongoDB.Bson;
using Unity.Mathematics;

namespace ET
{
    public class RoomTypeInfo
    {
        public RoomType roomType;
        public SubRoomType subRoomType;
        public int seasonIndex;
        public int seasonCfgId;
        public int pveIndex;
        public string gamePlayBattleLevelCfgId;

        public static RoomTypeInfo GetFromBytes(byte[] bytes)
        {
            RoomTypeInfo roomTypeInfo;
            if (bytes == null)
            {
                roomTypeInfo = new();
                roomTypeInfo.roomType = RoomType.Normal;
                roomTypeInfo.subRoomType = SubRoomType.None;
            }
            else
            {
                roomTypeInfo = MongoHelper.Deserialize<RoomTypeInfo>(bytes);
            }
            return roomTypeInfo;
        }

        public static byte[] ToBytes(RoomTypeInfo roomTypeInfo)
        {
            return roomTypeInfo.ToBson();
        }

        public bool ChkIsAR()
        {
            bool isAR = false;
            if (roomType == RoomType.Normal)
            {
                if (this.subRoomType == SubRoomType.NormalARCreate || this.subRoomType == SubRoomType.NormalARScanCode)
                {
                    isAR = true;
                }
            }
            else if (roomType == RoomType.AR)
            {
                isAR = true;
            }
            return isAR;
        }

        public override string ToString()
        {
            return "{ "
                + "roomType:" + roomType.ToString() + ","
                + "subRoomType:" + subRoomType.ToString() + ","
                + "seasonCfgId:" + seasonCfgId + ","
                + "pveIndex:" + pveIndex + ","
                + "gamePlayBattleLevelCfgId:" + gamePlayBattleLevelCfgId + ","
                + "}";
        }

    }
}