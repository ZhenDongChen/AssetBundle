using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace C_Framework
{

    public partial class ResExporter
    {


        public static Dictionary<string, string> Scene_res_bundle_dic;


        [MenuItem("Export/Windows/Export Scene For Windows")]
        public static void ExportSceneForWindows()
        {
            ExportScene(EditorConst.windows_out_path, BuildTarget.StandaloneWindows);
        }

        [MenuItem("Export/Android/Export Scene For Android")]
        public static void ExportSceneForAndroid()
        {
            ExportScene(EditorConst.andorid_out_path, BuildTarget.Android);
        }

        [MenuItem("Export/IOS/Export Scene For IOS")]
        public static void ExportSceneForIOS()
        {
            ExportScene(EditorConst.ios_out_path, BuildTarget.iOS);
        }



        public static void ExportScene(string outpath, BuildTarget target)
        {
            Scene_res_bundle_dic = new Dictionary<string, string>();
            GetResMap(Scene_res_bundle_dic, "Assets/NewFramework/Interface/Scenes", "scene/", "*.unity", "scene_");
            ClearBundleNames();
            SetAssetImporter(Scene_res_bundle_dic);
            Export(outpath, target);
            ExportBundleDependInfo(outpath);
            ClearBundleNames();
        }
    }

}