using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Reflection;
using TATools.MatchingRules;


namespace TATools.TextureFormatSetting
{
    public enum TextureCatalog
    {
        NotMatch = -1,
        NotDeal,

        #region  Character
        // PlayerCharcter
        CharacterM1RGBA_PNG,
        CharacterM2RGBA_PNG,
        CharacterNormal_PNG,
        CharacterTN_PNG,
        CharDiffuseRGBA_PNG,
        CharacterCommonRGBA_PNG,
        CharacterCommonRGB_PNG,
        CharacterM2RGB_PNG,
        CharacterM1RGB_PNG,
        CharDiffuseRGB_PNG,

        // FBody
        FBodyCharacterCommonRGBA_PNG,
        FBodyCharacterM1RGBA_PNG,
        FBodyCharacterM2RGBA_PNG,
        FBodyCharacterNormal_PNG,
        FBodyCharacterTN_PNG,
        FBodyCharacterAlpha_PNG,
        FBodyCharacterDiffuseRGBA_PNG,
        FBodyCharacterDiffuseRGB_PNG,
        FBodyCharacterM2RGB_PNG,
        FBodyCharacterM1RGB_PNG,
        FBodyCharacterCommonRGB_PNG,

        #endregion

        #region LightMap
        CenterCityLightmap,
        CommonLightMap,
        CommonDirLightMap,
        CommonShadowMaskLightMap,
        #endregion

        #region Cube
        CommonCube_EXR,
        Cube256_EXR,
        CommonCube_PNG,
        CommonCube_TGA,
        #endregion

        #region Enverionment
        // Effect
        EnvEffectRGBA_PNG,
        EnvEffectRGB_PNG,
        EnvCommonRGBA_PNG,
        EnvCommonRGB_PNG,
        #endregion

        #region Effect
        // UIEffect
        UIEffectRGBA_SupportETC2,

        // HD
        HDCommonEffectRGB_SupportETC,
        HDCommonEffectRGBA_SupportETC2,

        // Others
        EffectNormalMap,
        CommonEffectRGB_SupportETC,
        CommonEffectRGBA_SupportETC2,
        CommonEffectRGBA_NoETC2,
        CommonEffectRGB_NoETC,
        #endregion

        #region UI
        UIFullScreenBackgroundRGB,
        UIFullScreenBackgroundRGBA,
        UILodingScreenRGB,
        UIFullScreenSpriteRGB,
        UIFullScreenSpriteRGBA,
        UIPlotCharacterSpriteRGBA,
        UIAltasSpriteRGBA,
        UIDynamicSpriteRGBA,
        UISDFRBGA,
        #endregion

        #region Spine
        SpineJuqingRGBA_PNG,
        SpineJuqingRGB_PNG,
        SpineUIRGBA_PNG,
        SpineUIRGB_PNG,

        #endregion

        #region Common
        RGB24_PNG,
        CommonRGB_PNG,
        CommonRGBA_PNG,
        LUT_PNG,
        CommonEXR,

        #endregion

    }
    public class TextureCatalogHelper
    {
        public MatchingRule matchingRule;
        public string texturePath;
        public TextureCatalog textureCatalog
        {
            get
            {
                return (TextureCatalog)matchingRule.dealType;
            }
        }
        public int maxSize;
        public string maxSizeErr;

        public TextureCatalogHelper(string path, TextureImporter textureImporter)
        {
            this.texturePath = path;
            bool hasAlpha = textureImporter.DoesSourceTextureHaveAlpha();
            /// 使用反射的方法获得贴图的宽 和  高 判断etc
            (int width, int height) = GetTextureImporterSize(textureImporter);
            //Debug.Log("Tex Width: " + width + "; Height: " + height);
            bool etcSupported = (width % 4 == 0 && height % 4 == 0);
            this.matchingRule = GetTextureCatalog(path, etcSupported, hasAlpha);
            this.maxSize = this.matchingRule.maxSize;
            if (this.maxSize > 0)
            {
                if (width > maxSize || height > maxSize)
                {
                    this.maxSizeErr = $"{textureImporter.assetPath} {width}x{height} > maxSize={maxSize}";
                    do
                    {
                        maxSize *= 2;
                    } while (width > maxSize || height > maxSize);
                }
            }
        }


        /// <summary>
        /// 反射拿到贴图的宽 和 高
        /// </summary>
        /// <param name="importer"></param>
        /// <returns></returns>
        public static (int, int) GetTextureImporterSize(TextureImporter importer)
        {
            if (importer != null)
            {
                object[] args = new object[2];
                MethodInfo mi = typeof(TextureImporter).GetMethod("GetWidthAndHeight", BindingFlags.NonPublic | BindingFlags.Instance);
                mi.Invoke(importer, args);
                return ((int)args[0], (int)args[1]);
            }
            return (0, 0);
        }

        public static MatchingRule GetTextureCatalog(string path, bool etcSupported, bool hasAlpha = true)
        {
            return MatchingRuleMgr.DealMatching(path, MatchingTextureRuleMgr.Instance.MatchingTextureRulesList);
        }

    }

}