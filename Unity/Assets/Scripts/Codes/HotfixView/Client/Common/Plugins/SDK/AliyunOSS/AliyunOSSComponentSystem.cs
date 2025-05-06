using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Aliyun.OSS;
using Aliyun.OSS.Common;

namespace ET.Client
{
    [FriendOf(typeof(AliyunOSSComponent))]
    public static class AliyunOSSComponentSystem
    {
        [ObjectSystem]
        public class AliyunOSSComponentAwakeSystem : AwakeSystem<AliyunOSSComponent>
        {
            protected override void Awake(AliyunOSSComponent self)
            {
                AliyunOSSComponent.Instance = self;
                self.Awake().Coroutine();
            }
        }

        [ObjectSystem]
        public class AliyunOSSComponentDestroySystem : DestroySystem<AliyunOSSComponent>
        {
            protected override void Destroy(AliyunOSSComponent self)
            {
                self.Destroy().Coroutine();
                if (AliyunOSSComponent.Instance == self)
                {
                    AliyunOSSComponent.Instance = null;
                }
            }
        }

        public static async ETTask Awake(this AliyunOSSComponent self)
        {
            string AliyunOSS = ChannelSettingComponent.Instance.GetAliyunOSS();
            if (AliyunOSS.Trim().IsNullOrEmpty())
            {
                return;
            }

            string[] tmp = AliyunOSS.Trim().Split("|");
            if (tmp.Length < 4)
            {
                Log.Error($"-- ET.Client.AliyunOSSComponentSystem AliyunOSS.Split(|).Length < 4 [{AliyunOSS}]");
                return;
            }
            self.AccessKeyId = tmp[0].Trim();
            self.AccessKeySecret = tmp[1].Trim();
            self.EndPoint = tmp[2].Trim();
            self.Bucket = tmp[3].Trim();
            self._ossClient = new Aliyun.OSS.OssClient(self.EndPoint, self.AccessKeyId,
                self.AccessKeySecret);
            await ETTask.CompletedTask;
        }

        public static async ETTask Destroy(this AliyunOSSComponent self)
        {
            self._ossClient = null;
            await ETTask.CompletedTask;
        }

        public static bool ChkIsAliyunOSSReady(this AliyunOSSComponent self)
        {
            if (self._ossClient == null)
            {
                Log.Error($"-- ET.Client.AliyunOSSComponentSystem.ChkIsAliyunOSSReady self._ossClient == null");
                return false;
            }
            return true;
        }

        public static string GetAliyunOSSDownLoadURL(this AliyunOSSComponent self, string ossPath)
        {
            return $"{self.Bucket}.{self.EndPoint}/{ossPath}";
        }

        public static void UpLoadFile(this AliyunOSSComponent self, string fullFilePath, string ossPath, Action<string> complete, Action<float> action = null)
        {
            if (self._ossClient == null)
            {
                Log.Error($"-- ET.Client.AliyunOSSComponentSystem.UpLoadFile self._ossClient == null");
                return;
            }
            Thread putLocalThread = null;
            putLocalThread = new Thread(() =>
            {
                try
                {
                    using (var fs = File.Open(fullFilePath, FileMode.Open))
                    {
                        PutObjectRequest putObjectRequest = new PutObjectRequest(self.Bucket, ossPath, fs);
                        putObjectRequest.StreamTransferProgress += (obg, args) =>
                        {
                            float putProcess = (args.TransferredBytes * 100 / args.TotalBytes) / 100.0f;
                            action?.Invoke(putProcess);
                            if (putProcess >= 1)
                            {
                                complete?.Invoke(self.GetAliyunOSSDownLoadURL(ossPath));
                                putObjectRequest.StreamTransferProgress = null;
                            }
                        };
                        self._ossClient.PutObject(putObjectRequest);
                    }
                }
                catch (OssException e)
                {
                    Log.Debug("上传错误：" + e);
                }
                catch (Exception e)
                {
                    Log.Debug("上传错误：" + e);
                }
                finally
                {
                    putLocalThread.Abort();
                }
            });
            putLocalThread.Start();
        }

