using UnityEngine;
using UnityEngine.UI;

/// 用与画线 修改了DebugUILine 的代码
public class DrawRaycastor : MonoBehaviour
{
#if UNITY_EDITOR
    static Vector3[] fourCorners = new Vector3[4];
    void OnDrawGizmos()
    {
        var raycastors = transform.GetComponentsInChildren<Graphic>();
        foreach (Graphic g in raycastors)
        {
            if (g.raycastTarget)
            {
                RectTransform rectTransform = g.transform as RectTransform;
                rectTransform.GetWorldCorners(fourCorners);
                Gizmos.color = Color.blue;
                for (int i = 0; i < 4; i++)
                    Gizmos.DrawLine(fourCorners[i], fourCorners[(i + 1) % 4]);

            }
        }
    }
#endif
}