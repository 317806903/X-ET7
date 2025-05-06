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
        Material mDP_material;

        private Vector3 lastPos;

        void OnEnable()
        {
            mDP = GetComponent<DecalProjector>();
            if (this.mDP != null)
            {
                mDP_material = this.mDP.material;
            }
            if (this.mTarget != null)
            {
                mTarget = this.transform;
            }
        }

        void Update()
        {
            if (mTarget == null || this.mDP == null || this.mDP_material == null)
            {
                return;
            }

            if (this.lastPos.Equals(Vector3.zero) || this.lastPos.Equals(mTarget.position) == false)
            {
                this.lastPos = mTarget.position;
                mDP_material.SetFloat("_PosY", this.lastPos.y);
            }
        }
    }

}