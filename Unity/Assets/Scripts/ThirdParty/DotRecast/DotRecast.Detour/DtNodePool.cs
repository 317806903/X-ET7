/*
Copyright (c) 2009-2010 Mikko Mononen memon@inside.org
recast4j copyright (c) 2015-2019 Piotr Piastucki piotr@jtilia.org
DotRecast Copyright (c) 2023 Choi Ikpil ikpil@naver.com

This software is provided 'as-is', without any express or implied
warranty.  In no event will the authors be held liable for any damages
arising from the use of this software.
Permission is granted to anyone to use this software for any purpose,
including commercial applications, and to alter it and redistribute it
freely, subject to the following restrictions:
1. The origin of this software must not be misrepresented; you must not
 claim that you wrote the original software. If you use this software
 in a product, an acknowledgment in the product documentation would be
 appreciated but is not required.
2. Altered source versions must be plainly marked as such, and must not be
 misrepresented as being the original software.
3. This notice may not be removed or altered from any source distribution.
*/

using System.Collections.Generic;

namespace DotRecast.Detour
{
    public class DtNodePool
    {
        private readonly Dictionary<long, List<DtNode>> m_map = new Dictionary<long, List<DtNode>>();
        private readonly List<DtNode> m_nodes = new List<DtNode>();

        Queue<DtNode> queue = new();
        Queue<List<DtNode>> queueNodeList = new();

        public DtNodePool()
        {
        }

        public void Clear()
        {
            foreach (DtNode dtNode in this.m_nodes)
            {
                dtNode.Dispose();
            }
            m_nodes.Clear();

            foreach (var tmp in this.m_map)
            {
                this.RecycleNodeList(tmp.Value);
            }
            m_map.Clear();
        }

        public List<DtNode> FindNodes(long id)
        {
            var hasNode = m_map.TryGetValue(id, out var nodes);
            if (nodes == null)
            {
                nodes = this.FetchNodeList();
            }

            return nodes;
        }

        public DtNode FindNode(long id)
        {
            var hasNode = m_map.TryGetValue(id, out var nodes);
            ;
            if (nodes != null && 0 != nodes.Count)
            {
                return nodes[0];
            }

            return null;
        }

        public DtNode GetNode(long id, int state)
        {
            var hasNode = m_map.TryGetValue(id, out var nodes);
            if (nodes != null)
            {
                foreach (DtNode node in nodes)
                {
                    if (node.state == state)
                    {
                        return node;
                    }
                }
            }
            else
            {
                nodes = this.FetchNodeList();
                m_map.Add(id, nodes);
            }

            return Create(id, state, nodes);
        }

        private DtNode Create(long id, int state, List<DtNode> nodes)
        {
            DtNode node = DtNode.Create(this, m_nodes.Count + 1);
            node.id = id;
            node.state = state;
            m_nodes.Add(node);

            nodes.Add(node);
            return node;
        }

        public int GetNodeIdx(DtNode node)
        {
            return node != null ? node.index : 0;
        }

        public DtNode GetNodeAtIdx(int idx)
        {
            return idx != 0 ? m_nodes[idx - 1] : null;
        }

        public DtNode GetNode(long refs)
        {
            return GetNode(refs, 0);
        }

        public Dictionary<long, List<DtNode>> GetNodeMap()
        {
            return m_map;
        }

        public List<DtNode> FetchNodeList()
        {
            if (queueNodeList.Count == 0)
            {
                return new List<DtNode>();
            }
            return queueNodeList.Dequeue();
        }

        public void RecycleNodeList(List<DtNode> obj)
        {
            obj.Clear();
            queueNodeList.Enqueue(obj);
        }

        public DtNode Fetch()
        {
            if (queue.Count == 0)
            {
                return new DtNode();
            }
            return queue.Dequeue();
        }

        public void Recycle(DtNode obj)
        {
            queue.Enqueue(obj);
        }
    }
}
