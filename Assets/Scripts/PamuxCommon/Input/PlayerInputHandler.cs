using UnityEngine;
using System.Collections;

namespace Pamux
{
  public class PlayerInputHandler : MonoBehaviour
  {
      protected Player player;
  
      void Awake()
      {
          player = this.GetComponent<Player>();
      }
  }
}
