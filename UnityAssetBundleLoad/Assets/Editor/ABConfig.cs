using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ABConfig",menuName ="CreateABConfig",order  = 0)]
public class ABConfig : ScriptableObject
{

    public List<string> AllprefabPath = new List<string>();
    public List<FileDirectABName> fileDirectABNames = new List<FileDirectABName>();

    [System.Serializable]
    public struct FileDirectABName
    {
        public string ABName;
        public string Path;
    }

}
