using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
namespace ET
{
    [ExecuteInEditMode]
    public class DecalUpdate : MonoBehaviour
    {
        public Transform mTarget = null;

        DecalProjector mDP = null;

        void OnEnable()
        {
            mDP = GetComponent<DecalProjector>();

        }
        void Update()
        {
            if (mTarget != null)
            {
                mDP.material.SetFloat("_PosY", mTarget.transform.position.y);
            }
        }
    }

}