using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using UnityEngine;
using Debug = UnityEngine.Debug;

public enum TextMoveType
{
    None,
    Up,
    Down,
    Left,
    Right,
    LeftUp,
    LeftDown,
    RightUp,
    RightDown,
    LeftParabola,
    RightParabola
}

public class ShootTextProManager : MonoBehaviour
{
    public List<GameObject> normalNumber = new();
    public GameObject operatorPlusGo;
    public GameObject operatorMinusGo;

    private List<ShootTextComponent> handleShootTextGroup = new List<ShootTextComponent>();
    private Queue<ShootTextInfo> waitShootTextGroup = new Queue<ShootTextInfo>();
    private List<ShootTextComponent> waitDestoryGroup = new List<ShootTextComponent>();

    private Dictionary<string, Stack<GameObject>> pool = new();
    private Dictionary<int, Stack<GameObject>> poolDamage = new();

    private Transform shootTextCanvas = null;
    public Transform ShootTextCanvas
    {
        get
        {
            return shootTextCanvas;
        }
        set { shootTextCanvas = value; }
    }
    private Camera shootTextCamera = null;
    public Camera ShootTextCamera
    {
        get
        {
            return shootTextCamera;
        }
        set
        {
            shootTextCamera = value;
        }
    }

    [Header("渐隐曲线")]
    [SerializeField]
    private AnimationCurve shootTextCure = null;
    [Header("大小缩放曲线")]
    [SerializeField]
    private AnimationCurve shootTextScaleCure = null;
    [Header("抛物线曲线")]
    [SerializeField]
    private AnimationCurve shootParabolaCure = null;

    [Header("最大等待弹射数量")]
    [SerializeField]
    private int MaxWaitCount = 20;
    [Header("超过此数量弹射加速")]
    [SerializeField]
    private int accelerateThresholdValue = 10;
    [Header("一次创建数量")]
    [SerializeField]
    private int createCountOnce = 10;
    [Header("加速弹射速率因子")]
    [SerializeField]
    private float accelerateFactor = 2;
    private bool isAccelerate = false;
    [Header("默认创建周期：秒/一次")]
    [SerializeField]
    private float updateCreatDefualtTime = 0.2f;
    //[SerializeField]
    private float updateCreatTime = 0.2f;
    //[SerializeField]
    private float updateCreatTempTime;
    [Header("从移动到消失的生命周期 单位：秒")]
    [SerializeField]
    private float moveLifeTime = 1.0f;

    [Header("远近缩放保持最小高度差")]
    [SerializeField]
    private float shootTextMinHeight = 50f;
    [Header("远近缩放保持最大高度差")]
    [SerializeField]
    private float shootTextMaxHeight = 200f;
    [Header("远近缩放因子")]
    [SerializeField]
    private float shootTextScaleFactor = 0.6f;

    [Header("等待指定时间后开始移动")]
    [SerializeField]
    private float delayMoveTime = 0.3f;

    public float DelayMoveTime { get { return delayMoveTime; } set { delayMoveTime = value; } }
    [Range(-4, 4)]
    [Header("初始化位置垂直偏移量")]
    [SerializeField]
    private float initializedVerticalPositionOffset = 0.8f;
    [Range(-4, 4)]
    [Header("初始化位置水平偏移量")]
    [SerializeField]
    private float initializedHorizontalPositionOffset = 0.0f;
    [Header("垂直移动速率")]
    [Range(0, 20)]
    [SerializeField]
    private float verticalMoveSpeed = 10;
    [Header("水平移动速率")]
    [Range(0, 20)]
    [SerializeField]
    private float horizontalMoveSpeed = 10;

    [Header("字体移动类型")]
    public TextMoveType textMoveType;

    /// <summary>
    /// 一次飘字UI父节点
    /// </summary>
    public GameObject shootTextPrefab = null;
    void Start()
    {
    }

