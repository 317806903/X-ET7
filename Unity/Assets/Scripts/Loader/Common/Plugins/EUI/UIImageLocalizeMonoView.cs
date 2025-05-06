using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum LocalizeImageScaleType
{
    Org,
    CurSize,
}

[ExecuteInEditMode]
public class UIImageLocalizeMonoView : MonoBehaviour
{
    public Image mImage;
    public RawImage mRawImage;

    public string path;
    public LocalizeImageScaleType localizeImageScaleType = LocalizeImageScaleType.CurSize;

    public static readonly string UI_MultiLanguagePath = "Assets/ResAB/UI_MultiLanguage";
    public static readonly string UI3D_MultiLanguagePath = "Assets/ResAB/UI3D_MultiLanguage";
    private static Func<string, string, Sprite> SetUIImageByPath;

    public void Awake()
    {
        this.mImage = this.GetComponent<Image>();
        this.mRawImage = this.GetComponent<RawImage>();
    }

    public static void SetImageLocalizeAction(Func<string, string, Sprite> loadImageByPath)
    {
        SetUIImageByPath = loadImageByPath;
    }

#if UNITY_EDITOR
    public string GetImagePathWhenEditor(string language, string path, bool isUI3D)
    {
        if (language == "None")
        {
            language = "CN";
        }

        if (isUI3D)
        {
            return $"{UI3D_MultiLanguagePath}/{language}/{path}";
        }
        return $"{UI_MultiLanguagePath}/{language}/{path}";
    }

    public Sprite SetImageByPathWhenEditor(string language, string path)
    {
        string imgPath = GetImagePathWhenEditor(language, path, false);
        bool isValid = UnityEditor.AssetDatabase.IsValidFolder(imgPath);
        if (isValid)
        {
            return UnityEditor.AssetDatabase.LoadAssetAtPath<Sprite>(imgPath);
        }

        imgPath = GetImagePathWhenEditor(language, path, true);
        isValid = UnityEditor.AssetDatabase.IsValidFolder(imgPath);
        if (isValid)
        {
            return UnityEditor.AssetDatabase.LoadAssetAtPath<Sprite>(imgPath);
        }

        return null;
    }
#endif

    private void OnEnable()
    {
        if (string.IsNullOrEmpty(this.path))
        {
            return;
        }
        LoadImage("None");
    }

    public void DoRefreshImageValue()
    {
        LoadImage("None");
    }

    public void LoadImage(string language)
    {
        if (string.IsNullOrEmpty(this.path))
        {
            Debug.LogError($"string.IsNullOrEmpty(this.path)");
            return;
        }

#if UNITY_EDITOR
        if (Application.isPlaying == false)
        {
            SetUIImageByPath = this.SetImageByPathWhenEditor;
        }
#endif
        if (SetUIImageByPath == null)
        {
            return;
        }
        if (this.mImage != null)
        {
            this.mImage.sprite = SetUIImageByPath(language, this.path);
            if (this.localizeImageScaleType == LocalizeImageScaleType.Org)
            {
                this.mImage.SetNativeSize();
            }
        }
        else if (this.mRawImage != null)
        {
            Sprite sprite = SetUIImageByPath(language, this.path);
            if (sprite == null)
            {
                this.mRawImage.texture = null;
            }
            else
            {
                this.mRawImage.texture = sprite.texture;
                if (this.localizeImageScaleType == LocalizeImageScaleType.Org)
                {
                    this.mRawImage.SetNativeSize();
                }
            }
        }
    }

}