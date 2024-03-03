using UnityEngine;
using System.Collections;

namespace Pamux
{
  public class KeyboardHandler : PlayerInputHandler
  {
      private KeyCode[] inventoryActivationKeys = new KeyCode[]
      {
          KeyCode.Alpha1,
          KeyCode.Alpha2,
          KeyCode.Alpha3,
          KeyCode.Alpha4,
          KeyCode.Alpha5,
          KeyCode.Alpha6
      };
  
  	void Update ()
  	{
          for (int i = 0; i < inventoryActivationKeys.Length; ++i )
          {
              if (Input.GetKeyUp(inventoryActivationKeys[i]))
              {
                  player.UseInventory(i);
                  return;
              }
          }
  
  	}
  
  
  }
}
