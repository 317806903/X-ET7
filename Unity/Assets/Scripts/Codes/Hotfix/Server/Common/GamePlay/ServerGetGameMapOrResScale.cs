namespace ET.Server
{
    [Invoke]
    public class ServerGetGameMapOrResScale: AInvokeHandler<ET.GetGameMapOrResScale, float>
    {
        public override float Handle(ET.GetGameMapOrResScale args)
        {
            bool isGetMapScale = args.isGetMapScale;
            Scene scene = args.scene;
            bool isClient = args.isClient;

            if (isGetMapScale == false)
            {
                return 1;
            }
            if (isClient)
            {
                Log.Error($"--- ServerGetGameMapOrResScale isClient == true");
                return 1;
            }
            if (scene == null)
            {
                Log.Error($"--- ServerGetGameMapOrResScale scene == null");
                return 1;
            }
            GamePlayComponent gamePlayComponent = ET.GamePlayHelper.GetGamePlay(scene);
            if (gamePlayComponent == null)
            {
                Log.Error($"--- ServerGetGameMapOrResScale gamePlayComponent == null");
                return 1;
            }
            float mapScaleValue = gamePlayComponent.GetGameMapScale();
            return mapScaleValue;
#if DOTNET
#endif
        }
    }
}