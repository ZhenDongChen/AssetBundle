using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class LoadAssetBundle : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        //AssetBundle assetBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath+"/attack");
        //GameObject obj = GameObject.Instantiate(assetBundle.LoadAsset<GameObject>("attack"));

        LoadSync(LoadObject);

        // XmlDesSerialize();

       // BinarySerilizeTest();

       // DesBinarialize();
    }

    void BinarySerilizeTest()
    {
        TestSerialize testSerialize = new TestSerialize();

        testSerialize.Id = 1;

        testSerialize.Name = "test";
        testSerialize.list = new List<int>();
        testSerialize.list.Add(2);
        testSerialize.list.Add(3);
        BinarySerilize(testSerialize);
    }

    /// <summary>
    /// 二进制的序列化
    /// </summary>
    /// <param name="testSerialize"></param>
    void BinarySerilize(TestSerialize testSerialize)
    {
        FileStream fs = new FileStream(Application.streamingAssetsPath + "/test.byte", FileMode.Create, FileAccess.ReadWrite);

        BinaryFormatter bf = new BinaryFormatter();

        bf.Serialize(fs, testSerialize);
        fs.Close();

    }

    //void DesBinarialize()
    //{
    //    // FileStream fs = new FileStream(Application.streamingAssetsPath+"/test.byte",FileMode.Open,FileAccess.ReadWrite);
    //    TextAsset textAsset = UnityEditor.AssetDatabase.LoadAssetAtPath<TextAsset>(Application.dataPath +"/test.byte");
    //    MemoryStream stream = new MemoryStream(textAsset.bytes);

    //    BinaryFormatter bf = new BinaryFormatter();

    //    TestSerialize testSerialize = (TestSerialize)bf.Deserialize(stream);
    //    Debug.Log(testSerialize.Id + testSerialize.Name);
    //    stream.Close();


    //}


    /// <summary>
    /// 反序列化吧XML转成类u
    /// </summary>
    /// <returns></returns>
    TestSerialize XmlDesSerialize()
    {
        FileStream fileStream = new FileStream(Application.streamingAssetsPath+"/test.xml",FileMode.Open,FileAccess.ReadWrite);
        //StreamReader SR = new StreamReader(fileStream,System.Text.Encoding.UTF8);
        XmlSerializer xs = new XmlSerializer(typeof(TestSerialize));
        TestSerialize testSerialize =(TestSerialize)xs.Deserialize(fileStream);
        fileStream.Close();

        Debug.Log(testSerialize.Id + testSerialize.Name);
        return testSerialize;

    }

    /// <summary>
    /// 类的序列化
    /// </summary>
    void XmlSerilizeTest()
    {
        TestSerialize testSerialize = new TestSerialize();

        testSerialize.Id = 1;

        testSerialize.Name = "test";
        testSerialize.list = new List<int>();
        testSerialize.list.Add(2);
        testSerialize.list.Add(3);
        XmlSerilize(testSerialize);
    }

    void XmlSerilize(TestSerialize testSerialize)
    {
        FileStream fileStream = new FileStream(Application.streamingAssetsPath+"/test.xml",FileMode.Create,FileAccess.ReadWrite,FileShare.ReadWrite);

        StreamWriter streamWriter = new StreamWriter(fileStream,System.Text.Encoding.UTF8);
        XmlSerializer xml = new XmlSerializer(testSerialize.GetType());
        xml.Serialize(streamWriter,testSerialize);
        streamWriter.Close();
        fileStream.Close();

    }


    public void LoadObject(GameObject Object)
    {
        Object.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
    }

    void LoadSync(Action<GameObject> ab)
    {

        StartCoroutine(LoadAllAssetBundle(LoadObject));

    }

    IEnumerator LoadAllAssetBundle(Action<GameObject> ab)
    {
        AssetBundleCreateRequest assetBundle = AssetBundle.LoadFromFileAsync(Application.streamingAssetsPath + "/attack1/attack");
        yield return assetBundle;
        if (ab != null)
        {
            ab(GameObject.Instantiate(assetBundle.assetBundle.LoadAsset<GameObject>("attack")));
        }
       
    }

    // Update is called once per frame
    void Update()
    {

    }
}
