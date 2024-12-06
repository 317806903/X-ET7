using System;
using System.Xml.Schema;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UIGuide;
using Unity.Mathematics;

namespace ET.Client
{
    [FriendOf(typeof (UIGuideStepComponent))]
    public static class UIGuideStepComponentSystem
    {
        [ObjectSystem]
        public class UIGuideStepComponentAwakeSystem: AwakeSystem<UIGuideStepComponent>
        {
            protected override void Awake(UIGuideStepComponent self)
            {
            }
        }

        [ObjectSystem]
        public class UIGuideStepComponentDestroySystem: DestroySystem<UIGuideStepComponent>
        {
            protected override void Destroy(UIGuideStepComponent self)
            {
                if (self.RootTrans != null)
                {
                    self.DelGuideClickInfo();

                    self.RootTrans.gameObject.SetActive(false);
                    self.RootTrans = null;

                    self.curUIGuidePath = null;
                    self.finishedCallBack = null;
                    self.skipCallBack = null;
                }
            }
        }

        [ObjectSystem]
        public class UIGuideStepComponentUpdateSystem: UpdateSystem<UIGuideStepComponent>
        {
            protected override void Update(UIGuideStepComponent self)
            {
                if (self.RootTrans == null)
                {
                    return;
                }

                if (self.isGuiding == false)
                {
                    return;
                }
                self.Update();
            }
        }

        public static void Update(this UIGuideStepComponent self)
        {
            self.ChkNodeStatus().Coroutine();
            self.ChkPosChg().Coroutine();
            self.ChkSizeChg().Coroutine();
        }

        public static async ETTask ChkNodeStatus(this UIGuideStepComponent self)
        {
            if (self.isGuiding)
            {
                if (self.guidePathGo == null || self.RootTrans == null)
                {
                    return;
                }

                bool active = self.guidePathGo.activeInHierarchy;
                bool showRoot = self.RootTrans.gameObject.activeSelf;
                if (active != showRoot)
                {
                    self.RootTrans.gameObject.SetActive(active);
                    if (active)
                    {
                        //如果当前节点
                        await self._DoGuideStep(true);
                    }
                }
            }
        }

        public static async ETTask ChkPosChg(this UIGuideStepComponent self)
        {
            if (self.isGuiding)
            {
                if (self.guidePathGo == null || self.RootTrans == null)
                {
                    return;
                }

                bool active = self.guidePathGo.activeInHierarchy;
                if (active == false)
                {
                    return;
                }
                bool showRoot = self.RootTrans.gameObject.activeSelf;
                if (showRoot == false)
                {
                    return;
                }
                if (self.ChkIsNear(self.lastPos3D, self.guidePathGo.transform.position) == false)
                {
                    //如果当前节点
                    await self._DoGuideStep(true);
                }
            }
        }

        public static async ETTask ChkSizeChg(this UIGuideStepComponent self)
        {
            if (self.isGuiding)
            {
                if (self.guidePathGo == null || self.RootTrans == null)
                {
                    return;
                }

                bool active = self.guidePathGo.activeInHierarchy;
                if (active == false)
                {
                    return;
                }
                bool showRoot = self.RootTrans.gameObject.activeSelf;
                if (showRoot == false)
                {
                    return;
                }

                Vector2 canvasSize = self.canvasPathGo.GetComponent<RectTransform>().sizeDelta;
                if (self.ChkIsNear(self.lastCanvasSize, canvasSize) == false)
                {
                    //如果当前节点
                    await self._DoGuideStep(true);
                }
            }
        }

        public static async ETTask DoGuideStepOne(this UIGuideStepComponent self, Transform trans, UIGuidePath _UIGuidePath, Action finishedCallBack, Action SkipCallBack)
        {
            self.RootTrans = trans;
            if (self.RootTrans == null)
            {
                return;
            }

            self.guideMaskTrans = self.RootTrans.Find("GuideMask");
            self.maskWhenDown = self.RootTrans.Find("MaskWhenDown");

            self.curUIGuidePath = _UIGuidePath;
            self.curInNextType = _UIGuidePath.curInNextType;
            self.finishedCallBack = finishedCallBack;
            self.skipCallBack = SkipCallBack;

            await self._DoGuideStep(true);
        }

        public static async ETTask<bool> ChkGuideStepCondition(this UIGuideStepComponent self, bool enter)
        {
            TrigCondition trigCondition;
            string trigConditionStaticMethod;
            string trigConditionParam;

            if (enter)
            {
                trigCondition = self.curUIGuidePath.trigEnterCondition;
                trigConditionStaticMethod = self.curUIGuidePath.trigEnterConditionStaticMethod;
                trigConditionParam = self.curUIGuidePath.trigEnterConditionParam;
            }
            else
            {
                trigCondition = self.curUIGuidePath.trigExitCondition;
                trigConditionStaticMethod = self.curUIGuidePath.trigExitConditionStaticMethod;
                trigConditionParam = self.curUIGuidePath.trigExitConditionParam;
            }

            if (trigCondition == TrigCondition.None)
            {
                return true;
            }
            else if(trigCondition == TrigCondition.FindNode)
            {
                string nodePath = trigConditionParam;
                if (string.IsNullOrEmpty(nodePath))
                {
                    return true;
                }
                GameObject nodeGo = self.Find(null, nodePath);
                if (nodeGo != null)
                {
                    return true;
                }

                return false;
            }
            else if (trigCondition == TrigCondition.StaticMethod)
            {
                ET.Client.GuideConditionStaticMethodType conditionStaticMethod;
                if (string.IsNullOrEmpty(trigConditionStaticMethod))
                {
                    conditionStaticMethod = GuideConditionStaticMethodType.None;
                }
                else
                {
                    bool bRet = Enum.TryParse<ET.Client.GuideConditionStaticMethodType>(trigConditionStaticMethod, out conditionStaticMethod);
                    if (bRet == false)
                    {
                        conditionStaticMethod = GuideConditionStaticMethodType.None;
                    }
                }

                return await ET.Client.UIGuideHelper.DoStaticMethodChk(self.DomainScene(), conditionStaticMethod, trigConditionParam, self);
            }

            return false;
        }

