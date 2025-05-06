using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.ComponentModel;
using UnityEditor;
using UnityEngine.Bindings;

namespace TATools.TextureFormatSetting
{
    public class TextureImportSettingTemplate
    {
        //public TextureImportSettingTemplate();
        public TextureImporter textureImporter { get; set; }
        public TextureImporterPlatformSettings androidSetting { get; set; }
        public TextureImporterPlatformSettings iphoneSetting { get; set; }
        public TextureImporterPlatformSettings standaloneSetting { get; set; }

        public TextureImportSettingTemplate(TextureImporter textureImporter, int maxSize, TextureImporterFormat standaloneFormat, TextureImporterFormat androidFormat, TextureImporterFormat iphoneFormat)
        {
            this.androidSetting = new TextureImporterPlatformSettings();
            this.iphoneSetting = new TextureImporterPlatformSettings();
            this.standaloneSetting = new TextureImporterPlatformSettings();
            this.textureImporter = textureImporter;
            this.textureImporter.maxTextureSize = maxSize;


            this.androidSetting.name = "Android";
            this.androidSetting.overridden = true;
            this.androidSetting.compressionQuality = 100;
            this.androidSetting.format = androidFormat;
            this.androidSetting.maxTextureSize = maxSize;
            this.androidSetting.androidETC2FallbackOverride = AndroidETC2FallbackOverride.UseBuildSettings;
            this.androidSetting.resizeAlgorithm = TextureResizeAlgorithm.Mitchell;

            this.iphoneSetting.name = "iPhone";
            this.iphoneSetting.overridden = true;
            this.iphoneSetting.compressionQuality = 100;
            this.iphoneSetting.format = iphoneFormat;
            this.iphoneSetting.maxTextureSize = maxSize;
            this.iphoneSetting.androidETC2FallbackOverride = AndroidETC2FallbackOverride.UseBuildSettings;
            this.iphoneSetting.resizeAlgorithm = TextureResizeAlgorithm.Mitchell;

            this.standaloneSetting.name = "Standalone";
            this.standaloneSetting.overridden = true;
            this.standaloneSetting.compressionQuality = 100;
            this.standaloneSetting.format = standaloneFormat;
            this.standaloneSetting.maxTextureSize = maxSize;
            this.standaloneSetting.androidETC2FallbackOverride = AndroidETC2FallbackOverride.UseBuildSettings;
            this.standaloneSetting.resizeAlgorithm = TextureResizeAlgorithm.Mitchell;

        }

    }

    public class TextureImportSettingTemplateHelper
    {
        public TextureImportSettingTemplate template;
        public TextureImportSettingTemplateHelper(TextureCatalog textureCatalog, int maxSize, TextureImporter textureImporter)
        {
            this.template = GetImportSettingTemplate(textureCatalog, maxSize, textureImporter);
        }

        const bool _MipmapEnabled = true;
        const bool _IssRGBTexture = true;
        const bool _IsReadable = true;
        const bool _MipMapDisabled = false;
        const bool _NotsRGBTexture = false;
        const bool _NotReadable = false;
        const bool _AlphaNotTransparency = false;
        const int _TexSize_128 = 128;
        const int _TexSize_256 = 256;
        const int _TexSize_512 = 512;
        const int _TexSize_1024 = 1024;
        const int _TexSize_2048 = 2048;
        const int _TexSize_4096 = 4096;
        const int _AnisoLevel_0 = 0;
        const int _AnisoLevel_1 = 1;
        const int _AnisoLevel_2 = 2;

        const bool _StreamingMipMapEnabled = true;
        const int _StreamingMipMapPriority_0 = 0;
        const bool _StreamingMipMapDisabled = false;

        const TextureImporterType defaultTex = TextureImporterType.Default;
        const TextureImporterType normalMap = TextureImporterType.NormalMap;
        const TextureImporterType lightmap = TextureImporterType.Lightmap;
        const TextureImporterType sprite = TextureImporterType.Sprite;

        const TextureImporterShape tex_2D = TextureImporterShape.Texture2D;
        const TextureImporterShape tex_Cube = TextureImporterShape.TextureCube;

        const TextureImporterMipFilter kaiserFilter = TextureImporterMipFilter.KaiserFilter;
        const TextureImporterMipFilter boxFilter = TextureImporterMipFilter.BoxFilter;

        const FilterMode pointFilter = FilterMode.Point;

