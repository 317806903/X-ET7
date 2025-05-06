using System;
using System.Collections;
using System.Collections.Generic;
using Random = System.Random;

namespace ET
{
    /// <summary>
    /// 跳跃表层级数据
    /// </summary>
    public class SkipListLevel
    {
        /// <summary>
        /// 前进指针
        /// </summary>
        public SkipListNode Forward { get; set; }
        /// <summary>
        /// 跳跃级数
        /// </summary>
        public uint Span { get; set; }
    }

    public class SkipListNode
    {
        /// <summary>
        /// 存储数据
        /// </summary>
        public IComparable obj { get; set; }
        /// <summary>
        /// 分值
        /// </summary>
        public double Score { get; set; }
        /// <summary>
        /// 后退指针
        /// </summary>
        public SkipListNode Backward { get; set; }
        /// <summary>
        /// 层数据
        /// </summary>
        public SkipListLevel[] Level { get; set; }
    }

    public class SkipList
    {
        /// <summary>
        /// 跳跃表头节点
        /// </summary>
        public SkipListNode Header { get; private set; }
        /// <summary>
        /// 跳跃表尾节点
        /// </summary>
        public SkipListNode Tail { get; private set; }
        /// <summary>
        /// 跳跃表层数
        /// </summary>
        public int Level { get; private set; }
        /// <summary>
        /// 跳跃表长度
        /// </summary>
        public ulong Length { get; private set; }
        /// <summary>
        /// 默认最大层数
        /// </summary>
        private const int SkipList_MAXLevel = 32;
        /// <summary>
        /// 是否倒序
        /// </summary>
        public bool IsDescending = false;
        //······
        //未完整内容见跳跃表的基本操作方法

        /// <summary>
        /// 初始化跳跃表
        /// </summary>
        /// <returns>初始跳跃表</returns>
        public static SkipList CreateList(bool isDescending)
        {
            int j;
            SkipList list = new SkipList();
            list.Level = 1;
            list.Length = 0;
            list.IsDescending = isDescending;
            //创建一个层数为SkipList_MAXLevel，分数为0，值为空的跳跃表头节点
            list.Header = CreateNode(SkipList_MAXLevel, 0, null);
            for (j = 0; j < SkipList_MAXLevel; j++)
            {
                list.Header.Level[j].Forward = null;
                list.Header.Level[j].Span = 0;
            }
            list.Header.Backward = null;
            list.Tail = null;
            return list;
        }

        /// <summary>
        /// 创建一个跳跃表的节点
        /// </summary>
        /// <param name="Level">层数</param>
        /// <param name="score">分值</param>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        private static SkipListNode CreateNode(int Level, double score, IComparable obj)
        {
            SkipListNode node = new SkipListNode();
            node.obj = obj;
            node.Score = score;
            node.Level = new SkipListLevel[Level];
            for (int i = 0; i < Level; i++)
            {
                node.Level[i] = new SkipListLevel();
            }
            return node;
        }

        public void Insert(double score, IComparable obj)
        {
            //记录需要变更跨度层级对应的跳跃表节点
            SkipListNode[] update = new SkipListNode[SkipList_MAXLevel];
            //临时节点
            SkipListNode x = new SkipListNode();
            //记录各层的跨度
            uint[] rank = new uint[SkipList_MAXLevel];
            int i, level;
            //从跳跃表头节点开始查找
            x = this.Header;
            //从最高层循环遍历每层数据
            //记录每层的跨度以及需要变更跨度层级对应的跳跃表节点
            for (i = this.Level - 1; i >= 0; i--)
            {
                //最高层先确定为0，后续增加，否则为上一层的跨度的累加值
                rank[i] = i == (this.Level - 1) ? 0 : rank[i + 1];
                //判断前进节点是否为空并保证前进节点的分值小于要插入节点的分值
                //若分值相等则比较插入对象的先后顺序
                if (this.IsDescending)
                {
                    while (x.Level[i].Forward != null && x.Level[i].Forward?.Score > score
                           || (x.Level[i].Forward?.Score == score && ((x.Level[i].Forward?.obj.GetType() == obj.GetType()) && x.Level[i].Forward?.obj.CompareTo(obj) > 0)))
                    {
                        rank[i] += x.Level[i].Span;
                        x = x.Level[i].Forward;
                    }
                }
                else
                {
                    while (x.Level[i].Forward != null && x.Level[i].Forward?.Score < score
                           || (x.Level[i].Forward?.Score == score && ((x.Level[i].Forward?.obj.GetType() == obj.GetType()) && x.Level[i].Forward?.obj.CompareTo(obj) < 0)))
                    {
                        rank[i] += x.Level[i].Span;
                        x = x.Level[i].Forward;
                    }
                }
                //记录需要变更跨度层级对应的跳跃表节点
                update[i] = x;
            }
            //利用幂次定律获取随机层数
            level = this.GetRandomLevel();
            //当随机的层数超过跳跃表的层数时，修改最高层数
            //让高于层数的层级结构指向跳跃表头节点
            //并将跨度设置为跳跃表的长度
             if (level > this.Level)
            {
                for (i = this.Level; i < level; i++)
                {
                    rank[i] = 0;
                    update[i] = this.Header;
                    update[i].Level[i].Span = (uint)this.Length;
                }
                this.Level = level;
            }
            //根据随机层数、分值和对象创建节点
            x = CreateNode(level, score, obj);
            //从最底层开始循环遍历，更新跳跃表
            //详细解释,可以看对应图解
            for (i = 0; i < level; i++)
            {
                x.Level[i].Forward = update[i].Level[i].Forward;
                update[i].Level[i].Forward = x;
                x.Level[i].Span = update[i].Level[i].Span - (rank[0] - rank[i]);
                update[i].Level[i].Span = rank[0] - rank[i] + 1;
            }
            //当插入的节点层数低于最大层数
            //此时的最高层的跨度不会经过该节点(如下图示意)
            for (i = level; i < this.Level; i++)
            {
                update[i].Level[i].Span++;
            }
            //设置后退指针，若增加的节点位于最后曾更新尾节点
            x.Backward = (update[i] == this.Header) ? null : update[0];
            if (x.Level[0].Forward != null)
            {
                x.Level[0].Forward.Backward = x;
            }
            else
            {
                this.Tail = x;
            }
            //跳跃表长度增加
            this.Length++;
        }

