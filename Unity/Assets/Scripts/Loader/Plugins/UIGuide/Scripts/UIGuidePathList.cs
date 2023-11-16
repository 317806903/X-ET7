using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.IO;
using ET;
using MongoDB.Bson.Serialization.Attributes;

namespace UIGuide
{
    //下一步骤前(0罩黑,1透明,2无遮罩)
    public enum WaitToNextUIGuideStep
    {
        //0罩黑
        Black = 0,
        //1透明
        Transparent = 1,
        //2无遮罩
        NoMask = 2,
        //3罩黑但是不会产生点击
        BlackButNotClick = 3,
        //4罩黑但是不拦截所有点击
        BlackButNoMask = 4,
    }

    public enum GuideTextType
    {
        None,
        Text,
        Image,
    }

    public enum SpecImgMatchType
    {
        None,
        OrgSize,
        Scale,
    }

    public enum TrigCondition
    {
        None,
        FindNode,
        StaticMethod,
    }

    public enum CurInNextType
    {
        Click,
        Down,
    }

    [Serializable]
    public class UIGuidePath
    {
        [NonSerialized]
        [BsonIgnore]
        public int index;
        public string name;

        public TrigCondition trigEnterCondition;
        [NonSerialized]
        [BsonIgnore]
        public GameObject trigEnterConditionGo;
        public string trigEnterConditionStaticMethod;
        public string trigEnterConditionParam;

        [NonSerialized]
        [BsonIgnore]
        public GameObject go;
        public string hierarchyCanvasPath;
        public string hierarchyGuidePath;

        public GuideTextType guideTextType;
        [NonSerialized]
        [BsonIgnore]
        public Sprite guideTextImg;
        public string guideTextImgPath;
        public string text;

        public SpecImgMatchType specImgMatchType;
        [NonSerialized]
        [BsonIgnore]
        public Sprite specImg;
        public string specImgPath;

        public CurInNextType curInNextType;

        public WaitToNextUIGuideStep waitToNextUIGuideStep;

        [NonSerialized]
        [BsonIgnore]
        public AudioClip audioClip;
        public string audioPath;

        public string guideExecuteStaticMethod;
        public string guideExecuteParam;

        public TrigCondition trigExitCondition;
        [NonSerialized]
        [BsonIgnore]
        public GameObject trigExitConditionGo;
        public string trigExitConditionStaticMethod;
        public string trigExitConditionParam;
    }

    public class UIGuidePathList: ScriptableObject
    {
        public List<UIGuidePath> list;
    }

}