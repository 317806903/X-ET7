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
                self.translateExcel = null;
                self.translateUI = null;
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

            self.translateExcel = self.GetCurrentTranslator_Excel(self.CurrentLanguage);
            self.translateUI = self.GetCurrentTranslator_UI(self.CurrentLanguage);

            self.PreLoad(self.CurrentLanguage);

            ConfigComponent.Instance.TranslateText(self.translateExcel);

            EventSystem.Instance.Publish(self.DomainScene(), new EventType.SwitchLanguage() {languageType = language});

            return;
        }

        public static void ResetLanguage(this LocalizeComponent self)
        {
            self.translateExcel = self.GetCurrentTranslator_Excel(self.CurrentLanguage);
            self.translateUI = self.GetCurrentTranslator_UI(self.CurrentLanguage);

            ConfigComponent.Instance.TranslateText(self.translateExcel);

            EventSystem.Instance.Publish(self.DomainScene(), new EventType.SwitchLanguage() {languageType = self.CurrentLanguage});

            return;
        }

        public static string GetTextValue(this LocalizeComponent self, string textKey)
        {
            var translateExcel = self.GetCurrentTranslator_Excel(self.CurrentLanguage);
            string textValue = translateExcel(textKey, textKey);
            return textValue;
        }

        public static string GetTextValue(this LocalizeComponent self, LanguageType languageType, string textKey)
        {
            var translateExcel = self.GetCurrentTranslator_Excel(languageType);
            string textValue = translateExcel(textKey, textKey);
            return textValue;
        }

        public static string GetTextValue(this LocalizeComponent self, string textKey, params object[] args)
        {
            var translateExcel = self.GetCurrentTranslator_Excel(self.CurrentLanguage);
            string textValue = translateExcel(textKey, textKey);
            textValue = string.Format(textValue, args);
            return textValue;
        }

        public static string GetTextValue(this LocalizeComponent self, LanguageType languageType, string textKey, params object[] args)
        {
            var translateExcel = self.GetCurrentTranslator_Excel(languageType);
            string textValue = translateExcel(textKey, textKey);
            textValue = string.Format(textValue, args);
            return textValue;
        }

        public static Func<string, string, string> GetCurrentTranslator_Excel(this LocalizeComponent self, LanguageType languageType)
        {
            switch (languageType)
            {
                case LanguageType.CN:
                    return self._Translate_Excel_CN;

                case LanguageType.TW:
                    return self._Translate_Excel_TW;

                case LanguageType.EN:
                default:
                    return self._Translate_Excel_EN;
            }
        }

        public static void PreLoad(this LocalizeComponent self, LanguageType languageType)
        {
            switch (languageType)
            {
                case LanguageType.CN:
                    self._PreLoad_CN();
                    return;
                case LanguageType.TW:
                    self._PreLoad_TW();
                    return;
                case LanguageType.EN:
                default:
                    self._PreLoad_EN();
                    return;
            }
        }

        public static Func<string, string, string> GetCurrentTranslator_UI(this LocalizeComponent self, LanguageType languageType)
        {
            switch (languageType)
            {
                case LanguageType.CN:
                    return self._Translate_UI_CN;

                case LanguageType.TW:
                    return self._Translate_UI_TW;

                case LanguageType.EN:
                default:
                    return self._Translate_UI_EN;
            }
        }

        private static void _PreLoad_CN(this LocalizeComponent self)
        {
            var tmp = LocalizeConfig_Excel_CNCategory.Instance;
        }

        private static void _PreLoad_TW(this LocalizeComponent self)
        {
            var tmp = LocalizeConfig_Excel_TWCategory.Instance;
        }

        private static void _PreLoad_EN(this LocalizeComponent self)
        {
            var tmp = LocalizeConfig_Excel_ENCategory.Instance;
        }

        private static string _Translate_Excel_CN(this LocalizeComponent self, string key, string originText)
        {
            return LocalizeConfig_Excel_CNCategory.Instance.GetOrDefault(key)?.TextCn ?? originText;
        }

        private static string _Translate_Excel_TW(this LocalizeComponent self, string key, string originText)
        {
            return LocalizeConfig_Excel_TWCategory.Instance.GetOrDefault(key)?.TextTw ?? originText;
        }

        private static string _Translate_Excel_EN(this LocalizeComponent self, string key, string originText)
        {
            return LocalizeConfig_Excel_ENCategory.Instance.GetOrDefault(key)?.TextEn ?? originText;
        }

        private static string _Translate_UI_CN(this LocalizeComponent self, string key, string originText)
        {
            string str = LocalizeConfig_UI_CNCategory.Instance.GetOrDefault(key)?.TextCn ?? originText;
            str = _DecodeString(str);
            return str;
        }

        private static string _Translate_UI_TW(this LocalizeComponent self, string key, string originText)
        {
            string str = LocalizeConfig_UI_TWCategory.Instance.GetOrDefault(key)?.TextTw ?? originText;
            str = _DecodeString(str);
            return str;
        }

        private static string _Translate_UI_EN(this LocalizeComponent self, string key, string originText)
        {
            string str = LocalizeConfig_UI_ENCategory.Instance.GetOrDefault(key)?.TextEn ?? originText;
            str = _DecodeString(str);
            return str;
        }

        private static string _DecodeString(string text)
        {
            StringBuilder sb = new StringBuilder(text);
            sb.Replace("\\n", "\n");
            sb.Replace("\\t", "\t");
            sb.Replace("\\r", "\r");
            return sb.ToString();
        }
    }
}