        public ulong DeleteRangeByRank(uint rankStart, uint rankEnd)
        {
            ///同插入节点，先查找相应节点位置
            SkipListNode[] update = new SkipListNode[32];
            SkipListNode x = null;
            ulong traversed = 0;
            ulong removed = 0;

            int i;
            x = this.Header;
            for (i = this.Level - 1; i >= 0; i--)
            {
                while (x.Level[i].Forward != null && (traversed + x.Level[i].Span) < rankStart)
                {
                    traversed += x.Level[i].Span;
                    x = x.Level[i].Forward;
                }
                update[i] = x;
            }

            traversed++;
            x = x.Level[0].Forward;
            while (x != null && traversed <= rankEnd)
            {
                SkipListNode next = x.Level[0].Forward;
                this._DeleteNode(x, update);
                removed++;
                traversed++;
                x = next;
            }

            return removed;
        }

        public bool DeleteNode(double score, IComparable obj)
        {
            ///同插入节点，先查找相应节点位置
            SkipListNode[] update = new SkipListNode[32];
            SkipListNode x = null;
            int i;
            x = this.Header;
            for (i = this.Level - 1; i >= 0; i--)
            {
                if (this.IsDescending)
                {
                    while (x.Level[i].Forward != null && x.Level[i].Forward?.Score > score
                           || (x.Level[i].Forward?.Score == score && (obj != null && x.Level[i].Forward?.obj.CompareTo(obj) > 0)))
                    {
                        x = x.Level[i].Forward;
                    }
                }
                else
                {
                    while (x.Level[i].Forward != null && x.Level[i].Forward?.Score < score
                           || (x.Level[i].Forward?.Score == score && (obj != null && x.Level[i].Forward?.obj.CompareTo(obj) < 0)))
                    {
                        x = x.Level[i].Forward;
                    }
                }
                update[i] = x;
            }
            x = x.Level[0].Forward;
            bool isFind = false;
            //当分值和对象都相同时，删除该节点
            while (x != null && score == x.Score && (obj == null || x.obj.CompareTo(obj) == 0))
            {
                isFind = true;
                this._DeleteNode(x, update);
                x = x.Level[0].Forward;
            }

            if (isFind)
            {
                return true;
            }
            return false;
        }

        private void _DeleteNode(SkipListNode x, SkipListNode[] update)
        {
            int i;
            for (i = 0; i < this.Level; i++)
            {
                if (update[i].Level[i].Forward == x)
                {
                    //如果找到该节点，将前一个节点的跨度减1
                    update[i].Level[i].Span += x.Level[i].Span - 1;
                    //前一个节点的前进指针指向被删除的节点的后一个节点，跳过该节点
                    update[i].Level[i].Forward = x.Level[i].Forward;
                }
                else
                {
                    //在第i层没找到，只将该层的最后一个节点的跨度减1
                    update[i].Level[i].Span -= 1;
                }
            }
            //设置后退指针
            if (x.Level[0].Forward != null)
            {
                //如果被删除的前进节点不为空，后面还有节点
                //就将后面节点的后退指针指向被删除节点x的回退指针
                x.Level[0].Forward.Backward = x.Backward;
            }
            else
            {
                //否则直接将被删除的x节点的后退节点设置为表头的tail指针
                this.Tail = x.Backward;
            }
            //更新跳跃表最大层数
            while (this.Level > 1 && this.Header.Level[this.Level - 1].Forward == null)
                this.Level--;
            //节点计数器减1
            this.Length--;
        }

