using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowARCamera : MonoBehaviour
{
    private Camera ARCamera;
    private Camera curCamera;
    private float arScale;
    private bool init = false;


    public void OnEnable()
    {
        this.curCamera = this.GetComponent<Camera>();
        UnityEngine.Application.onBeforeRender += OnBeforeRendererUpdate;
    }

    public void OnDisable()
    {
        UnityEngine.Application.onBeforeRender -= OnBeforeRendererUpdate;
    }

    public void ResetARCamera()
    {
        if (this.ARCamera == null)
        {
            return;
        }

        if (this.init)
        {
            this.ARCamera.cullingMask = this.curCamera.cullingMask;
        }
        this.ARCamera = null;
        this.gameObject.SetActive(false);
    }

    public void SetARCamera(Camera ARCamera, float arScale)
    {
        this.gameObject.SetActive(true);
        this.ARCamera = ARCamera;
        this.arScale = arScale;

        if (this.init == false)
        {
            this.curCamera.cullingMask = ARCamera.cullingMask;
            this.init = true;
        }
        ARCamera.cullingMask = 0;
        this.curCamera.fieldOfView = ARCamera.fieldOfView;
        this.curCamera.nearClipPlane = ARCamera.nearClipPlane;
        this.curCamera.farClipPlane = ARCamera.farClipPlane;
    }

    public void ChkFieldOfViewChg()
    {
        if (this.ARCamera == null)
        {
            return;
        }

        if (this.curCamera.fieldOfView == this.ARCamera.fieldOfView)
        {
            return;
        }

        this.curCamera.fieldOfView = this.ARCamera.fieldOfView;
    }

    [BeforeRenderOrder(1000)]
    void OnBeforeRendererUpdate()
    {
        if (this.ARCamera == null)
        {
            return;
        }

        this.ChkFieldOfViewChg();

        this.transform.position = this.ARCamera.transform.position * this.arScale;
        this.transform.rotation = this.ARCamera.transform.rotation;
    }
}