        public static async ETTask DoGuideStepExecute(this UIGuideStepComponent self)
        {
            ET.Client.GuideExecuteStaticMethodType executeStaticMethod;
            if (string.IsNullOrEmpty(self.curUIGuidePath.guideExecuteStaticMethod))
            {
                executeStaticMethod = GuideExecuteStaticMethodType.None;
            }
            else
            {
                bool bRet = Enum.TryParse<ET.Client.GuideExecuteStaticMethodType>(self.curUIGuidePath.guideExecuteStaticMethod, out executeStaticMethod);
                if (bRet == false)
                {
                    executeStaticMethod = GuideExecuteStaticMethodType.None;
                }
            }

            string executeParam = self.curUIGuidePath.guideExecuteParam;

            await ET.Client.UIGuideHelper.DoStaticMethodExecute(self.DomainScene(), executeStaticMethod, executeParam, self);
        }

        public static async ETTask _DoGuideStep(this UIGuideStepComponent self, bool isNeedChkCondition = true)
        {
            self.curClickOutsideCount = 0;
            self.isGuiding = false;

            self.islastInit = false;
            self.lastGuideRectPos = Vector3.zero;
            self.lastGuideRectlossyScale = Vector3.zero;
            self.lastGuideRectSize = Vector2.zero;

            self.guideConditionStatus = new();

            string canvasPath = self.curUIGuidePath.hierarchyCanvasPath;
            string guidePath = self.curUIGuidePath.hierarchyGuidePath;

            await self.ShowUIMaskDefault();

            if (string.IsNullOrEmpty(canvasPath) && string.IsNullOrEmpty(guidePath))
            {
                await self.ShowUIMaskWhenNoPoint(isNeedChkCondition);
                return;
            }

            self.canvasPathGo = self.Find(null, canvasPath);
            while (self.canvasPathGo == null)
            {
                await TimerComponent.Instance.WaitFrameAsync();
                if (self.IsDisposed)
                {
                    return;
                }
                self.canvasPathGo = self.Find(null, canvasPath);
            }
            await self.SetParentUIGuide(self.canvasPathGo.transform, null);

            if (string.IsNullOrEmpty(canvasPath) == false && string.IsNullOrEmpty(guidePath))
            {
                await self.ShowUIMaskWhenNoPoint(isNeedChkCondition);
                return;
            }

            if (isNeedChkCondition)
            {
                bool bRet = await self.ChkGuideStepCondition(true);
                while (bRet == false)
                {
                    await TimerComponent.Instance.WaitFrameAsync();
                    if (self.IsDisposed)
                    {
                        return;
                    }
                    bRet = await self.ChkGuideStepCondition(true);
                    if (self.IsDisposed)
                    {
                        return;
                    }
                }
            }

            do
            {
                bool isNeedRefind = false;
                self.guidePathGo = self.Find(self.canvasPathGo, guidePath);
                if (self.guidePathGo == null)
                {
                    isNeedRefind = true;
                }
                else
                {
                    await self.SetParentUIGuide(self.canvasPathGo.transform, null);

                    Vector2 size = self.guidePathGo.GetComponent<RectTransform>().rect.size;

                    if (self.islastInit == false)
                    {
                        self.lastGuideRectSize = size;
                        isNeedRefind = true;
                    }
                    else if (self.ChkIsNear(self.lastGuideRectSize, size) == false)
                    {
                        self.lastGuideRectSize = size;
                        isNeedRefind = true;
                    }

                    Vector3 curPosition = self.guidePathGo.GetComponent<RectTransform>().position;
                    //Log.Error($"curPosition {curPosition} {self.lastGuideRectPos.Equals(curPosition)}");
                    if (self.islastInit == false)
                    {
                        self.lastGuideRectPos = curPosition;
                        isNeedRefind = true;
                    }
                    else if (self.ChkIsNear(self.lastGuideRectPos, curPosition) == false)
                    {
                        self.lastGuideRectPos = curPosition;
                        isNeedRefind = true;
                    }

                    Vector3 lossyScale = self.guidePathGo.GetComponent<RectTransform>().lossyScale;
                    if (self.islastInit == false)
                    {
                        self.lastGuideRectlossyScale = lossyScale;
                        isNeedRefind = true;
                    }
                    else if (self.ChkIsNear(self.lastGuideRectlossyScale, lossyScale) == false)
                    {
                        self.lastGuideRectlossyScale = lossyScale;
                        isNeedRefind = true;
                    }

                    if (self.islastInit == false)
                    {
                        self.islastInit = true;
                    }
                }

                if (isNeedRefind)
                {
                    self.guidePathGo = null;
                    //await TimerComponent.Instance.WaitFrameAsync();
                    await TimerComponent.Instance.WaitAsync(100);
                    if (self.IsDisposed)
                    {
                        return;
                    }
                }

            }
            while (self.guidePathGo == null);


            await self.SetParentUIGuide(self.canvasPathGo.transform, self.guidePathGo.transform);

            self.AddGuideClickInfo(self.guidePathGo);

            //高亮
            await self.ShowUIMask(self.guidePathGo.transform as RectTransform, self.canvasPathGo.GetComponent<Canvas>());

            self.isGuiding = true;
        }

