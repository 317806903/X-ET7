using System;
using System.Text;
using ET.AbilityConfig;

namespace ET
{
    [FriendOf(typeof(LocalizeComponent))]
    public static class LocalizeComponentSystem
    {
        [ObjectSystem]
        public class LocalizeComponentAwakeSystem : AwakeSystem<LocalizeComponent>
        {
            protected override void Awake(LocalizeComponent self)
            {
                self.CurrentLanguage = LanguageType.CN;
                //self.SwitchLanguage(self.CurrentLanguage, true);
                LocalizeComponent.Instance = self;
            }
        }

        [ObjectSystem]
        public class LocalizeComponentDestroySystem: DestroySystem<LocalizeComponent>
        {
            protected override void Destroy(LocalizeComponent self)
            {
                LocalizeComponent.Instance = null;
            }
        }

        public static void SwitchLanguage(this LocalizeComponent self, LanguageType language, bool forceSet)
        {
            if (self.CurrentLanguage == language && forceSet == false)
            {
                return;
            }

            self.CurrentLanguage = language;

            self.PreLoad(self.CurrentLanguage);

            ConfigComponent.Instance.TranslateText(self.GetTextValueByExcel);

            EventSystem.Instance.Publish(self.DomainScene(), new EventType.SwitchLanguage() {languageType = language});

            return;
        }

        public static void ResetLanguage(this LocalizeComponent self)
        {
            ConfigComponent.Instance.TranslateText(self.GetTextValueByExcel);

            EventSystem.Instance.Publish(self.DomainScene(), new EventType.SwitchLanguage() {languageType = self.CurrentLanguage});

            return;
        }

        public static string GetTextValueByExcel(this LocalizeComponent self, string textKey)
        {
            var translateCode = self.GetCurrentTranslator_Excel();
            string textValue = translateCode(self.CurrentLanguage, textKey, textKey);
            return textValue;
        }

        public static string GetTextValueByExcel(this LocalizeComponent self, string textKey, string originText)
        {
            var translateCode = self.GetCurrentTranslator_Excel();
            string textValue = translateCode(self.CurrentLanguage, textKey, originText);
            return textValue;
        }

        public static string GetTextValueByExcel(this LocalizeComponent self, LanguageType languageType, string textKey)
        {
            var translateCode = self.GetCurrentTranslator_Excel();
            string textValue = translateCode(languageType, textKey, textKey);
            return textValue;
        }

        public static string GetTextValueByExcel(this LocalizeComponent self, LanguageType languageType, string textKey, string originText)
        {
            var translateCode = self.GetCurrentTranslator_Excel();
            string textValue = translateCode(languageType, textKey, originText);
            return textValue;
        }

        public static string GetTextValue(this LocalizeComponent self, string textKey)
        {
            var translateCode = self.GetCurrentTranslator_Code();
            string textValue = translateCode(self.CurrentLanguage, textKey, textKey);
            return textValue;
        }

        public static string GetTextValue(this LocalizeComponent self, LanguageType languageType, string textKey)
        {
            var translateCode = self.GetCurrentTranslator_Code();
            string textValue = translateCode(languageType, textKey, textKey);
            return textValue;
        }

        public static string GetTextValue(this LocalizeComponent self, string textKey, params object[] args)
        {
            var translateCode = self.GetCurrentTranslator_Code();
            string textValue = translateCode(self.CurrentLanguage, textKey, textKey);
            textValue = string.Format(textValue, args);
            return textValue;
        }

        public static string GetTextValue(this LocalizeComponent self, LanguageType languageType, string textKey, params object[] args)
        {
            var translateCode = self.GetCurrentTranslator_Code();
            string textValue = translateCode(languageType, textKey, textKey);
            textValue = string.Format(textValue, args);
            return textValue;
        }

        public static Func<LanguageType, string, string, string> GetCurrentTranslator_Excel(this LocalizeComponent self)
        {
            return self._Translate_Excel;
        }

