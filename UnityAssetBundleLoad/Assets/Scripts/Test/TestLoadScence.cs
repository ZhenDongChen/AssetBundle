using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestLoadScence : MonoBehaviour
{

    public Transform transform;
    public Slider Slider;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(LoadScenceIEnumerator("DockScene"));
    }

    IEnumerator LoadScenceIEnumerator(string scenceName)
    {
        AsyncOperation async = null;

        async = SceneManager.LoadSceneAsync(scenceName);

        // 在allowSceneActivation设置为false时，progress进度最大只能到0.9，因此isDone永远不能为true
        async.allowSceneActivation = false;

        while (async.progress < 0.89999f)
        {
            float temp = async.progress;
            Debug.Log(temp);
            Slider.value = async.progress / 0.89999f;
           
        }
        yield return new WaitForEndOfFrame();
        Slider.value = 1;

        // async.allowSceneActivation = true;
        Debug.Log("加载场景完毕");
    }


    // Update is called once per frame
    void Update()
    {

    }
}
