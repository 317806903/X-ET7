using System;
using UnityEngine;

namespace ET
{
    public class FollowGameObject : MonoBehaviour
    {
        public bool isSetPosQuick;
        public Transform followTrans;
        public Vector3 offsetPos;
        public Vector3 lastTargetPos;

        private void Start()
        {
        }

        public bool IsEqualVector3(Vector3 data1, Vector3 data2, float dis = 0.001f)
        {
            if (Math.Abs(data1.x - data2.x) < dis
                && Math.Abs(data1.y - data2.y) < dis
                && Math.Abs(data1.z - data2.z) < dis
               )
            {
                return true;
            }

            return false;
        }

        private void Update()
        {
            if (this.followTrans == null)
            {
                return;
            }

            var followTransPos = this.followTrans.position;
            if (this.lastTargetPos.Equals(Vector3.zero))
            {
                this.lastTargetPos = followTransPos;
                return;
            }
            else if (IsEqualVector3(this.lastTargetPos, followTransPos, 0.001f) == false)
            {
                this.lastTargetPos = followTransPos;
                return;
            }
            else
            {
                var targetPos = this.followTrans.position + this.followTrans.rotation * this.offsetPos;
                if (this.transform.position == targetPos)
                {
                    this.transform.rotation = this.followTrans.rotation;
                }
                else
                {
                    this.transform.rotation = this.followTrans.rotation;
                    if (IsEqualVector3(this.transform.position, targetPos, 0.1f))
                    {
                        this.transform.position = targetPos;
                        return;
                    }
                    float t = 0.999f;
                    if (this.isSetPosQuick)
                    {
                        this.transform.position = targetPos;
                        return;
                    }

                    this.transform.position = Vector3.Lerp(this.transform.position, targetPos, t);
                }
            }
        }
    }
}