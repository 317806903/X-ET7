using System;

namespace ET
{
    public enum LanguageType
    {
        CN,
        TW,
        EN,
    }

    [ComponentOf(typeof(Scene))]
    public class LocalizeComponent : Entity, IAwake, IDestroy
    {
        public static LocalizeComponent Instance { get; set; }

        public LanguageType CurrentLanguage { get; set; }
        public Func<string, string, string> translateExcel;
        public Func<string, string, string> translateUI;
    }
}