        public static Func<LanguageType, string, string, string> GetCurrentTranslator_Code(this LocalizeComponent self)
        {
            return self._Translate_Code;
        }

        public static void PreLoad(this LocalizeComponent self, LanguageType languageType)
        {
            var tmp = LocalizeConfig_Excel_Category.Instance;
            var tmp2 = LocalizeConfig_Code_Category.Instance;
            var tmp3 = LocalizeConfig_UI_Category.Instance;
        }

        public static Func<LanguageType, string, string, string> GetCurrentTranslator_UI(this LocalizeComponent self)
        {
            return self._Translate_UI;
        }

        private static string _Translate_Excel(this LocalizeComponent self, LanguageType languageType, string key, string originText)
        {
            if (string.IsNullOrEmpty(key))
            {
                return string.Empty;
            }
            string str = string.Empty;
            if (LocalizeConfig_Excel_Category.Instance.Contain(key))
            {
                LocalizeConfig localizeConfig = LocalizeConfig_Excel_Category.Instance.GetOrDefault(key);

                switch (languageType)
                {
                    case LanguageType.CN:
                        str = localizeConfig.TextCn;
                        break;
                    case LanguageType.TW:
                        str = localizeConfig.TextTw;
                        break;
                    case LanguageType.EN:
                        str = localizeConfig.TextEn;
                        break;
                    default:
                        str = localizeConfig.TextEn;
                        break;
                }

                self._DecodeString(ref str);
                if (self.IsShowLanguagePre)
                {
                    str = $"[{languageType.ToString()}][Excel]{str}";
                }
            }
            else
            {
                str = $"[Org][Excel]{originText}";
            }
            return str;
        }

        private static string _Translate_Code(this LocalizeComponent self, LanguageType languageType, string key, string originText)
        {
            if (string.IsNullOrEmpty(key))
            {
                return string.Empty;
            }
            string str = string.Empty;
            if (LocalizeConfig_Code_Category.Instance.Contain(key))
            {
                LocalizeConfig localizeConfig = LocalizeConfig_Code_Category.Instance.GetOrDefault(key);

                switch (languageType)
                {
                    case LanguageType.CN:
                        str = localizeConfig.TextCn;
                        break;
                    case LanguageType.TW:
                        str = localizeConfig.TextTw;
                        break;
                    case LanguageType.EN:
                        str = localizeConfig.TextEn;
                        break;
                    default:
                        str = localizeConfig.TextEn;
                        break;
                }
                self._DecodeString(ref str);
                if (self.IsShowLanguagePre)
                {
                    str = $"[{languageType.ToString()}][Code]{str}";
                }
            }
            else
            {
                str = $"[Org][Code]{originText}";
            }
            return str;
        }

        private static string _Translate_UI(this LocalizeComponent self, LanguageType languageType, string key, string originText)
        {
            if (string.IsNullOrEmpty(key))
            {
                return string.Empty;
            }
            string str = string.Empty;
            if (LocalizeConfig_UI_Category.Instance.Contain(key))
            {
                LocalizeConfig localizeConfig = LocalizeConfig_UI_Category.Instance.GetOrDefault(key);

                switch (languageType)
                {
                    case LanguageType.CN:
                        str = localizeConfig.TextCn;
                        break;
                    case LanguageType.TW:
                        str = localizeConfig.TextTw;
                        break;
                    case LanguageType.EN:
                        str = localizeConfig.TextEn;
                        break;
                    default:
                        str = localizeConfig.TextEn;
                        break;
                }
                self._DecodeString(ref str);
                if (self.IsShowLanguagePre)
                {
                    str = $"[{languageType.ToString()}][UI]{str}";
                }
            }
            else
            {
                str = $"[Org][UI]{originText}";
            }
            return str;
        }

        private static void _DecodeString(this LocalizeComponent self, ref string text)
        {
            text = text.Replace("\\n", "\n").Replace("\\t", "\t").Replace("\\r", "\r");
        }
    }
}