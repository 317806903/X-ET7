using System;

namespace ET.Client
{

    [ObjectSystem]
    public class UIPathComponentAwakeSystem : AwakeSystem<UIPathComponent>
    {
        protected override void Awake(UIPathComponent self)
        {
            UIPathComponent.Instance = self;
            self.Awake();
        }
    }

    [ObjectSystem]
    public class UIPathComponentDestroySystem : DestroySystem<UIPathComponent>
    {
        protected override void Destroy(UIPathComponent self)
        {
            self.WindowPrefabPath.Clear();
            self.WindowTypeIdDict.Clear();
            if (UIPathComponent.Instance == self)
            {
                UIPathComponent.Instance = null;
            }
        }
    }

    [FriendOf(typeof(UIPathComponent))]
    public static class UIPathComponentSystem
    {
        public static string GetPlatform()
        {
#if Platform_Mobile
            return "Platform_Mobile";
#elif Platform_Quest
            return "Platform_Quest";
#elif Platform_AVP
            return "Platform_AVP";
#else
            return "Platform_NotDefine";
#endif
        }

        public static void Awake(this UIPathComponent self)
        {
            foreach (WindowID windowID in Enum.GetValues(typeof(WindowID)))
            {
                if (windowID.ToString().StartsWith($"WindowID_{GetPlatform()}_") == false)
                {
                    continue;
                }
                int index = windowID.ToString().IndexOf("_Dlg");
                string dlgName = windowID.ToString().Substring(index + 1);
                self.WindowPrefabPath.Add((int)windowID , dlgName);
                self.WindowTypeIdDict.Add(dlgName, (int)windowID );
            }
            foreach (WindowID windowID in Enum.GetValues(typeof(WindowID)))
            {
                if (windowID.ToString().StartsWith($"WindowID_{GetPlatform()}_"))
                {
                    continue;
                }
                int index = windowID.ToString().IndexOf("_Dlg");
                string dlgName = windowID.ToString().Substring(index + 1);
                if (self.WindowTypeIdDict.ContainsKey(dlgName))
                {
                    continue;
                }
                self.WindowPrefabPath.Add((int)windowID , dlgName);
                self.WindowTypeIdDict.Add(dlgName, (int)windowID );
            }
        }
    }
}