using System.Collections.Generic;

namespace ET.Client
{
    public static class EUIModelViewHelper
    {
        public static void AddUIScrollItems<K,T>(this K self, ref Dictionary<int, T> dictionary, int count) where K : Entity,IUILogic  where T : Entity,IAwake,IUIScrollItem
        {
            if (dictionary == null)
            {
                dictionary = new Dictionary<int, T>();
            }
            
            if (count <= 0)
            {
                return;
            }
            
            foreach (var item in dictionary)
            {
                item.Value.Dispose();
            }
            dictionary.Clear();
            for (int i = 0; i <= count; i++)
            {
                T itemServer = self.AddChild<T>(true);
                dictionary.Add(i , itemServer);
            }
        }

        //WJ:
        //上面的函数有IUILogic接口泛型约束，Page页面和Item没有继承IUIlogic;
        //故而重新在下面写一个没有IUILogic约束的AddUIScrollItemsPage函数;
        public static void AddUIScrollItemsPage<K, T>(this K self, ref Dictionary<int, T> dictionary, int count) where K : Entity where T : Entity, IAwake, IUIScrollItem
        {
            if (dictionary == null)
            {
                dictionary = new Dictionary<int, T>();
            }

            if (count <= 0)
            {
                return;
            }

            foreach (var item in dictionary)
            {
                item.Value.Dispose();
            }
            dictionary.Clear();
            for (int i = 0; i <= count; i++)
            {
                T itemServer = self.AddChild<T>(true);
                dictionary.Add(i, itemServer);
            }
        }
    }
}