        const TextureImporterFormat rgb24 = TextureImporterFormat.RGB24;
        const TextureImporterFormat rgba32 = TextureImporterFormat.RGBA32;
        const TextureImporterFormat dxt1 = TextureImporterFormat.DXT1;
        const TextureImporterFormat dxt5 = TextureImporterFormat.DXT5;
        const TextureImporterFormat rgbaHalf = TextureImporterFormat.RGBAHalf;
        const TextureImporterFormat astcRgb_4 = TextureImporterFormat.ASTC_4x4;
        const TextureImporterFormat astcRgb_5 = TextureImporterFormat.ASTC_5x5;
        const TextureImporterFormat astcRgb_6 = TextureImporterFormat.ASTC_6x6;
        const TextureImporterFormat astcRgba_4 = TextureImporterFormat.ASTC_4x4;
        const TextureImporterFormat astcRgba_5 = TextureImporterFormat.ASTC_5x5;
        const TextureImporterFormat astcRgba_6 = TextureImporterFormat.ASTC_6x6;
        const TextureImporterFormat etc_rgb_4 = TextureImporterFormat.ETC_RGB4;
        const TextureImporterFormat etc_rgb4_cru = TextureImporterFormat.ETC_RGB4Crunched;
        const TextureImporterFormat etc2_rgba8 = TextureImporterFormat.ETC2_RGBA8;
        const TextureImporterFormat etc2_rgba8_cru = TextureImporterFormat.ETC2_RGBA8Crunched;
        const TextureImporterFormat pvrtc_rgb4 = TextureImporterFormat.PVRTC_RGB4;
        const TextureImporterFormat pvrtc_rgba4 = TextureImporterFormat.PVRTC_RGBA4;

        const TextureImporterNPOTScale npot_Nearest = TextureImporterNPOTScale.ToNearest;



