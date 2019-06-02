using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;

namespace C_Framework {

    public partial class ResExporter
    {


        public static Dictionary<string, string> file_md5_dic;



        [MenuItem("Export/Windows/Export MD5 For Windows")]
        public static void ExportMD5ForWindows()
        {
            ExportMD5(EditorConst.windows_md5_root, EditorConst.windows_md5_file);
        }

        [MenuItem("Export/Android/Export MD5 For Android")]
        public static void ExportMD5ForAndroid()
        {
            ExportMD5(EditorConst.andorid_md5_root, EditorConst.andorid_md5_file);
        }

        [MenuItem("Export/IOS/Export MD5 For IOS")]
        public static void ExportMD5ForIOS()
        {
            ExportMD5(EditorConst.ios_md5_root, EditorConst.ios_md5_file);
        }


        public static void ExportMD5(string srcpath, string outpath)
        {
            file_md5_dic = new Dictionary<string, string>();
            DirectoryInfo dir = new DirectoryInfo(srcpath);
            FileInfo[] files = dir.GetFiles("*.*", SearchOption.AllDirectories);
            int count = files.Length;
            float finished = 0;

            foreach (FileInfo file in files)
            {
                string md5 = MD5Util.GetFileMD5(file.FullName);
                string relative_path = MD5Util.StandardlizePath(file.FullName.Substring(dir.FullName.Length + 1));
                file_md5_dic.Add(relative_path, md5);
                finished++;
                EditorUtility.DisplayProgressBar("md5 creator", string.Format("{0}/{1}", finished, count), finished / count);

            }
            if (file_md5_dic.ContainsKey("md5.txt"))
            {
                file_md5_dic.Remove("md5.txt");
            }
            StringBuilder sb = new StringBuilder();
            foreach (var pair in file_md5_dic)
            {
                sb.AppendLine(pair.Key + "|" + pair.Value);
            }
            File.WriteAllText(outpath, sb.ToString());
            EditorUtility.ClearProgressBar();
            System.Diagnostics.Process.Start(dir.FullName);
        }

    }

}
