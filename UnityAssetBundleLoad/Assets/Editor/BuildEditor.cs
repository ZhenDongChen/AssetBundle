using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class BuildEditor 
{
    public static string ABCONFIGPATH = "Assets/Editor/ABConfig.asset";

    //key是ab包名，value是路径，所有文件ab包是dic
    public static Dictionary<string, string> allFileDir = new Dictionary<string, string>();

    /// <summary>
    /// 单个prefab的ab包
    /// </summary>
    public static Dictionary<string, List<string>> allPrefabDir = new Dictionary<string, List<string>>();

    //过滤List
    public static List<string> allFileAb = new List<string>();



    [MenuItem("Tools/BuildAssetBundle")]
    public static void BuildBundle()
    {
        allFileDir.Clear();
        allFileAb.Clear();
        allPrefabDir.Clear();
        ABConfig aBConfig = AssetDatabase.LoadAssetAtPath<ABConfig>(ABCONFIGPATH);

        foreach (var fileDir in aBConfig.fileDirectABNames)
        {
            if (allFileDir.ContainsKey(fileDir.ABName))
            {
                Debug.LogErrorFormat("AB包配置重名，请检查{0}", fileDir.ABName);
            }
            else
            {
                allFileDir.Add(fileDir.ABName, fileDir.Path);
                allFileAb.Add(fileDir.Path);
                Debug.LogFormat("fileDir.ABName:{0} fileDir.Path:{1}", fileDir.ABName, fileDir.Path);
            }
        }

        ///能找到这个目录下面所有的prefab
        string[] allStr = AssetDatabase.FindAssets("t:Prefab",aBConfig.AllprefabPath.ToArray());

        for (int i = 0; i < allStr.Length; i++)
        {
            //Debug.Log(allStr[i]);
            string path = AssetDatabase.GUIDToAssetPath(allStr[i]);
          //  Debug.LogFormat("allStr[i]:{0}", allStr[i]);
            EditorUtility.DisplayProgressBar("查找Prefab","prefabPath:"+ path, i/allStr.Length);
            if (!ContainAllFileAB(path))
            {
                GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                string[] allDepend = AssetDatabase.GetDependencies(path);
                List<string> allDepend_Path = new List<string>();

              
                for (int depend_Index = 0; depend_Index < allDepend.Length; depend_Index++)
                {
                    Debug.Log(allDepend[depend_Index]);
                    if (!ContainAllFileAB(allDepend[depend_Index]) && !allDepend[depend_Index].EndsWith(".cs"))
                    {
                        allFileAb.Add(allDepend[depend_Index]);
                        allDepend_Path.Add(allDepend[depend_Index]);
                    }
                }

                if (allPrefabDir.ContainsKey(obj.name))
                {
                    Debug.LogErrorFormat("已经包含了相同的prefab:{0}", obj.name);
                }
                else
                {
                    allPrefabDir.Add(obj.name, allDepend_Path);
                }

               
            }
        }
      


        foreach (string name in allFileDir.Keys)
        {
            SetABName(name,allFileDir[name]);
        }

        foreach (string name in allPrefabDir.Keys)
        {
            SetABName(name,allPrefabDir[name]);
        }


        BuildAssetBuild();

        BuildPipeline.BuildAssetBundles(Application.streamingAssetsPath+ "/attack1", BuildAssetBundleOptions.ChunkBasedCompression, EditorUserBuildSettings.activeBuildTarget);

        //string[] oldABName = AssetDatabase.GetAllAssetBundleNames();
        //for (int ABName_index = 0; ABName_index < oldABName.Length; ABName_index++)
        //{
        //    AssetDatabase.RemoveAssetBundleName(oldABName[ABName_index],true);
        //    EditorUtility.DisplayProgressBar("清除AB包名字","名字："+oldABName[ABName_index],ABName_index*1.0f / oldABName.Length);
        //}
        EditorUtility.ClearProgressBar();

        //生成AB包配置表


       
    }



    static void WriteData(Dictionary<string,string> resPathDic)
    {
       AssetBundleConfig config = new AssetBundleConfig();
        config.ABBasList = new List<ABBase>();
        foreach (var path in resPathDic.Keys)
        {
            ABBase aBBase = new ABBase();
            aBBase.Path = path;
            aBBase.Crc = CRC32.GetCRC32(path);
            aBBase.AssetName = path.Remove(0, path.LastIndexOf("/") + 1);
            aBBase.ABName = resPathDic[path];

            string[] resdepence = AssetDatabase.GetDependencies(path);
            foreach (var item in resdepence)
            {
                if (item == path || path.EndsWith(".cs"))
                    continue;

                string abname = "";

                if (resPathDic.TryGetValue(item, out abname))
                {
                    if (abname == resPathDic[item])
                        continue;

                    if (!aBBase.ABDependce.Contains(abname))
                    {
                        aBBase.ABDependce.Add(abname);

                    }
                }
            }

            config.ABBasList.Add(aBBase);
        }


        //写入XML
        string xmlPath = Application.dataPath + "AssetbundleConfig.xml";
        if (File.Exists(xmlPath)) File.Delete(xmlPath);
        FileStream fileStream = new FileStream(xmlPath, FileMode.Create,FileAccess.ReadWrite);

        StreamWriter sw = new StreamWriter(fileStream,System.Text.Encoding.UTF8);

        XmlSerializer xs = new XmlSerializer(config.GetType());
        xs.Serialize(sw,config);

        sw.Close();
        fileStream.Close();


        ///写入二进制
        string bytePath = Application.dataPath+"/.." + "/AssetBundleConfig.bytes";

        FileStream fs = new FileStream(bytePath,FileMode.Create,FileAccess.ReadWrite,FileShare.ReadWrite);

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fs,config);
        fs.Close();


    }


    static void BuildAssetBuild()
    {
        string[] allBundles = AssetDatabase.GetAllAssetBundleNames();

        //key:全路径，value为包名
        Dictionary<string, string> resPathDic = new Dictionary<string, string>();

        for (int allBundles_index    = 0; allBundles_index < allBundles.Length; allBundles_index++)
        {
            string[] allBundlePath = AssetDatabase.GetAssetPathsFromAssetBundle(allBundles[allBundles_index]);
            for (int allBundlePath_Index = 0; allBundlePath_Index < allBundlePath.Length; allBundlePath_Index++)
            {
                if (allBundlePath[allBundlePath_Index].EndsWith(".cs"))
                {
                    continue;
                }
                Debug.LogFormat("此ab包名字：{0} 下面包含的路径为{1}", allBundles[allBundles_index], allBundlePath[allBundlePath_Index]);
                resPathDic.Add(allBundlePath[allBundlePath_Index],allBundles[allBundles_index]);

            }
        }

        WriteData(resPathDic);
    }


    static void SetABName(string name,string path)
    {

        AssetImporter assetImport = AssetImporter.GetAtPath(path);
        if (assetImport == null)
        {
            Debug.LogErrorFormat("不存在此文件：{0}", name);
        }
        else
        {
            assetImport.assetBundleName = name;
        }
    }

    static void SetABName(string name, List<string> path)
    {
        for (int path_index = 0; path_index < path.Count; path_index++)
        {
            SetABName(name, path[path_index]);
        }
    }


    static bool ContainAllFileAB(string path)
    {
        for (int i = 0; i < allFileAb.Count; i++)
        {
            if (path == allFileAb[i] || path.Contains(allFileAb[i]))
            {
                return true;
            }
        }

        return false;
    }
  
}
