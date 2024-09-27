using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [FriendOf(typeof(DlgPersionalName))]
    public static class DlgPersionalNameSystem
    {
        public static void RegisterUIEvent(this DlgPersionalName self)
        {
            self.View.E_SaveButton.AddListenerAsync(self.OnSave);
            self.View.E_BG_ClickButton.AddListener(self.OnBGClick);
            self.View.E_InputFieldTMP_InputField.onEndEdit.AddListener(self.OnEndEdit);
            self.View.E_InputFieldTMP_InputField.onValueChanged.AddListener(self.OnValueChanged);
            self.View.EButton_CloseButton.AddListener(self.OnBGClick);
        }

        public static async ETTask ShowWindow(this DlgPersionalName self, ShowWindowData contextData = null)
        {
            self.ShowBg().Coroutine();
            self._ShowWindow().Coroutine();
        }

        public static async ETTask ShowBg(this DlgPersionalName self)
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

        public static async ETTask _ShowWindow(this DlgPersionalName self)
        {
            PlayerBaseInfoComponent playerBaseInfoComponent =
                await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(self.DomainScene());
            self.oldName = playerBaseInfoComponent.PlayerName;
            self.View.E_InputFieldTMP_InputField.text = self.oldName;
            self.View.E_SaveButton.SetVisible(false);

        }

        public static void OnLogout(this DlgPersionalName self)
        {
            self.Logout().Coroutine();
        }

        public static async ETTask Logout(this DlgPersionalName self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);


            PlayerBaseInfoComponent playerBaseInfoComponent =
                await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(self.DomainScene());

            string msgTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_AreYouLeaving_Des", playerBaseInfoComponent.PlayerName);
            string sureTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_AreYouLeaving_Confirm");
            string cancelTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_AreYouLeaving_Cancel");
            string titleTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_AreYouLeaving_Title");
            ET.Client.UIManagerHelper.ShowConfirm(self.DomainScene(), msgTxt, () => { LoginHelper.LoginOut(self.ClientScene(), true).Coroutine(); }, null,
                sureTxt, cancelTxt, titleTxt);
        }

        public static async ETTask OnSave(this DlgPersionalName self)
        {
            if (!self.DetermineNameLength(self.curName))
            {
                return;
            }

            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);

            PlayerBaseInfoComponent playerBaseInfoComponent =
                await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(self.DomainScene());
            playerBaseInfoComponent.PlayerName = self.curName;
            await ET.Client.PlayerCacheHelper.SaveMyPlayerModel(self.DomainScene(), PlayerModelType.BaseInfo, new() { "PlayerName", "IconIndex" });

            self.oldName = self.curName;
            self.View.E_SaveButton.SetVisible(false);

            string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_PersonalInfo_Update");
            ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
        }

        public static void OnBGClick(this DlgPersionalName self)
        {
            if (self.ChkInfoChanged())
            {
                string msgTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_PersonalInfo_Abandon_Des");
                ET.Client.UIManagerHelper.ShowConfirm(self.DomainScene(), msgTxt, () => { self.HidePersonalInfo().Coroutine(); }, null
                    );
            }
            else
            {
                self.HidePersonalInfo().Coroutine();
            }
        }

        public static async ETTask HidePersonalInfo(this DlgPersionalName self)
        {
            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgPersionalName>();
            await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgPersonalInformation>();
        }

        public static bool DetermineNameLength(this DlgPersionalName self, string name)
        {
            if (name.Length < 1)
            {
                string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_key_CommonTip_Report_Empty");
                UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                return false;
            }

            if (name.Length > self.NameMaxLength)
            {
                string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_key_CommonTip_Report_Limit");
                UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                return false;
            }

            return true;
        }

        public static void OnEndEdit(this DlgPersionalName self, string name)
        {
            self.DetermineNameLength(name);
        }

        public static void OnValueChanged(this DlgPersionalName self, string name)
        {
            self.curName = name;
            //TODO 判断name是否合法
            self.ChkInfoChanged();
        }



        public static bool ChkInfoChanged(this DlgPersionalName self)
        {
            if ( self.curName != self.oldName)
            {
                self.View.E_SaveButton.SetVisible(true);
                return true;
            }
            self.View.E_SaveButton.SetVisible(false);
            return false;
        }
    }
}