using Unity.Mathematics;
using UnityEngine;

namespace ET.Client
{
	[FriendOf(typeof(ARSessionComponent))]
	public static class ARSessionHelper
	{
		public static bool ChkARCameraEnable(Scene scene)
		{
			ARSessionComponent arSessionComponent = GetARSession(scene, false);
			if (arSessionComponent == null)
			{
				return false;
			}
			return arSessionComponent.ChkARCameraEnable();
		}

		public static ARSessionComponent GetARSession(Scene scene, bool create = true)
		{
			Scene currentScene = null;
			Scene clientScene = null;
			if (scene == scene.ClientScene())
			{
				currentScene = scene.GetComponent<CurrentScenesComponent>().Scene;
				clientScene = scene;
			}
			else
			{
				currentScene = scene;
				clientScene = scene.ClientScene();
			}
			if (clientScene == null)
			{
				return null;
			}

			ARSessionComponent arSessionComponent = clientScene.GetComponent<ARSessionComponent>();
			if (arSessionComponent == null)
			{
				if (create)
				{
					arSessionComponent = clientScene.AddComponent<ARSessionComponent>();
				}
			}
			// CurrentScenesComponent currentScenesComponent = clientScene.GetComponent<CurrentScenesComponent>();
			// ARSessionComponent arSessionComponent = currentScenesComponent.GetComponent<ARSessionComponent>();
			// if (arSessionComponent == null)
			// {
			// 	if (create)
			// 	{
			// 		arSessionComponent = currentScenesComponent.AddComponent<ARSessionComponent>();
			// 	}
			// }
			return arSessionComponent;
		}

		public static Camera GetMainCamera(Scene scene)
		{
			ARSessionComponent arSessionComponent = GetARSession(scene);
			return arSessionComponent.CurARCamera;
		}

		public static bool ChkARSceneStatusCompleted(Scene scene)
		{
			ARSessionComponent arSessionComponent = GetARSession(scene);
			return arSessionComponent.ChkARSceneStatusCompleted();
		}

		public static void SetScaleARCamera(Scene scene, float fScaleAR)
		{
			ARSessionComponent arSessionComponent = GetARSession(scene);
			arSessionComponent.SetScaleARCamera(fScaleAR, true);
		}

		public static float GetScaleAR(Scene scene)
		{
			ARSessionComponent arSessionComponent = GetARSession(scene);
			float arScale = arSessionComponent.arScale;
			return arScale;
		}

		public static void ResetMainCamera(Scene scene, bool isARCamera, bool isQuickSet = true)
		{
			ARSessionComponent arSessionComponent = GetARSession(scene, false);
			if (arSessionComponent == null)
			{
				return;
			}
			arSessionComponent.ResetMainCamera(isARCamera, isQuickSet);
		}

		public static string GetARSceneId(Scene scene)
		{
			ARSessionComponent arSessionComponent = GetARSession(scene);
			if (arSessionComponent == null)
			{
				return "";
			}
			return arSessionComponent.GetARSceneId();
		}

		public static string GetARSceneMeshId(Scene scene)
		{
			ARSessionComponent arSessionComponent = GetARSession(scene);
			if (arSessionComponent == null)
			{
				return "";
            }
            return arSessionComponent.GetARSceneMeshId();
        }

        public static GameObject GetSceneMeshObj(Scene scene)
        {
            ARSessionComponent arSessionComponent = GetARSession(scene);
            if (arSessionComponent == null)
            {
                return null;
            }
            return arSessionComponent.GetSceneMeshObj();
        }

        public static void SetSceneMeshObj(Scene scene, Mesh mesh)
        {
            ARSessionComponent arSessionComponent = GetARSession(scene);
            if (arSessionComponent == null)
            {
                return;
            }
            arSessionComponent.SetSceneMeshObj(mesh);
        }

        public static Transform GetSpatialAnchorTransform(Scene scene)
        {
	        ARSessionComponent arSessionComponent = GetARSession(scene);
	        if (arSessionComponent == null)
	        {
		        return null;
	        }
	        return arSessionComponent.GetSpatialAnchorTransform();
        }

