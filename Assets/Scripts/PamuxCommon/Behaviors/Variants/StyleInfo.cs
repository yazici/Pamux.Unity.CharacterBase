using UnityEngine;
using System.Collections;


namespace Pamux
{
  public class StyleInfo : MonoBehaviour
  {
      // Transform
      internal Transform initialTransform;
  
      // Rigidbody
      public float mass = 1;
      public float drag = 0;
      public float angularDrag = 0;
  
      // Other
      public bool detachFromParent;
  
      void Awake()
      {
          initialTransform = transform;
      }
  }
}
