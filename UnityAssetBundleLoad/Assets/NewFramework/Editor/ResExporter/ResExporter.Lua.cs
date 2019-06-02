using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using UnityEngine;
using System;

namespace C_Framework
{

    public partial class ResExporter
    {



        [MenuItem("Export/Windows/Export Lua For Windows")]
        public static void ExportLuaForWindows()
        {
            ExportLua(EditorConst.windows_md5_root);
        }

        [MenuItem("Export/Android/Export Lua For Android")]
        public static void ExportLuaForAndroid()
        {
            ExportLua(EditorConst.andorid_md5_root);
        }

        [MenuItem("Export/IOS/Export Lua Model IOS")]
        public static void ExportLuaForIOS()
        {
            ExportLua(EditorConst.ios_md5_root);
        }



        public static void ExportLua(string outpath)
        {
            DirectoryInfo lua_dirInfo = new DirectoryInfo(EditorConst.lua_path_in_editor);
            DirectoryInfo pb_dirInfo = new DirectoryInfo(EditorConst.pb_path_in_editor);
            FileInfo[] lua_files = lua_dirInfo.GetFiles("*.lua", SearchOption.AllDirectories);
            FileInfo[] pb_files = pb_dirInfo.GetFiles("*.pb", SearchOption.AllDirectories);
            List<FileInfo> files = new List<FileInfo>();
            files.AddRange(lua_files);
            files.AddRange(pb_files);
            int count = files.Count;
            float finished = 0;

            foreach (FileInfo file in files)
            {
                int index = file.FullName.IndexOf("gamedata_encrypt");
                string copy_file_name = file.FullName.Substring(index);
                string new_file_name = MD5Util.StandardlizePath(Path.Combine(outpath, copy_file_name));
                string dir_name = new_file_name.Substring(0, new_file_name.LastIndexOf('/'));
                if (Directory.Exists(dir_name) == false)
                {
                    Directory.CreateDirectory(dir_name);
                }

                byte[] src_bytes = File.ReadAllBytes(file.FullName);
                if (LogicGlobalVars.GetInstance().bLoadScriptEncrypted)
                {
                    //转为16进制字符串
                    //string hex = MD5Util.ByteToHexStr(src_bytes);
                    //加密
                    string hex = MD5Util.ByteToHexStr(src_bytes);
                    File.WriteAllText(new_file_name, hex);
                }
                else
                {
                    File.WriteAllBytes(new_file_name, src_bytes);
                }
                finished++;
                EditorUtility.DisplayProgressBar("convert...", string.Format("{0}/{1}", finished, count), finished / count);
            }
            EditorUtility.ClearProgressBar();
        }
    }

}