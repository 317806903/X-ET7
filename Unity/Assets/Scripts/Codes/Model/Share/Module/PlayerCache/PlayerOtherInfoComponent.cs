using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace ET
{
    [ComponentOf(typeof (PlayerDataComponent))]
    public class PlayerOtherInfoComponent: Entity, IAwake, IDestroy, IFixedUpdate
    {
        public int waitFrameChk = 60;
        public int curFrameChk = 0;

        public Dictionary<string, bool> battleGuideStatus = new();
        public Dictionary<string, int> battleGuideStepIndex = new();

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<UIRedDotType, bool> uiRedDotTypeDic = new();

        public static Dictionary<UIRedDotType, UIRedDotType> UIRedDot2Parent = new()
        {
            {UIRedDotType.Settings, UIRedDotType.Root},
            {UIRedDotType.BattleDeck, UIRedDotType.Root},
            {UIRedDotType.Bags, UIRedDotType.Root},
            {UIRedDotType.Account, UIRedDotType.Root},
            {UIRedDotType.Mail, UIRedDotType.Root},
            {UIRedDotType.Questionnaire, UIRedDotType.Root},
            {UIRedDotType.Season, UIRedDotType.Root},
            {UIRedDotType.PVE, UIRedDotType.Root},
            {UIRedDotType.Store, UIRedDotType.Root},
            {UIRedDotType.NewSeasonNotice, UIRedDotType.Root},
            {UIRedDotType.MultPlayers, UIRedDotType.Root},

            {UIRedDotType.Tutorial, UIRedDotType.Settings},
            {UIRedDotType.Icon, UIRedDotType.Account},
            {UIRedDotType.SeasonBringUp, UIRedDotType.Season},
            {UIRedDotType.PVESeason, UIRedDotType.PVE},
            {UIRedDotType.TowerNew, UIRedDotType.Cards},
            {UIRedDotType.SkillNew, UIRedDotType.Skills},
            {UIRedDotType.AvatarFrameNew, UIRedDotType.Icon},
            {UIRedDotType.ItemOtherNew, UIRedDotType.Bags},
            {UIRedDotType.Cards, UIRedDotType.BattleDeck},
            {UIRedDotType.Skills, UIRedDotType.BattleDeck},
        };

        public HashSet<string> questionnaireStatus = new();

        public HashSet<string> battleNoticeStatus = new();
    }
}