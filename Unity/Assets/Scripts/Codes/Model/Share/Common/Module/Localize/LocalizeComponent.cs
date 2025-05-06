using System;
using System.Text;

namespace ET
{
    public enum LanguageType
    {
        Auto,
        CN,
        TW,
        EN,
    }

    [ComponentOf(typeof(Scene))]
    public class LocalizeComponent : Entity, IAwake, IDestroy
    {
        public static LocalizeComponent Instance { get; set; }

        public bool IsShowLanguagePre = false;
        public LanguageType CurrentLanguage { get; set; }
    }
}