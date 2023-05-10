using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ETProjectBrowserSettings : ScriptableObject
{
    public string ListenFolderPath = "./Assets/Scripts/Codes";
    private DirectoryInfo _dir;
    public DirectoryInfo ListenFolderInfo
    {
        get
        {
            if (this._dir != null && this._dir.Exists)
            {
                return this._dir;
            }
            this._dir = new DirectoryInfo(ListenFolderPath);
            return this._dir;
        }
    }
}
