using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

namespace TATools.MatchingRules
{
    public class SingleMatchingRule
    {
        /// <summary>
        /// 匹配规则:多条记录时，为or的关系
        ///     ^AABB 表示以AABB开头
        ///     AABB$ 表示以AABB结尾
        ///     AA*BB 表示AA和BB中间有若干字符
        /// </summary>
        public List<List<string>> matchingRule = new ();

        /// <summary>
        /// 排除规则：多条记录时，为or的关系
        /// </summary>
        public List<List<string>> excludeRule = new ();

        public SingleMatchingRule(string ruleString)
        {
            string[] array0 = ruleString.Split("&");
            for (int i = 0; i < array0.Length; i++)
            {
                string subRuleString = array0[i];
                this.matchingRule.Add(new());
                this.excludeRule.Add(new());
                string[] array1 = subRuleString.Split(";");
                Debug.Assert(array1.Length > 0 && array1.Length <= 2);
                string[] array11 = array1[0].Trim().Split("|");
                foreach (string rule in array11)
                {
                    string ruleDeal = rule.Trim();
                    if (string.IsNullOrWhiteSpace(ruleDeal) == false)
                    {
                        DealRuleString(ref ruleDeal);
                        this.matchingRule[i].Add(ruleDeal);
                    }
                }

                if (array1.Length == 2)
                {
                    string[] array12 = array1[1].Trim().Split("|");
                    foreach (string rule in array12)
                    {
                        string ruleDeal = rule.Trim();
                        if (string.IsNullOrWhiteSpace(ruleDeal) == false)
                        {
                            DealRuleString(ref ruleDeal);
                            this.excludeRule[i].Add(ruleDeal);
                        }
                    }
                }
            }
        }

        private void DealRuleString(ref string ruleDeal)
        {
            ruleDeal = ruleDeal.Replace(@"\\", @"\");
            ruleDeal = ruleDeal.Replace(@"\", "/");
            ruleDeal = ruleDeal.Replace("**", "*");
            ruleDeal = ruleDeal.Replace("*", "[.\\s\\S]*?");
        }

        public bool ChkMatching(string value)
        {
            for (int i = 0; i < this.matchingRule.Count; i++)
            {
                bool isMatch = true;
                foreach (string rule in this.matchingRule[i])
                {
                    isMatch = Regex.IsMatch(value, rule);
                    if (isMatch)
                    {
                        break;
                    }
                }

                if (isMatch == false)
                {
                    return false;
                }

                foreach (string rule in this.excludeRule[i])
                {
                    isMatch = Regex.IsMatch(value, rule);
                    if (isMatch)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }

    public class MatchingRule
    {
        /// <summary>
        /// 目录规则:
        ///     匹配规则示例:
        ///         rule1,rule2;rule51,rule52
        ///     表示满足 rule1 或 rule2 规则，且 不满足rule51,不满足规则rule52 的符合条件
        /// </summary>
        string _pathRule;

        /// <summary>
        /// 文件名规则:  同目录规则
        /// </summary>
        string _fileNameRule;

        /// <summary>
        /// 文件后缀规则:  同目录规则
        /// </summary>
        string _fileExtRule;

        public int maxSize;
        /// <summary>
        /// 优先级:同时匹配中的两条记录中,取小的那条生效,如果两条一样,以后面那条生效
        /// </summary>
        public int ordering;
        public string matchRule;
        public int dealType;

        private SingleMatchingRule _pathSingleMatchingRule;
        private SingleMatchingRule _fileNameSingleMatchingRule;
        private SingleMatchingRule _fileExtSingleMatchingRule;

        public MatchingRule(string pathRule, string fileNameRule, string fileExtRule, int maxSize, int ordering, int dealType)
        {
            this._pathRule = pathRule;
            this._fileNameRule = fileNameRule;
            this._fileExtRule = fileExtRule.ToLower().Replace("*.", "");
            this.maxSize = maxSize;
            this.ordering = ordering;
            this.matchRule = $"{this._pathRule}|{this._fileNameRule}|{this._fileExtRule}_{this.maxSize}";
            this.dealType = dealType;

            this._pathSingleMatchingRule = new SingleMatchingRule(this._pathRule);
            this._fileNameSingleMatchingRule = new SingleMatchingRule(this._fileNameRule);
            this._fileExtSingleMatchingRule = new SingleMatchingRule(this._fileExtRule);
        }

        /// <summary>
        /// 匹配规则
        /// </summary>
        /// <param name="pathRule">
        /// 目录规则:
        ///     匹配规则示例:
        ///         rule1,rule2;rule51,rule52
        ///     表示满足 rule1 或 rule2 规则，且 不满足rule51,不满足规则rule52 的符合条件</param>
        /// <param name="fileNameRule">文件后缀规则:  同目录规则</param>
        /// <param name="fileExtRule">文件后缀规则:  同目录规则</param>
        /// <param name="maxSize">图片允许的最大尺寸</param>
        /// <param name="ordering">优先级:同时匹配中的两条记录中,取小的那条生效,如果两条一样,以后面那条生效</param>
        /// <param name="dealType"></param>
        public static MatchingRule Init(string pathRule, string fileNameRule, string fileExtRule, int maxSize, int ordering, int dealType)
        {
            MatchingRule rule = new MatchingRule(pathRule, fileNameRule, fileExtRule, maxSize, ordering, dealType);
            return rule;
        }

        public bool ChkMatching(string path, string fileName, string fileExt)
        {
            if (this._pathSingleMatchingRule.ChkMatching(path) == false) return false;
            if (this._fileNameSingleMatchingRule.ChkMatching(fileName) == false) return false;
            if (this._fileExtSingleMatchingRule.ChkMatching(fileExt) == false) return false;

            return true;
        }
    }

    public static class MatchingRuleMgr
    {
        /// <summary>
        /// 强制执行这条规则
        /// </summary>
        public static readonly int ForceMatchOrdering = 9999;
        private static int _ordering = 100;
        /// <summary>
        /// 会自增的ordering值
        /// </summary>
        public static int Ordering
        {
            get { return _ordering++; }
        }

        public static MatchingRule DealMatching(string assetPath, List<MatchingRule> matchingRulesList)
        {
            string path = Path.GetDirectoryName(assetPath);
            path = path.Replace(@"\\", @"\");
            path = path.Replace(@"\", "/");
            path = $"{path}/";
            string fileName = Path.GetFileNameWithoutExtension(assetPath);
            string fileExt = Path.GetExtension(assetPath);

            int matchOrdering = -1;
            MatchingRule matchingRule = new MatchingRule("", "", "", 0, 0, -1);
            for (int i = 0; i < matchingRulesList.Count; i++)
            {
                MatchingRule curMatchingRule = matchingRulesList[i];
                if (curMatchingRule.ChkMatching(path, fileName, fileExt))
                {
                    int curOrdering = curMatchingRule.ordering;
                    if (curOrdering == ForceMatchOrdering)
                    {
                        matchOrdering = curOrdering;
                        matchingRule = curMatchingRule;
                        break;
                    }

                    if (matchOrdering != -1 && matchOrdering < curOrdering)
                    {
                        continue;
                    }

                    matchOrdering = curOrdering;
                    matchingRule = curMatchingRule;
                }
            }

            return matchingRule;
        }
    }
}