        public static bool ChkIsNear(this UIGuideStepComponent self, Vector3 pos1, Vector3 pos2)
        {
            if (math.abs(pos1.x - pos2.x) > 0.5f)
            {
                return false;
            }
            if (math.abs(pos1.y - pos2.y) > 0.5f)
            {
                return false;
            }
            if (math.abs(pos1.z - pos2.z) > 0.5f)
            {
                return false;
            }

            return true;
        }

        public static async ETTask<Vector2> ShowUIMask(this UIGuideStepComponent self, RectTransform mask, Canvas canvas)
        {
            float diaphaneity;
            if (self.curUIGuidePath.waitToNextUIGuideStep == WaitToNextUIGuideStep.Black)
            {
                diaphaneity = self.diaphaneityBlack;
            }
            else if (self.curUIGuidePath.waitToNextUIGuideStep == WaitToNextUIGuideStep.BlackButNotClick)
            {
                diaphaneity = self.diaphaneityBlack;
            }
            else if (self.curUIGuidePath.waitToNextUIGuideStep == WaitToNextUIGuideStep.BlackButNoMask)
            {
                diaphaneity = self.diaphaneityBlack;
            }
            else if (self.curUIGuidePath.waitToNextUIGuideStep == WaitToNextUIGuideStep.NoMask)
            {
                diaphaneity = self.diaphaneityTransparent;
            }
            else if (self.curUIGuidePath.waitToNextUIGuideStep == WaitToNextUIGuideStep.Transparent)
            {
                diaphaneity = self.diaphaneityTransparent;
            }
            else
            {
                diaphaneity = self.diaphaneityBlack;
            }

            bool showPerviousLight;
            if (self.curUIGuidePath.specImgMatchType == SpecImgMatchType.None || string.IsNullOrEmpty(self.curUIGuidePath.specImgPath))
            {
                showPerviousLight = true;
            }
            else
            {
                showPerviousLight = false;
            }

            Vector2 center;
            float sizeX;
            float sizeY;

            self.guideMaskTrans.gameObject.SetActive(true);
            Image imgBG = self.guideMaskTrans.gameObject.GetComponent<Image>();
            var newColor = new Color(imgBG.color.r, imgBG.color.g, imgBG.color.b, 0);
            imgBG.color = newColor;
            Transform rectMaskTrans = self.RootTrans.Find("GuideMask/E_RectMask");
            Transform circleMaskTrans = self.RootTrans.Find("GuideMask/E_CircleMask");
            bool rectMask = true;
            float delayTime = 0.1f;
            if (rectMask)
            {
                rectMaskTrans.gameObject.SetActive(true);
                circleMaskTrans.gameObject.SetActive(false);

                (center, sizeX, sizeY) = rectMaskTrans.gameObject.GetComponent<RectGuideMask>().Init(mask, canvas, diaphaneity, delayTime, showPerviousLight);
            }
            else
            {
                rectMaskTrans.gameObject.SetActive(false);
                circleMaskTrans.gameObject.SetActive(true);

                (center, sizeX) = circleMaskTrans.gameObject.GetComponent<CircleGuideMask>().Init(mask, canvas, diaphaneity, delayTime, showPerviousLight);
                sizeY = sizeX;
            }

            delayTime = delayTime + 0.5f;
            if (self.curUIGuidePath.waitToNextUIGuideStep == WaitToNextUIGuideStep.BlackButNotClick)
            {
                self.SetMaskClick(null, false);
            }
            else
            {
                self.SetMaskClick(mask, false);
            }
            self.SetTextShow(center, sizeX, sizeY, delayTime).Coroutine();
            self.SetAudio().Coroutine();
            self.SetImageShow(mask, center, sizeX, sizeY, delayTime).Coroutine();
            await self.DoGuideStepExecute();

            return center;
        }

        public static async ETTask ShowUIMaskWhenNoPoint(this UIGuideStepComponent self, bool isNeedChkCondition = true)
        {
            self.RootTrans.SetAsLastSibling();
            self.RootTrans.gameObject.SetActive(true);

            if (isNeedChkCondition)
            {
                bool bRet = await self.ChkGuideStepCondition(true);
                while (bRet == false)
                {
                    await TimerComponent.Instance.WaitFrameAsync();
                    if (self.IsDisposed)
                    {
                        return;
                    }
                    bRet = await self.ChkGuideStepCondition(true);
                    if (self.IsDisposed)
                    {
                        return;
                    }
                }
            }

            float diaphaneity;
            if (self.curUIGuidePath.waitToNextUIGuideStep == WaitToNextUIGuideStep.Black)
            {
                diaphaneity = self.diaphaneityBlack;
            }
            else if (self.curUIGuidePath.waitToNextUIGuideStep == WaitToNextUIGuideStep.BlackButNotClick)
            {
                diaphaneity = self.diaphaneityBlack;
            }
            else if (self.curUIGuidePath.waitToNextUIGuideStep == WaitToNextUIGuideStep.BlackButNoMask)
            {
                diaphaneity = self.diaphaneityBlack;
            }
            else if (self.curUIGuidePath.waitToNextUIGuideStep == WaitToNextUIGuideStep.NoMask)
            {
                diaphaneity = self.diaphaneityTransparent;
            }
            else if (self.curUIGuidePath.waitToNextUIGuideStep == WaitToNextUIGuideStep.Transparent)
            {
                diaphaneity = self.diaphaneityTransparent;
            }
            else
            {
                diaphaneity = 0f;
            }

            Vector2 center = Vector2.one;
            float sizeX = 100;
            float sizeY = 100;

            self.guideMaskTrans.gameObject.SetActive(true);
            Image imgBG = self.guideMaskTrans.gameObject.GetComponent<Image>();
            var newColor = new Color(imgBG.color.r, imgBG.color.g, imgBG.color.b, diaphaneity);
            imgBG.color = newColor;

            Transform rectMaskTrans = self.RootTrans.Find("GuideMask/E_RectMask");
            Transform circleMaskTrans = self.RootTrans.Find("GuideMask/E_CircleMask");
            rectMaskTrans.gameObject.SetActive(false);
            circleMaskTrans.gameObject.SetActive(false);

            self.isGuiding = true;
            self.SetMaskClick(null, false);
            self.SetTextShow(center, sizeX, sizeY, 0).Coroutine();
            self.SetAudio().Coroutine();
            self.SetImageShow(null, center, sizeX, sizeY, 0).Coroutine();

            await self.DoGuideStepExecute();

            self.RootTrans.SetAsLastSibling();
            self.RootTrans.gameObject.SetActive(true);

            if (self.curUIGuidePath.waitToNextUIGuideStep == WaitToNextUIGuideStep.NoMask
                || self.curUIGuidePath.waitToNextUIGuideStep == WaitToNextUIGuideStep.BlackButNoMask)
            {
                await self.FinishClick();
            }
        }

