using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using ET.AbilityConfig;

namespace ET.Client
{
    [Invoke(TimerInvokeType.DlgCameraPlayerSkillFrameTimer)]
    public class DlgCameraPlayerSkillTimer : ATimer<DlgCameraPlayerSkill>
    {
        protected override void Run(DlgCameraPlayerSkill self)
        {
            try
            {
	            if (self.IsDisposed)
	            {
		            return;
	            }
                self.Update();
            }
            catch (Exception e)
            {
                Log.Error($"move timer error: {self.Id}\n{e}");
            }
        }
    }
    [FriendOf(typeof(DlgCameraPlayerSkill))]
	public static class DlgCameraPlayerSkillSystem
	{
		public static void RegisterUIEvent(this DlgCameraPlayerSkill self)
		{
			self.View.E_BG_ClickButton.AddListenerAsync(self.OnClickBG);
			self.View.E_QuitBattleButton.AddListenerAsync(self.OnClickQuit);
			self.View.ELoopScrollList_SkillBattleDeckItemLoopHorizontalScrollRect.prefabSource.prefabName = "Item_SkillInfo";
			self.View.ELoopScrollList_SkillBattleDeckItemLoopHorizontalScrollRect.prefabSource.poolSize = GlobalSettingCfgCategory.Instance.MaxBattleSkillNum;
			self.View.ELoopScrollList_SkillBattleDeckItemLoopHorizontalScrollRect.AddItemRefreshListener((transform, index) =>
			{
				self.AddSkillBattleDeckItemRefreshListener(transform, index).Coroutine();
			});
			self.View.ELoopScrollList_SkillCardItemLoopHorizontalScrollRect.prefabSource.prefabName = "Item_SkillInfo";
			self.View.ELoopScrollList_SkillCardItemLoopHorizontalScrollRect.prefabSource.poolSize = 10;
			self.View.ELoopScrollList_SkillCardItemLoopHorizontalScrollRect.AddItemRefreshListener((transform, index) =>
			{
				self.AddSkillCardItemRefreshListener(transform, index).Coroutine();
			});
			self.BindMoveItem().Coroutine();
		}

		public static async ETTask ShowWindow(this DlgCameraPlayerSkill self, ShowWindowData contextData = null)
		{
			self.dlgShowTime = TimeHelper.ClientNow();
			await self.RefreshLoopList();
			self.ShowBg();
			self.Timer = TimerComponent.Instance.NewFrameTimer(TimerInvokeType.DlgCameraPlayerSkillFrameTimer, self);
		}

		public static bool ChkCanClickBg(this DlgCameraPlayerSkill self)
		{
			if (self.dlgShowTime < TimeHelper.ClientNow() - (long)(1000 * 1f))
			{
				return true;
			}
			return false;
		}

		public static void HideWindow(this DlgCameraPlayerSkill self)
		{
		}

		public static async ETTask OnClickBG(this DlgCameraPlayerSkill self)
		{
			if(self.ChkCanClickBg())
			{
				return;
			}
			UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgCameraPlayerSkill>();
			await ETTask.CompletedTask;
		}

		public static async ETTask OnClickQuit(this DlgCameraPlayerSkill self)
		{
			UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Quit);
			UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgCameraPlayerSkill>();
			await ETTask.CompletedTask;
		}

