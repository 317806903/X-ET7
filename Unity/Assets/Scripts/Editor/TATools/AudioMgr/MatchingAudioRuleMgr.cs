using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Reflection;
using TATools.MatchingRules;


namespace TATools.AudioFormatSetting
{
    public class MatchingAudioRuleMgr
    {
        private static MatchingAudioRuleMgr _Instance;
        public static MatchingAudioRuleMgr Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new MatchingAudioRuleMgr();
                    _Instance.Init();
                }

                return _Instance;
            }
            set
            {
                _Instance = null;
            }
        }

        private List<MatchingRule> _matchingAudioRulesList = new();
        public List<MatchingRule> MatchingAudioRulesList
        {
            get
            {
                return _matchingAudioRulesList;
            }
        }

        private void _AddMatchingRule(string pathRule, string fileNameRule, string fileExtRule, bool isForceOrdering, AudioCatalog audioCatalog)
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

            MatchingRule matchingRule = MatchingRule.Init(pathRule, fileNameRule, fileExtRule, 0, ordering, (int)audioCatalog);
            _matchingAudioRulesList.Add(matchingRule);
        }

        private void Init()
        {
            //目录名带 (test) 的不处理
            this._AddMatchingRule("(test),(Test),(TEST)", "*", "*", true, AudioCatalog.NotDeal);
            //文件名带 (test) 的不处理
            this._AddMatchingRule("*", "(test),(Test),(TEST)", "*", true, AudioCatalog.NotDeal);

            //Packages目录的不处理
            this._AddMatchingRule("^Packages/", "*", "*", true, AudioCatalog.NotDeal);

            this._AddMatchingRule("^Assets/ResAB/Audio/AudioBattle/", "*", "*", false, AudioCatalog.AduioBattle);
            this._AddMatchingRule("^Assets/ResAB/Audio/AudioUI/", "*", "*", false, AudioCatalog.AduioUI);
            this._AddMatchingRule("^Assets/ResAB/Audio/Music/", "*", "*", false, AudioCatalog.Music);

        }
    }

}