using UnityEngine;
using System.Collections;

namespace Pamux
{
  public class LinearGauge : Gauge
  {
    protected override void SetFG()
    {
    }

    protected Vector3 pos;

    void Awake()
    {
      BaseAwake();

      pos = Camera.main.WorldToScreenPoint(gameObject.transform.position) - Abstracts.UI.uiCameraOffset;

      bp1 = new Vector2(pos.x, pos.y);
      bp2 = new Vector2(pos.x + 100.0f, pos.y);

      p1 = new Vector2(bp1.x + borderWidth, pos.y);
      p2 = new Vector2(bp2.x - borderWidth, pos.y);

    }
    Color color = Color.blue;
    float width = 6.0f;
    Color borderColor = Color.green;
    float borderWidth = 3.0f;

    Vector2 p1;
    Vector2 p2;
    Vector2 bp1;
    Vector2 bp2;

    void OnGUI()
    {
      bp2.x += 0.1f;
      Drawing.DrawLine(bp1, bp2, borderColor, borderWidth * 2 + width);
      Drawing.DrawLine(p1, p2, color, width);
    }
  }
}
