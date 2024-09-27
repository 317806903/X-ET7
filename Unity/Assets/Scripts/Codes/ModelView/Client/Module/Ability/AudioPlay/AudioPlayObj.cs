using System.Collections.Generic;
using UnityEngine;

namespace ET.Ability.Client
{
    [ComponentOf]
	public class AudioPlayObj: Entity, IAwake, IDestroy, IFixedUpdate
    {
        public GameObject go;

        ///<summary>
        ///剩余多久，单位：秒
        ///</summary>
        public float duration;

        ///<summary>
        ///是否是一个永久的effect，永久的duration不会减少，但是timeElapsed还会增加
        ///</summary>
        public bool permanent;

        ///<summary>
        ///effect已经存在了多少时间了，单位：秒
        ///</summary>
        public float timeElapsed = 0.00f;

    }
}