        public static Vector3 GetSpatialAnchorPosition(Scene scene)
        {
            ARSessionComponent arSessionComponent = GetARSession(scene);
            if (arSessionComponent == null)
            {
                return Vector3.zero;
            }
            return arSessionComponent.GetSpatialAnchorPosition();
        }

        public static Quaternion GetSpatialAnchorRotation(Scene scene)
        {
            ARSessionComponent arSessionComponent = GetARSession(scene);
            if (arSessionComponent == null)
            {
                return Quaternion.identity;
            }
            return arSessionComponent.GetSpatialAnchorRotation();
        }

		public static string GetArcadeARSceneId()
		{
			string key = "ArcadeARSceneId";
			if (PlayerPrefs.HasKey(key) == false)
			{
				return "";
			}
			string arcadeARSceneId = PlayerPrefs.GetString(key);
			return arcadeARSceneId;
		}

		public static string GetArcadeARSceneMeshId()
		{
			return GetArcadeARSceneId();
		}

		public static void SetArcadeARSceneId(string arcadeARSceneId)
		{
			string key = "ArcadeARSceneId";
			PlayerPrefs.SetString(key, arcadeARSceneId);
		}

		public static string GetARMeshDownLoadUrl(Scene scene)
		{
			ARSessionComponent arSessionComponent = GetARSession(scene);
			string _ARMeshDownLoadUrl = arSessionComponent.GetARMeshDownLoadUrl();
			return _ARMeshDownLoadUrl;
		}

        public static async ETTask<byte[]> GetARMeshClientObjAsync(Scene scene)
		{
			ARSessionComponent arSessionComponent = GetARSession(scene);
			byte[] _ARMeshClientObj = arSessionComponent.GetARMeshClientObj();
			return _ARMeshClientObj;
		}

		public static string GetAREntranceType(Scene scene)
		{
			ARSessionComponent arSessionComponent = GetARSession(scene);
			if (arSessionComponent == null)
			{
				return "";
			}
			string entranceType = arSessionComponent.EntranceType;
			return entranceType;
		}

		public static void ResetAREntranceType(Scene scene)
		{
			ARSessionComponent arSessionComponent = GetARSession(scene);
			if (arSessionComponent == null)
			{
				return;
			}
			arSessionComponent.EntranceType = "";
		}

		public static void TriggerShowQrCode(Scene scene)
		{
			ARSessionComponent arSessionComponent = GetARSession(scene);
            if (arSessionComponent == null)
            {
                return;
            }
			arSessionComponent.TriggerShowQrCode();
		}

		public static void TriggerReScan(Scene scene)
		{
			ARSessionComponent arSessionComponent = GetARSession(scene);
            if (arSessionComponent == null)
            {
                return;
            }
			arSessionComponent.TriggerReScan();
		}

		public static void ShowARMesh(Scene scene, ARSessionComponent.ArMeshVisibility show)
		{
			if (ChkARCameraEnable(scene) == false)
			{
				return;
			}
			ARSessionComponent arSessionComponent = GetARSession(scene);
            if (arSessionComponent == null)
            {
                return;
            }
			arSessionComponent.ShowARMesh(show);
		}

		public static bool ChkARMirrorSceneUIShow(Scene scene)
		{
			ARSessionComponent arSessionComponent = GetARSession(scene);
			if (arSessionComponent != null && arSessionComponent.ChkARCameraEnable())
			{
				return arSessionComponent.ChkARMirrorSceneUIShow();
			}
			return false;
		}

		public static void HideMirrorSceneUIMenu(Scene scene)
		{
			ARSessionComponent arSessionComponent = GetARSession(scene);
			arSessionComponent.HideMenu();
		}

