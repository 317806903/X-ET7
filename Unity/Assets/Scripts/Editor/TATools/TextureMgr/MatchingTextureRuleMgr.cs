using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Reflection;
using TATools.MatchingRules;


namespace TATools.TextureFormatSetting
{
    public class MatchingTextureRuleMgr
    {
        private static MatchingTextureRuleMgr _Instance;
        public static MatchingTextureRuleMgr Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new MatchingTextureRuleMgr();
                    _Instance.Init();
                }

                return _Instance;
            }
            set
            {
                _Instance = null;
            }
        }
        const int _TexSize_128 = 128;
        const int _TexSize_256 = 256;
        const int _TexSize_512 = 512;
        const int _TexSize_1024 = 1024;
        const int _TexSize_2048 = 2048;
        const int _TexSize_4096 = 4096;
        private List<MatchingRule> _matchingTextureRulesList = new();
        public List<MatchingRule> MatchingTextureRulesList
        {
            get
            {
                return _matchingTextureRulesList;
            }
        }

        private void _AddMatchingRule(string pathRule, string fileNameRule, string fileExtRule, int maxSize, bool isForceOrdering, TextureCatalog textureCatalog)
        {
            int ordering = 0;
            if (isForceOrdering)
            {
                ordering = MatchingRuleMgr.ForceMatchOrdering;
            }
            else
            {
                ordering = MatchingRuleMgr.Ordering;
            }

            MatchingRule matchingRule = MatchingRule.Init(pathRule, fileNameRule, fileExtRule, maxSize, ordering, (int)textureCatalog);
            _matchingTextureRulesList.Add(matchingRule);
        }

        private void Init()
        {
            //目录名带 (test) 的不处理
            this._AddMatchingRule("(test),(Test),(TEST)", "*", "*", _TexSize_4096, true, TextureCatalog.NotDeal);
            //文件名带 (test) 的不处理
            this._AddMatchingRule("*", "(test),(Test),(TEST)", "*", _TexSize_4096, true, TextureCatalog.NotDeal);

            //Packages目录的不处理
            this._AddMatchingRule("^Packages/", "*", "*", _TexSize_4096, true, TextureCatalog.NotDeal);

            //设置UGUI目录下的图片
            this._AddMatchingRule("^Assets/ResAB/UI/&@dynamic", "*", "*.png", _TexSize_2048, false, TextureCatalog.UIDynamicSpriteRGBA);
            this._AddMatchingRule("^Assets/ResAB/UI/", "@dynamic", "*.png", _TexSize_2048, false, TextureCatalog.UIDynamicSpriteRGBA);

            this._AddMatchingRule("/BattleNum/", "*", "*.png", _TexSize_2048, false, TextureCatalog.UIAltasSpriteRGBA);

            this._AddMatchingRule("^Assets/ResAB/UI/", "*", "*.png", _TexSize_2048, false, TextureCatalog.UIAltasSpriteRGBA);

            this._AddMatchingRule("^Assets/ResAB/UI_MultiLanguage/", "*", "*.png", _TexSize_2048, false, TextureCatalog.UIAltasSpriteRGBA);

            this._AddMatchingRule("^Assets/ResAB/UI/", "*", "*.jpg", _TexSize_2048, false, TextureCatalog.UIAltasSpriteRGBA);

            this._AddMatchingRule("^Assets/ResAB/UI_MultiLanguage/", "*", "*.jpg", _TexSize_2048, false, TextureCatalog.UIAltasSpriteRGBA);

            //处理exr后缀
            this._AddMatchingRule("*", "Cube_*@256", "*.exr", _TexSize_256, false, TextureCatalog.Cube256_EXR);
            this._AddMatchingRule("*", "Cube_", "*.exr", _TexSize_128, false, TextureCatalog.CommonEXR);
            this._AddMatchingRule("*", "Lightmap-*Level_CenterCity", "*.exr", _TexSize_2048, false, TextureCatalog.CenterCityLightmap);
            this._AddMatchingRule("*", "Lightmap-*_dir", "*.exr", _TexSize_1024, false, TextureCatalog.CommonDirLightMap);
            this._AddMatchingRule("*", "Lightmap-*_shadowmask", "*.exr", _TexSize_1024, false, TextureCatalog.CommonShadowMaskLightMap);
            this._AddMatchingRule("*", "*", "*.exr", _TexSize_1024, false, TextureCatalog.CommonLightMap);

            //处理png后缀
            this._AddMatchingRule("*", "Cube_", "*", _TexSize_128, false, TextureCatalog.CommonCube_PNG);
            this._AddMatchingRule("*", "Lightmap-*_dir", "*", _TexSize_1024, false, TextureCatalog.CommonDirLightMap);
            this._AddMatchingRule("*", "Lightmap-*_shadowmask", "*", _TexSize_1024, false, TextureCatalog.CommonShadowMaskLightMap);
            this._AddMatchingRule("*", "Lightmap-", "*", _TexSize_1024, false, TextureCatalog.CommonLightMap);
        }
    }


}