using System.Collections.Generic;
using UnityEngine;

namespace Pamux
{
  public class GameArea2D
  {
      public static GameArea2D INSTANCE = new GameArea2D(-7.5f, 10.0f, 7.5f, -10.0f);
  
      private static List<Vector3> L2R = new List<Vector3>
      {
          new Vector3(INSTANCE.left-4.5f, 0.0f, 0.0f),
          new Vector3(INSTANCE.right+4.5f, 0.0f, 0.0f),
      };
      private static List<Vector3> R2L = new List<Vector3>
      {
          new Vector3(INSTANCE.right+4.5f, 0.0f, 0.0f),
          new Vector3(INSTANCE.left-4.5f, 0.0f, 0.0f),
      };
      private static List<Vector3> T2B = new List<Vector3>
      {
          new Vector3(0.0f, 0.0f, INSTANCE.top+5.0f),
          new Vector3(0.0f, 0.0f, INSTANCE.bottom-5.0f),
      };
      private static List<Vector3> B2T = new List<Vector3>
      {
          new Vector3(0.0f, 0.0f, INSTANCE.bottom-5.0f),
          new Vector3(0.0f, 0.0f, INSTANCE.top+5.0f),
      };
  
      private static List<Vector3> TL2BR = new List<Vector3>
      {
          new Vector3(INSTANCE.left-4.5f, 0.0f, INSTANCE.top+5.0f),
          new Vector3(INSTANCE.right+4.5f, 0.0f, INSTANCE.right+4.5f),
      };
      private static List<Vector3> TR2BL = new List<Vector3>
      {
          new Vector3(INSTANCE.right+4.5f, 0.0f, INSTANCE.top+5.0f),
          new Vector3(INSTANCE.left-4.5f, 0.0f, INSTANCE.bottom-5.0f),
      };
  
      private static List<Vector3> BL2TR = new List<Vector3>
      {
          new Vector3(INSTANCE.left-4.5f, 0.0f, INSTANCE.bottom-5.0f),
          new Vector3(INSTANCE.right+4.5f, 0.0f, INSTANCE.top+5.0f),
      };
      private static List<Vector3> BR2TL = new List<Vector3>
      {
          new Vector3(INSTANCE.right+4.5f, 0.0f, INSTANCE.bottom-5.0f),
          new Vector3(INSTANCE.left-4.5f, 0.0f, INSTANCE.top+5.0f),
      };
  
      public static Dictionary<string, List<Vector3>> PATHS = new Dictionary<string, List<Vector3>>();
  
      public float top;
      public float bottom;
      public float left;
      public float right;
  
      static GameArea2D()
      {
          PATHS["l2r"] = L2R;
          PATHS["r2l"] = R2L;
          PATHS["t2b"] = T2B;
          PATHS["b2t"] = B2T;
  
          PATHS["tl2br"] = TL2BR;
          PATHS["tr2bl"] = TR2BL;
          PATHS["bl2tr"] = BL2TR;
          PATHS["br2tl"] = BR2TL;
      }
  
      internal GameArea2D(float left, float top, float right, float bottom)
      {
          this.left = left;
          this.top = top;
          this.right = right;
          this.bottom = bottom;
      }
  
      internal Vector3 Clamp(Vector3 position)
      {
          return new Vector3
          (
              Mathf.Clamp(position.x, left, right),
              0.0f,
              Mathf.Clamp(position.z, bottom, top)
          );
      }
  
      internal static Vector3[] GetPath(string pathName)
      {
          if (PATHS.ContainsKey(pathName.ToLower()))
          {
              return PATHS[pathName.ToLower()].ToArray();
          }
          //return PATHS[pathName.ToLower()].ToArray();
          return iTweenPath.GetPath(pathName);
      }
  
      internal static Vector3[] GetPath(string pathName, PathVariants pathVariant)
      {
          Vector3[] gp = GetPath(pathName);
          List<Vector3> gpv = new List<Vector3>();
  
          if (pathVariant == PathVariants.None)
          {
              return gp;
          }
  
          if (pathVariant == PathVariants.Reverse)
          {
              foreach(var g in gp)
              {
                  gpv.Insert(0, g);
              }
          }
          else if (pathVariant == PathVariants.Horizontal)
          {
              foreach (var g in gp)
              {
                  gpv.Add(new Vector3(-g.x, g.y, g.z));
              }
          }
          else if (pathVariant == PathVariants.RHorizontal)
          {
              foreach (var g in gp)
              {
                  gpv.Insert(0, new Vector3(-g.x, g.y, g.z));
              }
          }
          else if (pathVariant == PathVariants.Vertical)
          {
              foreach (var g in gp)
              {
                  gpv.Add(new Vector3(g.x, g.y, -g.z));
              }
          }
          else if (pathVariant == PathVariants.RVertical)
          {
              foreach (var g in gp)
              {
                  gpv.Insert(0, new Vector3(g.x, g.y, -g.z));
              }
          }
          else if (pathVariant == PathVariants.Center)
          {
              foreach (var g in gp)
              {
                  gpv.Add(new Vector3(-g.x, g.y, -g.z));
              }
          }
          else if (pathVariant == PathVariants.RCenter)
          {
              foreach (var g in gp)
              {
                  gpv.Insert(0, new Vector3(-g.x, g.y, -g.z));
              }
          }
  
          return gpv.ToArray();
      }
  
      internal static Vector3[] GetPath(string pathName, PathVariants pathVariant, Vector3 offset)
      {
          Vector3[] path = GameArea2D.GetPath(pathName, pathVariant);
          if (path == null)
          {
              return null;
          }
          Vector3[] offsetPath = new Vector3[path.Length];
          for (int i = 0; i < path.Length; ++i)
          {
              offsetPath[i] = path[i] + offset;
          }
          return offsetPath;
  
      }
  
  }
}