        public static async ETTask ShowUIMaskDefault(this UIGuideStepComponent self)
        {
            Vector2 center = Vector2.one;
            float sizeX = 100;
            float sizeY = 100;

            if (self.curUIGuidePath.isFreeClickBeforeEnter)
            {
                self.guideMaskTrans.gameObject.SetActive(false);
            }
            else
            {
                self.guideMaskTrans.gameObject.SetActive(true);
            }
            self.maskWhenDown.gameObject.SetActive(false);

            Transform rectMaskTrans = self.RootTrans.Find("GuideMask/E_RectMask");
            Transform circleMaskTrans = self.RootTrans.Find("GuideMask/E_CircleMask");
            rectMaskTrans.gameObject.SetActive(false);
            circleMaskTrans.gameObject.SetActive(false);

            Transform skipNodeTrans = self.RootTrans.Find("SkipNode");
            skipNodeTrans.gameObject.SetActive(false);

            self.SetMaskClick(null, true);
            self.SetTextNotShow();
            self.SetImageShow(null, center, sizeX, sizeY, 0).Coroutine();

        }

        public static void SetMaskClick(this UIGuideStepComponent self, RectTransform mask, bool noClickFunc)
        {
            Transform GuideMask = self.RootTrans.Find("GuideMask");
            if (self.curUIGuidePath.waitToNextUIGuideStep == WaitToNextUIGuideStep.Black)
            {
                GuideMask.gameObject.GetComponent<Image>().raycastTarget = true;
            }
            else if (self.curUIGuidePath.waitToNextUIGuideStep == WaitToNextUIGuideStep.Transparent)
            {
                GuideMask.gameObject.GetComponent<Image>().raycastTarget = true;
            }
            else if (self.curUIGuidePath.waitToNextUIGuideStep == WaitToNextUIGuideStep.NoMask)
            {
                GuideMask.gameObject.GetComponent<Image>().raycastTarget = false;
            }
            else if (self.curUIGuidePath.waitToNextUIGuideStep == WaitToNextUIGuideStep.BlackButNotClick)
            {
                GuideMask.gameObject.GetComponent<Image>().raycastTarget = true;
            }
            else if (self.curUIGuidePath.waitToNextUIGuideStep == WaitToNextUIGuideStep.BlackButNoMask)
            {
                GuideMask.gameObject.GetComponent<Image>().raycastTarget = false;
            }
            else
            {
                GuideMask.gameObject.GetComponent<Image>().raycastTarget = false;
            }
            GuidanceEventPenetrate eventPenetrate = GuideMask.gameObject.GetComponent<GuidanceEventPenetrate>();
            if (eventPenetrate != null)
            {
                eventPenetrate.SetTargetRectTransform(mask);
            }

            if (noClickFunc)
            {
                //设置监听
                UIGuide.EventTriggerListener.GetListener(GuideMask.gameObject).onClick = (go) =>
                {
                };
            }
            else if (mask != null)
            {
                //设置监听
                UIGuide.EventTriggerListener.GetListener(GuideMask.gameObject).onClick = self.OnClickOutside;
            }
            else
            {
                //设置监听
                UIGuide.EventTriggerListener.GetListener(GuideMask.gameObject).onClick = (go) =>
                {
                    self.FinishClick().Coroutine();
                };
            }
        }

        public static async ETTask SetAudio(this UIGuideStepComponent self)
        {
            string audioPath = self.curUIGuidePath.audioPath;
            if (string.IsNullOrEmpty(audioPath))
            {
                return;
            }
            UIAudioManagerHelper.PlayUIGuideAudio(self.DomainScene(), audioPath);
            await ETTask.CompletedTask;
        }

        public static void SetTextNotShow(this UIGuideStepComponent self)
        {
            Transform tipNodeTrans = self.RootTrans.Find("EG_GuideInfo/E_TipNode");
            tipNodeTrans.gameObject.SetActive(false);
            tipNodeTrans.Find("E_TipTextNode").gameObject.SetActive(false);
            tipNodeTrans.Find("E_TipImageNode").gameObject.SetActive(false);
            tipNodeTrans.Find("E_TipDirectImageNode").gameObject.SetActive(false);
        }

