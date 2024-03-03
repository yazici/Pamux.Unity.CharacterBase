using UnityEngine;
using System.Collections;
using System.Collections.Generic;



namespace Pamux
{
  public class ToolBarButton : MonoBehaviour
  {
      internal ToolBar parent;
      internal int index;
      internal UISprite innerSprite;
      internal float lastClick = -1.0f;
      internal void OnClick()
      {
          if (Time.time - lastClick < 0.2f)
          {
              return;
          }
  
          lastClick = Time.time;
          if (innerSprite.spriteName.Length != 0)
          {
              this.parent.OnClick(index, innerSprite);
          }
      }
  }
  
}
