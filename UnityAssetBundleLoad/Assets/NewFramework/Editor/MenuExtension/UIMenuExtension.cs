using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace C_Framework
{

    public static class UIMenuExtension
    {

        [MenuItem("GameObject/UI/UIGroup")]
        public static void CreatUIGroup()
        {
            GameObject go = new GameObject("UIGroup_");
            RectTransform rectTrans = go.AddComponent<RectTransform>();
            go.AddComponent<UIGroup>();
            if (Selection.activeTransform != null)
            {
                rectTrans.SetParent(Selection.activeTransform);
                rectTrans.localScale = Vector3.one;
                rectTrans.localPosition = Vector3.zero;
                rectTrans.localEulerAngles = Vector3.zero;
                rectTrans.sizeDelta = Vector2.zero;
            }

        }


        [MenuItem("GameObject/UI/UIList")]
        public static void CreatUIList()
        {
            GameObject go = new GameObject("UIList_");
            RectTransform rectTrans = go.AddComponent<RectTransform>();
            go.AddComponent<UIList>();
            if (Selection.activeTransform != null)
            {
                rectTrans.SetParent(Selection.activeTransform);
                rectTrans.localScale = Vector3.one;
                rectTrans.localPosition = Vector3.zero;
                rectTrans.localEulerAngles = Vector3.zero;
                rectTrans.sizeDelta = Vector2.zero;
            }

        }

        [MenuItem("GameObject/UI/ExRawImage")]
        public static void ExRawImage()
        {
            GameObject go = new GameObject("ExRawImage_");
            RectTransform rectTrans = go.AddComponent<RectTransform>();
            go.AddComponent<ExRawImage>();
            if (Selection.activeTransform != null)
            {
                rectTrans.SetParent(Selection.activeTransform);
                rectTrans.sizeDelta = Vector2.one * 100;
                rectTrans.localScale = Vector3.one;
                rectTrans.localPosition = Vector3.zero;
                rectTrans.localEulerAngles = Vector3.zero;
            }

        }

        [MenuItem("GameObject/UI/ExImage")]
        public static void ExImage()
        {
            GameObject go = new GameObject("ExImage_");
            RectTransform rectTrans = go.AddComponent<RectTransform>();
            go.AddComponent<ExImage>();
            if (Selection.activeTransform != null)
            {
                rectTrans.SetParent(Selection.activeTransform);
                rectTrans.sizeDelta = Vector2.one * 100;
                rectTrans.localScale = Vector3.one;
                rectTrans.localPosition = Vector3.zero;
                rectTrans.localEulerAngles = Vector3.zero;
            }

        }
    }

}