#if UNITY_ANDROID
using System;
using System.IO;
using System.Xml;
using UnityEditor.Android;
using UnityEngine;

namespace Unity.Services.PlayerAccounts
{
    class AndroidBuildPostProcess : IPostGenerateGradleAndroidProject
    {
        public int callbackOrder => 1;

        public void OnPostGenerateGradleAndroidProject(string path)
        {
            if (string.IsNullOrEmpty(PlayerAccountSettings.DeepLinkUriHostPrefix) ||
                string.IsNullOrEmpty(PlayerAccountSettings.DeepLinkUriScheme))
            {
                return;
            }

            Logger.Log("AndroidBuildPostProcess: Adding deeplink intent for player login postback to AndroidManifest.xml");
            var manifestFilename = JoinPaths(new[] { path, "src", "main", "AndroidManifest.xml" });

            var document = new XmlDocument();
            document.Load(manifestFilename);

            var nsmgr = new XmlNamespaceManager(document.NameTable);
            nsmgr.AddNamespace("android", "http://schemas.android.com/apk/res/android");

            if (document.DocumentElement == null)
            {
                Debug.LogError("<PlayerAccounts> AndroidBuildPostProcess: Could not load AndroidManifest.xml");
                return;
            }

            var activityNode = document.DocumentElement.SelectSingleNode(
                "application/activity[@android:name=\"com.unity3d.player.UnityPlayerActivity\"]", nsmgr);

            if (activityNode?.OwnerDocument == null)
            {
                Debug.LogError("<PlayerAccounts> AndroidBuildPostProcess: AndroidManifest.xml: Could not load UnityPlayerActivity");
                return;
            }

            var intentNodes = activityNode.SelectNodes(
                $"intent-filter[action[@android:name=\"android.intent.action.VIEW\"] and data[starts-with(@android:host, '{PlayerAccountSettings.DeepLinkUriHostPrefix}')]]",
                nsmgr);

            if (intentNodes?.Count > 0)
            {
                foreach (XmlNode node in intentNodes)
                {
                    activityNode.RemoveChild(node);
                }
            }

            var uriHost = PlayerAccountSettings.UseCustomUri ? PlayerAccountSettings.DeepLinkUriHostPrefix : PlayerAccountSettings.DeepLinkUriHostPrefix + Application.cloudProjectId;
            activityNode.AppendChild(activityNode.OwnerDocument.ImportNode(BuildeNode($@"
          <intent-filter  xmlns:android=""http://schemas.android.com/apk/res/android"">
            <action android:name=""android.intent.action.VIEW"" />
            <category android:name=""android.intent.category.DEFAULT"" />
            <category android:name= ""android.intent.category.BROWSABLE"" />
            <data android:scheme=""{PlayerAccountSettings.DeepLinkUriScheme}"" android:host=""{uriHost}"" />
          </intent-filter>
        "), true));

            document.Save(manifestFilename);
        }

        XmlNode BuildeNode(string text)
        {
            var doc = new XmlDocument();
            doc.LoadXml(text);

            return doc.DocumentElement;
        }

        string JoinPaths(string[] parts)
        {
            var path = "";
            foreach (var part in parts)
            {
                path = Path.Combine(path, part);
            }

            return path;
        }
    }
}
#endif
