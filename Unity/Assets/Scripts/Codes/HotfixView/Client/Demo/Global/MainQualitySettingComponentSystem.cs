using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using ShadowQuality = UnityEngine.ShadowQuality;

namespace ET.Client
{
    [FriendOf(typeof (MainQualitySettingComponent))]
    public static class MainQualitySettingComponentSystem
    {
        [ObjectSystem]
        public class MainQualitySettingComponentAwakeSystem: AwakeSystem<MainQualitySettingComponent>
        {
            protected override void Awake(MainQualitySettingComponent self)
            {
                MainQualitySettingComponent.Instance = self;
                self.Awake().Coroutine();
            }
        }

        [ObjectSystem]
        public class MainQualitySettingComponentDestroySystem: DestroySystem<MainQualitySettingComponent>
        {
            protected override void Destroy(MainQualitySettingComponent self)
            {
                MainQualitySettingComponent.Instance = null;

            }
        }

        [ObjectSystem]
        public class MainQualitySettingComponentUpdateSystem: UpdateSystem<MainQualitySettingComponent>
        {
            protected override void Update(MainQualitySettingComponent self)
            {
                // if (self.UIRoot == null)
                // {
                //     return;
                // }
            }
        }

        public static async ETTask Awake(this MainQualitySettingComponent self)
        {
	        int screenHeight = 0;
            int screenWidth = 0;

            if (PlayerPrefs.HasKey("OrgScreenHeight"))
            {
	            self.orgScreenHeight = PlayerPrefs.GetInt("OrgScreenHeight");
	            self.orgScreenWidth = PlayerPrefs.GetInt("OrgScreenWidth");
	            screenHeight = self.orgScreenHeight;
	            screenWidth = self.orgScreenWidth;
            }
            else
            {
	            screenHeight = Screen.height;
	            screenWidth = Screen.width;
	            if (screenHeight > screenWidth)
	            {
		            int tmp = screenHeight;
		            screenHeight = screenWidth;
		            screenWidth = tmp;
	            }
	            self.orgScreenHeight = screenHeight;
	            self.orgScreenWidth = screenWidth;

	            PlayerPrefs.SetInt("OrgScreenHeight", self.orgScreenHeight);
	            PlayerPrefs.SetInt("OrgScreenWidth", self.orgScreenWidth);
            }

            self.qualityHeightList = new int[self.qualitySettingSize];
            self.qualityWidthList = new int[self.qualitySettingSize];
            self.orgRatio = (float) screenWidth / screenHeight;
            int curMaxHeight = self.maxHeight;
            if (screenHeight < curMaxHeight)
            {
                curMaxHeight = screenHeight;
            }
            if (self.minHeight <= curMaxHeight)
            {
                for (int i = 0; i < self.qualitySettingSize; i++)
                {
	                self.qualityHeightList[i] = (int)(self.minHeight + (float)(curMaxHeight - self.minHeight) / (self.qualitySettingSize - 1) * i);
	                self.qualityWidthList[i] = (int)(self.qualityHeightList[i] * self.orgRatio);
                }
            }
            else
            {
                for (int i = 0; i < self.qualitySettingSize; i++)
                {
	                self.qualityHeightList[i] = screenHeight;
	                self.qualityWidthList[i] = screenWidth;
                }
            }

            self.SetResoution();
            await ETTask.CompletedTask;
        }

	    public static float GetHeightScale(this MainQualitySettingComponent self)
	    {
		    Log.Debug($" --zpb -- Screen ----------------:quality_screen_width({self.quality_screen_width}x{self.quality_screen_height}) Screen.currentResolution({Screen.currentResolution.width}x{Screen.currentResolution.height}) Screen.width({Screen.width}x{Screen.height}) self.orgScreenWidth({self.orgScreenWidth}x{self.orgScreenHeight})");

		    if (self.quality_screen_width > self.quality_screen_height)
		    {
			    return (float) self.orgScreenHeight / self.quality_screen_height;
		    }
		    else
		    {
			    return (float) self.orgScreenHeight / self.quality_screen_width;
		    }
	    }

