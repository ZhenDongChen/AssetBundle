using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace C_Framework
{

    public partial class ResExporter
    {

        public static Dictionary<string, string> ui_res_bundle_dic;
        public static Dictionary<string, string> ui_dep_res_bundle_dic;

        [MenuItem("Export/Windows/Export UI For Windows")]
        public static void ExportUIForWindows()
        {
            ExportUI(EditorConst.windows_out_path, BuildTarget.StandaloneWindows);
        }

        [MenuItem("Export/Android/Export UI For Android")]
        public static void ExportUIForAndroid()
        {
            ExportUI(EditorConst.andorid_out_path, BuildTarget.Android);
        }

        [MenuItem("Export/IOS/Export UI For IOS")]
        public static void ExportUIForIOS()
        {
            ExportUI(EditorConst.ios_out_path, BuildTarget.iOS);
        }


        public static void ExportUI(string outPath, BuildTarget target)
        {
            ui_res_bundle_dic = new Dictionary<string, string>();
            ui_dep_res_bundle_dic = new Dictionary<string, string>();
            //清除eximage 和 exrawimage 信息
            RemoveUIDependForEx();
            //字体文件
            GetResMap(ui_res_bundle_dic, "Assets/NewFramework/Interface/Font", "font/", "*.ttf", "f_");
            GetResMap(ui_res_bundle_dic, "Assets/NewFramework/Interface/UI/Prefab", "ui/", "*.prefab", "ui_");
            GetResDepencies(ui_res_bundle_dic, ref ui_dep_res_bundle_dic, new string[] { "png", "jpg" }, "texture/", "t_");
            //GetResDepencies(ui_res_bundle_dic, ref ui_dep_res_bundle_dic, new string[] { "mat" }, "materia/", "m_");
            GetResDepencies(ui_res_bundle_dic, ref ui_dep_res_bundle_dic, new string[] { "shader" }, "shader/", "s_");
            CombineDictionary(ui_res_bundle_dic, ui_dep_res_bundle_dic);
            ClearBundleNames();
            //texture单独打包
            GetResMap(ui_res_bundle_dic, "Assets/NewFramework/Interface/UI/Texture", "texture/", "*.png", "t_");
            //sprite单独打包
            Dictionary<string, string> sprite_map = GetSpriteBundlesMap("Assets/NewFramework/Interface/UI/Altlas", "texture/", "*.png", "a_");
            CombineDictionary(ui_res_bundle_dic, sprite_map);
            SetAssetImporter(ui_res_bundle_dic);
            Export(outPath, target);
            ExportBundleDependInfo(outPath);
            ClearBundleNames();
        }

        public static Dictionary<string, string> GetSpriteBundlesMap(string inputPath, string outPath, string search_pattern, string prefix)
        {
            Dictionary<string, string> sprite_map = new Dictionary<string, string>();
            Dictionary<string, string> sprite_pack_map = new Dictionary<string, string>();
            GetResMap(sprite_map, inputPath, outPath, search_pattern, prefix);
            foreach (var pair in sprite_map)
            {
                string path = pair.Key;
                TextureImporter textureImporter = TextureImporter.GetAtPath(path) as TextureImporter;
                textureImporter.textureType = TextureImporterType.Sprite;
                string dirPath = Path.GetDirectoryName(path);
                string dirName = Path.GetFileName(dirPath);
                textureImporter.spritePackingTag = dirName;

                sprite_pack_map[path] = string.Format("{0}{1}{2}", outPath, prefix, dirName);
            }
            return sprite_pack_map;
        }

        public static void RemoveUIDependForEx()
        {
            Dictionary<string, string> prefab_dic = new Dictionary<string, string>();
            GetResMap(prefab_dic, "Assets/NewFramework/Interface/UI/Prefab", "ui/", "*.prefab", "ui_");
            foreach (string path in prefab_dic.Keys)
            {
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                ClearExImage(prefab.transform);
                EditorUtility.SetDirty(prefab);
            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }


        public static void ClearExImage(Transform transform)
        {
            int childCount = transform.childCount;
            ExImage eximage = transform.GetComponent<ExImage>();
            if (eximage != null)
            {
                eximage.sprite = null;
            }
            ExRawImage exRawimage = transform.GetComponent<ExRawImage>();
            if (exRawimage != null)
            {
                exRawimage.texture = null;
            }

            for (int i = 0; i < childCount; i++)
            {
                ClearExImage(transform.GetChild(i));
            }
        }

    }

}