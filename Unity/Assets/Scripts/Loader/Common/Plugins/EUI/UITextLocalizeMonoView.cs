using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public enum LocalizeType
{
    Static,
    Dynamic,
}

public class UITextLocalizeMonoView: MonoBehaviour
{
    private Text mText;
    private TMPro.TextMeshProUGUI mTextMesh;
    public string textKey;
    private string textDefaultValue;
    public LocalizeType localizeType = LocalizeType.Static;
    [SerializeField]
    private object[] args;
    public static Func<string, string, string, string> GetTextKeyValue;
    private void Awake()
    {
        this.mText = this.GetComponent<Text>();
        this.mTextMesh = this.GetComponent<TMPro.TextMeshProUGUI>();

        if (this.mText != null)
        {
            this.textDefaultValue = this.mText.text;
            this.mText.text = "";
        }
        if (this.mTextMesh != null)
        {
            this.textDefaultValue = this.mTextMesh.text;
            this.mTextMesh.text = "";
        }

        //this.args = null;
        //this.GetTextKeyValue = null;
    }

    private void Start()
    {
        this.OnChangeLanguage();
    }

    public (string, string) GetInfo()
    {
        this.mText = this.GetComponent<Text>();
        this.mTextMesh = this.GetComponent<TMPro.TextMeshProUGUI>();

        if (this.mText != null)
        {
            this.textDefaultValue = this.mText.text;
        }
        if (this.mTextMesh != null)
        {
            this.textDefaultValue = this.mTextMesh.text;
        }
        return (this.textKey, this.textDefaultValue);
    }

#if UNITY_EDITOR
    [ContextMenu("DoChangeLanguage_ResetKey")]
    public void DoChangeLanguage_ResetKey()
    {
        if (Application.isPlaying)
        {
            EditorUtility.DisplayDialog("请注意", $"当前是运行模式,无法修改", "OK");
            return;
        }
        Debug.Log("DoChangeLanguage_ResetKey");
        string textKey = this.GetNodePath();
        if (this.textKey != null && this.textKey.Equals(textKey))
        {
            EditorUtility.DisplayDialog("请注意", $"key 一致，没有改变", "OK");
            return;
        }

        this.textKey = textKey;
        UnityEditor.EditorUtility.SetDirty(this);
    }

    [ContextMenu("DoChangeLanguage_ReShowValue")]
    public void DoChangeLanguage_ReShowValue()
    {
        Debug.Log("DoChangeLanguage_ReShowValue");
        OnChangeLanguage();
    }
#endif

    private void OnDestroy()
    {
        this.args = null;
    }

    public static void SetTextLocalizeAction(Func<string, string, string, string> getTextKeyValue)
    {
        GetTextKeyValue = getTextKeyValue;
    }

    public void DoRefreshTextValue()
    {
        this.OnChangeLanguage();
    }

    private void OnChangeLanguage()
    {
        if (this.localizeType == LocalizeType.Static)
        {
            this.StaticSet();
        }
        else if(this.args != null)
        {
            this.DynamicSet(this.args);
        }
    }

    private void StaticSet()
    {
        this._StaticSet(GetTextKeyValue, "None");
    }

    private void _StaticSet(Func<string, string, string, string> InGetTextKeyValue, string language)
    {
        if (InGetTextKeyValue == null)
        {
            return;
        }
        if (string.IsNullOrEmpty(this.textKey))
        {
            return;
        }
        string textValue = InGetTextKeyValue(language, this.textKey, this.textDefaultValue);
        if (this.mText != null)
        {
            this.mText.text = textValue;
        }
        if (this.mTextMesh != null)
        {
            this.mTextMesh.text = textValue;
        }
    }

    /// <summary>
    /// 动态设置文本内容
    /// </summary>
    public void DynamicSet(params object[] _args)
    {
        this.args = _args;

        this._DynamicSet(GetTextKeyValue, "None", _args);
    }

    /// <summary>
    /// 动态设置文本内容
    /// </summary>
    private void _DynamicSet(Func<string, string, string, string> InGetTextKeyValue, string language, params object[] _args)
    {
        if (InGetTextKeyValue == null)
        {
            return;
        }
        if (string.IsNullOrEmpty(this.textKey))
        {
            return;
        }
        string textValue = InGetTextKeyValue(language, this.textKey, this.textDefaultValue);
        textValue = string.Format(textValue, _args);
        if (this.mText != null)
        {
            this.mText.text = textValue;
        }
        if (this.mTextMesh != null)
        {
            this.mTextMesh.text = textValue;
        }
    }


    public void DoChangeLanguage_Force(string language)
    {
        if (this.localizeType == LocalizeType.Static)
        {
            this._StaticSet(GetTextKeyValue, language);
        }
        else if(this.args != null)
        {
            this._DynamicSet(GetTextKeyValue, language, this.args);
        }
    }

    public string GetNodePath()
    {
        string result = "";
        Transform selectChild = this.transform;
        if (selectChild != null)
        {
            result = selectChild.name;
            while (selectChild.parent != null)
            {
                selectChild = selectChild.parent;
                if (selectChild.name == "Canvas (Environment)")
                {
                    break;
                }
                result = string.Format("{0}/{1}", selectChild.name, result);
                if (selectChild.gameObject.GetComponent<Canvas>() != null)
                {
                    break;
                }
            }
        }

        result = result.Trim();

        return result;
    }
}