        TextureImportSettingTemplate GetImportSettingTemplate(TextureCatalog textureCatalog, int maxSize, TextureImporter textureImporter)
        {
            switch (textureCatalog)
            {
                case TextureCatalog.NotMatch:
                    return null;
                case TextureCatalog.NotDeal:
                    return null;
            }

            TextureImportSettingTemplate temp;
            switch (textureCatalog)
            {
                case TextureCatalog.LUT_PNG:
                    if (maxSize == 0)
                    {
                        maxSize = _TexSize_1024;
                    }
                    temp = new TextureImportSettingTemplate(textureImporter, maxSize, dxt1, astcRgb_4, astcRgb_4);
                    SetTextureImpoterGeneralProperties(temp);
                    SetTextureImpoterMipMapProperties(temp, _MipMapDisabled);
                    break;

                //主要是matcap 和 1张 lut 1张 ramp； 这个格式；放弃命名中的rgb24的说法
                case TextureCatalog.RGB24_PNG:
                    if (maxSize == 0)
                    {
                        maxSize = _TexSize_512;
                    }
                    temp = new TextureImportSettingTemplate(textureImporter, maxSize, dxt1, astcRgb_4, astcRgb_4);
                    SetTextureImpoterGeneralProperties(temp);
                    SetTextureImpoterMipMapProperties(temp, _MipMapDisabled);
                    break;

                case TextureCatalog.CommonCube_PNG:
                case TextureCatalog.CommonCube_TGA:
                case TextureCatalog.CommonCube_EXR:
                case TextureCatalog.CommonEXR:
                    if (maxSize == 0)
                    {
                        maxSize = _TexSize_128;
                    }
                    temp = new TextureImportSettingTemplate(textureImporter, maxSize, rgbaHalf, rgbaHalf, rgbaHalf);
                    SetTextureImpoterGeneralProperties(temp, _NotsRGBTexture, defaultTex, tex_Cube);
                    SetTextureImpoterMipMapProperties(temp);
                    SetTextureImpoterCubeMappingProperties(temp);
                    break;

                case TextureCatalog.Cube256_EXR:
                    if (maxSize == 0)
                    {
                        maxSize = _TexSize_256;
                    }
                    temp = new TextureImportSettingTemplate(textureImporter, maxSize, rgbaHalf, rgbaHalf, rgbaHalf);
                    SetTextureImpoterGeneralProperties(temp, _NotsRGBTexture, defaultTex, tex_Cube);
                    SetTextureImpoterMipMapProperties(temp);
                    SetTextureImpoterCubeMappingProperties(temp);
                    break;

                //Aug 26, 角色和车 主要贴图分辨率压一半（车玻璃，车和轮胎的diffuse，角色ramp，matcap除外）
                //Dec 21th, 2020 ZBJ FBody的精度恢复成1024
                //FBody Char part
                //diffuse贴图设置为 Isreadable 便于低模贴图合并
                //Jan 22, 2021 ZBJ 人的贴图(除了Isreadable贴图)开启 mip map steaming
                case TextureCatalog.FBodyCharacterDiffuseRGBA_PNG:
                    if (maxSize == 0)
                    {
                        maxSize = _TexSize_1024;
                    }
                    temp = new TextureImportSettingTemplate(textureImporter, maxSize, dxt5, etc2_rgba8_cru, pvrtc_rgba4);
                    SetTextureImpoterGeneralProperties(temp, _IssRGBTexture, defaultTex, tex_2D, _IsReadable);
                    SetTextureImpoterMipMapProperties(temp);
                    SetTextureImpoterAlphaProperties(temp, _AlphaNotTransparency);
                    break;

                case TextureCatalog.FBodyCharacterDiffuseRGB_PNG:
                    if (maxSize == 0)
                    {
                        maxSize = _TexSize_1024;
                    }
                    temp = new TextureImportSettingTemplate(textureImporter, maxSize, dxt1, etc_rgb4_cru, pvrtc_rgb4);
                    SetTextureImpoterGeneralProperties(temp, _IssRGBTexture, defaultTex, tex_2D, _IsReadable);
                    SetTextureImpoterMipMapProperties(temp);
                    break;

                case TextureCatalog.FBodyCharacterM1RGBA_PNG:
                case TextureCatalog.FBodyCharacterM2RGBA_PNG:
                    if (maxSize == 0)
                    {
                        maxSize = _TexSize_1024;
                    }
                    temp = new TextureImportSettingTemplate(textureImporter, maxSize, dxt5, astcRgba_6, astcRgba_6);
                    SetTextureImpoterGeneralProperties(temp, _NotsRGBTexture);
                    SetTextureImpoterMipMapProperties(temp);
                    SetTextureImpoterAlphaProperties(temp, _AlphaNotTransparency);
                    SetTextureImpoterSteamingMipMapProperties(temp, _StreamingMipMapEnabled);
                    break;

                case TextureCatalog.FBodyCharacterM1RGB_PNG:
                case TextureCatalog.FBodyCharacterM2RGB_PNG:
                    if (maxSize == 0)
                    {
                        maxSize = _TexSize_1024;
                    }
                    temp = new TextureImportSettingTemplate(textureImporter, maxSize, dxt1, astcRgb_6, astcRgb_6);
                    SetTextureImpoterGeneralProperties(temp, _NotsRGBTexture);
                    SetTextureImpoterMipMapProperties(temp);
                    SetTextureImpoterSteamingMipMapProperties(temp, _StreamingMipMapEnabled);
                    break;

                //
                case TextureCatalog.FBodyCharacterNormal_PNG:
                    if (maxSize == 0)
                    {
                        maxSize = _TexSize_1024;
                    }
                    temp = new TextureImportSettingTemplate(textureImporter, maxSize, dxt1, astcRgb_6, astcRgb_6);
                    SetTextureImpoterGeneralProperties(temp, _NotsRGBTexture, normalMap);
                    SetTextureImpoterMipMapProperties(temp);
                    SetTextureImpoterSteamingMipMapProperties(temp, _StreamingMipMapEnabled);
                    break;

                //FBody 头发的TN 也应该是512； 另外一个因素，FBody不应该有单独的TN。。。
                //Jan 09, 2021 ZBJ 这里设置回_TexSize_512
                case TextureCatalog.FBodyCharacterTN_PNG:
                    if (maxSize == 0)
                    {
                        maxSize = _TexSize_512;
                    }
                    temp = new TextureImportSettingTemplate(textureImporter, maxSize, dxt1, astcRgb_6, astcRgb_6);
                    SetTextureImpoterGeneralProperties(temp, _NotsRGBTexture, normalMap);
                    SetTextureImpoterMipMapProperties(temp);
                    SetTextureImpoterSteamingMipMapProperties(temp, _StreamingMipMapEnabled);
                    break;

                case TextureCatalog.FBodyCharacterAlpha_PNG:
                    if (maxSize == 0)
                    {
                        maxSize = _TexSize_1024;
                    }
                    temp = new TextureImportSettingTemplate(textureImporter, maxSize, dxt1, etc_rgb4_cru, pvrtc_rgb4);
                    SetTextureImpoterGeneralProperties(temp);
                    SetTextureImpoterMipMapProperties(temp);
                    SetTextureImpoterSteamingMipMapProperties(temp, _StreamingMipMapEnabled);
                    break;

                case TextureCatalog.FBodyCharacterCommonRGBA_PNG:
                    if (maxSize == 0)
                    {
                        maxSize = _TexSize_256;
                    }
                    temp = new TextureImportSettingTemplate(textureImporter, maxSize, dxt5, etc2_rgba8_cru, pvrtc_rgba4);
                    SetTextureImpoterGeneralProperties(temp);
                    SetTextureImpoterMipMapProperties(temp);
                    SetTextureImpoterAlphaProperties(temp);
                    SetTextureImpoterSteamingMipMapProperties(temp, _StreamingMipMapEnabled);
                    break;

                case TextureCatalog.FBodyCharacterCommonRGB_PNG:
                    if (maxSize == 0)
                    {
                        maxSize = _TexSize_256;
                    }
                    temp = new TextureImportSettingTemplate(textureImporter, maxSize, dxt1, etc_rgb4_cru, pvrtc_rgb4);
                    SetTextureImpoterGeneralProperties(temp);
                    SetTextureImpoterMipMapProperties(temp);
                    SetTextureImpoterSteamingMipMapProperties(temp, _StreamingMipMapEnabled);
                    break;

                //Char, Foreword Char, npc, pet 文件夹下的
                //diffuse贴图设置为 Isreadable 便于低模贴图合并
                //Jan 26, 2021 ZBJ, NPC和Pet Diffuse贴图取消 Read/Write, 开启streaming mimap
                case TextureCatalog.CharDiffuseRGBA_PNG:
                    if (maxSize == 0)
                    {
                        maxSize = _TexSize_256;
                    }
                    temp = new TextureImportSettingTemplate(textureImporter, maxSize, dxt5, etc2_rgba8_cru, pvrtc_rgba4);
                    SetTextureImpoterGeneralProperties(temp, _IssRGBTexture, defaultTex, tex_2D, _IsReadable);
                    SetTextureImpoterMipMapProperties(temp);
                    SetTextureImpoterAlphaProperties(temp, _AlphaNotTransparency);
                    break;

                case TextureCatalog.CharDiffuseRGB_PNG:
                    if (maxSize == 0)
                    {
                        maxSize = _TexSize_256;
                    }
                    temp = new TextureImportSettingTemplate(textureImporter, maxSize, dxt1, etc_rgb4_cru, pvrtc_rgb4);
                    SetTextureImpoterGeneralProperties(temp, _IssRGBTexture, defaultTex, tex_2D, _IsReadable);
                    SetTextureImpoterMipMapProperties(temp);

                    break;
                case TextureCatalog.CharacterM1RGBA_PNG:
                case TextureCatalog.CharacterM2RGBA_PNG:
                    if (maxSize == 0)
                    {
                        maxSize = _TexSize_256;
                    }
                    temp = new TextureImportSettingTemplate(textureImporter, maxSize, dxt5, astcRgba_6, astcRgba_6);
                    SetTextureImpoterGeneralProperties(temp, _NotsRGBTexture);
                    SetTextureImpoterMipMapProperties(temp);
                    SetTextureImpoterAlphaProperties(temp, _AlphaNotTransparency);
                    SetTextureImpoterSteamingMipMapProperties(temp, _StreamingMipMapEnabled);
                    break;

                case TextureCatalog.CharacterM1RGB_PNG:
                case TextureCatalog.CharacterM2RGB_PNG:
                    if (maxSize == 0)
                    {
                        maxSize = _TexSize_256;
                    }
                    temp = new TextureImportSettingTemplate(textureImporter, maxSize, dxt1, astcRgb_6, astcRgb_6);
                    SetTextureImpoterGeneralProperties(temp, _NotsRGBTexture);
                    SetTextureImpoterMipMapProperties(temp);
                    SetTextureImpoterSteamingMipMapProperties(temp, _StreamingMipMapEnabled);
                    break;

                case TextureCatalog.CharacterNormal_PNG:
                case TextureCatalog.CharacterTN_PNG:
                    if (maxSize == 0)
                    {
                        maxSize = _TexSize_256;
                    }
                    temp = new TextureImportSettingTemplate(textureImporter, maxSize, dxt1, astcRgb_6, astcRgb_6);
                    SetTextureImpoterGeneralProperties(temp, _NotsRGBTexture, normalMap);
                    SetTextureImpoterMipMapProperties(temp);
                    SetTextureImpoterSteamingMipMapProperties(temp, _StreamingMipMapEnabled);
                    break;

                case TextureCatalog.CharacterCommonRGBA_PNG:
                    if (maxSize == 0)
                    {
                        maxSize = _TexSize_256;
                    }
                    temp = new TextureImportSettingTemplate(textureImporter, maxSize, dxt5, etc2_rgba8_cru, pvrtc_rgba4);
                    SetTextureImpoterGeneralProperties(temp);
                    SetTextureImpoterMipMapProperties(temp);
                    SetTextureImpoterAlphaProperties(temp);
                    SetTextureImpoterSteamingMipMapProperties(temp, _StreamingMipMapEnabled);
                    break;


                case TextureCatalog.CharacterCommonRGB_PNG:
                    if (maxSize == 0)
                    {
                        maxSize = _TexSize_256;
                    }
                    temp = new TextureImportSettingTemplate(textureImporter, maxSize, dxt1, etc_rgb4_cru, pvrtc_rgb4);
                    SetTextureImpoterGeneralProperties(temp);
                    SetTextureImpoterMipMapProperties(temp);
                    SetTextureImpoterSteamingMipMapProperties(temp, _StreamingMipMapEnabled);
                    break;
                case TextureCatalog.EnvEffectRGBA_PNG:
                case TextureCatalog.EnvCommonRGBA_PNG:
                    if (maxSize == 0)
                    {
                        maxSize = _TexSize_1024;
                    }
                    temp = new TextureImportSettingTemplate(textureImporter, maxSize, dxt5, etc2_rgba8_cru, pvrtc_rgba4);
                    SetTextureImpoterGeneralProperties(temp);
                    SetTextureImpoterMipMapProperties(temp);
                    SetTextureImpoterAlphaProperties(temp);
                    break;

                case TextureCatalog.EnvEffectRGB_PNG:
                case TextureCatalog.EnvCommonRGB_PNG:
                    if (maxSize == 0)
                    {
                        maxSize = _TexSize_1024;
                    }
                    temp = new TextureImportSettingTemplate(textureImporter, maxSize, dxt1, etc_rgb_4, pvrtc_rgb4);
                    SetTextureImpoterGeneralProperties(temp);
                    SetTextureImpoterMipMapProperties(temp);
                    // SetTextureImpoterAlphaProperties(temp);
                    break;

                //UI
                //July 25, 2020 "ResourcesExt/UI/" 下面贴图NPOT先设置为NONE
                //2020/11/23  BackGround下非半透的调整ASTC 5 * 5
                case TextureCatalog.UIFullScreenBackgroundRGB:
                    if (maxSize == 0)
                    {
                        maxSize = _TexSize_2048;
                    }
                    temp = new TextureImportSettingTemplate(textureImporter, maxSize, dxt1, astcRgb_5, astcRgb_5);
                    SetTextureImpoterGeneralProperties(temp);
                    SetTextureImpoterMipMapProperties(temp, _MipMapDisabled);
                    SetTextureImpoterNPOTProperties(temp);
                    break;

                case TextureCatalog.UIFullScreenBackgroundRGBA:
                    if (maxSize == 0)
                    {
                        maxSize = _TexSize_2048;
                    }
                    temp = new TextureImportSettingTemplate(textureImporter, maxSize, dxt5, astcRgba_4, astcRgba_4);
                    SetTextureImpoterGeneralProperties(temp);
                    SetTextureImpoterMipMapProperties(temp, _MipMapDisabled);
                    SetTextureImpoterAlphaProperties(temp);
                    SetTextureImpoterNPOTProperties(temp);
                    break;
                case TextureCatalog.UILodingScreenRGB:
                    //loading图暂时调整为ASTC5*5  美术有意见再说
                    if (maxSize == 0)
                    {
                        maxSize = _TexSize_2048;
                    }
                    temp = new TextureImportSettingTemplate(textureImporter, maxSize, dxt5, astcRgba_5, astcRgba_5);
                    SetTextureImpoterGeneralProperties(temp);
                    SetTextureImpoterMipMapProperties(temp, _MipMapDisabled);
                    SetTextureImpoterAlphaProperties(temp);
                    SetTextureImpoterNPOTProperties(temp);
                    break;
                case TextureCatalog.UIFullScreenSpriteRGBA:
                case TextureCatalog.UIPlotCharacterSpriteRGBA:
                case TextureCatalog.UI3DAltasSpriteRGBA:
                    if (maxSize == 0)
                    {
                        maxSize = _TexSize_1024;
                    }
                    temp = new TextureImportSettingTemplate(textureImporter, maxSize, dxt5, astcRgba_4, astcRgba_4);
                    SetTextureImpoterGeneralProperties(temp, _IssRGBTexture, sprite);
                    SetTextureImpoterMipMapProperties(temp, true);
                    SetTextureImpoterAlphaProperties(temp);
                    SetTextureImpoterNPOTProperties(temp);
                    break;
                case TextureCatalog.UIAltasSpriteRGBA:
                    if (maxSize == 0)
                    {
                        maxSize = _TexSize_1024;
                    }
                    temp = new TextureImportSettingTemplate(textureImporter, maxSize, dxt5, astcRgba_4, astcRgba_4);
                    SetTextureImpoterGeneralProperties(temp, _IssRGBTexture, sprite);
                    SetTextureImpoterMipMapProperties(temp, _MipMapDisabled);
                    SetTextureImpoterAlphaProperties(temp);
                    SetTextureImpoterNPOTProperties(temp);
                    break;
                case TextureCatalog.UIDynamicSpriteRGBA:
                    if (maxSize == 0)
                    {
                        maxSize = _TexSize_1024;
                    }
                    temp = new TextureImportSettingTemplate(textureImporter, maxSize, rgba32, rgba32, rgba32);
                    SetTextureImpoterGeneralProperties(temp, _IssRGBTexture, sprite);
                    SetTextureImpoterMipMapProperties(temp, _MipMapDisabled);
                    SetTextureImpoterAlphaProperties(temp);
                    SetTextureImpoterNPOTProperties(temp);
                    break;
                //Jan 23, 2021 ZBJ 增加字体SDF贴图文件格式设置
                case TextureCatalog.UISDFRBGA:
                    if (maxSize == 0)
                    {
                        maxSize = _TexSize_4096;
                    }
                    temp = new TextureImportSettingTemplate(textureImporter, maxSize, dxt5, astcRgba_5, astcRgba_5);
                    SetTextureImpoterGeneralProperties(temp);
                    SetTextureImpoterMipMapProperties(temp, _MipMapDisabled);
                    SetTextureImpoterAlphaProperties(temp, _AlphaNotTransparency);
                    SetTextureImpoterNPOTProperties(temp);
                    break;

                case TextureCatalog.UIFullScreenSpriteRGB:
                    if (maxSize == 0)
                    {
                        maxSize = _TexSize_1024;
                    }
                    temp = new TextureImportSettingTemplate(textureImporter, maxSize, dxt1, astcRgb_4, astcRgb_4);
                    SetTextureImpoterGeneralProperties(temp, _IssRGBTexture, sprite);
                    SetTextureImpoterMipMapProperties(temp, _MipMapDisabled);
                    SetTextureImpoterNPOTProperties(temp);
                    break;

                case TextureCatalog.SpineJuqingRGBA_PNG:
                    if (maxSize == 0)
                    {
                        maxSize = _TexSize_2048;
                    }
                    temp = new TextureImportSettingTemplate(textureImporter, maxSize, dxt5, etc2_rgba8_cru, pvrtc_rgba4);
                    SetTextureImpoterGeneralProperties(temp);
                    SetTextureImpoterMipMapProperties(temp, _MipMapDisabled);
                    SetTextureImpoterAlphaProperties(temp, _AlphaNotTransparency);
                    SetTextureImpoterNPOTProperties(temp, npot_Nearest);
                    break;

                case TextureCatalog.SpineJuqingRGB_PNG:
                    if (maxSize == 0)
                    {
                        maxSize = _TexSize_2048;
                    }
                    temp = new TextureImportSettingTemplate(textureImporter, maxSize, dxt5, etc_rgb4_cru, pvrtc_rgb4);
                    SetTextureImpoterGeneralProperties(temp);
                    SetTextureImpoterMipMapProperties(temp, _MipMapDisabled);
                    SetTextureImpoterNPOTProperties(temp, npot_Nearest);
                    break;


                case TextureCatalog.SpineUIRGBA_PNG:
                    if (maxSize == 0)
                    {
                        maxSize = _TexSize_2048;
                    }
                    temp = new TextureImportSettingTemplate(textureImporter, maxSize, dxt5, astcRgba_4, astcRgba_4);
                    SetTextureImpoterGeneralProperties(temp);
                    SetTextureImpoterMipMapProperties(temp, _MipMapDisabled);
                    SetTextureImpoterAlphaProperties(temp, _AlphaNotTransparency);
                    break;

                case TextureCatalog.SpineUIRGB_PNG:
                    if (maxSize == 0)
                    {
                        maxSize = _TexSize_2048;
                    }
                    temp = new TextureImportSettingTemplate(textureImporter, maxSize, dxt5, astcRgb_4, astcRgb_4);
                    SetTextureImpoterGeneralProperties(temp);
                    SetTextureImpoterMipMapProperties(temp, _MipMapDisabled);
                    break;

                //Effect

                case TextureCatalog.UIEffectRGBA_SupportETC2:
                    //case TextureCatalog.CommonEffectRGBA_SupportETC2:
                    if (maxSize == 0)
                    {
                        maxSize = _TexSize_1024;
                    }
                    temp = new TextureImportSettingTemplate(textureImporter, maxSize, dxt5, etc2_rgba8_cru, pvrtc_rgba4);
                    SetTextureImpoterGeneralProperties(temp);
                    SetTextureImpoterMipMapProperties(temp, _MipMapDisabled);
                    SetTextureImpoterAlphaProperties(temp);
                    break;

                //Jul 30, 2021 ZBJ 非HD的特效贴图开启mipmap
                case TextureCatalog.CommonEffectRGBA_SupportETC2:
                    if (maxSize == 0)
                    {
                        maxSize = _TexSize_1024;
                    }
                    temp = new TextureImportSettingTemplate(textureImporter, maxSize, dxt5, etc2_rgba8_cru, pvrtc_rgba4);
                    SetTextureImpoterGeneralProperties(temp);
                    SetTextureImpoterMipMapProperties(temp, _MipmapEnabled);
                    SetTextureImpoterAlphaProperties(temp);
                    break;
                case TextureCatalog.CommonEffectRGBA_NoETC2:
                    if (maxSize == 0)
                    {
                        maxSize = _TexSize_1024;
                    }
                    temp = new TextureImportSettingTemplate(textureImporter, maxSize, rgba32, astcRgba_6, astcRgba_6);
                    SetTextureImpoterGeneralProperties(temp);
                    SetTextureImpoterMipMapProperties(temp, _MipmapEnabled);
                    SetTextureImpoterAlphaProperties(temp);
                    break;

                case TextureCatalog.HDCommonEffectRGBA_SupportETC2:
                    if (maxSize == 0)
                    {
                        maxSize = _TexSize_1024;
                    }
                    temp = new TextureImportSettingTemplate(textureImporter, maxSize, rgba32, astcRgba_4, astcRgba_6);
                    SetTextureImpoterGeneralProperties(temp);
                    SetTextureImpoterMipMapProperties(temp, _MipMapDisabled);
                    SetTextureImpoterAlphaProperties(temp);
                    break;

                case TextureCatalog.CommonEffectRGB_SupportETC:
                    if (maxSize == 0)
                    {
                        maxSize = _TexSize_1024;
                    }
                    temp = new TextureImportSettingTemplate(textureImporter, maxSize, dxt1, etc_rgb4_cru, pvrtc_rgb4);
                    SetTextureImpoterGeneralProperties(temp);
                    SetTextureImpoterMipMapProperties(temp, _MipmapEnabled);
                    break;

                case TextureCatalog.CommonEffectRGB_NoETC:
                    if (maxSize == 0)
                    {
                        maxSize = _TexSize_1024;
                    }
                    temp = new TextureImportSettingTemplate(textureImporter, maxSize, rgb24, astcRgb_6, astcRgb_6);
                    SetTextureImpoterGeneralProperties(temp);
                    SetTextureImpoterMipMapProperties(temp, _MipmapEnabled);
                    break;

                case TextureCatalog.HDCommonEffectRGB_SupportETC:
                    if (maxSize == 0)
                    {
                        maxSize = _TexSize_1024;
                    }
                    temp = new TextureImportSettingTemplate(textureImporter, maxSize, rgb24, astcRgb_4, astcRgb_6);
                    SetTextureImpoterGeneralProperties(temp);
                    SetTextureImpoterMipMapProperties(temp, _MipMapDisabled);
                    break;

                case TextureCatalog.EffectNormalMap:
                    if (maxSize == 0)
                    {
                        maxSize = _TexSize_512;
                    }
                    temp = new TextureImportSettingTemplate(textureImporter, maxSize, dxt1, etc_rgb_4, pvrtc_rgb4);
                    SetTextureImpoterGeneralProperties(temp, _NotsRGBTexture, normalMap);
                    SetTextureImpoterMipMapProperties(temp, _MipmapEnabled);
                    break;

                case TextureCatalog.CommonRGBA_PNG:
                    if (maxSize == 0)
                    {
                        maxSize = _TexSize_512;
                    }
                    temp = new TextureImportSettingTemplate(textureImporter, maxSize, dxt5, etc2_rgba8_cru, pvrtc_rgba4);
                    SetTextureImpoterGeneralProperties(temp);
                    SetTextureImpoterMipMapProperties(temp);
                    SetTextureImpoterAlphaProperties(temp);
                    break;

                case TextureCatalog.CommonRGB_PNG:
                    if (maxSize == 0)
                    {
                        maxSize = _TexSize_512;
                    }
                    temp = new TextureImportSettingTemplate(textureImporter, maxSize, dxt1, etc_rgb4_cru, pvrtc_rgb4);
                    SetTextureImpoterGeneralProperties(temp);
                    SetTextureImpoterMipMapProperties(temp);
                    break;


                //lightMap
                //July 17, 2020; lightmap 的aniso level 设置为3 同之前工程的设置保持一致， CenterCityLightmap 修复texture type为lightmap
                case TextureCatalog.CenterCityLightmap:
                    if (maxSize == 0)
                    {
                        maxSize = _TexSize_2048;
                    }
                    temp = new TextureImportSettingTemplate(textureImporter, maxSize, dxt1, etc_rgb4_cru, pvrtc_rgb4);
                    SetTextureImpoterGeneralProperties(temp, _IssRGBTexture, lightmap);
                    SetTextureImpoterMipMapProperties(temp, _MipmapEnabled, _AnisoLevel_0);
                    break;

                case TextureCatalog.CommonLightMap:
                case TextureCatalog.CommonDirLightMap:
                case TextureCatalog.CommonShadowMaskLightMap:
                    if (maxSize == 0)
                    {
                        maxSize = _TexSize_1024;
                    }
                    temp = new TextureImportSettingTemplate(textureImporter, maxSize, dxt1, etc_rgb4_cru, pvrtc_rgb4);
                    SetTextureImpoterGeneralProperties(temp, _IssRGBTexture, lightmap);
                    SetTextureImpoterMipMapProperties(temp, _MipmapEnabled, _AnisoLevel_0);
                    break;

                default:
                    if (maxSize == 0)
                    {
                        maxSize = _TexSize_512;
                    }
                    temp = new TextureImportSettingTemplate(textureImporter, maxSize, dxt1, etc_rgb4_cru, pvrtc_rgb4);
                    SetTextureImpoterGeneralProperties(temp);
                    SetTextureImpoterMipMapProperties(temp);
                    break;
            }

            return temp;
        }

