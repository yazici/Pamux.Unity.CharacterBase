using UnityEngine;
using System.Collections;

namespace Pamux
{
  public class MouseHandler : PlayerInputHandler
  {
      public bool isMouseCaptured = true;
  
  	void Update ()
  	{
          if (!isMouseCaptured)
          {
              return;
          }
          player.MoveToScreenPoint(Input.mousePosition);
  	}
  
  
  }
}
