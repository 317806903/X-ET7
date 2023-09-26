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

using System;
using System.Collections.Generic;
using DotRecast.Core;

namespace DotRecast.Detour
{
    public class DtNode: IDisposable
    {
        public const int DT_NODE_OPEN = 0x01;
        public const int DT_NODE_CLOSED = 0x02;

        /** parent of the node is not adjacent. Found using raycast. */
        public const int DT_NODE_PARENT_DETACHED = 0x04;

        public int index { get; private set; }

        /** Position of the node. */
        public RcVec3f pos = new RcVec3f();

        /** Cost of reaching the given node. */
        public float cost;

        /** Total cost of reaching the goal via the given node including heuristics. */
        public float total;

        /** Index to parent node. */
        public int pidx;

        /**
     * extra state information. A polyRef can have multiple nodes with different extra info. see DT_MAX_STATES_PER_NODE
     */
        public int state;

        /** Node flags. A combination of dtNodeFlags. */
        public int flags;

        /** Polygon ref the node corresponds to. */
        public long id;

        /** Shortcut found by raycast. */
        public List<long> shortcut;

        private DtNodePool _DtNodePool;

        public void SetIndex(DtNodePool dtNodePool, int index)
        {
            this._DtNodePool = dtNodePool;

            this.index = index;
            this.pos = new RcVec3f();
            this.cost = 0;
            this.total = 0;
            this.pidx = 0;
            this.state = 0;
            this.flags = 0;
            this.id = 0;
            this.shortcut = null;
        }

        public override string ToString()
        {
            return "Node [id=" + id + "]";
        }

        public static DtNode Create(DtNodePool dtNodePool, int index)
        {
            DtNode dtNode = dtNodePool.Fetch();
            dtNode.SetIndex(dtNodePool, index);
            return dtNode;
        }

        public void Dispose()
        {
            this._DtNodePool.Recycle(this);
            this._DtNodePool = null;
            this.shortcut = null;
        }
    }
}