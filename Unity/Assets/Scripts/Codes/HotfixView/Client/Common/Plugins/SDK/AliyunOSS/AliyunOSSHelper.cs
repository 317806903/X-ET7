using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Aliyun.OSS;
using Aliyun.OSS.Common;

namespace ET.Client
{
    public static class AliyunOSSHelper
    {
        public static bool ChkIsAliyunOSSReady()
        {
            return AliyunOSSComponent.Instance.ChkIsAliyunOSSReady();
        }

        public static string GetAliyunOSSDownLoadURL(string ossPath)
        {
            return AliyunOSSComponent.Instance.GetAliyunOSSDownLoadURL(ossPath);
        }

        public static void UpLoadFile(string fullFilePath, string ossPath, Action<string> complete, Action<float> action = null)
        {
            AliyunOSSComponent.Instance.UpLoadFile(fullFilePath, ossPath, complete, action);
        }

        public static async ETTask<string> UpLoadBytes(byte[] data, string ossPath)
        {
            return await AliyunOSSComponent.Instance.UpLoadBytes(data, ossPath);
        }

        public static void UpLoadBytes(byte[] data, string ossPath, Action<string> complete, Action<float> action = null)
        {
            AliyunOSSComponent.Instance.UpLoadBytes(data, ossPath, complete, action);
        }

        /// <summary>
        /// 删除单个文件
        /// </summary>
        /// <param name="objectName"></param>
        public static void DeleteFile(string objectName)
        {
            AliyunOSSComponent.Instance.DeleteFile(objectName);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="files"></param>
        /// <param name="quietMode">设置为详细模式，返回所有删除的文件列表。</param>
        public static void DeleteMultiFiles(List<string> files, bool quietMode = false)
        {
            AliyunOSSComponent.Instance.DeleteMultiFiles(files, quietMode);
        }

        public static bool ExistFile(string objectName)
        {
            return AliyunOSSComponent.Instance.ExistFile(objectName);
        }
    }
}