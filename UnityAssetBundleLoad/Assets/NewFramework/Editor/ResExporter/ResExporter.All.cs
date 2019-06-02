using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace C_Framework
{

    public partial class ResExporter
    {

        [MenuItem("Export/Windows/Export ALL and CreatMD5")]
        public static void ExportAllAndCreatMD5ForWindows()
        {
            if (Directory.Exists(EditorConst.windows_md5_root))
            {
                Directory.Delete(EditorConst.windows_md5_root, true);
            }
            if (Directory.Exists(EditorConst.windows_md5_root))
            {
                Directory.Delete(EditorConst.windows_md5_root, true);
            }

            UnityEngine.Debug.LogFormat("ExportAllAndCreatMD5ForWindows begin.");
            ExportUIForWindows();
            ExportModelForWindows();
            ExportSceneForWindows();
            ExportLuaForWindows();
            ExportMD5ForWindows();
            UnityEngine.Debug.LogFormat("ExportAllAndCreatMD5ForWindows end.");
        }

        [MenuItem("Export/Android/Export ALL and CreatMD5")]
        public static void ExportAllAndCreatMD5ForAndroid()
        {
            if (Directory.Exists(EditorConst.andorid_md5_root))
            {
                Directory.Delete(EditorConst.andorid_md5_root, true);
            }
            ExportUIForAndroid();
            ExportModelForAndroid();
            ExportSceneForAndroid();
            ExportLuaForAndroid();
            ExportMD5ForAndroid();
        }

        [MenuItem("Export/IOS/Export ALL and CreatMD5")]
        public static void ExportAllAndCreatMD5ForIOS()
        {
            if (Directory.Exists(EditorConst.ios_md5_root))
            {
                Directory.Delete(EditorConst.ios_md5_root, true);
            }
            ExportUIForIOS();
            ExportModelForIOS();
            ExportSceneForIOS();
            ExportLuaForIOS();
            ExportMD5ForIOS();
        }

    }

}