        public static async ETTask SetTextShow(this UIGuideStepComponent self, Vector2 center, float halfSizeX, float halfSizeY, float delayTime)
        {
            Transform tipNodeTrans = self.RootTrans.Find("EG_GuideInfo/E_TipNode");

            if (self.curUIGuidePath.guideTextType == GuideTextType.None)
            {
                tipNodeTrans.gameObject.SetActive(false);
                return;
            }
            else if (self.curUIGuidePath.guideTextType == GuideTextType.Text)
            {
                if (string.IsNullOrEmpty(self.curUIGuidePath.text))
                {
                    tipNodeTrans.gameObject.SetActive(false);
                    return;
                }
            }
            else if (self.curUIGuidePath.guideTextType == GuideTextType.Image)
            {
                if (string.IsNullOrEmpty(self.curUIGuidePath.guideTextImgPath))
                {
                    tipNodeTrans.gameObject.SetActive(false);
                    return;
                }
            }

            if (delayTime > 0)
            {
                await TimerComponent.Instance.WaitAsync((long)(delayTime * 1000));
                if (self.IsDisposed)
                {
                    return;
                }
            }

            tipNodeTrans.gameObject.SetActive(true);

            if (self.curUIGuidePath.guideTextType == GuideTextType.Text)
            {
                await self.SetTextShowWhenText(tipNodeTrans, center, halfSizeX, halfSizeY);
            }
            else if (self.curUIGuidePath.guideTextType == GuideTextType.Image)
            {
                await self.SetTextShowWhenImage(tipNodeTrans, center, halfSizeX, halfSizeY);
            }

            await ETTask.CompletedTask;
        }

        public static async ETTask SetTextShowWhenText(this UIGuideStepComponent self, Transform tipNodeTrans, Vector2 center, float halfSizeX, float halfSizeY)
        {
            tipNodeTrans.gameObject.SetActive(true);
            Transform tipTextNode = tipNodeTrans.Find("E_TipTextNode");
            tipTextNode.gameObject.SetActive(true);

            string txt = LocalizeComponent.Instance.GetTextValue(self.curUIGuidePath.text);
            Transform tipTextTrans = tipNodeTrans.Find("E_TipTextNode/E_TipText");
            tipTextTrans.GetComponent<TextMeshProUGUI>().text = txt;

            Vector2 textPivot = Vector2.zero;
            Vector2 textPos = Vector2.zero;
            Vector2 directPos = Vector2.zero;
            Vector2 directRotation = Vector2.zero;
            RectTransform tipTextNodeRectTransform = (RectTransform)tipTextNode;
            tipTextNodeRectTransform.anchoredPosition = new Vector2(-5000, -5000);
            tipTextNodeRectTransform.sizeDelta = new Vector2(((RectTransform)self.RootTrans).sizeDelta.x, tipTextNodeRectTransform.sizeDelta.y);

            LayoutRebuilder.ForceRebuildLayoutImmediate(tipTextNodeRectTransform);

            tipTextNodeRectTransform.sizeDelta = new Vector2(((RectTransform)tipTextTrans).sizeDelta.x + 100, tipTextNodeRectTransform.sizeDelta.y);

            self.GetTextPos(tipTextNodeRectTransform, center, halfSizeX, halfSizeY, out textPivot, out textPos, out directPos, out directRotation);

            tipTextNodeRectTransform.pivot = textPivot;
            tipTextNodeRectTransform.anchoredPosition = textPos;

            if (directPos.Equals(Vector2.zero) == false)
            {
                Transform tipDirectImageNode = tipNodeTrans.Find("E_TipDirectImageNode");
                tipDirectImageNode.gameObject.SetActive(true);
                RectTransform tipDirectImageRectTransform = (RectTransform)tipDirectImageNode;
                tipDirectImageRectTransform.anchoredPosition = directPos;

                if (directRotation.x == 0)
                {
                    if (directRotation.y > 0)
                    {
                        tipDirectImageRectTransform.localEulerAngles = new Vector3(0, 0, 90);
                    }
                    else
                    {
                        tipDirectImageRectTransform.localEulerAngles = new Vector3(0, 0, -90);
                    }
                }
                else if (directRotation.y == 0)
                {
                    if (directRotation.x > 0)
                    {
                        tipDirectImageRectTransform.localEulerAngles = new Vector3(0, 0, 0);
                    }
                    else
                    {
                        tipDirectImageRectTransform.localEulerAngles = new Vector3(0, 0, 180);
                    }
                }
                else
                {
                    float t1 = math.atan2(directRotation.y, directRotation.x) * 180/math.PI;
                    tipDirectImageRectTransform.localEulerAngles = new Vector3(0, 0, t1);
                }
            }

            await ETTask.CompletedTask;
        }

        public static async ETTask SetTextShowWhenImage(this UIGuideStepComponent self, Transform tipNodeTrans, Vector2 center, float sizeX, float sizeY)
        {
            tipNodeTrans.gameObject.SetActive(true);
            Transform tipImageNode = tipNodeTrans.Find("E_TipImageNode");

            string txt = LocalizeComponent.Instance.GetTextValue(self.curUIGuidePath.text);
            Image image = tipImageNode.GetComponent<Image>();
            string imgPath = self.curUIGuidePath.guideTextImgPath;
            await image.SetImageByPath(self, imgPath);

            image.SetNativeSize();
            tipImageNode.gameObject.SetActive(true);

            // RectTransform rect = image.transform as RectTransform;
            // rect.anchorMax = new Vector2(0.5f, 0.5f);
            // rect.anchorMin = new Vector2(0.5f, 0.5f);
            // rect.pivot = new Vector2(0.5f, 0.5f);
            // rect.localPosition = center;
            // rect.localScale = Vector3.one;
            RectTransform rect = image.transform as RectTransform;
            RectTransform rootRect = self.RootTrans.transform as RectTransform;
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.localPosition = center;
            rect.localScale = Vector3.one;
            if (rootRect.rect.size.x < rect.rect.size.x || rootRect.rect.size.y < rect.rect.size.y)
            {
                rect.sizeDelta = rootRect.rect.size;
            }

            Vector2 textPivot = Vector2.zero;
            Vector2 textPos = Vector2.zero;
            Vector2 directPos = Vector2.zero;
            Vector2 directRotation = Vector2.zero;
            RectTransform tipImageNodeRectTransform = (RectTransform)tipImageNode;
            self.GetTextPos(tipImageNodeRectTransform, center, sizeX, sizeY, out textPivot, out textPos, out directPos, out directRotation);

            tipImageNodeRectTransform.pivot = textPivot;
            tipImageNodeRectTransform.anchoredPosition = textPos;

            if (directPos.Equals(Vector2.zero) == false)
            {
                Transform tipDirectImageNode = tipNodeTrans.Find("E_TipDirectImageNode");
                tipDirectImageNode.gameObject.SetActive(true);
                RectTransform tipDirectImageRectTransform = (RectTransform)tipDirectImageNode;
                tipDirectImageRectTransform.anchoredPosition = directPos;

                if (directRotation.x == 0)
                {
                    if (directRotation.y > 0)
                    {
                        tipDirectImageRectTransform.localEulerAngles = new Vector3(0, 0, 90);
                    }
                    else
                    {
                        tipDirectImageRectTransform.localEulerAngles = new Vector3(0, 0, -90);
                    }
                }
                else if (directRotation.y == 0)
                {
                    if (directRotation.x > 0)
                    {
                        tipDirectImageRectTransform.localEulerAngles = new Vector3(0, 0, 0);
                    }
                    else
                    {
                        tipDirectImageRectTransform.localEulerAngles = new Vector3(0, 0, 180);
                    }
                }
                else
                {
                    float t1 = math.atan2(directRotation.y, directRotation.x) * 180/math.PI;
                    tipDirectImageRectTransform.localEulerAngles = new Vector3(0, 0, t1);
                }
            }

            await ETTask.CompletedTask;
        }

