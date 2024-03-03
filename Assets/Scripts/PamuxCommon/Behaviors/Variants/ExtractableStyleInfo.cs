using UnityEngine;
using System.Collections;

namespace Pamux
{
  public class ExtractableStyleInfo : StyleInfo
  {
      internal void SetExtractable(bool isExtractable)
      {
          if (!isExtractable)
          {
              TweenColor tc = GetComponent<TweenColor>();
              tc.value = Color.green;
              tc.enabled = isExtractable;
          }
      }
  }
}
