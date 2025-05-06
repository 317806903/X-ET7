using UnityEngine;
using UnityEngine.UI;

namespace DynamicAtlas
{
    [RequireComponent(typeof (Image))]
    public class DynamicImage: MonoBehaviour
    {
        public DynamicAtlasGroup atlasGroup = DynamicAtlasGroup.Size_2048;

        private Image m_Image;
        public Image Image
        {
            get
            {
                if (this.m_Image == null)
                {
                    this.m_Image = this.gameObject.GetComponent<Image>();
                }
                return this.m_Image;
            }
        }

        private DynamicAtlasGroup m_Group;
        public DynamicAtlasGroup Group
        {
            get
            {
                return this.m_Group;
            }
        }
        private DynamicAtlas m_Atlas;
        public DynamicAtlas Atlas
        {
            get
            {
                return this.m_Atlas;
            }
        }
        public Sprite m_DefaultSprite;
        public Sprite DefaultSprite
        {
            get
            {
                return this.m_DefaultSprite;
            }
        }
        public string m_SpriteName;
        public string SpriteName
        {
            get
            {
                return this.m_SpriteName;
            }
        }

        protected void Awake()
        {
            this.atlasGroup = DynamicAtlasGroup.Size_2048;
#if UNITY_EDITOR
            //在编辑器下 退出playmode会再走一次start
            if (Application.isPlaying)
            {
                OnPreDoImage();
            }
#else
            OnPreDoImage();
#endif
        }

        private void OnPreDoImage()
        {
            if (this.ChkStatus() == false)
            {
                return;
            }

            if (this.Image.sprite != null) //事先挂载了一张图片
            {
                if (this.ChkTextureCanDynamic(this.Image.sprite.texture) == false)
                {
                    return;
                }

                SetGroup(atlasGroup);
                if (this.m_DefaultSprite == null) //事先挂载了一张图片
                {
                    //可以先放入到图集中去，在使用这一张图集里面的图片
                    SetImage();
                }
            }
        }

        private void SetGroup(DynamicAtlasGroup group)
        {
            if (m_Atlas != null)
            {
                return;
            }

            m_Group = group;
            m_Atlas = DynamicAtlasMgr.Instance.GetDynamicAtlas(group);
        }

        private void SetImage()
        {
            m_DefaultSprite = this.Image.sprite;
            m_SpriteName = this.Image.mainTexture.name;
            m_Atlas.SetTexture(this.Image.mainTexture, OnGetImageCallBack);
        }

        private void OnGetImageCallBack(Texture tex, Rect rect)
        {
            if (this.Image == null)
            {
                return;
            }
            // Debug.LogError(111);
            int length = (int)m_Group;
            Rect spriteRect = rect;
            // spriteRect.x *= length;
            // spriteRect.y *= length;
            // spriteRect.width *= length;
            // spriteRect.height *= length;

            if (m_DefaultSprite != null)
                this.Image.sprite = Sprite.Create((Texture2D)tex, spriteRect, m_DefaultSprite.pivot, m_DefaultSprite.pixelsPerUnit, 1,
                    SpriteMeshType.Tight, m_DefaultSprite.border);
            else
            {
                this.Image.sprite = Sprite.Create((Texture2D)tex, spriteRect, new Vector2(spriteRect.width * .5f, spriteRect.height * .5f), 100, 1,
                    SpriteMeshType.Tight, new Vector4(0, 0, 0, 0));
                m_DefaultSprite = this.Image.sprite;
            }
        }

        #region Public Func

        public bool ChkStatus()
        {
            if (DynamicAtlasMgr.Instance == null)
            {
                return false;
            }
            return DynamicAtlasMgr.Instance.Status;
        }

        public bool ChkTextureCanDynamic(Texture2D texture)
        {
            return DynamicAtlasPage.ChkTextureCanDynamic(texture);
        }

        public void SetImage(string name, Texture2D texture)
        {
            if (this.ChkStatus() == false)
            {
                return;
            }

            if (m_Atlas == null)
            {
                SetGroup(atlasGroup);
            }

            if (!string.IsNullOrEmpty(m_SpriteName) && m_SpriteName.Equals(name))
            {
                string textureName = texture.name;
                if (textureName.StartsWith("DynamicAtlas-"))
                {
                    return;
                }
                if (this.Image != null && this.Image.mainTexture != null)
                {
                    string mainTextureName = this.Image.mainTexture.name;
                    if (mainTextureName.StartsWith("DynamicAtlas-"))
                    {
                        return;
                    }

                    if (System.IO.Path.GetFileNameWithoutExtension(name).Equals(mainTextureName))
                    {
                        return;
                    }
                }
            }

            m_SpriteName = name;
            m_Atlas.GetTeture(name, texture, OnGetImageCallBack);
        }

        public void RemoveImage(bool clearRange = false)
        {
            if (m_Atlas == null) //并没有使用图集
                return;

            if (!string.IsNullOrEmpty(m_SpriteName))
            {
                m_Atlas.RemoveTexture(m_SpriteName, clearRange);
            }
        }

        #endregion
    }
}