using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Runtime.InteropServices;
using System;
using System.IO;
using System.Text;

namespace C_Framework
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]

    public class OpenDialogFile
    {
        public int structSize = 0;
        public IntPtr dlgOwner = IntPtr.Zero;
        public IntPtr instance = IntPtr.Zero;
        public String filter = null;
        public String customFilter = null;
        public int maxCustFilter = 0;
        public int filterIndex = 0;
        public String file = null;
        public int maxFile = 0;
        public String fileTitle = null;
        public int maxFileTitle = 0;
        public String initialDir = null;
        public String title = null;
        public int flags = 0;
        public short fileOffset = 0;
        public short fileExtension = 0;
        public String defExt = null;
        public IntPtr custData = IntPtr.Zero;
        public IntPtr hook = IntPtr.Zero;
        public String templateName = null;
        public IntPtr reservedPtr = IntPtr.Zero;
        public int reservedInt = 0;
        public int flagsEx = 0;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class OpenDialogDir
    {
        public IntPtr hwndOwner = IntPtr.Zero;
        public IntPtr pidlRoot = IntPtr.Zero;
        public String pszDisplayName = null;
        public String lpszTitle = null;
        public UInt32 ulFlags = 0;
        public IntPtr lpfn = IntPtr.Zero;
        public IntPtr lParam = IntPtr.Zero;
        public int iImage = 0;
    }

    public class DllOpenFileDialog
    {
        [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
        public static extern bool GetOpenFileName([In, Out] OpenDialogFile ofn);

        [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
        public static extern bool GetSaveFileName([In, Out] OpenDialogFile ofn);

        [DllImport("shell32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
        public static extern IntPtr SHBrowseForFolder([In, Out] OpenDialogDir ofn);

        [DllImport("shell32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
        public static extern bool SHGetPathFromIDList([In] IntPtr pidl, [In, Out] char[] fileName);

    }




    public class LuacodeCreatWindow : EditorWindow
    {



        public class Dlg
        {
            public GameObject prefab;
            public string luaName;
            public string className;
            public int index;
            public bool inited = false;
            public Dlg(int index)
            {
                this.index = index;
            }
        }

        [MenuItem("Export/Luacode for UI")]
        public static void ShowLuacodeCreator()
        {
            LuacodeCreatWindow window = EditorWindow.GetWindow<LuacodeCreatWindow>();
            window.Show();
            outDir = EditorPrefs.GetString("lua_dir");
        }

        Dictionary<int, Dlg> dlg_dic = new Dictionary<int, Dlg>();
        List<int> dlg_to_delete = new List<int>();
        int index = 0;
        Vector2 view;
        public static string outDir;

        private void OnGUI()
        {
            view = EditorGUILayout.BeginScrollView(this.view);
            ShowMenu();
            EditorGUILayout.EndScrollView();

        }


        private void ShowMenu()
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("+", EditorStyles.toolbarButton, GUILayout.Width(50)))
            {
                dlg_dic.Add(index, new Dlg(index));
                index++;
            }
            if (GUILayout.Button("导出路径", EditorStyles.toolbarButton, GUILayout.Width(80)))
            {
                string selectDir = SelectOutDir();
                if (string.IsNullOrEmpty(selectDir)) return;
                outDir = selectDir;
            }
            if (GUILayout.Button("永久保存", EditorStyles.toolbarButton, GUILayout.Width(80)))
            {
                EditorPrefs.SetString("lua_dir", outDir);
            }
            GUILayout.Label(outDir, GUILayout.Width(300));
            if (GUILayout.Button("导出lua", EditorStyles.toolbarButton, GUILayout.Width(80)))
            {
                foreach (var pair in dlg_dic)
                {
                    if (pair.Value.prefab != null)
                    {
                        ExportLua(pair.Value);
                    }
                }
                System.Diagnostics.Process.Start(outDir);
            }
            EditorGUILayout.EndHorizontal();
            foreach (var pair in dlg_dic)
            {
                ShowDlg(pair.Value);
            }
            foreach (var key in dlg_to_delete)
            {
                dlg_dic.Remove(key);
            }
            dlg_to_delete.Clear();
        }

        private void ShowDlg(Dlg dlg)
        {
            EditorGUILayout.BeginHorizontal();
            dlg.prefab = EditorGUILayout.ObjectField(dlg.prefab, typeof(GameObject), false, GUILayout.Width(250)) as GameObject;
            if (dlg.prefab != null && dlg.inited == false)
            {
                dlg.luaName = string.Format("ui/{0}", dlg.prefab.name);
                dlg.className = UpperHead(string.Format("{0}", dlg.prefab.name));
                dlg.inited = true;
            }
            GUILayout.Label("文件名", GUILayout.Width(40));
            dlg.luaName = EditorGUILayout.TextField(dlg.luaName, GUILayout.Width(120));
            GUILayout.Label("类名", GUILayout.Width(40));
            dlg.className = EditorGUILayout.TextField(dlg.className, GUILayout.Width(120));
            if (GUILayout.Button("-", EditorStyles.toolbarButton, GUILayout.Width(50)))
            {
                dlg_to_delete.Add(dlg.index);
            }
            EditorGUILayout.EndHorizontal();
        }


        public string SelectOutDir()
        {
            OpenDialogDir ofn2 = new OpenDialogDir();
            ofn2.pszDisplayName = new string(new char[2000]); ;     // 存放目录路径缓冲区  
            ofn2.lpszTitle = "Open Project";// 标题  
                                            //ofn2.ulFlags = BIF_NEWDIALOGSTYLE | BIF_EDITBOX; // 新的样式,带编辑框  
            IntPtr pidlPtr = DllOpenFileDialog.SHBrowseForFolder(ofn2);

            char[] charArray = new char[2000];
            for (int i = 0; i < 2000; i++)
                charArray[i] = '\0';

            DllOpenFileDialog.SHGetPathFromIDList(pidlPtr, charArray);
            string fullDirPath = new String(charArray);
            fullDirPath = fullDirPath.Substring(0, fullDirPath.IndexOf('\0'));
            return fullDirPath;
        }


        public string GetLuaString(Dlg dlg)
        {
            HashSet<string> set = new HashSet<string>();
            GameObject go = GameObject.Instantiate(dlg.prefab);
            UIComponentCollector collector = go.AddComponent<UIComponentCollector>();
            collector.CollectCompoentNameForEditor(dlg.prefab.transform, ref set);

            StringBuilder sb = new StringBuilder();
            foreach(string name in set)
            {
                sb.AppendLine(string.Format("   --self.uitable.{0}", name));
            }
            GameObject.DestroyImmediate(go);
            return string.Format(templete, dlg.className, dlg.luaName, sb.ToString());
        }

        public void ExportLua(Dlg dlg)
        {
            string fileName = dlg.luaName + ".lua";
            string luaFileName = Path.Combine(outDir, fileName);
            WriteText(luaFileName, GetLuaString(dlg));
        }

        public string StandLizePath(string path)
        {
            string pathReplace = path.Replace(@"\", @"/");
            string pathLower = pathReplace.ToLower();
            return pathLower;
        }

        public void WriteText(string path, string content)
        {
            string standPath = StandLizePath(path);
            int index = standPath.LastIndexOf('/');
            string dir = index < 0 ? path : standPath.Substring(0, index);
            if (Directory.Exists(dir) == false)
            {
                Directory.CreateDirectory(dir);
                DirectoryInfo info = new DirectoryInfo(dir);
            }
            File.WriteAllText(standPath, content);
        }


        public string UpperHead(string src)
        {
            List<int> index_need_upper = new List<int>();
            int length = src.Length;
            char[] des = new char[length];
            for (int i = 0; i < length; i++)
            {
                if(src[i] == '_')
                {
                    index_need_upper.Add(i + 1);
                }
                des[i] = src[i];
            }
            foreach(int index in index_need_upper)
            {
                des[index] = Char.ToUpper(des[index]);
            }
            des[0] = Char.ToUpper(des[0]);
            return new string(des);
        }


        string templete = @"
{0} = DefClass('{1}', Base_Dialog)

local instance

function {0}:ctor()
    instance = self
end

function {0}:__delete()
    instance = nil
end

function {0}.hotfix()
    if instance == nil then return end
end

function {0}:start(args)
{2}
end

function {0}:on_enter()

end

function {0}:on_exit()

end

function {0}:on_re_enter()

end

function {0}:on_change_language()

end
";

    }
}

