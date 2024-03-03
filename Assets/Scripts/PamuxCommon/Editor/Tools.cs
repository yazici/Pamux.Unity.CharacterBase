using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using System.Text;
using System.IO;

namespace Pamux
{
  namespace Editor
  {
    public static class Tools
    {
      [MenuItem("Pamux/List Pamux Scripts", false, 0)]
      static public void ListPamuxScripts()
      {
        Debug.Assert(false);
        //string[] searchInFolders  = new string[] { "Assets/Scripts", "Assets/Audio", "Assets/Materials", "Assets/Models", "Assets/Prefabs", "Assets/Resources", "Assets/Scenes", "Assets/Scripts", "Assets/Shaders", "Assets/SpreadSheets", "Assets/Standard Assets", "Assets/StreamingAssets", "Assets/Textures" };
        string[] searchInFolders = new string[] { "Assets/Scripts" };

        var guids = AssetDatabase.FindAssets(@"", searchInFolders);
        foreach (var guid in guids)
        {
          var path = AssetDatabase.GUIDToAssetPath(guid);


          Debug.Log("Asset: " + path);
        }


      }

      [MenuItem("Pamux/Rename Assets", false, 0)]
      static public void RenameAssets()
      {
        Debug.Assert(false);
        //string[] searchInFolders  = new string[] { "Assets/Scripts", "Assets/Audio", "Assets/Materials", "Assets/Models", "Assets/Prefabs", "Assets/Resources", "Assets/Scenes", "Assets/Scripts", "Assets/Shaders", "Assets/SpreadSheets", "Assets/Standard Assets", "Assets/StreamingAssets", "Assets/Textures" };
        //string[] searchInFolders = new string[] { "Assets/Scripts", "Assets/Scripts/Pamux/Social", "Assets/Scripts/Pamux/Platforms", "Assets/Scripts/Shooter/UI" };
        string[] searchInFolders = new string[] { "Assets/Scripts/Pamux/Social" };

        var guids = AssetDatabase.FindAssets(@"", searchInFolders);
        foreach (var guid in guids)
        {
          var path = AssetDatabase.GUIDToAssetPath(guid);
          int lastSlashIndex = path.LastIndexOf("/");
          int dotIndex = path.LastIndexOf(".");
          if (dotIndex == -1)
          {
            continue;
          }
          var assetPath = path.Substring(0, lastSlashIndex);
          var assetName = path.Substring(lastSlashIndex + 1, dotIndex - lastSlashIndex - 1);



          if (assetName.Contains("_"))
          {
            int underscoreIndex = assetName.LastIndexOf("_");
            var newAssetName = assetName.Substring(underscoreIndex + 1);
            Debug.Log(assetPath + "/" + assetName + ".cs ->" + newAssetName);
            Debug.Log(AssetDatabase.RenameAsset(assetPath + "/" + assetName + ".cs", newAssetName));
          }
        }
      }


      [MenuItem("Pamux/Move Assets", false, 0)]
      static public void MoveAssets()
      {
        Debug.Assert(false);
        //string[] searchInFolders  = new string[] { "Assets/Scripts", "Assets/Audio", "Assets/Materials", "Assets/Models", "Assets/Prefabs", "Assets/Resources", "Assets/Scenes", "Assets/Scripts", "Assets/Shaders", "Assets/SpreadSheets", "Assets/Standard Assets", "Assets/StreamingAssets", "Assets/Textures" };
        //string[] searchInFolders = new string[] { "Assets/Scripts", "Assets/Scripts/Pamux/Social", "Assets/Scripts/Pamux/Platforms", "Assets/Scripts/Shooter/UI" };
        string[] searchInFolders = new string[] { "Assets/Scripts/Pamux/Social" };

        var guids = AssetDatabase.FindAssets(@"", searchInFolders);
        foreach (var guid in guids)
        {
          var path = AssetDatabase.GUIDToAssetPath(guid);
          int lastSlashIndex = path.LastIndexOf("/");
          var assetName = path.Substring(lastSlashIndex + 1);

          //Debug.Log("Assets/Scripts/Pamux/Social/" + assetName + ".cs ->" + "Assets/Scripts/Pamux/SocialNetworks/" + assetName);
          Debug.Log(AssetDatabase.MoveAsset("Assets/Scripts/Pamux/Social/" + assetName, "Assets/Scripts/Pamux/SocialNetworks/" + assetName));
        }
      }



      [MenuItem("Pamux/Insert Namespaces", false, 0)]
      static public void InsertNamespaces()
      {
        Debug.Assert(false);
        string[] searchInFolders = new string[] { "Assets/Scripts", "Assets/Scripts/Pamux/Social", "Assets/Scripts/Pamux/Platforms", "Assets/Scripts/Shooter/UI" };

        var guids = AssetDatabase.FindAssets(@"", searchInFolders);
        foreach (var guid in guids)
        {
          var path = AssetDatabase.GUIDToAssetPath(guid);
          if (path.EndsWith(".cs"))
          {
            InsertNamespace(@"D:\Workspace\Unity\Zodiac\" + path.Replace('/', '\\'));
          }
        }
      }

      private static void InsertNamespace(string path)
      {
        Debug.Log(path);
        var file = new StreamReader(path);
        string line;
        StringBuilder sb = new StringBuilder();

        bool insertedNamespace = false;
        string indent = "";
        while ((line = file.ReadLine()) != null)
        {
          if (!insertedNamespace)
          {
            var trimmed = line.Trim();
            if (trimmed != "" && !trimmed.StartsWith("using"))
            {
              sb.AppendLine("namespace Pamux");
              sb.AppendLine("{");
              insertedNamespace = true;
              indent = "  ";
            }
          }
          sb.AppendLine(indent + line.TrimEnd());
        }

        file.Close();

        if (insertedNamespace)
        {
          sb.AppendLine("}");
        }

        File.WriteAllText(path, sb.ToString());
      }
    }
  }
}

