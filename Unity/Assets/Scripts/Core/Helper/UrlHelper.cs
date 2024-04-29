using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace ET
{
	public static class UrlHelper
	{
        /// <summary>
        /// 解析URL中的参数。
        /// 注意：在<see cref="Dictionary{TKey, TValue}"/>中，直接读取不存在的键值对时，会抛出异常。
        /// </summary>
        /// <param name="url">
        /// 支持解析示例：
        /// https://www.google.com/index?page=1&lang=eng
        /// https://www.google.com/
        /// https://www.google.com/index?page=title=index=1&lang=&chang&char=?&id=123
        /// https://www.google.com/index?
        /// </param>
        /// <param name="baseUrlKey"> URL中符号“?”的前面部分，在返回的结果中，对应的关键字。</param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetQueryDictionary(string url, string baseUrlKey = null, bool ignoreCase = false)
        {
            // 第一个“?”符号的下标。
            // 用于支持，参数中包括“?”符号的URL。
            int indexQuestionMark = url.IndexOf('?');
            int countQuery = url.Length - indexQuestionMark - 1;
            StringComparer comparer;
            if (ignoreCase)
                comparer = StringComparer.CurrentCultureIgnoreCase;
            else
                comparer = StringComparer.CurrentCulture;

            Dictionary<string, string> info = null;

            // 如果URL中包括有效的参数。
            if (indexQuestionMark > -1 && countQuery > 0)
            {
                string queryString = url.Substring(indexQuestionMark + 1, countQuery);

                // 为空的键值对没有意义，所以，舍弃为空的键值对。
                string[] keyAndValuePairs = queryString.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
                info = new Dictionary<string, string>(keyAndValuePairs.Length + 1, comparer);
                foreach (var pair in keyAndValuePairs)
                {
                    // 第一个“=”符号的下标。
                    // 用于支持，value中包括“=”符号的参数。
                    int indexEquals = pair.IndexOf('=');

                    // 如果包含“=”符号。
                    if (indexEquals > -1)
                    {
                        string key = pair.Substring(0, indexEquals);
                        key = Uri.UnescapeDataString(key);

                        int countValue = pair.Length - indexEquals - 1;
                        // 避免“=”符号后面没有内容时，indexEquals + 1超出数组的有效索引。
                        string value = (countValue == 0) ? string.Empty : pair.Substring(indexEquals + 1, countValue);
                        value = Uri.UnescapeDataString(value);

                        info[key] = value;
                    }
                    // 用关键字保存特殊参数。
                    else
                    {
                        info[pair] = null;
                    }
                }
            }
            info = info ?? new Dictionary<string, string>(comparer);

            if (baseUrlKey != null)
            {
                if (indexQuestionMark < 0 || indexQuestionMark > url.Length)
                {
                    indexQuestionMark = url.Length;
                }
                // URL中符号“?”的前面部分。
                string baseUrlValue = url.Substring(0, indexQuestionMark);
                info[baseUrlKey] = baseUrlValue;
            }
            return info;
        }

        /// <summary>
        /// 解析URL中的参数。
        /// 注意：在<see cref="NameValueCollection"/>中，直接读取不存在的键值对时，返回null，不会抛出异常。
        /// </summary>
        /// <param name="url">
        /// 支持解析示例：
        /// https://www.google.com/index?page=1&lang=eng
        /// https://www.google.com/
        /// https://www.google.com/index?page=title=index=1&lang=&chang&char=?&id=123
        /// https://www.google.com/index?
        /// </param>
        /// <param name="baseUrlKey"> URL中符号“?”的前面部分，在返回的结果中，对应的关键字。</param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static NameValueCollection GetQueryCollection(string url, string baseUrlKey = null, bool ignoreCase = false)
        {
            // 第一个“?”符号的下标。
            // 用于支持，参数中包括“?”符号的URL。
            int indexQuestionMark = url.IndexOf('?');
            int countQuery = url.Length - indexQuestionMark - 1;
            StringComparer comparer;
            if (ignoreCase)
                comparer = StringComparer.CurrentCultureIgnoreCase;
            else
                comparer = StringComparer.CurrentCulture;

            NameValueCollection info = null;

            // 如果URL中包括有效的参数。
            if (indexQuestionMark > -1 && countQuery > 0)
            {
                string queryString = url.Substring(indexQuestionMark + 1, countQuery);

                // 为空的键值对没有意义，所以，舍弃为空的键值对。
                string[] keyAndValuePairs = queryString.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
                info = new NameValueCollection(keyAndValuePairs.Length + 1, comparer);
                foreach (var pair in keyAndValuePairs)
                {
                    // 第一个“=”符号的下标。
                    // 用于支持，value中包括“=”符号的参数。
                    int indexEquals = pair.IndexOf('=');

                    // 如果包含“=”符号。
                    if (indexEquals > -1)
                    {
                        string key = pair.Substring(0, indexEquals);
                        key = Uri.UnescapeDataString(key);

                        int countValue = pair.Length - indexEquals - 1;
                        // 避免“=”符号后面没有内容时，indexEquals + 1超出数组的有效索引。
                        string value = (countValue == 0) ? string.Empty : pair.Substring(indexEquals + 1, countValue);
                        value = Uri.UnescapeDataString(value);

                        info.Add(key, value);
                    }
                    // 用关键字保存特殊参数。
                    else
                    {
                        info.Add(pair, null);
                    }
                }
            }
            info = info ?? new NameValueCollection(comparer);

            if (baseUrlKey != null)
            {
                if (indexQuestionMark < 0 || indexQuestionMark > url.Length)
                {
                    indexQuestionMark = url.Length;
                }
                // URL中符号“?”的前面部分。
                string baseUrlValue = url.Substring(0, indexQuestionMark);
                info.Add(baseUrlKey, baseUrlValue);
            }
            return info;
        }

        public static string GetUrlQueryString(ICollection<KeyValuePair<string, string>> parameters)
        {
            List<string> pairs = new List<string>(parameters.Count);
            foreach (KeyValuePair<string, string> item in parameters)
            {
                if (!string.IsNullOrEmpty(item.Value))
                {
                    pairs.Add(string.Format("{0}={1}", Uri.EscapeDataString(item.Key), Uri.EscapeDataString(item.Value)));
                }
            }
            return string.Join("&", pairs.ToArray());
        }

        public static string GetUrlQueryString(NameValueCollection parameters)
        {
            List<string> pairs = new List<string>(parameters.Count * 2);
            foreach (object itemKeyObj in parameters.Keys)
            {
                string itemKey = itemKeyObj?.ToString();
                if (itemKey != null)
                {
                    var itemValues = parameters.GetValues(itemKey);
                    if (itemValues != null)
                    {
                        foreach (var itemValue in itemValues)
                        {
                            if (!string.IsNullOrEmpty(itemValue))
                            {
                                pairs.Add(string.Format("{0}={1}", Uri.EscapeDataString(itemKey), Uri.EscapeDataString(itemValue)));
                            }
                        }
                    }
                }
            }
            return string.Join("&", pairs.ToArray());
        }

        public static string GetUrlString(string baseUrl, ICollection<KeyValuePair<string, string>> parameters)
        {
            if (parameters != null && parameters.Count > 0)
            {
                string queryString = GetUrlQueryString(parameters);
                if (!string.IsNullOrEmpty(queryString))
                {
                    StringBuilder builder = new StringBuilder(baseUrl.Length + queryString.Length + 1);
                    builder.Append(baseUrl);
                    builder.Append("?");
                    builder.Append(queryString);
                    return builder.ToString();
                }
            }
            return baseUrl;
        }

        public static string GetUrlString(string baseUrl, NameValueCollection parameters)
        {
            if (parameters != null && parameters.Count > 0)
            {
                string queryString = GetUrlQueryString(parameters);
                if (!string.IsNullOrEmpty(queryString))
                {
                    StringBuilder builder = new StringBuilder(baseUrl.Length + queryString.Length + 1);
                    builder.Append(baseUrl);
                    builder.Append("?");
                    builder.Append(queryString);
                    return builder.ToString();
                }
            }
            return baseUrl;
        }
    }
}