        //贴图默认属性，sRGB，defaultTex， 2D，  not read/write, bilinear filter
        //多增加一条 默认alphasource = None， 如果有alpha格式 则在后续的alpha中设置为fromInput
        void SetTextureImpoterGeneralProperties(TextureImportSettingTemplate template, bool sRGBTexture = true, TextureImporterType type = TextureImporterType.Default, TextureImporterShape shape = TextureImporterShape.Texture2D, bool readable = false, FilterMode filterMode = FilterMode.Bilinear)
        {
            template.textureImporter.textureType = type;
            template.textureImporter.textureShape = shape;
            template.textureImporter.sRGBTexture = sRGBTexture;
            template.textureImporter.isReadable = readable;
            template.textureImporter.filterMode = filterMode;


            bool hasAlpha = template.textureImporter.DoesSourceTextureHaveAlpha();
            if (hasAlpha)
            {
                template.textureImporter.alphaSource = TextureImporterAlphaSource.FromInput;
                template.textureImporter.alphaIsTransparency = true;
            }
            else
            {
                template.textureImporter.alphaSource = TextureImporterAlphaSource.None;
                template.textureImporter.alphaIsTransparency = false;
            }
        }

        //mipMap相关默认属性：mipmap开启， boxfilter，anisoLevel 默认5
        //July 26, 2021 ZBJ  AnisoLevel 默认值设置改为0
        void SetTextureImpoterMipMapProperties(TextureImportSettingTemplate template, bool mipmapEnabled = true, int anisoLevel = _AnisoLevel_0, TextureImporterMipFilter mipFilter = TextureImporterMipFilter.BoxFilter)
        {
            template.textureImporter.mipmapEnabled = mipmapEnabled;
            template.textureImporter.mipmapFilter = mipFilter;
            template.textureImporter.anisoLevel = anisoLevel;
        }
        void SetTextureImpoterAlphaProperties(TextureImportSettingTemplate template, bool isTransparency = true, TextureImporterAlphaSource alphaSource = TextureImporterAlphaSource.FromInput)
        {
            template.textureImporter.alphaSource = alphaSource;
            template.textureImporter.alphaIsTransparency = isTransparency;
        }

