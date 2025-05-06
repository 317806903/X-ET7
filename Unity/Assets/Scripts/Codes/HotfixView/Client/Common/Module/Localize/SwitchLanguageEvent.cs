namespace ET.Client
{
    [Event(SceneType.Client | SceneType.Process)]
    public class SwitchLanguageEvent : AEvent<Scene, ClientEventType.SwitchLanguage>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.SwitchLanguage arg)
        {
            UITextLocalizeComponent.Instance?.DoRefreshTextValue();

            UIImageLocalizeComponent.Instance?.DoRefreshImageValue();

            UnityEngine.UI.LoopScrollPrefabSourceInstance.AddCellItemAction = (go) =>
            {
                UITextLocalizeComponent.Instance.AddUITextLocalizeView(go);
                UIImageLocalizeComponent.Instance.AddUIImageLocalizeView(go);
            };

            UnityEngine.UI.LoopScrollPrefabSourceInstance.RemoveCellItemAction = (go) =>
            {
                UITextLocalizeComponent.Instance.RemoveUITextLocalizeView(go);
                UIImageLocalizeComponent.Instance.RemoveUIImageLocalizeView(go);
            };

            await ETTask.CompletedTask;
        }
    }
}