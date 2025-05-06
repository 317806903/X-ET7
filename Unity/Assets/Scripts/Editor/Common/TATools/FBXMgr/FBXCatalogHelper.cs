using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Reflection;
using TATools.MatchingRules;


namespace TATools.FBXFormatSetting
{
    public enum FBXCatalog
    {
        NotMatch = -1,
        NotDeal,
        Animation,
        Character,
        EffectUI,
        EffectScene,
        Item,
        Scene,
    }
    public class FBXCatalogHelper
    {
        public MatchingRule matchingRule;
        public string fbxPath;
        public FBXCatalog fbxCatalog
        {
            get
            {
                return (FBXCatalog)matchingRule.dealType;
            }
        }

        public FBXCatalogHelper(string path, ModelImporter fbxImporter)
        {
            this.fbxPath = path;
            this.matchingRule = GetFBXCatalog(path);
        }

        public static MatchingRule GetFBXCatalog(string path)
        {
            return MatchingRuleMgr.DealMatching(path, MatchingFBXRuleMgr.Instance.MatchingFBXRulesList);
        }

    }
}