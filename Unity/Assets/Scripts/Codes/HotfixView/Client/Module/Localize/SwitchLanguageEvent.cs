using ET.EventType;

namespace ET.Client
{
    [Event(SceneType.Client | SceneType.Process)]
    public class SwitchLanguageEvent : AEvent<Scene, SwitchLanguage>
    {
        protected override async ETTask Run(Scene scene, SwitchLanguage arg)
        {
            var translateUI = LocalizeComponent.Instance.GetCurrentTranslator_UI(arg.languageType);

            UITextLocalizeComponent.Instance?.SetGetTextKeyValueActionBack((languageType) =>
            {
                if (languageType == "CN")
                {
                    return LocalizeComponent.Instance.GetCurrentTranslator_UI(LanguageType.CN);
                }
                else if (languageType == "TW")
                {
                    return LocalizeComponent.Instance.GetCurrentTranslator_UI(LanguageType.TW);
                }
                else if (languageType == "EN")
                {
                    return LocalizeComponent.Instance.GetCurrentTranslator_UI(LanguageType.EN);
                }
                return LocalizeComponent.Instance.GetCurrentTranslator_UI(LanguageType.CN);
            });

            UITextLocalizeComponent.Instance?.SetTextLocalizeAction(translateUI);
            UITextLocalizeComponent.Instance?.DoRefreshTextValue();

            UnityEngine.UI.LoopScrollPrefabSourceInstance.AddCellItemAction = (go) =>
            {
                UITextLocalizeComponent.Instance.AddUITextLocalizeView(go);
            };

            UnityEngine.UI.LoopScrollPrefabSourceInstance.RemoveCellItemAction = (go) =>
            {
                UITextLocalizeComponent.Instance.RemoveUITextLocalizeView(go);
            };

            await ETTask.CompletedTask;
        }
    }
}