        private int GetRandomLevel()
        {
            int Level = 1;
            Random random = new Random((int)DateTime.Now.Ticks);
            while (random.Next(0xFFFF) < 0.25 * 0xFFFF)
            {
                Level += 1;
            }
            return Level < SkipList_MAXLevel ? Level : SkipList_MAXLevel;
        }

        public ulong GetRank(double score, IComparable obj)
        {
            SkipListNode x = this.Header;
            ulong rank = 0;
            for (int i = this.Level - 1; i >= 0; i--)
            {
                while (true)
                {
                    SkipListLevel skipListLevel = x.Level[i];
                    if (skipListLevel == null || skipListLevel.Forward == null)
                    {
                        break;
                    }

                    SkipListNode skipListNode = skipListLevel.Forward;
                    if (this.IsDescending)
                    {
                        if (skipListNode.Score < score)
                        {
                            break;
                        }
                        else if (skipListNode.Score == score && obj != null && skipListNode.obj.CompareTo(obj) < 0)
                        {
                            break;
                        }
                    }
                    else
                    {
                        if (skipListNode.Score > score)
                        {
                            break;
                        }
                        else if (skipListNode.Score == score && obj != null && skipListNode.obj.CompareTo(obj) > 0)
                        {
                            break;
                        }
                    }
                    rank += skipListLevel.Span;
                    x = skipListNode;
                }

                if (x.obj != null && obj != null && x.obj.CompareTo(obj) == 0)
                {
                    return rank;
                }
            }
            return rank + 1;
        }

        public SkipListNode GetNodeByRank(ulong rank)
        {
            SkipListNode x = this.Header;
            ulong traversed = 0;
            for (int i = this.Level - 1; i >= 0; i--)
            {
                while (true)
                {
                    SkipListLevel skipListLevel = x.Level[i];
                    if (skipListLevel == null || skipListLevel.Forward == null)
                    {
                        break;
                    }

                    if (traversed + skipListLevel.Span > rank)
                    {
                        break;
                    }

                    traversed += skipListLevel.Span;
                    x = skipListLevel.Forward;
                }

                if (traversed == rank)
                {
                    return x;
                }
            }
            return null;
        }

        public List<SkipListNode> GetNodeListByScore(ulong scoreStart, ulong scoreEnd)
        {
            List<SkipListNode> nodeList = new();

            SkipListNode x = null;

            int i;
            x = this.Header;
            for (i = this.Level - 1; i >= 0; i--)
            {
                while (true)
                {
                    if (x.Level[i] == null || x.Level[i].Forward == null)
                    {
                        break;
                    }

                    if (this.IsDescending)
                    {
                        if (x.Level[i].Forward.Score <= scoreEnd)
                        {
                            break;
                        }
                    }
                    else
                    {
                        if (x.Level[i].Forward.Score >= scoreStart)
                        {
                            break;
                        }
                    }
                    x = x.Level[i].Forward;
                }
            }

            x = x.Level[0].Forward;
            if (this.IsDescending)
            {
                while (x != null && x.Score >= scoreStart)
                {
                    SkipListNode next = x.Level[0].Forward;
                    nodeList.Add(x);
                    x = next;
                }
            }
            else
            {
                while (x != null && x.Score <= scoreEnd)
                {
                    SkipListNode next = x.Level[0].Forward;
                    nodeList.Add(x);
                    x = next;
                }
            }
            return nodeList;
        }

        public List<SkipListNode> GetNodeListByRank(uint rankStart, uint rankEnd)
        {
            List<SkipListNode> nodeList = new();

            ///同插入节点，先查找相应节点位置
            SkipListNode[] update = new SkipListNode[32];
            SkipListNode x = null;
            ulong traversed = 0;

            int i;
            x = this.Header;
            for (i = this.Level - 1; i >= 0; i--)
            {
                while (x.Level[i].Forward != null && (traversed + x.Level[i].Span) < rankStart)
                {
                    traversed += x.Level[i].Span;
                    x = x.Level[i].Forward;
                }
                update[i] = x;
            }

            traversed++;
            x = x.Level[0].Forward;
            while (x != null && traversed <= rankEnd)
            {
                SkipListNode next = x.Level[0].Forward;
                nodeList.Add(x);
                traversed++;
                x = next;
            }

            return nodeList;
        }

