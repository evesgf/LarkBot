using UnityEngine;
using System.IO;
using System.Text;
using System;
using UnityEditor;

namespace LarkFramework.Tool
{
    public static class QuickCreateFolder
    {
        [MenuItem("Lark Tools/Generate Project Folders")]
        public static void CreateBaseFolder()
        {
            GenerateFolder();

            BuildSetting.ProjectInitSettings();

            Debug.Log("Folders Created!");
        }

        private static void GenerateFolder()
        {
            //创建项目文件夹
            Directory.CreateDirectory(Application.dataPath + "/_Project");

            //文件路径
            string path = Application.dataPath + "/_Project/";

            CreateDirectoryAndFile(path, "Scripts");
            CreateDirectoryAndFile(path, "Resources");
            CreateDirectoryAndFile(path, "Scenes");
            CreateDirectoryAndFile(path, "Textures");
            CreateDirectoryAndFile(path, "Audios");
            CreateDirectoryAndFile(path, "Materials");
            CreateDirectoryAndFile(path, "Animations");
            CreateDirectoryAndFile(path, "Packages");
            CreateDirectoryAndFile(path, "Shaders");
            CreateDirectoryAndFile(path, "Prefabs");
            CreateDirectoryAndFile(path, "SandBoxs");
            CreateDirectoryAndFile(path, "_Documnets");
            CreateDirectoryAndFile(path, "Terrains");
            CreateDirectoryAndFile(path, "Effects");
            CreateDirectoryAndFile(path, "Models");

            CreateTxt(Application.dataPath + "/_Project/ReadMe");
            WriteTxt(Application.dataPath + "/_Project/ReadMe.txt", "Welcome to use LarkFramework：" + Application.productName + " CreateTime：" + DateTime.Now);

            AssetDatabase.Refresh();
        }

        /// <summary>
        /// 创建指定路径文件夹和说明文件
        /// 说明文件是为了防止空文件夹被版本管理干掉
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        private static void CreateDirectoryAndFile(string path, string name)
        {
            Directory.CreateDirectory(path + name);
            CreateTxt(path + name + "/" + name);
        }

        private static void CreateTxt(string path)
        {
            if (!File.Exists(path + ".txt"))
            {
                FileStream fs = new FileStream(path + ".txt", FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);

                sw.Close();
                fs.Close();

                WriteTxt(Application.dataPath + "/_Project/ReadMe.txt", "Welcome to use LarkFramework：" + Application.productName + " CreateTime：" + DateTime.Now);
            }
            else
            {
                Debug.LogWarning("txt说明文件创建失败，冲突文件：" + path + ".txt");
            }
        }

        private static void WriteTxt(string path, string context)
        {
            if (File.Exists(path))
            {
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                //ps:此处为简单的单行输入，多行输入需修改
                sw.WriteLine(context);

                sw.Close();
                fs.Close();
            }
            else
            {
                Debug.LogError("写入文件失败，冲突文件：" + path);
            }
        }
    }
}
