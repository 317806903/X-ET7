using System;
using System.Collections.Generic;
using System.Reflection;

namespace ET
{
    public static class AssemblyHelper
    {
        public static Dictionary<string, Type> GetAssemblyTypes(params Assembly[] args)
        {
            Dictionary<string, Type> types = new Dictionary<string, Type>();

            foreach (Assembly ass in args)
            {
                try
                {
                    foreach (Type type in ass.GetTypes())
                    {
                        types[type.FullName] = type;
                    }
                }
                catch (Exception e)
                {
                    Log.Error($"===type.FullName[{ass.FullName}] ass.GetTypes {e}");
                }
            }

            return types;
        }
    }
}