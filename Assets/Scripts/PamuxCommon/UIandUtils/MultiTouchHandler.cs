using UnityEngine;
using System.Collections;

namespace Pamux
{
  public class MultiTouchHandler : PlayerInputHandler
  {
  	void Update ()
  	{
          if (!Input.multiTouchEnabled || Input.touchCount == 0)
          {
              return;
          }
          Debug.Log(Input.touchCount);
          if (Input.touchCount == 1)
          {
              if (Input.touches[0].position.y < 100)
              {
  
              }
              else
              {
                  player.MoveToScreenPoint(Input.touches[0].position);
              }
  
          }
          else
          {
              HandleMultiTouch(Input.touches);
          }
  	}
  
      private void HandleMultiTouch(Touch[] touch)
      {
  
      }
  
  
  }
}
