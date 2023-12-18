using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class TransparentSetter: MonoBehaviour
    {
        Material mOriMat = null;

        public void SetTransparent(bool transparent, float alpha)
        {
            Material mat = null;
            Renderer renderer = GetComponent<MeshRenderer>();
            if (renderer == null)
            {
                renderer = GetComponent<SkinnedMeshRenderer>();
            }
            if (renderer == null)
            {
                return;
            }

            mat = renderer.material;
            if (mat == null)
            {
                return;
            }
            if (mOriMat == null)
            {
                mOriMat = mat;
            }

            if (transparent)
            {
                Material newMat = Instantiate(mOriMat);
                newMat.SetFloat("_Surface", 1.0f);
                newMat.SetFloat("_Blend", 0f);
                newMat.SetFloat("_BlendModePreserveSpecular", 0f);
                newMat.SetFloat("_ZWrite", 0.0f);
                newMat.SetFloat("_SrcBlend", 5.0f);
                newMat.SetFloat("_DstBlend", 10.0f);
                newMat.SetFloat("_SrcBlendAlpha", 5.0f);
                newMat.SetFloat("_DstBlendAlpha", 10.0f);
                newMat.SetColor("_BaseColor", new Color(1f, 1f, 1f, alpha));
                newMat.SetFloat("_QueueOffset", 1000);
                renderer.material = newMat;
            }
            else
            {
                if (renderer.material != mOriMat)
                {
                    Destroy(renderer.material);
                }

                renderer.material = mOriMat;
            }
        }
    }
}