namespace ET
{
    [ConsoleHandler(ConsoleMode.ReloadDll)]
    public class ReloadDllConsoleHandler: IConsoleHandler
    {
        public async ETTask Run(ModeContex contex, string content)
        {
            switch (content)
            {
                case ConsoleMode.ReloadDll:
                    contex.Parent.RemoveComponent<ModeContex>();

                    ET.CodeLoader.Instance.LoadHotfix();

                    EventSystem.Instance.Load();
                    break;
            }

            await ETTask.CompletedTask;
        }
    }
}