		public static async ETTask SetARRoomInfoAsync(Scene scene)
		{
			float arMapScale = ARSessionHelper.GetScaleAR(scene);
			if (arMapScale == 0)
			{
				arMapScale = 30;
			}

			bool isGetMeshFromClient = ChannelSettingComponent.Instance.ChkIsGetMeshFromClient();
			ARMeshType arMeshType = ARMeshType.FromRemoteURL_DM;
#if UNITY_EDITOR
#if Platform_Mobile
			arMeshType = ARMeshType.FromRemoteURL_DM;
#elif Platform_Quest
			arMeshType = ARMeshType.FromClientObj;
#elif Platform_AVP
			arMeshType = ARMeshType.FromClientObj;
#else
			arMeshType = ARMeshType.FromRemoteURL_DM;
#endif
#else
			if (isGetMeshFromClient)
			{
				arMeshType = ARMeshType.FromClientObj;
			}
			else
			{
#if Platform_Mobile
				arMeshType = ARMeshType.FromRemoteURL_DM;
#elif Platform_Quest
				arMeshType = ARMeshType.FromRemoteURL_AliyunOSS;
#elif Platform_AVP
				arMeshType = ARMeshType.FromRemoteURL_AliyunOSS;
#else
				arMeshType = ARMeshType.FromRemoteURL_AliyunOSS;
#endif
			}
#endif

			string arSceneId = ARSessionHelper.GetARSceneId(scene);
			string arSceneMeshId = ARSessionHelper.GetARSceneMeshId(scene);
			string arMeshDownLoadUrl = "";
			byte[] arMeshBytes = null;

			if (arMeshType == ARMeshType.FromRemoteURL_DM)
			{
				arMeshDownLoadUrl = ARSessionHelper.GetARMeshDownLoadUrl(scene);
			}
			else if (arMeshType == ARMeshType.FromRemoteURL_AliyunOSS)
			{
				byte[] bytes = await ET.GamePlayHelper.ReadMeshFileBytes(arSceneMeshId);
				if (bytes == null)
				{
					bytes = await ARSessionHelper.GetARMeshClientObjAsync(scene);
					await ET.GamePlayHelper.WriteMeshFileBytes(arSceneMeshId, bytes);
				}
				if (ARSessionHelper.ChkAliyunOSSFileExist(arSceneMeshId))
				{
					arMeshDownLoadUrl = ARSessionHelper.GetAliyunOSSDownLoadURL(arSceneMeshId);
				}
				else
				{
					arMeshDownLoadUrl = await ARSessionHelper.UpLoadARMeshToAliyun(scene, arSceneMeshId, bytes);
				}
			}
			else if (arMeshType == ARMeshType.FromClientObj)
			{
				byte[] bytes = await ET.GamePlayHelper.ReadMeshFileBytes(arSceneMeshId);
				if (bytes == null)
				{
					bytes = await ARSessionHelper.GetARMeshClientObjAsync(scene);
					await ET.GamePlayHelper.WriteMeshFileBytes(arSceneMeshId, bytes);
				}
				arMeshBytes = bytes;
			}

			bool result = await RoomHelper.SetARRoomInfoAsync(scene, arMapScale, arMeshType, arSceneId, arSceneMeshId, arMeshDownLoadUrl, arMeshBytes);
			if (result)
			{
			}
		}

		public static bool ChkAliyunOSSFileExist(string arSceneMeshId)
		{
			string ossPath = GetAliyunOSSPath(arSceneMeshId);
			return AliyunOSSHelper.ExistFile(ossPath);
		}

		public static string GetAliyunOSSPath(string arSceneMeshId)
		{
			string ossPath = $"ARMeshData/{arSceneMeshId}";
			return ossPath;
		}

		public static string GetAliyunOSSDownLoadURL(string arSceneMeshId)
		{
			string arMeshDownLoadUrl = AliyunOSSHelper.GetAliyunOSSDownLoadURL(GetAliyunOSSPath(arSceneMeshId));
			return arMeshDownLoadUrl;
		}

		public static async ETTask<string> UpLoadARMeshToAliyun(Scene scene, string arSceneMeshId, byte[] bytes)
		{
			bool isExist = ChkAliyunOSSFileExist(arSceneMeshId);
			string arMeshDownLoadUrl;
			if (isExist == false)
			{
				string ossPath = GetAliyunOSSPath(arSceneMeshId);
				arMeshDownLoadUrl = await AliyunOSSHelper.UpLoadBytes(bytes, ossPath);
			}
			else
			{
				arMeshDownLoadUrl = GetAliyunOSSDownLoadURL(arSceneMeshId);
			}

			return arMeshDownLoadUrl;
		}

