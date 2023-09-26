using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{

    public class MouseTrack : MonoBehaviour
    {
        /// <summary>
        /// 获取LineRenderer组件
        /// </summary>
        [Header("获得LineRenderer组件")]
        public LineRenderer lineRenderer;
        //获得鼠标跟踪位置
        private Vector3[] mouseTrackPositions = new Vector3[20];
        private Vector3 headPosition;   //头位置
        private Vector3 lastPosition;   //尾位置
        private int positionCount = 0;  //位置计数
        [Header("设置多远距离记录一个位置")]
        public float distanceOfPositions = 0.01f;
        private bool firstMouseDown = false;    //第一次鼠标点击
        private bool mouseDown = false;     //鼠标点击
        PolygonCollider2D polygonCollider;   //添加多边形碰撞
        void Start()
        {
            polygonCollider = gameObject.GetComponent<PolygonCollider2D>();
        }
        void Update()
        {
            //鼠标点击的时候
            if (Input.GetMouseButtonDown(0))
            {
                polygonCollider.enabled = true;
                lineRenderer.positionCount = 20;
                firstMouseDown = true;
                mouseDown = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                mouseDown = false;

                //ClearColliderAndLineRenderer();
            }
            OnDrawLine();
            firstMouseDown = false;
        }
        //画线
        private void OnDrawLine()
        {
            if (firstMouseDown == true)
            {
                positionCount = 0;
                //头坐标
                headPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition + new Vector3(0, 0, 11));
                lastPosition = headPosition;
            }
            if (mouseDown == true)
            {
                headPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 11));
                //判断头坐标到尾坐标的距离是否大于记录点位
                if (Vector3.Distance(headPosition, lastPosition) > distanceOfPositions)
                {
                    //用于保存位置
                    SavePosition(headPosition);
                    positionCount++;
                }
                lastPosition = headPosition;
            }
            //设置线性渲染器的位置
            SetLineRendererPosition(mouseTrackPositions);
        }
        //保存位置
        private void SavePosition(Vector3 pos)
        {
            pos.z = 0;
            if (positionCount <= 19)
            {
                for (int i = positionCount; i < 20; i++)
                {
                    mouseTrackPositions[i] = pos;
                }
            }
            else
            {
                for (int i = 0; i < 19; i++)
                {
                    mouseTrackPositions[i] = mouseTrackPositions[i + 1];
                }
            }
            mouseTrackPositions[19] = pos;

            //创建碰撞路径
            List<Vector2> colliderPath = GetColliderPath(mouseTrackPositions);
            polygonCollider.SetPath(0, colliderPath.ToArray());
        }
        //计算碰撞体轮廓
        float colliderWidth;
        List<Vector2> pointList2 = new List<Vector2>();
        List<Vector2> GetColliderPath(Vector3[] pointList3)
        {
            //碰撞体宽度
            colliderWidth = lineRenderer.startWidth;
            //Vector3转Vector2
            pointList2.Clear();
            for (int i = 0; i < pointList3.Length; i++)
            {
                pointList2.Add(pointList3[i]);
            }
            //碰撞体轮廓点位
            List<Vector2> edgePointList = new List<Vector2>();
            //以LineRenderer的点位为中心, 沿法线方向与法线反方向各偏移一定距离, 形成一个闭合且不交叉的折线
            for (int j = 1; j < pointList2.Count; j++)
            {
                //当前点指向前一点的向量
                Vector2 distanceVector = pointList2[j - 1] - pointList2[j];
                //法线向量
                Vector3 crossVector = Vector3.Cross(distanceVector, Vector3.forward);
                //标准化, 单位向量
                Vector2 offectVector = crossVector.normalized;
                //沿法线方向与法线反方向各偏移一定距离
                Vector2 up = pointList2[j - 1] + 0.5f * colliderWidth * offectVector;
                Vector2 down = pointList2[j - 1] - 0.5f * colliderWidth * offectVector;
                //分别加到List的首位和末尾, 保证List中的点位可以围成一个闭合且不交叉的折线
                edgePointList.Insert(0, down);
                edgePointList.Add(up);
                //加入最后一点
                if (j == pointList2.Count - 1)
                {
                    up = pointList2[j] + 0.5f * colliderWidth * offectVector;
                    down = pointList2[j] - 0.5f * colliderWidth * offectVector;
                    edgePointList.Insert(0, down);
                    edgePointList.Add(up);
                }
            }
            //返回点位
            return edgePointList;
        }
        //设置线条渲染器位置
        private void SetLineRendererPosition(Vector3[] position)
        {
            lineRenderer.SetPositions(position);
        }
        //用于清除碰撞和线性渲染
        void ClearColliderAndLineRenderer()
        {
            if (polygonCollider)
            {
                polygonCollider.enabled = false;
            }
            lineRenderer.positionCount = 0;
        }
    }
}