    public void Init()
    {
        //shootTextCure = new AnimationCurve(new Keyframe[] { new Keyframe(0, 1f), new Keyframe(moveLifeTime, 0f) });
        //shootTextPrefab = Resources.Load<GameObject>("Prefabs/ShootText_Pure");
        updateCreatTempTime = updateCreatTime;

        for (int i = 0; i < this.normalNumber.Count; i++)
        {
            GameObject go = this.normalNumber[i];
            if (go != null)
            {
                if (this.ChkPoolExist($"{i}") == false)
                {
                    this.InitPool($"{i}", Instantiate(go));
                }
            }
        }

        if (this.ChkPoolExist("shootTextPrefab") == false)
        {
            this.InitPool("shootTextPrefab", Instantiate(this.shootTextPrefab));
        }
        if (this.ChkPoolExist("+") == false)
        {
            this.InitPool("+", Instantiate(this.operatorPlusGo));
        }
        if (this.ChkPoolExist("-") == false)
        {
            this.InitPool("-", Instantiate(this.operatorMinusGo));
        }
    }

    public void Clear()
    {
        for (int i = 0; i < handleShootTextGroup.Count; i++)
        {
            ShootTextComponent shootTextComponent = handleShootTextGroup[i];
            if (shootTextComponent.gameObject != null)
            {
                GameObject.Destroy(shootTextComponent.gameObject);
            }
        }
        handleShootTextGroup.Clear();

        waitShootTextGroup.Clear();
        waitDestoryGroup.Clear();
    }

    public void ClearPool()
    {
        foreach (var item in this.pool.Values)
        {
            foreach (GameObject go in item)
            {
                GameObject.Destroy(go);
            }
        }
        this.pool.Clear();

        foreach (var item in this.poolDamage.Values)
        {
            foreach (GameObject go in item)
            {
                GameObject.Destroy(go);
            }
        }
        this.poolDamage.Clear();
    }

