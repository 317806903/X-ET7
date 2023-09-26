using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Rendering.Universal;

public class CameraUpCompare : IComparer<Camera>
{
	public int Compare(Camera x, Camera y)
	{
		CameraStackAdd x1 = x.GetComponent<CameraStackAdd>();
		CameraStackAdd y1 = y.GetComponent<CameraStackAdd>();
		if (x1 != null && y1 != null)
		{
			return x1.SortID > y1.SortID ? 1 : -1;
		}
		else {
			return 0;
		}
	}
}

public class CameraStackAdd : MonoBehaviour
{
	public static Camera arBaseCamera;
	public static Camera keepBaseCamera;
	public static Camera curBaseCamera;
	public bool isBaseCamera;
	public bool isARBaseCamera;
	public int SortID = 0;//排序ID

	void OnEnable()
	{
		SetKeepBaseCamera();
		if (this.isBaseCamera)
		{
			SetWhenBaseCamera();
		}
		else
		{
			StartCoroutine(WaitSetWhenOverlayCamera());
			//SetWhenOverlayCamera();
		}
	}
	
	void OnDestroy()
	{
		if (curBaseCamera != null && curBaseCamera == GetComponent<Camera>())
		{
			if (keepBaseCamera != null)
			{
				if (keepBaseCamera.enabled == false)
				{
					keepBaseCamera.enabled = true;
				}
			}
		}
	}
	
	void SetARBaseCamera(Camera curCamera)
	{
		arBaseCamera = curCamera;
	}
	
	void SetKeepBaseCamera()
	{
		if (keepBaseCamera == null)
		{
			GameObject go = GameObject.Find("BaseCamera");
			if (go != null)
			{
				keepBaseCamera = go.GetComponent<Camera>();
				keepBaseCamera.enabled = false;
			}
		}
		else
		{
			if (keepBaseCamera.enabled)
			{
				keepBaseCamera.enabled = false;
			}
		}
	}
	
	void SetKeepBaseCameraStack(List<Camera> cameraList)
	{
		if (keepBaseCamera != null)
		{
			ResetCameraStack(keepBaseCamera, cameraList);
		}
	}
	
	void SetWhenBaseCamera()
	{
		Camera curCamera = GetComponent<Camera>();

		if (curBaseCamera != null && arBaseCamera != null
			 && curBaseCamera == arBaseCamera
			 && curBaseCamera.gameObject.activeInHierarchy == true
			 && curBaseCamera.enabled)
		{
			if (curCamera != null && curCamera != curBaseCamera)
			{
				curCamera.enabled = false;
			}
			return;
		}
		
		if (curCamera != null)
		{
			curCamera.clearFlags = CameraClearFlags.Skybox;
			curCamera.GetUniversalAdditionalCameraData().renderType = CameraRenderType.Base;
		}

		curBaseCamera = curCamera;
		
		if (this.isARBaseCamera)
		{
			SetARBaseCamera(curBaseCamera);
		}
		
		if (keepBaseCamera != null)
		{
			List<Camera> cameraList = keepBaseCamera.GetComponent<UniversalAdditionalCameraData>().cameraStack;
			ResetCameraStack(curBaseCamera, cameraList);
		}
	}

	IEnumerator WaitSetWhenOverlayCamera()
	{
		while (true) {
			if (curBaseCamera != null) {
				break;
			}
			yield return new WaitForEndOfFrame();
		}
		SetWhenOverlayCamera();
		yield return null;
	}

	void SetWhenOverlayCamera()
	{
		if (curBaseCamera == null)
		{
			return;
		}
		if (curBaseCamera.enabled == false)
		{
			return;
		}
		Camera curCamera = GetComponent<Camera>();
		if (curCamera != null)
		{
			curCamera.clearFlags = CameraClearFlags.SolidColor;
			curCamera.GetUniversalAdditionalCameraData().renderType = CameraRenderType.Overlay;
		}
		List<Camera> temp = new List<Camera>();
		temp.Add(curCamera);
		List<Camera> cameraList = curBaseCamera.GetComponent<UniversalAdditionalCameraData>().cameraStack;
		temp.AddRange(cameraList);

		temp.Sort(new CameraUpCompare());

		ResetCameraStack(curBaseCamera, temp);
		SetKeepBaseCameraStack(temp);
	}

	void ResetCameraStack(Camera baseCamera, List<Camera> cameraList)
	{
		baseCamera.GetComponent<UniversalAdditionalCameraData>().cameraStack.Clear();
		HashSet<Camera> hashSetCamera = new HashSet<Camera>();
		for (int i = 0; i < cameraList.Count; i++)
		{
			if (cameraList[i] != null)
			{
				if (hashSetCamera.Contains(cameraList[i]))
				{
					continue;
				}
				else
				{
					hashSetCamera.Add(cameraList[i]);
				}
				cameraList[i].rect = new Rect(baseCamera.rect);
				baseCamera.GetComponent<UniversalAdditionalCameraData>().cameraStack.Add(cameraList[i]);
			}
		}
	}
	
	public static void HideOverlayCamera()
	{
		List<Camera> cameraList = curBaseCamera.GetComponent<UniversalAdditionalCameraData>().cameraStack;
		for (int i = 0; i < cameraList.Count; i++)
		{
			if (cameraList[i] != null)
			{
				cameraList[i].enabled = false;
			}
		}
	}
	public static void ShowOverlayCamera()
	{
		List<Camera> cameraList = curBaseCamera.GetComponent<UniversalAdditionalCameraData>().cameraStack;
		for (int i = 0; i < cameraList.Count; i++)
		{
			if (cameraList[i] != null)
			{
				cameraList[i].enabled = true;
			}
		}
	}
}
