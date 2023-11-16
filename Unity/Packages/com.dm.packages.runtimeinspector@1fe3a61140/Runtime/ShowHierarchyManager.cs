using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RuntimeInspectorNamespace
{
    public class ShowHierarchyManager : MonoBehaviour
    {
        public static ShowHierarchyManager Instance { get; private set; }

#pragma warning disable 0649
        [Header( "Properties" )]
        [SerializeField]
        [HideInInspector]
        [Tooltip( "If enabled, console window will persist between scenes (i.e. not be destroyed when scene changes)" )]
        private bool singleton = true;

        internal RectTransform canvasTR;
        public Transform runtimeInspector;
        public Transform runtimeHierarchy;
        private bool isShow = false;
        public DebugHierarchyPopup popupManager;

        void Awake()
        {
            // Only one instance of debug console is allowed
            if( !Instance )
            {
                Instance = this;

                // If it is a singleton object, don't destroy it between scene changes
                if( singleton )
                    DontDestroyOnLoad( gameObject );
            }
            else if( Instance != this )
            {
                Destroy( gameObject );
                return;
            }
            
            canvasTR = (RectTransform)transform;
            runtimeInspector.gameObject.SetActive(false);
            runtimeHierarchy.gameObject.SetActive(false);
        }

        void Start()
        {
            popupManager.UpdatePosition(true);
        }

        // Update is called once per frame
        public void ShowHierarchyWindow()
        {
            this.isShow = !this.isShow;
            runtimeInspector.gameObject.SetActive(this.isShow);
            runtimeHierarchy.gameObject.SetActive(this.isShow);
        }
    }
}
