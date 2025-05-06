using UnityEngine;
using UnityEngine.Rendering;
namespace ET
{

    [ExecuteAlways]
    public class LightRenderingLayerMaskSetter : MonoBehaviour
    {
        public Light targetLight;
        public int renderingLayer = 1; // MapLayer 是第 1 层，比如 Layer1

        void Start()
        {
            if (targetLight == null)
                targetLight = GetComponent<Light>();

            if (targetLight != null)
            {
                targetLight.renderingLayerMask = (1 << renderingLayer);
            }
        }
    }

}
