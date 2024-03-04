using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class TransparentSetter: MonoBehaviour
    {
        Material[] mOriMat = null;

        public void SetTransparent(bool transparent, float alpha)
        {
            Renderer renderer = GetComponent<MeshRenderer>();
            if (renderer == null)
            {
                renderer = GetComponent<SkinnedMeshRenderer>();
            }
            if (renderer == null)
            {
                return;
            }

            if (mOriMat == null)
            {
                mOriMat = renderer.materials;
            }

            if (transparent)
            {
                Material[] mats = renderer.materials;
                Material newMat;
                for(int i =0; i < mats.Length; i++)
                {
                    if (mOriMat[i] == null)
                    {
                        mats[i] = null;
                        continue;
                    }
                    newMat = Instantiate(mOriMat[i]);
                    newMat.SetFloat("_Surface", 1.0f);
                    newMat.SetFloat("_Blend", 0f);
                    newMat.SetFloat("_BlendModePreserveSpecular", 0f);
                    newMat.SetFloat("_ZWrite", 0.0f);
                    newMat.SetFloat("_SrcBlend", 5.0f);
                    newMat.SetFloat("_DstBlend", 10.0f);
                    newMat.SetFloat("_SrcBlendAlpha", 5.0f);
                    newMat.SetFloat("_DstBlendAlpha", 10.0f);
                    if(newMat.HasProperty("_BaseColor"))
                    {
                        newMat.SetColor("_BaseColor", new Color(1f, 1f, 1f, alpha));
                    }else{
                        newMat.SetColor("_Color", new Color(1f, 1f, 1f, alpha));
                    }
                    newMat.SetFloat("_QueueOffset", 1000);
                    mats[i] = newMat;
                }
                renderer.materials = mats;
            }
            else
            {
                Material[] mats = renderer.materials;
                for(int i =0; i < mats.Length; i++)
                {
                    if(mats[i] != null)
                    {
                        Destroy(mats[i]);
                    }
                }
                renderer.materials = mOriMat;
            }
        }
    }
}