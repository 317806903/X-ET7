using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ET
{
    /// 公式解析类，将公式的基础单元与运算符分割出来//
    public class FormulaStringFx
    {
        public static FormulaStringFx GetInstance(string formatString)
        {
            if (!Instances.ContainsKey(formatString))
            {
                Instances[formatString] = new FormulaStringFx(formatString);
            }

            return Instances[formatString];
        }

        private static Dictionary<string, FormulaStringFx> Instances = new ();

        public class FormulaNode: DisposablClass
        {
            public int Key;
            public string Value;
            public bool IsSelf;
            public bool isSymbol;
            public bool isNum;
            public float numValue;

            public FormulaNode()
            {
            }

            public bool isDisposed; //表示是否已经被回收
            protected override void Dispose(bool disposing)
            {
                if(this.isDisposed) return; //如果已经被回收，就中断执行
                if(disposing) //如果需要回收一些托管资源
                {
                }

                try
                {
                    this.isDisposed = true;
                    if (disposing)
                    {
                        if (ObjectPool.Instance != null)
                        {
                            ObjectPool.Instance.Recycle(this);
                        }
                    }
                    else
                    {
                        if (ObjectPool.Instance != null)
                        {
                            ObjectPool.Instance.Recycle(this);
                            GC.ReRegisterForFinalize(this);
                        }
                    }

                }
                catch (Exception e)
                {
                    Log.Error($"FormulaNode.Dispose {e}");
                }

                base.Dispose(disposing);//再调用父类的垃圾回收逻辑
            }

            public override void Dispose()
            {
                base.Dispose();
            }

            public override void Reuse()
            {
                this.isDisposed = false;
                base.Reuse();
            }

            public static FormulaNode Create(int Key, string Value, bool IsSelf, bool isSymbol, bool isNum, float numValue)
            {
                try
                {
                    FormulaNode FormulaNode = ObjectPool.Instance.Fetch(typeof (FormulaNode)) as FormulaNode;
                    FormulaNode.Reuse();
                    FormulaNode.Key = Key;
                    FormulaNode.Value = Value;
                    FormulaNode.IsSelf = IsSelf;
                    FormulaNode.isSymbol = isSymbol;
                    FormulaNode.isNum = isNum;
                    FormulaNode.numValue = numValue;
                    return FormulaNode;
                }
                catch (Exception e)
                {
                    Log.Error($"FormulaNode.Create Error: {e}");
                    FormulaNode FormulaNode = new ();
                    FormulaNode.Key = Key;
                    FormulaNode.Value = Value;
                    FormulaNode.IsSelf = IsSelf;
                    FormulaNode.isSymbol = isSymbol;
                    FormulaNode.isNum = isNum;
                    FormulaNode.numValue = numValue;
                    FormulaNode.isDisposed = false;
                    return FormulaNode;
                }
            }

            public static FormulaNode Clone(FormulaNode formulaNode)
            {
                FormulaNode FormulaNode = Create(formulaNode.Key, formulaNode.Value, formulaNode.IsSelf, formulaNode.isSymbol, formulaNode.isNum, formulaNode.numValue);
                return FormulaNode;
            }

            public void ChgValue(float newValue)
            {
                this.numValue = newValue;
                this.isSymbol = false;
                this.isNum = true;
            }

        }

        /// 获得字符类型//
        protected int GetPriority(char c)
        {
            if (c == '+' || c == '-') return 1;
            if (c == '*' || c == '/') return 2;
            if (c == '^') return 3;
            if (c == '(' || c == ')') return 4;
            return 0;
        }

        protected const int priorityMax = 4;

        protected List<FormulaNode> formulaNodeList = new ();

        /// 解析字符串获得公式结构//
        private FormulaStringFx(string formatString)
        {
            if (formatString == null)
                formatString = "";
            formatString = formatString.Replace(" ", "").Replace("\n", "").Replace("\t", "");

            if (formatString.Length > 0)
            {
                formatString = formatString.Replace("(-", "(0-");
                formatString = formatString.Replace("(+", "(0+");
                if (formatString[0] == '-' || formatString[0] == '+')
                    formatString = "0" + formatString;
            }

            //第一层 用来分类//
            int laststate = -1;
            string temp = "";
            for (int i = 0; i < formatString.Length; i++)
            {
                char c = formatString[i];
                if ((c >= '0' && c <= '9') || (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') ||
                    c == '.' ||
                    c == '+' || c == '-' || c == '*' || c == '/' ||
                    c == '^' || c == '(' || c == ')' ||
                    c == '_' || c == '$')
                {
                }
                else
                    continue;

                int state = GetPriority(c);

                if (state == 0)
                {
                    //数值//
                    temp += c;
                }
                else
                {
                    if (laststate == 0)
                    {
                        AddNode(laststate, temp);
                        temp = "";
                    }

                    temp += c;
                    AddNode(state, temp);
                    temp = "";
                }

                laststate = state;
            }

            if (!string.IsNullOrEmpty(temp))
            {
                AddNode(laststate, temp);
            }

            List<FormulaNode> templist = new List<FormulaNode>();
            int operadd = 0;
            for (int i = 0; i < formulaNodeList.Count; i++)
            {
                if (formulaNodeList[i].Value == "(")
                {
                    operadd += priorityMax;
                }
                else if (formulaNodeList[i].Value == ")")
                {
                    operadd -= priorityMax;
                }

                formulaNodeList[i].Key += operadd;
                if (formulaNodeList[i].Value != "(" && formulaNodeList[i].Value != ")")
                    templist.Add(formulaNodeList[i]);
            }

            formulaNodeList = templist;
        }

        public void AddNode(int Key, string Value)
        {
            bool isSelf = false;
            bool isSymbol = false;
            bool isNum = false;
            int priority = Key;
            if (Value.StartsWith("$"))
            {
                Value = Value.Substring(1, Value.Length - 1);
                isSelf = false;
            }
            else
            {
                isSelf = true;
            }

            if (priority > 0)
            {
                isSymbol = true;
            }

            float numValue = 0;
            if (float.TryParse(Value, out numValue))
            {
                isNum = true;
            }
            formulaNodeList.Add(FormulaNode.Create(Key, Value, isSelf, isSymbol, isNum, numValue));
        }

        public void Print()
        {
            string outstring = "";
            for (int i = 0; i < formulaNodeList.Count; i++)
            {
                outstring += string.Format("[{0}-({1}):({2}) ]", formulaNodeList[i].IsSelf? "Self" : "Other", formulaNodeList[i].Key, formulaNodeList[i].Value);
            }

            Log.Info(outstring);
        }

        // FormulaStringFx fx = FormulaStringFx.GetInstance(@"ATK*(1-$DEF/($DEF+$Lv*100+500)*Lv/$Lv)");
        // Dictionary<string, float> numericDicSelf = new();
        // Dictionary<string, float> numericDicOther = new();
        // numericDicSelf.Add("ATK", 1000.1f);
        // numericDicSelf.Add("Lv", 3);
        // numericDicOther.Add("DEF", 500.5f);
        // numericDicOther.Add("Lv", 6);
        // var cost = fx.GetData(numericDicSelf, numericDicOther);
        /// <summary>
        /// 计算结果,传入的Dictionary中必须包含所有信息//
        /// </summary>
        /// <param name="numericDicSelf"></param>
        /// <param name="numericDicOther"></param>
        /// <returns></returns>
        public float GetData(Dictionary<string, float> numericDicSelf, Dictionary<string, float> numericDicOther)
        {
            using ListComponent<int> priorityList = ListComponent<int>.Create();

            using ListComponent<FormulaNode> tempList = ListComponent<FormulaNode>.Create();

            for (int i = 0; i < formulaNodeList.Count; i++)
            {
                FormulaNode node = FormulaNode.Clone(formulaNodeList[i]);
                tempList.Add(node);
            }

            for (int i = 0; i < tempList.Count; i++)
            {
                bool isSymbol = tempList[i].isSymbol;
                bool isNum = tempList[i].isNum;
                string formulaNodeValue = tempList[i].Value;
                if (isSymbol || isNum)
                {
                }
                else
                {
                    if (tempList[i].IsSelf)
                    {
                        if (numericDicSelf == null)
                        {
                            Log.Error($"numericDicSelf == null formulaNodeValue[{formulaNodeValue}]");
                            return 0;
                        }
                        if (numericDicSelf.TryGetValue(formulaNodeValue, out float numericValue) == false)
                        {
                            Log.Error($"numericDicSelf.TryGetValue(formulaNodeValue, out float numericValue) == false formulaNodeValue[{formulaNodeValue}]");
                            return 0;
                        }
                        tempList[i].ChgValue(numericValue);
                    }
                    else
                    {
                        if (numericDicOther == null)
                        {
                            Log.Error($"numericDicOther == null formulaNodeValue[{formulaNodeValue}]");
                            return 0;
                        }
                        if (numericDicOther.TryGetValue(formulaNodeValue, out float numericValue) == false)
                        {
                            Log.Error($"numericDicOther.TryGetValue(formulaNodeValue, out float numericValue) == false formulaNodeValue[{formulaNodeValue}]");
                            return 0;
                        }
                        tempList[i].ChgValue(numericValue);
                    }
                }

                if (tempList[i].Key != 0 && !priorityList.Contains(tempList[i].Key))
                {
                    priorityList.Add(tempList[i].Key);
                }
            }

            priorityList.Sort();

            int listCount = tempList.Count;
            if (listCount < 3)
            {
                Log.Error($"listCount < 3");
                return 0;
            }
            while (priorityList.Count > 0)
            {
                int currentpri = priorityList[priorityList.Count - 1];
                bool hasfind = false;
                do
                {
                    hasfind = false;
                    for (int i = 0; i < listCount; i++)
                    {
                        if (tempList[i].Key == currentpri && tempList[i].isSymbol)
                        {
                            DealOperactorFinal(tempList, ref listCount, i);

                            hasfind = true;
                            break;
                        }
                    }
                }
                while (hasfind);

                priorityList.RemoveAt(priorityList.Count - 1);
                priorityList.Sort();
            }
            if (listCount == 0)
            {
                return 0;
            }

            // while (priorityList.Count > 0)
            // {
            //     int currentpri = priorityList[priorityList.Count - 1];
            //     bool hasfind = false;
            //     do
            //     {
            //         hasfind = false;
            //         for (int i = 0; i < tempList.Count && tempList.Count >= 3; i++)
            //         {
            //             if (tempList[i].Key == currentpri && tempList[i].isSymbol)
            //             {
            //                 float final = GetOperactorFinal(tempList[i - 1].Value, tempList[i].Value, tempList[i + 1].Value);
            //                 var newformula = tempList[i - 1];
            //                 newformula.ChgValue(final);
            //                 tempList.RemoveRange(i, 2);
            //                 hasfind = true;
            //                 break;
            //             }
            //         }
            //     }
            //     while (hasfind);
            //
            //     priorityList.RemoveAt(priorityList.Count - 1);
            //     priorityList.Sort();
            // }
            // if (tempList.Count == 0)
            // {
            //     return 0;
            // }

            float finalnum = tempList[0].isNum?tempList[0].numValue:0;

            priorityList.Dispose();
            foreach (FormulaNode formulaNode in tempList)
            {
                formulaNode.Dispose();
            }
            tempList.Clear();
            tempList.Dispose();
            return finalnum;
        }

        protected void DealOperactorFinal(List<FormulaNode> list, ref int listCount, int index)
        {
            float final = GetOperactorFinal(list, index);
            var newformula = list[index - 1];
            newformula.ChgValue(final);

            list[index].Dispose();
            list[index + 1].Dispose();
            for (int j = index; j < listCount-2; j++)
            {
                list[j] = list[j + 2];
            }
            listCount -= 2;
        }

        /// s1 ope s2格式的计算式 例如 5 + 6
        protected float GetOperactorFinal(List<FormulaNode> list, int index)
        {
            string ope = list[index].Value;
            float num1 = list[index - 1].numValue;
            float num2 = list[index + 1].numValue;

            if (ope == "+")
            {
                return num1 + num2;
            }

            if (ope == "-")
            {
                return num1 - num2;
            }

            if (ope == "*")
            {
                return num1 * num2;
            }

            if (ope == "/")
            {
                if (num2 == 0)
                    return 0;
                return num1 / num2;
            }

            if (ope == "^")
            {
                return (float)Math.Pow(num1, num2);
            }

            return 0;
        }
    }
}