		public static async ETTask AddSkillBattleDeckItemRefreshListener(this DlgCameraPlayerSkill self, Transform transform, int index)
		{
			Scroll_Item_SkillInfo skillItem = self.skillBattleDeskDic[index].BindTrans(transform);
			PlayerSkillComponent playerSkillComponent = await PlayerCacheHelper.GetMyPlayerSkill(self.DomainScene());
			List<string> skillCfgIdList = playerSkillComponent.GetUsingSkillCfgList();

            if(skillItem.uiTransform==null)
            {
                Debug.Log("skillItem.uiTransform为空");
                return;
            }
            //是否显示红点
            bool isShowRedDot = false;
			string skilCfgid = "";

            if (index < skillCfgIdList.Count)
            {
                isShowRedDot = playerSkillComponent.ChkIsNewSkill(skillCfgIdList[index]);
                skilCfgid = skillCfgIdList[index];
            }
            //初始化skillItem  清除之前所有事件
            skillItem.Init((skilCfgid,true), isShowRedDot);
            //添加交互事件
            EventTriggerListener.Get(skillItem.EButton_SelectButton.gameObject).onDrag.AddListener((go, xx) =>
            {
                self.replaceIndex = index;
            });
            EventTriggerListener.Get(skillItem.EButton_SelectButton.gameObject).onEnter.AddListener((go, xx) =>
            {
                if (string.IsNullOrEmpty(self.moveItemCfgId))
                {
                    return;
                }
                skillItem.uiTransform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
                self.replaceIndex = index;
            });
            EventTriggerListener.Get(skillItem.EButton_SelectButton.gameObject).onExit.AddListener(async (go, xx) =>
            {
                await TimerComponent.Instance.WaitFrameAsync();
                skillItem.uiTransform.localScale = Vector3.one;
                if (string.IsNullOrEmpty(self.moveItemCfgId) == false)
                {
                    self.replaceIndex = -1;
                }
            });
        }

		public static async ETTask AddSkillCardItemRefreshListener(this DlgCameraPlayerSkill self, Transform transform, int index)
		{
			Scroll_Item_SkillInfo scroll_Item_SkillInfo = self.skillCardDic[index].BindTrans(transform);

            List<(string,bool)> overageSkillItemCfgList = await self.GetOverageSkillItemCfg();

            (string skillCfgId,bool isLearned) skillItemCfg=("",false);
            if (index < overageSkillItemCfgList.Count)
			{
				skillItemCfg= overageSkillItemCfgList[index];
			}

            PlayerSkillComponent playerSkillComponent = await PlayerCacheHelper.GetMyPlayerSkill(self.DomainScene());
            bool isShowRedDot = playerSkillComponent.ChkIsNewSkill(skillItemCfg.skillCfgId);
            scroll_Item_SkillInfo.Init(skillItemCfg,isShowRedDot);
			EventTriggerListener.Get(scroll_Item_SkillInfo.EButton_SelectButton.gameObject).onPress.AddListener((go, xx) =>
			{

                if(!skillItemCfg.isLearned)
                {
                    return;
                }
                self.moveItemCfgId = skillItemCfg.skillCfgId;
                self.replaceIndex = -1;
                self.ShowMoveItem(skillItemCfg).Coroutine();

            });
        }

		public static async ETTask BindMoveItem(this DlgCameraPlayerSkill self)
		{
			self.skillMoveItem = self.AddChild<Scroll_Item_SkillInfo>();
			Transform transform = self.View.EG_MoveItemRectTransform.GetChild(0);
			transform.gameObject.SetActive(false);
			self.skillMoveItem.BindTrans(transform);
			await ETTask.CompletedTask;
		}

		public static async ETTask ShowMoveItem(this DlgCameraPlayerSkill self, (string,bool) skillItemCfg)
		{
			Scroll_Item_SkillInfo item = self.skillMoveItem;
			item.Init(skillItemCfg);
			item.uiTransform.GetComponent<RectTransform>().gameObject.SetActive(true);
			await ETTask.CompletedTask;
		}

		public static async ETTask HideMoveItem(this DlgCameraPlayerSkill self)
		{
            Scroll_Item_SkillInfo item = self.skillMoveItem;
            item.uiTransform.GetComponent<RectTransform>().gameObject.SetActive(false);
			item.uiTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(-10000, -10000);
			await ETTask.CompletedTask;
        }