        void SetTextureImpoterCubeMappingProperties(TextureImportSettingTemplate template, TextureImporterGenerateCubemap generateCubemap = TextureImporterGenerateCubemap.AutoCubemap, TextureImporterCubemapConvolution cubemapConvolution = TextureImporterCubemapConvolution.Specular)
        {
            template.textureImporter.generateCubemap = generateCubemap;
            TextureImporterSettings importerSettings = new TextureImporterSettings();
            template.textureImporter.ReadTextureSettings(importerSettings);
            importerSettings.cubemapConvolution = cubemapConvolution;
            importerSettings.seamlessCubemap = false;
            template.textureImporter.SetTextureSettings(importerSettings);
        }

        void SetTextureImpoterNPOTProperties(TextureImportSettingTemplate template, TextureImporterNPOTScale npot = TextureImporterNPOTScale.None)
        {
            template.textureImporter.npotScale = npot;
        }

        //Jan 20, 2021 ZBJ 增加 mip map steaming 设置
        void SetTextureImpoterSteamingMipMapProperties(TextureImportSettingTemplate template, bool isTextureMipMapSteaming, int steamMipMapPriority = 0)
        {
            template.textureImporter.streamingMipmaps = isTextureMipMapSteaming;
            template.textureImporter.streamingMipmapsPriority = steamMipMapPriority;
        }

    }

}