		public static async ETTask<byte[]> DownLoadARMeshFromAliyun(Scene scene, string arSceneMeshId)
		{
			byte[] bytes = await ET.GamePlayHelper.ReadMeshFileBytes(arSceneMeshId);
			if (bytes == null)
			{
				string arMeshDownLoadUrl = GetAliyunOSSDownLoadURL(arSceneMeshId);
				bytes = await ET.GamePlayHelper.DownloadFileBytesAsync(scene, arMeshDownLoadUrl);
				if (bytes == null)
				{
					return null;
				}
				await ET.GamePlayHelper.WriteMeshFileBytes(arSceneMeshId, bytes);
			}

			return bytes;
		}

		public static async ETTask CreateMeshFromAliyun(Scene scene, string arSceneMeshId)
		{
			byte[] bytes = await DownLoadARMeshFromAliyun(scene, arSceneMeshId);
			if (bytes == null)
			{
				return;
			}

			bytes = ZipHelper.Decompress(bytes);
			string content = bytes.ToString();
			ET.LoadMesh.ObjMesh objInstace = new ET.LoadMesh.ObjMesh();
			objInstace = objInstace.LoadFromObj(content);

			Transform spatialAnchorTran = ARSessionHelper.GetSpatialAnchorTransform(scene);
			for (int i = 0; i < objInstace.VertexArray.Length; i++)
			{
				quaternion qTmp = quaternion.Euler(math.radians(new float3(0, spatialAnchorTran.eulerAngles.y, 0)));
				float3 pointWhenWorld = math.mul(qTmp, objInstace.VertexArray[i]);
				float3 pointWhenB = math.mul(math.inverse(spatialAnchorTran.rotation), pointWhenWorld);
				objInstace.VertexArray[i] = pointWhenB;
			}

			Mesh mesh = UIManagerHelper.CreateMesh(objInstace.VertexArray, objInstace.TriangleArray, objInstace.NormalArray, objInstace.UVArray, 1);
			ARSessionHelper.SetSceneMeshObj(scene, mesh);
			GameObject go = ARSessionHelper.GetSceneMeshObj(scene);
			go.layer = LayerMask.NameToLayer("Map");
		}

		public static async ETTask CreateMeshFromClientObj(Scene scene, byte[] bytes)
		{
			bytes = ZipHelper.Decompress(bytes);
			string content = System.Text.Encoding.UTF8.GetString(bytes);

			ET.LoadMesh.ObjMesh objInstace = new ET.LoadMesh.ObjMesh();
			objInstace = objInstace.LoadFromObj(content);

			if (ARSessionHelper.GetSceneMeshObj(scene) == null)
			{
				UIManagerHelper.CreateMeshAndGameObject(objInstace.VertexArray, objInstace.TriangleArray, objInstace.NormalArray, objInstace.UVArray, 1);
				return;
			}

			Transform spatialAnchorTran = ARSessionHelper.GetSpatialAnchorTransform(scene);
			for (int i = 0; i < objInstace.VertexArray.Length; i++)
			{
				quaternion qTmp = quaternion.Euler(math.radians(new float3(0, spatialAnchorTran.eulerAngles.y, 0)));
				float3 pointWhenWorld = math.mul(qTmp, objInstace.VertexArray[i]);
				float3 pointWhenB = math.mul(math.inverse(spatialAnchorTran.rotation), pointWhenWorld);
				objInstace.VertexArray[i] = pointWhenB;
			}

			Mesh mesh = UIManagerHelper.CreateMesh(objInstace.VertexArray, objInstace.TriangleArray, objInstace.NormalArray, objInstace.UVArray, 1);

			ARSessionHelper.SetSceneMeshObj(scene, mesh);
			GameObject go = ARSessionHelper.GetSceneMeshObj(scene);
			go.layer = LayerMask.NameToLayer("Map");
		}
	}
}