    void Update()
    {
        if (ShootTextCanvas == null)
        {
            return;
        }

        float deltaTime = Time.deltaTime;

        //操作handleShootTextGroup中移动
        for (int i = 0; i < handleShootTextGroup.Count; i++)
        {
            ShootTextComponent shootTextComponent = handleShootTextGroup[i];
            if (shootTextComponent.gameObject.activeSelf == false)
            {
                continue;
            }
            if (shootTextComponent.rectTransform == null)
            {
                continue;
            }

            Vector3 shootTextCreatPosition = Vector3.zero;
            shootTextCreatPosition = shootTextComponent.getShootTextTopPoint();
            shootTextCreatPosition.x += (float)shootTextComponent.initializedHorizontalPositionOffset;
            shootTextCreatPosition.y += (float)shootTextComponent.initializedVerticalPositionOffset;

            Vector2 anchors = ShootTextCamera.WorldToViewportPoint(shootTextCreatPosition);//飘字初始锚点位置
            Vector2 changeAnchoredPosition = new Vector2((float)(anchors.x + shootTextComponent.xMoveOffeset), (float)(anchors.y + shootTextComponent.yMoveOffeset));//飘字这一帧所在位置

            //设定锚点
            shootTextComponent.rectTransform.anchorMax = anchors;
            shootTextComponent.rectTransform.anchorMin = anchors;
            //设置相对坐标
            shootTextComponent.rectTransform.anchoredPosition = changeAnchoredPosition;

            if (shootTextComponent.delayMoveTime <= Time.time)//允许移动操作
            {
                shootTextComponent.isMove = true;
            }
            if (shootTextComponent.moveLifeTime <= Time.time)
            {
                shootTextComponent.isMoveDone = true;
            }

            //处理缩放
            shootTextComponent.scaleCurveTime += deltaTime;
            float scaleCurve = shootTextScaleCure.Evaluate((float)(shootTextComponent.scaleCurveTime));
            shootTextComponent.ChangeScaleCurve(scaleCurve);

            //处理近大远小
            double objectHigh = Math.Min(shootTextMaxHeight, Math.Max(shootTextMinHeight, ModelInScreenHigh(shootTextComponent)));
            double scale = (objectHigh / 100) * shootTextScaleFactor;

            shootTextComponent.ChangeScale(scale);
            double xMoveOffeset = horizontalMoveSpeed * deltaTime * objectHigh;
            double yMoveOffeset = verticalMoveSpeed * deltaTime * objectHigh;

            if (shootTextComponent.isMove == true)//处理位置信息
            {
                switch (shootTextComponent.moveType)
                {
                    case TextMoveType.None:
                        break;
                    case TextMoveType.Up:
                        {
                            shootTextComponent.yMoveOffeset += yMoveOffeset;
                        }
                        break;
                    case TextMoveType.Down:
                        {
                            shootTextComponent.yMoveOffeset -= yMoveOffeset;
                        }
                        break;
                    case TextMoveType.Left:
                        {
                            shootTextComponent.xMoveOffeset -= xMoveOffeset;
                        }
                        break;
                    case TextMoveType.Right:
                        {
                            shootTextComponent.xMoveOffeset += xMoveOffeset;
                        }
                        break;
                    case TextMoveType.LeftUp:
                        {
                            shootTextComponent.xMoveOffeset -= xMoveOffeset;
                            shootTextComponent.yMoveOffeset += yMoveOffeset;
                        }
                        break;
                    case TextMoveType.LeftDown:
                        {
                            shootTextComponent.xMoveOffeset -= xMoveOffeset;
                            shootTextComponent.yMoveOffeset -= yMoveOffeset;
                        }
                        break;
                    case TextMoveType.RightUp:
                        {
                            shootTextComponent.xMoveOffeset += xMoveOffeset;
                            shootTextComponent.yMoveOffeset += yMoveOffeset;
                        }
                        break;
                    case TextMoveType.RightDown:
                        {
                            shootTextComponent.xMoveOffeset += xMoveOffeset;
                            shootTextComponent.yMoveOffeset -= yMoveOffeset;

                        }
                        break;
                    case TextMoveType.LeftParabola:
                        {
                            float parabola = shootParabolaCure.Evaluate((float)(shootTextComponent.fadeCurveTime / moveLifeTime));
                            shootTextComponent.xMoveOffeset -= xMoveOffeset;
                            shootTextComponent.yMoveOffeset += yMoveOffeset + parabola;
                        }
                        break;
                    case TextMoveType.RightParabola:
                        {
                            float parabola = shootParabolaCure.Evaluate((float)(shootTextComponent.fadeCurveTime / moveLifeTime));
                            shootTextComponent.xMoveOffeset += xMoveOffeset;
                            shootTextComponent.yMoveOffeset += yMoveOffeset + parabola;
                        }
                        break;
                    default:
                        break;
                }
            }

            //处理渐隐
            if (shootTextComponent.isMove == true)
            {
                shootTextComponent.fadeCurveTime += deltaTime;
                float alpha = shootTextCure.Evaluate((float)(shootTextComponent.fadeCurveTime));
                shootTextComponent.ChangeAlpha(alpha);
            }
            else
            {
                shootTextComponent.ChangeAlpha(1);
            }

            //处理删除对应的飘字
            if (shootTextComponent.isMoveDone)
            {
                waitDestoryGroup.Add(shootTextComponent);
            }
        }

        //是否加速
        isAccelerate = waitShootTextGroup.Count >= accelerateThresholdValue ? true : false;
        if (isAccelerate)
        {
            if (updateCreatTime > 0 && accelerateFactor > 0)
            {
                updateCreatTime = updateCreatTime / accelerateFactor;
            }
        }
        else
        {
            updateCreatTime = updateCreatDefualtTime;
        }

        //创建
        if ((updateCreatTempTime -= deltaTime) <= 0)
        {
            updateCreatTempTime = updateCreatTime;
            int count = this.createCountOnce;
            while (waitShootTextGroup.Count > 0 && count-- > 0)
            {
                GameObject tempObj = InstanceShootText(waitShootTextGroup.Dequeue());
                tempObj.transform.SetParent(ShootTextCanvas, false);
                tempObj.transform.localScale = Vector3.one;
            }
        }

        //删除已经完全消失飘字
        for (int i = 0; i < waitDestoryGroup.Count; i++)
        {
            handleShootTextGroup.Remove(waitDestoryGroup[i]);

            ShootTextComponent shootTextComponent = waitDestoryGroup[i];
            // for (int j = 0; j < shootTextComponent.childTransformGroup.Count; j++)
            // {
            //     RectTransform rect = shootTextComponent.childTransformGroup[j];
            //     Vector2 orgSize = shootTextComponent.sizeDeltaGroup[j];
            //     rect.sizeDelta = orgSize;
            //     this.ReturnToPool(rect.gameObject);
            // }
            shootTextComponent.Clear();

            this.ReturnToPoolDamage(shootTextComponent.valueShow, shootTextComponent.gameObject);
        }

        waitDestoryGroup.Clear();
    }

