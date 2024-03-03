using UnityEngine;
using System.Collections;

namespace Pamux
{
  public class TextureDraw
  {
  
      static void Circle(Texture2D texture, int cx, int cy, int radius, Color col)
      {
          var y = radius;
          var d = 1 / 4 - radius;
          var end = Mathf.Ceil(radius / Mathf.Sqrt(2));
  
          for (int x = 0; x <= end; x++)
          {
              texture.SetPixel(cx + x, cy + y, col);
              texture.SetPixel(cx + x, cy - y, col);
              texture.SetPixel(cx - x, cy + y, col);
              texture.SetPixel(cx - x, cy - y, col);
              texture.SetPixel(cx + y, cy + x, col);
              texture.SetPixel(cx - y, cy + x, col);
              texture.SetPixel(cx + y, cy - x, col);
              texture.SetPixel(cx - y, cy - x, col);
  
              d += 2 * x + 1;
              if (d > 0)
              {
                  d += 2 - 2 * y--;
              }
          }
      }
  
  
      static void Line(Texture2D texture, int x1, int y1, int x2, int y2, Color col)
      {
          int stepx;
          int stepy;
          int fraction;
          int dy = y2 - y1;
          int dx = x2 - x1;
  
          if (dy < 0)
          {
              dy = -dy;
              stepy = -1;
          }
          else
          {
              stepy = 1;
          }
          if (dx < 0)
          {
              dx = -dx; stepx = -1;
          }
          else
          {
              stepx = 1;
          }
          dy <<= 1;
          dx <<= 1;
  
          texture.SetPixel(x1, y1, col);
          if (dx > dy)
          {
              fraction = dy - (dx >> 1);
              while (x1 != x2)
              {
                  if (fraction >= 0)
                  {
                      y1 += stepy;
                      fraction -= dx;
                  }
                  x1 += stepx;
                  fraction += dy;
                  texture.SetPixel(x1, y1, col);
              }
          }
          else
          {
              fraction = dx - (dy >> 1);
              while (y1 != y2)
              {
                  if (fraction >= 0)
                  {
                      x1 += stepx;
                      fraction -= dy;
                  }
                  y1 += stepy;
                  fraction += dx;
                  texture.SetPixel(x1, y1, col);
              }
          }
      }
  }
}
