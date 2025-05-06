using System;
using System.Collections.Generic;

namespace ET.Client
{
    [ComponentOf(typeof(Scene))]
    public class AliyunOSSComponent : Entity,IAwake,IDestroy
    {
        [StaticField]
        public static AliyunOSSComponent Instance;

        public Aliyun.OSS.OssClient _ossClient;
        public string AccessKeyId;
        public string AccessKeySecret;
        public string EndPoint;
        public string Bucket;
    }
}