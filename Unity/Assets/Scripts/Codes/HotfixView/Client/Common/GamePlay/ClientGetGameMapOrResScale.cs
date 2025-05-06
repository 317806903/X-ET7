namespace ET.Client
{
    [Invoke]
    public class ClientGetGameMapOrResScale: AInvokeHandler<ET.GetGameMapOrResScale, float>
    {
        public override float Handle(ET.GetGameMapOrResScale args)
        {
            bool isGetMapScale = args.isGetMapScale;
            Scene scene = args.scene;
            bool isClient = args.isClient;

            ClientResScaleType clientResScaleType = GlobalConfig.Instance.clientResScaleType;
#if UNITY_EDITOR
#elif Platform_Mobile
            clientResScaleType = ClientResScaleType.ResOrgAndCameraScale;
#elif Platform_Quest
            clientResScaleType = ClientResScaleType.ResScaleAndCameraOrg;
#elif Platform_AVP
            clientResScaleType = ClientResScaleType.ResScaleAndCameraOrg;
#else
#endif
            if (isGetMapScale)
            {
                if (isClient)
                {
                    if (clientResScaleType == ClientResScaleType.ResOrgAndCameraScale)
                    {
                        if (scene == null)
                        {
                            Log.Error($"--- ClientGetGameMapOrResScale scene == null");
                            return 1;
                        }
                        GamePlayComponent gamePlayComponent = ET.GamePlayHelper.GetGamePlay(scene);
                        if (gamePlayComponent == null)
                        {
                            PlayerStatusComponent playerStatusComponent = ET.Client.PlayerStatusHelper.GetMyPlayerStatusComponent(scene);
                            if (playerStatusComponent.MapScale <= 0)
                            {
                                Log.Error($"--- ClientGetGameMapOrResScale gamePlayComponent == null and playerStatusComponent.MapScale[{playerStatusComponent.MapScale}]");
                            }
                            return playerStatusComponent.MapScale;
                        }
                        float mapScaleValue = gamePlayComponent.GetGameMapScale();
                        return mapScaleValue;
                    }
                    else if (clientResScaleType == ClientResScaleType.ResScaleAndCameraOrg)
                    {
                        return 1;
                    }
                    else
                    {
                        Log.Error($"---Error clientResScaleType == null");
                        return 1;
                    }
                }
                else
                {
                    if (scene == null)
                    {
                        Log.Error($"--- ClientGetGameMapOrResScale scene == null");
                        return 1;
                    }
                    GamePlayComponent gamePlayComponent = ET.GamePlayHelper.GetGamePlay(scene);
                    if (gamePlayComponent == null)
                    {
                        Log.Error($"--- ClientGetGameMapOrResScale gamePlayComponent == null");
                        return 1;
                    }
                    float mapScaleValue = gamePlayComponent.GetGameMapScale();
                    return mapScaleValue;
                }
            }
            else
            {
                if (isClient)
                {
                    if (clientResScaleType == ClientResScaleType.ResOrgAndCameraScale)
                    {
                        return 1;
                    }
                    else if (clientResScaleType == ClientResScaleType.ResScaleAndCameraOrg)
                    {
                        if (scene == null)
                        {
                            Log.Error($"--- ClientGetGameMapOrResScale scene == null");
                            return 1;
                        }
                        GamePlayComponent gamePlayComponent = ET.GamePlayHelper.GetGamePlay(scene);
                        if (gamePlayComponent == null)
                        {
                            PlayerStatusComponent playerStatusComponent = ET.Client.PlayerStatusHelper.GetMyPlayerStatusComponent(scene);
                            if (playerStatusComponent.MapScale <= 0)
                            {
                                Log.Error($"--- ClientGetGameMapOrResScale gamePlayComponent == null and playerStatusComponent.MapScale[{playerStatusComponent.MapScale}]");
                            }
                            return 1 / playerStatusComponent.MapScale;
                        }
                        float mapScaleValue = gamePlayComponent.GetGameMapScale();
                        return 1 / mapScaleValue;
                    }
                    else
                    {
                        Log.Error($"---Error clientResScaleType == null");
                        return 1;
                    }
                }
                else
                {
                    return 1;
                }
            }
        }
    }
}