    public void CreatShootText(bool isNeedShowPreOperator, TextMoveType textMoveType, int showValue, Func<Vector3> getShootTextTopPoint, Func<Vector3> getShootTextButtomPoint)
    {
        if (textMoveType == TextMoveType.None)
        {
            textMoveType = this.textMoveType;
        }
        CreatShootText(isNeedShowPreOperator, showValue, textMoveType, this.delayMoveTime, this.initializedVerticalPositionOffset, this.initializedHorizontalPositionOffset, getShootTextTopPoint, getShootTextButtomPoint);
    }

    public void CreatShootText(bool isNeedShowPreOperator, int showValue, TextMoveType textMoveType, float delayMoveTime, float initializedVerticalPositionOffset, float initializedHorizontalPositionOffset, Func<Vector3> getShootTextTopPoint, Func<Vector3> getShootTextButtomPoint)
    {
        ShootTextInfo shootTextInfo = new();
        shootTextInfo.isNeedShowPreOperator = isNeedShowPreOperator;
        shootTextInfo.showValue = showValue;
        shootTextInfo.moveType = textMoveType;
        shootTextInfo.delayMoveTime = delayMoveTime;
        shootTextInfo.moveLifeTime = this.moveLifeTime;
        shootTextInfo.initializedVerticalPositionOffset = initializedVerticalPositionOffset;
        shootTextInfo.initializedHorizontalPositionOffset = initializedHorizontalPositionOffset;
        shootTextInfo.getShootTextTopPoint = getShootTextTopPoint;
        shootTextInfo.getShootTextButtomPoint = getShootTextButtomPoint;

        CreatShootText(shootTextInfo);
    }

    public void CreatShootText(ShootTextInfo shootTextInfo)
    {
        if (IsAllowAddShootText())
        {
            waitShootTextGroup.Enqueue(shootTextInfo);
        }
        else
        {
            //Debug.LogWarning("数量过多不能添加!!!");
        }
    }

    private GameObject InstanceShootText(ShootTextInfo shootTextInfo)
    {
        GameObject shootText = this.GetFromPoolDamage(shootTextInfo.showValue);
        if (shootText == null)
        {
            shootText = this.GetFromPool("shootTextPrefab");
            //先拼装字体，顺序颠倒会造成组件无法找到对应物体
            BuildNumber(shootTextInfo, shootText.transform);
            ShootTextComponent tempShootTextComponent = shootText.GetComponent<ShootTextComponent>();
            tempShootTextComponent.SetInfo(shootTextInfo.showValue, shootTextInfo, false);
            handleShootTextGroup.Add(tempShootTextComponent);
        }
        else
        {
            ShootTextComponent tempShootTextComponent = shootText.GetComponent<ShootTextComponent>();
            tempShootTextComponent.SetInfo(shootTextInfo.showValue, shootTextInfo, true);
            handleShootTextGroup.Add(tempShootTextComponent);
        }

        return shootText;
    }