	    public static void SetResoution(this MainQualitySettingComponent self)
	    {
	        int qualitySetting = 0;
	        Log.Debug("processorType=" + SystemInfo.processorType +
	                  "\nprocessorCount=" + SystemInfo.processorCount +
	                  "\nprocessorFrequency=" + SystemInfo.processorFrequency +
	                  "\ngraphicsMemorySize=" + SystemInfo.graphicsMemorySize +
	                  "\ngraphicsShaderLevel=" + SystemInfo.graphicsShaderLevel +
	                  "\nsystemMemorySize=" + SystemInfo.systemMemorySize +
	                  "\ngraphicsDeviceName=" + SystemInfo.graphicsDeviceName +
	                  "\nprocessorCount=" + SystemInfo.processorCount +
	                  "\nsystemMemorySize=" + SystemInfo.systemMemorySize +
	                  "\ngraphicsDeviceType=" + SystemInfo.graphicsDeviceType);
	        if (PlayerPrefs.GetInt("InitQuality", 0) == 0)
	        {
	#if UNITY_EDITOR
		        int performanceScore = 100;
	#elif UNITY_ANDROID
		        self.GetAndroidScore(out int performanceScore);
	#elif UNITY_IOS
		        self.GetIOSScore(out int performanceScore);
	#else
		        int performanceScore = 100;
	#endif
	            if (performanceScore >= 75)
	                qualitySetting = 3;
	            else if (performanceScore >= 50)
	                qualitySetting = 2;
	            else
	                qualitySetting = 1;

	            Log.Debug("First Login Set QualitySetting:" + qualitySetting + " performanceScore=" + performanceScore);

	            PlayerPrefs.SetInt("QualitySetting", qualitySetting);
	            PlayerPrefs.SetInt("InitQuality", qualitySetting);
	        }

	        self.ForceResetResoution();
	    }

	    public static void GetAndroidScore(this MainQualitySettingComponent self, out int performanceScore)
	    {
		    performanceScore = 0;
		    if (SystemInfo.graphicsDeviceType == UnityEngine.Rendering.GraphicsDeviceType.OpenGLES3)
			    performanceScore += 10;


		    if (SystemInfo.processorFrequency >= 2000)
			    performanceScore += 12;
		    else if (SystemInfo.processorFrequency >= 1800)
			    performanceScore += 8;
		    else
			    performanceScore += 0;


		    if (SystemInfo.graphicsMemorySize >= 2048)
			    performanceScore += 25;
		    else if (SystemInfo.graphicsMemorySize >= 1536)
			    performanceScore += 20;
		    else
			    performanceScore += 0;

		    if (SystemInfo.systemMemorySize >= 6144)
			    performanceScore += 25;
		    else if (SystemInfo.systemMemorySize >= 4096)
			    performanceScore += 20;
		    else
			    performanceScore += 0;


		    if (SystemInfo.graphicsShaderLevel >= 200)
			    performanceScore += 10;
		    else if (SystemInfo.graphicsShaderLevel >= 100)
			    performanceScore += 6;
		    else
			    performanceScore += 0;


		    if (SystemInfo.processorCount >= 8)
			    performanceScore += 10;
		    else if (SystemInfo.processorCount >= 6)
			    performanceScore += 6;
		    else
			    performanceScore += 0;

	    }

	    public static void GetIOSScore(this MainQualitySettingComponent self, out int performanceScore)
	    {
		    performanceScore = 0;

		    string temp = "Apple A10 GPU";
		    temp = temp.Replace ("Apple A", "");
		    temp = temp.Replace (" GPU", "");

		    int gpu_a;
		    if(int.TryParse(temp, out gpu_a))
		    {
			    if(gpu_a >= 12){
				    performanceScore += 40;
			    }else if(gpu_a >= 10){
				    performanceScore += 20;
			    }else{
				    performanceScore += 0;
			    }
		    }
		    if (SystemInfo.graphicsMemorySize >= 800)
			    performanceScore += 25;
		    else if (SystemInfo.graphicsMemorySize >= 500)
			    performanceScore += 20;
		    else
			    performanceScore += 0;

		    if (SystemInfo.systemMemorySize >= 2200)
			    performanceScore += 25;
		    else if (SystemInfo.systemMemorySize >= 1200)
			    performanceScore += 20;
		    else
			    performanceScore += 0;

		    if (SystemInfo.processorCount >= 4)
			    performanceScore += 10;
		    else if (SystemInfo.processorCount >= 3)
			    performanceScore += 6;
		    else
			    performanceScore += 0;
	    }


	    public static void GetQualityScreenSize(this MainQualitySettingComponent self, int qualitySetting)
	    {
		    self.quality_screen_height = self.qualityHeightList[qualitySetting - 1];
		    self.quality_screen_width = self.qualityWidthList[qualitySetting - 1];

		    //self.curRatio = (self.orgScreenWidth - (int)(self.safeAreaOffset * Mathf.Clamp(self.chgLenScale, 0, 100) * 0.01f))/(float)self.orgScreenHeight;

		    //quality_screen_width = (int)(quality_screen_height * self.curRatio);

		    if (Screen.height > Screen.width)
		    {
			    int tmp = self.quality_screen_height;
			    self.quality_screen_height = self.quality_screen_width;
			    self.quality_screen_width = tmp;
		    }
	    }

