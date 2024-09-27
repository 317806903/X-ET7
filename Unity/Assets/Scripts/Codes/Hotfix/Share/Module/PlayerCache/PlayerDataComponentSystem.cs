using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ET
{
    [FriendOf(typeof(PlayerDataComponent))]
    public static class PlayerDataComponentSystem
    {
        [ObjectSystem]
        public class PlayerDataComponentAwakeSystem : AwakeSystem<PlayerDataComponent>
        {
            protected override void Awake(PlayerDataComponent self)
            {
                self.playerId = self.Id;
            }
        }

        public static Entity GetPlayerModel(this PlayerDataComponent self, PlayerModelType playerModelType)
        {
            switch (playerModelType)
            {
                case PlayerModelType.BaseInfo:
                    return self.GetComponent<PlayerBaseInfoComponent>();
                case PlayerModelType.BackPack:
                    return self.GetComponent<PlayerBackPackComponent>();
                case PlayerModelType.BattleCard:
                    return self.GetComponent<PlayerBattleCardComponent>();
                case PlayerModelType.OtherInfo:
                    return self.GetComponent<PlayerOtherInfoComponent>();
                case PlayerModelType.SeasonInfo:
                    return self.GetComponent<PlayerSeasonInfoComponent>();
                case PlayerModelType.FunctionMenu:
                    return self.GetComponent<PlayerFunctionMenuComponent>();
                case PlayerModelType.Mails:
                    return self.GetComponent<PlayerMailComponent>();
                case PlayerModelType.Skills:
                    return self.GetComponent<PlayerSkillComponent>();
                default:
                    break;
            }

            return null;
        }

        public static Entity SetPlayerModel(this PlayerDataComponent self, PlayerModelType playerModelType, byte[] bytes, List<string> setPlayerKeys)
        {
            Entity entity = MongoHelper.Deserialize<Entity>(bytes);
            if (setPlayerKeys == null || setPlayerKeys.Count == 0)
            {
                switch (playerModelType)
                {
                    case PlayerModelType.BaseInfo:
                        self.RemoveComponent<PlayerBaseInfoComponent>();
                        break;
                    case PlayerModelType.BackPack:
                        self.RemoveComponent<PlayerBackPackComponent>();
                        break;
                    case PlayerModelType.BattleCard:
                        self.RemoveComponent<PlayerBattleCardComponent>();
                        break;
                    case PlayerModelType.OtherInfo:
                        self.RemoveComponent<PlayerOtherInfoComponent>();
                        break;
                    case PlayerModelType.SeasonInfo:
                        self.RemoveComponent<PlayerSeasonInfoComponent>();
                        break;
                    case PlayerModelType.FunctionMenu:
                        self.RemoveComponent<PlayerFunctionMenuComponent>();
                        break;
                    case PlayerModelType.Mails:
                        self.RemoveComponent<PlayerMailComponent>();
                        break;
                    case PlayerModelType.Skills:
                        self.RemoveComponent<PlayerSkillComponent>();
                        break;
                    default:
                        break;
                }
                return self.AddComponent(entity);
            }
            else
            {
                switch (playerModelType)
                {
                    case PlayerModelType.BaseInfo:
                        PlayerBaseInfoComponent playerBaseInfoComponent = self.GetComponent<PlayerBaseInfoComponent>();
                        self.ChgFieldValue<PlayerBaseInfoComponent>(playerBaseInfoComponent, (PlayerBaseInfoComponent)entity, setPlayerKeys);
                        entity.Dispose();
                        return playerBaseInfoComponent;
                    case PlayerModelType.BackPack:
                        PlayerBackPackComponent playerBackPackComponent = self.GetComponent<PlayerBackPackComponent>();
                        self.ChgFieldValue<PlayerBackPackComponent>(playerBackPackComponent, (PlayerBackPackComponent)entity, setPlayerKeys);
                        entity.Dispose();
                        return playerBackPackComponent;
                    case PlayerModelType.BattleCard:
                        PlayerBattleCardComponent battleCardComponent = self.GetComponent<PlayerBattleCardComponent>();
                        self.ChgFieldValue<PlayerBattleCardComponent>(battleCardComponent, (PlayerBattleCardComponent)entity, setPlayerKeys);
                        entity.Dispose();
                        return battleCardComponent;
                    case PlayerModelType.OtherInfo:
                        PlayerOtherInfoComponent playerOtherInfoComponent = self.GetComponent<PlayerOtherInfoComponent>();
                        self.ChgFieldValue<PlayerOtherInfoComponent>(playerOtherInfoComponent, (PlayerOtherInfoComponent)entity, setPlayerKeys);
                        entity.Dispose();
                        return playerOtherInfoComponent;
                    case PlayerModelType.SeasonInfo:
                        PlayerSeasonInfoComponent playerSeasonInfoComponent = self.GetComponent<PlayerSeasonInfoComponent>();
                        self.ChgFieldValue<PlayerSeasonInfoComponent>(playerSeasonInfoComponent, (PlayerSeasonInfoComponent)entity, setPlayerKeys);
                        entity.Dispose();
                        return playerSeasonInfoComponent;
                    case PlayerModelType.FunctionMenu:
                        PlayerFunctionMenuComponent playerFunctionMenuComponent = self.GetComponent<PlayerFunctionMenuComponent>();
                        self.ChgFieldValue<PlayerFunctionMenuComponent>(playerFunctionMenuComponent, (PlayerFunctionMenuComponent)entity, setPlayerKeys);
                        entity.Dispose();
                        return playerFunctionMenuComponent;
                    case PlayerModelType.Mails:
                        PlayerMailComponent playerMailComponent = self.GetComponent<PlayerMailComponent>();
                        self.ChgFieldValue<PlayerMailComponent>(playerMailComponent, (PlayerMailComponent)entity, setPlayerKeys);
                        entity.Dispose();
                        return playerMailComponent;
                    case PlayerModelType.Skills:
                        PlayerSkillComponent playerSkillComponent = self.GetComponent<PlayerSkillComponent>();
                        self.ChgFieldValue<PlayerSkillComponent>(playerSkillComponent, (PlayerSkillComponent)entity, setPlayerKeys);
                        entity.Dispose();
                        return playerSkillComponent;
                    default:
                        entity.Dispose();
                        break;
                }

                return null;
            }
        }

        public static void ChgFieldValue<T>(this PlayerDataComponent self, T entity, T entityNew, List<string> setPlayerKeys) where T:Entity
        {
            Type type = typeof(T);
            for (int i = 0; i < setPlayerKeys.Count; i++)
            {
                try
                {
                    FieldInfo field = type.GetField(setPlayerKeys[i]);
                    field.SetValue(entity, field.GetValue(entityNew));
                }
                catch (Exception e)
                {
                    Log.Error($"Err [{type.Name}] [{setPlayerKeys[i]}] ChgFieldValue {e}");
                }
            }
        }

    }
}