    private void BuildNumber(ShootTextInfo shootTextInfo, Transform parent)
    {
        bool isNeedShowPreOperator = shootTextInfo.isNeedShowPreOperator;
        int showValue = shootTextInfo.showValue;
        if (isNeedShowPreOperator)
        {
            GameObject operatorObj = null;
            if (showValue > 0)
            {
                operatorObj = this.GetFromPool("+");
            }
            else
            {
                operatorObj = this.GetFromPool("-");
            }
            operatorObj.transform.SetParent(parent, false);
            operatorObj.transform.localScale = Vector3.one;
        }

        #region 数字
        DealOneByOne(parent, Math.Abs(showValue));

        #endregion
    }

    private List<int> tmp = new();
    private void DealOneByOne(Transform parent, int showValue)
    {
        this.tmp.Clear();
        while (showValue > 9)
        {
            this.tmp.Add(showValue % 10);
            showValue = showValue / 10;
        }
        this.tmp.Add(showValue);
        for (int i = this.tmp.Count - 1; i >= 0; i--)
        {
            int num = this.tmp[i];
            GameObject numberObj = this.GetFromPool(GetStrByInt(num));
            numberObj.transform.SetParent(parent, false);
            numberObj.transform.localScale = Vector3.one;
        }
        this.tmp.Clear();
    }

    private string GetStrByInt(int num)
    {
        switch (num)
        {
            case 0:
                return "0";
            case 1:
                return "1";
            case 2:
                return "2";
            case 3:
                return "3";
            case 4:
                return "4";
            case 5:
                return "5";
            case 6:
                return "6";
            case 7:
                return "7";
            case 8:
                return "8";
            case 9:
                return "9";
            default:
                return "0";
        }
    }

    private double ModelInScreenHigh(ShootTextComponent shootTextComponent)
    {
        float topValue = ShootTextCamera.WorldToScreenPoint(shootTextComponent.getShootTextTopPoint()).y;
        float bottomValue = ShootTextCamera.WorldToScreenPoint(shootTextComponent.getShootTextButtomPoint()).y;
        return Mathf.Abs(topValue - bottomValue);
    }

    private bool IsAllowAddShootText()
    {
        return waitShootTextGroup.Count < MaxWaitCount;
    }

    private bool ChkPoolExist(string name)
    {
        if (this.pool.TryGetValue(name, out var stack))
        {
            if (stack.Count >= 1)
            {
                return true;
            }
        }
        return false;
    }

    private void InitPool(string name, GameObject go)
    {
        if (this.pool.TryGetValue(name, out var stack) == false)
        {
            stack = new();
            this.pool[name] = stack;
        }
        go.transform.SetParent(this.transform);
        this.transform.localScale = Vector3.zero;
        stack.Push(go);
    }

    private GameObject GetFromPool(string goName)
    {
        if (this.pool.TryGetValue(goName, out var stack))
        {
            if (stack.Count > 1)
            {
                return stack.Pop();
            }
            if (stack.Count == 1)
            {
                GameObject go = stack.Peek();
                go = Instantiate(go);
                go.name = goName;
                return go;
            }
        }

        return null;
    }

    private void ReturnToPool(GameObject go)
    {
        if (this.pool.TryGetValue(go.name, out var stack) == false)
        {
            stack = new();
            this.pool[go.name] = stack;
        }
        go.transform.SetParent(this.transform);
        this.transform.localScale = Vector3.zero;
        stack.Push(go);
    }

    private GameObject GetFromPoolDamage(int valueShow)
    {
        if (this.poolDamage.TryGetValue(valueShow, out var stack))
        {
            if (stack.Count > 1)
            {
                return stack.Pop();
            }
            if (stack.Count == 1)
            {
                GameObject go = stack.Peek();
                go = Instantiate(go);
                return go;
            }
        }

        return null;
    }

    private void ReturnToPoolDamage(int valueShow, GameObject go)
    {
        if (this.poolDamage.TryGetValue(valueShow, out var stack) == false)
        {
            stack = new();
            this.poolDamage[valueShow] = stack;
        }
        go.transform.SetParent(this.transform);
        this.transform.localScale = Vector3.zero;
        stack.Push(go);
    }
}