	    public static void SetResoutionByQuality(this MainQualitySettingComponent self)
	    {
	        if (!self.initQuality)
	        {
	            return;
	        }

	        int qualitySetting = PlayerPrefs.GetInt("QualitySetting", 2);
	        self.ChkQualitySettingLimit(ref qualitySetting);

	        self.GetQualityScreenSize(qualitySetting);

	        bool bFullScreen = Screen.fullScreen;
	        Screen.SetResolution(self.quality_screen_width, self.quality_screen_height, bFullScreen);
	        Log.Debug(qualitySetting
	                  + " Set Screen Pixel-------------------------:" + self.quality_screen_width + "x" + self.quality_screen_height
	                  + "(" + Screen.currentResolution.width + "x" + Screen.currentResolution.height + ")"
	                  + "(" + Screen.width + "x" + Screen.height + ")");
	    }

	    public static void ChkQualitySettingLimit(this MainQualitySettingComponent self, ref int qualitySetting)
	    {
		    // int initQualitySetting = PlayerPrefs.GetInt("InitQuality", 0);
		    // if (initQualitySetting <= qualitySetting)
		    // {
			   //  //分辨率永远不能设置超过，评分时候的分辨率
			   //  qualitySetting = initQualitySetting;
		    // }

		    if (qualitySetting <= 0)
		    {
			    qualitySetting = 1;
		    }

		    if (qualitySetting >= self.qualitySettingSize)
		    {
			    qualitySetting = self.qualitySettingSize;
		    }
	    }

	    public static string GetQualitySetting(this MainQualitySettingComponent self)
	    {
		    int qualitySetting = PlayerPrefs.GetInt("QualitySetting", 0);

		    self.GetQualityScreenSize(qualitySetting);

		    string qualityInfo = qualitySetting + " Set Screen Pixel-------------------------:" + self.quality_screen_width + "x" + self.quality_screen_height + "(" + Screen.currentResolution.width + "x" +
		              Screen.currentResolution.height + ")"+ "(" + Screen.width + "x" +
		              Screen.height + ")";
		    return qualityInfo;
	    }

	    public static void ForceSetWidthScale(this MainQualitySettingComponent self, float chgLenScale)
	    {
		    self.chgLenScale = (int)Mathf.Clamp(chgLenScale, 0, 100);

		    self.ForceResetResoution();
	    }

	    public static void ForceSetQualitySetting(this MainQualitySettingComponent self, int qualitySetting)
	    {
		    self.ChkQualitySettingLimit(ref qualitySetting);
		    PlayerPrefs.SetInt("QualitySetting", qualitySetting);

			QualitySettings.SetQualityLevel(qualitySetting);
			switch (qualitySetting)
	        {
				case 1:
					self.SetQualityShadow(ShadowQuality.Disable);
					break;
				case 2:
					self.SetQualityShadow(ShadowQuality.HardOnly);
					break;
				case 3:
					self.SetQualityShadow(ShadowQuality.All);
					break;
				default:
					self.SetQualityShadow(ShadowQuality.All);
					break;
			}

		    self.ForceResetResoution();
	    }

		public static void SetQualityShadow(this MainQualitySettingComponent self, ShadowQuality shadowQuality)
	    {
			QualitySettings.shadows = shadowQuality;
	    }

	    public static void ForceResetResoution(this MainQualitySettingComponent self)
	    {
		    self.initQuality = true;
		    self.SetResoutionByQuality();
	    }

	    public static void ForceResetScreenOrientation(this MainQualitySettingComponent self)
	    {
		    self.forceResetResoution = true;
		    self.lastHeight = Screen.height;
	    }

	    public static void Update(this MainQualitySettingComponent self)
	    {
		    if (self.forceResetResoution)
		    {
			    if (self.lastHeight != Screen.height)
			    {
				    self.forceResetResoution = false;
				    self.ForceResetResoution();
			    }
		    }
		    if (self.keepChgLenScale)
		    {
			    if (self.lastChgLenScale != self.chgLenScale)
			    {
				    self.lastChgLenScale = self.chgLenScale;
				    self.ForceResetResoution();
			    }
		    }
	    }

	    public static void ChgShadowResolution(this MainQualitySettingComponent self, int shadowResolution)
	    {
		    UniversalRenderPipelineAsset URPAsset = (UniversalRenderPipelineAsset)QualitySettings.renderPipeline;
		    //URPAsset.mainShadowResolution = shadowResolution;
		    // ShadowResolution res = ShadowResolution._2048;
		    // if(shadowResolution == 512)
		    // {
			   //  res = ShadowResolution._512;
		    // }else if(shadowResolution == 1024)
		    // {
			   //  res = ShadowResolution._1024;
		    // }else if(shadowResolution == 4096)
		    // {
			   //  res = ShadowResolution._4096;
		    // }
		    // URPAsset.mainLightShadowmapResolution = res;
	    }

	    public static void ChgShadowDistance(this MainQualitySettingComponent self, int shadowDistance)
	    {
		    UniversalRenderPipelineAsset URPAsset = (UniversalRenderPipelineAsset)QualitySettings.renderPipeline;
		    URPAsset.shadowDistance = shadowDistance;
	    }
    }
}