using UnityEngine;
using System.Collections;

namespace Pamux
{
  public class Attractor : MonoBehaviour
  {
      [System.Serializable]
      public class AttractorLevelsParameters
      {
          public float strength;
          public float rangeRadius;
  
          internal AttractorLevelsParameters(float strength, float rangeRadius)
          {
              this.strength = strength;
              this.rangeRadius = rangeRadius;
          }
      }
  
      internal AttractorLevelsParameters[] levelParameters = new AttractorLevelsParameters[]
      {
          new AttractorLevelsParameters(1.0f, 2.0f),
          new AttractorLevelsParameters(2.0f, 2.0f),
          new AttractorLevelsParameters(3.0f, 3.0f),
          new AttractorLevelsParameters(4.0f, 3.0f),
          new AttractorLevelsParameters(5.0f, 5.0f),
          new AttractorLevelsParameters(6.0f, 5.0f),
          new AttractorLevelsParameters(7.0f, 6.0f),
          new AttractorLevelsParameters(8.0f, 7.0f),
          new AttractorLevelsParameters(10.0f, 8.0f),
          new AttractorLevelsParameters(15.0f, 9.0f),
      };
  
      public float strength { get { return _featureLevel < 0 ? 0.0f : levelParameters[_featureLevel].strength; } }
      public float rangeRadius { get { return _featureLevel < 0 ? 0.0f : levelParameters[_featureLevel].rangeRadius; } }
  
      private int _featureLevel;
  
      public int featureLevel { set { _featureLevel = value - 1; } }
  }
}
