namespace ET.Client
{
    [ComponentOf(typeof (UIBaseWindow))]
    public class DlgGameMode: Entity, IAwake, IUILogic
    {
        public DlgGameModeViewComponent View
        {
            get => this.GetComponent<DlgGameModeViewComponent>();
        }
    }
}