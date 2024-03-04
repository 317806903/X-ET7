using System.Collections;
using System.Collections.Generic;
using System;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using ET.AbilityConfig;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace ET.Client
{
	[FriendOf(typeof(DlgKnapsack))]
	public static class DlgKnapsackSystem
	{
		public static void RegisterUIEvent(this DlgKnapsack self)
		{
			self.View.EButton_AddButton.AddListenerAsync(self.OnAddButton);
			self.View.EButton_ClearButton.AddListenerAsync(self.OnClearButton);
			self.View.E_ReturnButton.AddListenerAsync(self.OnReturnButton);
			EventTriggerListener.Get(self.View.E_Sprite_BGImage.gameObject).onDown.AddListener((go, xx) =>
			{
				self.OnClickBG();
			});
			
			self.View.ELoopScrollList_CardLoopHorizontalScrollRect.prefabSource.prefabName = "Item_Card";
			self.View.ELoopScrollList_CardLoopHorizontalScrollRect.prefabSource.poolSize = 8;
			self.View.ELoopScrollList_CardLoopHorizontalScrollRect.AddItemRefreshListener((transform, i) =>
					self.AddCardItemRefreshListener(transform, i));
			self.View.ELoopScrollList_MyCardLoopVerticalScrollRect.prefabSource.prefabName = "Item_Card";
			self.View.ELoopScrollList_MyCardLoopVerticalScrollRect.prefabSource.poolSize = 18;
			self.View.ELoopScrollList_MyCardLoopVerticalScrollRect.AddItemRefreshListener((transform, i) =>
					self.AddMyCardItemRefreshListener(transform, i));

			//self.InitTypeDropdown();
		}

		// public static void InitTypeDropdown(this DlgKnapsack self)
		// {
		// 	//清空默认节点
		// 	self.View.EDropdown_TypeTMP_Dropdown.options.Clear();
		// 	//初始化
		// 	for (int i = 0; i < self.itemTypeList.Count; i++)
		// 	{
		// 		var tempData = new TMP_Dropdown.OptionData();
		// 		string textKey = self.itemTypeList[i];
		// 		tempData.text = LocalizeComponent.Instance.GetTextValue(textKey);
		// 		//tempData.image = gameModeList[i][2];
		// 		self.View.EDropdown_TypeTMP_Dropdown.options.Add(tempData);
		// 	}
		// 	//初始选项的显示
		// 	self.View.EDropdown_TypeTMP_Dropdown.value = 0;
		// 	//self.View.EDropdown_TypeTMP_Dropdown.AddListener(self.OnItemTypeChoose);
		// }

		public static void ShowWindow(this DlgKnapsack self, ShowWindowData contextData = null)
		{
			self.CreateCardScrollItem();
		}

		public static void OnClickBG(this DlgKnapsack self)
		{
			self.View.ELoopScrollList_MyCardLoopVerticalScrollRect.RefreshCells();
		}
		
		public static void CreateCardScrollItem(this DlgKnapsack self)
		{
			int count = ItemCfgCategory.Instance.GetAll().Count;
			self.AddUIScrollItems(ref self.ScrollItemMyCrad, count);
			self.View.ELoopScrollList_MyCardLoopVerticalScrollRect.SetVisible(true, count);
			self.AddUIScrollItems(ref self.ScrollItemCrad, count);
			self.View.ELoopScrollList_CardLoopHorizontalScrollRect.SetVisible(true, count);
		}

		public static async ETTask AddCardItemRefreshListener(this DlgKnapsack self, Transform transform, int index)
		{
			Scroll_Item_Card itemCard = self.ScrollItemCrad[index].BindTrans(transform);
			List<string> allItemList = ItemCfgCategory.Instance.GetAll().Keys.ToList();

			string itemCfgId = allItemList[index];
			itemCard.ELabel_NameTextMeshProUGUI.text = ItemHelper.GetItemName(itemCfgId);
			string icon = ItemHelper.GetItemIcon(itemCfgId);
			if (string.IsNullOrEmpty(icon) == false)
			{
				await itemCard.EButton_TowerIcoImage.SetImageByPath(icon);
			}
			
			itemCard.EButton_SelectButton.AddListener(() =>
			{
				self.AddItem(itemCfgId).Coroutine();
			});

			if (ItemHelper.ChkIsTower(itemCfgId))
			{
				itemCard.SetLabels(itemCfgId);
				itemCard.SetLevel(itemCfgId);
				itemCard.SetQuality(itemCfgId);
			}
		}

		public static async ETTask AddMyCardItemRefreshListener(this DlgKnapsack self, Transform transform, int index)
		{
			Scroll_Item_Card itemCard = self.ScrollItemMyCrad[index].BindTrans(transform);
			
			PlayerBackPackComponent playerBackPackComponent = await ET.Client.PlayerCacheHelper.GetMyPlayerBackPack(self.DomainScene());
			List<ItemComponent> itemList = playerBackPackComponent.GetItemList();

			//itemCard.EButton_SelectImage.color = new Color(0, 0, 0, 0);
			self.View.EButton_DeleteButton.SetVisible(false);
			self.View.E_SetNodeImage.SetVisible(false);

			if (index < itemList.Count)
			{
				itemCard.uiTransform.SetVisible(true);

				itemCard.ELabel_NumTextMeshProUGUI.text = itemList[index].count.ToString();

				string itemCfgId = itemList[index].CfgId;
				itemCard.ELabel_NameTextMeshProUGUI.text = ItemHelper.GetItemName(itemCfgId);
				string icon = ItemHelper.GetItemIcon(itemCfgId);
				if (string.IsNullOrEmpty(icon) == false)
				{
					await itemCard.EButton_TowerIcoImage.SetImageByPath(icon);
				}
				
				itemCard.EButton_SelectButton.AddListener(() =>
				{
					//itemCard.EButton_SelectImage.color = new Color(0, 0, 0, 0.5f);
					self.View.EButton_DeleteButton.SetVisible(true);
					self.View.E_SetNodeImage.SetVisible(true);
					
					self.View.EButton_DeleteButton.AddListener(()=>
					{
						self.OnDeleteButton(itemCfgId).Coroutine();
					});
					self.View.EButton_SetButton.AddListener(() =>
					{
						self.OnSetButton(itemCfgId).Coroutine();
					});
				});
				
				if (ItemHelper.ChkIsTower(itemCfgId))
				{
					itemCard.SetLabels(itemCfgId);
					itemCard.SetLevel(itemCfgId);
					itemCard.SetQuality(itemCfgId);
				}
			}
			else
			{
				itemCard.uiTransform.SetVisible(false);
			}
		}
		

		public static async ETTask OnAddButton(this DlgKnapsack self)
		{
			PlayerBackPackComponent playerBackPackComponent = await ET.Client.PlayerCacheHelper.GetMyPlayerBackPack(self.DomainScene());
			List<string> list = ItemCfgCategory.Instance.GetAll().Keys.ToList();
			int index = RandomGenerator.RandomNumber(0, list.Count);
			int count = int.Parse(self.View.EInputField_AddNumTMP_InputField.text);
			playerBackPackComponent.AddItem(list[index], count);
			self.View.ELoopScrollList_MyCardLoopVerticalScrollRect.RefreshCells();
			await ET.Client.PlayerCacheHelper.SaveMyPlayerModel(self.DomainScene(), PlayerModelType.BackPack, null);
		}

		public static async ETTask AddItem(this DlgKnapsack self, string itemCfgId)
		{
			PlayerBackPackComponent playerBackPackComponent = await ET.Client.PlayerCacheHelper.GetMyPlayerBackPack(self.DomainScene());
			int count = int.Parse(self.View.EInputField_AddNumTMP_InputField.text);
			playerBackPackComponent.AddItem(itemCfgId, count);
			self.View.ELoopScrollList_MyCardLoopVerticalScrollRect.RefreshCells();
			await ET.Client.PlayerCacheHelper.SaveMyPlayerModel(self.DomainScene(), PlayerModelType.BackPack, null);
		}
		
		public static async ETTask OnDeleteButton(this DlgKnapsack self, string itemCfgId)
		{
			PlayerBackPackComponent playerBackPackComponent = await ET.Client.PlayerCacheHelper.GetMyPlayerBackPack(self.DomainScene());
			playerBackPackComponent.RemoveItem(itemCfgId);
			self.View.ELoopScrollList_MyCardLoopVerticalScrollRect.RefreshCells();
			await ET.Client.PlayerCacheHelper.SaveMyPlayerModel(self.DomainScene(), PlayerModelType.BackPack, null);
		}
		
		public static async ETTask OnSetButton(this DlgKnapsack self, string itemCfgId)
		{
			PlayerBackPackComponent playerBackPackComponent = await ET.Client.PlayerCacheHelper.GetMyPlayerBackPack(self.DomainScene());
			int count = int.Parse(self.View.EInputField_SetNumTMP_InputField.text);
			playerBackPackComponent.SetItem(itemCfgId, count);
			self.View.ELoopScrollList_MyCardLoopVerticalScrollRect.RefreshCells();
			await ET.Client.PlayerCacheHelper.SaveMyPlayerModel(self.DomainScene(), PlayerModelType.BackPack, null);
		}

		public static async ETTask OnClearButton(this DlgKnapsack self)
		{
			PlayerBackPackComponent playerBackPackComponent = await ET.Client.PlayerCacheHelper.GetMyPlayerBackPack(self.DomainScene());
			playerBackPackComponent.ClearAllItem();
			self.View.ELoopScrollList_MyCardLoopVerticalScrollRect.RefillCells();
			await ET.Client.PlayerCacheHelper.SaveMyPlayerModel(self.DomainScene(), PlayerModelType.BackPack, null);
		}

		public static async ETTask OnGetListButton(this DlgKnapsack self)
		{
			PlayerBackPackComponent playerBackPackComponent = await ET.Client.PlayerCacheHelper.GetMyPlayerBackPack(self.DomainScene());
			List<ItemComponent> list = playerBackPackComponent.GetItemList();
			if (list.Count == 0)
			{
				Debug.Log("Null");
				return;
			}
			foreach (var itemComponent in list)
			{
				Debug.Log(itemComponent.CfgId + " " + itemComponent.count);
			}
			self.View.ELoopScrollList_MyCardLoopVerticalScrollRect.RefreshCells();
		}

		public static async ETTask OnReturnButton(this DlgKnapsack self)
		{
			UIAudioManagerHelper.PlayUIAudio(self.DomainScene(),SoundEffectType.Back);
			
			await ET.Client.UIManagerHelper.ExitRoom(self.DomainScene());
		}
	}
}
