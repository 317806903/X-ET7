using System;
using UnityEngine;

namespace ET.Client
{
	[ComponentOf(typeof(Scene))]
	public class ARSessionComponent : Entity, IAwake, ILateUpdate, IDestroy
	{
		public GameObject ARSessoinGo;
		public Camera ARCamera;
		public Action OnMenuCancelCallBack;
		public Action OnMenuFinishedCallBack;
	}
}
