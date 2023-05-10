using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Ability
{
    ///<summary>
    ///角色的“状态”，用来管理当前应该怎么移动、应该怎么旋转、应该怎么播放动画的。
    ///是一个角色的总的“调控中心”。
    ///</summary>
    [ComponentOf(typeof (Unit))]
    public class ChaState: Entity, IAwake, IDestroy
    {
        //收到的来自各方的播放动画的请求
        private List<string> animOrder = new List<string>();

        ///<summary>
        ///角色所处阵营，阵营不同就会对打
        ///</summary>
        public int side = 0;

        ///<summary>
        ///根据tags可以判断出这是什么样的人
        ///</summary>
        public string[] tags = new string[0];
    }
}