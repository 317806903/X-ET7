using System;
using BlurBackground;
using UnityEngine;

namespace ET.Client
{
	[ComponentOf(typeof(Scene))]
	public class ARSessionComponent : Entity, IAwake, ILateUpdate, IDestroy
	{
		public GameObject ARSessoinGo;
		public Transform StaticMeshTran;
		public Transform QrCodeImageTran;
		public Camera ARCamera;
		public GameObject ScaleARCameraGo;
		public Camera ScaleARCamera;
		public Camera CurARCamera { get; set; }
		public TranslucentImageSource translucentImageSource;

		public Action OnMenuCancelCallBack;
		public Action OnMenuFinishedCallBack;
		public Action OnMenuCreateSceneCallBack;
		public Action OnMenuExitSceneCallBack;
		public Action OnMenuLoadRecentSceneCallBack;
		public Action<string> OnMenuJoinSceneCallBack;
		public Func<(bool, string)> OnRequestQRCodeExtraData;
	}
}
