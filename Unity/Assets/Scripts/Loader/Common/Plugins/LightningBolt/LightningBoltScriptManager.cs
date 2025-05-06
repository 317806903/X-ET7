//
// Lightning Bolt for Unity
// (c) 2016 Digital Ruby, LLC
// Source code may be used for personal or commercial projects.
// Source code may NOT be redistributed or sold.
//

using System;
using UnityEngine;
using System.Collections.Generic;

namespace DigitalRuby.LightningBolt
{
    public class LightningBoltScriptManager : MonoBehaviour
    {
        public float speed = 1;
        public float initTime = 3;
        private float waitTime = 0;

        private Vector3 startPos;
        private Vector3 lastPos;

        private Dictionary<long, Vector3> dicKey2Pos;
        private List<long> midKeys;
        private int midIndex = -1;

        private List<LightningBoltScript> lightningBoltScriptList;
        private LightningBoltScript lightningBoltScript;

        private void Awake()
        {
            this.dicKey2Pos = new();
            this.midKeys = new();
            this.midIndex = -1;

            this.lightningBoltScript = this.GetComponentInChildren<LightningBoltScript>(true);
            this.lightningBoltScript.gameObject.SetActive(false);
            this.lightningBoltScriptList = new();
        }

        private void OnEnable()
        {
            this.dicKey2Pos.Clear();
            this.midKeys.Clear();
            foreach (LightningBoltScript lightningBoltScriptTmp in this.lightningBoltScriptList)
            {
                DestroyImmediate(lightningBoltScriptTmp.gameObject);
            }
            this.lightningBoltScriptList.Clear();

            this.waitTime = 0;
            this.midIndex = -1;
            this.startPos = this.GetCurPos();
            this.lastPos = this.GetCurPos();
            this.AddLightningBoltScript(this.startPos, this.lastPos);
        }

        private void OnDisable()
        {
            this.dicKey2Pos.Clear();
            this.midKeys.Clear();
            foreach (LightningBoltScript lightningBoltScriptTmp in this.lightningBoltScriptList)
            {
                DestroyImmediate(lightningBoltScriptTmp.gameObject);
            }
            this.lightningBoltScriptList.Clear();
        }

        private void Update()
        {
            this.lastPos = this.GetCurPos();
            this.ChgLastPos();

            if (this.waitTime >= this.initTime)
            {
                this.ChgStartPos();
            }
            else
            {
                this.waitTime += Time.deltaTime;
            }
        }

        private Vector3 GetCurPos()
        {
            return this.transform.position;
        }

        private void ChgStartPos()
        {
            Vector3 firstNearPos = this.GetFirstNearPos();
            Vector3 dis = firstNearPos - this.startPos;
            float disDelta = this.speed * Time.deltaTime;
            if (dis.sqrMagnitude <= disDelta * disDelta)
            {
                if (firstNearPos.Equals(this.lastPos))
                {
                    this.startPos = firstNearPos;
                }
                else
                {
                    this.startPos = firstNearPos;
                    long key = this.midKeys[this.midIndex];
                    this.midIndex++;

                    Destroy(this.lightningBoltScriptList[0].gameObject);
                    this.lightningBoltScriptList.RemoveAt(0);
                }
            }
            else
            {
                this.startPos += dis.normalized * disDelta;
            }

            this.lightningBoltScriptList[0].StartPosition = this.startPos;
        }

        private void ChgLastPos()
        {
            if (this.lightningBoltScriptList.Count > 0)
            {
                this.lightningBoltScriptList[this.lightningBoltScriptList.Count - 1].EndPosition = this.lastPos;
            }
        }

        private Vector3 GetFirstNearPos()
        {
            if (this.midIndex == -1 || this.midIndex >= this.midKeys.Count)
            {
                return this.lastPos;
            }

            long key = this.midKeys[this.midIndex];
            return this.dicKey2Pos[key];
        }

        public void AddMidPos(long key, Vector3 pos)
        {
            this.dicKey2Pos[key] = pos;
            this.midKeys.Add(key);
            if (this.midIndex == -1)
            {
                this.midIndex = 0;
            }

            this.lightningBoltScriptList[this.lightningBoltScriptList.Count - 1].EndPosition = pos;
            this.AddLightningBoltScript(pos, this.lastPos);
        }

        public int GetMidIndex()
        {
            return this.midIndex;
        }

        public void ResetDicKeyPos(List<(long key, Vector3 pos)> dic)
        {
            int count = dic.Count;
            for (int i = 0; i < this.midKeys.Count; i++)
            {
                long key = dic[i].key;
                Vector3 pos = dic[i].pos;

                if (this.midIndex <= i)
                {
                    if (pos.Equals(Vector3.zero))
                    {
                        continue;
                    }
                    this.dicKey2Pos[key] = pos;
                    this.lightningBoltScriptList[i - this.midIndex].EndPosition = pos;
                    this.lightningBoltScriptList[i - this.midIndex + 1].StartPosition = pos;
                }
            }
            for (int i = this.midKeys.Count; i < count; i++)
            {
                long key = dic[i].key;
                Vector3 pos = dic[i].pos;
                this.AddMidPos(key, pos);
            }
        }

        private void AddLightningBoltScript(Vector3 startPos, Vector3 endPos)
        {
            GameObject newGo = GameObject.Instantiate(this.lightningBoltScript.gameObject);
            newGo.SetActive(true);
            newGo.transform.SetParent(this.transform);
            LightningBoltScript lightningBoltScript = newGo.GetComponent<LightningBoltScript>();
            lightningBoltScript.StartObject = null;
            lightningBoltScript.EndObject = null;
            lightningBoltScript.StartPosition = startPos;
            lightningBoltScript.EndPosition = endPos;
            this.lightningBoltScriptList.Add(lightningBoltScript);
        }
    }
}