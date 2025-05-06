using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class WorldPositionSetter : MonoBehaviour
    {
        Material[] materials;
        public Vector3 originPosition;

        private void OnEnable()
        {
            UpdateTowerPosition();
        }

        void UpdateTowerPosition()
        {
            materials = GetComponent<MeshRenderer>().materials;
            
            foreach(Material m in materials)
            {
                if (m.name.StartsWith("NavMeshWithScan"))
                {
                    m.SetVector("_Center", originPosition);
                    m.renderQueue = 1998;
                }

                if (m.name.StartsWith("NavMeshGlitter"))
                {
                    m.renderQueue = 1997;
                }
            }
        }
    }
}
