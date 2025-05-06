using System.Collections;

namespace ET
{
	public static class ObjectHelper
	{
		public static void Swap<T>(ref T t1, ref T t2)
		{
			(t1, t2) = (t2, t1);
		}

        public static string GetString(object obj)
        {
            string str = "";
            if (obj is string)
            {
                str = obj.ToString();
            }
            else if (obj is IList list)
            {
                foreach (var item in list)
                {
                    if (str.IsNullOrEmpty())
                    {
                        str = $"{GetString(item)}";
                    }
                    else
                    {
                        str = $"{str},{GetString(item)}";
                    }
                }
            }
            else if (obj is IEnumerable enumerable) // IEnumerable 可以处理 HashSet<T>
            {
                foreach (var item in enumerable)
                {
                    if (str.IsNullOrEmpty())
                    {
                        str = $"{GetString(item)}";
                    }
                    else
                    {
                        str = $"{str},{GetString(item)}";
                    }
                }
            }
            else
            {
                str = obj.ToString();
            }

            return str;
        }
	}
}