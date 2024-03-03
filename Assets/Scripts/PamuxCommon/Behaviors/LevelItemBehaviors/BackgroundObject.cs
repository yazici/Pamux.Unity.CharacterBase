using UnityEngine;
using System.Collections;

namespace Pamux
{
  public class BackgroundObject : MonoBehaviour
  {
      internal BackgroundStyleInfo styleInfo;
  
  
      void SetStyle(string name)
      {
          var stylable = this.gameObject.GetComponent<Styleable>();
          stylable.SetStyle(name);
          styleInfo = stylable.styleInfo as BackgroundStyleInfo;
          if (styleInfo == null)
          {
              return;
          }
      }
  
  }
}
