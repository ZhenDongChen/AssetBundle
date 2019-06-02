using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace C_Framework
{

    public partial class ResExporter
    {

        public static Dictionary<string, string> model_res_bundle_dic;
        public static Dictionary<string, string> model_dep_res_bundle_dic;


        [MenuItem("Export/Windows/Export Model For Windows")]
        public static void ExportModelForWindows()
        {
            Exportbuilding(EditorConst.windows_out_path, BuildTarget.StandaloneWindows);
            ExportEquip(EditorConst.windows_out_path, BuildTarget.StandaloneWindows);
            ExportFightEffects(EditorConst.windows_out_path, BuildTarget.StandaloneWindows);
            ExportFightFightTarget(EditorConst.windows_out_path, BuildTarget.StandaloneWindows);
            ExportFightGuns(EditorConst.windows_out_path, BuildTarget.StandaloneWindows);
            ExportIsland(EditorConst.windows_out_path, BuildTarget.StandaloneWindows); ;
            ExportIslandPath(EditorConst.windows_out_path, BuildTarget.StandaloneWindows);
            ExportPlane(EditorConst.windows_out_path, BuildTarget.StandaloneWindows);
            ExportPVE_GuanKa(EditorConst.windows_out_path, BuildTarget.StandaloneWindows); 
            ExportShips(EditorConst.windows_out_path, BuildTarget.StandaloneWindows);
            ExportWaypointLine(EditorConst.windows_out_path, BuildTarget.StandaloneWindows);
        }

        [MenuItem("Export/Android/Export Model For Android")]
        public static void ExportModelForAndroid()
        {
            Exportbuilding(EditorConst.windows_out_path, BuildTarget.StandaloneWindows);
            ExportEquip(EditorConst.windows_out_path, BuildTarget.StandaloneWindows);
            ExportFightEffects(EditorConst.windows_out_path, BuildTarget.StandaloneWindows);
            ExportFightFightTarget(EditorConst.windows_out_path, BuildTarget.StandaloneWindows);
            ExportFightGuns(EditorConst.windows_out_path, BuildTarget.StandaloneWindows);
            ExportIsland(EditorConst.windows_out_path, BuildTarget.StandaloneWindows); ;
            ExportIslandPath(EditorConst.windows_out_path, BuildTarget.StandaloneWindows);
            ExportPlane(EditorConst.windows_out_path, BuildTarget.StandaloneWindows);
            ExportPVE_GuanKa(EditorConst.windows_out_path, BuildTarget.StandaloneWindows);
            ExportShips(EditorConst.windows_out_path, BuildTarget.StandaloneWindows);
            ExportWaypointLine(EditorConst.windows_out_path, BuildTarget.StandaloneWindows);
        }

        [MenuItem("Export/IOS/Export Model For IOS")]
        public static void ExportModelForIOS()
        {
            Exportbuilding(EditorConst.windows_out_path, BuildTarget.StandaloneWindows);
            ExportEquip(EditorConst.windows_out_path, BuildTarget.StandaloneWindows);
            ExportFightEffects(EditorConst.windows_out_path, BuildTarget.StandaloneWindows);
            ExportFightFightTarget(EditorConst.windows_out_path, BuildTarget.StandaloneWindows);
            ExportFightGuns(EditorConst.windows_out_path, BuildTarget.StandaloneWindows);
            ExportIsland(EditorConst.windows_out_path, BuildTarget.StandaloneWindows); ;
            ExportIslandPath(EditorConst.windows_out_path, BuildTarget.StandaloneWindows);
            ExportPlane(EditorConst.windows_out_path, BuildTarget.StandaloneWindows);
            ExportPVE_GuanKa(EditorConst.windows_out_path, BuildTarget.StandaloneWindows);
            ExportShips(EditorConst.windows_out_path, BuildTarget.StandaloneWindows);
            ExportWaypointLine(EditorConst.windows_out_path, BuildTarget.StandaloneWindows);
        }


        public static void Exportbuilding(string outpath, BuildTarget target)
        {
            model_res_bundle_dic = new Dictionary<string, string>();
            model_dep_res_bundle_dic = new Dictionary<string, string>();
            GetResMap(model_res_bundle_dic, "Assets/Models/Building", "Building/", "*.prefab", "");
            GetResDepencies(model_res_bundle_dic, ref model_dep_res_bundle_dic, new string[] { "png", "jpg" }, "texture/", "t_");
            //GetResDepencies(model_res_bundle_dic, ref model_dep_res_bundle_dic, new string[] { "mat" }, "mat/", "m_");
            GetResDepencies(model_res_bundle_dic, ref model_dep_res_bundle_dic, new string[] { "shader" }, "shader/", "s_");
            CombineDictionary(model_res_bundle_dic, model_dep_res_bundle_dic);
            ClearBundleNames();
            SetAssetImporter(model_res_bundle_dic);
            Export(outpath, target);
            ExportBundleDependInfo(outpath);
            ClearBundleNames();
        }


        public static void ExportEquip(string outpath, BuildTarget target)
        {
            model_res_bundle_dic = new Dictionary<string, string>();
            model_dep_res_bundle_dic = new Dictionary<string, string>();
            GetResMap(model_res_bundle_dic, "Assets/Models/Equip", "Equip/", "*.prefab", "");
            GetResDepencies(model_res_bundle_dic, ref model_dep_res_bundle_dic, new string[] { "png", "jpg" }, "texture/", "t_");
            //GetResDepencies(model_res_bundle_dic, ref model_dep_res_bundle_dic, new string[] { "mat" }, "mat/", "m_");
            GetResDepencies(model_res_bundle_dic, ref model_dep_res_bundle_dic, new string[] { "shader" }, "shader/", "s_");
            CombineDictionary(model_res_bundle_dic, model_dep_res_bundle_dic);
            ClearBundleNames();
            SetAssetImporter(model_res_bundle_dic);
            Export(outpath, target);
            ExportBundleDependInfo(outpath);
            ClearBundleNames();
        }

        public static void ExportFightEffects(string outpath, BuildTarget target)
        {
            model_res_bundle_dic = new Dictionary<string, string>();
            model_dep_res_bundle_dic = new Dictionary<string, string>();
            GetResMap(model_res_bundle_dic, "Assets/Models/FightEffects", "FightEffects/", "*.prefab", "");
            GetResDepencies(model_res_bundle_dic, ref model_dep_res_bundle_dic, new string[] { "png", "jpg" }, "texture/", "t_");
            //GetResDepencies(model_res_bundle_dic, ref model_dep_res_bundle_dic, new string[] { "mat" }, "mat/", "m_");
            GetResDepencies(model_res_bundle_dic, ref model_dep_res_bundle_dic, new string[] { "shader" }, "shader/", "s_");
            CombineDictionary(model_res_bundle_dic, model_dep_res_bundle_dic);
            ClearBundleNames();
            SetAssetImporter(model_res_bundle_dic);
            Export(outpath, target);
            ExportBundleDependInfo(outpath);
            ClearBundleNames();
        }

        public static void ExportFightFightTarget(string outpath, BuildTarget target)
        {
            model_res_bundle_dic = new Dictionary<string, string>();
            model_dep_res_bundle_dic = new Dictionary<string, string>();
            GetResMap(model_res_bundle_dic, "Assets/Models/FightTarget", "FightTarget/", "*.prefab", "");
            GetResDepencies(model_res_bundle_dic, ref model_dep_res_bundle_dic, new string[] { "png", "jpg" }, "texture/", "t_");
            //GetResDepencies(model_res_bundle_dic, ref model_dep_res_bundle_dic, new string[] { "mat" }, "mat/", "m_");
            GetResDepencies(model_res_bundle_dic, ref model_dep_res_bundle_dic, new string[] { "shader" }, "shader/", "s_");
            CombineDictionary(model_res_bundle_dic, model_dep_res_bundle_dic);
            ClearBundleNames();
            SetAssetImporter(model_res_bundle_dic);
            Export(outpath, target);
            ExportBundleDependInfo(outpath);
            ClearBundleNames();
        }
        public static void ExportFightGuns(string outpath, BuildTarget target)
        {
            model_res_bundle_dic = new Dictionary<string, string>();
            model_dep_res_bundle_dic = new Dictionary<string, string>();
            GetResMap(model_res_bundle_dic, "Assets/Models/Guns", "Guns/", "*.prefab", "");
            GetResDepencies(model_res_bundle_dic, ref model_dep_res_bundle_dic, new string[] { "png", "jpg" }, "texture/", "t_");
            //GetResDepencies(model_res_bundle_dic, ref model_dep_res_bundle_dic, new string[] { "mat" }, "mat/", "m_");
            GetResDepencies(model_res_bundle_dic, ref model_dep_res_bundle_dic, new string[] { "shader" }, "shader/", "s_");
            CombineDictionary(model_res_bundle_dic, model_dep_res_bundle_dic);
            ClearBundleNames();
            SetAssetImporter(model_res_bundle_dic);
            Export(outpath, target);
            ExportBundleDependInfo(outpath);
            ClearBundleNames();
        }
        public static void ExportIsland(string outpath, BuildTarget target)
        {
            model_res_bundle_dic = new Dictionary<string, string>();
            model_dep_res_bundle_dic = new Dictionary<string, string>();
            GetResMap(model_res_bundle_dic, "Assets/Models/Island", "Island/", "*.prefab", "");
            GetResDepencies(model_res_bundle_dic, ref model_dep_res_bundle_dic, new string[] { "png", "jpg" }, "texture/", "t_");
            //GetResDepencies(model_res_bundle_dic, ref model_dep_res_bundle_dic, new string[] { "mat" }, "mat/", "m_");
            GetResDepencies(model_res_bundle_dic, ref model_dep_res_bundle_dic, new string[] { "shader" }, "shader/", "s_");
            CombineDictionary(model_res_bundle_dic, model_dep_res_bundle_dic);
            ClearBundleNames();
            SetAssetImporter(model_res_bundle_dic);
            Export(outpath, target);
            ExportBundleDependInfo(outpath);
            ClearBundleNames();
        }

        public static void ExportIslandPath(string outpath, BuildTarget target)
        {
            model_res_bundle_dic = new Dictionary<string, string>();
            model_dep_res_bundle_dic = new Dictionary<string, string>();
            GetResMap(model_res_bundle_dic, "Assets/Models/IslandPath", "IslandPath/", "*.prefab", "");
            GetResDepencies(model_res_bundle_dic, ref model_dep_res_bundle_dic, new string[] { "png", "jpg" }, "texture/", "t_");
            //GetResDepencies(model_res_bundle_dic, ref model_dep_res_bundle_dic, new string[] { "mat" }, "mat/", "m_");
            GetResDepencies(model_res_bundle_dic, ref model_dep_res_bundle_dic, new string[] { "shader" }, "shader/", "s_");
            CombineDictionary(model_res_bundle_dic, model_dep_res_bundle_dic);
            ClearBundleNames();
            SetAssetImporter(model_res_bundle_dic);
            Export(outpath, target);
            ExportBundleDependInfo(outpath);
            ClearBundleNames();
        }

        public static void ExportPlane(string outpath, BuildTarget target)
        {
            model_res_bundle_dic = new Dictionary<string, string>();
            model_dep_res_bundle_dic = new Dictionary<string, string>();
            GetResMap(model_res_bundle_dic, "Assets/Models/Plane", "Plane/", "*.prefab", "");
            GetResDepencies(model_res_bundle_dic, ref model_dep_res_bundle_dic, new string[] { "png", "jpg" }, "texture/", "t_");
            //GetResDepencies(model_res_bundle_dic, ref model_dep_res_bundle_dic, new string[] { "mat" }, "mat/", "m_");
            GetResDepencies(model_res_bundle_dic, ref model_dep_res_bundle_dic, new string[] { "shader" }, "shader/", "s_");
            CombineDictionary(model_res_bundle_dic, model_dep_res_bundle_dic);
            ClearBundleNames();
            SetAssetImporter(model_res_bundle_dic);
            Export(outpath, target);
            ExportBundleDependInfo(outpath);
            ClearBundleNames();
        }
        public static void ExportPVE_GuanKa(string outpath, BuildTarget target)
        {
            model_res_bundle_dic = new Dictionary<string, string>();
            model_dep_res_bundle_dic = new Dictionary<string, string>();
            GetResMap(model_res_bundle_dic, "Assets/Models/PVE_GuanKa", "PVE_GuanKa/", "*.prefab", "");
            GetResDepencies(model_res_bundle_dic, ref model_dep_res_bundle_dic, new string[] { "png", "jpg" }, "texture/", "t_");
            //GetResDepencies(model_res_bundle_dic, ref model_dep_res_bundle_dic, new string[] { "mat" }, "mat/", "m_");
            GetResDepencies(model_res_bundle_dic, ref model_dep_res_bundle_dic, new string[] { "shader" }, "shader/", "s_");
            CombineDictionary(model_res_bundle_dic, model_dep_res_bundle_dic);
            ClearBundleNames();
            SetAssetImporter(model_res_bundle_dic);
            Export(outpath, target);
            ExportBundleDependInfo(outpath);
            ClearBundleNames();
        }
        public static void ExportShips(string outpath, BuildTarget target)
        {
            model_res_bundle_dic = new Dictionary<string, string>();
            model_dep_res_bundle_dic = new Dictionary<string, string>();
            GetResMap(model_res_bundle_dic, "Assets/Models/Ships", "Ships/", "*.prefab", "");
            GetResDepencies(model_res_bundle_dic, ref model_dep_res_bundle_dic, new string[] { "png", "jpg" }, "texture/", "t_");
            //GetResDepencies(model_res_bundle_dic, ref model_dep_res_bundle_dic, new string[] { "mat" }, "mat/", "m_");
            GetResDepencies(model_res_bundle_dic, ref model_dep_res_bundle_dic, new string[] { "shader" }, "shader/", "s_");
            CombineDictionary(model_res_bundle_dic, model_dep_res_bundle_dic);
            ClearBundleNames();
            SetAssetImporter(model_res_bundle_dic);
            Export(outpath, target);
            ExportBundleDependInfo(outpath);
            ClearBundleNames();
        }
        public static void ExportWaypointLine(string outpath, BuildTarget target)
        {
            model_res_bundle_dic = new Dictionary<string, string>();
            model_dep_res_bundle_dic = new Dictionary<string, string>();
            GetResMap(model_res_bundle_dic, "Assets/Models/WaypointLine", "WaypointLine/", "*.prefab", "");
            GetResDepencies(model_res_bundle_dic, ref model_dep_res_bundle_dic, new string[] { "png", "jpg" }, "texture/", "t_");
            //GetResDepencies(model_res_bundle_dic, ref model_dep_res_bundle_dic, new string[] { "mat" }, "mat/", "m_");
            GetResDepencies(model_res_bundle_dic, ref model_dep_res_bundle_dic, new string[] { "shader" }, "shader/", "s_");
            CombineDictionary(model_res_bundle_dic, model_dep_res_bundle_dic);
            ClearBundleNames();
            SetAssetImporter(model_res_bundle_dic);
            Export(outpath, target);
            ExportBundleDependInfo(outpath);
            ClearBundleNames();
        }

        
    }

}