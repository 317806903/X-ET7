using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Reflection;
using TATools.MatchingRules;


namespace TATools.FBXFormatSetting
{
    public class MatchingFBXRuleMgr
    {
        private static MatchingFBXRuleMgr _Instance;
        public static MatchingFBXRuleMgr Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new MatchingFBXRuleMgr();
                    _Instance.Init();
                }

                return _Instance;
            }
            set
            {
                _Instance = null;
            }
        }

        private List<MatchingRule> _matchingFBXRulesList = new();
        public List<MatchingRule> MatchingFBXRulesList
        {
            get
            {
                return _matchingFBXRulesList;
            }
        }

        private void _AddMatchingRule(string pathRule, string fileNameRule, string fileExtRule, bool isForceOrdering, FBXCatalog fbxCatalog)
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

            MatchingRule matchingRule = MatchingRule.Init(pathRule, fileNameRule, fileExtRule, 0, ordering, (int)fbxCatalog);
            _matchingFBXRulesList.Add(matchingRule);
        }

        private void Init()
        {
            //目录名带 (test) 的不处理
            this._AddMatchingRule("(test),(Test),(TEST)", "*", "*", true, FBXCatalog.NotDeal);
            //文件名带 (test) 的不处理
            this._AddMatchingRule("*", "(test),(Test),(TEST)", "*", true, FBXCatalog.NotDeal);

            //Packages目录的不处理
            this._AddMatchingRule("^Packages/", "*", "*", true, FBXCatalog.NotDeal);

            this._AddMatchingRule("/animation/,/Animation/", "*", "*", true, FBXCatalog.Animation);
            this._AddMatchingRule("/Scene/", "*", "*", true, FBXCatalog.Scene);
            this._AddMatchingRule("/Character/", "*", "*", true, FBXCatalog.Character);
            this._AddMatchingRule("/Effect/", "*", "*", true, FBXCatalog.EffectScene);
            this._AddMatchingRule("/Item/", "*", "*", true, FBXCatalog.Item);
        }
    }

}