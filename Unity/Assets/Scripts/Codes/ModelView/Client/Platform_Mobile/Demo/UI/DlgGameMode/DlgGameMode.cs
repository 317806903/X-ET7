namespace ET.Client
{
    [ComponentOf(typeof (UIBaseWindow))]
    public class DlgGameMode: Entity, IAwake, IUILogic, IUIDlg
    {
        public UnityEngine.Transform GetUITransform { get => View.uiTransform; }
        public DlgGameModeViewComponent View
        {
            get => this.GetComponent<DlgGameModeViewComponent>();
        }
    }
}