        public static void GetTextPos(this UIGuideStepComponent self, RectTransform tipNodeTrans, Vector2 center, float halfSizeX, float halfSizeY,
        out Vector2 textPivot, out Vector2 textPos, out Vector2 directPos, out Vector2 directRotation)
        {
            if (self.guidePathGo == null)
            {
                textPivot = new Vector2(0.5f, 0.5f);
                textPos = new Vector2(0, 0);
                directPos = Vector2.zero;
                directRotation = Vector2.zero;
                return;
            }
            RectTransform rectGuide = self.RootTrans as RectTransform;
            Vector2 sizeGuide = rectGuide.sizeDelta;

            float directDis = 70;
            float textDis = 50;
            float screenDis = 100;

            float screenWidth = sizeGuide.x;
            float screenHeight = sizeGuide.y;

            float distToLeft = center.x - halfSizeX + screenWidth * 0.5f;
            float distToRight = screenWidth * 0.5f - center.x - halfSizeX ;

            float distToBottom = center.y - halfSizeY + screenHeight * 0.5f;
            float distToTop = screenHeight * 0.5f - center.y - halfSizeY ;

            float horDistance = Mathf.Max(distToLeft, distToRight);
            float vertDistance = Mathf.Max(distToBottom, distToTop);

            float textWidth = 0;
            if (horDistance > vertDistance)
            {
                if (distToLeft < distToRight)
                {
                    textPivot = new Vector2(0, 0.5f);
                    tipNodeTrans.pivot = textPivot;
                    directRotation = new Vector2(-1, 0);
                    directPos = center + new Vector2(halfSizeX + directDis, 0);
                    textWidth = horDistance - directDis - textDis - screenDis;
                    if (tipNodeTrans.sizeDelta.x > textWidth)
                    {
                        tipNodeTrans.sizeDelta = new Vector2(textWidth, tipNodeTrans.sizeDelta.y);
                    }
                    if (center.y + tipNodeTrans.sizeDelta.y * 0.5f + screenDis > screenHeight * 0.5f)
                    {
                        textPos = center + new Vector2(halfSizeX + directDis + textDis, 0);
                        textPos.y = screenHeight * 0.5f - screenDis - tipNodeTrans.sizeDelta.y * 0.5f;
                    }
                    else if (center.y - tipNodeTrans.sizeDelta.y * 0.5f - screenDis < -screenHeight * 0.5f)
                    {
                        textPos = center + new Vector2(halfSizeX + directDis + textDis, 0);
                        textPos.y = -screenHeight * 0.5f + screenDis + tipNodeTrans.sizeDelta.y * 0.5f;
                    }
                    else
                    {
                        textPos = center + new Vector2(halfSizeX + directDis + textDis, 0);
                    }
                }
                else
                {
                    textPivot = new Vector2(1, 0.5f);
                    tipNodeTrans.pivot = textPivot;
                    directRotation = new Vector2(1, 0);
                    directPos = center - new Vector2(halfSizeX + directDis, 0);
                    textWidth = horDistance - directDis - textDis - screenDis;
                    if (tipNodeTrans.sizeDelta.x > textWidth)
                    {
                        tipNodeTrans.sizeDelta = new Vector2(textWidth, tipNodeTrans.sizeDelta.y);
                    }
                    if (center.y + tipNodeTrans.sizeDelta.y * 0.5f + screenDis > screenHeight * 0.5f)
                    {
                        textPos = center - new Vector2(halfSizeX + directDis + textDis, 0);
                        textPos.y = screenHeight * 0.5f - screenDis - tipNodeTrans.sizeDelta.y * 0.5f;
                    }
                    else if (center.y - tipNodeTrans.sizeDelta.y * 0.5f - screenDis < -screenHeight * 0.5f)
                    {
                        textPos = center - new Vector2(halfSizeX + directDis + textDis, 0);
                        textPos.y = -screenHeight * 0.5f + screenDis + tipNodeTrans.sizeDelta.y * 0.5f;
                    }
                    else
                    {
                        textPos = center - new Vector2(halfSizeX + directDis + textDis, 0);
                    }
                }
            }
            else
            {
                if (distToBottom < distToTop)
                {
                    textPivot = new Vector2(0.5f, 0);
                    tipNodeTrans.pivot = textPivot;
                    directRotation = new Vector2(0, -1);
                    directPos = center + new Vector2(0, halfSizeY + directDis);
                    textWidth = screenWidth * 2/3f;
                    if (tipNodeTrans.sizeDelta.x > textWidth)
                    {
                        tipNodeTrans.sizeDelta = new Vector2(textWidth, tipNodeTrans.sizeDelta.y);
                    }

                    if (center.x + tipNodeTrans.sizeDelta.x * 0.5f + screenDis > screenWidth * 0.5f)
                    {
                        textPos = center + new Vector2(0, halfSizeY + directDis + textDis);
                        textPos.x = screenWidth * 0.5f - screenDis - tipNodeTrans.sizeDelta.x * 0.5f;
                    }
                    else if (center.x - tipNodeTrans.sizeDelta.x * 0.5f - screenDis < -screenWidth * 0.5f)
                    {
                        textPos = center + new Vector2(0, halfSizeY + directDis + textDis);
                        textPos.x = -screenWidth * 0.5f + screenDis + tipNodeTrans.sizeDelta.x * 0.5f;
                    }
                    else
                    {
                        textPos = center + new Vector2(0, halfSizeY + directDis + textDis);
                    }
                }
                else
                {
                    textPivot = new Vector2(0.5f, 1);
                    tipNodeTrans.pivot = textPivot;
                    directRotation = new Vector2(0, 1);
                    directPos = center - new Vector2(0, halfSizeY + directDis);
                    textWidth = screenWidth * 2/3f;
                    if (tipNodeTrans.sizeDelta.x > textWidth)
                    {
                        tipNodeTrans.sizeDelta = new Vector2(textWidth, tipNodeTrans.sizeDelta.y);
                    }
                    if (center.x + tipNodeTrans.sizeDelta.x * 0.5f + screenDis > screenWidth * 0.5f)
                    {
                        textPos = center - new Vector2(0, halfSizeY + directDis + textDis);
                        textPos.x = screenWidth * 0.5f - screenDis - tipNodeTrans.sizeDelta.x * 0.5f;
                    }
                    else if (center.x - tipNodeTrans.sizeDelta.x * 0.5f - screenDis < -screenWidth * 0.5f)
                    {
                        textPos = center - new Vector2(0, halfSizeY + directDis + textDis);
                        textPos.x = -screenWidth * 0.5f + screenDis + tipNodeTrans.sizeDelta.x * 0.5f;
                    }
                    else
                    {
                        textPos = center - new Vector2(0, halfSizeY + directDis + textDis);
                    }
                }
            }

        }

