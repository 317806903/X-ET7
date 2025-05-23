﻿using System;
using UnityEngine;

namespace ET.Client
{
	[ComponentOf(typeof(Scene))]
	public class ARSessionComponent : Entity, IAwake, ILateUpdate, IDestroy
	{
		public GameObject ARSessoinGo;
		public Transform StaticMeshTran;
		public Transform QrCodeImageTran;
		public Transform MirrorSceneClassyUI;
		public Camera ARCamera;
		public GameObject ScaleARCameraGo;
		public Camera ScaleARCamera;
		public Camera CurARCamera { get; set; }
		public float arScale;
		public int frameCaptured;
		public int meshFaceCount;
		public int lastScanWarning;

		public string EntranceType;  // Could be "scan", "load" (recent) or "join" (from QR code) or "reconnect" (after recover)
		public string CurrentARSceneId;
        public string CurrentARSceneMeshId; // Room mesh ID
		public bool ARScenePrepared;

		public Action OnMenuCancelCallBack;
		public Action OnMenuFinishedCallBack;
		public Action<bool> OnMenuCreateSceneCallBack;
		public Action OnMenuExitSceneCallBack;
		public Action<bool> OnMenuLoadRecentSceneCallBack;
		public Action<string> OnMenuJoinSceneCallBack;
		public Func<(bool, string)> OnRequestQRCodeExtraData;

		// False for normal scan finish button then confirm/resume flow.
		// True for going back from room page by clicking rescale button then confirm/rescan flow.
		public bool IsReScanMeshing;

		public long Timer;
		public bool targetIsARCamera;

		public long lastSendCameraPosTime;

		public enum ArMeshVisibility
		{
			ColliderOnly,
			Visible,
			TranslucentOcclusion
		}
	}
}
