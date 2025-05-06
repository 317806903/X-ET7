using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Reflection;
using TATools.MatchingRules;


namespace TATools.AudioFormatSetting
{
    public enum AudioCatalog
    {
        NotMatch = -1,
        NotDeal,
        AduioBattle,
        AduioUI,
        Music,
    }
    public class AudioCatalogHelper
    {
        public MatchingRule matchingRule;
        public string audioPath;
        public AudioCatalog audioCatalog
        {
            get
            {
                return (AudioCatalog)matchingRule.dealType;
            }
        }

        public AudioCatalogHelper(string path, AudioImporter audioImporter)
        {
            this.audioPath = path;
            this.matchingRule = GetAudioCatalog(path);
        }

        public static MatchingRule GetAudioCatalog(string path)
        {
            return MatchingRuleMgr.DealMatching(path, MatchingAudioRuleMgr.Instance.MatchingAudioRulesList);
        }

    }
}