        public static async ETTask SetImageShow(this UIGuideStepComponent self, RectTransform mask, Vector2 center, float sizeX, float sizeY, float delayTime)
        {
            Transform hightImageNodeTrans = self.RootTrans.Find("EG_GuideInfo/E_HightNode/E_HightImageNode");
            Transform specImageNodeTrans = self.RootTrans.Find("EG_GuideInfo/E_HightNode/E_SpecImageNode");
            hightImageNodeTrans.gameObject.SetActive(false);
            specImageNodeTrans.gameObject.SetActive(false);
            bool showSpecImage = false;
            if (self.curUIGuidePath.specImgMatchType == SpecImgMatchType.None || string.IsNullOrEmpty(self.curUIGuidePath.specImgPath))
            {
                showSpecImage = false;
            }
            else
            {
                showSpecImage = true;
            }

            if (showSpecImage)
            {
                await self.SetSpecImageShow(center, sizeX, sizeY, delayTime);
            }
            else
            {
                await self.SetHighlightShow(mask, center, sizeX, sizeY, delayTime);
            }
        }

        public static async ETTask SetSpecImageShow(this UIGuideStepComponent self, Vector2 center, float sizeX, float sizeY, float delayTime)
        {
            Transform specImageNodeTrans = self.RootTrans.Find("EG_GuideInfo/E_HightNode/E_SpecImageNode");

            if (delayTime > 0)
            {
                await TimerComponent.Instance.WaitAsync((long)(delayTime * 1000));
                if (self.IsDisposed)
                {
                    return;
                }
            }

            Image image = specImageNodeTrans.Find("Image").GetComponent<Image>();
            string imgPath = self.curUIGuidePath.specImgPath;
            await image.SetImageByPath(self, imgPath);
            RectTransform rect = image.transform as RectTransform;
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.localPosition = center;
            rect.localScale = Vector3.one;
            if (self.curUIGuidePath.specImgMatchType == SpecImgMatchType.Scale)
            {
                rect.sizeDelta = new Vector2(sizeX*2, sizeY*2);
            }
            else if (self.curUIGuidePath.specImgMatchType == SpecImgMatchType.OrgSize)
            {
                image.SetNativeSize();
            }
            specImageNodeTrans.gameObject.SetActive(true);

            await ETTask.CompletedTask;
        }

        public static async ETTask SetHighlightShow(this UIGuideStepComponent self, RectTransform mask, Vector2 center, float sizeX, float sizeY, float delayTime)
        {
            Transform hightImageNodeTrans = self.RootTrans.Find("EG_GuideInfo/E_HightNode/E_HightImageNode");
            if (mask == null)
            {
                hightImageNodeTrans.gameObject.SetActive(false);
                return;
            }

            if (delayTime > 0)
            {
                await TimerComponent.Instance.WaitAsync((long)(delayTime * 1000));
                if (self.IsDisposed)
                {
                    return;
                }
            }

            hightImageNodeTrans.gameObject.SetActive(true);
            RectTransform rect = hightImageNodeTrans as RectTransform;
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.localPosition = center;
            rect.localScale = Vector3.one;
            rect.sizeDelta = new Vector2(sizeX*2, sizeY*2);

            await ETTask.CompletedTask;
        }

