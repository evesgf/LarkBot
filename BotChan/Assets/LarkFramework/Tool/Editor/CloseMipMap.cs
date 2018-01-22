using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace LarkFramework.Tool
{
    public class CloseMipMap : UnityEditor.Editor
    {
        [MenuItem("Lark Tools/Close All Mip Map")]
        public static void SetMipMap()
        {
            //查找指定路径下指定类型的所有资源，返回的是资源GUID
            string[] guids = AssetDatabase.FindAssets("t:Texture");
            //从GUID获得资源所在路径
            List<string> paths = new List<string>();
            guids.ToList().ForEach(m => paths.Add(AssetDatabase.GUIDToAssetPath(m)));
            //从路径获得该资源
            List<TextureImporter> importers = new List<TextureImporter>();
            paths.ForEach(p => importers.Add(AssetImporter.GetAtPath(p) as TextureImporter));
            //下面就可以对该资源做任何你想要的操作了
            importers.ForEach(i => { if (i) { i.mipmapEnabled = false; Debug.Log("set mipmap disable."); } else Debug.Log("is null"); });

        }
    }
}
