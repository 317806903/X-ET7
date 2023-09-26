using System;
using UnityEngine;

namespace ET.Client
{
	[ComponentOf(typeof(Scene))]
	public class ARSessionComponent : Entity, IAwake, ILateUpdate, IDestroy
	{
		public GameObject ARSessoinGo;
		public Transform StaticMeshTran;
		public Camera ARCamera;
		public GameObject ScaleARCameraGo;
		public Camera ScaleARCamera;
		public Camera CurARCamera { get; set; }

		public Action OnMenuCancelCallBack;
		public Action OnMenuFinishedCallBack;
		public Action OnMenuCreateSceneCallBack;
		public Action OnMenuQuitSceneCallBack;
		public Action OnMenuJoinByExistSceneCallBack;
		public Action<string> OnMenuJoinByQRCodeCallBack;
		public Func<(bool, string)> OnGetQRCodeInfo;
	}
}