        public static async ETTask SetParentUIGuide(this UIGuideStepComponent self, Transform canvasTrans, Transform maskTrans)
        {
            if (canvasTrans != null)
            {
                RectTransform rectTransform = self.RootTrans.gameObject.GetComponent<RectTransform>();
                rectTransform.SetParent(canvasTrans);
                self.RootTrans.SetAsLastSibling();
                rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                rectTransform.pivot = new Vector2(0.5f, 0.5f);
                rectTransform.localPosition = Vector3.zero;
                rectTransform.localScale = Vector3.one;
                rectTransform.sizeDelta = canvasTrans.GetComponent<RectTransform>().sizeDelta;

                self.RootTrans.gameObject.SetActive(true);

                if (maskTrans != null)
                {
                    self.lastPos3D = maskTrans.position;
                    //self.lastPos3D = ((RectTransform)maskTrans).anchoredPosition3D;
                    self.lastCanvasSize = canvasTrans.GetComponent<RectTransform>().sizeDelta;
                }
            }
            else
            {
                self.RootTrans.SetAsLastSibling();
                self.RootTrans.gameObject.SetActive(true);
            }
            await ETTask.CompletedTask;
        }

        public static GameObject Find(this UIGuideStepComponent self, GameObject go, string name)
        {
            if (go == null)
            {
                GameObject goFind = GameObject.Find(name);
                if (goFind == null || goFind.activeInHierarchy == false) return null;
                return goFind;
            }
            else
            {
                Transform t = go.transform.Find(name);
                if (t == null) return null;
                if (t.gameObject.activeInHierarchy == false) return null;
                else return t.gameObject;
            }
        }

        public static void OnClickOutside(this UIGuideStepComponent self, GameObject go)
        {
            self.curClickOutsideCount++;

            if (self.curClickOutsideCount >= self.ClickOutsideMaxCount)
            {
                self.ShowSkipNode();
            }
        }

        public static void ShowSkipNode(this UIGuideStepComponent self)
        {
            Transform skipNodeTrans = self.RootTrans.Find("SkipNode");
            skipNodeTrans.gameObject.SetActive(true);
            UIGuide.EventTriggerListener.GetListener(skipNodeTrans.gameObject).onClick = (go) =>
            {
                self.DoSkipNode();
            };
        }

        public static void DoSkipNode(this UIGuideStepComponent self)
        {
            self.skipCallBack?.Invoke();
        }

        public static async ETTask FinishClick(this UIGuideStepComponent self)
        {
            self.DelGuideClickInfo();

            bool bRet = await self.ChkGuideStepCondition(false);
            while (bRet == false)
            {
                await TimerComponent.Instance.WaitFrameAsync();
                if (self.IsDisposed)
                {
                    return;
                }
                bRet = await self.ChkGuideStepCondition(false);
                if (self.IsDisposed)
                {
                    return;
                }
            }

            self.isGuiding = false;

            self.finishedCallBack?.Invoke();
        }

        public static void OnPointerClick(this UIGuideStepComponent self, GameObject go)
        {
            self.FinishClick().Coroutine();
        }

        public static void OnPointerDown(this UIGuideStepComponent self, GameObject go)
        {
            self.FinishClick().Coroutine();
        }

        public static void _HideMaskWhenDown(this UIGuideStepComponent self)
        {
            self.guideMaskTrans.gameObject.SetActive(false);
            Transform hightImageNodeTrans = self.RootTrans.Find("EG_GuideInfo/E_HightNode/E_HightImageNode");
            hightImageNodeTrans.gameObject.SetActive(false);
        }

        public static void _ShowMaskWhenUp(this UIGuideStepComponent self)
        {
            self.guideMaskTrans.gameObject.SetActive(true);
            Transform hightImageNodeTrans = self.RootTrans.Find("EG_GuideInfo/E_HightNode/E_HightImageNode");
            hightImageNodeTrans.gameObject.SetActive(true);
        }

        public static void AddGuideClickInfo(this UIGuideStepComponent self, GameObject go)
        {
            //设置监听
            if (self.curInNextType == CurInNextType.Click)
            {
                UIGuide.EventTriggerListener.GetListener(go).onClick -= self.OnPointerClick;
                UIGuide.EventTriggerListener.GetListener(go).onClick += self.OnPointerClick;
            }
            else if (self.curInNextType == CurInNextType.Down)
            {
                UIGuide.EventTriggerListener.GetListener(go).onDown -= self.OnPointerDown;
                UIGuide.EventTriggerListener.GetListener(go).onDown += self.OnPointerDown;

                self.maskWhenDown.gameObject.SetActive(true);
                EventTriggerListener.Get(self.maskWhenDown.gameObject).RemoveAllListeners();
                EventTriggerListener.Get(self.maskWhenDown.gameObject).onDown.AddListener((go, xx) =>
                {
                    self._HideMaskWhenDown();
                });
                EventTriggerListener.Get(self.maskWhenDown.gameObject).onUp.AddListener((go, xx) =>
                {
                    self._ShowMaskWhenUp();
                });
            }

        }

        public static void DelGuideClickInfo(this UIGuideStepComponent self)
        {
            if (self.guidePathGo == null)
            {
                self.canvasPathGo = null;
                self.guidePathGo = null;
                return;
            }
            if (self.curInNextType == CurInNextType.Click)
            {
                UIGuide.EventTriggerListener.GetListener(self.guidePathGo).onClick -= self.OnPointerClick;
            }
            else if (self.curInNextType == CurInNextType.Down)
            {
                UIGuide.EventTriggerListener.GetListener(self.guidePathGo).onDown -= self.OnPointerDown;
            }
            self.canvasPathGo = null;
            self.guidePathGo = null;
        }

    }
}