        public static async ETTask<string> UpLoadBytes(this AliyunOSSComponent self, byte[] data, string ossPath)
        {
            string downloadURL = "";
            ETTask doneTask = ETTask.Create();
            Action<string> completeTmp = (url) =>
            {
                doneTask.SetResult();
                downloadURL = url;
            };
            self.UpLoadBytes(data, ossPath, completeTmp);
            await doneTask;
            doneTask = null;
            return downloadURL;
        }

        public static void UpLoadBytes(this AliyunOSSComponent self, byte[] data, string ossPath, Action<string> complete, Action<float> action = null)
        {
            if (self._ossClient == null)
            {
                Log.Error($"-- ET.Client.AliyunOSSComponentSystem.UpLoadBytes self._ossClient == null");
                return;
            }
            Thread putLocalThread = null;
            putLocalThread = new Thread(() =>
            {
                try
                {

                    PutObjectRequest putObjectRequest = new PutObjectRequest(self.Bucket, ossPath, new MemoryStream(data));
                    putObjectRequest.StreamTransferProgress += (obg, args) =>
                    {
                        float putProcess = (args.TransferredBytes * 100 / args.TotalBytes) / 100.0f;
                        action?.Invoke(putProcess);
                        if (putProcess >= 1)
                        {
                            complete?.Invoke(self.GetAliyunOSSDownLoadURL(ossPath));
                            putObjectRequest.StreamTransferProgress = null;
                        }
                    };
                    self._ossClient.PutObject(putObjectRequest);
                }
                catch (OssException e)
                {
                    Log.Debug("上传错误：" + e);
                }
                catch (Exception e)
                {
                    Log.Debug("上传错误：" + e);
                }
                finally
                {
                    putLocalThread.Abort();
                }
            });
            putLocalThread.Start();
        }

        /// <summary>
        /// 删除单个文件
        /// </summary>
        /// <param name="objectName"></param>
        public static void DeleteFile(this AliyunOSSComponent self, string objectName)
        {
            if (self._ossClient == null)
            {
                Log.Error($"-- ET.Client.AliyunOSSComponentSystem.DeleteFile self._ossClient == null");
                return;
            }
            var client = self._ossClient;
            try
            {
                // 删除文件。
                client.DeleteObject(self.Bucket, objectName);
                Log.Debug("Delete object succeeded");
            }
            catch (Exception ex)
            {
                Log.Debug(string.Format("Delete object failed. {0}", ex.Message));
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="files"></param>
        /// <param name="quietMode">设置为详细模式，返回所有删除的文件列表。</param>
        public static void DeleteMultiFiles(this AliyunOSSComponent self, List<string> files, bool quietMode = false)
        {
            if (self._ossClient == null)
            {
                Log.Error($"-- ET.Client.AliyunOSSComponentSystem.DeleteMultiFiles self._ossClient == null");
                return;
            }
            var client = self._ossClient;
            try
            {
                var request = new DeleteObjectsRequest(self.Bucket, files, quietMode);
                var result = client.DeleteObjects(request);
                if ((!quietMode) && (result.Keys != null))
                {
                    foreach (var obj in result.Keys)
                    {
                        Log.Debug(string.Format("Delete successfully : {0} ", obj.Key));
                    }
                }

                Log.Debug("Delete objects succeeded");
            }
            catch (Exception ex)
            {
                Log.Debug(string.Format("Delete objects failed. {0}", ex.Message));
            }
        }

        public static bool ExistFile(this AliyunOSSComponent self, string objectName)
        {
            if (self._ossClient == null)
            {
                Log.Error($"-- ET.Client.AliyunOSSComponentSystem.ExistFile self._ossClient == null");
                return false;
            }
            bool exist = false;
            try
            {
                exist = self._ossClient.DoesObjectExist(self.Bucket, objectName);
                Log.Debug("Object exist ? " + exist);
                return exist;
            }
            catch (OssException ex)
            {
                Log.Debug(string.Format("Failed with error code: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}",
                    ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId));
                return exist;
            }
            catch (Exception ex)
            {
                Log.Debug(string.Format("Failed with error info: {0}", ex.Message));
                return exist;
            }
        }
    }
}