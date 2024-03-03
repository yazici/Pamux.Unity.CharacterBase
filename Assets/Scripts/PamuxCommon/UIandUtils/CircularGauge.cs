using UnityEngine;
using System.Collections;

namespace Pamux
{
  public class CircularGauge : Gauge
  {
    private BuildCircleMesh bg;
    private BuildCircleMesh fg;

    protected override void SetFG()
    {
      if (fg != null)
      {
        fg.endAngle = (_v * 360.0f) / fullValue;
      }
    }

    void Awake()
    {
      BaseAwake();

      bg = this.gameObject.GetComponent<BuildCircleMesh>();
      bgMR = this.gameObject.GetComponent<MeshRenderer>();

      fg = fgObject.AddComponent<BuildCircleMesh>();
      fgMR = fgObject.GetComponent<MeshRenderer>();

      fgMR.material = bgMR.material;

      fg.innerRadius = bg.innerRadius;
      fg.circleWidth = bg.circleWidth;
    }
  }
}