		public static async ETTask MovingMoveItem(this DlgCameraPlayerSkill self, Vector2 screenPos)
		{
            if (self.lastScreenPos.Equals(screenPos))
            {
                return;
            }
            self.lastScreenPos = screenPos;
            Scroll_Item_SkillInfo item = self.skillMoveItem;
            var canvasRT = item.uiTransform.parent.GetComponent<RectTransform>();
            // 将屏幕坐标转换为UI坐标
            Vector2 canvasPosition;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRT, screenPos, UIRootManagerComponent.Instance.UICamera, out canvasPosition))
            {
                item.uiTransform.GetComponent<RectTransform>().anchoredPosition = canvasPosition;
            }
			await ETTask.CompletedTask;
        }

		//刷新循环列表
		public static async ETTask RefreshLoopList(this DlgCameraPlayerSkill self)
		{
            int count = GlobalSettingCfgCategory.Instance.MaxBattleSkillNum;
            if (self.View.ELoopScrollList_SkillBattleDeckItemLoopHorizontalScrollRect.totalCount != count)
            {
                self.AddUIScrollItems(ref self.skillBattleDeskDic, count);
                self.View.ELoopScrollList_SkillBattleDeckItemLoopHorizontalScrollRect.SetVisible(true, count);
            }
            self.View.ELoopScrollList_SkillBattleDeckItemLoopHorizontalScrollRect.RefreshCells();

            List<(string, bool)> itemList = await self.GetOverageSkillItemCfg();
            int itemCount = itemList.Count;
            if (self.View.ELoopScrollList_SkillCardItemLoopHorizontalScrollRect.totalCount != itemCount)
            {
                self.AddUIScrollItems(ref self.skillCardDic, itemCount);
                self.View.ELoopScrollList_SkillCardItemLoopHorizontalScrollRect.SetVisible(true, itemCount);
            }
            self.View.ELoopScrollList_SkillCardItemLoopHorizontalScrollRect.RefreshCells();
        }

		//获取除上场外其余技能id
		public static async ETTask<List<(string,bool)>> GetOverageSkillItemCfg(this DlgCameraPlayerSkill self)
		{
            List<(string,bool)> skillItems = ListComponent<(string, bool)>.Create();
            PlayerSkillComponent playerSkillComponent = await ET.Client.PlayerCacheHelper.GetMyPlayerSkill(self.DomainScene());
            List<(string,bool)> allSkills = playerSkillComponent.GetAllSkillCfgList();
			//未解锁排在已解锁的后面，优先级低的排在优先级高的后面 越往左优先级越高
            allSkills.Sort((x, y) => {
				if (x.Item2 == true && y.Item2 == false)
					return -1;
				else if(x.Item2==false && y.Item2 ==true)
					return 1;
				else
					return -PlayerSkillCfgCategory.Instance.Get(x.Item1).ShowPriority.CompareTo(PlayerSkillCfgCategory.Instance.Get(y.Item1).ShowPriority);
			});

            skillItems = allSkills;
            return skillItems;
        }

        public static void ShowBg(this DlgCameraPlayerSkill self)
        {
            bool isARCameraEnable = ET.Client.ARSessionHelper.ChkARCameraEnable(self.DomainScene());
            isARCameraEnable = false;
            if (isARCameraEnable)
            {
                self.View.EG_bgARRectTransform.SetVisible(true);
                self.View.EG_bgRectTransform.SetVisible(false);
            }
            else
            {
                self.View.EG_bgARRectTransform.SetVisible(false);
                self.View.EG_bgRectTransform.SetVisible(true);
            }
        }

        public static void Update(this DlgCameraPlayerSkill self)
        {
            if (string.IsNullOrEmpty(self.moveItemCfgId))
            {
                return;
            }

            bool bRet = false;
            Vector2 screenPos = Vector2.zero;
            (bRet, screenPos) = ET.UGUIHelper.GetUserInputDownOrPress();
            if (bRet)
            {
                self.MovingMoveItem(screenPos).Coroutine();
            }
            else
            {
                (bRet, screenPos) = ET.UGUIHelper.GetUserInputUp();
                if (bRet)
                {
                    self.ChkPointUp().Coroutine();
                }
            }
        }

        public static async ETTask ChkPointUp(this DlgCameraPlayerSkill self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.BattleForbidden);
            string moveItemCfgId = self.moveItemCfgId;
            self.moveItemCfgId = "";
            self.HideMoveItem().Coroutine();
            if (self.replaceIndex == -1)
            {
                return;
            }
            int replaceIndex = self.replaceIndex;
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.TowerPush);
            await SkillHelper.ReplacePlayerSkill(self.DomainScene(), replaceIndex, moveItemCfgId);

        }
    }
}