        public List<List<SkipListNode>> GetListShow()
        {
            List<List<SkipListNode>> list = new();
            for (int i = this.Level - 1; i >= 0; i--)
            {
                list.Add(new ());
            }
            for (int i = this.Level - 1; i >= 0; i--)
            {
                SkipListNode nextNode = this.Header;
                while (true)
                {
                    SkipListLevel SkipListLevel = nextNode.Level[i];
                    if (SkipListLevel == null)
                    {
                        break;
                    }
                    nextNode = SkipListLevel.Forward;
                    if (nextNode == null)
                    {
                        break;
                    }
                    list[i].Add(nextNode);
                }
            }
            return list;
        }

        public static void Test()
        {
            SkipList skipList = SkipList.CreateList(true);
            skipList.Insert(1, "adsfasdf 1");
            skipList.Insert(1, "adsfasdf 2");
            skipList.Insert(1, 123123123);
            skipList.Insert(1, 123);
            skipList.Insert(2, "adsfasdf 2");
            skipList.Insert(0, "adsfasdf 0");
            skipList.Insert(4, "adsfasd 4");
            skipList.Insert(4, "adsfasdfa 4");
            skipList.Insert(14, 123123123123);
            skipList.Insert(42, 4563456);



            skipList.DeleteNode(4, "adsfasd 4");
            skipList.DeleteNode(14, 123123123123);

            for (int i = 0; i < 100; i++)
            {
                int value = RandomGenerator.RandomNumber(1, 100);
                skipList.Insert(value, $"adsfasdf {value}");
            }

            /*var tmp1 = skipList.GetListShow();
            index = skipList.GetRank(33, null);
            index2 = skipList.GetRank(41, null);
            index3 = skipList.GetRank(2, null);
            index4 = skipList.GetRank(80, null);*/
            /*var tt = skipList.GetNodeListByRank(30, 50);
            var tt2 = skipList.GetNodeListByScore(30, 50);*/

            List<List<SkipListNode>> list = skipList.GetListShow();
            Log.Debug(list.Count.ToString());
            string str = " ";
            for (int i = 0; i < list.Count; i++)
            {
                str = " ";
                for (int j = 0; j < list[i].Count; j++)
                {
                    int k = j + 1;
                   str = str + k+")" + list[i][j].Score + " " + list[i][j].obj + "     ";
                }
                Log.Debug(str);
            }

            ulong index = skipList.GetRank(3, null);
            ulong index2 = skipList.GetRank(4, "adsfasdfa 4");
            ulong index3 = skipList.GetRank(2, null);
            ulong index4 = skipList.GetRank(1, null);
            ulong index5 = skipList.GetRank(150, null);

            Log.Debug("3:"+index);
            Log.Debug("4:"+index2);
            Log.Debug("2:"+index3);
            Log.Debug("1:"+index4);
            Log.Debug("150:"+index5);

            var tt = skipList.GetNodeListByRank(5, 10);
            str = " ";
            for (int j = 0; j < tt.Count; j++)
            {
                str = str + tt[j].Score + " ";
            }
            Log.Debug("GetNodeListByRank:"+str);

            var tt2 = skipList.GetNodeListByScore(3, 10);
            str = " ";
            for (int j = 0; j < tt2.Count; j++)
            {
                str = str + tt2[j].Score + " ";
            }
            Log.Debug("GetNodeListByScore:"+str);

            SkipListNode node = skipList.GetNodeByRank(13);
            Log.Debug("Rank13:" + node.Score + " " + node.obj);
            node = skipList.GetNodeByRank(26);
            Log.Debug("Rank26:" + node.Score + " " + node.obj);
            node = skipList.GetNodeByRank(47);
            Log.Debug("Rank47:" + node.Score + " " + node.obj);
            node = skipList.GetNodeByRank(86);
            Log.Debug("Rank86:" + node.Score + " " + node.obj);

            skipList.DeleteRangeByRank(10, 15);
            list = skipList.GetListShow();
            for (int i = 0; i < list.Count; i++)
            {
                str = " ";
                for (int j = 0; j < list[i].Count; j++)
                {
                    int k = j + 1;
                    str = str + k+")" + list[i][j].Score + " " + list[i][j].obj + "     ";
                }
                Log.Debug(str);
            }
            node = skipList.GetNodeByRank(47);
            Log.Debug("47:" + node.Score + " " + node.obj);
            node = skipList.GetNodeByRank(86);
            Log.Debug("86:" + node.Score + " " + node.obj);

        }
    }

}