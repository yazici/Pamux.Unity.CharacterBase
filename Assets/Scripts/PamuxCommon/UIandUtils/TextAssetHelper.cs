
namespace Pamux
{
    using UnityEngine;
    using System.Collections.Generic;
    using System.IO;
    using Pamux.Interfaces;

    public static class TextAssetHelper
  {
      internal static bool Load(string path, ITextAssetHandler tah)
      {
          TextAsset ta = Resources.Load(path, typeof(TextAsset)) as TextAsset;
          if (ta == null)
          {
              Debug.LogError(path);
              return false;
          }
          StringReader reader = new StringReader(ta.text);
          Dictionary<string, int> headerNameToColMap = null;
          string[] previousFields = null;
          while (true)
          {
              string line = reader.ReadLine();
              if (line == null)
              {
                  break;
              }
              string l = line.Trim().ToLower();
              if (l.Length == 0 || l.StartsWith("//"))
              {
                  continue;
              }
  
              if (l.StartsWith("@"))
              {
                  if (l == "@end")
                  {
                      break;
                  }
                  string[] s = l.Substring(1).Split('=');
                  tah.SetVariable(s[0].Trim(), s[1].Trim());
                  continue;
              }
  
              string[] fields = l.Split(',');
  
              for (int i = 0; i < fields.Length; ++i)
              {
                  fields[i] = fields[i].Trim();
              }
  
              if (headerNameToColMap == null)
              {
                headerNameToColMap = new Dictionary<string, int>();
                for (int colId = 0; colId < fields.Length; ++colId)
                {
                  headerNameToColMap[fields[colId]] = colId;
                }
              }
              else
              {
                if (previousFields != null)
                {
                  for (int i = 0; i < fields.Length;++i)
                  {
                    if (fields[i].Trim().Length == 0)
                    {
                      fields[i] = previousFields[i];
                    }
                  }
                }
                tah.AddItems(headerNameToColMap, fields);
                previousFields = fields;
              }
          }
  
          tah.OnItemsReady();
  
          return true;
      }
  }
}
